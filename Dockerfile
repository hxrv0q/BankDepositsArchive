FROM mcr.microsoft.com/mssql/server:2022-latest

ENV ACCEPT_EULA=Y

COPY ./initdb.d/init.sql /docker-entrypoint-initdb.d/init.sql

CMD /opt/mssql/bin/sqlservr & sleep 30 && \
    /opt/mssql-tools/bin/sqlcmd -S localhost -U SA -P "${SA_PASSWORD}" -d master -i /docker-entrypoint-initdb.d/init.sql && \
    tail -f /dev/null
