using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using WebApiClient.Attributes;

namespace WebApiClient.Tool
{
    public interface ISwaggerApi : IHttpApi
    {
        [HttpGet]
        ITask<JObject> GetApiJson([Url] string swaggerJsonUrl);
    }
}
