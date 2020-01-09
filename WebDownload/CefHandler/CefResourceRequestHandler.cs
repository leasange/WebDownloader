using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebDownloader.CefHandler
{
    public class CefResourceRequestHandler : CefSharp.Handler.ResourceRequestHandler
    {
        protected override CefSharp.CefReturnValue OnBeforeResourceLoad(CefSharp.IWebBrowser chromiumWebBrowser, CefSharp.IBrowser browser, CefSharp.IFrame frame, CefSharp.IRequest request, CefSharp.IRequestCallback callback)
        {
            return CefSharp.CefReturnValue.Continue;
        }
        protected override CefSharp.IResponseFilter GetResourceResponseFilter(CefSharp.IWebBrowser chromiumWebBrowser, CefSharp.IBrowser browser, CefSharp.IFrame frame, CefSharp.IRequest request, CefSharp.IResponse response)
        {
            return new CefResponseFilter(request.Url);
        }
        protected override bool OnResourceResponse(CefSharp.IWebBrowser chromiumWebBrowser, CefSharp.IBrowser browser, CefSharp.IFrame frame, CefSharp.IRequest request, CefSharp.IResponse response)
        {
            return base.OnResourceResponse(chromiumWebBrowser, browser, frame, request, response);
        }
    }
}
