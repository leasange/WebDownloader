using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebDownloader.CefHandler
{
    public  class BeforeContextMenuEvenArgs:EventArgs
    {
        public CefSharp.IContextMenuParams parameters;
        public CefSharp.IMenuModel model;
        public BeforeContextMenuEvenArgs(CefSharp.IContextMenuParams parameters, CefSharp.IMenuModel model)
        {
            this.parameters = parameters;
            this.model = model;
        }
    }
}
