using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using WebDownloader.Control;
using CefSharp;
using WebDownloader.CefHandler;

namespace WebDownloader.Browser
{
    public partial class TabBrowsers : UserControl
    {
        public event EventHandler<TitleChangedEventArgs> TitleChanged;
        public SuperTabControlX SuperTabControlX
        {
            get { return this.superTabControlX; }
        }

        public TabBrowsers()
        {
            InitializeComponent();
        }

        public CefWebBrowerX NewBrowser(string url=null)
        {
            try
            {
                var superItem = new SuperTabItem();
                superItem.Text = "空白页";
                superItem.TabFont = new Font("微软雅黑", 9f);

                SuperTabControlPanel superTabControlPanel = new SuperTabControlPanel();
                superItem.AttachedControl = superTabControlPanel;
                superTabControlPanel.TabItem = superItem;

                CefWebBrowerX cefWebBrowerX = new CefWebBrowerX();
                cefWebBrowerX.Dock = DockStyle.Fill;

                cefWebBrowerX.NewNavigateBrowser += cefWebBrowerX_NewTabEvent;
                cefWebBrowerX.LoadingStateChanged += cefWebBrowerX_LoadingStateChanged;
                cefWebBrowerX.FrameLoadStart += cefWebBrowerX_FrameLoadStart;
                cefWebBrowerX.CreateTab += cefWebBrowerX_CreateTab;
                cefWebBrowerX.TitleChanged += cefWebBrowerX_TitleChanged;

                superTabControlPanel.Controls.Add(cefWebBrowerX);
                this.superTabControlX.Tabs.Add(superItem);
                this.superTabControlX.SelectedTab = superItem;
                this.superTabControlX.Controls.Add(superTabControlPanel);

                cefWebBrowerX.OpenUrl(url);
                return cefWebBrowerX;
            }
            catch (Exception)
            {
                return null;
            }
            finally
            {
            }
        }

        private void cefWebBrowerX_TitleChanged(object sender, CefSharp.TitleChangedEventArgs e)
        {
            this.Invoke(new Action(() =>
                {
                    var item = GetTabItem((CefWebBrowerX)sender);
                    if (item != null)
                    {
                        item.Text = e.Title;
                        item.Tooltip = e.Title;
                        if (TitleChanged != null)
                        {
                            TitleChanged(item, e);
                        }
                    }
                }));
        }

        private void cefWebBrowerX_CreateTab(object sender, EventArgs e)
        {
            NewBrowser();
        }
        private SuperTabItem GetTabItem(CefWebBrowerX cefWebBrowerX)
        {
            foreach (SuperTabItem item in this.superTabControlX.Tabs)
            {
                var browser = item.AttachedControl.Controls[0] as CefWebBrowerX;
                if (browser != null && browser == cefWebBrowerX)
                {
                    return item;
                }
            }
            return null;
        }
        private void cefWebBrowerX_FrameLoadStart(object sender, CefSharp.FrameLoadStartEventArgs e)
        {
            if (e.Frame.IsMain)
            {
                SuperTabItem item = GetTabItem((CefWebBrowerX)sender);
                if (item == null)
                {
                    return;
                }
                
            }
        }

        private void cefWebBrowerX_LoadingStateChanged(object sender, CefSharp.LoadingStateChangedEventArgs e)
        {
            var cefWebBrowerX = (CefWebBrowerX)sender;
            SuperTabItem item = GetTabItem(cefWebBrowerX);
            if (item == null)
            {
                return;
            }
            if (e.IsLoading)
            {
                item.Text = "加载...";
            }
            else
            {
                item.Text = cefWebBrowerX.WebName;
            }
        }

        private void cefWebBrowerX_NewTabEvent(object sender, NewWindowEventArgs e)
        {
           var cefBrowser =  NewBrowser(e.targetUrl);
           e.newBrowser = cefBrowser.webBrowser;
        }

        public void OpenUrl(string url)
        {
            CefWebBrowerX cefWebBrowerX = null;
            if (this.superTabControlX.Tabs.Count > 0)
            {
                var  superItem = this.superTabControlX.SelectedTab;
                cefWebBrowerX = superItem.AttachedControl.Controls[0] as CefWebBrowerX;
                cefWebBrowerX.OpenUrl(url);
            }
            else
            {
                cefWebBrowerX = NewBrowser(url);
            }
        }

        private void TabBrowsers_Load(object sender, EventArgs e)
        {
            if (!this.DesignMode)
            {
                this.superTabControlX.Tabs.Clear();
                NewBrowser("about:blank");
            }
        }

        private void superTabControlX_SelectedTabChanged(object sender, SuperTabStripSelectedTabChangedEventArgs e)
        {

        }

        private void superTabControlX_TabItemClose(object sender, SuperTabStripTabItemCloseEventArgs e)
        {
            if (superTabControlX.Tabs.Count <= 1)
            {
                e.Cancel = true;
                OpenUrl(null);
            }
        }
    }
}
