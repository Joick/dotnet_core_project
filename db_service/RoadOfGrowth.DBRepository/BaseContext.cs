using RoadOfGrowth.DBUtility.Providers;

/// <summary>
/// 命名空间一定要加Implement 与Implement文件夹中的实现类相同
/// </summary>
namespace RoadOfGrowth.DBRepository.Implement
{
    /// <summary>
    /// 服务基类
    /// </summary>
    public abstract class BaseContext
    {
        BaseDbProvider dbProvider;
        readonly string databaseName;

        protected BaseContext(string dbName)
        {
            databaseName = dbName;
        }

        public BaseDbProvider DbProvider => dbProvider ?? (dbProvider = DBFactory.InitProvider(databaseName));
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
