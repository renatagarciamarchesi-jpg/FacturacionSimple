namespace FacturacionSimple.Formularios;

public partial class FormPrincipal : Form
{
    public FormPrincipal()
    {
        InitializeComponent();
    }

    private void btnProductos_Click(object? sender, EventArgs e)
    {
        using FormProductos formulario = new FormProductos();
        formulario.ShowDialog(this);
    }

    private void btnNuevaFactura_Click(object? sender, EventArgs e)
    {
        using FormFacturaNueva formulario = new FormFacturaNueva();
        formulario.ShowDialog(this);
    }

    private void btnConsultarFacturas_Click(object? sender, EventArgs e)
    {
        using FormFacturas formulario = new FormFacturas();
        formulario.ShowDialog(this);
    }

    private void btnInforme_Click(object? sender, EventArgs e)
    {
        using FormInforme formulario = new FormInforme();
        formulario.ShowDialog(this);
    }

    private void btnSalir_Click(object? sender, EventArgs e)
    {
        Close();
    }
}
