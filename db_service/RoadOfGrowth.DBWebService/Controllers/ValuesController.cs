using Microsoft.AspNetCore.Mvc;
using RoadOfGrowth.DBRepository.Interface;
using System.Collections.Generic;

namespace RoadOfGrowth.DBWebService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        public static IUserService _userService;
        public static ILogService _logService;

        public ValuesController(IUserService userService,ILogService logService)
        {
            _userService = userService;
            _logService = logService;
        }
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            string temp = _userService.GetUserAccount(1);
            _logService.Insert();
            return new string[] { "value1", "value2", temp };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
