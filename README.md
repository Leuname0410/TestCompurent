# TestCompurent

TestCompurent es una aplicación web desarrollada con ASP.NET Core y Entity Framework Core, que permite la gestión de clientes y administradores para una empresa de alquiler de equipos de cómputo.

Configuración inicial
1. Configurar la cadena de conexión
En el archivo appsettings.json, modifica la propiedad DefaultConnection para que apunte a tu instancia de SQL Server. Reemplaza (tu server) con el nombre de tu servidor:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=(tu server);Database=MusicRadioDB;Trusted_Connection=True;MultipleActiveResultSets=true"
}

2. Aplicar Migraciones a la Base de Datos
Abre la Consola del Administrador de Paquetes NuGet y ejecuta el siguiente comando para aplicar las migraciones y crear la base de datos:

Update-Database

3. Procedimiento Almacenado para Restablecer Contraseña
La aplicación incluye un procedimiento almacenado en la base de datos para restablecer la contraseña de un cliente. Puedes crear este procedimiento ejecutando el siguiente script en SQL Server Management Studio:


```sql
USE [MusicRadioDB];

CREATE PROCEDURE ResetClientPassword
    @ClientId NVARCHAR(10),
    @NewPassword NVARCHAR(100) = 'AQAAAAIAAYagAAAAEAMN7qhbcUpKu80y5sM19TwWJNhSW+ureLYIWHW3ZL+WaOWCCTiA1koUzrNG53X6tw=' -- MusicRadio
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY
        UPDATE Clients
        SET Password = @NewPassword
        WHERE Id = @ClientId;

        -- Opcional: podrías verificar si se actualizó alguna fila
        IF @@ROWCOUNT = 0
        BEGIN
            RAISERROR('No se encontró el cliente con el ID especificado.', 16, 1);
        END
    END TRY
    BEGIN CATCH
        DECLARE @ErrorMessage NVARCHAR(4000);
        DECLARE @ErrorSeverity INT;
        DECLARE @ErrorState INT;

        SELECT 
            @ErrorMessage = ERROR_MESSAGE(),
            @ErrorSeverity = ERROR_SEVERITY(),
            @ErrorState = ERROR_STATE();

        RAISERROR(@ErrorMessage, @ErrorSeverity, @ErrorState);
    END CATCH
END

4. Ejecutar la Aplicación
Inicia la aplicación desde Visual Studio o utilizando el comando dotnet run. Una vez en ejecución, puedes acceder a las siguientes vistas:

Vista de Usuario: https://localhost:7174/index.html

Vista de Administrador: https://localhost:7174/indexAdmin.html

Estructura del Proyecto
Controllers/: Controladores que manejan las solicitudes HTTP.

Models/: Modelos de datos que representan las entidades de la aplicación.

Data/: Contexto de la base de datos y configuración de Entity Framework.

Migrations/: Archivos de migración generados por Entity Framework.

wwwroot/: Archivos estáticos como HTML, CSS y JavaScript.

appsettings.json: Archivo de configuración de la aplicación.

Requisitos
.NET 6 SDK

SQL Server

Visual Studio 2022 (opcional, pero recomendado)