using RoadOfGrowth.IDataAccess;
using RoadOfGrowth.IRepository;

namespace RoadOfGrowth.Repository
{
    public class TestRepository : ITestRepository
    {
        private static ITestDataAccess _testDto;
        public TestRepository(ITestDataAccess testDto)
        {
            _testDto = testDto;
        }

        public string DoTestRepository()
        {
            return _testDto.DoTest();
        }
    }
}
