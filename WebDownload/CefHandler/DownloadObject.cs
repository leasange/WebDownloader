using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebDownloader.CefHandler
{
    public delegate void StreamFinishCallBack(DownloadObject downloadObject);
    public class DownloadObject
    {
        public bool isCache { get; private set; }
        public string url;
        public CefSharp.ResourceType resType;
        public Stream streamSave = null;
        public StreamFinishCallBack callback = null;
        public DownloadObject(string url, CefSharp.ResourceType resType, bool isCache)
        {
            this.url = url;
            this.resType = resType;
            this.isCache = isCache;
        }
        public DownloadObject(string url, CefSharp.ResourceType resType, Stream streamSave = null, StreamFinishCallBack callback = null)
        {
            this.url = url;
            this.resType = resType;
            this.streamSave = streamSave;
            this.callback = callback;
            this.isCache = false;
        }
    }
}
