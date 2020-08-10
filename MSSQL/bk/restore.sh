#!/usr/bin/env bash

# #CHECK MEDIA
#    /opt/mssql-tools/bin/sqlcmd \
#    -S localhost -U SA -P 'Password123' \
#    -Q "RESTORE FILELISTONLY FROM DISK = '/var/opt/mssql/backup/xtlab.bak'"

   
 /opt/mssql-tools/bin/sqlcmd \
-S localhost -U SA -P 'Password123' \
-Q 'RESTORE DATABASE xtlab FROM DISK = "/var/opt/mssql/backup/xtlab.bak" WITH MOVE "xtlab" TO "/var/opt/mssql/data/xtlab.mdf", MOVE "xtlab_log" TO "/var/opt/mssql/data/xtlab_log.ldf"'

