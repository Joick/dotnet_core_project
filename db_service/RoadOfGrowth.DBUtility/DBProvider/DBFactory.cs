using System;
using System.Data;

namespace RoadOfGrowth.DBUtility
{
    /// <summary>
    /// 数据连接工厂
    /// </summary>
    public static class DBFactory
    {
        /// <summary>
        /// 初始化数据库连接实例
        /// </summary>
        /// <returns></returns>
        public static IDbConnection InitConnection(string dbName)
        {
            IDbConnection connection;
            var dbTypeName = ConfigUtility.GetSectionValueDeep("ConnectionConfigs", dbName, "dbType");

            //获取配置进行转换
            var dbType = GetDataBaseType(dbTypeName);

            var strConn = ConfigUtility.GetSectionValueDeep("ConnectionConfigs", dbName, "ConnectionString");

            switch (dbType)
            {
                case DatabaseType.MySql:
                    connection = new MySql.Data.MySqlClient.MySqlConnection(strConn);
                    break;
                //case DatabaseType.SqlServer:
                //    //connection = new System.Data.SqlClient.SqlConnection(strConn);
                //    break;
                //case DatabaseType.Npgsql:
                //    //connection = new Npgsql.NpgsqlConnection(strConn);
                //    break;
                //case DatabaseType.Sqlite:
                //    //connection = new SQLiteConnection(strConn);
                //    break;
                //case DatabaseType.Oracle:
                //    //connection = new Oracle.ManagedDataAccess.Client.OracleConnection(strConn);
                //    //connection = new System.Data.OracleClient.OracleConnection(strConn);
                //    break;
                default:
                    connection = null;
                    break;
                    //case DatabaseType.DB2:
                    //    //connection = new System.Data.OleDb.OleDbConnection(strConn);
                    //    break;
            }

            return connection;
        }

        /// <summary>
        /// 转换数据库类型
        /// </summary>
        /// <param name="databaseType">数据库类型</param>
        /// <returns></returns>
        private static DatabaseType GetDataBaseType(string databaseType)
        {
            // 设置默认数据库为mysql
            DatabaseType returnValue = DatabaseType.MySql;
            foreach (DatabaseType dbType in Enum.GetValues(typeof(DatabaseType)))
            {
                if (dbType.ToString().Equals(databaseType, StringComparison.OrdinalIgnoreCase))
                {
                    returnValue = dbType;
                    break;
                }
            }

            return returnValue;
        }

    }

    /// <summary>
    /// 数据库类型定义
    /// </summary>
    public enum DatabaseType
    {
        SqlServer,
        MySql,
        Oracle,
        Sqlite,
        Npgsql
    }
}
