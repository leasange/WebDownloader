using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebDownloader.CefHandler
{
    public class CefResourceRequestHandlerFactory : CefSharp.IResourceRequestHandlerFactory
    {
        private bool _hasHandlers = true;
        public CefSharp.IResourceRequestHandler GetResourceRequestHandler(CefSharp.IWebBrowser chromiumWebBrowser, CefSharp.IBrowser browser, CefSharp.IFrame frame, CefSharp.IRequest request, bool isNavigation, bool isDownload, string requestInitiator, ref bool disableDefaultHandling)
        {
            if (request.ResourceType== CefSharp.ResourceType.Image)
            {
                return new CefResourceRequestHandler();
            }
            return null;
        }

        public bool HasHandlers
        {
            get { return _hasHandlers; }
        }
    }
}
