using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebDownloader.CefHandler
{
    public class CefDownloadHandler : CefSharp.IDownloadHandler
    {

        public void OnBeforeDownload(CefSharp.IWebBrowser chromiumWebBrowser, CefSharp.IBrowser browser, CefSharp.DownloadItem downloadItem, CefSharp.IBeforeDownloadCallback callback)
        {
            //throw new NotImplementedException();
        }

        public void OnDownloadUpdated(CefSharp.IWebBrowser chromiumWebBrowser, CefSharp.IBrowser browser, CefSharp.DownloadItem downloadItem, CefSharp.IDownloadItemCallback callback)
        {
            //throw new NotImplementedException();
        }
    }
}
