using System;
using System.Collections.Generic;
using System.Text;

namespace RoadOfGrowth.IRepository
{
    public interface ITestRepository : IBLLDependency
    {
        string DoTestRepository();
    }
}
