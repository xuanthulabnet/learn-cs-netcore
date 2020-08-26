# (EF Core) Sinh ra các entity từ database với công cụ dotnet ef trong C# CSharp

https://xuanthulab.net/ef-core-sinh-ra-cac-entity-tu-database-voi-cong-cu-dotnet-ef-trong-c-csharp.html


Install dotnet-ef
```
dotnet tool install --global dotnet-ef
```
Test
```
dotnet ef --version
```
Use
```
dotnet ef dbcontext scaffold -o Models -f -d "Data Source=localhost,1433;Initial Catalog=shopdata;User ID=SA;Password=Password123" "Microsoft.EntityFrameworkCore.SqlServer"

```