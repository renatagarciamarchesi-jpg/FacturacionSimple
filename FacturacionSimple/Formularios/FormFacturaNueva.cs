using FacturacionSimple.Datos;
using FacturacionSimple.Entidades;

namespace FacturacionSimple.Formularios;

public partial class FormFacturaNueva : Form
{
    private readonly ProductoDao productoDao = new();
    private readonly FacturaDao facturaDao = new();
    private readonly List<FacturaDetalle> lineas = new();

    public FormFacturaNueva()
    {
        InitializeComponent();
        Load += FormFacturaNueva_Load;
    }

    private void FormFacturaNueva_Load(object? sender, EventArgs e)
    {
        dtpFecha.Value = DateTime.Today;
        ActualizarGrillaYTotal(); //Arranca con la grilla de detalle vacía

        try
        {
            List<Producto> productosActivos = productoDao.ListarActivos();
            cmbProducto.DataSource = productosActivos; //Sin DisplayMember: usa Producto.ToString()
        }
        catch (Exception ex)
        {
            MostrarError(ex);
        }
    }

    private void btnAgregarLinea_Click(object? sender, EventArgs e)
    {
        if (cmbProducto.SelectedItem is not Producto productoSeleccionado)
        {
            MessageBox.Show(this, "Seleccione un producto.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        int cantidad = (int)numCantidad.Value;
        if (cantidad <= 0)
        {
            MessageBox.Show(this, "La cantidad debe ser mayor a 0.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        //Si el producto ya está en la factura, se suman
        //las cantidades en la misma línea en lugar de crear una línea repetida.
        FacturaDetalle? existente = lineas.FirstOrDefault(l => l.ProductoId == productoSeleccionado.Id);
        if (existente is not null)
        {
            existente.Cantidad += cantidad;
        }
        else
        {
            lineas.Add(new FacturaDetalle
            {
                ProductoId = productoSeleccionado.Id,
                ProductoCodigo = productoSeleccionado.Codigo,
                ProductoNombre = productoSeleccionado.Nombre,
                Cantidad = cantidad,
                PrecioUnitario = productoSeleccionado.Precio
            });
        }

        numCantidad.Value = 1;
        ActualizarGrillaYTotal();
    }

    private void btnQuitarLinea_Click(object? sender, EventArgs e)
    {
        if (dgvDetalle.CurrentRow?.DataBoundItem is not FacturaDetalle linea)
        {
            MessageBox.Show(this, "Seleccione una línea de la grilla.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        lineas.Remove(linea);
        ActualizarGrillaYTotal();
    }

    private void ActualizarGrillaYTotal()
    {
        dgvDetalle.DataSource = null;
        dgvDetalle.DataSource = lineas;

        dgvDetalle.Columns["Id"].Visible = false;
        dgvDetalle.Columns["FacturaId"].Visible = false;
        dgvDetalle.Columns["ProductoId"].Visible = false;
        dgvDetalle.Columns["ProductoCodigo"].Visible = false;
        dgvDetalle.Columns["ProductoNombre"].HeaderText = "Producto";
        dgvDetalle.Columns["PrecioUnitario"].HeaderText = "Precio unitario";
        dgvDetalle.Columns["PrecioUnitario"].DefaultCellStyle.Format = "0.00";
        dgvDetalle.Columns["Subtotal"].DefaultCellStyle.Format = "0.00";

        decimal total = lineas.Sum(l => l.Subtotal);
        lblTotal.Text = $"Total: $ {total:0.00}";
    }

    private void btnGrabar_Click(object? sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(txtClienteNombre.Text))
        {
            MessageBox.Show(this, "Ingrese el nombre del cliente.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        if (lineas.Count == 0)
        {
            MessageBox.Show(this, "Agregue al menos una línea a la factura.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        Factura factura = new()
        {
            Fecha = dtpFecha.Value.Date,
            ClienteNombre = txtClienteNombre.Text.Trim(),
            ClienteDocumento = txtClienteDocumento.Text.Trim(),
            Detalle = lineas
        };

        try
        {
            int numero = facturaDao.Insertar(factura);
            MessageBox.Show(this, $"Factura N° {numero} grabada correctamente.", "Factura emitida",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
            DialogResult = DialogResult.OK;
            Close();
        }
        catch (Exception ex)
        {
            MostrarError(ex);
        }
    }

    private void btnCancelar_Click(object? sender, EventArgs e)
    {
        Close();
    }

    private void MostrarError(Exception ex)
    {
        MessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
    }
}
