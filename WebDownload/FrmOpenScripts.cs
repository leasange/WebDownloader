using DevComponents.AdvTree;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using WebDownloader.Browser;

namespace WebDownloader
{
    public partial class FrmOpenScripts : DevComponents.DotNetBar.Office2007Form
    {
        private CefWebBrowserX cefBrowser;
        public FrmOpenScripts(CefWebBrowserX cefBrowser)
        {
            InitializeComponent();
            this.cefBrowser = cefBrowser;
        }

        private void FrmOpenScripts_Load(object sender, EventArgs e)
        {
            string[] files = Directory.GetFiles(Path.Combine(Application.StartupPath, "InjectScripts"));
            foreach (var file in files)
            {
                if (file.EndsWith(".xml"))
                {
                    XmlDocument doc = new XmlDocument();
                    doc.Load(file);
                    XmlNodeList batches = doc.SelectNodes("root/batch");
                    foreach (XmlNode node in batches)
                    {
                        string scriptFile = node.Attributes["script"].Value;
                        string batchName = node.Attributes["name"].Value;
                        Node batchNode = new Node(batchName);

                        StreamReader sr = new StreamReader(File.OpenRead(Application.StartupPath + "\\InjectScripts\\BatchScripts\\" + scriptFile));
                        string scriptBase = sr.ReadToEnd();
                        XmlNodeList items = node.SelectNodes("item");
                        foreach (XmlNode item in items)
                        {
                            string thScript = scriptBase;
                            string name = item.Attributes["name"].Value;
                            //string browser = item.Attributes["browser"].Value;//browser,tab
                            XmlNodeList prms = item.SelectNodes("param");
                            foreach (XmlNode prm in prms)
                            {
                                string id = prm.Attributes["id"].Value;
                                string val = prm.InnerText;
                                thScript = thScript.Replace("##{" + id + "}##", val);
                            }
                            Node itemNode = new Node(name);
                            itemNode.Tag = thScript;
                            batchNode.Nodes.Add(itemNode);
                            //tabBrowsers.OpenUrl("http://kzgm.bbshjz.cn:8000/ncms/mask/book-view", thScript, true);
                        }

                        treeScripts.Nodes[0].Nodes.Add(batchNode);
                    }
                }
                else
                {

                }
            }
            treeScripts.Nodes[0].ExpandAll();
        }

        private void treeScripts_NodeMouseDown(object sender, TreeNodeMouseEventArgs e)
        {
            if (e.Node.Tag==null)
            {
                tbScript.Text = "";
            }
            else
            {
                tbScript.Text = (string)e.Node.Tag;
            }
        }

        private void treeScripts_NodeDoubleClick(object sender, TreeNodeMouseEventArgs e)
        {
            if (e.Node.Tag!=null)
            {
                cefBrowser.InjectScript = (string)e.Node.Tag;
                cefBrowser.ExcuteScript();
                this.Close();
            }
        }
    }
}
