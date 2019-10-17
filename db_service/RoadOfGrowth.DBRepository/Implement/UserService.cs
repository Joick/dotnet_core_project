using Dapper;
using RoadOfGrowth.DBUtility;
using RoadOfGrowth.DBRepository.Interface;

namespace RoadOfGrowth.DBRepository.Implement
{
    public class UserService : IUserService
    {
        public string GetUserAccount(int id)
        {
            string sql = $"select account from sys_user where id={id}";
            using (var conn = DBContext.GetInstance())
            {
                return conn.QueryFirstOrDefault<string>(sql);
            }
        }
    }
}
