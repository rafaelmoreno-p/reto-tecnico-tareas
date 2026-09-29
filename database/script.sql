CREATE DATABASE TaskManagementDb;
GO

USE TaskManagementDb;
GO

CREATE TABLE Tasks
(
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Title NVARCHAR(150) NOT NULL,
    Description NVARCHAR(500) NULL,
    Priority NVARCHAR(20) NOT NULL,
    Status NVARCHAR(20) NOT NULL,
    CreatedAt DATETIME2 NOT NULL DEFAULT GETDATE()
);
GO

INSERT INTO Tasks
(
    Title,
    Description,
    Priority,
    Status
)
VALUES
(
    'Implementar login',
    'Desarrollar autenticación de usuarios',
    'Alta',
    'Pendiente'
),
(
    'Crear API de tareas',
    'Implementar endpoints REST',
    'Alta',
    'En Progreso'
),
(
    'Diseñar pantalla principal',
    'Crear interfaz móvil',
    'Media',
    'Pendiente'
),
(
    'Realizar pruebas',
    'Ejecutar pruebas funcionales',
    'Media',
    'Completada'
),
(
    'Documentar proyecto',
    'Crear README y diagramas',
    'Baja',
    'Pendiente'
);
GO

CREATE OR ALTER PROCEDURE sp_Tasks_Get
    @Status NVARCHAR(20) = NULL,
    @Priority NVARCHAR(20) = NULL
AS
BEGIN

    SET NOCOUNT ON;

    SELECT
        Id,
        Title,
        Description,
        Priority,
        Status,
        CreatedAt
    FROM Tasks
    WHERE
        (@Status IS NULL OR Status = @Status)
        AND
        (@Priority IS NULL OR Priority = @Priority)
    ORDER BY Id DESC;

END;
GO

CREATE OR ALTER PROCEDURE sp_Tasks_GetById
    @Id INT
AS
BEGIN

    SET NOCOUNT ON;

    SELECT
        Id,
        Title,
        Description,
        Priority,
        Status,
        CreatedAt
    FROM Tasks
    WHERE Id = @Id;

END;
GO

EXEC sp_Tasks_Get;

EXEC sp_Tasks_Get
    @Status = 'Pendiente';

EXEC sp_Tasks_Get
    @Priority = 'Alta';

EXEC sp_Tasks_Get
    @Status = 'Pendiente',
    @Priority = 'Alta';

EXEC sp_Tasks_GetById
    @Id = 1;