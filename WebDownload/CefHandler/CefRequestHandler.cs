using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebDownloader.CefHandler
{
    public class CefRequestHandler : CefSharp.Handler.RequestHandler
    {
        protected override bool OnBeforeBrowse(CefSharp.IWebBrowser chromiumWebBrowser, CefSharp.IBrowser browser, CefSharp.IFrame frame, CefSharp.IRequest request, bool userGesture, bool isRedirect)
        {

            Console.WriteLine(request.ResourceType);

            return base.OnBeforeBrowse(chromiumWebBrowser, browser, frame, request, userGesture, isRedirect);
        }
    }
}
