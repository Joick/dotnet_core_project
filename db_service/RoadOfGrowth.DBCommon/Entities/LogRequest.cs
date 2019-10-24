using RoadOfGrowth.DBUtility.Providers.EntityExtension;
using System;

namespace RoadOfGrowth.DBCommon.Entities
{
    /// <summary>
    /// 请求记录实体映射类
    /// </summary>
    public class LogRequest : BaseEntity
    {
        /// <summary>
        /// 请求url
        /// </summary>
        [Column(Name = "url")]
        public string Url { get; set; }

        /// <summary>
        /// 请求方法
        /// </summary>
        [Column(Name = "methods")]
        public string Method { get; set; }

        /// <summary>
        /// 请求参数
        /// </summary>
        [Column(Name = "request_body")]
        public string RequestBody { get; set; }

        /// <summary>
        /// 请求时间
        /// </summary>
        [Column(Name = "request_time")]
        public DateTime RequestTime { get; set; }

        /// <summary>
        /// 同步相应数据
        /// </summary>
        [Column(Name = "response_body")]
        public string ResponseBody { get; set; }

        /// <summary>
        /// 同步响应时间
        /// </summary>
        [Column(Name = "response_time")]
        public DateTime? ResponseTime { get; set; }

        /// <summary>
        /// 总处理时间(单位:秒)
        /// </summary>
        [Column(Name = "processing_time")]
        public int ProcessingTime => ResponseTime.HasValue ? (ResponseTime.Value - RequestTime).Seconds : 0;
    }
}
