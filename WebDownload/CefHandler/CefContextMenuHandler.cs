﻿using System;
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

        private const int CopyImage = (int)CefSharp.CefMenuCommand.UserFirst + 1;//复制图片
        private const int OpenDevTool =(int)CefSharp.CefMenuCommand.UserFirst + 2;//开发工具
        public void OnBeforeContextMenu(CefSharp.IWebBrowser chromiumWebBrowser, CefSharp.IBrowser browser, CefSharp.IFrame frame, CefSharp.IContextMenuParams parameters, CefSharp.IMenuModel model)
        {
            if (BeforeContextMenu != null)
            {
                BeforeContextMenu(this, new BeforeContextMenuEvenArgs(parameters, model));
            }
            model.AddSeparator();
            if (parameters.MediaType == CefSharp.ContextMenuMediaType.Image)
            {
                model.AddItem((CefSharp.CefMenuCommand)CopyImage, "复制图片");
            }
            model.AddItem((CefSharp.CefMenuCommand)OpenDevTool, "开发着工具");
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
                case (CefSharp.CefMenuCommand)CopyImage:
                    {
                       // Clipboard.SetImage()
                    }
                    return true;
                case (CefSharp.CefMenuCommand)OpenDevTool:
                    {
                        // Clipboard.SetImage()
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