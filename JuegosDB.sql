CREATE DATABASE JuegosDB
GO

USE JuegosDB
GO

-- Tablas
CREATE TABLE Usuario (
    ID INT PRIMARY KEY IDENTITY(1,1),
    NombreUsuario NVARCHAR(50) NOT NULL,
    CorreoElectronico NVARCHAR(100) NOT NULL,
    Contraseña NVARCHAR(255) NOT NULL,
);
GO

CREATE TABLE Compañia (
    ID INT PRIMARY KEY IDENTITY(1,1),
    Nombre NVARCHAR(100) NOT NULL
);
GO

CREATE TABLE Plataforma (
    ID INT PRIMARY KEY IDENTITY(1,1),
    Nombre NVARCHAR(50) NOT NULL
);
GO

CREATE TABLE Juego (
    ID INT PRIMARY KEY IDENTITY(1,1),
    Nombre NVARCHAR(100) NOT NULL,
    Descripcion NVARCHAR(MAX),
    FechaLanzamiento DATE,
    CompañiaID INT,
    FOREIGN KEY (CompañiaID) REFERENCES Compañia(ID)
);
GO

CREATE TABLE Personaje (
    ID INT PRIMARY KEY IDENTITY(1,1),
    Nombre NVARCHAR(100) NOT NULL,
    Descripcion NVARCHAR(MAX),
    JuegoID INT,
    FOREIGN KEY (JuegoID) REFERENCES Juego(ID)
);
GO

--Bitacora
CREATE TABLE BitacoraJuegos(
Id INT PRIMARY KEY IDENTITY NOT NULL,
Transaccion VARCHAR (10) DEFAULT NULL,
Usuario VARCHAR(40) DEFAULT NULL,
Host VARCHAR (40) NOT NULL,
Fecha_Mod DATETIME DEFAULT NULL,
Tabla VARCHAR(20) NOT NULL,
);
Go


--procedimientos almacenados
--Usuarios 
CREATE PROCEDURE VerUsuarios
@id
AS
BEGIN
    SELECT * FROM Usuario
	WHERE ID = @id
END
GO

CREATE PROCEDURE AgregarUsuario
    @NombreUsuario NVARCHAR(50),
    @CorreoElectronico NVARCHAR(100),
    @Contraseña NVARCHAR(255)
AS
BEGIN
    INSERT INTO Usuario (NombreUsuario, CorreoElectronico, Contraseña)
    VALUES (@NombreUsuario, @CorreoElectronico, @Contraseña);
END
GO

CREATE PROCEDURE ActualizarUsuario
    @UsuarioID INT,
    @NombreUsuario NVARCHAR(50),
    @CorreoElectronico NVARCHAR(100),
    @Contraseña NVARCHAR(255)
AS
BEGIN
    UPDATE Usuario
    SET NombreUsuario = @NombreUsuario,
        CorreoElectronico = @CorreoElectronico,
        Contraseña = @Contraseña
    WHERE ID = @UsuarioID;
END;
GO

CREATE PROCEDURE EliminarUsuario
    @UsuarioID INT
AS
BEGIN
    DELETE FROM Usuario
    WHERE ID = @UsuarioID;
END;
GO


--Compañia
CREATE PROCEDURE VerCompañia
@id
AS
BEGIN
    SELECT * FROM Compañia
	WHERE ID = @id
END
GO

CREATE PROCEDURE AgegarCompañia
    @Nombre NVARCHAR(100),
AS
BEGIN
    INSERT INTO Compañia (Nombre)
    VALUES (@Nombre);
END;
GO

CREATE PROCEDURE ActualizarCompañia
@ID int,
@Nombre NVARCHAR(100)
AS
BEGIN
UPDATE Compañia
SET Nombre = @Nombre
WHERE ID = @ID
END
GO

CREATE PROCEDURE EliminarCompañia
    @UsuarioID INT
AS
BEGIN
    DELETE FROM Compañia
    WHERE ID = @UsuarioID;
