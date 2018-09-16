using System;
using System.Collections.Generic;
using System.Text;
using WebApiClient;
using WebApiClient.Attributes;

namespace Test.NameSpace
{
    public interface ISwaggerApi : IHttpApi
    {
		/// <summary>
		/// 这个是测试GET方法
		/// <summary>
		[HttpGet("/api/Test/getDemo")]
		ITask<object> ApiTestGetDemo([Header("id")]string id,[FormField]string name);

		/// <summary>
		/// 这个是测试POST方法
		/// <summary>
		[HttpGet("/api/Test/postDemo")]
		ITask<object> ApiTestPostDemo([JsonContent]TestPostParameter testPostParameter);

		/// <summary>
		/// GET
		/// <summary>
		[HttpGet("/api/Values")]
		ITask<string[]> ApiValues();

		/// <summary>
		/// POST api/values
		/// <summary>
		[HttpPost("/api/Values")]
		ITask<MyPostModel> ApiValues([JsonContent]string value);

		/// <summary>
		/// GET BY MyQueryModel
		/// <summary>
		[HttpGet("/api/Values/{id}")]
		ITask<string> ApiValuesById([PathQuery]string keyword,[PathQuery]string keyword1,[PathQuery]string keyword2,[PathQuery]string keyword3,[PathQuery]string keyword4,[PathQuery]string keyword5,[PathQuery]string id);

		/// <summary>
		/// POST BY MyPostModel
		/// <summary>
		[HttpPut("/api/Values/{id}")]
		ITask<object> ApiValuesById([PathQuery]int id,[JsonContent]MyPostModel value);

		/// <summary>
		/// DELETE api/values/5
		/// <summary>
		[HttpDelete("/api/Values/{id}")]
		ITask<MyReturnModel> ApiValuesById([PathQuery]int id);


    }
}