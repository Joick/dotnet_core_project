using RoadOfGrowth.DBCommon.Entities;

namespace RoadOfGrowth.DBRepository.Interface
{
    /// <summary>
    /// 请求记录服务
    /// </summary>
    public partial interface ILogRequestService : IDependency
    {
        /// <summary>
        /// 新增请求记录
        /// </summary>
        /// <param name="uri">请求完整地址</param>
        /// <param name="methods">请求动作</param>
        /// <param name="content">请求报文</param>
        /// <returns></returns>
        int Insert(string uri, string methods, string content);

        /// <summary>
        /// 更新请求记录
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        int Update(LogRequest entity);
    }
}
