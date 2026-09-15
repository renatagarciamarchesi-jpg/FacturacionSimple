-- conectado a la instancia (localdb)\mssqllocaldb (Por ahora)

CREATE DATABASE FacturacionSimple;
GO

USE FacturacionSimple;
GO

CREATE TABLE Productos
(
    Id      INT IDENTITY(1,1) PRIMARY KEY,
    Codigo  NVARCHAR(30)   NOT NULL,
    Nombre  NVARCHAR(100)  NOT NULL,
    Precio  DECIMAL(18,2)  NOT NULL CONSTRAINT CK_Productos_Precio CHECK (Precio >= 0),
    Activo  BIT            NOT NULL CONSTRAINT DF_Productos_Activo DEFAULT (1),
    CONSTRAINT UQ_Productos_Codigo UNIQUE (Codigo)
);
GO

CREATE TABLE Facturas
(
    Id                INT IDENTITY(1,1) PRIMARY KEY,
    Numero            INT            NOT NULL,
    Fecha             DATE           NOT NULL,
    ClienteNombre     NVARCHAR(150)  NOT NULL,
    ClienteDocumento  NVARCHAR(20)   NOT NULL,
    Total             DECIMAL(18,2)  NOT NULL,
    Anulada           BIT            NOT NULL CONSTRAINT DF_Facturas_Anulada DEFAULT (0),
    CONSTRAINT UQ_Facturas_Numero UNIQUE (Numero)
);
GO

CREATE TABLE FacturaDetalle
(
    Id              INT IDENTITY(1,1) PRIMARY KEY,
    FacturaId       INT            NOT NULL,
    ProductoId      INT            NOT NULL,
    Cantidad        INT            NOT NULL CONSTRAINT CK_FacturaDetalle_Cantidad CHECK (Cantidad > 0),
    PrecioUnitario  DECIMAL(18,2)  NOT NULL CONSTRAINT CK_FacturaDetalle_Precio CHECK (PrecioUnitario >= 0),
    Subtotal        DECIMAL(18,2)  NOT NULL,
    CONSTRAINT FK_FacturaDetalle_Facturas FOREIGN KEY (FacturaId)
        REFERENCES Facturas (Id) ON DELETE CASCADE,
    CONSTRAINT FK_FacturaDetalle_Productos FOREIGN KEY (ProductoId)
        REFERENCES Productos (Id)
);
GO

-- Datos de ejemplo opcionales, para poder probar la app sin cargar nada a mano.
INSERT INTO Productos (Codigo, Nombre, Precio, Activo) VALUES
    ('P001', 'Mouse',        5500.00,  1),
    ('P002', 'Teclado USB',         8900.00,  1),
    ('P003', 'Monitor',       145000.00,  1),
    ('P004', 'Auriculares Bluetooth', 12300.00, 1),
    ('P005', 'Webcam HD',           9800.00,  0); -- inactivo, no debe aparecer para facturar
GO
