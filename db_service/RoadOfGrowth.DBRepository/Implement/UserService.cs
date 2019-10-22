using Dapper;
using RoadOfGrowth.DBRepository.Interface;

namespace RoadOfGrowth.DBRepository.Implement
{
    public partial class UserService : IUserService
    {
        public string GetUserAccount(int id)
        {
            string sql = $"select account from sys_user where id={id}";

            return DbProvider.DbConn.QueryFirstOrDefault<string>(sql);
        }
    }
}
