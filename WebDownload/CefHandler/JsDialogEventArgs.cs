using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebDownloader.CefHandler
{
    public class JsDialogEventArgs:EventArgs
    {
        public CefSharp.CefJsDialogType dialogType;
        public string messageText;
        public string defaultPromptText;
        public CefSharp.IJsDialogCallback callback;
        public bool suppressMessage;
        public bool result=false;
        public JsDialogEventArgs(CefSharp.CefJsDialogType dialogType, string messageText, string defaultPromptText, CefSharp.IJsDialogCallback callback, bool suppressMessage)
        {
            this.dialogType = dialogType;
            this.messageText = messageText;
            this.defaultPromptText = defaultPromptText;
            this.callback = callback;
            this.suppressMessage = suppressMessage;
        }
    }
}
