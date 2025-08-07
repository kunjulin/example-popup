# Example Popup - StrucForm2 with CefSharp Browser

這是一個 Windows Forms 應用程式，整合了 StrucForm2 控制項和 CefSharp 瀏覽器，提供結構化表單和網頁瀏覽功能。

## 功能特色

### 🎯 主要功能
- **StrucForm2 控制項**：提供結構化表單處理功能
- **CefSharp 瀏覽器**：內嵌 Chromium 瀏覽器，支援現代網頁技術
- **分割視窗**：左側點選按鈕可彈出 StrucForm2 結構化子視窗，填完表純文字單資料回傳文字控制窗，右側為網頁瀏覽器
- **麥克風支援**：瀏覽器支援麥克風存取
- **複製貼上**：啟用剪貼簿功能
- **檔案存取**：支援檔案下載和儲存功能

### 🌐 網頁功能
- 預設載入：`https://lvaiw.cgmf.org.tw/go/temp3.html`
- 支援媒體串流（麥克風、攝影機）
- 啟用剪貼簿操作
- 支援檔案下載

## 技術架構

### 使用技術
- **.NET Framework**：Windows Forms 應用程式
- **CefSharp**：Chromium Embedded Framework 的 .NET 包裝器
- **StrucForm2**：結構化表單控制項
- **SplitContainer**：分割視窗佈局

### 專案結構
```
example-popup/
├── Form1.cs                 # 主表單和 UI 邏輯
├── Form1.Designer.cs        # UI 設計器生成的程式碼
├── Program.cs               # 應用程式進入點，CefSharp 初始化
├── example-popup.csproj     # 專案檔案
├── packages.config          # NuGet 套件配置
└── README.md               # 專案說明文件
```

## 安裝和執行

### 前置需求
- Visual Studio 2019 或更新版本
- .NET Framework 4.7.2 或更新版本
- 網路連線（用於載入網頁）

### 建置步驟
1. 開啟 `example-popup.sln` 解決方案
2. 還原 NuGet 套件
3. 建置專案
4. 執行應用程式

### NuGet 套件
- `CefSharp.WinForms`：CefSharp 瀏覽器控制項
- `CefSharp.Common`：CefSharp 共用元件

## 使用說明

### 視窗佈局
- **視窗大小**：800x600 像素
- **左側面板**：StrucForm2 控制項區域
- **右側面板**：CefSharp 瀏覽器（寬度 600px）
- **分割器**：可拖曳調整左右面板比例

### 瀏覽器功能
- **網頁瀏覽**：載入指定的 URL
- **麥克風存取**：支援網頁應用程式的麥克風權限
- **複製貼上**：支援鍵盤快捷鍵和右鍵選單
- **檔案操作**：支援檔案下載和儲存

## 開發說明

### CefSharp 設定
在 `Program.cs` 中設定了以下 CefSharp 參數：
```csharp
settings.CefCommandLineArgs.Add("enable-media-stream", "1");
settings.CefCommandLineArgs.Add("enable-usermedia-screen-capturing", "1");
settings.CefCommandLineArgs.Add("allow-running-insecure-content", "1");
settings.CefCommandLineArgs.Add("disable-web-security", "1");
settings.CefCommandLineArgs.Add("enable-clipboard", "1");
```

### 主要程式碼檔案
- **Form1.cs**：包含 UI 初始化和 CefSharp 瀏覽器設定
- **Program.cs**：CefSharp 全域初始化和設定

## 授權

此專案僅供學習和研究使用。

## 聯絡資訊

如有問題或建議，請透過 GitHub Issues 聯絡。

---

**注意**：此專案整合了 StrucForm2 控制項，請確保您有適當的授權來使用該控制項。
