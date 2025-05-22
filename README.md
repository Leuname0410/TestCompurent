# TestCompurent

Execute next procedure in MS SQL Server Management Studio:
```sql
USE [MusicRadioDB]

CREATE PROCEDURE ResetClientPassword
    @ClientId NVARCHAR(10),
    @NewPassword NVARCHAR(30) = 'MusicRadio'
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE Clients
    SET Password = @NewPassword
    WHERE Id = @ClientId;
END
