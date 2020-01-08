namespace WebDownloader.Browser
{
    partial class TabBrowsers
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.superTabControlX = new WebDownloader.Control.SuperTabControlX();
            this.superTabControlPanel1 = new DevComponents.DotNetBar.SuperTabControlPanel();
            this.superTabItem1 = new DevComponents.DotNetBar.SuperTabItem();
            ((System.ComponentModel.ISupportInitialize)(this.superTabControlX)).BeginInit();
            this.superTabControlX.SuspendLayout();
            this.SuspendLayout();
            // 
            // superTabControlX
            // 
            this.superTabControlX.CloseButtonOnTabsVisible = true;
            // 
            // 
            // 
            // 
            // 
            // 
            this.superTabControlX.ControlBox.CloseBox.Name = "";
            // 
            // 
            // 
            this.superTabControlX.ControlBox.MenuBox.Name = "";
            this.superTabControlX.ControlBox.Name = "";
            this.superTabControlX.ControlBox.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.superTabControlX.ControlBox.MenuBox,
            this.superTabControlX.ControlBox.CloseBox});
            this.superTabControlX.Controls.Add(this.superTabControlPanel1);
            this.superTabControlX.Dock = System.Windows.Forms.DockStyle.Fill;
            this.superTabControlX.Location = new System.Drawing.Point(0, 0);
            this.superTabControlX.Name = "superTabControlX";
            this.superTabControlX.ReorderTabsEnabled = true;
            this.superTabControlX.SelectedTabFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold);
            this.superTabControlX.SelectedTabIndex = 0;
            this.superTabControlX.Size = new System.Drawing.Size(501, 288);
            this.superTabControlX.TabFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.superTabControlX.TabIndex = 1;
            this.superTabControlX.Tabs.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.superTabItem1});
            this.superTabControlX.Text = "superTabControlX1";
            // 
            // superTabControlPanel1
            // 
            this.superTabControlPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.superTabControlPanel1.Location = new System.Drawing.Point(0, 30);
            this.superTabControlPanel1.Name = "superTabControlPanel1";
            this.superTabControlPanel1.Size = new System.Drawing.Size(501, 258);
            this.superTabControlPanel1.TabIndex = 1;
            this.superTabControlPanel1.TabItem = this.superTabItem1;
            // 
            // superTabItem1
            // 
            this.superTabItem1.AttachedControl = this.superTabControlPanel1;
            this.superTabItem1.GlobalItem = false;
            this.superTabItem1.Name = "superTabItem1";
            this.superTabItem1.Text = "about:blank";
            // 
            // TabBrowsers
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.superTabControlX);
            this.Name = "TabBrowsers";
            this.Size = new System.Drawing.Size(501, 288);
            this.Load += new System.EventHandler(this.TabBrowsers_Load);
            ((System.ComponentModel.ISupportInitialize)(this.superTabControlX)).EndInit();
            this.superTabControlX.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Control.SuperTabControlX superTabControlX;
        private DevComponents.DotNetBar.SuperTabControlPanel superTabControlPanel1;
        private DevComponents.DotNetBar.SuperTabItem superTabItem1;
    }
}
