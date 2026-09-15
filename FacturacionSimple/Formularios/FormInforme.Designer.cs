namespace FacturacionSimple.Formularios;

partial class FormInforme
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

    private Label lblDesde;
    private DateTimePicker dtpDesde;
    private Label lblHasta;
    private DateTimePicker dtpHasta;
    private Button btnGenerar;
    private DataGridView dgvInforme;
    private Label lblSinResultados;
    private Label lblTotalGeneral;
    private Button btnCerrar;

    private void InitializeComponent()
    {
        lblDesde = new Label();
        dtpDesde = new DateTimePicker();
        lblHasta = new Label();
        dtpHasta = new DateTimePicker();
        btnGenerar = new Button();
        dgvInforme = new DataGridView();
        lblSinResultados = new Label();
        lblTotalGeneral = new Label();
        btnCerrar = new Button();
        ((System.ComponentModel.ISupportInitialize)dgvInforme).BeginInit();
        SuspendLayout();

        lblDesde.Location = new Point(15, 15);
        lblDesde.Size = new Size(45, 23);
        lblDesde.Text = "Desde:";

        dtpDesde.Location = new Point(65, 12);
        dtpDesde.Size = new Size(120, 23);
        dtpDesde.Format = DateTimePickerFormat.Short;
        dtpDesde.Value = DateTime.Today.AddMonths(-1);

        lblHasta.Location = new Point(195, 15);
        lblHasta.Size = new Size(40, 23);
        lblHasta.Text = "Hasta:";

        dtpHasta.Location = new Point(238, 12);
        dtpHasta.Size = new Size(120, 23);
        dtpHasta.Format = DateTimePickerFormat.Short;

        btnGenerar.Location = new Point(370, 11);
        btnGenerar.Size = new Size(100, 25);
        btnGenerar.Text = "Generar";
        btnGenerar.Click += btnGenerar_Click;

        dgvInforme.Location = new Point(15, 50);
        dgvInforme.Size = new Size(560, 250);
        dgvInforme.AllowUserToAddRows = false;
        dgvInforme.AllowUserToDeleteRows = false;
        dgvInforme.ReadOnly = true;
        dgvInforme.AutoGenerateColumns = true; // arma las columnas solo, a partir de las propiedades de ItemInforme
        dgvInforme.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        dgvInforme.MultiSelect = false;
        dgvInforme.RowHeadersVisible = false;

        lblSinResultados.Location = new Point(15, 305);
        lblSinResultados.Size = new Size(400, 23);
        lblSinResultados.Text = "No hay facturas en el período seleccionado.";
        lblSinResultados.ForeColor = Color.DimGray;
        lblSinResultados.Visible = false;

        lblTotalGeneral.Location = new Point(300, 305);
        lblTotalGeneral.Size = new Size(275, 23);
        lblTotalGeneral.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
        lblTotalGeneral.TextAlign = ContentAlignment.MiddleRight;

        btnCerrar.Location = new Point(485, 340);
        btnCerrar.Size = new Size(90, 30);
        btnCerrar.Text = "Cerrar";
        btnCerrar.Click += btnCerrar_Click;

        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(592, 385);
        Controls.Add(lblDesde);
        Controls.Add(dtpDesde);
        Controls.Add(lblHasta);
        Controls.Add(dtpHasta);
        Controls.Add(btnGenerar);
        Controls.Add(dgvInforme);
        Controls.Add(lblSinResultados);
        Controls.Add(lblTotalGeneral);
        Controls.Add(btnCerrar);
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;
        MinimizeBox = false;
        StartPosition = FormStartPosition.CenterParent;
        Text = "Informe por producto";
        ((System.ComponentModel.ISupportInitialize)dgvInforme).EndInit();
        ResumeLayout(false);
        PerformLayout();
    }
}
