using System;
using System.Collections.Generic;
using System.Text;

namespace reptile
{
    /// <summary>
    /// 爬虫启动事件
    /// </summary>
    public class OnStartEventArgs
    {
        /// <summary>
        /// 爬虫url地址
        /// </summary>
        public Uri Uri { get; set; }
        public OnStartEventArgs(Uri uri) { this.Uri = uri; }
    }
}
