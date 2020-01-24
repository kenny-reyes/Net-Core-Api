pwd
dotnet restore
dotnet ef database update 
dotnet watch run --urls "http://0.0.0.0:5000;https://0.0.0.0:5001"
