using System;
using System.Collections.Generic;
using System.Text;

namespace RoadOfGrowth.DBRepository.Interface
{
    /// <summary>
    /// 错误记录服务
    /// </summary>
    public partial interface ILogExceptionService : IDependency
    {
        /// <summary>
        /// 新增错误记录
        /// </summary>
        /// <param name="message"></param>
        /// <param name="ex"></param>
        /// <returns></returns>
        int Insert(string message, Exception ex);
    }
}
