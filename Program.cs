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
            
            // 初始化 CefSharp
            var settings = new CefSettings();
            
            // 啟用媒體功能（麥克風、攝影機等）
            settings.CefCommandLineArgs.Add("enable-media-stream", "1");
            settings.CefCommandLineArgs.Add("enable-usermedia-screen-capturing", "1");
            settings.CefCommandLineArgs.Add("allow-running-insecure-content", "1");
            settings.CefCommandLineArgs.Add("disable-web-security", "1");
            
            // 啟用剪貼簿功能
            settings.CefCommandLineArgs.Add("enable-clipboard", "1");
            
            Cef.Initialize(settings);
            
            Application.Run(new Form1());
        }
    }
}
