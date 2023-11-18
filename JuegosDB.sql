CREATE DATABASE JuegosDB
GO

USE JuegosDB
GO

-- Tablas
CREATE TABLE Usuario (
    ID INT PRIMARY KEY IDENTITY(1,1),
    NombreUsuario NVARCHAR(50) NOT NULL,
    CorreoElectronico NVARCHAR(100) NOT NULL,
    Contrase�a NVARCHAR(255) NOT NULL,
);
GO

CREATE TABLE Compa�ia (
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
    Compa�iaID INT,
    FOREIGN KEY (Compa�iaID) REFERENCES Compa�ia(ID)
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
@id int
AS
BEGIN
    SELECT * FROM Usuario
	WHERE ID = @id
END
GO

CREATE PROCEDURE AgregarUsuario
    @NombreUsuario NVARCHAR(50),
    @CorreoElectronico NVARCHAR(100),
    @Contrase�a NVARCHAR(255)
AS
BEGIN
    INSERT INTO Usuario (NombreUsuario, CorreoElectronico, Contrase�a)
    VALUES (@NombreUsuario, @CorreoElectronico, @Contrase�a);
END
GO

CREATE PROCEDURE ActualizarUsuario
    @UsuarioID INT,
    @NombreUsuario NVARCHAR(50),
    @CorreoElectronico NVARCHAR(100),
    @Contrase�a NVARCHAR(255)
AS
BEGIN
    UPDATE Usuario
    SET NombreUsuario = @NombreUsuario,
        CorreoElectronico = @CorreoElectronico,
        Contrase�a = @Contrase�a
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


--Compa�ia
CREATE PROCEDURE VerCompa�ia
@id int
AS
BEGIN
    SELECT * FROM Compa�ia
	WHERE ID = @id
END
GO

CREATE PROCEDURE AgegarCompa�ia
    @Nombre NVARCHAR(100)
AS
BEGIN
    INSERT INTO Compa�ia (Nombre)
    VALUES (@Nombre);
END;
GO

CREATE PROCEDURE ActualizarCompa�ia
@ID int,
@Nombre NVARCHAR(100)
AS
BEGIN
UPDATE Compa�ia
SET Nombre = @Nombre
WHERE ID = @ID
END
GO

CREATE PROCEDURE EliminarCompa�ia
    @id INT
AS
BEGIN
    DELETE FROM Compa�ia
    WHERE ID = @id;
END;
GO

--Plataforma
CREATE PROCEDURE VerPlataforma
@id int
AS
BEGIN
    SELECT * FROM Plataforma
	WHERE ID = @id
END
GO

CREATE PROCEDURE AgregarPlataforma
    @Nombre NVARCHAR(100)
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

CREATE PROCEDURE EliminarPlataforma
    @id INT
AS
BEGIN
    DELETE FROM Plataforma
    WHERE ID = @id;
END;
GO

--juego
CREATE PROCEDURE VerJuego
@id int
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
    @Compa�iaID INT
AS
BEGIN
    INSERT INTO Juego(Nombre, Descripcion, FechaLanzamiento, Compa�iaID)
    VALUES (@Nombre, @Descripcion, @FechaLanzamiento, @Compa�iaID);
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
        Compa�iaID = @CompaniaID
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
@id int
AS
BEGIN
    SELECT * FROM Personaje
	WHERE ID = @id
END
GO

CREATE PROCEDURE AgregarPersonaje
    @Nombre NVARCHAR(100),
    @Descripcion NVARCHAR(MAX),
    @JuegoID INT
AS
BEGIN
    INSERT INTO Personaje(Nombre, Descripcion, JuegoID)
    VALUES (@Nombre, @Descripcion, @JuegoID);
END
GO

CREATE PROCEDURE ActualizarPersonaje
    @PersonajeID INT,
	@Nombre VARCHAR(100),
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

CREATE PROCEDURE EliminarPersonaje
    @id INT
AS
BEGIN
    DELETE FROM Personaje
    WHERE ID = @id;
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

--compa�ia
CREATE TRIGGER InsCompa�ia
ON Compa�ia
after insert
AS
BEGIN
INSERT INTO BitacoraJuegos(Host, Transaccion, Usuario, Fecha_Mod, Tabla)
VALUES (@@SERVERNAME, SUSER_NAME(),'Insert', GETDATE(), 'Compa�ia')
END
GO

CREATE TRIGGER UpdCompa�ia
ON Compa�ia
after update
AS
BEGIN
INSERT INTO BitacoraJuegos(Host, Transaccion, Usuario, Fecha_Mod, Tabla)
VALUES (@@SERVERNAME, SUSER_NAME(),'Update', GETDATE(), 'Compa�ia')
END
GO

CREATE TRIGGER DelCompa�ia
ON Compa�ia
after delete
AS
BEGIN
INSERT INTO BitacoraJuegos(Host, Transaccion, Usuario, Fecha_Mod, Tabla)
VALUES (@@SERVERNAME, SUSER_NAME(),'Delete', GETDATE(), 'Compa�ia')
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

