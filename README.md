# Facturación simple — TP Desarrollo y arquitectura de software

App de escritorio (WinForms, .NET 8) para emitir y consultar facturas, usando
**ADO.NET en modo conectado** puro (sin `SqlDataAdapter`/`DataSet` ni ORMs).

## 1\. Cómo crear la base de datos

1. Abrir SQL Server Management Studio (o el Query Editor de Visual Studio)
conectado a tu instancia (por defecto se usa LocalDB).
2. Ejecutar completo el script `FacturacionSimple/Scripts/CrearBaseDatos.sql`.
Crea la base `FacturacionSimple`, las 3 tablas con sus PK/FK/`CHECK`, y
carga 5 productos de ejemplo (uno inactivo, para probar los filtros).

Si tu SQL Server no es LocalDB, ajustá la cadena de conexión.

## 2\. Cadena de conexión

Está centralizada en `FacturacionSimple/Datos/Conexion.cs`:

```csharp
Server=(localdb)\\\\\\\\\\\\\\\\mssqllocaldb;Database=FacturacionSimple;Integrated Security=true;TrustServerCertificate=true;
```

## 3\. Cómo ejecutar

1. Abrir `FacturacionSimple.sln` en Visual Studio 2022 (con la carga de
trabajo ".NET desktop development").
2. Restaurar paquetes NuGet (se descarga `Microsoft.Data.SqlClient`).
3. F5 para compilar y ejecutar. Arranca `FormPrincipal`, desde donde se
accede a las 4 pantallas: Productos, Nueva factura, Consultar facturas
e Informe por producto.

## 4\. Estructura del proyecto

```
TP 1 - ADO.NET Modo conectado/
├── README.md
├── Diagrama de clases.jpg
└── FacturacionSimple/
    ├── FacturacionSimple.sln
    └── FacturacionSimple/
        ├── FacturacionSimple.csproj
        ├── Program.cs
        ├── Entidades/     (Producto, Factura, FacturaDetalle, ItemInforme)
        ├── Datos/          (Conexion, ProductoDao, FacturaDao, ManejadorErroresSql)
        ├── Formularios/    (FormPrincipal, FormProductos, FormFacturaNueva,
        │                     FormFacturas, FormInforme)
        └── Scripts/
            └── CrearBaseDatos.sql
```

**Los formularios no tienen SQL embebido**: toda la persistencia vive en
`ProductoDao` y `FacturaDao`, usando siempre `SqlConnection` + `SqlCommand`
parametrizado (nunca concatenación de strings) + `SqlDataReader`.

## 5\. Decisiones de arquitectura

* **El precio vive en `FacturaDetalle`, no se relee de `Productos`.**
`FacturaDao.Insertar` copia `PrecioUnitario` del producto al momento de
facturar. Si después cambia `Productos.Precio`, las facturas ya emitidas
no se ven afectadas porque `ObtenerConDetalle` siempre lee el precio
guardado en la línea, no en el catálogo.
* **Cabecera + detalle van en una sola transacción** (`SqlTransaction` en
`FacturaDao.Insertar`): si falla cualquier `INSERT` (de la cabecera o de
cualquier línea), se hace `Rollback()` completo y no queda nada grabado.
* **Dónde se valida:** en la UI se valida lo básico antes de tocar la base
(campos vacíos, cantidad > 0, al menos una línea) para dar feedback rápido;
en SQL quedan las reglas de integridad que no se pueden garantizar solo
desde la UI (`UNIQUE` en `Codigo`/`Numero`, `CHECK` de precio/cantidad ≥ 0,
`FOREIGN KEY` para no dejar detalle huérfano). `ManejadorErroresSql`
traduce esos errores de SQL Server a mensajes legibles.
* **Baja de productos:** si un producto ya fue usado en alguna factura, no
se permite `DELETE` (rompería la integridad histórica); se ofrece baja
lógica (`Activo = 0`). Si nunca fue facturado, se permite eliminarlo
físicamente.
* **Anulación de facturas** por estado (`Anulada = 1`) en vez de `DELETE`,
para no perder el historial; `FacturaDao.Anular` no permite anular dos
veces (actualiza solo si `Anulada = 0` y valida `RowsAffected`).

## 6\. Fuera de alcance

Login/roles, impresión PDF, AFIP, múltiples puntos de venta, stock, EF Core.
