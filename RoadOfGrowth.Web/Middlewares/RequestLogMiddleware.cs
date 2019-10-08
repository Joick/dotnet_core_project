using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using RoadOfGrowth.Utility;
using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace RoadOfGrowth.Web.Middlewares
{
    /// <summary>
    /// 请求记录中间件
    /// </summary>
    public class RequestLogMiddleware
    {
        readonly RequestDelegate _next;

        readonly Stopwatch _stopwatch;

        public RequestLogMiddleware(RequestDelegate next)
        {
            _next = next;
            _stopwatch = new Stopwatch();
        }

        public async Task Invoke(HttpContext context)
        {
            _stopwatch.Restart();
            HttpRequest request = context.Request;

            bool isApiRequest = request.Path.HasValue && request.Path.Value.Contains("/api/");

            if (!isApiRequest)
            {
                await _next(context);
            }
            else
            {
                LogRequest(request, out string timestamp);

                var originalBodyStream = context.Response.Body;

                using (var responseBody = new MemoryStream())
                {
                    context.Response.Body = responseBody;

                    await _next(context);

                    await LogResponseAsync(context.Response, timestamp);

                    await responseBody.CopyToAsync(originalBodyStream);
                }
            }
        }

        /// <summary>
        /// 记录请求数据包
        /// </summary>
        /// <param name="request"></param>
        /// <param name="timestamp"></param>
        private static void LogRequest(HttpRequest request, out string timestamp)
        {
            timestamp = DateTime.Now.ToString("yyMMddHHmmssfff");
            LogUtility.Info($"REQ_{timestamp}【{request.Method.ToUpper()}】:{request.Host.Value}{request.Path.Value}{request.QueryString.Value}\r\nBody:{GetRequestBody(request)}");
        }

        /// <summary>
        /// 记录响应数据
        /// </summary>
        /// <param name="response"></param>
        /// <param name="timestamp"></param>
        /// <returns></returns>
        private async Task LogResponseAsync(HttpResponse response, string timestamp)
        {
            LogUtility.Info($"RESP_{timestamp}：{ await GetResponse(response) }");
        }

        /// <summary>
        /// 获取请求body内容
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        private static string GetRequestBody(HttpRequest request)
        {
            if (request.ContentLength > 0)
            {
                request.EnableRewind();
                Stream stream = request.Body;
                byte[] buffer = new byte[request.ContentLength.Value];
                stream.Read(buffer, 0, buffer.Length);
                return Encoding.UTF8.GetString(buffer);
            }

            return null;
        }

        /// <summary>
        /// 获取响应内容
        /// </summary>
        /// <param name="response"></param>
        /// <returns></returns>
        public async Task<string> GetResponse(HttpResponse response)
        {
            response.Body.Seek(0, SeekOrigin.Begin);
            var text = await new StreamReader(response.Body).ReadToEndAsync();
            response.Body.Seek(0, SeekOrigin.Begin);
            return text;
        }
    }

    public static class RequestLogMiddlewareExtensions
    {
        public static IApplicationBuilder UseLogRequest(this IApplicationBuilder app)
        {
            return app.UseMiddleware<RequestLogMiddleware>();
        }
    }
}
