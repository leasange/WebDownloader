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
using CefSharp.Web;
using CefSharp;

namespace WebDownloader.Browser
{
    public partial class ChromiumWebBrowserX : ChromiumWebBrowser
    {
        public ChromiumWebBrowserX():base()
        {
            InitializeComponent();
        }
        //
        // 摘要: 
        //     Initializes a new instance of the CefSharp.WinForms.ChromiumWebBrowser class.
        //
        // 参数: 
        //   html:
        //     html string to be initially loaded in the browser.
        //
        //   requestContext:
        //     Request context that will be used for this browser instance, if null the
        //     Global Request Context will be used
        public ChromiumWebBrowserX(HtmlString html, IRequestContext requestContext = null):base(html,requestContext)
        {
            InitializeComponent();
        }
        //
        // 摘要: 
        //     Initializes a new instance of the CefSharp.WinForms.ChromiumWebBrowser class.
        //
        // 参数: 
        //   address:
        //     The address.
        //
        //   requestContext:
        //     Request context that will be used for this browser instance, if null the
        //     Global Request Context will be used
        public ChromiumWebBrowserX(string address, IRequestContext requestContext = null):base(address,requestContext)
        {
            InitializeComponent();
        }

     /*   public override bool PreProcessMessage(ref Message msg)
        { 
            const int WM_SYSKEYDOWN = 0x104;
            const int WM_KEYDOWN = 0x100;
            const int WM_KEYUP = 0x101;
            const int WM_SYSKEYUP = 0x105;
            const int WM_CHAR = 0x102;
            const int WM_SYSCHAR = 0x106;
            const int VK_TAB = 0x9;
            const int VK_LEFT = 0x25;
            const int VK_UP = 0x26;
            const int VK_RIGHT = 0x27;
            const int VK_DOWN = 0x28;
            bool ret = base.PreProcessMessage(ref msg);
            if (msg.Msg == WM_CHAR)
            {
               // WParam = new IntPtr(windowsKeyCode),
               // LParam = new IntPtr(nativeKeyCode)
                Keys w = (Keys)msg.WParam.ToInt32();
                OnKeyUp(new KeyEventArgs(w));
            }
            return ret;
        }*/
    }
}
