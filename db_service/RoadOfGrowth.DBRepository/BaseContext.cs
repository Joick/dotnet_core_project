using RoadOfGrowth.DBUtility;
using System.Data;

namespace RoadOfGrowth.DBRepository.Implement
{
    /// <summary>
    /// 服务基类
    /// </summary>
    public abstract class BaseContext
    {
        IDbConnection dbContext;
        string databaseName;

        public BaseContext(string dbName)
        {
            databaseName = dbName;
        }

        protected IDbConnection DbContext
        {
            get
            {
                if (dbContext == null)
                {
                    dbContext = DBFactory.InitConnection(databaseName);
                }

                return dbContext;
            }
        }
    }

    public partial class UserService : BaseContext
    {
        public UserService()
            : base("MainDb")
        {
        }
    }

    public partial class LogService : BaseContext
    {
        public LogService()
            : base("LogDb")
        {
        }
    }
}
