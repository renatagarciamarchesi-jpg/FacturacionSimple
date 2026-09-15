using FacturacionSimple.Datos;
using FacturacionSimple.Entidades;

namespace FacturacionSimple.Formularios;

public partial class FormFacturas : Form
{
    private readonly FacturaDao facturaDao = new();

    public FormFacturas()
    {
        InitializeComponent();
        Load += FormFacturas_Load;
    }

    private void FormFacturas_Load(object? sender, EventArgs e)
    {
        Buscar();
    }

    private void chkFiltrarFecha_CheckedChanged(object? sender, EventArgs e)
    {
        dtpDesde.Enabled = chkFiltrarFecha.Checked;
        dtpHasta.Enabled = chkFiltrarFecha.Checked;
    }

    private void btnFiltrar_Click(object? sender, EventArgs e)
    {
        Buscar();
    }

    private void btnLimpiarFiltro_Click(object? sender, EventArgs e)
    {
        chkFiltrarFecha.Checked = false;
        txtClienteFiltro.Clear();
        Buscar();
    }

    private void Buscar()
    {
        try
        {
            DateTime? desde = chkFiltrarFecha.Checked ? dtpDesde.Value.Date : null;
            DateTime? hasta = chkFiltrarFecha.Checked ? dtpHasta.Value.Date : null;
            string? texto = string.IsNullOrWhiteSpace(txtClienteFiltro.Text) ? null : txtClienteFiltro.Text.Trim();

            List<Factura> facturas = facturaDao.Listar(desde, hasta, texto);

            dgvFacturas.DataSource = null;
            dgvFacturas.DataSource = facturas;

            dgvFacturas.Columns["Id"].Visible = false;
            dgvFacturas.Columns["Detalle"].Visible = false;
            dgvFacturas.Columns["Numero"].HeaderText = "N°";
            dgvFacturas.Columns["Fecha"].DefaultCellStyle.Format = "dd/MM/yyyy";
            dgvFacturas.Columns["ClienteNombre"].HeaderText = "Cliente";
            dgvFacturas.Columns["ClienteDocumento"].HeaderText = "Documento";
            dgvFacturas.Columns["Total"].DefaultCellStyle.Format = "0.00";

            lblSinResultados.Visible = facturas.Count == 0;
            dgvDetalle.DataSource = null;
        }
        catch (Exception ex)
        {
            MostrarError(ex);
        }
    }

    private void dgvFacturas_SelectionChanged(object? sender, EventArgs e)
    {
        if (dgvFacturas.CurrentRow?.DataBoundItem is not Factura facturaSeleccionada)
        {
            dgvDetalle.DataSource = null;
            return;
        }

        try
        {
            Factura? facturaCompleta = facturaDao.ObtenerConDetalle(facturaSeleccionada.Id);
            dgvDetalle.DataSource = null;
            dgvDetalle.DataSource = facturaCompleta?.Detalle ?? new List<FacturaDetalle>();

            dgvDetalle.Columns["Id"].Visible = false;
            dgvDetalle.Columns["FacturaId"].Visible = false;
            dgvDetalle.Columns["ProductoId"].Visible = false;
            dgvDetalle.Columns["ProductoCodigo"].Visible = false;
            dgvDetalle.Columns["ProductoNombre"].HeaderText = "Producto";
            dgvDetalle.Columns["PrecioUnitario"].HeaderText = "Precio unitario";
            dgvDetalle.Columns["PrecioUnitario"].DefaultCellStyle.Format = "0.00";
            dgvDetalle.Columns["Subtotal"].DefaultCellStyle.Format = "0.00";
        }
        catch (Exception ex)
        {
            MostrarError(ex);
        }
    }

    private void btnAnular_Click(object? sender, EventArgs e)
    {
        if (dgvFacturas.CurrentRow?.DataBoundItem is not Factura facturaSeleccionada)
        {
            MessageBox.Show(this, "Seleccione una factura de la grilla.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        if (facturaSeleccionada.Anulada)
        {
            MessageBox.Show(this, "Esa factura ya está anulada.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        DialogResult confirmacion = MessageBox.Show(this,
            $"¿Confirma que desea anular la factura N° {facturaSeleccionada.Numero}? Esta acción no se puede deshacer.",
            "Confirmar anulación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

        if (confirmacion != DialogResult.Yes)
        {
            return;
        }

        try
        {
            facturaDao.Anular(facturaSeleccionada.Id);
            Buscar();
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
