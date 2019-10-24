using RoadOfGrowth.DBRepository.Interface;
using System;

namespace RoadOfGrowth.DBRepository.Implement
{
    /// <summary>
    /// 错误记录
    /// </summary>
    public partial class LogExceptionService : ILogExceptionService
    {
        /// <summary>
        /// 添加记录
        /// </summary>
        /// <param name="message"></param>
        /// <param name="ex"></param>
        /// <returns></returns>
        public int Insert(string message, Exception ex)
        {
            string sql = "insert into log_exception(`server_error_code`,`remark`,`description`,`stack_trace`) values(@code,@remark,@desc,@stack);";

            return DbProvider.Insert(sql,
                new
                {
                    code = ex.Data["Server Error Code"],
                    remark = message,
                    desc = ex.Message,
                    stack = ex.StackTrace
                });
        }
    }
}
