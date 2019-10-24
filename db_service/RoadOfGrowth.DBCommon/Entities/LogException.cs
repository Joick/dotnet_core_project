using RoadOfGrowth.DBUtility.Providers.EntityExtension;
using System;

namespace RoadOfGrowth.DBCommon.Entities
{
    /// <summary>
    /// 错误记录
    /// </summary>
    public class LogException : BaseEntity
    {
        /// <summary>
        /// 服务错误码
        /// </summary>
        [Column(Name = "server_error_code")]
        public string ServerErrorCode { get; set; }

        /// <summary>
        /// 自定义错误描述
        /// </summary>
        [Column(Name = "remark")]
        public string Remark { get; set; }

        /// <summary>
        /// 错误描述
        /// </summary>
        [Column(Name = "description")]
        public string Description { get; set; }

        /// <summary>
        /// 堆栈跟踪信息
        /// </summary>
        [Column(Name = "stack_trace")]
        public string StackTrace { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [Column(Name = "create_time")]
        public DateTime CreateTime { get; set; }

    }
}
