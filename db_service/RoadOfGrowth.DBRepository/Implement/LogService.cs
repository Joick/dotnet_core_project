using Dapper;
using RoadOfGrowth.DBRepository.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace RoadOfGrowth.DBRepository.Implement
{
    public partial class LogService : ILogService
    {
        public int Insert()
        {
            string sql = $"insert into log_request(`url`,`method`,`request_params`,`request_body`,`request_time`,`response_body`,`response_time`,`processing_time`) values('baidu','GET','123','123',CURRENT_TIMESTAMP(),'123',CURRENT_TIMESTAMP(),10);select @@Identity;";
            return DbProvider.DbConn.QueryFirstOrDefault<int>(sql);
            //DbProvider.DbConn.Execute(sql);
        }
    }
}
