using FacturacionSimple.Datos;
using FacturacionSimple.Entidades;

namespace FacturacionSimple.Formularios;

public partial class FormProductos : Form
{
    private readonly ProductoDao productoDao = new();
    private int idSeleccionado = 0;

    public FormProductos()
    {
        InitializeComponent();
        CargarGrilla(productoDao.Listar());
    }

    private void CargarGrilla(List<Producto> productos)
    {
        dgvProductos.DataSource = null;
        dgvProductos.DataSource = productos;

        dgvProductos.Columns["Id"].Visible = false;
        dgvProductos.Columns["Codigo"].HeaderText = "Código";
        dgvProductos.Columns["Precio"].DefaultCellStyle.Format = "0.00";
    }

    private void btnBuscar_Click(object? sender, EventArgs e)
    {
        try
        {
            string texto = txtBuscar.Text.Trim();
            List<Producto> resultado = string.IsNullOrEmpty(texto)
                ? productoDao.Listar()
                : productoDao.Buscar(texto);
            CargarGrilla(resultado);
        }
        catch (Exception ex)
        {
            MostrarError(ex);
        }
    }

    private void dgvProductos_SelectionChanged(object? sender, EventArgs e)
    {
        if (dgvProductos.CurrentRow?.DataBoundItem is not Producto producto)
        {
            return;
        }

        idSeleccionado = producto.Id;
        txtCodigo.Text = producto.Codigo;
        txtNombre.Text = producto.Nombre;
        txtPrecio.Text = producto.Precio.ToString("0.00");
        chkActivo.Checked = producto.Activo;
    }

    private void btnNuevo_Click(object? sender, EventArgs e)
    {
        idSeleccionado = 0;
        txtCodigo.Clear();
        txtNombre.Clear();
        txtPrecio.Clear();
        chkActivo.Checked = true;
        dgvProductos.ClearSelection();
        txtCodigo.Focus();
    }

    private void btnGuardar_Click(object? sender, EventArgs e)
    {
        string codigo = txtCodigo.Text.Trim();
        string nombre = txtNombre.Text.Trim();

        if (string.IsNullOrEmpty(codigo))
        {
            MessageBox.Show(this, "El código no puede estar vacío.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        if (string.IsNullOrEmpty(nombre))
        {
            MessageBox.Show(this, "El nombre no puede estar vacío.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        if (!decimal.TryParse(txtPrecio.Text.Trim(), out decimal precio) || precio < 0)
        {
            MessageBox.Show(this, "El precio debe ser un número mayor o igual a 0.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        try
        {
            if (productoDao.ExisteCodigo(codigo, idSeleccionado == 0 ? null : idSeleccionado))
            {
                MessageBox.Show(this, "Ya existe un producto con ese código.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Producto producto = new()
            {
                Id = idSeleccionado,
                Codigo = codigo,
                Nombre = nombre,
                Precio = precio,
                Activo = chkActivo.Checked
            };

            if (idSeleccionado == 0)
            {
                productoDao.Insertar(producto);
            }
            else
            {
                productoDao.Modificar(producto);
            }

            CargarGrilla(productoDao.Listar());
            btnNuevo_Click(sender, e);
        }
        catch (Exception ex)
        {
            MostrarError(ex);
        }
    }

    private void btnEliminar_Click(object? sender, EventArgs e)
    {
        if (idSeleccionado == 0)
        {
            MessageBox.Show(this, "Seleccione un producto de la grilla.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        try
        {
            if (productoDao.TieneDetalles(idSeleccionado))
            {
                DialogResult respuesta = MessageBox.Show(this,
                    "Este producto ya fue usado en facturas y no se puede eliminar. ¿Desea darlo de baja (dejarlo inactivo) en su lugar?",
                    "No se puede eliminar", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

                if (respuesta == DialogResult.Yes)
                {
                    productoDao.DarDeBaja(idSeleccionado);
                    CargarGrilla(productoDao.Listar());
                    btnNuevo_Click(sender, e);
                }
                return;
            }

            DialogResult confirmacion = MessageBox.Show(this,
                "¿Confirma que desea eliminar este producto?",
                "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirmacion != DialogResult.Yes)
            {
                return;
            }

            productoDao.Eliminar(idSeleccionado);
            CargarGrilla(productoDao.Listar());
            btnNuevo_Click(sender, e);
        }
        catch (Exception ex)
        {
            MostrarError(ex);
        }
    }

    private void btnCerrar_Click(object? sender, EventArgs e)
    {
        Close();
    }

    private void MostrarError(Exception ex)
    {
        MessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
    }
}
