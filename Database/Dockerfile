FROM mcr.microsoft.com/mssql/server:2022-latest
WORKDIR /scripts

# Copia i file (non serve --chmod se lo eseguiamo con bash)
COPY wait-and-run.sh /wait-and-run.sh
COPY CreateDatabaseAndSeed.sql /CreateDatabaseAndSeed.sql

# Assicurati che lo script abbia la riga iniziale: #!/bin/bash
CMD [ "bash", "/wait-and-run.sh" ]
