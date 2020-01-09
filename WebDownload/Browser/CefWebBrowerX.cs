using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CefSharp.WinForms;
using CefSharp;
using WebDownloader.CefHandler;

namespace WebDownloader.Browser
{
    public partial class CefWebBrowerX : UserControl
    {
        public event EventHandler<NewWindowEventArgs> NewNavigateBrowser;
        public event EventHandler<CefSharp.LoadingStateChangedEventArgs> LoadingStateChanged;
        public event EventHandler<CefSharp.FrameLoadStartEventArgs> FrameLoadStart;
        public event EventHandler<CefSharp.FrameLoadEndEventArgs> FrameLoadEnd;
        public event EventHandler CreateTab;
        public event EventHandler<TitleChangedEventArgs> TitleChanged;

        public ChromiumWebBrowser webBrowser { get; private set; }
        public string WebName
        {
            get
            {
                if (webBrowser == null || webBrowser.GetBrowser() == null || webBrowser.GetBrowser().MainFrame == null)
                {
                    return "空页面";
                }

                return webBrowser.GetBrowser().MainFrame.Name;
            }
        }
        static CefWebBrowerX()
        {
            var setting = new CefSettings()
            {
                Locale = "zh-CN",
                AcceptLanguageList = "zh-CN",
                MultiThreadedMessageLoop = true
            };
            CefSharp.Cef.Initialize(setting);
            if (CefSharpSettings.ShutdownOnExit)
            {
                Application.ApplicationExit += OnApplicationExit;
            } 
        }
        private static void OnApplicationExit(object sender, EventArgs e)
        {
            Cef.Shutdown();
        }  
        public CefWebBrowerX()
        {
            InitializeComponent();
        }

        private void btnEnter_Click(object sender, EventArgs e)
        {
            this.OpenUrl(this.tbUrl.Text.Trim());
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            if (CreateTab!=null)
            {
                CreateTab(this, e);
            }
        }

        private void tbUrl_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.OpenUrl(this.tbUrl.Text.Trim());
            }
        }


        public void OpenUrl(string url)
        {
            if (string.IsNullOrWhiteSpace(url))
            {
                url = "about:blank";
            }
            if (url == "about:blank")
            {
                url = "http://www.baidu.com";
            }
            if (webBrowser == null)
            {
                webBrowser = new ChromiumWebBrowser(url);
                tbUrl.Text = url;
                webBrowser.LoadingStateChanged += webBrowser_LoadingStateChanged;
                webBrowser.FrameLoadStart += webBrowser_FrameLoadStart;
                webBrowser.FrameLoadEnd += webBrowser_FrameLoadEnd;
                webBrowser.StatusMessage += webBrowser_StatusMessage;
                webBrowser.TitleChanged += webBrowser_TitleChanged;

                var ceflife = new CefLifeSpanHandler();
                ceflife.BeforePopupEvent += ceflife_BeforePopupEvent;
                webBrowser.LifeSpanHandler = ceflife;
                webBrowser.Dock = DockStyle.Fill;
                this.panelBrowser.Controls.Add(webBrowser);
            }
            else
                webBrowser.Load(url);
        }

        private void webBrowser_TitleChanged(object sender, TitleChangedEventArgs e)
        {
            if (TitleChanged!=null)
            {
                TitleChanged(this, e);
            }
        }

        private void webBrowser_StatusMessage(object sender, CefSharp.StatusMessageEventArgs e)
        {
            this.Invoke(new Action(() =>
            {
                lbTips.Text = e.Value;
            }));
        }

        private void webBrowser_FrameLoadStart(object sender, CefSharp.FrameLoadStartEventArgs e)
        {
            this.Invoke(new Action(() =>
            {
                lbTips.Text = "正在加载:" + e.Url;
                if (FrameLoadStart!=null)
                {
                    FrameLoadStart(this, e);
                }
            }));
        }

        private void webBrowser_FrameLoadEnd(object sender, CefSharp.FrameLoadEndEventArgs e)
        {
            this.Invoke(new Action(() =>
            {
                try
                {
                    lbTips.Text = "结束加载:" + e.Url + ",结果:" + e.HttpStatusCode;
                    if (e.Frame.IsMain)
                    {
                        tbUrl.Text = e.Frame.Url;
                    }
                    if (FrameLoadEnd != null)
                    {
                        FrameLoadEnd(this, e);
                    }
                }
                catch (Exception)
                {
                    
                    //throw;
                }

            }));
        }

        private void ceflife_BeforePopupEvent(object sender, NewWindowEventArgs e)
        {
            switch (e.targetDisposition)
            {
                case CefSharp.WindowOpenDisposition.CurrentTab:
                    break;
                case CefSharp.WindowOpenDisposition.IgnoreAction:
                    break;
                case CefSharp.WindowOpenDisposition.NewBackgroundTab:
                    OnNewNavigateBrowser(e);
                    break;
                case CefSharp.WindowOpenDisposition.NewForegroundTab:
                    OnNewNavigateBrowser(e);
                    break;
                case CefSharp.WindowOpenDisposition.NewPopup:
                    break;
                case CefSharp.WindowOpenDisposition.NewWindow:
                    OnNewNavigateBrowser(e);
                    break;
                case CefSharp.WindowOpenDisposition.OffTheRecord:
                    break;
                case CefSharp.WindowOpenDisposition.SaveToDisk:
                    break;
                case CefSharp.WindowOpenDisposition.SingletonTab:
                    break;
                case CefSharp.WindowOpenDisposition.Unknown:
                    break;
                default:
                    break;
            }
        }

        private void webBrowser_LoadingStateChanged(object sender, CefSharp.LoadingStateChangedEventArgs e)
        {
            this.Invoke(new Action(() =>
           {
               try
               {
                   lbTips.Text = e.IsLoading ? "正在加载中..." : "加载完毕";
                   tbUrl.Text = e.Browser.MainFrame.Url;
                   if (LoadingStateChanged != null)
                   {
                       LoadingStateChanged(this, e);
                   }
               }
               catch (Exception)
               {
                    
               }
           }));
        }
        private void CefWebBrowerX_Load(object sender, EventArgs e)
        {
        }

        protected void OnNewNavigateBrowser(NewWindowEventArgs e)
        {
            this.Invoke(new Action(() =>
                {
                    if (NewNavigateBrowser == null)
                    {
                        return;
                    }
                    NewNavigateBrowser(this, e);
                }));
        }
    }
}
