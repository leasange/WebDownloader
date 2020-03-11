namespace WebDownloader.Settings
{
    partial class FrmSysSetting
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.tbUserAgent = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.btnUASelect = new DevComponents.DotNetBar.ButtonX();
            this.biUAWechat = new DevComponents.DotNetBar.ButtonItem();
            this.biUADefault = new DevComponents.DotNetBar.ButtonItem();
            this.btnSave = new DevComponents.DotNetBar.ButtonX();
            this.btnCancel = new DevComponents.DotNetBar.ButtonX();
            this.SuspendLayout();
            // 
            // labelX1
            // 
            this.labelX1.AutoSize = true;
            // 
            // 
            // 
            this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX1.Location = new System.Drawing.Point(18, 15);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(99, 18);
            this.labelX1.TabIndex = 0;
            this.labelX1.Text = "全局UserAgent：";
            // 
            // tbUserAgent
            // 
            // 
            // 
            // 
            this.tbUserAgent.Border.Class = "TextBoxBorder";
            this.tbUserAgent.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.tbUserAgent.Location = new System.Drawing.Point(123, 12);
            this.tbUserAgent.Multiline = true;
            this.tbUserAgent.Name = "tbUserAgent";
            this.tbUserAgent.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.tbUserAgent.Size = new System.Drawing.Size(384, 56);
            this.tbUserAgent.TabIndex = 1;
            // 
            // btnUASelect
            // 
            this.btnUASelect.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnUASelect.AutoExpandOnClick = true;
            this.btnUASelect.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnUASelect.Location = new System.Drawing.Point(513, 12);
            this.btnUASelect.Name = "btnUASelect";
            this.btnUASelect.Size = new System.Drawing.Size(61, 56);
            this.btnUASelect.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnUASelect.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.biUADefault,
            this.biUAWechat});
            this.btnUASelect.TabIndex = 2;
            this.btnUASelect.Text = "选择";
            // 
            // biUAWechat
            // 
            this.biUAWechat.GlobalItem = false;
            this.biUAWechat.Name = "biUAWechat";
            this.biUAWechat.Text = "微信";
            this.biUAWechat.Click += new System.EventHandler(this.biUAWechat_Click);
            // 
            // biUADefault
            // 
            this.biUADefault.GlobalItem = false;
            this.biUADefault.Name = "biUADefault";
            this.biUADefault.Text = "默认";
            this.biUADefault.Click += new System.EventHandler(this.biUADefault_Click);
            // 
            // btnSave
            // 
            this.btnSave.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnSave.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnSave.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnSave.Location = new System.Drawing.Point(195, 365);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(105, 30);
            this.btnSave.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnSave.TabIndex = 3;
            this.btnSave.Text = "保存";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnCancel.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnCancel.Location = new System.Drawing.Point(334, 365);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(105, 30);
            this.btnCancel.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "取消";
            // 
            // FrmSysSetting
            // 
            this.AcceptButton = this.btnSave;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(600, 407);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnUASelect);
            this.Controls.Add(this.tbUserAgent);
            this.Controls.Add(this.labelX1);
            this.Name = "FrmSysSetting";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "系统设置";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.DotNetBar.Controls.TextBoxX tbUserAgent;
        private DevComponents.DotNetBar.ButtonX btnUASelect;
        private DevComponents.DotNetBar.ButtonItem biUAWechat;
        private DevComponents.DotNetBar.ButtonItem biUADefault;
        private DevComponents.DotNetBar.ButtonX btnSave;
        private DevComponents.DotNetBar.ButtonX btnCancel;
    }
}