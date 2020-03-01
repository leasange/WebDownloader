namespace WebDownloader
{
    partial class FrmOpenScripts
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
            this.treeScripts = new DevComponents.AdvTree.AdvTree();
            this.node1 = new DevComponents.AdvTree.Node();
            this.nodeConnector1 = new DevComponents.AdvTree.NodeConnector();
            this.elementStyle1 = new DevComponents.DotNetBar.ElementStyle();
            this.tbScript = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.treeScripts)).BeginInit();
            this.SuspendLayout();
            // 
            // treeScripts
            // 
            this.treeScripts.AccessibleRole = System.Windows.Forms.AccessibleRole.Outline;
            this.treeScripts.AllowDrop = true;
            this.treeScripts.BackColor = System.Drawing.SystemColors.Window;
            // 
            // 
            // 
            this.treeScripts.BackgroundStyle.Class = "TreeBorderKey";
            this.treeScripts.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.treeScripts.Dock = System.Windows.Forms.DockStyle.Left;
            this.treeScripts.Location = new System.Drawing.Point(0, 0);
            this.treeScripts.Name = "treeScripts";
            this.treeScripts.Nodes.AddRange(new DevComponents.AdvTree.Node[] {
            this.node1});
            this.treeScripts.NodesConnector = this.nodeConnector1;
            this.treeScripts.NodeStyle = this.elementStyle1;
            this.treeScripts.PathSeparator = ";";
            this.treeScripts.Size = new System.Drawing.Size(229, 314);
            this.treeScripts.Styles.Add(this.elementStyle1);
            this.treeScripts.TabIndex = 0;
            this.treeScripts.Text = "advTree1";
            this.treeScripts.NodeMouseDown += new DevComponents.AdvTree.TreeNodeMouseEventHandler(this.treeScripts_NodeMouseDown);
            this.treeScripts.NodeDoubleClick += new DevComponents.AdvTree.TreeNodeMouseEventHandler(this.treeScripts_NodeDoubleClick);
            // 
            // node1
            // 
            this.node1.Expanded = true;
            this.node1.Name = "node1";
            this.node1.Text = "所有脚本";
            // 
            // nodeConnector1
            // 
            this.nodeConnector1.LineColor = System.Drawing.SystemColors.ControlText;
            // 
            // elementStyle1
            // 
            this.elementStyle1.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.elementStyle1.Name = "elementStyle1";
            this.elementStyle1.TextColor = System.Drawing.SystemColors.ControlText;
            // 
            // tbScript
            // 
            this.tbScript.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbScript.Location = new System.Drawing.Point(229, 0);
            this.tbScript.Multiline = true;
            this.tbScript.Name = "tbScript";
            this.tbScript.ReadOnly = true;
            this.tbScript.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tbScript.Size = new System.Drawing.Size(422, 314);
            this.tbScript.TabIndex = 1;
            this.tbScript.WordWrap = false;
            // 
            // FrmOpenScripts
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(651, 314);
            this.Controls.Add(this.tbScript);
            this.Controls.Add(this.treeScripts);
            this.DoubleBuffered = true;
            this.Name = "FrmOpenScripts";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "打开脚本";
            this.Load += new System.EventHandler(this.FrmOpenScripts_Load);
            ((System.ComponentModel.ISupportInitialize)(this.treeScripts)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevComponents.AdvTree.AdvTree treeScripts;
        private DevComponents.AdvTree.Node node1;
        private DevComponents.AdvTree.NodeConnector nodeConnector1;
        private DevComponents.DotNetBar.ElementStyle elementStyle1;
        private System.Windows.Forms.TextBox tbScript;
    }
}