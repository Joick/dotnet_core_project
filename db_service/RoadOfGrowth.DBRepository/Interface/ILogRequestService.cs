namespace RoadOfGrowth.DBRepository.Interface
{
    /// <summary>
    /// 请求记录服务
    /// </summary>
    public partial interface ILogRequestService : IDependency
    {
        int Insert();
    }
}
