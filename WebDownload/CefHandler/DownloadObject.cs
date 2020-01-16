using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WebDownloader.CefHandler
{
    public delegate void StreamFinishCallBack(DownloadObject downloadObject);
    public enum DownloadToWhere
    {
        Cache,
        Local,
        Memory,
        ClipBoard
    }
    public class DownloadObject
    {
        public int Id;
        public string url;
        public CefSharp.DownloadItem downloadItem = null;
        public DownloadToWhere toWhere { get; private set; }
        public string localPathFile { get; private set; }
        
        public CefSharp.ResourceType resType{get;private set;}

        public Stream streamSave{get;private set;}

        public StreamFinishCallBack callback = null;

        public bool finish { get; private set; }
        private bool isForceDownload = false;
        public DownloadObject(string url, CefSharp.ResourceType resType = CefSharp.ResourceType.Xhr, DownloadToWhere toWhere = DownloadToWhere.Cache, string pathFile = null, bool isForceDownload=false,StreamFinishCallBack callback = null)
        {
            this.url = url;
            this.resType = resType;
            this.toWhere = toWhere;
            this.localPathFile = pathFile;
            this.callback = callback;
            this.isForceDownload = isForceDownload;
            this.finish = false;
            Uri uri = new Uri(this.url);
            string host = uri.Host/*.Replace('.', '_')*/;
            string file = Path.Combine(Application.StartupPath, "WebCaches", host, uri.LocalPath.Trim('/')).Replace('\\', '/').TrimEnd('/');
            if (this.toWhere == DownloadToWhere.Local)
            {
                if (!string.IsNullOrWhiteSpace(this.localPathFile))
                {
                    file = this.localPathFile;
                }
            }

            string name = Path.GetFileName(file);
            string path = file.Substring(0, file.Length - name.Length - 1);
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            file = Path.Combine(path, name).Replace('/','\\');
            this.localPathFile = file;
        }

        public bool Finish()
        {
            if (finish)
            {
                return true;
            }
            finish = true;
            if (streamSave!=null)
            {
                streamSave.Flush();
                streamSave.Close();
                streamSave.Dispose();
                streamSave = null;
            }
            switch (this.toWhere)
            {
                case DownloadToWhere.Memory:
                case DownloadToWhere.ClipBoard:
                    {
                        if (!File.Exists(this.localPathFile))
                        {
                            return false;
                        }
                        var fs = File.Open(this.localPathFile, FileMode.Open);
                        if (fs.Length==0)
                        {
                            return false;
                        }
                        MemoryStream ms = new MemoryStream();
                        fs.CopyTo(ms);
                        fs.Close();
                        fs.Dispose();
                        File.Delete(this.localPathFile);
                        streamSave = ms;
                        if (this.toWhere== DownloadToWhere.ClipBoard)
                        {
                            if (resType== CefSharp.ResourceType.Image)
                            {
                                Image image = Image.FromStream(ms);
                                Clipboard.SetImage(image);
                                image.Dispose();
                            }
                            else
                            {
                                Clipboard.SetDataObject(streamSave);
                            }
                        }
                    }
                    break;
                default:
                    break;
            }
            if (callback!=null)
            {
                callback(this);
            }
            return true;
        }
    }
}
