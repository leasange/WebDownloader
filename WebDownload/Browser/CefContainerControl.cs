using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WebDownloader.Browser
{
    public partial class CefContainerControl : UserControl
    {
        public CefContainerControl()
        {
            InitializeComponent();
        }
        protected override bool IsInputKey(Keys keyData)
        {
            //This code block is only called/required when CEF is running in the
            //same message loop as the WinForms UI (CefSettings.MultiThreadedMessageLoop = false)
            //Without this code, arrows and tab won't be processed
            switch (keyData)
            {
                case Keys.Right:
                case Keys.Left:
                case Keys.Up:
                case Keys.Down:
                case Keys.Tab:
                    {
                        return true;
                    }
                case Keys.Shift | Keys.Tab:
                case Keys.Shift | Keys.Right:
                case Keys.Shift | Keys.Left:
                case Keys.Shift | Keys.Up:
                case Keys.Shift | Keys.Down:
                    {
                        return true;
                    }
            }

            return base.IsInputKey(keyData);
        }
    }
}
