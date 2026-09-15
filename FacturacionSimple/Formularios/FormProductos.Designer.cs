namespace FacturacionSimple.Formularios;

partial class FormProductos
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

    private TextBox txtBuscar;
    private Button btnBuscar;
    private DataGridView dgvProductos;
    private Label lblCodigo;
    private TextBox txtCodigo;
    private Label lblNombre;
    private TextBox txtNombre;
    private Label lblPrecio;
    private TextBox txtPrecio;
    private CheckBox chkActivo;
    private Button btnNuevo;
    private Button btnGuardar;
    private Button btnEliminar;
    private Button btnCerrar;

    private void InitializeComponent()
    {
        txtBuscar = new TextBox();
        btnBuscar = new Button();
        dgvProductos = new DataGridView();
        lblCodigo = new Label();
        txtCodigo = new TextBox();
        lblNombre = new Label();
        txtNombre = new TextBox();
        lblPrecio = new Label();
        txtPrecio = new TextBox();
        chkActivo = new CheckBox();
        btnNuevo = new Button();
        btnGuardar = new Button();
        btnEliminar = new Button();
        btnCerrar = new Button();
        ((System.ComponentModel.ISupportInitialize)dgvProductos).BeginInit();
        SuspendLayout();

        // txtBuscar
        txtBuscar.Location = new Point(15, 15);
        txtBuscar.Size = new Size(260, 23);
        txtBuscar.PlaceholderText = "Buscar por código o nombre...";

        // btnBuscar
        btnBuscar.Location = new Point(285, 14);
        btnBuscar.Size = new Size(90, 25);
        btnBuscar.Text = "Buscar";
        btnBuscar.Click += btnBuscar_Click;

        // dgvProductos
        dgvProductos.Location = new Point(15, 50);
        dgvProductos.Size = new Size(600, 220);
        dgvProductos.AllowUserToAddRows = false;
        dgvProductos.AllowUserToDeleteRows = false;
        dgvProductos.ReadOnly = true;
        dgvProductos.AutoGenerateColumns = true; // arma las columnas solo, a partir de las propiedades de Producto
        dgvProductos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        dgvProductos.MultiSelect = false;
        dgvProductos.RowHeadersVisible = false;
        dgvProductos.SelectionChanged += dgvProductos_SelectionChanged;

        // lblCodigo
        lblCodigo.Location = new Point(15, 285);
        lblCodigo.Size = new Size(80, 23);
        lblCodigo.Text = "Código:";

        // txtCodigo
        txtCodigo.Location = new Point(100, 282);
        txtCodigo.Size = new Size(120, 23);

        // lblNombre
        lblNombre.Location = new Point(235, 285);
        lblNombre.Size = new Size(70, 23);
        lblNombre.Text = "Nombre:";

        // txtNombre
        txtNombre.Location = new Point(310, 282);
        txtNombre.Size = new Size(200, 23);

        // lblPrecio
        lblPrecio.Location = new Point(15, 318);
        lblPrecio.Size = new Size(80, 23);
        lblPrecio.Text = "Precio:";

        // txtPrecio
        txtPrecio.Location = new Point(100, 315);
        txtPrecio.Size = new Size(120, 23);

        // chkActivo
        chkActivo.Location = new Point(235, 317);
        chkActivo.Size = new Size(80, 23);
        chkActivo.Text = "Activo";
        chkActivo.Checked = true;

        // btnNuevo
        btnNuevo.Location = new Point(15, 355);
        btnNuevo.Size = new Size(100, 30);
        btnNuevo.Text = "Nuevo";
        btnNuevo.Click += btnNuevo_Click;

        // btnGuardar
        btnGuardar.Location = new Point(125, 355);
        btnGuardar.Size = new Size(100, 30);
        btnGuardar.Text = "Guardar";
        btnGuardar.Click += btnGuardar_Click;

        // btnEliminar
        btnEliminar.Location = new Point(235, 355);
        btnEliminar.Size = new Size(100, 30);
        btnEliminar.Text = "Eliminar";
        btnEliminar.Click += btnEliminar_Click;

        // btnCerrar
        btnCerrar.Location = new Point(515, 355);
        btnCerrar.Size = new Size(100, 30);
        btnCerrar.Text = "Cerrar";
        btnCerrar.Click += btnCerrar_Click;

        // FormProductos
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(632, 405);
        Controls.Add(txtBuscar);
        Controls.Add(btnBuscar);
        Controls.Add(dgvProductos);
        Controls.Add(lblCodigo);
        Controls.Add(txtCodigo);
        Controls.Add(lblNombre);
        Controls.Add(txtNombre);
        Controls.Add(lblPrecio);
        Controls.Add(txtPrecio);
        Controls.Add(chkActivo);
        Controls.Add(btnNuevo);
        Controls.Add(btnGuardar);
        Controls.Add(btnEliminar);
        Controls.Add(btnCerrar);
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;
        MinimizeBox = false;
        StartPosition = FormStartPosition.CenterParent;
        Text = "Productos";
        ((System.ComponentModel.ISupportInitialize)dgvProductos).EndInit();
        ResumeLayout(false);
        PerformLayout();
    }
}
