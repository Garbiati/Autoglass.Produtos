USE master
GO

CREATE LOGIN api_autoglass 
	WITH PASSWORD = '' 
GO

USE dbAutoglass
GO
CREATE USER api_autoglass FROM LOGIN api_autoglass;
ALTER ROLE db_datareader ADD MEMBER api_autoglass;
ALTER ROLE db_datawriter ADD MEMBER api_autoglass;
GRANT CREATE TABLE TO api_autoglass;
GRANT CONTROL ON SCHEMA::dbo TO api_autoglass;