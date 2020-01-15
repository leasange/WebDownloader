using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebDownloader.CefHandler
{
    public class CefResourceRequestHandler : CefSharp.Handler.ResourceRequestHandler
    {
        private static List<DownloadObject> RegisterDownloadObjects = new List<DownloadObject>();
        public static void RegisterDownloadObject(DownloadObject obj)
        {
            lock (RegisterDownloadObjects)
            {
                RegisterDownloadObjects.Add(obj);                
            }
        }
        protected override CefSharp.CefReturnValue OnBeforeResourceLoad(CefSharp.IWebBrowser chromiumWebBrowser, CefSharp.IBrowser browser, CefSharp.IFrame frame, CefSharp.IRequest request, CefSharp.IRequestCallback callback)
        {
            return CefSharp.CefReturnValue.Continue;
        }
        protected override CefSharp.IResponseFilter GetResourceResponseFilter(CefSharp.IWebBrowser chromiumWebBrowser, CefSharp.IBrowser browser, CefSharp.IFrame frame, CefSharp.IRequest request, CefSharp.IResponse response)
        {
            var first = RegisterDownloadObjects.Find(m => m.url == request.Url);
            if (first!=null)
            {
                lock (RegisterDownloadObjects)
                {
                    RegisterDownloadObjects.Remove(first);
                }
                return new CefResponseFilter(first);
            }
            return null;
        }
        protected override bool OnResourceResponse(CefSharp.IWebBrowser chromiumWebBrowser, CefSharp.IBrowser browser, CefSharp.IFrame frame, CefSharp.IRequest request, CefSharp.IResponse response)
        {
            return base.OnResourceResponse(chromiumWebBrowser, browser, frame, request, response);
        }
    }
}
