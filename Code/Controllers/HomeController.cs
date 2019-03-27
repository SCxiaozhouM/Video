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
        public IEnumerable<mac_vod> list { get; set; }
        public IEnumerable<mac_vod> listp { get; set; }
    }
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            //各类型推荐
            var dataLists = SugarBase.DB.SqlQueryable<mac_vod>(@"select vod_id, type_id,type_id_1,vod_name,vod_pic,vod_actor,vod_director,vod_remarks,vod_score from mac_vod 
                where type_id = 3  LIMIT 12
                union all
                select vod_id, type_id,type_id_1,vod_name,vod_pic,vod_actor,vod_director,vod_remarks,vod_score from mac_vod 
                where type_id = 4  LIMIT 12
                union all
                select vod_id, type_id,type_id_1,vod_name,vod_pic,vod_actor,vod_director,vod_remarks,vod_score  from mac_vod
                where type_id_1 = 1  LIMIT 12
                union all
                select vod_id, type_id,type_id_1,vod_name,vod_pic,vod_actor,vod_director,vod_remarks,vod_score  from mac_vod 
                where  type_id_1 = 2 LIMIT 48").ToList();
            SetUrl(dataLists);
            //return View();
            var dsjList = dataLists.Where(o => o.Type_Id_1 == 2);
            var dianyingList = dataLists.Where(o => o.Type_Id_1 == 1);
            var dongman = dataLists.Where(o => o.Type_Id == 4);
            var zongyiList = dataLists.Where(o => o.Type_Id == 3);
            ViewBag.BannerList = SugarBase.DB.Queryable<banner>().ToList();
            //最近更新
            dataLists = SugarBase.DB.Queryable<mac_vod>().OrderBy(o => o.Vod_Time_Add, SqlSugar.OrderByType.Desc).ToPageList(1, 10).ToList();
            SetUrl(dataLists);
            ViewBag.NewList = dataLists;
            //排行榜
            dataLists = SugarBase.DB.SqlQueryable<mac_vod>(@"select vod_id, type_id,type_id_1,vod_name,vod_pic,vod_actor,vod_director,vod_remarks,vod_score from mac_vod 
                where type_id = 3  LIMIT 12
                union all
                select vod_id, type_id,type_id_1,vod_name,vod_pic,vod_actor,vod_director,vod_remarks,vod_score from mac_vod 
                where type_id = 4  LIMIT 12
                union all
                select vod_id, type_id,type_id_1,vod_name,vod_pic,vod_actor,vod_director,vod_remarks,vod_score  from mac_vod
                where type_id_1 = 1  LIMIT 12
                union all
                select vod_id, type_id,type_id_1,vod_name,vod_pic,vod_actor,vod_director,vod_remarks,vod_score  from mac_vod 
                where  type_id_1 = 2  LIMIT 48").ToList();
            SetUrl(dataLists);
            //评分
            var dsjListp = dataLists.Where(o => o.Type_Id_1 == 2);
            var dianyingListp = dataLists.Where(o => o.Type_Id_1 == 1);
            var dongmanp = dataLists.Where(o => o.Type_Id == 4);
            var zongyiListp = dataLists.Where(o => o.Type_Id == 3);
            Dictionary<string, Mo> model = new Dictionary<string, Mo>();
            model.Add("电影", new Mo { list = dianyingList, listp = dianyingListp });
            model.Add("电视剧", new Mo { list = dsjList, listp = dsjListp });
            model.Add("动漫", new Mo { list = dongman, listp = dongmanp });
            model.Add("综艺", new Mo { list = zongyiList, listp = zongyiListp });
            return View(model);
            // 类型 分类 影名
        }
        //select vod_id, type_id,type_id_1,vod_name,vod_pic,vod_actor,vod_director,vod_remarks  from  mac_vod where type_id_1 in (1,2) or type_id in (3,4) 
        public IActionResult Detail(string movieId, string page)
        {

            movieId = movieId.ToLower().Replace(".html", "");
            mac_vod moiveModel = null;
            ViewBag.Page = page;
            var pageinfo = movieId.Split('-');
            if (page == "play")
            {
                var iD = Convert.ToInt32(pageinfo[0]);
                moiveModel = SugarBase.DB.Queryable<mac_vod>().Single(o => o.Vod_Id == iD);
            }
            else
            {
                moiveModel = SugarBase.DB.Queryable<mac_vod>().Single(o => o.Vod_Id == Convert.ToInt32(movieId));
                moiveModel.Vod_Time_Add = GetTime(moiveModel.Vod_Time_Add).ToShortDateString();
            }
            //播放地址解析
            var urls = moiveModel.Vod_Play_Url.Split("$$$");
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
            Random ran = new Random();
            var num = ran.Next(1, 200);
            //推荐
            var dataList = SugarBase.DB.Queryable<mac_vod>().Where(o => o.Type_Id == moiveModel.Type_Id).OrderBy(o => o.Vod_SCore, SqlSugar.OrderByType.Desc).Skip(num).Take(14).ToList();
             SetUrl(dataList);
            ViewBag.TJ = dataList;
            //播放地址获
            if (page == "play")
            {
                int sum = 0;
                var data = list[Convert.ToInt32(pageinfo[1])];
                foreach (var item in data)
                {
                    if (sum == Convert.ToInt32(pageinfo[2]))
                    {
                        ViewBag.vType = pageinfo[1];
                        ViewBag.url = item.Value;
                    }
                    sum++;
                }
            }
            return View(moiveModel);
        }
        [Route("/Home/Search")]
        public IActionResult Search(string query)
        {
            var dataList = SugarBase.DB.SqlQueryable<mac_vod>("select vod_id, type_id,type_id_1,vod_name from mac_vod  where Vod_Name like '%" + query + "%' or Vod_En like '%" + query + "%'").ToPageList(1, 5);/*.Where(o => o.Vod_Name.Contains(query) || o.Vod_En.Contains(query)).ToPageList(1,8);*/
            SetUrl(dataList);
            return Json(new { CODE = 1, DATA = dataList });
        }
        public IActionResult Movies()
        {
            var dataLists = SugarBase.DB.Queryable<mac_vod>().OrderBy(o => o.Vod_Time_Add, SqlSugar.OrderByType.Desc).ToPageList(1, 50).ToList();
            SetUrl(dataLists);
            ViewBag.MovieList = dataLists;
             
            return View();
        }




        /// <summary>
        /// 时间戳转为C#格式时间
        /// </summary>
        /// <param name="timeStamp">Unix时间戳格式</param>
        /// <returns>C#格式时间</returns>
        private static DateTime GetTime(string timeStamp)
        {
            DateTime dtStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));

            long lTime = long.Parse(timeStamp + "0000000");
            TimeSpan toNow = new TimeSpan(lTime);
            return dtStart.Add(toNow);
        }
        private static void SetUrl(List<mac_vod> dataLists)
        {
            var types = SugarBase.DB.Queryable<mac_type>().ToList();
            foreach (var data in dataLists)
            {
                var type = types.First(o => o.Type_Id == data.Type_Id);
                string en = type.Type_En;
                if (type.type_Pid != 0)
                {
                    en = types.First(o => o.Type_Id == type.type_Pid).Type_En + "/" + en;
                }

                data.Vod_Play_Url = "/" + en + "/" + data.Vod_Id + ".html";
            }
        }
    }
}
