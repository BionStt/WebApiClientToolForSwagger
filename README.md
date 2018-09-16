# WebApiClientToolForSwagger
一个用于解析swagger.json生成符合[WebApiClient](https://github.com/dotnetcore/WebApiClient)接口代码的代码生成工具

### quick start （demo）
```
git clone https://github.com/fanpan26/WebApiClientToolForSwagger
```
运行 run-api.bat  run-tool.bat


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
