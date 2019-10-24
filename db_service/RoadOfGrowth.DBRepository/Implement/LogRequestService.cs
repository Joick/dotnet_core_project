using Dapper;
using RoadOfGrowth.DBCommon.Entities;
using RoadOfGrowth.DBRepository.Interface;

namespace RoadOfGrowth.DBRepository.Implement
{
    /// <summary>
    /// 添加请求记录
    /// </summary>
    public partial class LogRequestService : ILogRequestService
    {
        /// <summary>
        /// 添加新的请求记录
        /// </summary>
        /// <returns></returns>
        public int Insert(string uri, string methods, string content)
        {
            string sql = "insert into log_request(`url`,`method`,`request_body`) values(@url,@method,@request_body);";
            return DbProvider.Insert(sql, new { url = uri, method = methods, request_body = content });

        }

        /// <summary>
        /// 更新同步响应数据
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int Update(LogRequest entity)
        {
            string sql = "update log_request set response_body=@content,response_time=@time,processing_time=@processTime,req_timestamp=@reqTimestamp where id=@id;";

            return DbProvider.DbConn.Execute(sql,
                new
                {
                    content = entity.ResponseBody,
                    time = entity.ResponseTime,
                    reqTimestamp = entity.RequestTimestamp,
                    processTime = entity.ProcessingTime,
                    id = entity.Id
                });
        }
    }
}
