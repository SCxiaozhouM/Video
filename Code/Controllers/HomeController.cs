using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Code.Models;
using Code.EntityFramework;
using Code.EntityFramework.Entity;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;

namespace Code.Controllers
{
    public class Mo
    {
        public List<mac_vod> list { get; set; }
        public List<mac_vod> listp { get; set; }
    }
    public class HomeController : Controller
    {
        public IActionResult Index()
        {

            //var dsjList = SugarBase.DB.Queryable<mac_vod>().ToPageList(1, 12);

            //return View();
            var dsjList = SugarBase.DB.Queryable<mac_vod>().Where(o => o.Type_Id_1 == 2).ToPageList(1, 12);
            var dianyingList = SugarBase.DB.Queryable<mac_vod>().Where(o => o.Type_Id_1 == 1).ToPageList(1, 12);
            var dongman = SugarBase.DB.Queryable<mac_vod>().Where(o => o.Type_Id == 4).ToPageList(1, 12);
            var zongyiList = SugarBase.DB.Queryable<mac_vod>().Where(o => o.Type_Id == 3).ToPageList(1, 12);
            ViewBag.BannerList = SugarBase.DB.Queryable<banner>().ToList();
            ViewBag.NewList = SugarBase.DB.Queryable<mac_vod>().OrderBy(o => o.Vod_Time_Add, SqlSugar.OrderByType.Desc).ToPageList(1, 10).ToList();
            //评分
            var dsjListp = SugarBase.DB.Queryable<mac_vod>().Where(o => o.Type_Id_1 == 2).OrderBy(o => o.Vod_SCore, SqlSugar.OrderByType.Desc).ToPageList(1, 12);
            var dianyingListp = SugarBase.DB.Queryable<mac_vod>().Where(o => o.Type_Id_1 == 1).OrderBy(o => o.Vod_SCore, SqlSugar.OrderByType.Desc).ToPageList(1, 12);
            var dongmanp = SugarBase.DB.Queryable<mac_vod>().Where(o => o.Type_Id == 4).OrderBy(o => o.Vod_SCore, SqlSugar.OrderByType.Desc).ToPageList(1, 12);
            var zongyiListp = SugarBase.DB.Queryable<mac_vod>().Where(o => o.Type_Id == 3).OrderBy(o => o.Vod_SCore, SqlSugar.OrderByType.Desc).ToPageList(1, 12);
            Dictionary<string, Mo> model = new Dictionary<string, Mo>();
            model.Add("电影", new Mo { list = dianyingList, listp = dianyingListp });
            model.Add("电视剧", new Mo { list = dsjList, listp = dsjListp });
            model.Add("动漫", new Mo { list = dongman, listp = dongmanp });
            model.Add("综艺", new Mo { list = zongyiList, listp = zongyiListp });
            return View(model);
            // 类型 分类 影名
        }

        public IActionResult Detail(string movieId, string page)
        {
            movieId = movieId.ToLower().Replace(".html", "");
            mac_vod moiveModel = null;
            ViewBag.Page = page;
           
            if (page == "play")
            {

                var pageinfo = movieId.Split('-');
                var iD = Convert.ToInt32(pageinfo[0]);
                moiveModel = SugarBase.DB.Queryable<mac_vod>().Single(o => o.Vod_Id == iD);
            }
            else
            {
                moiveModel = SugarBase.DB.Queryable<mac_vod>().Single(o => o.Vod_Id == Convert.ToInt32(movieId));
                moiveModel.Vod_Time_Add = GetTime(moiveModel.Vod_Time_Add).ToShortDateString();
            }
            //播放地址解析
            var urls = moiveModel.Vod_Play_Url.Split(moiveModel.Vod_Play_Note);
            List<Dictionary<string, string>> list = new List<Dictionary<string, string>>();
            foreach (var url in urls)
            {
                Dictionary<string, string> dic = new Dictionary<string, string>();
                var paths = url.Split("#");
                foreach (var path in paths)
                {
                    var mo = path.Split("$");
                    dic.Add(mo[0], mo[1]);
                }
                list.Add(dic);
            }
            ViewBag.Path = list;

            return View(moiveModel);
        }
        /// <summary>
        /// 时间戳转为C#格式时间
        /// </summary>
        /// <param name="timeStamp">Unix时间戳格式</param>
        /// <returns>C#格式时间</returns>
        public static DateTime GetTime(string timeStamp)
        {
            DateTime dtStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));

            long lTime = long.Parse(timeStamp + "0000000");
            TimeSpan toNow = new TimeSpan(lTime);
            return dtStart.Add(toNow);
        }

    }
}
