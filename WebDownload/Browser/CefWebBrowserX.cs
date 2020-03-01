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
    public partial class CefWebBrowserX : UserControl
    {
        public event EventHandler<NewWindowEventArgs> NewNavigateBrowser;
        public event EventHandler<CefSharp.LoadingStateChangedEventArgs> LoadingStateChanged;
        public event EventHandler<CefSharp.FrameLoadStartEventArgs> FrameLoadStart;
        public event EventHandler<CefSharp.FrameLoadEndEventArgs> FrameLoadEnd;
        public event EventHandler<CreateTabEventArgs> CreateTab;
        public event EventHandler<TitleChangedEventArgs> TitleChanged;

        private bool _isViewSource = false;
        private string _injectScript;
        public string InjectScript
        {
            get { return _injectScript; }
            set { _injectScript = value; }
        }
        public ChromiumWebBrowserX webBrowser { get; private set; }
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
        static CefWebBrowserX()
        {
            var setting = new CefSettings()
            {
                Locale = "zh-CN",
                AcceptLanguageList = "zh-CN",
                MultiThreadedMessageLoop = true,
                //UserAgent = "Mozilla/5.0 (iPhone; CPU iPhone OS 8_0 like Mac OS X) AppleWebKit/600.1.4 (KHTML, like Gecko) Mobile/12A365 MicroMessenger/5.4.1 NetType/WIFI"
            };
            CefSharp.Cef.Initialize(setting);
            CefSharpSettings.LegacyJavascriptBindingEnabled = true;
            if (CefSharpSettings.ShutdownOnExit)
            {
                Application.ApplicationExit += OnApplicationExit;
            }
        }
        private static void OnApplicationExit(object sender, EventArgs e)
        {
            Cef.Shutdown();
        }
        public CefWebBrowserX()
        {
            InitializeComponent();
            splitContainer.Panel2Collapsed = true;
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

        private void cefMenu_OpenLinkOrSource(object sender, OpenLinkOrSourceArgs e)
        {
            string url = (e.isOnlySource ? "view-source:" : "") + e.url;
            OnNewNavigateBrowser(new NewWindowEventArgs(webBrowser, webBrowser.GetBrowser(), webBrowser.GetMainFrame(), url, null, WindowOpenDisposition.NewForegroundTab, null));
        }

        public void OpenUrl(string url,string injectScript=null)
        {
            if (string.IsNullOrWhiteSpace(url))
            {
                url = "about:blank";
            }
            /*if (url == "about:blank")
            {
                url = "https://www.baidu.com";
            }*/
            if (url.StartsWith("view-source:"))
            {
                _isViewSource = true;
            }
            else
            {
                _isViewSource = false;
            }
            _injectScript = injectScript;
            if (webBrowser == null)
            {
                webBrowser = new ChromiumWebBrowserX(url);
                webBrowser.RegisterJsObject("jsCallObject",new JsCallObject(),new BindingOptions(){CamelCaseJavascriptNames=false});
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
                cefRequest.GetResourceRequest += cefRequest_GetResourceRequest;
                webBrowser.RequestHandler = cefRequest;

                var cefMenu = new CefContextMenuHandler();
                cefMenu.BeforeContextMenu += cefMenu_BeforeContextMenu;
                cefMenu.ViewSource += cefMenu_ViewSource;
                cefMenu.ShowDevTool += cefMenu_ShowDevTool;
                cefMenu.CopyImageToClipboard += cefMenu_CopyImageToClipboard;
                cefMenu.OpenLinkOrSource += cefMenu_OpenLinkOrSource;
                cefMenu.LoadScript += cefMenu_LoadScript;
                webBrowser.MenuHandler = cefMenu;

                var resFact = new CefResourceRequestHandlerFactory();
                webBrowser.ResourceRequestHandlerFactory = resFact;


                webBrowser.DownloadHandler = new CefDownloadHandler();
                webBrowser.KeyboardHandler = new CefKeyboardHandler();

                var cefJsDialog = new CefJsDialogHandler();
                cefJsDialog.JsDialog += cefJsDialog_JsDialog;
                webBrowser.JsDialogHandler = cefJsDialog;

                webBrowser.Dock = DockStyle.Fill;
                webBrowser.PreviewKeyDown += webBrowser_PreviewKeyDown;

                webBrowser.RenderProcessMessageHandler = new CefRenderProcessMessageHandler();

                webBrowser.ActivateBrowserOnCreation = true;
                webBrowser.CreateControl();

                this.panelBrowser.Controls.Add(webBrowser);
            }
            else
            {
                webBrowser.Load(url);
            }
        }

        private void cefMenu_LoadScript(object sender, EventArgs e)
        {
            this.InvokeOnUiThreadIfRequired(new Action(() =>
              {
                  FrmOpenScripts frmOpenScript = new FrmOpenScripts(this);
                  frmOpenScript.ShowDialog(this);
              }));
        }

        private void cefJsDialog_JsDialog(object sender, JsDialogEventArgs e)
        {
            string script = "$(\"#cefMsg\").html(\"" + e.messageText + "\");";
            e.callback.Continue(true, string.Empty);
            e.suppressMessage = false;
            e.result = true;
            if (e.messageText.Contains("过于频繁"))
            {
                script += "setTimeout(\"bindNext()\", 2000);";
                webBrowser.GetMainFrame().ExecuteJavaScriptAsync(script);
            }
            else if (e.messageText.Contains("预约成功"))
            {
                webBrowser.GetMainFrame().ExecuteJavaScriptAsync(script);
            }
            else if(e.messageText.Contains("已经约满"))
            {
                script += "afternoonNext();";
                webBrowser.GetMainFrame().ExecuteJavaScriptAsync(script);
            }
        }

        private void cefRequest_GetResourceRequest(object sender, GetResourceRequestArgs e)
        {
            //headers["User-Agent"] = "Mozilla/5.0 (iPhone; CPU iPhone OS 8_0 like Mac OS X) AppleWebKit/600.1.4 (KHTML, like Gecko) Mobile/12A365 MicroMessenger/5.4.1 NetType/WIFI";
            e.Headers = new System.Collections.Specialized.NameValueCollection();
            e.Headers["User-Agent"] = "Mozilla/5.0 (iPhone; CPU iPhone OS 8_0 like Mac OS X) AppleWebKit/600.1.4 (KHTML, like Gecko) Mobile/12A365 MicroMessenger/5.4.1 NetType/WIFI";
        }
        public async Task<string> GetSource()
        {
            if (webBrowser != null)
            {
                var source = await webBrowser.GetSourceAsync();
                return source;
            }
            else return null;
        }
        public async Task<string> GetSource(string url)
        {
            OpenUrl("view-source:" + url);
            var source = await GetSource();
            return source;
        }
        public void ShowDevTools()
        {
            if (webBrowser == null)
            {
                return;
            }
            if (splitContainer.Panel2Collapsed)
            {
                splitContainer.Panel2Collapsed = false;
                if (splitContainer.Orientation == Orientation.Horizontal)
                {
                    splitContainer.SplitterDistance = this.Height - 300;
                }
                else
                {
                    splitContainer.SplitterDistance = this.Width - 300;
                }
            }
            var rect = cefDevContainer.CefContainer.ClientRectangle;
            var windowInfo = new WindowInfo();
            windowInfo.SetAsChild(cefDevContainer.CefContainer.Handle, rect.Left, rect.Top, rect.Right, rect.Bottom);
            webBrowser.GetBrowserHost().ShowDevTools(windowInfo);
            cefDevContainer.Tag = windowInfo;
        }
        public void CloseDevTools()
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

        public void DownloadImage(string imageUrl, DownloadImageCallback callback)
        {
            if (webBrowser==null||webBrowser.GetBrowserHost()==null)
            {
                OpenUrl(imageUrl);
            }
            webBrowser.GetBrowserHost().DownloadImage(imageUrl, false, 0, false, callback);
        }


        private void cefMenu_CopyImageToClipboard(object sender, CopyImageEventArgs e)
        {
           // DownloadObject dobj = new DownloadObject(e.url, ResourceType.Image, DownloadToWhere.ClipBoard);
           // DownloadManager.RegisterDownloadObject(dobj);
            webBrowser.GetBrowserHost().DownloadImage(e.url, false, 0, false, new DownloadImageCallback((url, code, image,ex) =>
                {
                    if (image!=null)
                    {
                        Clipboard.SetImage(image);
                    }
                }));
        }

        protected void webBrowser_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            Console.WriteLine("webBrowser_PreviewKeyDown:" + e.Modifiers + "+" + e.KeyCode);
            if (e.Modifiers==Keys.None)
            {
                switch (e.KeyCode)
                {
                    case Keys.F12:
                        {
                            if (cefDevContainer.Tag != null)
                            {
                                CloseDevTools();
                            }
                            else
                            {
                                ShowDevTools();
                            }
                        }
                        break;
                    case Keys.F5:
                        {
                            RefreshPage();
                        }
                        break;
                    case Keys.F6:
                        {
                            FrmOpenScripts frm = new FrmOpenScripts(this);
                            frm.ShowDialog(this);
                        }
                        break;
                    default:
                        break;
                }
            }
        }


        private void cefMenu_ShowDevTool(object sender, EventArgs e)
        {
            this.InvokeOnUiThreadIfRequired(new Action(() =>
            {
                ShowDevTools();
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
                    if (e.Frame.IsMain && !string.IsNullOrWhiteSpace(_injectScript))
                    {
                        e.Frame.ExecuteJavaScriptAsync(_injectScript);
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
                   btnBack.Enabled = e.Browser.CanGoBack;
                   btnNext.Enabled = e.Browser.CanGoForward;

                   if (e.IsLoading)
                   {
                       btnRefresh.Text = "终止";
                       btnRefresh.Tooltip = "终止加载";
                   }
                   else
                   {
                       btnRefresh.Text = "刷新";
                       btnRefresh.Tooltip = "刷新页面";
                   }
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

        public void RefreshPage()
        {
            if (webBrowser != null)
            {
                if (webBrowser.IsLoading)
                {
                    webBrowser.Stop();
                }
                else
                {
                    webBrowser.Reload(true);
                }
            }
        }
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            RefreshPage();
        }

        [DllImport("user32.dll", SetLastError = true)]
        private static extern IntPtr GetWindow(IntPtr hWnd, GetWindowCmd uCmd);
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
        private static extern int MoveWindow(IntPtr hWnd, int x, int y, int nWidth, int nHeight, bool BRePaint);
        private const int WM_CLOSE = 0x10;
        [DllImport("user32.dll", EntryPoint = "SendMessageA")]
        private static extern int SendMessage(IntPtr hwnd, int wMsg, int wParam, int lParam);
        [DllImport("user32.dll", EntryPoint = "SetParent")]
        private static extern int SetParent(IntPtr hWndChild, IntPtr hWndNewParent);
        private enum GetWindowCmd : uint
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
            CloseDevTools();
        }

        private void cefDevContainer_ChangeDockPosition(object sender, EventArgs e)
        {
            splitContainer.Orientation = splitContainer.Orientation == Orientation.Horizontal ? Orientation.Vertical : Orientation.Horizontal;
            if (splitContainer.Orientation== Orientation.Horizontal)
            {
                splitContainer.SplitterDistance = this.Height - 300;
            }
            else
            {
                splitContainer.SplitterDistance = this.Width - 300;
            }
        }

        public void ExcuteScript()
        {
            if (!string.IsNullOrWhiteSpace(_injectScript))
            {
                webBrowser.GetMainFrame().ExecuteJavaScriptAsync(_injectScript);
            }
        }
    }
}
