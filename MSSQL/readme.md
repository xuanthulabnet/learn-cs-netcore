## Cài đặt MS SQL Server trên Docker
```
docker-compose up -d
```
container name: sqlserver-xtlab

port: 1433

user: sa

password: Password123

## Phục hồi dữ liệu mẫu
```
docker exec sqlserver-xtlab /var/opt/mssql/backup/restore.sh
```
Tên CSDL: xtlab

Cấu trúc dữ liệu mẫu:
https://xuanthulab.net/chay-sql-online-cong-cu-hoc-cau-lenh-sql.html