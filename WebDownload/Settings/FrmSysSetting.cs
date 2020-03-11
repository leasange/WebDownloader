using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WebDownloader.Settings
{
    public partial class FrmSysSetting : DevComponents.DotNetBar.Office2007Form
    {
        public FrmSysSetting()
        {
            InitializeComponent();
            tbUserAgent.Text = SysSettingManager.Instance.UserAgent;
        }

        private void biUAWechat_Click(object sender, EventArgs e)
        {
            tbUserAgent.Text = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/53.0.2785.116 Safari/537.36 QBCore/4.0.1295.400 QQBrowser/9.0.2524.400 Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/53.0.2875.116 Safari/537.36 NetType/WIFI MicroMessenger/7.0.5 WindowsWechat";
        }

        private void biUADefault_Click(object sender, EventArgs e)
        {
            tbUserAgent.Text = "";
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SysSettingManager.Instance.UserAgent = tbUserAgent.Text.Trim();
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }
    }
}
