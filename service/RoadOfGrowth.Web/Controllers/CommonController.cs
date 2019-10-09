using Microsoft.AspNetCore.Mvc;

namespace RoadOfGrowth.Web.Controllers
{
    public class CommonController : Controller
    {
        public IActionResult Error()
        {
            return Content("500");
        }
    }
}