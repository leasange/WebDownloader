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
                downloadObject.Finish();
            }
            catch (Exception)
            { 
            }
        }
    }
}
