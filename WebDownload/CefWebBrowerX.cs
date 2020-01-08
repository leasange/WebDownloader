using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CefSharp.WinForms;

namespace WebDownloader
{
    public partial class CefWebBrowerX : UserControl
    {
        private ChromiumWebBrowser webBrowser;
        public CefWebBrowerX()
        {
            InitializeComponent();
        }

        private void CefWebBrowerX_Load(object sender, EventArgs e)
        {
            webBrowser = new ChromiumWebBrowser();
        }
    }
}
