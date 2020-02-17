using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebDownloader.CefHandler
{
    public class OpenLinkOrSourceArgs:EventArgs
    {
        public string url
        {
            get;
            private set;
        }
        public bool isOnlySource
        {
            get;
            private set;
        }
        public OpenLinkOrSourceArgs(string url, bool isOnlySource)
        {
            this.url = url;
            this.isOnlySource = isOnlySource;
        }
    }
}
