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

namespace WebDownloader.Control
{
    public partial class SuperTabControlX : DevComponents.DotNetBar.SuperTabControl
    {
        public SuperTabControlX()
        {
            InitializeComponent();
        }
        protected override void OnPaint(PaintEventArgs e)
        {
           // base.OnPaint(e);
        }
        /*private SuperTabItem CreateTab()
        {
            var superTabControlPanel = new DevComponents.DotNetBar.SuperTabControlPanel();
            superTabControlPanel.Dock = DockStyle.Fill;
            SuperTabItem superTabItem = new SuperTabItem();
            superTabItem.AttachedControl = superTabControlPanel;
            superTabControlPanel.TabItem = superTabItem;
            this.Tabs.Add(superTabItem);
            this.Controls.Add(superTabControlPanel);
            this.SelectedTab = superTabItem;
            return superTabItem;
        }*/
    }
}
