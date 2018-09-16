using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiSample.Param;
using Microsoft.AspNetCore.Mvc;

namespace ApiSample.Controllers
{
    [Route("api/[controller]/[action]")]
    public class TestController : Controller
    {
        /// <summary>
        /// 这个是测试GET方法
        /// </summary>
        /// <param name="id">参数ID</param>
        /// <param name="name">参数Name</param>
        /// <returns></returns>
        [HttpGet]
        [ActionName("getDemo")]
        public IActionResult GetDemo([FromHeader]string id,[FromForm]string name)
        {
            return Ok($"{id}{name}");
        }

        /// <summary>
        ///这个是测试POST方法 
        /// </summary>
        /// <param name="testPostParameter">参数PostParameter</param>
        /// <returns></returns>
        [HttpGet]
        [ActionName("postDemo")]
        public IActionResult PostDemo([FromBody]TestPostParameter testPostParameter)
        {
            return Ok(new TestPostParameter());
        }
    }
}