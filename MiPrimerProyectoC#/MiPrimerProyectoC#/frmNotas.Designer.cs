namespace MiPrimerProyectoC_
{
    partial class frmNotas
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
            System.Windows.Forms.Label lblidNota;
            System.Windows.Forms.Label lblMateria;
            System.Windows.Forms.Label lblPeriodo;
            System.Windows.Forms.Label lblFecha;
            this.btnGuardar = new System.Windows.Forms.Button();
            this.dnotasDataGridView = new System.Windows.Forms.DataGridView();
            this.idDetalle = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.alumno = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nota_final = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.idNotaLabel1 = new System.Windows.Forms.Label();
            this.cboMateria = new System.Windows.Forms.ComboBox();
            this.cboPeriodo = new System.Windows.Forms.ComboBox();
            this.fechaDateTimePicker = new System.Windows.Forms.DateTimePicker();
            lblidNota = new System.Windows.Forms.Label();
            lblMateria = new System.Windows.Forms.Label();
            lblPeriodo = new System.Windows.Forms.Label();
            lblFecha = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dnotasDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // btnGuardar
            // 
            this.btnGuardar.Location = new System.Drawing.Point(899, 149);
            this.btnGuardar.Margin = new System.Windows.Forms.Padding(4);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(100, 28);
            this.btnGuardar.TabIndex = 19;
            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.UseVisualStyleBackColor = true;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // dnotasDataGridView
            // 
            this.dnotasDataGridView.AllowUserToAddRows = false;
            this.dnotasDataGridView.AllowUserToDeleteRows = false;
            this.dnotasDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dnotasDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.idDetalle,
            this.alumno,
            this.nota_final});
            this.dnotasDataGridView.Location = new System.Drawing.Point(13, 149);
            this.dnotasDataGridView.Margin = new System.Windows.Forms.Padding(4);
            this.dnotasDataGridView.Name = "dnotasDataGridView";
            this.dnotasDataGridView.RowHeadersWidth = 51;
            this.dnotasDataGridView.Size = new System.Drawing.Size(877, 337);
            this.dnotasDataGridView.TabIndex = 18;
            // 
            // idDetalle
            // 
            this.idDetalle.DataPropertyName = "idDetalle";
            this.idDetalle.HeaderText = "ID";
            this.idDetalle.MinimumWidth = 6;
            this.idDetalle.Name = "idDetalle";
            this.idDetalle.ReadOnly = true;
            this.idDetalle.Visible = false;
            this.idDetalle.Width = 50;
            // 
            // alumno
            // 
            this.alumno.DataPropertyName = "nombre";
            this.alumno.HeaderText = "ALUMNO";
            this.alumno.MinimumWidth = 6;
            this.alumno.Name = "alumno";
            this.alumno.ReadOnly = true;
            this.alumno.Width = 200;
            // 
            // nota_final
            // 
            this.nota_final.DataPropertyName = "nf";
            this.nota_final.HeaderText = "NF";
            this.nota_final.MinimumWidth = 6;
            this.nota_final.Name = "nota_final";
            this.nota_final.ReadOnly = true;
            this.nota_final.Width = 125;
            // 
            // lblidNota
            // 
            lblidNota.AutoSize = true;
            lblidNota.Location = new System.Drawing.Point(9, 22);
            lblidNota.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            lblidNota.Name = "lblidNota";
            lblidNota.Size = new System.Drawing.Size(23, 16);
            lblidNota.TabIndex = 10;
            lblidNota.Text = "ID:";
            // 
            // idNotaLabel1
            // 
            this.idNotaLabel1.Location = new System.Drawing.Point(93, 22);
            this.idNotaLabel1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.idNotaLabel1.Name = "idNotaLabel1";
            this.idNotaLabel1.Size = new System.Drawing.Size(267, 28);
            this.idNotaLabel1.TabIndex = 11;
            this.idNotaLabel1.Text = "label1";
            // 
            // lblMateria
            // 
            lblMateria.AutoSize = true;
            lblMateria.Location = new System.Drawing.Point(9, 58);
            lblMateria.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            lblMateria.Name = "lblMateria";
            lblMateria.Size = new System.Drawing.Size(55, 16);
            lblMateria.TabIndex = 12;
            lblMateria.Text = "Materia:";
            // 
            // cboMateria
            // 
            this.cboMateria.DisplayMember = "nombre";
            this.cboMateria.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboMateria.FormattingEnabled = true;
            this.cboMateria.Location = new System.Drawing.Point(93, 54);
            this.cboMateria.Margin = new System.Windows.Forms.Padding(4);
            this.cboMateria.Name = "cboMateria";
            this.cboMateria.Size = new System.Drawing.Size(341, 24);
            this.cboMateria.TabIndex = 13;
            this.cboMateria.ValueMember = "idMateria";
            // 
            // lblPeriodo
            // 
            lblPeriodo.AutoSize = true;
            lblPeriodo.Location = new System.Drawing.Point(527, 58);
            lblPeriodo.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            lblPeriodo.Name = "lblPeriodo";
            lblPeriodo.Size = new System.Drawing.Size(71, 16);
            lblPeriodo.TabIndex = 14;
            lblPeriodo.Text = "PERIODO:";
            // 
            // cboPeriodo
            // 
            this.cboPeriodo.DisplayMember = "periodo";
            this.cboPeriodo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboPeriodo.FormattingEnabled = true;
            this.cboPeriodo.Location = new System.Drawing.Point(611, 54);
            this.cboPeriodo.Margin = new System.Windows.Forms.Padding(4);
            this.cboPeriodo.Name = "cboPeriodo";
            this.cboPeriodo.Size = new System.Drawing.Size(265, 24);
            this.cboPeriodo.TabIndex = 15;
            this.cboPeriodo.ValueMember = "idPeriodo";
            // 
            // lblFecha
            // 
            lblFecha.AutoSize = true;
            lblFecha.Location = new System.Drawing.Point(9, 106);
            lblFecha.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            lblFecha.Name = "lblFecha";
            lblFecha.Size = new System.Drawing.Size(55, 16);
            lblFecha.TabIndex = 16;
            lblFecha.Text = "FECHA:";
            // 
            // fechaDateTimePicker
            // 
            this.fechaDateTimePicker.Location = new System.Drawing.Point(93, 101);
            this.fechaDateTimePicker.Margin = new System.Windows.Forms.Padding(4);
            this.fechaDateTimePicker.Name = "fechaDateTimePicker";
            this.fechaDateTimePicker.Size = new System.Drawing.Size(341, 22);
            this.fechaDateTimePicker.TabIndex = 17;
            // 
            // frmNotas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1016, 530);
            this.Controls.Add(this.btnGuardar);
            this.Controls.Add(this.dnotasDataGridView);
            this.Controls.Add(lblidNota);
            this.Controls.Add(this.idNotaLabel1);
            this.Controls.Add(lblMateria);
            this.Controls.Add(this.cboMateria);
            this.Controls.Add(lblPeriodo);
            this.Controls.Add(this.cboPeriodo);
            this.Controls.Add(lblFecha);
            this.Controls.Add(this.fechaDateTimePicker);
            this.Name = "frmNotas";
            this.Text = "frmNotas";
            this.Load += new System.EventHandler(this.frmNotas_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dnotasDataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.DataGridView dnotasDataGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn idDetalle;
        private System.Windows.Forms.DataGridViewTextBoxColumn alumno;
        private System.Windows.Forms.DataGridViewTextBoxColumn nota_final;
        private System.Windows.Forms.Label idNotaLabel1;
        private System.Windows.Forms.ComboBox cboMateria;
        private System.Windows.Forms.ComboBox cboPeriodo;
        private System.Windows.Forms.DateTimePicker fechaDateTimePicker;
    }
}