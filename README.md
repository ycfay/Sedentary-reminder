# Sedentary-reminder 久坐提醒小工具
一款轻量级的桌面久坐提醒工具，帮助你按设定周期切换 **坐姿工作-休息-站立工作** 模式，告别久坐危害，养成健康办公习惯。
> 本项目基于开源项目 [Sedentary-reminder](https://github.com/wjbgis/Sedentary-reminder) fork 进行二次修改开发，原作者为 Wei Junbo。

## ✨ 核心功能
1.  **智能循环提醒**
    遵循固定倒计时流程循环：`坐姿工作 → 休息 → 坐姿工作 → …`
    
3.  **贴心倒计时提示**
    坐姿工作倒计时剩余 `15秒` 时，弹窗提醒即将进入休息阶段
    
4.  **强制休息机制**
    倒计时结束后自动弹出遮罩层，屏蔽鼠标与键盘操作
    > 🔐 安全兜底：未屏蔽 `Ctrl+Alt+Del` 组合键，可通过该快捷键执行关机等应急操作（无法打开任务管理器）

## 🚀 改版新增功能
1.  修复无法屏蔽功能按键。
2.  增加了最近一次设置的记忆功能，避免每次打开软件都需要重复设置
3.  增加窗体位置记忆功能，下次打开再同一位置
4.  增加开机自启动设置项
5.  增加了运动动画
6.  增加休息时间是否需要锁屏选项

## 💻 支持系统
- Windows 10 / Windows 11

## 🖼️ 界面展示
| 功能界面                   | 截图展示                                                     |
| -------------------------- | ------------------------------------------------------------ |
| 设置界面                   | ![设置界面](https://github.com/janu-hwh/Sedentary-reminder/blob/master/ScreenShot/1.png) |
| 坐姿工作倒计时             | ![坐姿工作倒计时](https://github.com/janu-hwh/Sedentary-reminder/blob/master/ScreenShot/2.1.png) |
| 休息倒计时提醒（剩余15秒） | ![休息提醒](https://github.com/janu-hwh/Sedentary-reminder/blob/master/ScreenShot/4.png) |
| 强制休息遮罩层             | ![强制休息遮罩层](https://github.com/janu-hwh/Sedentary-reminder/blob/master/ScreenShot/3.png) |

## 🤝 贡献指南
欢迎提交 Issue 反馈 Bug 或提出新功能建议，也可以直接 Fork 仓库提交 Pull Request 参与开发。