END;
GO

--Plataforma
CREATE PROCEDURE VerPlataforma
@id
AS
BEGIN
    SELECT * FROM Plataforma
	WHERE ID = @id
END
GO

CREATE PROCEDURE AgregarPlataforma
    @Nombre NVARCHAR(100),
AS
BEGIN
    INSERT INTO Plataforma (Nombre)
    VALUES (@Nombre);
END;
GO

CREATE PROCEDURE ActualizarPlataforma
@ID int,
@Nombre NVARCHAR(100)
AS
BEGIN
UPDATE Plataforma
SET Nombre = @Nombre
WHERE ID = @ID
END
GO

--juego
CREATE PROCEDURE VerJuego
@id
AS
BEGIN
    SELECT * FROM Juego
	WHERE ID = @id
END
GO

CREATE PROCEDURE AgregarJuego
    @Nombre NVARCHAR(100),
    @Descripcion NVARCHAR(MAX),
    @FechaLanzamiento DATE,
    @CompañiaID INT
AS
BEGIN
    INSERT INTO Juego(Nombre, Descripcion, FechaLanzamiento, CompañiaID)
    VALUES (@Nombre, @Descripcion, @FechaLanzamiento, CompañiaID);
END
GO

CREATE PROCEDURE ActualizarJuego
    @JuegoID INT,
    @Nombre NVARCHAR(100),
    @Descripcion NVARCHAR(MAX),
    @FechaLanzamiento DATE,
    @CompaniaID INT
AS
BEGIN
    UPDATE Juego
    SET Nombre = @Nombre,
        Descripcion = @Descripcion,
        FechaLanzamiento = @FechaLanzamiento,
        CompaniaID = @CompaniaID
    WHERE ID = @JuegoID;
END;
GO

CREATE PROCEDURE EliminarJuego
    @JuegoID INT
AS
BEGIN
    DELETE FROM Juego
    WHERE ID = @JuegoID;
END;
GO

--personaje
CREATE PROCEDURE VerPersonaje
@id
AS
BEGIN
    SELECT * FROM Personaje
	WHERE ID = @id
END
GO

CREATE PROCEDURE AgregarPersonaje
    @Nombre NVARCHAR(100),
    @Descripcion NVARCHAR(MAX),
    @JuegoID INT,
AS
BEGIN
    INSERT INTO Juego(Nombre, Descripcion, JuegoId)
    VALUES (@Nombre, @Descripcion, @JuegoId);
END
GO

CREATE PROCEDURE ActualizarPersonaje
    @PersonajeID INT,
    @Descripcion NVARCHAR(MAX),
    @JuegoID INT
AS
BEGIN
    UPDATE Personaje
    SET Nombre = @Nombre,
        Descripcion = @Descripcion,
        JuegoID = @JuegoID
    WHERE ID = @PersonajeID;
END;
GO

--triggers
--usuario
CREATE TRIGGER InsUsuario
ON Usuario
after insert
AS
BEGIN
INSERT INTO BitacoraJuegos(Host, Transaccion, Usuario, Fecha_Mod, Tabla)
VALUES (@@SERVERNAME, SUSER_NAME(),'Insert', GETDATE(), 'Usuario')
END
GO

CREATE TRIGGER UpdUsuario
ON Usuario
after update
AS
BEGIN
INSERT INTO BitacoraJuegos(Host, Transaccion, Usuario, Fecha_Mod, Tabla)
VALUES (@@SERVERNAME, SUSER_NAME(),'Update', GETDATE(), 'Usuario')
END
GO

CREATE TRIGGER DelUsuario
ON Usuario
after delete
AS
BEGIN
INSERT INTO BitacoraJuegos(Host, Transaccion, Usuario, Fecha_Mod, Tabla)
VALUES (@@SERVERNAME, SUSER_NAME(),'Delete', GETDATE(), 'Usuario')
END
GO

