using RoadOfGrowth.Utility;
using System;
using System.Collections.Generic;
using System.Text;

namespace RoadOfGrowth.Repository.DataAccess
{
    public static class User
    {
        public static string GetUser()
        {
            return HttpTool.Get(string.Format(DbUri.GetUser, 1));
        }
    }
}
