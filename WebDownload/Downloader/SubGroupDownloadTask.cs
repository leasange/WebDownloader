using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebDownloader.Downloader
{
    public class SubGroupDownloadTask : DownloadTask
    {
        /// <summary>
        /// 父级任务
        /// </summary>
        private DownloadTask parTask;
        /// <summary>
        /// 源码正则匹配
        /// </summary>
        public ContentBatch sourceBatch;
        public UrlBatch urlBatch;
        /// <summary>
        /// 生成的Tasks列表
        /// </summary>
        public List<DownloadTask> tasks;
        public SubGroupDownloadTask(DownloadTask parTask)
        {
            this.parTask = parTask;
        }
    }
    public class ContentBatch
    {
        public string content;
        public string regex;
        public ContentBatch(string content, string regex)
        {
            this.content = content;
        }    
    }
    public class UrlBatch
    {
        /// <summary>
        /// 通配Url地址(*)
        /// </summary>
        public string batchUrl;
        /// <summary>
        /// 数目
        /// </summary>
        public int batchCount;
        /// <summary>
        /// 起始编号
        /// </summary>
        public int startIndex;

        public string content;
        public string regexCount;

        public List<DownloadTask> DownloadTasks
        {
            get
            {
                return null;
            }
        }
        public UrlBatch()
        {
            this.batchCount = 0;
            this.startIndex = 1;
        }
        public UrlBatch(string batchUrl, int startIndex, string content = null, string regexCount = null)
        {
            this.batchUrl = batchUrl;
            this.content = content;
            this.regexCount = regexCount;

            this.batchCount = 0;
            this.startIndex = 1;
        }
    }
}
