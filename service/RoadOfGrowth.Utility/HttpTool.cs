using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace RoadOfGrowth.Utility
{
    /// <summary>
    /// http请求帮助类
    /// </summary>
    public static class HttpTool
    {
        private static readonly HttpClient instance = new HttpClient(new HttpClientHandler() { UseCookies = false });

        /// <summary>
        /// 发起get请求
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="uri"></param>
        /// <param name="conditions"></param>
        /// <returns></returns>
        public static string Get(this string uri, object conditions = null)
        {
            var request = GetAsync(uri, conditions);

            request.Wait();

            return request.Result;
        }

        /// <summary>
        /// 发起get请求
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="uri"></param>
        /// <param name="conditions"></param>
        /// <returns></returns>
        public static T Get<T>(this string uri, object conditions = null)
        {
            var request = GetAsync<T>(uri, conditions);

            request.Wait();

            return request.Result;
        }

        /// <summary>
        /// 发起get请求(异步)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="uri"></param>
        /// <param name="conditions"></param>
        /// <returns></returns>
        public static async Task<string> GetAsync(this string uri, object conditions = null)
        {
            uri.AssemblyUri(conditions);

            using (var response = await instance.GetAsync(uri))
            {
                return ConvertResponse(response);
            }

        }

        /// <summary>
        /// 发起get请求(异步)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="uri"></param>
        /// <param name="conditions"></param>
        /// <returns></returns>
        public static async Task<T> GetAsync<T>(this string uri, object conditions = null)
        {
            uri.AssemblyUri(conditions);

            using (var response = await instance.GetAsync(uri))
            {
                return ConvertResponse<T>(response);
            }
        }

        /// <summary>
        /// 发送post请求
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="uri"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static T Post<T>(this string uri, object data)
        {
            var request = PostAsync<T>(uri, data);

            request.Wait();

            return request.Result;
        }

        /// <summary>
        /// 发送post请求
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="uri"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string Post(this string uri, object data)
        {
            var request = PostAsync(uri, data);

            request.Wait();

            return request.Result;
        }

        /// <summary>
        /// 发送post请求(异步)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="uri"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static async Task<T> PostAsync<T>(this string uri, object data)
        {
            HttpContent content = new StringContent(SerializeObject(data));

            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            using (var response = await instance.PostAsync(uri, content))
            {
                return ConvertResponse<T>(response); 
            }
        }

        /// <summary>
        /// 发送post请求(异步)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="uri"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static async Task<string> PostAsync(this string uri, object data)
        {
            HttpContent content = new StringContent(SerializeObject(data));

            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            using (var response = await instance.PostAsync(uri, content))
            {
                return ConvertResponse(response); 
            }
        }

        /// <summary>
        /// 发送HttpPut请求
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="uri"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static T Put<T>(this string uri, object data)
        {
            var request = PutAsync<T>(uri, data);

            request.Wait();

            return request.Result;
        }

        /// <summary>
        /// 发送HttpPut请求
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="uri"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string Put(this string uri, object data)
        {
            var request = PutAsync(uri, data);

            request.Wait();

            return request.Result;
        }

        /// <summary>
        /// 发送HttpPut请求(异步)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="uri"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static async Task<T> PutAsync<T>(this string uri, object data)
        {
            HttpContent content = new StringContent(SerializeObject(data));

            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            using (var request = await instance.PutAsync(uri, content))
            {
                return ConvertResponse<T>(request); 
            }
        }

        /// <summary>
        /// 发送HttpPut请求(异步)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="uri"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static async Task<string> PutAsync(this string uri, object data)
        {
            HttpContent content = new StringContent(SerializeObject(data));

            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            using (var response = await instance.PutAsync(uri, content))
            {
                return ConvertResponse(response); 
            }
        }

        /// <summary>
        /// 发送HttpDelete请求
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="uri"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static T Delete<T>(this string uri, object conditions = null)
        {
            var request = DeleteAsync<T>(uri, conditions);

            request.Wait();

            return request.Result;
        }

        /// <summary>
        /// 发送HttpDelete请求
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="uri"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string Delete(this string uri, object conditions = null)
        {
            var request = DeleteAsync(uri, conditions);

            request.Wait();

            return request.Result;
        }

        /// <summary>
        /// 发送HttpDelete请求(异步)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="uri"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static async Task<T> DeleteAsync<T>(this string uri, object conditions = null)
        {
            uri.AssemblyUri(conditions);

            using (var response = await instance.DeleteAsync(uri))
            {
                return ConvertResponse<T>(response); 
            }
        }

        /// <summary>
        /// 发送HttpDelete请求(异步)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="uri"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static async Task<string> DeleteAsync(this string uri, object conditions = null)
        {
            uri.AssemblyUri(conditions);

            using (var response = await instance.DeleteAsync(uri))
            {
                return ConvertResponse(response); 
            }
        }

        /// <summary>
        /// 组装uri请求
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="conditions"></param>
        private static void AssemblyUri(this string uri, object conditions = null)
        {
            if (conditions != null)
            {
                StringBuilder urlParams = new StringBuilder();
                urlParams.Append("?");

                Dictionary<string, object> _condition = (Dictionary<string, object>)conditions;

                int index = 0;
                int count = _condition.Count;
                foreach (var item in _condition)
                {
                    urlParams.Append($"{item.Key}={HttpUtility.UrlEncode(SerializeObject(item.Value))}");
                    index++;

                    if (index < count)
                    {
                        urlParams.Append("&");
                    }
                }

                uri += urlParams.ToString();
            }
        }

        /// <summary>
        /// 转换响应数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="response"></param>
        /// <returns></returns>
        private static T ConvertResponse<T>(HttpResponseMessage response)
        {
            var readTask = response.Content.ReadAsStringAsync();
            readTask.Wait();

            return DeserializeObject<T>(readTask.Result);
        }

        /// <summary>
        /// 获取string格式响应数据
        /// </summary>
        /// <param name="response"></param>
        /// <returns></returns>
        private static string ConvertResponse(HttpResponseMessage response)
        {
            var readTask = response.Content.ReadAsStringAsync();
            readTask.Wait();

            return readTask.Result;
        }

        /// <summary>
        /// 序列化对象成字符串
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private static string SerializeObject(this object data)
        {
            return JsonConvert.SerializeObject(data);
        }

        /// <summary>
        /// 反序列化数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <returns></returns>
        private static T DeserializeObject<T>(this string data)
        {
            return JsonConvert.DeserializeObject<T>(data);
        }
    }
}
