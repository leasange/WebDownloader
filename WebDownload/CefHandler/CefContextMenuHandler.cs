using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WebDownloader.CefHandler
{
    public class CefContextMenuHandler : CefSharp.IContextMenuHandler
    {
        public event EventHandler<BeforeContextMenuEvenArgs> BeforeContextMenu;
        public event EventHandler ViewSource;
        public event EventHandler ShowDevTool;
        public event EventHandler LoadScript;
        public event EventHandler<CopyImageEventArgs> CopyImageToClipboard;
        public event EventHandler<OpenLinkOrSourceArgs> OpenLinkOrSource;

        private const int CopyImage = (int)CefSharp.CefMenuCommand.UserFirst + 1;//复制图片
        private const int OpenLink = (int)CefSharp.CefMenuCommand.UserFirst + 2;//打开链接
        private const int OpenLinkSource = (int)CefSharp.CefMenuCommand.UserFirst + 3;//打开链接源码
        private const int OpenDevTool =(int)CefSharp.CefMenuCommand.UserFirst + 4 ;//开发工具
        private const int CopyLink = (int)CefSharp.CefMenuCommand.UserFirst + 5;//复制链接
        private const int OpenLoadScript = (int)CefSharp.CefMenuCommand.UserFirst + 6;//加载脚本

        public void OnBeforeContextMenu(CefSharp.IWebBrowser chromiumWebBrowser, CefSharp.IBrowser browser, CefSharp.IFrame frame, CefSharp.IContextMenuParams parameters, CefSharp.IMenuModel model)
        {
            if (BeforeContextMenu != null)
            {
                BeforeContextMenu(this, new BeforeContextMenuEvenArgs(parameters, model));
            }
            model.AddSeparator();
            if (parameters.MediaType == CefSharp.ContextMenuMediaType.Image)
            {
                model.AddItem((CefSharp.CefMenuCommand)CopyImage, "复制图片(&I)");
            }
            if ((CefSharp.ContextMenuType)(parameters.TypeFlags & CefSharp.ContextMenuType.Link) == CefSharp.ContextMenuType.Link)
            {
                if (!string.IsNullOrWhiteSpace(parameters.LinkUrl)&&!parameters.LinkUrl.StartsWith("about:black"))
                {
                    model.AddItem((CefSharp.CefMenuCommand)OpenLink, "打开链接(&L)");

                    model.AddItem((CefSharp.CefMenuCommand)OpenLinkSource, "打开链接源码(&S)");

                    model.AddItem((CefSharp.CefMenuCommand)CopyLink, "复制链接(&C)");
                }
            }
            model.AddItem((CefSharp.CefMenuCommand)OpenDevTool, "开发者工具 F12");
            model.AddItem((CefSharp.CefMenuCommand)OpenLoadScript, "加载脚本 F6");
        }

        public bool OnContextMenuCommand(CefSharp.IWebBrowser chromiumWebBrowser, CefSharp.IBrowser browser, CefSharp.IFrame frame, CefSharp.IContextMenuParams parameters, CefSharp.CefMenuCommand commandId, CefSharp.CefEventFlags eventFlags)
        {
            switch (commandId)
            {
                case CefSharp.CefMenuCommand.ViewSource:
                    if (ViewSource != null)
                    {
                        ViewSource(this, new EventArgs());
                    }
                    return true;
                case (CefSharp.CefMenuCommand)OpenLink:
                case (CefSharp.CefMenuCommand)OpenLinkSource:
                    {
                        if (OpenLinkOrSource != null)
                        {
                            OpenLinkOrSource(this, new OpenLinkOrSourceArgs(parameters.LinkUrl, commandId == (CefSharp.CefMenuCommand)OpenLinkSource));
                        }
                    }
                    return true;
                case (CefSharp.CefMenuCommand)CopyImage:
                    {
                        if (CopyImageToClipboard != null)
                        {
                            CopyImageToClipboard(this, new CopyImageEventArgs(parameters.SourceUrl));
                        }
                    }
                    return true;
                case (CefSharp.CefMenuCommand)OpenDevTool:
                    {
                        if (ShowDevTool != null)
                        {
                            ShowDevTool(this, new EventArgs());
                        }
                    }
                    return true;
                case (CefSharp.CefMenuCommand)CopyLink:
                    {
                        Clipboard.SetText(parameters.LinkUrl);
                    }
                    return true;
                case (CefSharp.CefMenuCommand)OpenLoadScript:
                    {
                        if (LoadScript!=null)
                        {
                            LoadScript(this, new EventArgs());
                        }
                    }
                    return true;
                default:
                    break;
            }
            return false;
        }

        public void OnContextMenuDismissed(CefSharp.IWebBrowser chromiumWebBrowser, CefSharp.IBrowser browser, CefSharp.IFrame frame)
        {
        }

        public bool RunContextMenu(CefSharp.IWebBrowser chromiumWebBrowser, CefSharp.IBrowser browser, CefSharp.IFrame frame, CefSharp.IContextMenuParams parameters, CefSharp.IMenuModel model, CefSharp.IRunContextMenuCallback callback)
        {
            return false;
        }
    }
}
