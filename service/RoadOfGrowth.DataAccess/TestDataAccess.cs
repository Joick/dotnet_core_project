using RoadOfGrowth.IDataAccess;
using System;

namespace RoadOfGrowth.DataAccess
{
    public class TestDataAccess : ITestDataAccess
    {
        public string DoTest()
        {
            return "success";
        }
    }
}
