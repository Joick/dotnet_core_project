using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using RoadOfGrowth.ExternalService;
using RoadOfGrowth.Utility;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace RoadOfGrowth.Web.Middlewares
{
    public class ExceptionLogMiddleware
    {
        readonly RequestDelegate _next;

        public ExceptionLogMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                HandleException(context, ex);
            }
        }

        /// <summary>
        /// 记录日志,重定向
        /// </summary>
        /// <param name="context"></param>
        /// <param name="ex"></param>
        private async void HandleException(HttpContext context, Exception ex)
        {
            string message = $"请求接口:{context.Request.Scheme}://{context.Request.Host}{(context.Request.Path.HasValue ? context.Request.Path.Value : "")}\n请求报文:{GetRequestBody(context.Request)}\n报错:";

            await RabbitMQUtility.PushLog(new { msg = message, err = ex });

            context.Response.Redirect($"{context.Request.Scheme}://{context.Request.Host}/common/error");
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
