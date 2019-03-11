using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace reptile
{
    public class SimpleCrawler
    {
        public event EventHandler<OnStartEventArgs> Onstart;//爬虫启动事件
        public event EventHandler<OnCompletedEventArgs> OnCompleted;//爬虫完成事件
        public event EventHandler<Exception> OnError;//爬虫错误时间
        public CookieContainer CookieContainer { get; set; }//定义cookie容器
        public SimpleCrawler() { }

        public async Task<string> Start(Uri uri,WebProxy proxy=null)
        {
            return await Task.Run(() =>
            {
                var pageSource = string.Empty;
                try
                {
                    if (this.Onstart != null) this.Onstart(this, new OnStartEventArgs(uri));
                    var watch = new Stopwatch();
                    watch.Start();
                    var request = (HttpWebRequest)WebRequest.Create(uri);
                    request.Accept = "*/*";
                    request.ContentType = "application/x-www-form-urlencoded";//定义文档类型及编码
                    request.AllowAutoRedirect = false;//禁止自动跳转
                    //设置User-Agent 伪装成Google Chrome浏览器
                    request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/72.0.3626.121 Safari/537.36";
                    request.Timeout = 5000;//定义请求超时时间为5秒
                    request.Method = "GET";//定义请求方式为GET
                    if (proxy != null) request.Proxy = proxy;//设置代理服务器IP 伪装成请求地址
                    request.CookieContainer = this.CookieContainer;//附加cookie容器
                    request.ServicePoint.ConnectionLimit = int.MaxValue;//定义最大整数
                    var response = (HttpWebResponse)request.GetResponse();//获取请求响应
                    foreach (Cookie cookie in response.Cookies)
                    {
                        this.CookieContainer.Add(cookie);//将cookie加入容器  保存登录状态
                    }
                    var stream = response.GetResponseStream();//获取响应流
                    var reader = new StreamReader(stream, Encoding.UTF8);//以UTF8的方式读取流
                    pageSource = reader.ReadToEnd();//获取网页源代码
                    watch.Stop();
                    var threadId = System.Threading.Thread.CurrentThread.ManagedThreadId;//获取当前任务线程ID
                    var milliseconds = watch.ElapsedMilliseconds;//获取请求执行时间
                    reader.Close();//释放资源
                    stream.Close();
                    request.Abort();
                    response.Close();
                    if (this.OnCompleted != null) this.OnCompleted(this, new OnCompletedEventArgs(uri, threadId, milliseconds, pageSource));
                }
                catch(Exception ex)
                {
                    if (this.OnError != null) this.OnError(this, ex);
                }
                return pageSource;
            });
        }
    }
}
