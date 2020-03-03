using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WebDownloader.Plugins;

namespace WebDownloader.Browser
{
    public class JsCallObject
    {
        public static string[] base64ImageStart = new string[]{
            "data:image/gif;base64,",
            "data:image/png;base64,",
            "data:image/bmp;base64,",
            "data:image/jpg;base64,",
            "data:image/jpeg;base64,",
            "data:image/x-icon;base64,"
        };
        public string GetValidateCode(string base64Image)
        {
            try
            {
                foreach (var item in base64ImageStart)
                {
                    base64Image =  base64Image.Replace(item, "");
                }
                //过滤特殊字符即可    
                string dummyData = base64Image.Replace("%0A","").Replace("%0D","");
                byte[] arr = Convert.FromBase64String(dummyData);
                MemoryStream ms = new MemoryStream(arr);
                Bitmap bmp = new Bitmap(ms);
                ms.Close();
                /*Form frm1 = new Form();
                frm1.BackgroundImageLayout = ImageLayout.Zoom;
                frm1.BackgroundImage = bmp;
                frm1.Show();
                UnCodebase codebase = new UnCodebase(bmp);
                Bitmap bitmap = codebase.GrayByPixels(); //灰度处理
                codebase.ClearNoise();//128, 2
                Form frm2 = new Form();
                frm2.BackgroundImageLayout = ImageLayout.Zoom;
                frm2.BackgroundImage = bitmap;
                frm2.Show();*/
                tessnet2.Tesseract ocr = new tessnet2.Tesseract();//声明一个OCR类
                ocr.SetVariable("tessedit_char_whitelist", "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz");
                ocr.Init(Application.StartupPath+"\\tessdata", "eng", false);
                List<tessnet2.Word> result = ocr.DoOCR(bmp, Rectangle.Empty);//执行识别操作
                bmp.Dispose();
                string res = result[0].Text;
                return res;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
