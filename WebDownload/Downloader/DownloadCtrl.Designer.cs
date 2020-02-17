namespace WebDownloader.Downloader
{
    partial class DownloadCtrl
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
            this.superGridControl = new DevComponents.DotNetBar.SuperGrid.SuperGridControl();
            this.SuspendLayout();
            // 
            // superGridControl
            // 
            this.superGridControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.superGridControl.FilterExprColors.SysFunction = System.Drawing.Color.DarkRed;
            this.superGridControl.Location = new System.Drawing.Point(0, 0);
            this.superGridControl.Name = "superGridControl";
            this.superGridControl.PrimaryGrid.SelectionGranularity = DevComponents.DotNetBar.SuperGrid.SelectionGranularity.RowWithCellHighlight;
            this.superGridControl.PrimaryGrid.ShowTreeLines = true;
            this.superGridControl.Size = new System.Drawing.Size(643, 203);
            this.superGridControl.TabIndex = 0;
            this.superGridControl.Text = "superGridControl1";
            // 
            // DownloadCtrl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.superGridControl);
            this.Name = "DownloadCtrl";
            this.Size = new System.Drawing.Size(643, 203);
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.SuperGrid.SuperGridControl superGridControl;
    }
}
