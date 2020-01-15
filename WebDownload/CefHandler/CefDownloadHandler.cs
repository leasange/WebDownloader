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
            if (!callback.IsDisposed)
            {
                var find = DownloadManager.Find(downloadItem.OriginalUrl);
                if (find != null)
                {
                    find.Id = downloadItem.Id;
                    callback.Continue(find.localPathFile, false);
                }
            }
        }

        public void OnDownloadUpdated(CefSharp.IWebBrowser chromiumWebBrowser, CefSharp.IBrowser browser, CefSharp.DownloadItem downloadItem, CefSharp.IDownloadItemCallback callback)
        {
            if (downloadItem.PercentComplete==100||downloadItem.IsCancelled||downloadItem.IsComplete)
            {
                DownloadManager.Remove(downloadItem.OriginalUrl);
            }
        }
    }
}
