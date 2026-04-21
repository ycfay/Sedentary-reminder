import path from 'path';
import { readdir, mkdir, writeFile } from 'fs/promises';
import { createReadStream } from 'fs';


/**
 * 处理 libraries 相关的请求
 * @param {Array} libraries - 库文件数组
 * @param {Object} req - 请求对象
 * @param {Object} res - 响应对象
 */

// 缓存图片列表（避免每次 readdir）
let cachedImages = null;
let lastLoadTime = 0;
const CACHE_TTL = 10 * 1000; // 10秒缓存

const MIME_MAP = {
  '.jpg': 'image/jpeg',
  '.jpeg': 'image/jpeg',
  '.png': 'image/png',
  '.gif': 'image/gif',
  '.webp': 'image/webp',
};

const IMAGE_REGEX = /\.(jpg|jpeg|png|gif|webp)$/i;

async function loadImages(assetsPath) {
  const now = Date.now();
  // 缓存有效直接返回
  if (cachedImages && (now - lastLoadTime < CACHE_TTL)) {
    return cachedImages;
  }
  const files = await fs.readdir(assetsPath);
  const imageFiles = files.filter(file => IMAGE_REGEX.test(file));
  cachedImages = imageFiles;
  lastLoadTime = now;
  return imageFiles;
}

/**
 * 随机返回 assets 目录图片
 */
export async function handleLoadImage(req, res) {
  try {
    const assetsPath = path.join(process.cwd(), 'assets');

    const imageFiles = await loadImages(assetsPath);

    if (!imageFiles.length) {
      res.writeHead(404, { 'Content-Type': 'text/plain' });
      return res.end('No images found');
    }

    // 随机选取
    const randomFile = imageFiles[
      Math.floor(Math.random() * imageFiles.length)
    ];

    const filePath = path.join(assetsPath, randomFile);
    const ext = path.extname(randomFile).toLowerCase();

    const contentType = MIME_MAP[ext] || 'application/octet-stream';

    // 设置响应头
    res.writeHead(200, {
      'Content-Type': contentType,
      'Cache-Control': 'no-cache',
    });

    const stream = createReadStream(filePath);

    stream.pipe(res);

    stream.on('error', (err) => {
      console.error('Stream error:', err);
      if (!res.headersSent) {
        res.writeHead(500);
      }
      res.end();
    });

  } catch (err) {
    console.error('Handler error:', err);
    if (!res.headersSent) {
      res.writeHead(500, { 'Content-Type': 'text/plain' });
      res.end('Internal Server Error');
    }
  }
}

/**
 * 解析 multipart/form-data（简化版，仅支持单文件）
 */
function parseMultipart(req) {
  return new Promise((resolve, reject) => {
    const contentType = req.headers['content-type'] || '';
    const boundaryMatch = contentType.match(/boundary=(.+)$/);

    if (!boundaryMatch) {
      return reject(new Error('Invalid multipart/form-data'));
    }

    const boundary = '--' + boundaryMatch[1];
    let data = Buffer.alloc(0);

    req.on('data', chunk => {
      data = Buffer.concat([data, chunk]);
    });

    req.on('end', () => {
      const parts = data.toString('binary').split(boundary);

      for (let part of parts) {
        if (part.includes('Content-Disposition')) {
          const match = part.match(/filename="(.+?)"/);
          if (!match) continue;

          const filename = match[1];
          const ext = path.extname(filename).toLowerCase();

          const start = part.indexOf('\r\n\r\n') + 4;
          const end = part.lastIndexOf('\r\n');

          const fileBuffer = Buffer.from(
            part.substring(start, end),
            'binary'
          );

          return resolve({ ext, buffer: fileBuffer });
        }
      }

      reject(new Error('No file found'));
    });

    req.on('error', reject);
  });
}

/**
 * 获取下一个文件名（max + 1）
 */
async function getNextIndex(assetsPath) {
  const files = await readdir(assetsPath);

  let max = -1;

  for (const file of files) {
    const match = file.match(/^(\d+)\./);
    if (match) {
      const num = parseInt(match[1], 10);
      if (num > max) max = num;
    }
  }

  return max + 1;
}

/**
 * 上传图片处理
 */
export async function handleUploadImage(req, res) {
  try {
    const assetsPath = path.join(process.cwd(), 'assets');

    // 确保目录存在
    await mkdir(assetsPath, { recursive: true });

    // 解析上传文件
    const { ext, buffer } = await parseMultipart(req);

    // 校验格式
    if (!/\.(jpg|jpeg|png|gif|webp)$/i.test(ext)) {
      res.writeHead(400);
      return res.end('Invalid file type');
    }

    // 获取文件名（max + 1）
    let index = await getNextIndex(assetsPath);

    let filePath;
    let filename;

    // 简单防并发冲突（重试机制）
    for (let i = 0; i < 5; i++) {
      filename = `${index}${ext}`;
      filePath = path.join(assetsPath, filename);

      try {
        await writeFile(filePath, buffer, { flag: 'wx' });
        break; // 写入成功
      } catch (err) {
        if (err.code === 'EEXIST') {
          index++;
          continue;
        }
        throw err;
      }
    }

    // 返回图片
    res.writeHead(200, {
      'Content-Type': 'application/json',
    });

    res.end(JSON.stringify({
      success: true,
      filename,
      url: `/assets/${filename}`,
    }));

  } catch (err) {
    console.error(err);
    res.writeHead(500);
    res.end('Upload failed');
  }
}