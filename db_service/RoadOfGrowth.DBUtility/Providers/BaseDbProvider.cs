using RoadOfGrowth.DBUtility.Providers.DataObject;
using System.Data;

namespace RoadOfGrowth.DBUtility.Providers
{
    /// <summary>
    /// 数据服务提供抽象类
    /// </summary>
    public abstract partial class BaseDbProvider
    {
        readonly string databaseName;
        IDbConnection dbConn;

        protected BaseDbProvider(string dbName)
        {
            databaseName = dbName;
        }

        public IDbConnection DbConn => dbConn ?? (dbConn = DBFactory.InitConnection(databaseName));

        /// <summary>
        /// 添加单项数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data">新增数据</param>
        /// <param name="isReturnId">是否返回新增数据id</param>
        /// <returns>新增id</returns>
        public abstract int Insert<T>(T data);

        /// <summary>
        /// 添加单项数据
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public abstract int Insert(string sql, object param = null);

        /// <summary>
        /// 更新单项数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data">更新数据集</param>
        /// <param name="isReturnRowCount">是否返回受影响行数</param>
        /// <returns>受影响行数</returns>
        public abstract int Update<T>(T data);

        /// <summary>
        /// 删除单项数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <returns></returns>
        public abstract int Delete<T>(T data);

        /// <summary>
        /// 删除单项数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public abstract int Delete(int id);

        /// <summary>
        /// 查询分页数据
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public abstract QueryPageModel QueryPage(string sql);
    }


}
