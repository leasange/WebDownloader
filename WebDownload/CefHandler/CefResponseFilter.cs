using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WebDownloader.CefHandler
{
    public class CefResponseFilter : CefSharp.IResponseFilter
    {
        private DownloadObject downloadObject = null;
        public CefResponseFilter(DownloadObject downloadObject)
        {
            this.downloadObject = downloadObject;
        }
        public CefSharp.FilterStatus Filter(System.IO.Stream dataIn, out long dataInRead, System.IO.Stream dataOut, out long dataOutWritten)
        {
            dataInRead = 0;
            dataOutWritten = 0;
            if (dataIn==null)
            {
                return CefSharp.FilterStatus.Done;
            }
            long positionIn = dataIn.Position;
            long positionOut = dataOut.Position;

            dataIn.CopyTo(dataOut);
            dataInRead = dataIn.Position - positionIn;
            dataOutWritten = dataOut.Position - positionOut;

            if (downloadObject!=null&&downloadObject.streamSave != null)
            {
                dataIn.Position = positionIn;
                dataIn.CopyTo(downloadObject.streamSave);
            }
            return CefSharp.FilterStatus.Done;
        }

        public bool InitFilter()
        {
            try
            {
                if (downloadObject==null)
                {
                    return true;
                }
                if (!downloadObject.isCache)
                {
                    if (downloadObject.callback != null && downloadObject.streamSave == null)
                    {
                        downloadObject.streamSave = new MemoryStream();
                    }
                    return true;
                }

                Uri uri = new Uri(downloadObject.url);

                string host = uri.Host/*.Replace('.', '_')*/;
                string file = Path.Combine(Application.StartupPath, "WebCaches", host, uri.LocalPath.Trim('/')).Replace('\\', '/').TrimEnd('/');
                string name = Path.GetFileName(file);
                string path = file.Substring(0, file.Length - name.Length - 1);
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                /*if (!name.Contains('.'))
                {
                    name = name + ".jpg";
                }*/
                file = Path.Combine(path, name);
                if (File.Exists(file))
                {
                    return true; 
                }
                downloadObject.streamSave = File.OpenWrite(file);
            }
            catch (Exception ex)
            {
            }

            return true;
        }

        public void Dispose()
        {
            try
            {
                if (downloadObject==null)
                {
                    return;
                }
                if (downloadObject.callback != null)
                {
                    downloadObject.callback.BeginInvoke(downloadObject, null, null);
                }
                else
                {
                    if (downloadObject.streamSave != null)
                    {
                        downloadObject.streamSave.Flush();
                        downloadObject.streamSave.Close();
                        downloadObject.streamSave.Dispose();
                    }
                }
            }
            catch (Exception)
            { 
            }
        }
    }
}
