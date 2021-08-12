# MicroRabbit
Microservices with RabbitMQ


## O que precisa ter instalado?
- .net 6
- Docker
- SQL Server Managment ou qualquer gerenciador SQL;


### Subir um docker do Rabbit e um do SQL Server

```docker
docker run -e 'ACCEPT_EULA=Y' -e 'SA_PASSWORD=Q!w2e3r4t5' -e 'MSSQL_PID=Express' -p 1433:1433 -d mcr.microsoft.com/mssql/server:2017-latest-ubuntu

docker run -d --hostname my-rabbit --name some-rabbit -p 8080:15672 -p 5672:5672 rabbitmq:3-management
```

### Acessar o SQL Server com as credencias:
usuário: sa 
senha: Q!w2e3r4t5

### Executar os scripts abaixo na ordem para criar as bases de dados e os usuários de cada microserviço

```sql


---------- Criação das bases de dados -----------
USE master
GO

IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'BankingDb')
BEGIN
  CREATE DATABASE BankingDb;
END;
GO

IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'TransferDb')
BEGIN
  CREATE DATABASE TransferDb;
END;
GO

-------------------------

-- Creates the login usr_banking with password 'Q!w2e3r4t5'.  
CREATE LOGIN usr_banking   
    WITH PASSWORD = 'Q!w2e3r4t5';  
GO  

-- Creates a database user for the login created above.  
CREATE USER usr_banking FOR LOGIN usr_banking;  
GO

------------CREATE USER BankingDb -------------------

USE BankingDb
GO

CREATE USER usr_banking FOR LOGIN usr_banking;  
GO

EXEC sp_addrolemember N'db_owner', N'usr_banking'
GO

------------CREATE USER TransferDb -------------------

USE TransferDb
GO

CREATE USER usr_banking FOR LOGIN usr_banking;  
GO


EXEC sp_addrolemember N'db_owner', N'usr_banking'
GO
```

### Executar as migrates dos dois microserviços
```
Update-Database
```

Após, só rodar os microserviços na ordem que precisar.
