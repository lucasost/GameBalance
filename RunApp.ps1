docker-compose up -d
docker cp createDb.sql GameBalance:/home 
docker exec -it GameBalance /opt/mssql-tools/bin/sqlcmd -S 127.0.0.1 -U sa -P admin123@ -i /home/createDb.sql