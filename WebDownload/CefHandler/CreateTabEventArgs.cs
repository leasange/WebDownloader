using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebDownloader.CefHandler
{
    public class CreateTabEventArgs
    {
        public bool selected { get; private set; }
        public string url { get; private set; }
        public CreateTabEventArgs(bool selected,string url)
        {
            this.selected = selected;
            this.url = url;
        }
    }
}
