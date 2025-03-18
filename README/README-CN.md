<h1 align="center">🔥 热点别跑！（Keep My Hotspot Alive）</h1>
<h3 align="center">让 iPhone 热点保持在线，不再自动断开！</h1>

<p align="center">
    <img width="200" src="./icon.png" alt="Mouse Steering Wheel">
</p>

<p align="center">
    <a href="https://github.com/Siriusq/KeepMyHotspotAlive"><img src="https://img.shields.io/badge/ENGLISH_README-4285F4?style=for-the-badge&logo=googletranslate&logoColor=ffffff"/></a>
    <img alt="GitHub Release Date" src="https://img.shields.io/github/release-date/Siriusq/KeepMyHotspotAlive?style=for-the-badge&logo=github">
    <img alt="GitHub top language" src="https://img.shields.io/github/languages/top/Siriusq/KeepMyHotspotAlive?style=for-the-badge&logo=C">    
    <img alt="DotNet Framework" src="https://img.shields.io/badge/4.8-lightgrey.svg?style=for-the-badge&label=Framework&labelColor=%23555555&color=%23512BD4&logo=.NET">
    <img alt="GitHub License" src="https://img.shields.io/github/license/Siriusq/KeepMyHotspotAlive?style=for-the-badge&logo=git">
    <img alt="Platform" src="https://img.shields.io/badge/platform-windows-lightgrey.svg?style=for-the-badge&label=Platform&color=%230078D4">
</p>

---

## 🧐 为什么开发这个工具？  
当我用 **iPhone** 给笔记本电脑开热点时，如果 **几分钟内没有明显的网络活动**，**iOS 就会自动断开连接**，而且 **不会给任何提示**。更糟糕的是，此时 iPhone 的热点开关依然显示为开启，但 **电脑的 Wi-Fi 列表中找不到 SSID**，必须手动关闭并重新打开热点，才能重新连接。  

这种情况在**阅读长篇文章**时尤其让人抓狂。每次读完一篇文章，回过神来，发现 Wi-Fi 又断了，然后不得不 **手动重启热点**，反复循环，烦不胜烦。  

为了解决这个问题，我开发了 **「热点别跑！」**—— 一个轻量级小工具，每 **15 秒** 自动 `ping` 一下 iPhone，**让它误以为电脑一直在使用网络，从而保持热点连接不断开**。  

---  

## 🚀 如何使用？  
1. 在 [Releases](https://github.com/Siriusq/KeepMyHotspotAlive/releases) 页面下载最新版本，或直接[点击此处](https://github.com/Siriusq/KeepMyHotspotAlive/releases/download/1.0.0/KeepMyHotspotAlive.exe)下载 `exe` 文件。  
2. **双击运行**，无需安装，开箱即用！  

**⚠️ 注意：Windows 10 1903 及以下版本** 需要先安装 [.NET Framework 4.8](https://dotnet.microsoft.com/en-us/download/dotnet-framework/net48) 才能运行程序。  

---  

## ✨ 主要特性  
✅ **热点保活**：每 **15 秒** `ping` 一次 iPhone，防止热点因无活动被断开。  
✅ **多语言支持**：根据系统语言自动适配，支持**简体中文、繁体中文和英文**。  
✅ **静默运行**：程序不会弹出窗口，仅在 **任务栏托盘** 中显示图标，右键可暂停或退出。  
✅ **自动暂停**：当 Wi-Fi 断开或切换网络时，程序会自动暂停 `ping` 并发送通知（不会自动退出）。  
✅ **状态提示**：将鼠标悬停在任务栏图标上，可查看当前运行状态。  

---

## 📦 第三方资源  

### 🔧 NuGet Packages  
**「热点别跑！」** 使用了以下两个 **NuGet** 包，以便将所有资源打包进 **单个独立的 exe 文件**，无需额外依赖：  

- [**Costura**](https://github.com/Fody/Costura) – 用于自动嵌入依赖项，使应用程序无需额外的 DLL 文件即可运行。  
- [**Resource.Embedder**](https://www.nuget.org/packages/Resource.Embedder/) – 让程序能够打包本地化资源，实现多语言支持。  

### 🎨 图标来源  
程序的图标由以下两个素材组合而成：  

- 🔗 [**链接图标**](https://www.flaticon.com/free-icon/link_1824953?term=connection&related_id=1824953) – 由 **Freepik** 设计，来自 **Flaticon**。  
- ❤️ [**心形图标**](https://www.flaticon.com/free-icon/cardiogram_3004451?k=1742204085939) – 由 **Freepik** 设计，来自 **Flaticon**。  

---

如果觉得有用，欢迎 **Star⭐** 支持！

👉 [GitHub 项目地址](https://github.com/Siriusq/KeepMyHotspotAlive)
