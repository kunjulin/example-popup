using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using CefSharp;
using CefSharp.WinForms;

namespace example_popup
{
    internal static class Program
    {
        /// <summary>
        /// 應用程式的主要進入點。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            
            // 初始化 CefSharp（集中於進程啟動時）
            var settings = new CefSettings();
            settings.CefCommandLineArgs.Add("enable-media-stream", "1");
            settings.CefCommandLineArgs.Add("enable-usermedia-screen-capturing", "1");
            settings.CefCommandLineArgs.Add("allow-running-insecure-content", "1");
            settings.CefCommandLineArgs.Add("disable-web-security", "1");
            // 穩定性相關建議開關
            settings.CefCommandLineArgs.Add("disable-gpu", "1");
            settings.CefCommandLineArgs.Add("disable-gpu-sandbox", "1");
            settings.CefCommandLineArgs.Add("disable-software-rasterizer", "1");
            settings.CefCommandLineArgs.Add("disable-dev-shm-usage", "1");
            Cef.Initialize(settings);
            
            Application.Run(new Form1());
            
            // 結束時關閉 CEF（與初始化對稱）
            Cef.Shutdown();
        }
    }
}
