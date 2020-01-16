using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebDownloader.CefHandler
{
    public class CefDownloadHandler : CefSharp.IDownloadHandler
    {
        private static CefSharp.DownloadItem oldDownloadItem = null;
        private static CefSharp.DownloadItem lastDownloadItem = null;
        private static System.Windows.Forms.Timer timerUpdate = new System.Windows.Forms.Timer();
        static CefDownloadHandler()
        {
            timerUpdate.Interval = 200;
            timerUpdate.Tick += timerUpdate_Tick;
            timerUpdate.Start();
        }

        static void timerUpdate_Tick(object sender, EventArgs e)
        {
            if (lastDownloadItem!=oldDownloadItem)
            {
                DownloadManager.Update(lastDownloadItem);
                oldDownloadItem = lastDownloadItem;
            }
        }
        public void OnBeforeDownload(CefSharp.IWebBrowser chromiumWebBrowser, CefSharp.IBrowser browser, CefSharp.DownloadItem downloadItem, CefSharp.IBeforeDownloadCallback callback)
        {
            if (!callback.IsDisposed)
            {
                using (callback)
                {
                    var find = DownloadManager.Find(downloadItem.OriginalUrl);
                    if (find != null)
                    {
                        find.Id = downloadItem.Id;
                        Console.WriteLine("OnBeforeDownload downloadItem.Id=" + downloadItem.Id + ",downloadItem.OriginalUrl=" + downloadItem.OriginalUrl);
                        find.downloadItem = downloadItem;
                        callback.Continue(find.localPathFile, false);
                    } 
                }
            }
        }

        public void OnDownloadUpdated(CefSharp.IWebBrowser chromiumWebBrowser, CefSharp.IBrowser browser, CefSharp.DownloadItem downloadItem, CefSharp.IDownloadItemCallback callback)
        {
            if (downloadItem.IsCancelled||downloadItem.IsComplete||!downloadItem.IsInProgress||!downloadItem.IsValid)
            {
                Console.WriteLine("OnDownloadUpdated downloadItem.Id=" + downloadItem.Id + ",downloadItem.OriginalUrl=" + downloadItem.OriginalUrl);
                DownloadManager.Update(downloadItem);
                DownloadManager.Remove(downloadItem.Id);
            }
            else
            {
                lastDownloadItem = downloadItem;
            }
        }
    }
}
