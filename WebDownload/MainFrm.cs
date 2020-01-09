using DevComponents.DotNetBar;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WebDownloader
{
    public partial class MainFrm : DevComponents.DotNetBar.Office2007Form
    {
        public MainFrm()
        {
            InitializeComponent();
            tabBrowsers.SuperTabControlX.SelectedTabChanged += SuperTabControlX_SelectedTabChanged;
            tabBrowsers.TitleChanged += tabBrowsers_TitleChanged;
        }

        private void tabBrowsers_TitleChanged(object sender, CefSharp.TitleChangedEventArgs e)
        {
            this.Text = tabBrowsers.SuperTabControlX.SelectedTab.Text;
        }

        private void SuperTabControlX_SelectedTabChanged(object sender, SuperTabStripSelectedTabChangedEventArgs e)
        {
            this.Text = e.NewValue.Text;
        }
    }
}
