namespace FacturacionSimple.Formularios;

partial class FormPrincipal
{
    private System.ComponentModel.IContainer components = null;

    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    private Label lblTitulo;
    private Button btnProductos;
    private Button btnNuevaFactura;
    private Button btnConsultarFacturas;
    private Button btnInforme;
    private Button btnSalir;

    private void InitializeComponent()
    {
        lblTitulo = new Label();
        btnProductos = new Button();
        btnNuevaFactura = new Button();
        btnConsultarFacturas = new Button();
        btnInforme = new Button();
        btnSalir = new Button();
        SuspendLayout();
        // 
        // lblTitulo
        // 
        lblTitulo.Font = new Font("Segoe UI", 16F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
        lblTitulo.Location = new Point(43, 42);
        lblTitulo.Margin = new Padding(4, 0, 4, 0);
        lblTitulo.Name = "lblTitulo";
        lblTitulo.Size = new Size(486, 67);
        lblTitulo.TabIndex = 0;
        lblTitulo.Text = "Facturación simple";
        lblTitulo.TextAlign = ContentAlignment.MiddleCenter;
        // 
        // btnProductos
        // 
        btnProductos.Location = new Point(43, 150);
        btnProductos.Margin = new Padding(4, 5, 4, 5);
        btnProductos.Name = "btnProductos";
        btnProductos.Size = new Size(486, 75);
        btnProductos.TabIndex = 1;
        btnProductos.Text = "Productos";
        btnProductos.Click += btnProductos_Click;
        // 
        // btnNuevaFactura
        // 
        btnNuevaFactura.Location = new Point(43, 242);
        btnNuevaFactura.Margin = new Padding(4, 5, 4, 5);
        btnNuevaFactura.Name = "btnNuevaFactura";
        btnNuevaFactura.Size = new Size(486, 75);
        btnNuevaFactura.TabIndex = 2;
        btnNuevaFactura.Text = "Nueva factura";
        btnNuevaFactura.Click += btnNuevaFactura_Click;
        // 
        // btnConsultarFacturas
        // 
        btnConsultarFacturas.Location = new Point(43, 333);
        btnConsultarFacturas.Margin = new Padding(4, 5, 4, 5);
        btnConsultarFacturas.Name = "btnConsultarFacturas";
        btnConsultarFacturas.Size = new Size(486, 75);
        btnConsultarFacturas.TabIndex = 3;
        btnConsultarFacturas.Text = "Consultar facturas";
        btnConsultarFacturas.Click += btnConsultarFacturas_Click;
        // 
        // btnInforme
        // 
        btnInforme.Location = new Point(43, 425);
        btnInforme.Margin = new Padding(4, 5, 4, 5);
        btnInforme.Name = "btnInforme";
        btnInforme.Size = new Size(486, 75);
        btnInforme.TabIndex = 4;
        btnInforme.Text = "Informe por producto";
        btnInforme.Click += btnInforme_Click;
        // 
        // btnSalir
        // 
        btnSalir.Location = new Point(449, 561);
        btnSalir.Margin = new Padding(4, 5, 4, 5);
        btnSalir.Name = "btnSalir";
        btnSalir.Size = new Size(109, 58);
        btnSalir.TabIndex = 5;
        btnSalir.Text = "Salir";
        btnSalir.Click += btnSalir_Click;
        // 
        // FormPrincipal
        // 
        AutoScaleDimensions = new SizeF(10F, 25F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(571, 633);
        Controls.Add(lblTitulo);
        Controls.Add(btnProductos);
        Controls.Add(btnNuevaFactura);
        Controls.Add(btnConsultarFacturas);
        Controls.Add(btnInforme);
        Controls.Add(btnSalir);
        FormBorderStyle = FormBorderStyle.FixedSingle;
        Margin = new Padding(4, 5, 4, 5);
        MaximizeBox = false;
        Name = "FormPrincipal";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "Facturación simple";
        ResumeLayout(false);
    }
}
