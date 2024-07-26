-- Crear la base de datos
CREATE DATABASE LocalEmpanadas;
GO

-- Usar la base de datos creada
USE LocalEmpanadas;
GO

-- Crear tabla de Productos
CREATE TABLE Productos (
    ID_Producto INT PRIMARY KEY IDENTITY(1,1),
    Nombre NVARCHAR(100) NOT NULL,
    Descripcion NVARCHAR(255),
    Precio DECIMAL(10, 2) NOT NULL,
    Estado BIT NOT NULL DEFAULT 1
);
GO

-- Crear tabla de Clientes
CREATE TABLE Clientes (
    ID_Cliente INT PRIMARY KEY IDENTITY(1,1),
    Nombre NVARCHAR(100) NOT NULL,
    Apellido NVARCHAR(100) NOT NULL,
    Telefono NVARCHAR(20),
    Estado BIT NOT NULL DEFAULT 1
);
GO

-- Crear tabla de Pedidos
CREATE TABLE Pedidos (
    ID_Pedido INT PRIMARY KEY IDENTITY(1,1),
    ID_Cliente INT NOT NULL,
    FechaPedido DATETIME NOT NULL DEFAULT GETDATE(),
    Total DECIMAL(10, 2) NOT NULL,
    Estado CHAR NOT NULL CHECK(Estado IN ('T', 'P')), -- 'T' = Terminado ; 'P' = Pendiente.
    FOREIGN KEY (ID_Cliente) REFERENCES Clientes(ID_Cliente)
);
GO

-- Crear tabla de DetallePedido
CREATE TABLE DetallePedido (
    ID_Detalle INT PRIMARY KEY IDENTITY(1,1),
    ID_Pedido INT NOT NULL,
    ID_Producto INT NOT NULL,
    Cantidad INT NOT NULL,
    Precio DECIMAL(10, 2) NOT NULL,
    FOREIGN KEY (ID_Pedido) REFERENCES Pedidos(ID_Pedido),
    FOREIGN KEY (ID_Producto) REFERENCES Productos(ID_Producto)
);
GO

-- Insertar datos en la tabla de Productos
INSERT INTO Productos (Nombre, Descripcion, Precio) VALUES
('Empanada de Carne', 'Empanada rellena de carne', 20.00),
('Empanada de Pollo', 'Empanada rellena de pollo', 20.00),
('Empanada de Jamón y Queso', 'Empanada rellena de jamón y queso', 20.00);
GO

-- Insertar datos en la tabla de Clientes
INSERT INTO Clientes (Nombre, Apellido, Telefono) VALUES
('Carlos', 'López', '123456789'),
('Ana', 'Martínez', '987654321');
GO

-- Insertar un pedido de ejemplo
INSERT INTO Pedidos (ID_Cliente, Total, Estado) VALUES
(1, 40.00, 'P');
GO

-- Obtener el ID del pedido recién insertado
DECLARE @ID_Pedido INT;
SET @ID_Pedido = SCOPE_IDENTITY();

-- Insertar detalles del pedido
INSERT INTO DetallePedido (ID_Pedido, ID_Producto, Cantidad, Precio) VALUES
(@ID_Pedido, 1, 2, 20.00), -- 2 Empanadas de Carne
(@ID_Pedido, 2, 1, 20.00); -- 1 Empanada de Pollo
GO

Create Or Alter Procedure SP_InsertarCliente(
@Nombre Nvarchar(100),
@Apellido Nvarchar(100),
@Telefono Nvarchar(20),
@Estado Bit,
@IDCliente Int Output,
@Mensaje Varchar(500) Output
)
As Begin
		
		Declare @HayCliente Int
		Set @IDCliente = 0;

		Select @HayCliente = Count(*) From Clientes Where Telefono = @Telefono

		If @HayCliente = 0 Begin
			Insert Into Clientes(Nombre, Apellido, Telefono, Estado) Values (@Nombre, @Apellido, @Telefono, @Estado)
			Set @IDCliente = SCOPE_IDENTITY();
		End
		Else Begin
			Set @Mensaje = 'Ya existe un cliente con ese mismo telefono.'
		End
End
go
CREATE OR ALTER PROCEDURE SP_EditarCliente
(
    @IDCliente INT,
    @Nombre NVARCHAR(100),
    @Apellido NVARCHAR(100),
    @Telefono NVARCHAR(20),
    @Estado BIT,
    @Resultado BIT OUTPUT,
    @Mensaje VARCHAR(500) OUTPUT
)
AS
BEGIN
    DECLARE @HayCliente INT;
    SET @Resultado = 0;

    -- Verificar que el cliente existe y el número de teléfono no pertenece a otro cliente
    SELECT @HayCliente = COUNT(*) 
    FROM Clientes 
    WHERE ID_Cliente = @IDCliente AND (Telefono = @Telefono OR NOT EXISTS (SELECT 1 FROM Clientes WHERE Telefono = @Telefono));

    IF @HayCliente = 1
    BEGIN
        UPDATE Clientes 
        SET 
            Nombre = @Nombre,
            Apellido = @Apellido,
            Telefono = @Telefono,
            Estado = @Estado
        WHERE ID_Cliente = @IDCliente;

        SET @Resultado = 1;
    END
    ELSE
    BEGIN
        SET @Mensaje = 'El cliente que intenta editar no existe o el teléfono pertenece a otro cliente.';
    END
END;

