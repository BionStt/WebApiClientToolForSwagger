# WebApiClientToolForSwagger
一个用于解析swagger.json生成符合[WebApiClient](https://github.com/dotnetcore/WebApiClient)接口代码的代码生成工具

### quick start （demo）
```
git clone https://github.com/fanpan26/WebApiClientToolForSwagger
```
运行 ApiSample/build.bat 

修改 WebApiClient.Tool.Console/Program.cs
```
 static string swaggerJsonUrl = "http://localhost:5000/swagger/v1/swagger.json";//改成自己项目的URL
```
运行 WebApiClient.Tool.Console项目。
