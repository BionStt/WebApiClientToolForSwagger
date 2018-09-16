cd ./Tool.EXE
dotnet build
dotnet publish -o publish
cd publish
dotnet WebApiClient.Tool.Console.dll