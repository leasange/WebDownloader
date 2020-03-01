using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebDownloader.CefHandler
{
    public class CefJsDialogHandler : CefSharp.IJsDialogHandler
    {
        public event EventHandler<JsDialogEventArgs> JsDialog;
        public bool OnBeforeUnloadDialog(CefSharp.IWebBrowser chromiumWebBrowser, CefSharp.IBrowser browser, string messageText, bool isReload, CefSharp.IJsDialogCallback callback)
        {
            return true;
        }

        public void OnDialogClosed(CefSharp.IWebBrowser chromiumWebBrowser, CefSharp.IBrowser browser)
        {
            
        }

        public bool OnJSDialog(CefSharp.IWebBrowser chromiumWebBrowser, CefSharp.IBrowser browser, string originUrl, CefSharp.CefJsDialogType dialogType, string messageText, string defaultPromptText, CefSharp.IJsDialogCallback callback, ref bool suppressMessage)
        {
            if (JsDialog!=null)
            {
                var args = new JsDialogEventArgs(dialogType, messageText, defaultPromptText, callback, suppressMessage);
                JsDialog(this, args);
                suppressMessage = args.suppressMessage;
                return args.result;
            }
            return false;
        }

        public void OnResetDialogState(CefSharp.IWebBrowser chromiumWebBrowser, CefSharp.IBrowser browser)
        {
        }
    }
}
