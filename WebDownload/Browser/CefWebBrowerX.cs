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

namespace WebDownloader.Browser
{
    public partial class CefWebBrowerX : UserControl
    {
        public event EventHandler<NewWindowEventArgs> NewTabEvent;
        public event EventHandler<CefSharp.LoadingStateChangedEventArgs> LoadingStateChanged;

        private ChromiumWebBrowser webBrowser=null;
        public string WebName
        {
            get
            {
                if (webBrowser == null || webBrowser.GetBrowser() == null || webBrowser.GetBrowser().MainFrame==null)
                {
                    return "空页面";
                }

                return webBrowser.GetBrowser().MainFrame.Name;
            }
        }
        public CefWebBrowerX()
        {
            InitializeComponent();
        }

        public void OpenUrl(string url)
        {
            if (string.IsNullOrWhiteSpace(url))
            {
                url = "about:blank";
            }
            if (webBrowser==null)
            {
                webBrowser = new ChromiumWebBrowser(url);
                webBrowser.LoadingStateChanged += webBrowser_LoadingStateChanged;
                var ceflife = new CefLifeSpanHandler();
                ceflife.BeforePopupEvent += ceflife_BeforePopupEvent;
                webBrowser.LifeSpanHandler = ceflife;
            }
            else
            {
                webBrowser.Load(url);
            }
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
                    OnNewTabEvent(e);
                    break;
                case CefSharp.WindowOpenDisposition.NewForegroundTab:
                    OnNewTabEvent(e);
                    break;
                case CefSharp.WindowOpenDisposition.NewPopup:
                    break;
                case CefSharp.WindowOpenDisposition.NewWindow:
                    OnNewTabEvent(e);
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
            if (LoadingStateChanged!=null)
            {
                this.Invoke(new Action(() =>
                    {
                        LoadingStateChanged(this, e);
                    }));
            }
        }
        private void CefWebBrowerX_Load(object sender, EventArgs e)
        {
        }

        protected void OnNewTabEvent(NewWindowEventArgs e)
        {
            this.Invoke(new Action(() =>
                {
                    if (NewTabEvent == null)
                    {
                        return;
                    }
                    NewTabEvent(this, e);
                }));
        }
    }
}
