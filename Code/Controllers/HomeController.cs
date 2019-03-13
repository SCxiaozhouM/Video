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
using Dapper;
using MySql.Data.MySqlClient;

namespace Code.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            using (var conn = new MySqlConnection(AppConfig.SqlConn))
            {
                //banner
                var sql_banner = @"select * from movie_banner";
                //最近更新
                var sql_new = "select * from movie_list order by updatetime desc limit 0,10";
                var sql_dianying = "select * from movie_list where type not  like '%剧%' and type not like '%综艺%' and type not like '%动漫%' and type not like '%伦理%' order by updatetime desc limit 0,12";
                var sql_zongyi = "";
                var sql_dongman = "select * from movie_list where type like '%动漫%' order by updatetime desc limit 0,10";
                var sql_sdj = "select * from movie_list where type like '%剧%' order by updatetime desc limit 0,12";
                var newList = conn.Query<Video>(sql_new);
                var bannerList = conn.Query<Banner>(sql_banner);
                var zongyiList = conn.Query<Video>("select * from movie_list where type like '%综艺%' order by updatetime desc limit 0,12");
                var dongman = conn.Query<Video>(sql_dongman);
                var dsjList = conn.Query<Video>(sql_sdj).ToList();
                var dianyingList = conn.Query<Video>(sql_dianying);
               
                ViewBag.BannerList = bannerList;
                ViewBag.NewList = newList;
                //ViewBag.DianyingList = dianyingList;
                //ViewBag.ZongyiList = zongyiList;
                //ViewBag.DongmanList = dongman;
                //ViewBag.DsjList = dsjList;
                Dictionary<string, IEnumerable<Video>> model = new Dictionary<string, IEnumerable<Video>>();
                model.Add("电影", dianyingList);
                model.Add("电视剧", dsjList);
                model.Add("动漫", dongman);
                model.Add("综艺", zongyiList);
                return View(model);
            }
               
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
