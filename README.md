<h1 align="center">🔥 Keep My Hotspot Alive!</h1>
<h3 align="center">Keep your iPhone hotspot connected—no more unexpected disconnections!  </h1>

<p align="center">
    <img width="200" src="./README/icon.png" alt="Mouse Steering Wheel">
</p>

<p align="center">
    <a href="https://github.com/Siriusq/KeepMyHotspotAlive/blob/master/README/README-CN.md"><img src="https://img.shields.io/badge/简体中文_README-4285F4?style=for-the-badge&logo=googletranslate&logoColor=ffffff"/></a>
    <img alt="GitHub Release Date" src="https://img.shields.io/github/release-date/Siriusq/KeepMyHotspotAlive?style=for-the-badge&logo=github">
    <img alt="GitHub top language" src="https://img.shields.io/github/languages/top/Siriusq/KeepMyHotspotAlive?style=for-the-badge&logo=C">    
    <img alt="DotNet Framework" src="https://img.shields.io/badge/4.8-lightgrey.svg?style=for-the-badge&label=Framework&labelColor=%23555555&color=%23512BD4&logo=.NET">
    <img alt="GitHub License" src="https://img.shields.io/github/license/Siriusq/KeepMyHotspotAlive?style=for-the-badge&logo=git">
    <img alt="Platform" src="https://img.shields.io/badge/platform-windows-lightgrey.svg?style=for-the-badge&label=Platform&color=%230078D4">
</p>

---

## 🧐 Why Do I Developed This?  
When using an **iPhone hotspot** on a laptop, if there's **no noticeable network activity for a few minutes**, **iOS automatically disconnects the hotspot** without any warning. Even worse, the **hotspot toggle on the iPhone still appears to be ON**, but the **SSID disappears from the Wi-Fi list**, forcing me to manually turn the hotspot off and back on to reconnect.  

This is especially frustrating when **reading long articles**. Every time I finish an article, I find that my Wi-Fi has disconnected, forcing me to **manually restart the iPhone hotspot**—again and again.  

To solve this issue, I developed **"Keep My Hotspot Alive!"**, a lightweight utility that **pings** your iPhone every **15 seconds**, tricking it into thinking the network is active and preventing the hotspot from disconnecting.  

---

## 🚀 How to Use?  
1. Download the latest version from the [Releases](https://github.com/Siriusq/KeepMyHotspotAlive/releases) page, or directly [click here](https://github.com/Siriusq/KeepMyHotspotAlive/releases/download/1.0.0/KeepMyHotspotAlive.exe) to download the `exe` file.  
2. **Double-click to run**—no installation required!  

**⚠️ Note: Windows 10 1903 and earlier** requires installing [.NET Framework 4.8](https://dotnet.microsoft.com/en-us/download/dotnet-framework/net48) before running the program.  

---

## ✨ Features  
✅ **Prevents hotspot disconnection**: Sends a **ping** every 15 seconds to keep the iPhone hotspot alive.  
✅ **Multilingual support**: Automatically adapts to system language, supporting **Simplified Chinese, Traditional Chinese, and English**.  
✅ **Runs in the background**: No annoying windows—just a **tray icon** where you can right-click to pause or exit.  
✅ **Smart pause**: Automatically pauses when Wi-Fi is switched or disconnected and sends a notification (but does not exit).  
✅ **Status display**: Hover over the tray icon to see the current program status.  

---

## 📦 Third-Party Resources  

### 🔧 NuGet Packages  
**Keep My Hotspot Alive!** uses the following **NuGet** packages to bundle all resources into a **single standalone exe file** with no extra dependencies:  

- [**Costura**](https://github.com/Fody/Costura) – Embeds dependencies, eliminating the need for additional DLL files.  
- [**Resource.Embedder**](https://www.nuget.org/packages/Resource.Embedder/) – Embeds localized resources for multi-language support.  

### 🎨 Icon Sources  
The program icon is a combination of the following two assets:  

- 🔗 [**Connection Icon**](https://www.flaticon.com/free-icon/link_1824953?term=connection&related_id=1824953) – Created by **Freepik**, from **Flaticon**.  
- ❤️ [**Heart Icon**](https://www.flaticon.com/free-icon/cardiogram_3004451?k=1742204085939) – Created by **Freepik**, from **Flaticon**.  

---

This tool is simple yet effective—I hope it helps others who struggle with **iPhone hotspot disconnections**! If you find it useful, please **Star⭐** the project to support it!  

👉 [GitHub Repository](https://github.com/Siriusq/KeepMyHotspotAlive)  