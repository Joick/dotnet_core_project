namespace RoadOfGrowth.DBUtility.Providers.EntityExtension
{
    /// <summary>
    /// 基类
    /// </summary>
    public class BaseEntity
    {
        /// <summary>
        /// 主键id
        /// </summary>
        [Column(Name = "id")]
        public long Id { get; set; }
    }
}
