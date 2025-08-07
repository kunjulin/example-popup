using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using strucform2;
using CefSharp;
using CefSharp.WinForms;

namespace example_popup
{
    public partial class Form1 : Form
    {
        private Button btnOpenChild;
        private TextBox txtResult;
        private StrucForm strucForm;
        private ChromiumWebBrowser webBrowser;
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

        public Form1()
        {
            InitializeComponent();
            InitializeUI();
        }

        private void InitializeUI()
        {
            // 建立 CefSharp 瀏覽器控制項
            webBrowser = new ChromiumWebBrowser("https://lvaiw.cgmf.org.tw/go/temp3.html");
            webBrowser.Dock = DockStyle.Fill;
            
            // 使用預設瀏覽器設定

            // 建立分割容器
            SplitContainer splitContainer = new SplitContainer
            {
                Dock = DockStyle.Fill,
                Orientation = Orientation.Vertical,
                SplitterDistance = 60
            };

            // 左側面板 - 控制項
            Panel leftPanel = new Panel
            {
                Dock = DockStyle.Fill
            };

            btnOpenChild = new Button
            {
                Text = "結構化2.0",
                Location = new System.Drawing.Point(20, 20),
                Size = new System.Drawing.Size(150, 30)
            };
            btnOpenChild.Click += btnOpenChild_Click;

            txtResult = new TextBox
            {
                Location = new System.Drawing.Point(20, 70),
                Size = new System.Drawing.Size(250, 200),
                Multiline = true,
                ScrollBars = ScrollBars.Vertical,
                ReadOnly = true,
                WordWrap = true
            };

            leftPanel.Controls.Add(btnOpenChild);
            leftPanel.Controls.Add(txtResult);

            // 右側面板 - 瀏覽器
            Panel rightPanel = new Panel
            {
                Dock = DockStyle.Fill
            };
            rightPanel.Controls.Add(webBrowser);

            splitContainer.Panel1.Controls.Add(leftPanel);
            splitContainer.Panel2.Controls.Add(rightPanel);

            this.Controls.Add(splitContainer);
            this.Text = "父應用程式 - 含 CefSharp 瀏覽器";
            this.Size = new System.Drawing.Size(1000, 600);
            this.WindowState = FormWindowState.Normal;
        }

        // 處理按鈕點擊事件，啟動結構化表單
        private async void btnOpenChild_Click(object sender, EventArgs e)
        {
            btnOpenChild.Enabled = false;

            try
            {
                strucForm = new StrucForm();
                strucForm.OnDataSaved += HandleDataSaved;

                var config = new StructureConfiguration
                {
                    // 使用者資料
                    UserID = "V4J",   // 使用者代號
                    UserName = "蕭秉旻",   // 使用者姓名
                    Idno = "A123456789", // 使用者身分證字號 (用來查詢VIP病歷參數) 

                    // 病患資料
                    Chtno = "12345678",  // 病人病歷號                    
                    Opdno = "20240101001", //  就診號
                    DeptNo = "83K0",  // 部門代號

                    // HIS 端作業資料                    
                    DocTypeName = "xx病歷",  // HIS欄位名稱，例如: "門診S，progress note..."
                    DocTypeID = "b04d996a-baba-460f-9409-d8ceb4de71d6",  // 上述欄位字典代號 (由結構化團隊提供)

                    // 可先使用以下預設值，有必要才調整
                    DocAutoLoad = true,   // 預設自動載入暫存資料
                    showSubpanel = false,  // 預設不顯示子表單(助手多功能表單)
                    EnableParentTemplateShortcut = true,  // 預設提供切換表單功能列
                    F2Fix = true,    // 固切換表單功能列
                    BaselineFormID = "f93afc97-9403-4613-bd67-2f683d805234",  // 基準表單(預設段落表單)
                    SubpanelFormID = "7011725e-81f5-421f-8caf-337c6f2f7a21",  // 子表單(預設門診AI小助手表單)
                    showCharCount = false,  // 預設不顯示字數計算
                };

                // 直接使用 ShowDialogAsync，它會內部處理 InitAsync
                await strucForm.ShowDialogAsync(config);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"結構化2.0執行時發生錯誤：{ex.Message}");
            }
            finally
            {
                btnOpenChild.Enabled = true;
            }
        }

        // 處理儲存事件
        private void HandleDataSaved(object sender, string jsonData)
        {
            if (txtResult.InvokeRequired)
            {
                txtResult.Invoke(new Action(() => UpdateResultText(jsonData)));
            }
            else
            {
                UpdateResultText(jsonData);
            }
        }

        private void UpdateResultText(string jsonData)
        {
            try
            {
                // 可以在這裡添加 JSON 格式化顯示
                txtResult.Text = jsonData;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"更新結果時發生錯誤：{ex.Message}");
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            strucForm?.Dispose(); // 確保資源被正確釋放
            webBrowser?.Dispose(); // 釋放瀏覽器資源
            Cef.Shutdown(); // 關閉 CefSharp
        }
    }
}
