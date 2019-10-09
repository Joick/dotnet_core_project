using System;
using System.Collections.Generic;
using System.Text;

namespace RoadOfGrowth.IDataAccess
{
    public interface ITestDataAccess : IDataDependency
    {
        string DoTest();
    }
}
