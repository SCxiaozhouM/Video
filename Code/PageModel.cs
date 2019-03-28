using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Code
{
    public class PageModel
    {
        /// <summary>
        /// 当前页数
        /// </summary>
        public int PageIndex { get; set; } = 1;

        /// <summary>
        /// 一页多少条
        /// </summary>
        public int PgaeSize { get; set; } = 30;

        /// <summary>
        /// 总页数
        /// </summary>
        public int PageCount
        {
            get
            {
                return (int)Math.Ceiling(this.Sum / 30.0);
            }
            set { this.PageCount = value; }
        }

        /// <summary>
        /// 总数据
        /// </summary>
        public int Sum { get; set; }

        /// <summary>
        /// 显示页数
        /// </summary>
        public int PageShow { get; set; } = 9;
        /// <summary>
        /// 最小页数
        /// </summary>
        public int MinPage
        {
            get
            {
                if (PageIndex >= PageCount)
                {
                    return PageCount-9;
                }
                return PageShow - PageIndex < 3 ? PageIndex - 5 : 1;
            }
            set { MinPage = value; }
        }
        /// <summary>
        /// 最大页数
        /// </summary>
        public int MaxPage
        {
            get
            {
                if(PageIndex>=PageCount)
                {
                    return PageCount;
                }
                int max= PageShow - PageIndex < 3 ? PageIndex + 3 : PageShow;
                if(max> PageCount)
                {
                    return PageCount;
                }
                return max;
            }
            set { MaxPage = value; }
        }
        /// <summary>
        /// 分类
        /// 
        /// </summary>
        public string Category { get; set; }

    }
}