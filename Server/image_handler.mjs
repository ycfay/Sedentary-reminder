import path from 'path';
import fs from 'fs/promises';
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
export async function handleImageRequest(req, res) {
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