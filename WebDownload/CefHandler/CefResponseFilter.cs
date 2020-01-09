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
        private FileStream fileStream=null;
        private string _url;
        public CefResponseFilter(string url)
        {
            _url = url;
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

            if (fileStream!=null)
            {
                dataIn.Position = positionIn;
                dataIn.CopyTo(fileStream);
            }
            return CefSharp.FilterStatus.Done;
        }

        public bool InitFilter()
        {
            try
            {
                Uri uri = new Uri(_url);
                string host = uri.Host.Replace('.', '_');
                string file = Path.Combine(Application.StartupPath, "WebImages",host, uri.LocalPath.Trim('/')).Replace('\\', '/').TrimEnd('/');
                string name = Path.GetFileName(file);
                string path = file.Substring(0, file.Length - name.Length - 1);
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                if (!name.Contains('.'))
                {
                    name = name + ".jpg";
                }
                file = Path.Combine(path, name);
                if (File.Exists(file))
                {
                    return true; 
                }
                fileStream = File.OpenWrite(file);
            }
            catch (Exception ex)
            {
            }

            return true;
        }

        public void Dispose()
        {
            if (fileStream!=null)
            {
                fileStream.Flush();
                fileStream.Close();
                fileStream.Dispose();
            }
        }
    }
}
