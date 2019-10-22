namespace RoadOfGrowth.DBUtility.Providers.DataObject
{
    /// <summary>
    /// 分页查询结果数据
    /// </summary>
    public class QueryPageModel
    {
        public QueryPageModel(int pageIndex = 0, int pageSize = 10, int totalCount = 0)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
            TotalCount = totalCount;
        }

        /// <summary>
        /// 当前页码
        /// </summary>
        public int PageIndex { get; set; }

        /// <summary>
        /// 每页数量
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// 结果总数
        /// </summary>
        public int TotalCount { get; set; }

        /// <summary>
        /// 结果数据
        /// </summary>
        public object Data { get; set; }

    }

    /// <summary>
    /// 分页查询结果数据
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class QueryPageModel<T> : QueryPageModel
    {
        public QueryPageModel(int pageIndex = 0, int pageSize = 10, int totalCount = 0)
            : base(pageIndex, pageSize, totalCount)
        {
        }

        /// <summary>
        /// 结果数据
        /// </summary>
        public new T Data { get; set; }
    }
}
