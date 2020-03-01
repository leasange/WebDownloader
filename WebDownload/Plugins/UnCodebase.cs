using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace WebDownloader.Plugins
{
    /// <summary>
    /// 验证码识别
    /// </summary>
    public class UnCodebase
    {
        public Bitmap bmpobj;
        public UnCodebase(Bitmap pic)
        {
            bmpobj = new Bitmap(pic);    //转换为Format32bppRgb
        }

        /// <summary>
        /// 根据RGB，计算灰度值
        /// </summary>
        /// <param name="posClr">Color值</param>
        /// <returns>灰度值，整型</returns>
        private int GetGrayNumColor(System.Drawing.Color posClr)
        {
            return (posClr.R * 19595 + posClr.G * 38469 + posClr.B * 7472) >> 16;
        }

        /// <summary>
        /// 灰度转换,逐点方式
        /// </summary>
        public Bitmap GrayByPixels()
        {
            for (int i = 0; i < bmpobj.Height; i++)
            {
                for (int j = 0; j < bmpobj.Width; j++)
                {
                    int tmpValue = GetGrayNumColor(bmpobj.GetPixel(j, i));
                    bmpobj.SetPixel(j, i, Color.FromArgb(tmpValue, tmpValue, tmpValue));
                }
            }
            return bmpobj;
        }

        /// <summary>
        /// 去图形边框
        /// </summary>
        /// <param name="borderWidth"></param>
        private Bitmap ClearPicBorder(int borderWidth)
        {
            for (int i = 0; i < bmpobj.Height; i++)
            {
                for (int j = 0; j < bmpobj.Width; j++)
                {
                    if (i < borderWidth || j < borderWidth || j > bmpobj.Width - 1 - borderWidth || i > bmpobj.Height - 1 - borderWidth)
                        bmpobj.SetPixel(j, i, Color.FromArgb(255, 255, 255));
                }
            }
            return bmpobj;
        }

        /// <summary>
        /// 灰度转换,逐行方式
        /// </summary>
        private Bitmap GrayByLine()
        {
            Rectangle rec = new Rectangle(0, 0, bmpobj.Width, bmpobj.Height);
            BitmapData bmpData = bmpobj.LockBits(rec, ImageLockMode.ReadWrite, bmpobj.PixelFormat);// PixelFormat.Format32bppPArgb);
            //    bmpData.PixelFormat = PixelFormat.Format24bppRgb;
            IntPtr scan0 = bmpData.Scan0;
            int len = bmpobj.Width * bmpobj.Height;
            int[] pixels = new int[len];
            Marshal.Copy(scan0, pixels, 0, len);

            //对图片进行处理
            int GrayValue = 0;
            for (int i = 0; i < len; i++)
            {
                GrayValue = GetGrayNumColor(Color.FromArgb(pixels[i]));
                pixels[i] = (byte)(Color.FromArgb(GrayValue, GrayValue, GrayValue)).ToArgb();      //Color转byte
            }

            bmpobj.UnlockBits(bmpData);
            return bmpobj;
        }

        /// <summary>
        /// 得到有效图形并调整为可平均分割的大小
        /// </summary>
        /// <param name="dgGrayValue">灰度背景分界值</param>
        /// <param name="CharsCount">有效字符数</param>
        /// <returns></returns>
        private void GetPicValidByValue(int dgGrayValue, int CharsCount)
        {
            int posx1 = bmpobj.Width; int posy1 = bmpobj.Height;
            int posx2 = 0; int posy2 = 0;
            for (int i = 0; i < bmpobj.Height; i++)      //找有效区
            {
                for (int j = 0; j < bmpobj.Width; j++)
                {
                    int pixelValue = bmpobj.GetPixel(j, i).R;
                    if (pixelValue < dgGrayValue)     //根据灰度值
                    {
                        if (posx1 > j) posx1 = j;
                        if (posy1 > i) posy1 = i;

                        if (posx2 < j) posx2 = j;
                        if (posy2 < i) posy2 = i;
                    };
                };
            };
            // 确保能整除
            int Span = CharsCount - (posx2 - posx1 + 1) % CharsCount;   //可整除的差额数
            if (Span < CharsCount)
            {
                int leftSpan = Span / 2;    //分配到左边的空列 ，如span为单数,则右边比左边大1
                if (posx1 > leftSpan)
                    posx1 = posx1 - leftSpan;
                if (posx2 + Span - leftSpan < bmpobj.Width)
                    posx2 = posx2 + Span - leftSpan;
            }
            //复制新图
            Rectangle cloneRect = new Rectangle(posx1, posy1, posx2 - posx1 + 1, posy2 - posy1 + 1);
            bmpobj = bmpobj.Clone(cloneRect, bmpobj.PixelFormat);
        }

        /// <summary>
        /// 得到有效图形,图形为类变量
        /// </summary>
        /// <param name="dgGrayValue">灰度背景分界值</param>
        /// <param name="CharsCount">有效字符数</param>
        /// <returns></returns>
        private void GetPicValidByValue(int dgGrayValue)
        {
            int posx1 = bmpobj.Width; int posy1 = bmpobj.Height;
            int posx2 = 0; int posy2 = 0;
            for (int i = 0; i < bmpobj.Height; i++)      //找有效区
            {
                for (int j = 0; j < bmpobj.Width; j++)
                {
                    int pixelValue = bmpobj.GetPixel(j, i).R;
                    if (pixelValue < dgGrayValue)     //根据灰度值
                    {
                        if (posx1 > j) posx1 = j;
                        if (posy1 > i) posy1 = i;

                        if (posx2 < j) posx2 = j;
                        if (posy2 < i) posy2 = i;
                    };
                };
            };
            //复制新图
            Rectangle cloneRect = new Rectangle(posx1, posy1, posx2 - posx1 + 1, posy2 - posy1 + 1);
            bmpobj = bmpobj.Clone(cloneRect, bmpobj.PixelFormat);
        }

        /// <summary>
        /// 得到有效图形,图形由外面传入
        /// </summary>
        /// <param name="dgGrayValue">灰度背景分界值</param>
        /// <param name="CharsCount">有效字符数</param>
        /// <returns></returns>
        private Bitmap GetPicValidByValue(Bitmap singlepic, int dgGrayValue)
        {
            int posx1 = singlepic.Width; int posy1 = singlepic.Height;
            int posx2 = 0; int posy2 = 0;
            for (int i = 0; i < singlepic.Height; i++)      //找有效区
            {
                for (int j = 0; j < singlepic.Width; j++)
                {
                    int pixelValue = singlepic.GetPixel(j, i).R;
                    if (pixelValue < dgGrayValue)     //根据灰度值
                    {
                        if (posx1 > j) posx1 = j;
                        if (posy1 > i) posy1 = i;

                        if (posx2 < j) posx2 = j;
                        if (posy2 < i) posy2 = i;
                    };
                };
            };
            //复制新图
            Rectangle cloneRect = new Rectangle(posx1, posy1, posx2 - posx1 + 1, posy2 - posy1 + 1);
            return singlepic.Clone(cloneRect, singlepic.PixelFormat);
        }

        /// <summary>
        /// 去掉噪点
        /// </summary>
        /// <param name="dgGrayValue"></param>
        /// <param name="MaxNearPoints"></param>
        public Bitmap ClearNoise()
        {

            Color piexl;
            int dgGrayValue = 190;
            int nearDots = 0;
            //逐点判断
            for (int i = 0; i < bmpobj.Width; i++)
                for (int j = 0; j < bmpobj.Height; j++)
                {
                    piexl = bmpobj.GetPixel(i, j);
                    if (piexl.R < dgGrayValue)
                    {
                        bool isOk = false;
                        nearDots = 0;
                        if (i == 0 || i == bmpobj.Width - 1 || j == 0 || j == bmpobj.Height - 1)  //边框全去掉
                        {
                            bmpobj.SetPixel(i, j, Color.FromArgb(255, 255, 255));
                        }
                        else
                        {
                            bool isV = isValidPoint(dgGrayValue, 8, i, j, "y");

                            bool isH = isValidPoint(dgGrayValue, 8, i, j, "x");

                            if (isV && isH)
                                isOk = true;
                            else if (isV)
                            {
                                isH = isValidPoint(dgGrayValue, 5, i, j, "x");

                                if (isH)
                                    isOk = true;
                            }
                            else if (isH)
                            {
                                isV = isValidPoint(dgGrayValue, 5, i, j, "y");

                                if (isV)
                                    isOk = true;
                            }
                        }

                        if (!isOk)
                            bmpobj.SetPixel(i, j, Color.FromArgb(255, 255, 255));   //去掉单点 && 粗细小3邻边点
                        else
                            bmpobj.SetPixel(i, j, Color.FromArgb(0, 0, 0));
                    }
                    else  //背景
                        bmpobj.SetPixel(i, j, Color.FromArgb(255, 255, 255));
                }
            return bmpobj;

        }

        private bool isValidPoint(int dgGrayValue, int maxNearPoints, int x, int y, string type)
        {
            int p = y;
            int w = bmpobj.Height;
            if (type == "x")
            {
                p = x;
                w = bmpobj.Width;
            }
            int nearDots = 0;
            for (int k = maxNearPoints * -1 + 1; k < 1; k++)
            {
                if (p + k < 0 || p + k + maxNearPoints > w)
                    continue;

                for (int h = k; h < k + maxNearPoints; h++)
                {
                    int R = 0;
                    if (type == "x")
                        R = bmpobj.GetPixel(x + h, y).R;
                    else
                        R = bmpobj.GetPixel(x, y + h).R;

                    if (R < dgGrayValue)
                        nearDots++;
                }

                if (nearDots < maxNearPoints)
                    nearDots = 0;
                else
                    break;
            }

            return nearDots >= maxNearPoints;
        }

        /// <summary>
        /// 扭曲图片校正
        /// </summary>
        private Bitmap ReSetBitMap()
        {
            Graphics g = Graphics.FromImage(bmpobj);
            Matrix X = new Matrix();
            //  X.Rotate(30);
            X.Shear((float)0.16666666667, 0);   //  2/12
            g.Transform = X;
            Rectangle cloneRect = new Rectangle(0, 0, bmpobj.Width, bmpobj.Height);
            Bitmap tmpBmp = bmpobj.Clone(cloneRect, bmpobj.PixelFormat);
            g.DrawImage(tmpBmp,
                new Rectangle(0, 0, bmpobj.Width, bmpobj.Height),
                 0, 0, tmpBmp.Width,
                 tmpBmp.Height,
                 GraphicsUnit.Pixel);

            return tmpBmp;
        }

        /// <summary>
        /// 生成缩略图重载方法1，返回缩略图的Image对象
        /// </summary>
        /// <param name="Width">缩略图的宽度</param>
        /// <param name="Height">缩略图的高度</param>
        /// <returns>缩略图的Image对象</returns>
        private Image GetReducedImage(Image orgImg, int height)
        {
            try
            {
                int width = (int)(height * orgImg.Width / (double)orgImg.Height);
                //用指定的大小和格式初始化Bitmap类的新实例
                Bitmap bitmap = new Bitmap(width, height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
                //从指定的Image对象创建新Graphics对象
                Graphics graphics = Graphics.FromImage(bitmap);
                //清除整个绘图面并以透明背景色填充
                graphics.Clear(Color.Transparent);
                //在指定位置并且按指定大小绘制原图片对象
                graphics.DrawImage(orgImg, new Rectangle(0, 0, width, height));
                return bitmap;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public static Bitmap PreProcess(Bitmap pic)
        {
            UnCodebase ud = new UnCodebase(pic);
            ud.GrayByPixels();
            ud.ClearNoise();
            Bitmap bitmap = ud.ReSetBitMap();
            bitmap = (Bitmap)ud.GetReducedImage(bitmap, 30);
            return bitmap;
        }
    }
}
