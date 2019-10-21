
using RoadOfGrowth.Repository.DataAccess;

namespace RoadOfGrowth.Repository
{
    public class TestRepository : ITestRepository
    {
        public string DoTestRepository()
        {
            return User.GetUser();
        }
    }
}
