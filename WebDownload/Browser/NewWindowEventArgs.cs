using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebDownloader.Browser
{
    public class NewWindowEventArgs : EventArgs
    {
        public CefSharp.IWebBrowser chromiumWebBrowser { get; private set; }
        public CefSharp.IBrowser browser { get; private set; }
        public CefSharp.IFrame frame { get; private set; }
        public string targetUrl { get; private set; }
        public string targetFrameName { get; private set; }
        public CefSharp.WindowOpenDisposition targetDisposition { get; private set; }
        public CefSharp.IWindowInfo windowInfo { get; private set; }
        public CefSharp.IWebBrowser newBrowser { get; set; }
        public NewWindowEventArgs(CefSharp.IWebBrowser chromiumWebBrowser, CefSharp.IBrowser browser, CefSharp.IFrame frame, string targetUrl, string targetFrameName, CefSharp.WindowOpenDisposition targetDisposition, CefSharp.IWindowInfo windowInfo)
        {
            this.chromiumWebBrowser = chromiumWebBrowser;
            this.browser = browser;
            this.frame = frame;
            this.targetUrl = targetUrl;
            this.targetFrameName = targetFrameName;
            this.targetDisposition = targetDisposition;
            this.windowInfo = windowInfo;
        }
    }
}
