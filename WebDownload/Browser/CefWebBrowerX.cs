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
using System.Runtime.InteropServices;

namespace WebDownloader.Browser
{
    public partial class CefWebBrowerX : UserControl
    {
        public event EventHandler<NewWindowEventArgs> NewNavigateBrowser;
        public event EventHandler<CefSharp.LoadingStateChangedEventArgs> LoadingStateChanged;
        public event EventHandler<CefSharp.FrameLoadStartEventArgs> FrameLoadStart;
        public event EventHandler<CefSharp.FrameLoadEndEventArgs> FrameLoadEnd;
        public event EventHandler<CreateTabEventArgs> CreateTab;
        public event EventHandler<TitleChangedEventArgs> TitleChanged;
        private bool _isViewSource = false;

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
            if (CreateTab != null)
            {
                CreateTab(this, new CreateTabEventArgs(true, ""));
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
                url = "https://www.baidu.com";
            }
            if (url.StartsWith("view-source:"))
            {
                _isViewSource = true;
            }
            else
            {
                _isViewSource = false;
            }
            if (webBrowser == null)
            {
                webBrowser = new ChromiumWebBrowser(url);
                webBrowser.ActivateBrowserOnCreation = true;
                webBrowser.CreateControl();
                tbUrl.Text = url;
                webBrowser.LoadingStateChanged += webBrowser_LoadingStateChanged;
                webBrowser.FrameLoadStart += webBrowser_FrameLoadStart;
                webBrowser.FrameLoadEnd += webBrowser_FrameLoadEnd;
                webBrowser.StatusMessage += webBrowser_StatusMessage;
                webBrowser.TitleChanged += webBrowser_TitleChanged;
                webBrowser.AddressChanged += webBrowser_AddressChanged;

                var ceflife = new CefLifeSpanHandler();
                ceflife.BeforePopupEvent += ceflife_BeforePopupEvent;
                webBrowser.LifeSpanHandler = ceflife;
                var cefRequest = new CefRequestHandler();
                webBrowser.RequestHandler = cefRequest;

                var cefMenu = new CefContextMenuHandler();
                cefMenu.BeforeContextMenu += cefMenu_BeforeContextMenu;
                cefMenu.ViewSource += cefMenu_ViewSource;
                cefMenu.ShowDevTool += cefMenu_ShowDevTool;
                webBrowser.MenuHandler = cefMenu;

                var resFact = new CefResourceRequestHandlerFactory();
                webBrowser.ResourceRequestHandlerFactory = resFact;


                webBrowser.DownloadHandler = new CefDownloadHandler();

                webBrowser.Dock = DockStyle.Fill;
                this.panelBrowser.Controls.Add(webBrowser);
            }
            else
            {
                webBrowser.Load(url);
            }
        }


        private void cefMenu_ShowDevTool(object sender, EventArgs e)
        {
            this.InvokeOnUiThreadIfRequired(new Action(() =>
            {
                if (webBrowser == null)
                {
                    return;
                }
                if (splitContainer.Panel2Collapsed)
                {
                    splitContainer.Panel2Collapsed = false;
                }
                var rect = cefDevContainer.CefContainer.ClientRectangle;
                var windowInfo = new WindowInfo();
                windowInfo.SetAsChild(cefDevContainer.CefContainer.Handle, rect.Left, rect.Top, rect.Right, rect.Bottom);
                webBrowser.GetBrowserHost().ShowDevTools(windowInfo);
                cefDevContainer.Tag = windowInfo;
            }));
        }

        private void cefMenu_BeforeContextMenu(object sender, BeforeContextMenuEvenArgs e)
        {
            e.model.SetEnabled(CefMenuCommand.ViewSource, !_isViewSource);
        }

        private void cefMenu_ViewSource(object sender, EventArgs e)
        {
            this.InvokeOnUiThreadIfRequired(new Action(() =>
            {
                if (CreateTab != null)
                {
                    CreateTab(this, new CreateTabEventArgs(true, "view-source:" + webBrowser.GetBrowser().MainFrame.Url));
                }
            }));
        }

