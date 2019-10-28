using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using RoadOfGrowth.DBCommon.Entities;
using RoadOfGrowth.DBRepository.Interface;
using RoadOfGrowth.DBUtility;
using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace RoadOfGrowth.DBWebService.Middlewares
{
    /// <summary>
    /// 请求记录中间件
    /// </summary>
    public class RequestLogMiddleware
    {
        readonly RequestDelegate _next;
        readonly Stopwatch _stopwatch;
        readonly ILogRequestService _logReqSev;

        public RequestLogMiddleware(RequestDelegate next, ILogRequestService logReqSev)
        {
            _next = next;
            _stopwatch = new Stopwatch();
            _logReqSev = logReqSev;
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
                int id = _logReqSev.Insert($"{ request.Host.Value}{ request.Path.Value}{ request.QueryString.Value}", request.Method.ToUpper(), GetRequestBody(request));

                string timestamp = await LogRequest(request);

                var originalBodyStream = context.Response.Body;

                using (var responseBody = new MemoryStream())
                {
                    context.Response.Body = responseBody;

                    await _next(context);

                    await LogResponseAsync(context.Response, timestamp, id);

                    await responseBody.CopyToAsync(originalBodyStream);
                }
            }
        }

        /// <summary>
        /// 记录请求数据包
        /// </summary>
        /// <param name="request"></param>
        /// <param name="timestamp"></param>
        private static async Task<string> LogRequest(HttpRequest request)
        {
            string timestamp = DateTime.Now.ToString("yyMMddHHmmssfff");

            await RabbitMQUtility.PushLog(new { msg = $"REQ_{timestamp}【{request.Method.ToUpper()}】:{request.Host.Value}{request.Path.Value}{request.QueryString.Value}\r\nBody:{GetRequestBody(request)}" });

            return timestamp;
        }

        /// <summary>
        /// 记录响应数据
        /// </summary>
        /// <param name="response"></param>
        /// <param name="timestamp"></param>
        /// <returns></returns>
        private async Task LogResponseAsync(HttpResponse response, string timestamp, int id)
        {
            string content = await GetResponse(response);
            string message = $"RESP_{timestamp}：{content}";

            _logReqSev.Update(new LogRequest { Id = id, ResponseBody = content, ResponseTime = DateTime.Now, RequestTimestamp = timestamp });

            await RabbitMQUtility.PushLog(new { msg = message });
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
        public static async Task<string> GetResponse(HttpResponse response)
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