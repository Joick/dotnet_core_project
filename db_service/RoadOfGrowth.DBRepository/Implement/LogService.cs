using Dapper;
using RoadOfGrowth.DBCommon.Entities;
using RoadOfGrowth.DBRepository.Interface;

namespace RoadOfGrowth.DBRepository.Implement
{
    public partial class LogService : ILogService
    {
        public int Insert()
        {
            //string sql = $"insert into log_request(`url`,`method`,`request_params`,`request_body`,`request_time`,`response_body`,`response_time`,`processing_time`) values('baidu','GET','123','123',CURRENT_TIMESTAMP(),'123',CURRENT_TIMESTAMP(),10);select @@Identity;";
            string sql1 = "select * from log_request where id=1 ";
            var t = DbProvider.DbConn.QueryFirstOrDefault<LogRequest>(sql1);
            return 1;
            //DbProvider.DbConn.Execute(sql);
        }
    }
}
