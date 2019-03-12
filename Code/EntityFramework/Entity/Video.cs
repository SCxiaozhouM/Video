using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Code.EntityFramework.Entity
{
    public class Video
    {
        public int Id { get; set; }
        /// <summary>
        /// 电影名+画质
        /// </summary>
        public string Name { get; set; }
        ///// <summary>
        ///// 采集网站地址
        ///// </summary>
        //public string Link_name { get; set; }
        /// <summary>
        /// 电影类型
        /// </summary>
        public string Cate { get; set; }
        /// <summary>
        /// 地区
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public string Status { get; set; }
        /// <summary>
        /// 更新时间
        /// </summary>
        public string Update_time { get; set; }
        //public string Phone { get; set; }
        /// <summary>
        /// 图片地址
        /// </summary>
        public string Pic_address { get; set; }
        /// <summary>
        /// 影片名称（需）
        /// </summary>
        public string Move_name { get; set; }
        /// <summary>
        /// 影片别名
        /// </summary>
        public string Move_nickname { get; set; }
        /// <summary>
        /// 影片备注（画质）
        /// </summary>
        public string Move_mark { get; set; }
        /// <summary>
        /// 主演
        /// </summary>
        public string Move_star { get; set; }
        /// <summary>
        /// 导演
        /// </summary>
        public string Move_director { get; set; }
        ///// <summary>
        ///// 分类
        ///// </summary>
        //public string Column { get; set; }
        //public string Move_cate { get; set; }
        /// <summary>
        /// 语言
        /// </summary>
        public string Language { get; set; }
        //public string Move_region { get; set; }
        /// <summary>
        /// 上映年份
        /// </summary>
        public string Up_Time { get; set; }
        /// <summary>
        /// 播放地址
        /// </summary>
        public string Paly_address { get; set; }
        //电影简介
        public string Move_desc { get; set; }

    }
}
