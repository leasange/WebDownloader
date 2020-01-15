namespace WebDownloader.Browser
{
    partial class CefWebBrowerX
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
            this.barStatus = new DevComponents.DotNetBar.Bar();
            this.lbTips = new DevComponents.DotNetBar.LabelItem();
            this.panelEx1 = new DevComponents.DotNetBar.PanelEx();
            this.tbUrl = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.btnRefresh = new DevComponents.DotNetBar.ButtonX();
            this.btnEnter = new DevComponents.DotNetBar.ButtonX();
            this.btnNew = new DevComponents.DotNetBar.ButtonX();
            this.btnNext = new DevComponents.DotNetBar.ButtonX();
            this.btnBack = new DevComponents.DotNetBar.ButtonX();
            this.panelBrowser = new System.Windows.Forms.Panel();
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.cefDevContainer = new WebDownloader.Browser.CefContainerControl();
            ((System.ComponentModel.ISupportInitialize)(this.barStatus)).BeginInit();
            this.panelEx1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // barStatus
            // 
            this.barStatus.AntiAlias = true;
            this.barStatus.BarType = DevComponents.DotNetBar.eBarType.StatusBar;
            this.barStatus.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barStatus.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.barStatus.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.lbTips});
            this.barStatus.Location = new System.Drawing.Point(0, 332);
            this.barStatus.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.barStatus.Name = "barStatus";
            this.barStatus.RoundCorners = false;
            this.barStatus.Size = new System.Drawing.Size(719, 22);
            this.barStatus.Stretch = true;
            this.barStatus.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.barStatus.TabIndex = 3;
            this.barStatus.TabStop = false;
            this.barStatus.Text = "bar1";
            // 
            // lbTips
            // 
            this.lbTips.Name = "lbTips";
            this.lbTips.Text = "...";
            // 
            // panelEx1
            // 
            this.panelEx1.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx1.Controls.Add(this.tbUrl);
            this.panelEx1.Controls.Add(this.btnRefresh);
            this.panelEx1.Controls.Add(this.btnEnter);
            this.panelEx1.Controls.Add(this.btnNew);
            this.panelEx1.Controls.Add(this.btnNext);
            this.panelEx1.Controls.Add(this.btnBack);
            this.panelEx1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelEx1.Font = new System.Drawing.Font("微软雅黑", 7.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.panelEx1.Location = new System.Drawing.Point(0, 0);
            this.panelEx1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panelEx1.Name = "panelEx1";
            this.panelEx1.Size = new System.Drawing.Size(719, 26);
            this.panelEx1.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx1.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx1.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx1.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx1.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx1.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx1.Style.GradientAngle = 90;
            this.panelEx1.TabIndex = 4;
            // 
            // tbUrl
            // 
            this.tbUrl.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.tbUrl.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.HistoryList;
            // 
            // 
            // 
            this.tbUrl.Border.Class = "TextBoxBorder";
            this.tbUrl.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.tbUrl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbUrl.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbUrl.Location = new System.Drawing.Point(103, 0);
            this.tbUrl.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tbUrl.Name = "tbUrl";
            this.tbUrl.Size = new System.Drawing.Size(529, 26);
            this.tbUrl.TabIndex = 9;
            this.tbUrl.KeyUp += new System.Windows.Forms.KeyEventHandler(this.tbUrl_KeyUp);
            // 
            // btnRefresh
            // 
            this.btnRefresh.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnRefresh.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnRefresh.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnRefresh.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnRefresh.Location = new System.Drawing.Point(66, 0);
            this.btnRefresh.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(37, 26);
            this.btnRefresh.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnRefresh.TabIndex = 8;
            this.btnRefresh.Text = "刷新";
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // btnEnter
            // 
            this.btnEnter.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnEnter.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnEnter.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnEnter.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnEnter.Location = new System.Drawing.Point(632, 0);
            this.btnEnter.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnEnter.Name = "btnEnter";
            this.btnEnter.Size = new System.Drawing.Size(50, 26);
            this.btnEnter.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnEnter.TabIndex = 6;
            this.btnEnter.Text = "打开";
            this.btnEnter.Click += new System.EventHandler(this.btnEnter_Click);
            // 
            // btnNew
            // 
            this.btnNew.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnNew.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnNew.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnNew.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnNew.Location = new System.Drawing.Point(682, 0);
            this.btnNew.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(37, 26);
            this.btnNew.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnNew.TabIndex = 4;
            this.btnNew.Text = "新建";
            this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
            // 
            // btnNext
            // 
            this.btnNext.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnNext.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnNext.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnNext.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnNext.Location = new System.Drawing.Point(33, 0);
            this.btnNext.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(33, 26);
            this.btnNext.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnNext.TabIndex = 3;
            this.btnNext.Text = "→";
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // btnBack
            // 
            this.btnBack.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnBack.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnBack.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnBack.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnBack.Location = new System.Drawing.Point(0, 0);
            this.btnBack.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(33, 26);
            this.btnBack.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnBack.TabIndex = 2;
            this.btnBack.Text = "←";
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // panelBrowser
            // 
            this.panelBrowser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelBrowser.Location = new System.Drawing.Point(0, 0);
            this.panelBrowser.Name = "panelBrowser";
            this.panelBrowser.Size = new System.Drawing.Size(511, 306);
            this.panelBrowser.TabIndex = 5;
            // 
            // splitContainer
            // 
            this.splitContainer.BackColor = System.Drawing.Color.PaleTurquoise;
            this.splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer.Location = new System.Drawing.Point(0, 26);
            this.splitContainer.Name = "splitContainer";
            // 
            // splitContainer.Panel1
            // 
            this.splitContainer.Panel1.Controls.Add(this.panelBrowser);
            // 
            // splitContainer.Panel2
            // 
            this.splitContainer.Panel2.Controls.Add(this.cefDevContainer);
            this.splitContainer.Size = new System.Drawing.Size(719, 306);
            this.splitContainer.SplitterDistance = 511;
            this.splitContainer.TabIndex = 6;
            // 
            // cefDevContainer
            // 
            this.cefDevContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cefDevContainer.Location = new System.Drawing.Point(0, 0);
            this.cefDevContainer.Name = "cefDevContainer";
            this.cefDevContainer.Size = new System.Drawing.Size(204, 306);
            this.cefDevContainer.TabIndex = 0;
            this.cefDevContainer.Close += new System.EventHandler(this.cefDevContainer_Close);
            this.cefDevContainer.ChangeDockPosition += new System.EventHandler(this.cefDevContainer_ChangeDockPosition);
            this.cefDevContainer.SizeChanged += new System.EventHandler(this.cefDevContainer_SizeChanged);
            // 
            // CefWebBrowerX
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.splitContainer);
            this.Controls.Add(this.panelEx1);
            this.Controls.Add(this.barStatus);
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "CefWebBrowerX";
            this.Size = new System.Drawing.Size(719, 354);
            this.Load += new System.EventHandler(this.CefWebBrowerX_Load);
            ((System.ComponentModel.ISupportInitialize)(this.barStatus)).EndInit();
            this.panelEx1.ResumeLayout(false);
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
            this.splitContainer.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.Bar barStatus;
        private DevComponents.DotNetBar.LabelItem lbTips;
        private DevComponents.DotNetBar.PanelEx panelEx1;
        private DevComponents.DotNetBar.Controls.TextBoxX tbUrl;
        private DevComponents.DotNetBar.ButtonX btnRefresh;
        private DevComponents.DotNetBar.ButtonX btnEnter;
        private DevComponents.DotNetBar.ButtonX btnNew;
        private DevComponents.DotNetBar.ButtonX btnNext;
        private DevComponents.DotNetBar.ButtonX btnBack;
        private System.Windows.Forms.Panel panelBrowser;
        private System.Windows.Forms.SplitContainer splitContainer;
        private CefContainerControl cefDevContainer;
    }
}
