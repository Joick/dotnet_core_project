namespace RoadOfGrowth.DBRepository.Interface
{
    /// <summary>
    /// 用户
    /// </summary>
    public interface IUserService : IDependency
    {
        string GetUserAccount(int id);
    }
}
