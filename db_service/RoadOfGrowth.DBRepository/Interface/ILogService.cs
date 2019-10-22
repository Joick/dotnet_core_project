using System;
using System.Collections.Generic;
using System.Text;

namespace RoadOfGrowth.DBRepository.Interface
{
    public partial interface ILogService : IDependency
    {
        int Insert();
    }
}
