using Microsoft.AspNetCore.Mvc;

namespace RoadOfGrowth.Web.Controllers
{
    /// <summary>
    /// 1.0版本接口入口
    /// </summary>
    [ApiVersion("1.0")]
    [ApiExplorerSettings(GroupName = "v1")]
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ApiV1Controller : BaseApiController
    {
    }

    /// <summary>
    /// 2.0版本接口入口
    /// </summary>
    [ApiVersion("2.0")]
    [Route("api/v2/[controller]")]
    [ApiExplorerSettings(GroupName = "v2")]
    [ApiController]
    public class ApiV2Controller : BaseApiController
    {
    }

    public enum ApiVersions
    {
        v1 = 1, v2 = 2
    }

}