USE master
GO

IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'TestTemplate14Db')
BEGIN
  CREATE DATABASE TestTemplate14Db;
END;
GO

USE TestTemplate14Db;
GO

IF NOT EXISTS (SELECT 1
                 FROM sys.server_principals
                WHERE [name] = N'TestTemplate14Db_Login' 
                  AND [type] IN ('C','E', 'G', 'K', 'S', 'U'))
BEGIN
    CREATE LOGIN TestTemplate14Db_Login
        WITH PASSWORD = '<DB_PASSWORD>';
END;
GO  

IF NOT EXISTS (select * from sys.database_principals where name = 'TestTemplate14Db_User')
BEGIN
    CREATE USER TestTemplate14Db_User FOR LOGIN TestTemplate14Db_Login;
END;
GO  


EXEC sp_addrolemember N'db_datareader', N'TestTemplate14Db_User';
GO

EXEC sp_addrolemember N'db_datawriter', N'TestTemplate14Db_User';
GO

EXEC sp_addrolemember N'db_ddladmin', N'TestTemplate14Db_User';
GO
