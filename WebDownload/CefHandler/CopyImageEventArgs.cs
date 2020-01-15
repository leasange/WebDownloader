using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebDownloader.CefHandler
{
    public class CopyImageEventArgs:EventArgs
    {
        public string url;
        public CopyImageEventArgs(string url)
        {
            this.url = url;
        }
    }
}
