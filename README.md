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