        private void webBrowser_AddressChanged(object sender, AddressChangedEventArgs e)
        {
            this.InvokeOnUiThreadIfRequired(new Action(() =>
            {
                if (_isViewSource)
                {
                    tbUrl.Text = "view-source:" + e.Address;
                }
                else
                {
                    tbUrl.Text = e.Address;
                }
            }));
        }

        private void webBrowser_TitleChanged(object sender, TitleChangedEventArgs e)
        {
            if (TitleChanged != null)
            {
                TitleChanged(this, e);
            }
        }

        private void webBrowser_StatusMessage(object sender, CefSharp.StatusMessageEventArgs e)
        {
            this.InvokeOnUiThreadIfRequired(new Action(() =>
            {
                lbTips.Text = e.Value;
            }));
        }

        private void webBrowser_FrameLoadStart(object sender, CefSharp.FrameLoadStartEventArgs e)
        {
            this.InvokeOnUiThreadIfRequired(new Action(() =>
            {
                lbTips.Text = "正在加载:" + e.Url;
                if (FrameLoadStart != null)
                {
                    FrameLoadStart(this, e);
                }
            }));
        }

        private void webBrowser_FrameLoadEnd(object sender, CefSharp.FrameLoadEndEventArgs e)
        {
            this.InvokeOnUiThreadIfRequired(new Action(() =>
            {
                try
                {
                    lbTips.Text = "结束加载:" + e.Url + ",结果:" + e.HttpStatusCode;
                    /*if (e.Frame.IsMain)
                    {
                        tbUrl.Text = e.Frame.Url;
                    }*/
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
            if (e.targetUrl.StartsWith("about:blank"))//"about:blank#blocked"
            {
                e.newBrowser = new ChromiumWebBrowser(e.targetUrl);
                return;
            }
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
            this.InvokeOnUiThreadIfRequired(new Action(() =>
           {
               try
               {
                   lbTips.Text = e.IsLoading ? "正在加载中..." : "加载完毕";
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
            this.InvokeOnUiThreadIfRequired(new Action(() =>
                {
                    if (NewNavigateBrowser == null)
                    {
                        return;
                    }
                    NewNavigateBrowser(this, e);
                }));
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            if (webBrowser != null)
            {
                webBrowser.Back();
            }

        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (webBrowser != null)
            {
                webBrowser.Forward();
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            if (webBrowser != null)
            {
                webBrowser.Reload(true);
            }
        }

        [DllImport("user32.dll", SetLastError = true)]
        static extern IntPtr GetWindow(IntPtr hWnd, GetWindowCmd uCmd);
        /// <summary>
        /// 设置目标窗体大小，位置
        /// </summary>
        /// <param name="hWnd">目标句柄</param>
        /// <param name="x">目标窗体新位置X轴坐标</param>
        /// <param name="y">目标窗体新位置Y轴坐标</param>
        /// <param name="nWidth">目标窗体新宽度</param>
        /// <param name="nHeight">目标窗体新高度</param>
        /// <param name="BRePaint">是否刷新窗体</param>
        /// <returns></returns>
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int MoveWindow(IntPtr hWnd, int x, int y, int nWidth, int nHeight, bool BRePaint);
        public const int WM_CLOSE = 0x10;
        [DllImport("user32.dll", EntryPoint = "SendMessageA")]
        public static extern int SendMessage(IntPtr hwnd, int wMsg, int wParam, int lParam);
        [DllImport("user32.dll", EntryPoint = "SetParent")]
        public static extern int SetParent(IntPtr hWndChild, IntPtr hWndNewParent);
        public enum GetWindowCmd : uint
        {
            /// <summary>
            /// 返回的句柄标识了在Z序最高端的相同类型的窗口。
            /// 如果指定窗口是最高端窗口，则该句柄标识了在Z序最高端的最高端窗口；
            /// 如果指定窗口是顶层窗口，则该句柄标识了在z序最高端的顶层窗口：
            /// 如果指定窗口是子窗口，则句柄标识了在Z序最高端的同属窗口。
            /// </summary>
            GW_HWNDFIRST = 0,
            /// <summary>
            /// 返回的句柄标识了在z序最低端的相同类型的窗口。
            /// 如果指定窗口是最高端窗口，则该柄标识了在z序最低端的最高端窗口：
            /// 如果指定窗口是顶层窗口，则该句柄标识了在z序最低端的顶层窗口；
            /// 如果指定窗口是子窗口，则句柄标识了在Z序最低端的同属窗口。
            /// </summary>
            GW_HWNDLAST = 1,
            /// <summary>
            /// 返回的句柄标识了在Z序中指定窗口下的相同类型的窗口。
            /// 如果指定窗口是最高端窗口，则该句柄标识了在指定窗口下的最高端窗口：
            /// 如果指定窗口是顶层窗口，则该句柄标识了在指定窗口下的顶层窗口；
            /// 如果指定窗口是子窗口，则句柄标识了在指定窗口下的同属窗口。
            /// </summary>
            GW_HWNDNEXT = 2,
            /// <summary>
            /// 返回的句柄标识了在Z序中指定窗口上的相同类型的窗口。
            /// 如果指定窗口是最高端窗口，则该句柄标识了在指定窗口上的最高端窗口；
            /// 如果指定窗口是顶层窗口，则该句柄标识了在指定窗口上的顶层窗口；
            /// 如果指定窗口是子窗口，则句柄标识了在指定窗口上的同属窗口。
            /// </summary>
            GW_HWNDPREV = 3,
            /// <summary>
            /// 返回的句柄标识了指定窗口的所有者窗口（如果存在）。
            /// GW_OWNER与GW_CHILD不是相对的参数，没有父窗口的含义，如果想得到父窗口请使用GetParent()。
            /// 例如：例如有时对话框的控件的GW_OWNER，是不存在的。
            /// </summary>
            GW_OWNER = 4,
            /// <summary>
            /// 如果指定窗口是父窗口，则获得的是在Tab序顶端的子窗口的句柄，否则为NULL。
            /// 函数仅检查指定父窗口的子窗口，不检查继承窗口。
            /// </summary>
            GW_CHILD = 5,
            /// <summary>
            /// （WindowsNT 5.0）返回的句柄标识了属于指定窗口的处于使能状态弹出式窗口（检索使用第一个由GW_HWNDNEXT 查找到的满足前述条件的窗口）；
            /// 如果无使能窗口，则获得的句柄与指定窗口相同。
            /// </summary>
            GW_ENABLEDPOPUP = 6
        }
        private void cefDevContainer_SizeChanged(object sender, EventArgs e)
        {
            if (cefDevContainer.Tag == null)
            {
                return;
            }
            var rect = cefDevContainer.CefContainer.ClientRectangle;
            var windowInfo = (WindowInfo)cefDevContainer.Tag;
            IntPtr ptr = GetWindow(cefDevContainer.CefContainer.Handle, GetWindowCmd.GW_CHILD);
            if (ptr != IntPtr.Zero)
            {
                MoveWindow(ptr, 0, 0, rect.Width, rect.Height, true);
            }
        }

        private void cefDevContainer_Close(object sender, EventArgs e)
        {
            if (cefDevContainer.Tag != null)
            {
                IntPtr ptr = GetWindow(cefDevContainer.CefContainer.Handle, GetWindowCmd.GW_CHILD);
                if (ptr != IntPtr.Zero)
                {
                    SetParent(ptr, IntPtr.Zero);
                    //SendMessage(ptr, WM_CLOSE, 0, 0);
                    webBrowser.GetBrowserHost().CloseDevTools();
                }
                cefDevContainer.Tag = null;
            }
            splitContainer.Panel2Collapsed = true;
        }
    }
}
