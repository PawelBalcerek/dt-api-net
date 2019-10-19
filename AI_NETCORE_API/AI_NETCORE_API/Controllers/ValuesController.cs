//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using Data.SomethingProviding.Abstract;
//using Microsoft.AspNetCore.Mvc;

//namespace AI_NETCORE_API.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class ValuesController : ControllerBase
//    {
//        private readonly ISomethingProvider _somethingProvider;

//        public ValuesController(ISomethingProvider somethingProvider)
//        {
//            _somethingProvider = somethingProvider;
//        }

//        // GET api/values
//        [HttpGet]
//        public ActionResult<IEnumerable<string>> Get()
//        {
//            return new string[] { "value1", "value2" };
//        }

//        // GET api/values/5
//        [HttpGet("{id}")]
//        public ActionResult<bool> Get(int id)
//        {
//            return _somethingProvider.sialalala();
//        }

//        // POST api/values
//        [HttpPost]
//        public void Post([FromBody] string value)
//        {
//        }

//        // PUT api/values/5
//        [HttpPut("{id}")]
//        public void Put(int id, [FromBody] string value)
//        {
//        }

//        // DELETE api/values/5
//        [HttpDelete("{id}")]
//        public void Delete(int id)
//        {
//        }
//    }
//}
