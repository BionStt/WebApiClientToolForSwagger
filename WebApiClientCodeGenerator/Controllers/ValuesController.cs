using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiSample.Param;
using Microsoft.AspNetCore.Mvc;

namespace ApiSample.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
       
        /// <summary>
        /// GET 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "value1", "value2" };
        }

        
        /// <summary>
        /// GET BY MyQueryModel
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public ActionResult<string> Get([FromQuery]MyQueryModel query)
        {
            return Ok("value");
        }

        /// <summary>
        ///  POST api/values
        /// </summary>
        /// <param name="value">VALUE</param>
        [HttpPost]
        public MyPostModel Post([FromBody] string value)
        {
            return new MyPostModel { };
        }

       
        /// <summary>
        /// POST BY MyPostModel
        /// </summary>
        /// <param name="id">id</param>
        /// <param name="value">value</param>
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] MyPostModel value)
        {
        }


        /// <summary>
        /// DELETE api/values/5
        /// </summary>
        /// <param name="id">ID</param>
        [HttpDelete("{id}")]
        public MyReturnModel Delete(int id)
        {
            return new MyReturnModel { };
        }
    }
}
