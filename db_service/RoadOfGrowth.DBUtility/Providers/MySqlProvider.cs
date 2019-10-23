using Dapper;
using RoadOfGrowth.DBUtility.Providers.DataObject;
using System;

namespace RoadOfGrowth.DBUtility.Providers
{
    /// <summary>
    /// mysql service provider
    /// </summary>
    public class MySqlProvider : BaseDbProvider
    {
        private const string queryIdentitySql = "select @@identity";

        public MySqlProvider(string dbName)
            : base(dbName)
        {
        }

        public override int Delete<T>(T data)
        {
            return 0;
        }

        public override int Delete(int id)
        {
            throw new NotImplementedException();
        }

        public override int Insert<T>(T data)
        {
            DbConn.Open();
            var trans = DbConn.BeginTransaction();

            try
            {
                DbConn.Execute("");

                int id = DbConn.QueryFirstOrDefault<int>(queryIdentitySql);

                if (id > 0)
                {
                    trans.Commit();
                }
                else
                {
                    trans.Rollback();
                }

                return id;
            }
            catch (Exception ex)
            {
                trans.Rollback();
                RabbitMQUtility.PushLog(new { msg = "操作数据库Insert错误", err = ex });
                return 0;
            }
            finally
            {
                trans.Dispose();
                DbConn.Close();
            }
        }

        public override QueryPageModel QueryPage(string sql)
        {
            return new QueryPageModel();
        }

        public override int Update<T>(T data)
        {
            return 1;
        }
    }

}
