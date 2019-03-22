﻿using Microsoft.AspNetCore.Mvc.Internal;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Code
{
    public class LegacyRoute : IRouter
    {
        private readonly string[] _urls = new string[] { "dianying", "dongman", "dsj", "zongyi", "play" };
        private readonly IRouter _mvcRoute;
        public LegacyRoute(IServiceProvider services, params string[] urls)
        {
            _mvcRoute = services.GetRequiredService<MvcRouteHandler>();
        }

        public VirtualPathData GetVirtualPath(VirtualPathContext context)
        {
            //var path = string.Empty;
            //var hasController = context.Values.TryGetValue("controller", out var controller);
            //var hasAction = context.Values.TryGetValue("action", out var action);
            //var hasCategory = context.Values.TryGetValue("dy", out var category);
            //if (hasController && hasAction && hasCategory)
            //{
            //    path = @"/{category /{title}.html";
            //}
            return null;
            //return path != string.Empty ? new VirtualPathData(this, path) : null;
        }

        public async Task RouteAsync(RouteContext context)
        {
            //获取请求的地址
            var requestedUrl = context.HttpContext.Request.Path.Value.TrimStart('/').ToLower();
            var split = requestedUrl.Split('/', StringSplitOptions.RemoveEmptyEntries);
            var type = _urls.Where(o => requestedUrl.Contains(o)).FirstOrDefault();
            //判断在不在指定请求内
            if (type != null)
            {
                //根据分段判断页面
                if (split.Length == 3)
                {
                    context.RouteData.Values["controller"] = "Home";
                    context.RouteData.Values["action"] = "Privacy";
                    //context.RouteData.Values["type"] = type;
                    //context.RouteData.Values["category"] = split[1];
                    context.RouteData.Values["movieId"] = split[2];
                }
            }
            //if(secoend)
            //最后注入`MvcRouteHandler`示例执行`RouteAsync`方法，表示匹配成功
            await context.HttpContext.RequestServices.GetService<MvcRouteHandler>().RouteAsync(context);
        }
    }
}