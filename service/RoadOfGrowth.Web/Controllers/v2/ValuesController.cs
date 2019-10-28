using Microsoft.AspNetCore.Mvc;
using RoadOfGrowth.Repository;
using System.Collections.Generic;

namespace RoadOfGrowth.Web.Controllers.v2
{
    public class ValuesController : ApiV2Controller
    {
        public static ITestRepository _testRepository;
        public ValuesController(ITestRepository testRepository)
        {
            _testRepository = testRepository;
        }
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "value1", _testRepository.DoTestRepository() };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return $"{1 / id}";
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
