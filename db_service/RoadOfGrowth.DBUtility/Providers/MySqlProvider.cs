using RoadOfGrowth.DBUtility.Providers.DataObject;
using System;

namespace RoadOfGrowth.DBUtility.Providers
{
    /// <summary>
    /// mysql service provider
    /// </summary>
    public class MySqlProvider : BaseDbProvider
    {
        public MySqlProvider(string dbName)
            : base(dbName)
        {
        }

        public override int Delete<T>(T data)
        {
            return 1;
        }

        public override int Delete(int id)
        {
            throw new NotImplementedException();
        }

        public override int Insert<T>(T data)
        {
            return 1;
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