--compañia
CREATE TRIGGER InsCompañia
ON Compañia
after insert
AS
BEGIN
INSERT INTO BitacoraJuegos(Host, Transaccion, Usuario, Fecha_Mod, Tabla)
VALUES (@@SERVERNAME, SUSER_NAME(),'Insert', GETDATE(), 'Compañia')
END
GO

CREATE TRIGGER UpdCompañia
ON Compañia
after update
AS
BEGIN
INSERT INTO BitacoraJuegos(Host, Transaccion, Usuario, Fecha_Mod, Tabla)
VALUES (@@SERVERNAME, SUSER_NAME(),'Update', GETDATE(), 'Compañia')
END
GO

CREATE TRIGGER DelCompañia
ON Compañia
after delete
AS
BEGIN
INSERT INTO BitacoraJuegos(Host, Transaccion, Usuario, Fecha_Mod, Tabla)
VALUES (@@SERVERNAME, SUSER_NAME(),'Delete', GETDATE(), 'Compañia')
END
GO

--plataforma
CREATE TRIGGER InsPlataforma
ON Plataforma
after insert
AS
BEGIN
INSERT INTO BitacoraJuegos(Host, Transaccion, Usuario, Fecha_Mod, Tabla)
VALUES (@@SERVERNAME, SUSER_NAME(),'Insert', GETDATE(), 'Plataforma')
END
GO

CREATE TRIGGER UpdPlataforma
ON Plataforma
after update
AS
BEGIN
INSERT INTO BitacoraJuegos(Host, Transaccion, Usuario, Fecha_Mod, Tabla)
VALUES (@@SERVERNAME, SUSER_NAME(),'Update', GETDATE(), 'Plataforma')
END
GO

CREATE TRIGGER DelPlataforma
ON Plataforma
after delete
AS
BEGIN
INSERT INTO BitacoraJuegos(Host, Transaccion, Usuario, Fecha_Mod, Tabla)
VALUES (@@SERVERNAME, SUSER_NAME(),'Delete', GETDATE(), 'Plataforma')
END
GO

--juego
CREATE TRIGGER InsJuego
ON Juego
after insert
AS
BEGIN
INSERT INTO BitacoraJuegos(Host, Transaccion, Usuario, Fecha_Mod, Tabla)
VALUES (@@SERVERNAME, SUSER_NAME(),'Insert', GETDATE(), 'Juego')
END
GO

CREATE TRIGGER UpdJuego
ON Juego
after update
AS
BEGIN
INSERT INTO BitacoraJuegos(Host, Transaccion, Usuario, Fecha_Mod, Tabla)
VALUES (@@SERVERNAME, SUSER_NAME(),'Update', GETDATE(), 'Juego')
END
GO

CREATE TRIGGER DelJuego
ON Juego
after delete
AS
BEGIN
INSERT INTO BitacoraJuegos(Host, Transaccion, Usuario, Fecha_Mod, Tabla)
VALUES (@@SERVERNAME, SUSER_NAME(),'Delete', GETDATE(), 'Juego')
END
GO

--personaje

CREATE TRIGGER InsPersonaje
ON Personaje
after insert
AS
BEGIN
INSERT INTO BitacoraJuegos(Host, Transaccion, Usuario, Fecha_Mod, Tabla)
VALUES (@@SERVERNAME, SUSER_NAME(),'Insert', GETDATE(), 'Personaje')
END
GO

CREATE TRIGGER UpdPersonaje
ON Personaje
after update
AS
BEGIN
INSERT INTO BitacoraJuegos(Host, Transaccion, Usuario, Fecha_Mod, Tabla)
VALUES (@@SERVERNAME, SUSER_NAME(),'Update', GETDATE(), 'Personaje')
END
GO

CREATE TRIGGER DelPersonaje
ON Personaje
after delete
AS
BEGIN
INSERT INTO BitacoraJuegos(Host, Transaccion, Usuario, Fecha_Mod, Tabla)
VALUES (@@SERVERNAME, SUSER_NAME(),'Delete', GETDATE(), 'Personaje')
END
GO