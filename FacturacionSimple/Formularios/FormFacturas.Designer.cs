namespace FacturacionSimple.Formularios;

partial class FormFacturas
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

    private CheckBox chkFiltrarFecha;
    private Label lblDesde;
    private DateTimePicker dtpDesde;
    private Label lblHasta;
    private DateTimePicker dtpHasta;
    private Label lblCliente;
    private TextBox txtClienteFiltro;
    private Button btnFiltrar;
    private Button btnLimpiarFiltro;

    private DataGridView dgvFacturas;
    private Label lblSinResultados;

    private Label lblDetalle;
    private DataGridView dgvDetalle;

    private Button btnAnular;
    private Button btnCerrar;

    private void InitializeComponent()
    {
        chkFiltrarFecha = new CheckBox();
        lblDesde = new Label();
        dtpDesde = new DateTimePicker();
        lblHasta = new Label();
        dtpHasta = new DateTimePicker();
        lblCliente = new Label();
        txtClienteFiltro = new TextBox();
        btnFiltrar = new Button();
        btnLimpiarFiltro = new Button();
        dgvFacturas = new DataGridView();
        lblSinResultados = new Label();
        lblDetalle = new Label();
        dgvDetalle = new DataGridView();
        btnAnular = new Button();
        btnCerrar = new Button();
        ((System.ComponentModel.ISupportInitialize)dgvFacturas).BeginInit();
        ((System.ComponentModel.ISupportInitialize)dgvDetalle).BeginInit();
        SuspendLayout();
        // 
        // chkFiltrarFecha
        // 
        chkFiltrarFecha.Location = new Point(21, 25);
        chkFiltrarFecha.Margin = new Padding(4, 5, 4, 5);
        chkFiltrarFecha.Name = "chkFiltrarFecha";
        chkFiltrarFecha.Size = new Size(157, 38);
        chkFiltrarFecha.TabIndex = 0;
        chkFiltrarFecha.Text = "Filtrar por fecha";
        chkFiltrarFecha.CheckedChanged += chkFiltrarFecha_CheckedChanged;
        // 
        // lblDesde
        // 
        lblDesde.Location = new Point(186, 27);
        lblDesde.Margin = new Padding(4, 0, 4, 0);
        lblDesde.Name = "lblDesde";
        lblDesde.Size = new Size(64, 38);
        lblDesde.TabIndex = 1;
        lblDesde.Text = "Desde:";
        // 
        // dtpDesde
        // 
        dtpDesde.Enabled = false;
        dtpDesde.Format = DateTimePickerFormat.Short;
        dtpDesde.Location = new Point(254, 22);
        dtpDesde.Margin = new Padding(4, 5, 4, 5);
        dtpDesde.Name = "dtpDesde";
        dtpDesde.Size = new Size(170, 31);
        dtpDesde.TabIndex = 2;
        // 
        // lblHasta
        // 
        lblHasta.Location = new Point(436, 27);
        lblHasta.Margin = new Padding(4, 0, 4, 0);
        lblHasta.Name = "lblHasta";
        lblHasta.Size = new Size(57, 38);
        lblHasta.TabIndex = 3;
        lblHasta.Text = "Hasta:";
        // 
        // dtpHasta
        // 
        dtpHasta.Enabled = false;
        dtpHasta.Format = DateTimePickerFormat.Short;
        dtpHasta.Location = new Point(497, 22);
        dtpHasta.Margin = new Padding(4, 5, 4, 5);
        dtpHasta.Name = "dtpHasta";
        dtpHasta.Size = new Size(170, 31);
        dtpHasta.TabIndex = 4;
        // 
        // lblCliente
        // 
        lblCliente.Location = new Point(683, 27);
        lblCliente.Margin = new Padding(4, 0, 4, 0);
        lblCliente.Name = "lblCliente";
        lblCliente.Size = new Size(71, 38);
        lblCliente.TabIndex = 5;
        lblCliente.Text = "Cliente:";
        // 
        // txtClienteFiltro
        // 
        txtClienteFiltro.Location = new Point(757, 22);
        txtClienteFiltro.Margin = new Padding(4, 5, 4, 5);
        txtClienteFiltro.Name = "txtClienteFiltro";
        txtClienteFiltro.Size = new Size(213, 31);
        txtClienteFiltro.TabIndex = 6;
        // 
        // btnFiltrar
        // 
        btnFiltrar.Location = new Point(986, 20);
        btnFiltrar.Margin = new Padding(4, 5, 4, 5);
        btnFiltrar.Name = "btnFiltrar";
        btnFiltrar.Size = new Size(129, 42);
        btnFiltrar.TabIndex = 7;
        btnFiltrar.Text = "Filtrar";
        btnFiltrar.Click += btnFiltrar_Click;
        // 
        // btnLimpiarFiltro
        // 
        btnLimpiarFiltro.Location = new Point(1123, 20);
        btnLimpiarFiltro.Margin = new Padding(4, 5, 4, 5);
        btnLimpiarFiltro.Name = "btnLimpiarFiltro";
        btnLimpiarFiltro.Size = new Size(129, 42);
        btnLimpiarFiltro.TabIndex = 8;
        btnLimpiarFiltro.Text = "Limpiar";
        btnLimpiarFiltro.Click += btnLimpiarFiltro_Click;
        // 
        // dgvFacturas
        // 
        dgvFacturas.AllowUserToAddRows = false;
        dgvFacturas.AllowUserToDeleteRows = false;
        dgvFacturas.ColumnHeadersHeight = 34;
        dgvFacturas.Location = new Point(21, 83);
        dgvFacturas.Margin = new Padding(4, 5, 4, 5);
        dgvFacturas.MultiSelect = false;
        dgvFacturas.Name = "dgvFacturas";
        dgvFacturas.ReadOnly = true;
        dgvFacturas.RowHeadersVisible = false;
        dgvFacturas.RowHeadersWidth = 62;
        dgvFacturas.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        dgvFacturas.Size = new Size(1230, 250);
        dgvFacturas.TabIndex = 9;
        dgvFacturas.SelectionChanged += dgvFacturas_SelectionChanged;
        // 
        // lblSinResultados
        // 
        lblSinResultados.ForeColor = Color.DimGray;
        lblSinResultados.Location = new Point(21, 342);
        lblSinResultados.Margin = new Padding(4, 0, 4, 0);
        lblSinResultados.Name = "lblSinResultados";
        lblSinResultados.Size = new Size(714, 38);
        lblSinResultados.TabIndex = 10;
        lblSinResultados.Text = "No hay facturas para el período/filtro seleccionado.";
        lblSinResultados.Visible = false;
        // 
        // lblDetalle
        // 
        lblDetalle.Location = new Point(21, 347);
        lblDetalle.Margin = new Padding(4, 0, 4, 0);
        lblDetalle.Name = "lblDetalle";
        lblDetalle.Size = new Size(286, 33);
        lblDetalle.TabIndex = 11;
        lblDetalle.Text = "Detalle de la factura seleccionada:";
        // 
        // dgvDetalle
        // 
        dgvDetalle.AllowUserToAddRows = false;
        dgvDetalle.AllowUserToDeleteRows = false;
        dgvDetalle.ColumnHeadersHeight = 34;
        dgvDetalle.Location = new Point(21, 383);
        dgvDetalle.Margin = new Padding(4, 5, 4, 5);
        dgvDetalle.MultiSelect = false;
        dgvDetalle.Name = "dgvDetalle";
        dgvDetalle.ReadOnly = true;
        dgvDetalle.RowHeadersVisible = false;
        dgvDetalle.RowHeadersWidth = 62;
        dgvDetalle.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        dgvDetalle.Size = new Size(1230, 250);
        dgvDetalle.TabIndex = 12;
        // 
        // btnAnular
        // 
        btnAnular.Location = new Point(21, 650);
        btnAnular.Margin = new Padding(4, 5, 4, 5);
        btnAnular.Name = "btnAnular";
        btnAnular.Size = new Size(200, 50);
        btnAnular.TabIndex = 13;
        btnAnular.Text = "Anular factura";
        btnAnular.Click += btnAnular_Click;
        // 
        // btnCerrar
        // 
        btnCerrar.Location = new Point(1123, 650);
        btnCerrar.Margin = new Padding(4, 5, 4, 5);
        btnCerrar.Name = "btnCerrar";
        btnCerrar.Size = new Size(129, 50);
        btnCerrar.TabIndex = 14;
        btnCerrar.Text = "Cerrar";
        btnCerrar.Click += btnCerrar_Click;
        // 
        // FormFacturas
        // 
        AutoScaleDimensions = new SizeF(10F, 25F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(1276, 725);
        Controls.Add(chkFiltrarFecha);
        Controls.Add(lblDesde);
        Controls.Add(dtpDesde);
        Controls.Add(lblHasta);
        Controls.Add(dtpHasta);
        Controls.Add(lblCliente);
        Controls.Add(txtClienteFiltro);
        Controls.Add(btnFiltrar);
        Controls.Add(btnLimpiarFiltro);
        Controls.Add(dgvFacturas);
        Controls.Add(lblSinResultados);
        Controls.Add(lblDetalle);
        Controls.Add(dgvDetalle);
        Controls.Add(btnAnular);
        Controls.Add(btnCerrar);
        FormBorderStyle = FormBorderStyle.FixedDialog;
        Margin = new Padding(4, 5, 4, 5);
        MaximizeBox = false;
        MinimizeBox = false;
        Name = "FormFacturas";
        StartPosition = FormStartPosition.CenterParent;
        Text = "Consulta de facturas";
        ((System.ComponentModel.ISupportInitialize)dgvFacturas).EndInit();
        ((System.ComponentModel.ISupportInitialize)dgvDetalle).EndInit();
        ResumeLayout(false);
        PerformLayout();
    }
}
