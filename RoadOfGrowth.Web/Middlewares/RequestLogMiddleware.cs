using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.WebUtilities;
using RoadOfGrowth.Utility;
using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RoadOfGrowth.Web.Middlewares
{
    /// <summary>
    /// 请求记录中间件
    /// </summary>
    public class RequestLogMiddleware
    {
        private static RequestDelegate _next;

        private Stopwatch _stopwatch;

        public RequestLogMiddleware(RequestDelegate next)
        {
            _next = next;
            _stopwatch = new Stopwatch();
        }

        public async Task Invoke(HttpContext context)
        {
            _stopwatch.Restart();
            HttpRequest request = context.Request;

            LogRequest(request, out string timestamp);


            var originalBodyStream = context.Response.Body;

            using (var responseBody = new MemoryStream())
            {
                context.Response.Body = responseBody;

                await _next(context);

                LogResponseAsync(context.Response, timestamp);

                await responseBody.CopyToAsync(originalBodyStream);
            }
        }

        private static void LogRequest(HttpRequest request, out string timestamp)
        {
            timestamp = DateTime.Now.ToString("yyMMddHHmmssfff");
            LogUtility.Info($"REQ_{timestamp}【{request.Method.ToUpper()}】:{request.Host.Host}{request.Path.Value}\r\nBody:{GetRequestBody(request)}");
        }

        /// <summary>
        /// 记录响应数据
        /// </summary>
        /// <param name="response"></param>
        /// <param name="timestamp"></param>
        /// <returns></returns>
        private async Task LogResponseAsync(HttpResponse response, string timestamp)
        {
            LogUtility.Info($"Response_{timestamp}：{ await GetResponse(response) }");
        }

        /// <summary>
        /// 获取请求body内容
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        private static string GetRequestBody(HttpRequest request)
        {
            request.EnableRewind();
            Stream stream = request.Body;
            byte[] buffer = new byte[request.ContentLength.Value];
            stream.Read(buffer, 0, buffer.Length);
            return Encoding.UTF8.GetString(buffer);
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

        /// <summary>
        /// 获取请求数据
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        private async Task<string> ReadBodyAsync(HttpRequest request)
        {
            if (request.ContentLength > 0)
            {
                await EnabledRewindAsync(request);

                var encoding = GetRequestEncoding(request);
                return await ReadStreamAsync(request.Body, encoding).ConfigureAwait(false);
            }

            return null;
        }

        /// <summary>
        /// 启用重新读取数据流
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        private async Task EnabledRewindAsync(HttpRequest request)
        {
            if (!request.Body.CanSeek)
            {
                request.EnableBuffering();

                await request.Body.DrainAsync(CancellationToken.None);
                request.Body.Seek(0, SeekOrigin.Begin);
            }
        }

        /// <summary>
        /// 获取请求数据编码类型
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        private Encoding GetRequestEncoding(HttpRequest request)
        {
            var contentType = request.ContentType;
            var mediaType = contentType == null ? default(MediaType) : new MediaType(contentType);
            var encoding = mediaType.Encoding;
            return encoding ?? Encoding.UTF8;
        }

        /// <summary>
        /// 异步读取数据流
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        private async Task<string> ReadStreamAsync(Stream stream, Encoding encoding)
        {
            using (StreamReader reader = new StreamReader(stream, encoding, true, 1024, true))
            {
                var result = await reader.ReadToEndAsync();
                stream.Seek(0, SeekOrigin.Begin);
                return result;
            }
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
