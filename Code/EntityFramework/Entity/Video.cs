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
        /// 电影名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 别名
        /// </summary>
        public string Bname { get; set; }

        /// <summary>
        /// 主演
        /// </summary>
        public string Star { get; set; }

        /// <summary>
        /// 导演
        /// </summary>
        public string Director { get; set; }

        /// <summary>
        /// 类型
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// 地区
        /// </summary>
        public string Reigion { get; set; }

        /// <summary>
        /// 语言
        /// </summary>
        public string Language { get; set; }

        /// <summary>
        /// 上映时间
        /// </summary>
        public string Show { get; set; }

        /// <summary>
        /// 时长
        /// </summary>
        public string Mins { get; set; }

        /// <summary>
        /// 更改时间
        /// </summary>
        public string UpdateTime { get; set; }

        /// <summary>
        /// 评分
        /// </summary>
        public string Grade { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 播放地址
        /// </summary>
        public string VideoUrl { get; set; }

        /// <summary>
        /// 图片
        /// </summary>
        public string Img { get; set; }



    }
}
