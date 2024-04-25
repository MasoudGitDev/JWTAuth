dotnet tool update --global dotnet-ef --version 8.0.4
dotnet ef migrations add init --context AppWriteDbContext
dotnet ef database update