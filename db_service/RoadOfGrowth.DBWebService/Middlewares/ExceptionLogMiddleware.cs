using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using RoadOfGrowth.DBRepository.Interface;
using RoadOfGrowth.DBUtility;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace RoadOfGrowth.DBWebService.Middlewares
{
    /// <summary>
    /// 全局错误拦截
    /// </summary>
    public class ExceptionLogMiddleware
    {
        readonly RequestDelegate _next;
        readonly ILogExceptionService _logExSev;

        public ExceptionLogMiddleware(RequestDelegate next, ILogExceptionService logExSev)
        {
            _next = next;
            _logExSev = logExSev;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleException(context, ex);
            }
        }

        /// <summary>
        /// 记录日志,重定向
        /// </summary>
        /// <param name="context"></param>
        /// <param name="ex"></param>
        private async Task HandleException(HttpContext context, Exception ex)
        {
            string message = $"请求接口:{context.Request.Scheme}://{context.Request.Host}{(context.Request.Path.HasValue ? context.Request.Path.Value : "")}\n请求报文:{GetRequestBody(context.Request)}";

            _logExSev.Insert(message, ex);

            await RabbitMQUtility.PushLog(new { msg = message, err = ex });

            throw ex;
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
    }
    public static class ExceptionHandlerMiddlewareExtensions
    {
        public static IApplicationBuilder UseExceptionLog(this IApplicationBuilder app)
        {
            return app.UseMiddleware<ExceptionLogMiddleware>();
        }
    }
}
