using System;
using System.Collections.Generic;
using System.Text;

namespace reptile
{
    /// <summary>
    /// 爬虫完成事件
    /// </summary>
    public class OnCompletedEventArgs
    {
        /// <summary>
        /// 爬虫URL地址
        /// </summary>
        public Uri Uri { get; set; }
        /// <summary>
        /// //任务线程ID
        /// </summary>
        public int TheadId { get; set; }
        /// <summary>
        /// //页面源代码
        /// </summary>
        public string PageSource { get; set; }
        /// <summary>
        /// //爬虫请求执行事件
        /// </summary>
        public long Milliseconds { get; set; }
        public OnCompletedEventArgs(Uri uri,int theadId,long milliseconds,string pageSource)
        {
            this.Uri = uri;
            this.TheadId = theadId;
            this.Milliseconds = milliseconds;
            this.PageSource = pageSource;
        }


    }
}
