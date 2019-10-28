using System;
using System.Runtime.Serialization;

namespace RoadOfGrowth.Web.Models
{
    /// <summary>
    /// 同步响应数据模型
    /// </summary>
    [DataContract, Serializable]
    public class ServiceResponseProperty
    {
        /// <summary>
        /// 响应状态码
        /// </summary>
        [DataMember(Name = "code")]
        public int Code { get; set; }

        /// <summary>
        /// 响应状态描述
        /// </summary>
        [DataMember(Name = "msg")]
        public string Message { get; set; }

        /// <summary>
        /// 响应数据
        /// </summary>
        [DataMember(Name = "data")]
        public object Data { get; set; }
    }

    /// <summary>
    /// 同步响应数据模型
    /// </summary>
    [DataContract, Serializable]
    public class ServiceReponse : ServiceResponseProperty
    {
        /// <summary>
        /// 处理成功
        /// </summary>
        /// <param name="message"></param>
        public void Success(string message = null)
        {
            this.Message = message ?? "处理成功";
        }

        /// <summary>
        /// 处理成功
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        public void Success<T>(T data)
        {
            this.Data = data;
        }

        /// <summary>
        /// 处理成功
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="message"></param>
        /// <param name="data"></param>
        public void Success<T>(string message, T data)
        {
            this.Message = message;
            this.Data = data;
        }

        /// <summary>
        /// 处理失败
        /// </summary>
        /// <param name="message"></param>
        public void Fail(string message = null)
        {
            this.Message = message ?? "处理出错";
        }
    }


    /// <summary>
    /// (展示用)同步响应数据模型
    /// </summary>
    [DataContract, Serializable]
    public class ServiceResponseProperty<T> : ServiceReponse
    {
        /// <summary>
        /// 响应数据
        /// </summary>
        [DataMember(Name = "data")]
        public new T Data { get; set; }
    }
}
