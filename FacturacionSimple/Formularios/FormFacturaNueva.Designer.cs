namespace FacturacionSimple.Formularios;

partial class FormFacturaNueva
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

    private Label lblFecha;
    private DateTimePicker dtpFecha;
    private Label lblClienteNombre;
    private TextBox txtClienteNombre;
    private Label lblClienteDocumento;
    private TextBox txtClienteDocumento;

    private Label lblProducto;
    private ComboBox cmbProducto;
    private Label lblCantidad;
    private NumericUpDown numCantidad;
    private Button btnAgregarLinea;
    private Button btnQuitarLinea;

    private DataGridView dgvDetalle;
    private Label lblTotal;

    private Button btnGrabar;
    private Button btnCancelar;

    private void InitializeComponent()
    {
        lblFecha = new Label();
        dtpFecha = new DateTimePicker();
        lblClienteNombre = new Label();
        txtClienteNombre = new TextBox();
        lblClienteDocumento = new Label();
        txtClienteDocumento = new TextBox();
        lblProducto = new Label();
        cmbProducto = new ComboBox();
        lblCantidad = new Label();
        numCantidad = new NumericUpDown();
        btnAgregarLinea = new Button();
        btnQuitarLinea = new Button();
        dgvDetalle = new DataGridView();
        lblTotal = new Label();
        btnGrabar = new Button();
        btnCancelar = new Button();
        ((System.ComponentModel.ISupportInitialize)numCantidad).BeginInit();
        ((System.ComponentModel.ISupportInitialize)dgvDetalle).BeginInit();
        SuspendLayout();
        // 
        // lblFecha
        // 
        lblFecha.Location = new Point(21, 25);
        lblFecha.Margin = new Padding(4, 0, 4, 0);
        lblFecha.Name = "lblFecha";
        lblFecha.Size = new Size(86, 38);
        lblFecha.TabIndex = 0;
        lblFecha.Text = "Fecha:";
        // 
        // dtpFecha
        // 
        dtpFecha.Format = DateTimePickerFormat.Short;
        dtpFecha.Location = new Point(114, 20);
        dtpFecha.Margin = new Padding(4, 5, 4, 5);
        dtpFecha.Name = "dtpFecha";
        dtpFecha.Size = new Size(184, 31);
        dtpFecha.TabIndex = 1;
        // 
        // lblClienteNombre
        // 
        lblClienteNombre.Location = new Point(329, 25);
        lblClienteNombre.Margin = new Padding(4, 0, 4, 0);
        lblClienteNombre.Name = "lblClienteNombre";
        lblClienteNombre.Size = new Size(157, 38);
        lblClienteNombre.TabIndex = 2;
        lblClienteNombre.Text = "Cliente (nombre):";
        // 
        // txtClienteNombre
        // 
        txtClienteNombre.Location = new Point(493, 20);
        txtClienteNombre.Margin = new Padding(4, 5, 4, 5);
        txtClienteNombre.Name = "txtClienteNombre";
        txtClienteNombre.Size = new Size(327, 31);
        txtClienteNombre.TabIndex = 3;
        // 
        // lblClienteDocumento
        // 
        lblClienteDocumento.Location = new Point(21, 75);
        lblClienteDocumento.Margin = new Padding(4, 0, 4, 0);
        lblClienteDocumento.Name = "lblClienteDocumento";
        lblClienteDocumento.Size = new Size(157, 38);
        lblClienteDocumento.TabIndex = 4;
        lblClienteDocumento.Text = "Documento:";
        // 
        // txtClienteDocumento
        // 
        txtClienteDocumento.Location = new Point(186, 70);
        txtClienteDocumento.Margin = new Padding(4, 5, 4, 5);
        txtClienteDocumento.Name = "txtClienteDocumento";
        txtClienteDocumento.Size = new Size(213, 31);
        txtClienteDocumento.TabIndex = 5;
        // 
        // lblProducto
        // 
        lblProducto.Location = new Point(21, 142);
        lblProducto.Margin = new Padding(4, 0, 4, 0);
        lblProducto.Name = "lblProducto";
        lblProducto.Size = new Size(93, 38);
        lblProducto.TabIndex = 6;
        lblProducto.Text = "Producto:";
        // 
        // cmbProducto
        // 
        cmbProducto.DropDownStyle = ComboBoxStyle.DropDownList;
        cmbProducto.Location = new Point(121, 137);
        cmbProducto.Margin = new Padding(4, 5, 4, 5);
        cmbProducto.Name = "cmbProducto";
        cmbProducto.Size = new Size(455, 33);
        cmbProducto.TabIndex = 7;
        // 
        // lblCantidad
        // 
        lblCantidad.Location = new Point(600, 142);
        lblCantidad.Margin = new Padding(4, 0, 4, 0);
        lblCantidad.Name = "lblCantidad";
        lblCantidad.Size = new Size(93, 38);
        lblCantidad.TabIndex = 8;
        lblCantidad.Text = "Cantidad:";
        // 
        // numCantidad
        // 
        numCantidad.Location = new Point(693, 137);
        numCantidad.Margin = new Padding(4, 5, 4, 5);
        numCantidad.Maximum = new decimal(new int[] { 100000, 0, 0, 0 });
        numCantidad.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
        numCantidad.Name = "numCantidad";
        numCantidad.Size = new Size(86, 31);
        numCantidad.TabIndex = 9;
        numCantidad.Value = new decimal(new int[] { 1, 0, 0, 0 });
        // 
        // btnAgregarLinea
        // 
        btnAgregarLinea.Location = new Point(793, 135);
        btnAgregarLinea.Margin = new Padding(4, 5, 4, 5);
        btnAgregarLinea.Name = "btnAgregarLinea";
        btnAgregarLinea.Size = new Size(143, 42);
        btnAgregarLinea.TabIndex = 10;
        btnAgregarLinea.Text = "Agregar línea";
        btnAgregarLinea.Click += btnAgregarLinea_Click;
        // 
        // btnQuitarLinea
        // 
        btnQuitarLinea.Location = new Point(21, 525);
        btnQuitarLinea.Margin = new Padding(4, 5, 4, 5);
        btnQuitarLinea.Name = "btnQuitarLinea";
        btnQuitarLinea.Size = new Size(171, 47);
        btnQuitarLinea.TabIndex = 12;
        btnQuitarLinea.Text = "Quitar línea";
        btnQuitarLinea.Click += btnQuitarLinea_Click;
        // 
        // dgvDetalle
        // 
        dgvDetalle.AllowUserToAddRows = false;
        dgvDetalle.AllowUserToDeleteRows = false;
        dgvDetalle.ColumnHeadersHeight = 34;
        dgvDetalle.Location = new Point(21, 197);
        dgvDetalle.Margin = new Padding(4, 5, 4, 5);
        dgvDetalle.MultiSelect = false;
        dgvDetalle.Name = "dgvDetalle";
        dgvDetalle.ReadOnly = true;
        dgvDetalle.RowHeadersVisible = false;
        dgvDetalle.RowHeadersWidth = 62;
        dgvDetalle.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        dgvDetalle.Size = new Size(914, 317);
        dgvDetalle.TabIndex = 11;
        // 
        // lblTotal
        // 
        lblTotal.Font = new Font("Segoe UI", 11F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
        lblTotal.Location = new Point(614, 525);
        lblTotal.Margin = new Padding(4, 0, 4, 0);
        lblTotal.Name = "lblTotal";
        lblTotal.Size = new Size(321, 47);
        lblTotal.TabIndex = 13;
        lblTotal.Text = "Total: $ 0,00";
        lblTotal.TextAlign = ContentAlignment.MiddleRight;
        // 
        // btnGrabar
        // 
        btnGrabar.Location = new Point(664, 592);
        btnGrabar.Margin = new Padding(4, 5, 4, 5);
        btnGrabar.Name = "btnGrabar";
        btnGrabar.Size = new Size(129, 50);
        btnGrabar.TabIndex = 14;
        btnGrabar.Text = "Grabar";
        btnGrabar.Click += btnGrabar_Click;
        // 
        // btnCancelar
        // 
        btnCancelar.Location = new Point(807, 592);
        btnCancelar.Margin = new Padding(4, 5, 4, 5);
        btnCancelar.Name = "btnCancelar";
        btnCancelar.Size = new Size(129, 50);
        btnCancelar.TabIndex = 15;
        btnCancelar.Text = "Cancelar";
        btnCancelar.Click += btnCancelar_Click;
        // 
        // FormFacturaNueva
        // 
        AutoScaleDimensions = new SizeF(10F, 25F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(960, 667);
        Controls.Add(lblFecha);
        Controls.Add(dtpFecha);
        Controls.Add(lblClienteNombre);
        Controls.Add(txtClienteNombre);
        Controls.Add(lblClienteDocumento);
        Controls.Add(txtClienteDocumento);
        Controls.Add(lblProducto);
        Controls.Add(cmbProducto);
        Controls.Add(lblCantidad);
        Controls.Add(numCantidad);
        Controls.Add(btnAgregarLinea);
        Controls.Add(dgvDetalle);
        Controls.Add(btnQuitarLinea);
        Controls.Add(lblTotal);
        Controls.Add(btnGrabar);
        Controls.Add(btnCancelar);
        FormBorderStyle = FormBorderStyle.FixedDialog;
        Margin = new Padding(4, 5, 4, 5);
        MaximizeBox = false;
        MinimizeBox = false;
        Name = "FormFacturaNueva";
        StartPosition = FormStartPosition.CenterParent;
        Text = "Nueva factura";
        ((System.ComponentModel.ISupportInitialize)numCantidad).EndInit();
        ((System.ComponentModel.ISupportInitialize)dgvDetalle).EndInit();
        ResumeLayout(false);
        PerformLayout();
    }
}
