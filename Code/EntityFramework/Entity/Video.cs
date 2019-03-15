using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Code.EntityFramework.Entity
{
    public class mac_vod
    {
        /// <summary>
        /// ID编号
        /// </summary>
        public int Vod_Id { get; set; }
        /// <summary>
        /// 二级类型
        /// </summary>
        public int Type_Id { get; set; }

        /// <summary>
        /// 一级类型
        /// </summary>
        public int Type_Id_1 { get; set; }

        /// <summary>
        /// 电影名
        /// </summary>
        public string Vod_Name { get; set; }

        /// <summary>
        /// 电影名拼
        /// </summary>
        public string Vod_En { get; set; }

        /// <summary>
        /// 电影首字母
        /// </summary>
        public string Vod_Letter { get; set; }

        /// <summary>
        /// 类型
        /// </summary>
        public string Vod_Class { get; set; }

        /// <summary>
        /// 图片地址
        /// </summary>
        public string Vod_Pic { get; set; }

        /// <summary>
        /// 演员名
        /// </summary>
        public string Vod_Actor { get; set; }

        /// <summary>
        /// 导演
        /// </summary>
        public string Vod_Director { get; set; }

        /// <summary>
        /// 简介部分
        /// </summary>
        public string Vod_Blurb { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Vod_Remarks { get; set; }
        /// <summary>
        /// 地区
        /// </summary>
        public string Vod_Area { get; set; }

        /// <summary>
        /// 语言
        /// </summary>
        public string Vod_lang { get; set; }

        /// <summary>
        /// 上映年限
        /// </summary>
        public string Vod_Year { get; set; }

        /// <summary>
        /// 描述简介
        /// </summary>
        public string Vod_Content { get; set; }

        /// <summary>
        /// 播放类型
        /// </summary>
        public string Vod_Play_From { get; set; }

        /// <summary>
        /// 分割符号
        /// </summary>
        public string Vod_Play_Note { get; set; }

        /// <summary>
        /// 播放路径
        /// </summary>
        public string Vod_Play_Url { get; set; }

        public string Vod_Time_Add { get; set; }

        /// <summary>
        /// 分数
        /// </summary>
        public string Vod_SCore { get; set; }
    }
}
