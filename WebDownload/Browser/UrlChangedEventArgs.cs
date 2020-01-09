using DevComponents.DotNetBar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebDownloader.Browser
{
    public class UrlChangedEventArgs : EventArgs
    {
        public SuperTabItem tabItem;
        public CefWebBrowerX cefWebBrowerX;
    }
}
