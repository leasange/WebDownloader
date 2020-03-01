using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebDownloader.CefHandler
{
    public class GetResourceRequestArgs:EventArgs
    {
        public NameValueCollection Headers;
        public GetResourceRequestArgs()
        {

        }
    }
}
