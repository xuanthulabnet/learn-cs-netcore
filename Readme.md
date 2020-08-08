# Học C# với .net core
https://xuanthulab.net/lap-trinh-c-co-ban/

## Chuẩn bị MS SQL SERVER
Nếu chưa có một SQL Server để thực hành thì Repo 
này cung cấp một ```docker-compose``` để nhanh chóng tạo một SQL Server với Docker.

Tải về Repo:
```
git clone git@github.com:xuanthulabnet/learn-cs-netcore.git
```
Sau đó chạy lệnh tạo SQL Server
```
cd learn-cs-netcore/MSSQL
docker-compose up -d
```
SQL Server này lắng nghe ở cổng ```1433``` có password tài khoản ```sa``` là ```Password123```, container có tên ```sqlserver-xtlab```


Nếu muốn có một database mẫu, thì thực hiện lệnh:
```
docker exec sqlserver-xtlab /var/opt/mssql/backup/restore.sh
```

Nó phục hồi một CSDL mẫu có tên ```xtlab```, cấu trúc và dữ liệu mẫu của database này giống ở trường hợp này: https://xuanthulab.net/chay-sql-online-cong-cu-hoc-cau-lenh-sql.html

Có thể dùng công cụ Azure Data Studio để kết nối thử theo hướng dẫn tại: https://xuanthulab.net/cai-dat-ms-sql-server-linux-voi-docker.html#connect

# Cập nhật NET CORE 3.X
Trong Repo có các ví dụ chạy trên NET CORE 2.X nếu muốn chuyển sang .NET CORE 3.X
thì mở file ```.csproj``` và thay 
```
<TargetFramework>netcoreapp2.2</TargetFramework>
```
Bằng 
```
<TargetFramework>netcoreapp3.1</TargetFramework>
```
Sau đó có thể phải kiểm tra từng Package trong mục ```ItemGroup``` cần cập nhật bản mới, tìm bản mới tại
https://www.nuget.org/packages/

Ví dụ
```
  <ItemGroup>
    <!-- <PackageReference Include="Microsoft.Extensions.Configuration" Version="2.2.0" /> -->
    <PackageReference Include="Microsoft.Extensions.Configuration" Version="3.1.6" />
    <PackageReference Include="Microsoft.Extensions.Configuration.Json" Version="3.1.6" />
    <PackageReference Include="Microsoft.Extensions.Options.ConfigurationExtensions" Version="3.1.6" />
    <!-- <PackageReference Include="System.Data.SqlClient" Version="4.6.1" /> -->
    <PackageReference Include="System.Data.SqlClient" Version="4.8.1" />
  </ItemGroup>
```
Sau đó có thể build, clean để kiểm thư
```
dotnet restore
dotnet build
dotnet clean
```
