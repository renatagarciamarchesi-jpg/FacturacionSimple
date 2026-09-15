using FacturacionSimple.Datos;
using FacturacionSimple.Entidades;

namespace FacturacionSimple.Formularios;

public partial class FormInforme : Form
{
    private readonly FacturaDao facturaDao = new();

    public FormInforme()
    {
        InitializeComponent();
    }

    private void btnGenerar_Click(object? sender, EventArgs e)
    {
        if (dtpDesde.Value.Date > dtpHasta.Value.Date)
        {
            MessageBox.Show(this, "La fecha 'Desde' no puede ser posterior a la fecha 'Hasta'.", "Validación",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        try
        {
            List<ItemInforme> items = facturaDao.InformePorProducto(dtpDesde.Value.Date, dtpHasta.Value.Date);

            dgvInforme.DataSource = null;
            dgvInforme.DataSource = items;

            // dgvInforme tiene AutoGenerateColumns = true: arma solo una
            // columna por cada propiedad de ItemInforme.
            dgvInforme.Columns["ProductoId"].Visible = false;
            dgvInforme.Columns["ProductoCodigo"].HeaderText = "Código";
            dgvInforme.Columns["ProductoNombre"].HeaderText = "Producto";
            dgvInforme.Columns["CantidadFacturada"].HeaderText = "Cantidad facturada";
            dgvInforme.Columns["MontoFacturado"].HeaderText = "Monto facturado";
            dgvInforme.Columns["MontoFacturado"].DefaultCellStyle.Format = "0.00";

            lblSinResultados.Visible = items.Count == 0;
            lblTotalGeneral.Text = $"Total del período: $ {items.Sum(i => i.MontoFacturado):0.00}";
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void btnCerrar_Click(object? sender, EventArgs e)
    {
        Close();
    }
}
