CREATE DATABASE HistorialOperaciones;
GO

USE HistorialOperaciones;
GO

CREATE TABLE HistorialOperaciones (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Operacion VARCHAR(100),
    Resultado FLOAT,
    Fecha DATETIME DEFAULT GETDATE()
);

select * from HistorialOperaciones
