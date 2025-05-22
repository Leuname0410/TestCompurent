# TestCompurent

Execute next procedure in MS SQL Server Management Studio:
```sql
USE [MusicRadioDB];

CREATE PROCEDURE ResetClientPassword
    @ClientId NVARCHAR(10),
    @NewPassword NVARCHAR(100) = 'AQAAAAIAAYagAAAAEAMN7qhbcUpKu80y5sM19TwWJNhSW+ureLYIWHW3ZL+WaOWCCTiA1koUzrNG53X6tw=' -- MusicRadio
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE Clients
    SET Password = @NewPassword
    WHERE Id = @ClientId;
END
