namespace MiPrimerProyectoC_
{
    partial class Materias
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.grbBusquedaMaterias = new System.Windows.Forms.GroupBox();
            this.cboBuscarMaterias = new System.Windows.Forms.ComboBox();
            this.grdMaterias = new System.Windows.Forms.DataGridView();
            this.id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Codigo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nombre = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.uv = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txtBuscarMateria = new System.Windows.Forms.TextBox();
            this.grbEdicionMaterias = new System.Windows.Forms.GroupBox();
            this.btnModificarMateria = new System.Windows.Forms.Button();
            this.btnEliminarMateria = new System.Windows.Forms.Button();
            this.btnAgregarMateria = new System.Windows.Forms.Button();
            this.grbNavegacionMateria = new System.Windows.Forms.GroupBox();
            this.lblnRegistrosAlumno = new System.Windows.Forms.Label();
            this.btnUltimo = new System.Windows.Forms.Button();
            this.btnAnterior = new System.Windows.Forms.Button();
            this.btnSiguiente = new System.Windows.Forms.Button();
            this.btnPrimero = new System.Windows.Forms.Button();
            this.grbDatosMateria = new System.Windows.Forms.GroupBox();
            this.idMateria = new System.Windows.Forms.Label();
            this.lblIDalumno = new System.Windows.Forms.Label();
            this.txtUVMateria = new System.Windows.Forms.TextBox();
            this.lblDireccionalumno = new System.Windows.Forms.Label();
            this.txtNombreMateria = new System.Windows.Forms.TextBox();
            this.lblNombrealumno = new System.Windows.Forms.Label();
            this.txtCodigoMateria = new System.Windows.Forms.TextBox();
            this.lblCodigoalumno = new System.Windows.Forms.Label();
            this.grbBusquedaMaterias.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdMaterias)).BeginInit();
            this.grbEdicionMaterias.SuspendLayout();
            this.grbNavegacionMateria.SuspendLayout();
            this.grbDatosMateria.SuspendLayout();
            this.SuspendLayout();
            // 
            // grbBusquedaMaterias
            // 
            this.grbBusquedaMaterias.Controls.Add(this.cboBuscarMaterias);
            this.grbBusquedaMaterias.Controls.Add(this.grdMaterias);
            this.grbBusquedaMaterias.Controls.Add(this.txtBuscarMateria);
            this.grbBusquedaMaterias.Location = new System.Drawing.Point(357, 12);
            this.grbBusquedaMaterias.Name = "grbBusquedaMaterias";
            this.grbBusquedaMaterias.Size = new System.Drawing.Size(622, 308);
            this.grbBusquedaMaterias.TabIndex = 7;
            this.grbBusquedaMaterias.TabStop = false;
            this.grbBusquedaMaterias.Text = "Busqueda Materias.";
            // 
            // cboBuscarMaterias
            // 
            this.cboBuscarMaterias.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboBuscarMaterias.FormattingEnabled = true;
            this.cboBuscarMaterias.Items.AddRange(new object[] {
            "Codigo",
            "Nombre"});
            this.cboBuscarMaterias.Location = new System.Drawing.Point(7, 22);
            this.cboBuscarMaterias.Name = "cboBuscarMaterias";
            this.cboBuscarMaterias.Size = new System.Drawing.Size(193, 24);
            this.cboBuscarMaterias.TabIndex = 2;
            // 
            // grdMaterias
            // 
            this.grdMaterias.AllowUserToAddRows = false;
            this.grdMaterias.AllowUserToDeleteRows = false;
            this.grdMaterias.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdMaterias.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.id,
            this.Codigo,
            this.nombre,
            this.uv});
            this.grdMaterias.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnF2;
            this.grdMaterias.Location = new System.Drawing.Point(7, 56);
            this.grdMaterias.Name = "grdMaterias";
            this.grdMaterias.ReadOnly = true;
            this.grdMaterias.RowHeadersWidth = 51;
            this.grdMaterias.RowTemplate.Height = 24;
            this.grdMaterias.Size = new System.Drawing.Size(609, 240);
            this.grdMaterias.TabIndex = 1;
            this.grdMaterias.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.grdMaterias_CellClick);
            // 
            // id
            // 
            this.id.DataPropertyName = "IdMaterias";
            this.id.HeaderText = "ID";
            this.id.MinimumWidth = 6;
            this.id.Name = "id";
            this.id.ReadOnly = true;
            this.id.Visible = false;
            this.id.Width = 125;
            // 
            // Codigo
            // 
            this.Codigo.DataPropertyName = "codigo";
            this.Codigo.HeaderText = "Codigo";
            this.Codigo.MinimumWidth = 6;
            this.Codigo.Name = "Codigo";
            this.Codigo.ReadOnly = true;
            this.Codigo.Width = 90;
            // 
            // nombre
            // 
            this.nombre.DataPropertyName = "nombre";
            this.nombre.HeaderText = "Nombre";
            this.nombre.MinimumWidth = 6;
            this.nombre.Name = "nombre";
            this.nombre.ReadOnly = true;
            this.nombre.Width = 150;
            // 
            // uv
            // 
            this.uv.DataPropertyName = "uv";
            this.uv.HeaderText = "UV";
            this.uv.MinimumWidth = 6;
            this.uv.Name = "uv";
            this.uv.ReadOnly = true;
            this.uv.Width = 120;
            // 
            // txtBuscarMateria
            // 
            this.txtBuscarMateria.Location = new System.Drawing.Point(206, 21);
            this.txtBuscarMateria.Name = "txtBuscarMateria";
            this.txtBuscarMateria.Size = new System.Drawing.Size(410, 22);
            this.txtBuscarMateria.TabIndex = 0;
            this.txtBuscarMateria.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtBuscarAlumnos_KeyUp);
            // 
            // grbEdicionMaterias
            // 
            this.grbEdicionMaterias.Controls.Add(this.btnModificarMateria);
            this.grbEdicionMaterias.Controls.Add(this.btnEliminarMateria);
            this.grbEdicionMaterias.Controls.Add(this.btnAgregarMateria);
            this.grbEdicionMaterias.Location = new System.Drawing.Point(462, 344);
            this.grbEdicionMaterias.Name = "grbEdicionMaterias";
            this.grbEdicionMaterias.Size = new System.Drawing.Size(243, 88);
            this.grbEdicionMaterias.TabIndex = 6;
            this.grbEdicionMaterias.TabStop = false;
            this.grbEdicionMaterias.Text = "Edición";
            // 
            // btnModificarMateria
            // 
            this.btnModificarMateria.Location = new System.Drawing.Point(73, 41);
            this.btnModificarMateria.Name = "btnModificarMateria";
            this.btnModificarMateria.Size = new System.Drawing.Size(78, 32);
            this.btnModificarMateria.TabIndex = 2;
            this.btnModificarMateria.Text = "Modificar";
            this.btnModificarMateria.UseVisualStyleBackColor = true;
            this.btnModificarMateria.Click += new System.EventHandler(this.btnModificarMateria_Click);
            // 
            // btnEliminarMateria
            // 
            this.btnEliminarMateria.Location = new System.Drawing.Point(157, 41);
            this.btnEliminarMateria.Name = "btnEliminarMateria";
            this.btnEliminarMateria.Size = new System.Drawing.Size(68, 32);
            this.btnEliminarMateria.TabIndex = 1;
            this.btnEliminarMateria.Text = "Eliminar";
            this.btnEliminarMateria.UseVisualStyleBackColor = true;
            this.btnEliminarMateria.Click += new System.EventHandler(this.btnEliminarMateria_Click);
            // 
            // btnAgregarMateria
            // 
            this.btnAgregarMateria.Location = new System.Drawing.Point(6, 41);
            this.btnAgregarMateria.Name = "btnAgregarMateria";
            this.btnAgregarMateria.Size = new System.Drawing.Size(61, 32);
            this.btnAgregarMateria.TabIndex = 0;
            this.btnAgregarMateria.Text = "Nuevo";
            this.btnAgregarMateria.UseVisualStyleBackColor = true;
            this.btnAgregarMateria.Click += new System.EventHandler(this.btnAgregarMateria_Click);
            // 
            // grbNavegacionMateria
            // 
            this.grbNavegacionMateria.Controls.Add(this.lblnRegistrosAlumno);
            this.grbNavegacionMateria.Controls.Add(this.btnUltimo);
            this.grbNavegacionMateria.Controls.Add(this.btnAnterior);
            this.grbNavegacionMateria.Controls.Add(this.btnSiguiente);
            this.grbNavegacionMateria.Controls.Add(this.btnPrimero);
            this.grbNavegacionMateria.Location = new System.Drawing.Point(119, 344);
            this.grbNavegacionMateria.Name = "grbNavegacionMateria";
            this.grbNavegacionMateria.Size = new System.Drawing.Size(302, 88);
            this.grbNavegacionMateria.TabIndex = 5;
            this.grbNavegacionMateria.TabStop = false;
            this.grbNavegacionMateria.Text = "Navegación";
            // 
            // lblnRegistrosAlumno
            // 
            this.lblnRegistrosAlumno.AutoSize = true;
            this.lblnRegistrosAlumno.Location = new System.Drawing.Point(112, 49);
            this.lblnRegistrosAlumno.Name = "lblnRegistrosAlumno";
            this.lblnRegistrosAlumno.Size = new System.Drawing.Size(42, 16);
            this.lblnRegistrosAlumno.TabIndex = 10;
            this.lblnRegistrosAlumno.Text = "x de n";
            // 
            // btnUltimo
            // 
            this.btnUltimo.Location = new System.Drawing.Point(220, 41);
            this.btnUltimo.Name = "btnUltimo";
            this.btnUltimo.Size = new System.Drawing.Size(38, 32);
            this.btnUltimo.TabIndex = 3;
            this.btnUltimo.Text = ">|";
            this.btnUltimo.UseVisualStyleBackColor = true;
            this.btnUltimo.Click += new System.EventHandler(this.btnUltimo_Click);
            // 
            // btnAnterior
            // 
            this.btnAnterior.Location = new System.Drawing.Point(50, 41);
            this.btnAnterior.Name = "btnAnterior";
            this.btnAnterior.Size = new System.Drawing.Size(38, 32);
            this.btnAnterior.TabIndex = 2;
            this.btnAnterior.Text = "<";
            this.btnAnterior.UseVisualStyleBackColor = true;
            this.btnAnterior.Click += new System.EventHandler(this.btnAnterior_Click);
            // 
            // btnSiguiente
            // 
            this.btnSiguiente.Location = new System.Drawing.Point(176, 41);
            this.btnSiguiente.Name = "btnSiguiente";
            this.btnSiguiente.Size = new System.Drawing.Size(38, 32);
            this.btnSiguiente.TabIndex = 1;
            this.btnSiguiente.Text = ">";
            this.btnSiguiente.UseVisualStyleBackColor = true;
            this.btnSiguiente.Click += new System.EventHandler(this.btnSiguiente_Click);
            // 
            // btnPrimero
            // 
            this.btnPrimero.Location = new System.Drawing.Point(6, 41);
            this.btnPrimero.Name = "btnPrimero";
            this.btnPrimero.Size = new System.Drawing.Size(38, 32);
            this.btnPrimero.TabIndex = 0;
            this.btnPrimero.Text = "|<";
            this.btnPrimero.UseVisualStyleBackColor = true;
            this.btnPrimero.Click += new System.EventHandler(this.btnPrimero_Click);
            // 
            // grbDatosMateria
            // 
            this.grbDatosMateria.Controls.Add(this.idMateria);
            this.grbDatosMateria.Controls.Add(this.lblIDalumno);
            this.grbDatosMateria.Controls.Add(this.txtUVMateria);
            this.grbDatosMateria.Controls.Add(this.lblDireccionalumno);
            this.grbDatosMateria.Controls.Add(this.txtNombreMateria);
            this.grbDatosMateria.Controls.Add(this.lblNombrealumno);
            this.grbDatosMateria.Controls.Add(this.txtCodigoMateria);
            this.grbDatosMateria.Controls.Add(this.lblCodigoalumno);
            this.grbDatosMateria.Enabled = false;
            this.grbDatosMateria.Location = new System.Drawing.Point(15, 82);
            this.grbDatosMateria.Name = "grbDatosMateria";
            this.grbDatosMateria.Size = new System.Drawing.Size(336, 209);
            this.grbDatosMateria.TabIndex = 4;
            this.grbDatosMateria.TabStop = false;
            this.grbDatosMateria.Text = "Datos";
            // 
            // idMateria
            // 
            this.idMateria.AutoSize = true;
            this.idMateria.Location = new System.Drawing.Point(104, 29);
            this.idMateria.Name = "idMateria";
            this.idMateria.Size = new System.Drawing.Size(20, 16);
            this.idMateria.TabIndex = 9;
            this.idMateria.Text = "ID";
            // 
            // lblIDalumno
            // 
            this.lblIDalumno.AutoSize = true;
            this.lblIDalumno.Location = new System.Drawing.Point(75, 29);
            this.lblIDalumno.Name = "lblIDalumno";
            this.lblIDalumno.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lblIDalumno.Size = new System.Drawing.Size(23, 16);
            this.lblIDalumno.TabIndex = 8;
            this.lblIDalumno.Text = "ID:";
            // 
            // txtUVMateria
            // 
            this.txtUVMateria.Location = new System.Drawing.Point(104, 129);
            this.txtUVMateria.Name = "txtUVMateria";
            this.txtUVMateria.Size = new System.Drawing.Size(174, 22);
            this.txtUVMateria.TabIndex = 5;
            // 
            // lblDireccionalumno
            // 
            this.lblDireccionalumno.AutoSize = true;
            this.lblDireccionalumno.Location = new System.Drawing.Point(69, 135);
            this.lblDireccionalumno.Name = "lblDireccionalumno";
            this.lblDireccionalumno.Size = new System.Drawing.Size(29, 16);
            this.lblDireccionalumno.TabIndex = 4;
            this.lblDireccionalumno.Text = "UV:";
            // 
            // txtNombreMateria
            // 
            this.txtNombreMateria.Location = new System.Drawing.Point(104, 89);
            this.txtNombreMateria.Name = "txtNombreMateria";
            this.txtNombreMateria.Size = new System.Drawing.Size(174, 22);
            this.txtNombreMateria.TabIndex = 3;
            // 
            // lblNombrealumno
            // 
            this.lblNombrealumno.AutoSize = true;
            this.lblNombrealumno.Location = new System.Drawing.Point(39, 95);
            this.lblNombrealumno.Name = "lblNombrealumno";
            this.lblNombrealumno.Size = new System.Drawing.Size(59, 16);
            this.lblNombrealumno.TabIndex = 2;
            this.lblNombrealumno.Text = "Nombre:";
            // 
            // txtCodigoMateria
            // 
            this.txtCodigoMateria.Location = new System.Drawing.Point(104, 50);
            this.txtCodigoMateria.Name = "txtCodigoMateria";
            this.txtCodigoMateria.Size = new System.Drawing.Size(174, 22);
            this.txtCodigoMateria.TabIndex = 1;
            // 
            // lblCodigoalumno
            // 
            this.lblCodigoalumno.AutoSize = true;
            this.lblCodigoalumno.Location = new System.Drawing.Point(44, 56);
            this.lblCodigoalumno.Name = "lblCodigoalumno";
            this.lblCodigoalumno.Size = new System.Drawing.Size(54, 16);
            this.lblCodigoalumno.TabIndex = 0;
            this.lblCodigoalumno.Text = "Codigo:";
            // 
            // Materias
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(992, 521);
            this.Controls.Add(this.grbBusquedaMaterias);
            this.Controls.Add(this.grbEdicionMaterias);
            this.Controls.Add(this.grbNavegacionMateria);
            this.Controls.Add(this.grbDatosMateria);
            this.Name = "Materias";
            this.Text = "Materias";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Materias_Load);
            this.grbBusquedaMaterias.ResumeLayout(false);
            this.grbBusquedaMaterias.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdMaterias)).EndInit();
            this.grbEdicionMaterias.ResumeLayout(false);
            this.grbNavegacionMateria.ResumeLayout(false);
            this.grbNavegacionMateria.PerformLayout();
            this.grbDatosMateria.ResumeLayout(false);
            this.grbDatosMateria.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grbBusquedaMaterias;
        private System.Windows.Forms.DataGridView grdMaterias;
        private System.Windows.Forms.TextBox txtBuscarMateria;
        private System.Windows.Forms.GroupBox grbEdicionMaterias;
        private System.Windows.Forms.Button btnModificarMateria;
        private System.Windows.Forms.Button btnEliminarMateria;
        private System.Windows.Forms.Button btnAgregarMateria;
        private System.Windows.Forms.GroupBox grbNavegacionMateria;
        private System.Windows.Forms.Label lblnRegistrosAlumno;
        private System.Windows.Forms.Button btnUltimo;
        private System.Windows.Forms.Button btnAnterior;
        private System.Windows.Forms.Button btnSiguiente;
        private System.Windows.Forms.Button btnPrimero;
        private System.Windows.Forms.GroupBox grbDatosMateria;
        private System.Windows.Forms.Label idMateria;
        private System.Windows.Forms.Label lblIDalumno;
        private System.Windows.Forms.TextBox txtUVMateria;
        private System.Windows.Forms.Label lblDireccionalumno;
        private System.Windows.Forms.TextBox txtNombreMateria;
        private System.Windows.Forms.Label lblNombrealumno;
        private System.Windows.Forms.TextBox txtCodigoMateria;
        private System.Windows.Forms.Label lblCodigoalumno;
        private System.Windows.Forms.DataGridViewTextBoxColumn id;
        private System.Windows.Forms.DataGridViewTextBoxColumn Codigo;
        private System.Windows.Forms.DataGridViewTextBoxColumn nombre;
        private System.Windows.Forms.DataGridViewTextBoxColumn uv;
        private System.Windows.Forms.ComboBox cboBuscarMaterias;
    }
}