﻿using System;
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
        private string SqlConn = "server=www.tchfans.com;User Id=root;password=root;Database=movide;";
        public IActionResult Index()
        {
            using (var conn = new MySqlConnection(SqlConn))
            {
                var sql_banner = @"select * from banner";
                var bannerList = conn.Query<Banner>(sql_banner);
                ViewBag.BannerList = bannerList;
                return View(bannerList);
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
