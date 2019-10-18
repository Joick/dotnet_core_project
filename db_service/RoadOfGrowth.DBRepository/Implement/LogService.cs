using Dapper;
using RoadOfGrowth.DBRepository.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace RoadOfGrowth.DBRepository.Implement
{
    public partial class LogService : ILogService
    {
        public void Insert()
        {
            string sql = $"insert into keyname(`name`) values('{Guid.NewGuid().ToString()}')";

            DbContext.Execute(sql);
        }
    }
}
