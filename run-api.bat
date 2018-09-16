cd ./ApiSample
dotnet build
dotnet publish -o publish
cd publish
dotnet ApiSample.dll