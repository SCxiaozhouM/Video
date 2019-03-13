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
        public List<movie_list> list { get; set; }
        public List<movie_list> listp { get; set; }
    }
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            var dsjList = SugarBase.DB.Queryable<movie_list>().Where(o => o.Types.Contains("剧")).ToPageList(1, 12);
            var dianyingList = SugarBase.DB.Queryable<movie_list>().Where(o => !o.Types.Contains("剧") && !o.Types.Contains("动漫") && !o.Types.Contains("综艺")).ToPageList(1, 12);
            var dongman = SugarBase.DB.Queryable<movie_list>().Where(o => o.Types.Contains("动漫")).ToPageList(1, 12);
            var zongyiList = SugarBase.DB.Queryable<movie_list>().Where(o => o.Types.Contains("综艺")).ToPageList(1, 12);
            ViewBag.BannerList = SugarBase.DB.Queryable<movie_banner>().ToList();
            ViewBag.NewList = SugarBase.DB.Queryable<movie_list>().ToPageList(1, 10).ToList();
            //评分
            var dsjListp = SugarBase.DB.Queryable<movie_list>().Where(o => o.Types.Contains("剧")).OrderBy(o => o.Grade, SqlSugar.OrderByType.Desc).ToPageList(1, 12);
            var dianyingListp = SugarBase.DB.Queryable<movie_list>().Where(o => !o.Types.Contains("剧") && !o.Types.Contains("动漫") && !o.Types.Contains("综艺")).OrderBy(o => o.Grade, SqlSugar.OrderByType.Desc).ToPageList(1, 12);
            var dongmanp = SugarBase.DB.Queryable<movie_list>().Where(o => o.Types.Contains("动漫")).OrderBy(o => o.Grade, SqlSugar.OrderByType.Desc).ToPageList(1, 12);
            var zongyiListp = SugarBase.DB.Queryable<movie_list>().Where(o => o.Types.Contains("综艺")).OrderBy(o => o.Grade, SqlSugar.OrderByType.Desc).ToPageList(1, 12);
            Dictionary<string, Mo> model = new Dictionary<string, Mo>();
            model.Add("电影", new Mo{ list = dianyingList, listp= dianyingListp });
            model.Add("电视剧", new Mo { list = dsjList, listp = dsjListp });
            model.Add("动漫", new Mo { list = dongman, listp = dongmanp });
            model.Add("综艺", new Mo { list = zongyiList, listp = zongyiListp });
            return View(model);

        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
