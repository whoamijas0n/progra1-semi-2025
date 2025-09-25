namespace MiPrimerProyectoC_
{
    partial class Docentes
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
            this.grbBusquedaDocentes = new System.Windows.Forms.GroupBox();
            this.grdDocentes = new System.Windows.Forms.DataGridView();
            this.txtBuscarDocentes = new System.Windows.Forms.TextBox();
            this.grbEdicionDocentes = new System.Windows.Forms.GroupBox();
            this.btnModificarDocente = new System.Windows.Forms.Button();
            this.btnEliminarDocente = new System.Windows.Forms.Button();
            this.btnAgregarDocente = new System.Windows.Forms.Button();
            this.grbNavegacionDocentes = new System.Windows.Forms.GroupBox();
            this.lblnRegistrosAlumno = new System.Windows.Forms.Label();
            this.btnUltimo = new System.Windows.Forms.Button();
            this.btnAnterior = new System.Windows.Forms.Button();
            this.btnSiguiente = new System.Windows.Forms.Button();
            this.btnPrimero = new System.Windows.Forms.Button();
            this.grbDatosDocente = new System.Windows.Forms.GroupBox();
            this.idDocente = new System.Windows.Forms.Label();
            this.lblIDalumno = new System.Windows.Forms.Label();
            this.txtTelefonoDocente = new System.Windows.Forms.TextBox();
            this.lblTelefonoalumno = new System.Windows.Forms.Label();
            this.txtDireccionDocente = new System.Windows.Forms.TextBox();
            this.lblDireccionalumno = new System.Windows.Forms.Label();
            this.txtNombreDocente = new System.Windows.Forms.TextBox();
            this.lblNombrealumno = new System.Windows.Forms.Label();
            this.txtCodigoDocente = new System.Windows.Forms.TextBox();
            this.lblCodigoalumno = new System.Windows.Forms.Label();
            this.IdDocentes = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Codigo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nombre = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.direccion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.telefono = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.grbBusquedaDocentes.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdDocentes)).BeginInit();
            this.grbEdicionDocentes.SuspendLayout();
            this.grbNavegacionDocentes.SuspendLayout();
            this.grbDatosDocente.SuspendLayout();
            this.SuspendLayout();
            // 
            // grbBusquedaDocentes
            // 
            this.grbBusquedaDocentes.Controls.Add(this.grdDocentes);
            this.grbBusquedaDocentes.Controls.Add(this.txtBuscarDocentes);
            this.grbBusquedaDocentes.Location = new System.Drawing.Point(429, 12);
            this.grbBusquedaDocentes.Name = "grbBusquedaDocentes";
            this.grbBusquedaDocentes.Size = new System.Drawing.Size(622, 308);
            this.grbBusquedaDocentes.TabIndex = 7;
            this.grbBusquedaDocentes.TabStop = false;
            this.grbBusquedaDocentes.Text = "Busqueda Docente.";
            // 
            // grdDocentes
            // 
            this.grdDocentes.AllowUserToAddRows = false;
            this.grdDocentes.AllowUserToDeleteRows = false;
            this.grdDocentes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdDocentes.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.IdDocentes,
            this.Codigo,
            this.nombre,
            this.direccion,
            this.telefono});
            this.grdDocentes.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnF2;
            this.grdDocentes.Location = new System.Drawing.Point(7, 56);
            this.grdDocentes.Name = "grdDocentes";
            this.grdDocentes.ReadOnly = true;
            this.grdDocentes.RowHeadersWidth = 51;
            this.grdDocentes.RowTemplate.Height = 24;
            this.grdDocentes.Size = new System.Drawing.Size(609, 240);
            this.grdDocentes.TabIndex = 1;
            this.grdDocentes.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.grdDocentes_CellClick);
            // 
            // txtBuscarDocentes
            // 
            this.txtBuscarDocentes.Location = new System.Drawing.Point(7, 19);
            this.txtBuscarDocentes.Name = "txtBuscarDocentes";
            this.txtBuscarDocentes.Size = new System.Drawing.Size(410, 22);
            this.txtBuscarDocentes.TabIndex = 0;
            this.txtBuscarDocentes.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtBuscarDocentes_KeyUp);
            // 
            // grbEdicionDocentes
            // 
            this.grbEdicionDocentes.Controls.Add(this.btnModificarDocente);
            this.grbEdicionDocentes.Controls.Add(this.btnEliminarDocente);
            this.grbEdicionDocentes.Controls.Add(this.btnAgregarDocente);
            this.grbEdicionDocentes.Location = new System.Drawing.Point(457, 344);
            this.grbEdicionDocentes.Name = "grbEdicionDocentes";
            this.grbEdicionDocentes.Size = new System.Drawing.Size(243, 88);
            this.grbEdicionDocentes.TabIndex = 6;
            this.grbEdicionDocentes.TabStop = false;
            this.grbEdicionDocentes.Text = "Edición";
            // 
            // btnModificarDocente
            // 
            this.btnModificarDocente.Location = new System.Drawing.Point(73, 41);
            this.btnModificarDocente.Name = "btnModificarDocente";
            this.btnModificarDocente.Size = new System.Drawing.Size(78, 32);
            this.btnModificarDocente.TabIndex = 2;
            this.btnModificarDocente.Text = "Modificar";
            this.btnModificarDocente.UseVisualStyleBackColor = true;
            this.btnModificarDocente.Click += new System.EventHandler(this.btnModificarDocente_Click);
            // 
            // btnEliminarDocente
            // 
            this.btnEliminarDocente.Location = new System.Drawing.Point(157, 41);
            this.btnEliminarDocente.Name = "btnEliminarDocente";
            this.btnEliminarDocente.Size = new System.Drawing.Size(68, 32);
            this.btnEliminarDocente.TabIndex = 1;
            this.btnEliminarDocente.Text = "Eliminar";
            this.btnEliminarDocente.UseVisualStyleBackColor = true;
            this.btnEliminarDocente.Click += new System.EventHandler(this.btnEliminarDocente_Click);
            // 
            // btnAgregarDocente
            // 
            this.btnAgregarDocente.Location = new System.Drawing.Point(6, 41);
            this.btnAgregarDocente.Name = "btnAgregarDocente";
            this.btnAgregarDocente.Size = new System.Drawing.Size(61, 32);
            this.btnAgregarDocente.TabIndex = 0;
            this.btnAgregarDocente.Text = "Nuevo";
            this.btnAgregarDocente.UseVisualStyleBackColor = true;
            this.btnAgregarDocente.Click += new System.EventHandler(this.btnAgregarDocente_Click);
            // 
            // grbNavegacionDocentes
            // 
            this.grbNavegacionDocentes.Controls.Add(this.lblnRegistrosAlumno);
            this.grbNavegacionDocentes.Controls.Add(this.btnUltimo);
            this.grbNavegacionDocentes.Controls.Add(this.btnAnterior);
            this.grbNavegacionDocentes.Controls.Add(this.btnSiguiente);
            this.grbNavegacionDocentes.Controls.Add(this.btnPrimero);
            this.grbNavegacionDocentes.Location = new System.Drawing.Point(114, 344);
            this.grbNavegacionDocentes.Name = "grbNavegacionDocentes";
            this.grbNavegacionDocentes.Size = new System.Drawing.Size(302, 88);
            this.grbNavegacionDocentes.TabIndex = 5;
            this.grbNavegacionDocentes.TabStop = false;
            this.grbNavegacionDocentes.Text = "Navegación";
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
            // grbDatosDocente
            // 
            this.grbDatosDocente.Controls.Add(this.idDocente);
            this.grbDatosDocente.Controls.Add(this.lblIDalumno);
            this.grbDatosDocente.Controls.Add(this.txtTelefonoDocente);
            this.grbDatosDocente.Controls.Add(this.lblTelefonoalumno);
            this.grbDatosDocente.Controls.Add(this.txtDireccionDocente);
            this.grbDatosDocente.Controls.Add(this.lblDireccionalumno);
            this.grbDatosDocente.Controls.Add(this.txtNombreDocente);
            this.grbDatosDocente.Controls.Add(this.lblNombrealumno);
            this.grbDatosDocente.Controls.Add(this.txtCodigoDocente);
            this.grbDatosDocente.Controls.Add(this.lblCodigoalumno);
            this.grbDatosDocente.Enabled = false;
            this.grbDatosDocente.Location = new System.Drawing.Point(10, 12);
            this.grbDatosDocente.Name = "grbDatosDocente";
            this.grbDatosDocente.Size = new System.Drawing.Size(336, 308);
            this.grbDatosDocente.TabIndex = 4;
            this.grbDatosDocente.TabStop = false;
            this.grbDatosDocente.Text = "Datos";
            // 
            // idDocente
            // 
            this.idDocente.AutoSize = true;
            this.idDocente.Location = new System.Drawing.Point(104, 29);
            this.idDocente.Name = "idDocente";
            this.idDocente.Size = new System.Drawing.Size(20, 16);
            this.idDocente.TabIndex = 9;
            this.idDocente.Text = "ID";
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
            // txtTelefonoDocente
            // 
            this.txtTelefonoDocente.Location = new System.Drawing.Point(104, 170);
            this.txtTelefonoDocente.Name = "txtTelefonoDocente";
            this.txtTelefonoDocente.Size = new System.Drawing.Size(174, 22);
            this.txtTelefonoDocente.TabIndex = 7;
            // 
            // lblTelefonoalumno
            // 
            this.lblTelefonoalumno.AutoSize = true;
            this.lblTelefonoalumno.Location = new System.Drawing.Point(34, 176);
            this.lblTelefonoalumno.Name = "lblTelefonoalumno";
            this.lblTelefonoalumno.Size = new System.Drawing.Size(64, 16);
            this.lblTelefonoalumno.TabIndex = 6;
            this.lblTelefonoalumno.Text = "Telefono:";
            // 
            // txtDireccionDocente
            // 
            this.txtDireccionDocente.Location = new System.Drawing.Point(104, 129);
            this.txtDireccionDocente.Name = "txtDireccionDocente";
            this.txtDireccionDocente.Size = new System.Drawing.Size(174, 22);
            this.txtDireccionDocente.TabIndex = 5;
            // 
            // lblDireccionalumno
            // 
            this.lblDireccionalumno.AutoSize = true;
            this.lblDireccionalumno.Location = new System.Drawing.Point(31, 135);
            this.lblDireccionalumno.Name = "lblDireccionalumno";
            this.lblDireccionalumno.Size = new System.Drawing.Size(67, 16);
            this.lblDireccionalumno.TabIndex = 4;
            this.lblDireccionalumno.Text = "Dirección:";
            // 
            // txtNombreDocente
            // 
            this.txtNombreDocente.Location = new System.Drawing.Point(104, 89);
            this.txtNombreDocente.Name = "txtNombreDocente";
            this.txtNombreDocente.Size = new System.Drawing.Size(174, 22);
            this.txtNombreDocente.TabIndex = 3;
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
            // txtCodigoDocente
            // 
            this.txtCodigoDocente.Location = new System.Drawing.Point(104, 50);
            this.txtCodigoDocente.Name = "txtCodigoDocente";
            this.txtCodigoDocente.Size = new System.Drawing.Size(174, 22);
            this.txtCodigoDocente.TabIndex = 1;
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
            // IdDocentes
            // 
            this.IdDocentes.DataPropertyName = "IdDocentes";
            this.IdDocentes.HeaderText = "ID";
            this.IdDocentes.MinimumWidth = 6;
            this.IdDocentes.Name = "IdDocentes";
            this.IdDocentes.ReadOnly = true;
            this.IdDocentes.Visible = false;
            this.IdDocentes.Width = 125;
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
            // direccion
            // 
            this.direccion.DataPropertyName = "direccion";
            this.direccion.HeaderText = "Dirección";
            this.direccion.MinimumWidth = 6;
            this.direccion.Name = "direccion";
            this.direccion.ReadOnly = true;
            this.direccion.Width = 120;
            // 
            // telefono
            // 
            this.telefono.DataPropertyName = "telefono";
            this.telefono.HeaderText = "Telefono";
            this.telefono.MinimumWidth = 6;
            this.telefono.Name = "telefono";
            this.telefono.ReadOnly = true;
            this.telefono.Width = 80;
            // 
            // Docentes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1058, 559);
            this.Controls.Add(this.grbBusquedaDocentes);
            this.Controls.Add(this.grbEdicionDocentes);
            this.Controls.Add(this.grbNavegacionDocentes);
            this.Controls.Add(this.grbDatosDocente);
            this.Name = "Docentes";
            this.Text = "Docentes";
            this.Load += new System.EventHandler(this.Docentes_Load);
            this.grbBusquedaDocentes.ResumeLayout(false);
            this.grbBusquedaDocentes.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdDocentes)).EndInit();
            this.grbEdicionDocentes.ResumeLayout(false);
            this.grbNavegacionDocentes.ResumeLayout(false);
            this.grbNavegacionDocentes.PerformLayout();
            this.grbDatosDocente.ResumeLayout(false);
            this.grbDatosDocente.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grbBusquedaDocentes;
        private System.Windows.Forms.DataGridView grdDocentes;
        private System.Windows.Forms.TextBox txtBuscarDocentes;
        private System.Windows.Forms.GroupBox grbEdicionDocentes;
        private System.Windows.Forms.Button btnModificarDocente;
        private System.Windows.Forms.Button btnEliminarDocente;
        private System.Windows.Forms.Button btnAgregarDocente;
        private System.Windows.Forms.GroupBox grbNavegacionDocentes;
        private System.Windows.Forms.Label lblnRegistrosAlumno;
        private System.Windows.Forms.Button btnUltimo;
        private System.Windows.Forms.Button btnAnterior;
        private System.Windows.Forms.Button btnSiguiente;
        private System.Windows.Forms.Button btnPrimero;
        private System.Windows.Forms.GroupBox grbDatosDocente;
        private System.Windows.Forms.Label idDocente;
        private System.Windows.Forms.Label lblIDalumno;
        private System.Windows.Forms.TextBox txtTelefonoDocente;
        private System.Windows.Forms.Label lblTelefonoalumno;
        private System.Windows.Forms.TextBox txtDireccionDocente;
        private System.Windows.Forms.Label lblDireccionalumno;
        private System.Windows.Forms.TextBox txtNombreDocente;
        private System.Windows.Forms.Label lblNombrealumno;
        private System.Windows.Forms.TextBox txtCodigoDocente;
        private System.Windows.Forms.Label lblCodigoalumno;
        private System.Windows.Forms.DataGridViewTextBoxColumn IdDocentes;
        private System.Windows.Forms.DataGridViewTextBoxColumn Codigo;
        private System.Windows.Forms.DataGridViewTextBoxColumn nombre;
        private System.Windows.Forms.DataGridViewTextBoxColumn direccion;
        private System.Windows.Forms.DataGridViewTextBoxColumn telefono;
    }
}