# WebApiClientToolForSwagger
一个用于解析swagger.json生成符合[WebApiClient](https://github.com/dotnetcore/WebApiClient)接口代码的代码生成工具.
+ （目前只支持大部分的类型解析，可能会有部分类型解析不了。在实际项目应用中慢慢完善吧） 

### quick start （demo）
```
git clone https://github.com/fanpan26/WebApiClientToolForSwagger
```
运行 run-api.bat  run-tool.bat

生成代码路径 ：./Tool.EXE/publish/codes


#### Swashbuckle.AspNetCore 3.0.0 .Net Core 2.1

```
        //实际应用，修改此URL即可
        static string swaggerJsonUrl = "http://localhost:5000/swagger/v1/swagger.json";


        static void Main(string[] args)
        {
            SwaggerToWebApiClientGenerator codeGenerator = new SwaggerToWebApiClientGenerator();
            codeGenerator.Start(swaggerJsonUrl).Wait();
            SysConsole.WriteLine("finished...");
            SysConsole.Read();
        }
```

生成的代码格式如下：
```
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
```
