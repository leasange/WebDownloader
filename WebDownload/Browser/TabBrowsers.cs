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

namespace WebDownloader.Browser
{
    public partial class TabBrowsers : UserControl
    {
        public TabBrowsers()
        {
            InitializeComponent();
        }

        private CefWebBrowerX NewBrowser(string url=null)
        {
            try
            {
                var superItem = new SuperTabItem();
                superItem.Text = "空白页";
                SuperTabControlPanel superTabControlPanel = new SuperTabControlPanel();

                superItem.AttachedControl = superTabControlPanel;
                superTabControlPanel.TabItem = superItem;

                CefWebBrowerX cefWebBrowerX = new CefWebBrowerX();
                cefWebBrowerX.Dock = DockStyle.Fill;

                superTabControlPanel.Controls.Add(cefWebBrowerX);

                this.superTabControlX.Tabs.Add(superItem);

                this.superTabControlX.Controls.Add(superTabControlPanel);
                this.superTabControlX.SelectedTab = superItem;

                cefWebBrowerX.OpenUrl(url);
                return cefWebBrowerX;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public void OpenUrl(string url)
        {
            CefWebBrowerX cefWebBrowerX = null;
            if (this.superTabControlX.Tabs.Count > 0)
            {
                var  superItem = this.superTabControlX.SelectedTab;
                cefWebBrowerX = superItem.AttachedControl.Controls[0] as CefWebBrowerX;
            }
            else
            {
                cefWebBrowerX = NewBrowser();
            }
            cefWebBrowerX.OpenUrl(url);
        }

        private void TabBrowsers_Load(object sender, EventArgs e)
        {
            if (!this.DesignMode)
            {
                this.superTabControlX.Tabs.Clear();
                NewBrowser("about:blank");
            }
        }
    }
}
