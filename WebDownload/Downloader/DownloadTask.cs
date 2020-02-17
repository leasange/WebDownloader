using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebDownloader.Downloader
{
    /// <summary>
    /// 下载任务
    /// </summary>
    public class DownloadTask
    {   
        /// <summary>
        /// 是否是文件夹
        /// </summary>
        public bool isFolder=false;
        /// <summary>
        /// 名称
        /// </summary>
        public string name;
        /// <summary>
        /// 网页地址、图片、视频等地址
        /// </summary>
        public string url;
        /// <summary>
        /// 是否是页面
        /// </summary>
        public bool isPage=true;
        /// <summary>
        /// 是否只打开源
        /// </summary>
        public bool isOnlyOpenSource=true;
        /// <summary>
        /// 是否保存到本地
        /// </summary>
        public bool isSave=false;
        /// <summary>
        /// 0 初始状态，1 运行状态，2 已结束
        /// </summary>
        public int state=0;
        /// <summary>
        /// 子任务
        /// </summary>
        public List<SubGroupDownloadTask> subGroupTasks;
    }
}
