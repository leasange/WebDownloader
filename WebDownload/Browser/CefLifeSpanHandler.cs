using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebDownloader.Browser
{
    public class CefLifeSpanHandler : CefSharp.ILifeSpanHandler
    {
        public event EventHandler<NewWindowEventArgs> BeforePopupEvent;
        public CefLifeSpanHandler()
        {
        }
        public bool DoClose(CefSharp.IWebBrowser chromiumWebBrowser, CefSharp.IBrowser browser)
        {
            if (browser.IsDisposed || browser.IsPopup)
            {
                return false;
            }
            return true;
        }

        public void OnAfterCreated(CefSharp.IWebBrowser chromiumWebBrowser, CefSharp.IBrowser browser)
        {
            //throw new NotImplementedException();
        }

        public void OnBeforeClose(CefSharp.IWebBrowser chromiumWebBrowser, CefSharp.IBrowser browser)
        {
            //throw new NotImplementedException();
        }

        public bool OnBeforePopup(CefSharp.IWebBrowser chromiumWebBrowser, CefSharp.IBrowser browser, CefSharp.IFrame frame, string targetUrl, string targetFrameName, CefSharp.WindowOpenDisposition targetDisposition, bool userGesture, CefSharp.IPopupFeatures popupFeatures, CefSharp.IWindowInfo windowInfo, CefSharp.IBrowserSettings browserSettings, ref bool noJavascriptAccess, out CefSharp.IWebBrowser newBrowser)
        {
            newBrowser = null;
            if (BeforePopupEvent == null)
            {
                return false;
            }
            NewWindowEventArgs e = new NewWindowEventArgs(chromiumWebBrowser, browser, frame, targetUrl, targetFrameName, targetDisposition, windowInfo);
            BeforePopupEvent(this, e);
            if (e.newBrowser==null)
            {
                return false;
            }
            //newBrowser = e.newBrowser;
            return true;
        }
    }
}
