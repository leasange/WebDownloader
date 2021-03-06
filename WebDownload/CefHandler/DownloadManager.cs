﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebDownloader.CefHandler
{
    public class DownloadManager
    {
        private static List<DownloadObject> registerDownloadObjects = new List<DownloadObject>();
        public static void RegisterDownloadObject(DownloadObject obj)
        {
            lock (registerDownloadObjects)
            {
                registerDownloadObjects.RemoveAll(m =>
                {
                    if (m.url==obj.url)
                    {
                        m.Finish();
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                });
                registerDownloadObjects.Add(obj);
            }
        }
        public static DownloadObject Find(string url)
        {
            lock (registerDownloadObjects)
            {
                return registerDownloadObjects.Find(m => m.url == url);
            }
        }

        public static DownloadObject Find(int id)
        {
            lock (registerDownloadObjects)
            {
                return registerDownloadObjects.Find(m => m.Id == id);
            }
        }

        public static void Remove(string url)
        {
            lock (registerDownloadObjects)
            {
                registerDownloadObjects.RemoveAll(m =>
                {
                    if (m.url == url)
                    {
                        m.Finish();
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                });
            }
        }
        public static void Remove(int id)
        {
            lock (registerDownloadObjects)
            {
                registerDownloadObjects.RemoveAll(m => {
                    if (m.Id == id)
                    {
                        m.Finish();
                        return true;
                    }
                    else return false;
                });
            }
        }

        internal static void Update(CefSharp.DownloadItem downloadItem)
        {
            var f = Find(downloadItem.Id);
            if (f!=null)
            {
                f.downloadItem = downloadItem;
            }
        }
    }
}
