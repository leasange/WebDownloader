using CefSharp;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebDownloader.CefHandler
{
    public class CefRequestHandler : CefSharp.Handler.RequestHandler
    {
        public event EventHandler<GetResourceRequestArgs> GetResourceRequest;
        protected override IResourceRequestHandler GetResourceRequestHandler(IWebBrowser chromiumWebBrowser, IBrowser browser, IFrame frame, IRequest request, bool iNavigation, bool isDownload, string requestInitiator, ref bool disableDefaultHandling)
        {
            if (GetResourceRequest!=null)
            {
                var args = new GetResourceRequestArgs();
                GetResourceRequest(this,args);
                return new CustomResourceRequestHandler(args);
            }
            else
            {
                return base.GetResourceRequestHandler(chromiumWebBrowser, browser, frame, request, iNavigation, isDownload, requestInitiator, ref disableDefaultHandling);
            }
        }
    }

    public class CustomResourceRequestHandler : CefSharp.Handler.ResourceRequestHandler
    {
        public GetResourceRequestArgs getArgs;
        public CustomResourceRequestHandler(GetResourceRequestArgs args)
        {
            this.getArgs = args;
        }
        protected override CefReturnValue OnBeforeResourceLoad(IWebBrowser chromiumWebBrowser, IBrowser browser, IFrame frame, IRequest request, IRequestCallback callback)
        {
            if (getArgs!=null)
            {
                if (getArgs.Headers!=null&&getArgs.Headers.Count>0)
                {
                    foreach (string item in getArgs.Headers)
                    {
                        request.SetHeaderByName(item, getArgs.Headers[item], true);
                    }
                }
            }
            return CefReturnValue.Continue;
        }
    }
}
