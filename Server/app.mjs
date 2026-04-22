import { fileURLToPath } from "url";
import * as path from "path";
import express from "express";
import cors from "cors";
import net from "net";
import chalk from "chalk";
import morgan from "morgan";
import compression from "compression";
import { handleLoadImage, handleUploadImage} from "./image_handler.mjs";

const serverPort = 5180;

const __filename = fileURLToPath(import.meta.url);
const __dirname = path.dirname(__filename);
const targetDir = path.join(__dirname,"assets",
);

// 端口检查
function checkPort(port) {
    return new Promise((resolve) => {
        const tester = net
            .createServer()
            .once("error", (err) => {
                if (err.code === "EADDRINUSE") {
                    resolve(true); // 端口被占用
                } else {
                    resolve(false);
                }
            })
            .once("listening", () => {
                tester.close();
                resolve(false); // 端口可用
            })
            .listen(port);
    });
}

// 4.启动资源服务器
async function startServer() {
    const portInUse = await checkPort(serverPort);
    if (portInUse) {
        console.error(
            chalk.red(`Port ${serverPort} is already in use. Server not started.`),
        );
        return;
    }

    const app = express();

    // 允许跨域
    app.use(cors());

    // 日志
    app.use(morgan("combined"));

    // 启用 gzip 压缩
    app.use(compression());

    // 解析 JSON 请求体，限制大小为 5MB
    app.use(express.json({ limit: "8mb" }));

    // 中间件：把连续斜杠替换为单斜杠
    app.use((req, res, next) => {
        req.url = req.url.replace(/\/+/g, "/");
        next();
    });

    // 发布静态资源
    app.use(express.static(targetDir));

    // 返回库图片接口
    app.get("/image", (req, res) => {
        handleLoadImage(req, res);
    });
    // 上传图片
    app.post("/upload", (req, res) => {
        handleUploadImage(req, res);
    });

    /// 返回序列动画 怪物等
    app.get("/", async (req, res) => {
        return res.status(404).json({
            success: false,
            error: `Image not found`
        });
    });

    app.listen(serverPort, () => {
        console.log(chalk.green(`Server running on port ${serverPort}`));
    });
}

startServer();
