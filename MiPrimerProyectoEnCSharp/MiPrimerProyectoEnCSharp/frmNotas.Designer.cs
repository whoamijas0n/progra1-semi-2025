namespace MiPrimerProyectoEnCSharp
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.Label idAlumnoLabel;
            System.Windows.Forms.Label idNotaLabel;
            System.Windows.Forms.Label idPeriodoLabel;
            System.Windows.Forms.Label fechaLabel;
            System.Windows.Forms.Label periodoLabel;
            this.db_academicaDataSet1 = new MiPrimerProyectoEnCSharp.db_academicaDataSet1();
            this.materiasBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.materiasTableAdapter = new MiPrimerProyectoEnCSharp.db_academicaDataSet1TableAdapters.materiasTableAdapter();
            this.periodosBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.periodosTableAdapter = new MiPrimerProyectoEnCSharp.db_academicaDataSet1TableAdapters.periodosTableAdapter();
            this.notasBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.notasTableAdapter = new MiPrimerProyectoEnCSharp.db_academicaDataSet1TableAdapters.notasTableAdapter();
            this.tableAdapterManager = new MiPrimerProyectoEnCSharp.db_academicaDataSet1TableAdapters.TableAdapterManager();
            this.dnotasTableAdapter = new MiPrimerProyectoEnCSharp.db_academicaDataSet1TableAdapters.dnotasTableAdapter();
            this.dnotasBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.btnGuardar = new System.Windows.Forms.Button();
            this.dnotasDataGridView = new System.Windows.Forms.DataGridView();
            this.IdDetalle = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.idNotaDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.idMateriaDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.alumno = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lab1DataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lab2DataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.parcialDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nota_final = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.bindingSource2 = new System.Windows.Forms.BindingSource(this.components);
            this.bindingSource3 = new System.Windows.Forms.BindingSource(this.components);
            this.bindingSource4 = new System.Windows.Forms.BindingSource(this.components);
            this.idNotaLabel1 = new System.Windows.Forms.Label();
            this.fechaDateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.fKnotasperiodoBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.cboMateria = new System.Windows.Forms.ComboBox();
            this.cboPeriodo = new System.Windows.Forms.ComboBox();
            idAlumnoLabel = new System.Windows.Forms.Label();
            idNotaLabel = new System.Windows.Forms.Label();
            idPeriodoLabel = new System.Windows.Forms.Label();
            fechaLabel = new System.Windows.Forms.Label();
            periodoLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.db_academicaDataSet1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.materiasBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.periodosBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.notasBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dnotasBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dnotasDataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fKnotasperiodoBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // idAlumnoLabel
            // 
            idAlumnoLabel.AutoSize = true;
            idAlumnoLabel.Location = new System.Drawing.Point(77, 66);
            idAlumnoLabel.Name = "idAlumnoLabel";
            idAlumnoLabel.Size = new System.Drawing.Size(44, 13);
            idAlumnoLabel.TabIndex = 19;
            idAlumnoLabel.Text = "materia:";
            // 
            // idNotaLabel
            // 
            idNotaLabel.AutoSize = true;
            idNotaLabel.Location = new System.Drawing.Point(77, 27);
            idNotaLabel.Name = "idNotaLabel";
            idNotaLabel.Size = new System.Drawing.Size(21, 13);
            idNotaLabel.TabIndex = 21;
            idNotaLabel.Text = "ID:";
            // 
            // idPeriodoLabel
            // 
            idPeriodoLabel.AutoSize = true;
            idPeriodoLabel.Location = new System.Drawing.Point(462, 103);
            idPeriodoLabel.Name = "idPeriodoLabel";
            idPeriodoLabel.Size = new System.Drawing.Size(45, 13);
            idPeriodoLabel.TabIndex = 23;
            idPeriodoLabel.Text = "periodo:";
            // 
            // fechaLabel
            // 
            fechaLabel.AutoSize = true;
            fechaLabel.Location = new System.Drawing.Point(77, 103);
            fechaLabel.Name = "fechaLabel";
            fechaLabel.Size = new System.Drawing.Size(37, 13);
            fechaLabel.TabIndex = 25;
            fechaLabel.Text = "fecha:";
            // 
            // db_academicaDataSet1
            // 
            this.db_academicaDataSet1.DataSetName = "db_academicaDataSet1";
            this.db_academicaDataSet1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // materiasBindingSource
            // 
            this.materiasBindingSource.DataMember = "materias";
            this.materiasBindingSource.DataSource = this.db_academicaDataSet1;
            // 
            // materiasTableAdapter
            // 
            this.materiasTableAdapter.ClearBeforeFill = true;
            // 
            // periodosBindingSource
            // 
            this.periodosBindingSource.DataMember = "periodos";
            this.periodosBindingSource.DataSource = this.db_academicaDataSet1;
            // 
            // periodosTableAdapter
            // 
            this.periodosTableAdapter.ClearBeforeFill = true;
            // 
            // notasBindingSource
            // 
            this.notasBindingSource.DataMember = "notas";
            this.notasBindingSource.DataSource = this.db_academicaDataSet1;
            // 
            // notasTableAdapter
            // 
            this.notasTableAdapter.ClearBeforeFill = true;
            // 
            // tableAdapterManager
            // 
            this.tableAdapterManager.alumnosTableAdapter = null;
            this.tableAdapterManager.BackupDataSetBeforeUpdate = false;
            this.tableAdapterManager.dnotasTableAdapter = this.dnotasTableAdapter;
            this.tableAdapterManager.docentesTableAdapter = null;
            this.tableAdapterManager.materiasTableAdapter = this.materiasTableAdapter;
            this.tableAdapterManager.notasTableAdapter = this.notasTableAdapter;
            this.tableAdapterManager.periodosTableAdapter = this.periodosTableAdapter;
            this.tableAdapterManager.UpdateOrder = MiPrimerProyectoEnCSharp.db_academicaDataSet1TableAdapters.TableAdapterManager.UpdateOrderOption.InsertUpdateDelete;
            // 
            // dnotasTableAdapter
            // 
            this.dnotasTableAdapter.ClearBeforeFill = true;
            // 
            // dnotasBindingSource
            // 
            this.dnotasBindingSource.DataMember = "FK_dnotas_notas";
            this.dnotasBindingSource.DataSource = this.notasBindingSource;
            // 
            // btnGuardar
            // 
            this.btnGuardar.Location = new System.Drawing.Point(706, 158);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(75, 23);
            this.btnGuardar.TabIndex = 19;
            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.UseVisualStyleBackColor = true;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click_1);
            // 
            // dnotasDataGridView
            // 
            this.dnotasDataGridView.AllowUserToAddRows = false;
            this.dnotasDataGridView.AllowUserToDeleteRows = false;
            this.dnotasDataGridView.AutoGenerateColumns = false;
            this.dnotasDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dnotasDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.IdDetalle,
            this.idNotaDataGridViewTextBoxColumn,
            this.idMateriaDataGridViewTextBoxColumn,
            this.alumno,
            this.lab1DataGridViewTextBoxColumn,
            this.lab2DataGridViewTextBoxColumn,
            this.parcialDataGridViewTextBoxColumn,
            this.nota_final});
            this.dnotasDataGridView.DataSource = this.dnotasBindingSource;
            this.dnotasDataGridView.Location = new System.Drawing.Point(42, 158);
            this.dnotasDataGridView.Name = "dnotasDataGridView";
            this.dnotasDataGridView.Size = new System.Drawing.Size(658, 274);
            this.dnotasDataGridView.TabIndex = 18;
            // 
            // IdDetalle
            // 
            this.IdDetalle.DataPropertyName = "IdDetalle";
            this.IdDetalle.HeaderText = "ID";
            this.IdDetalle.Name = "IdDetalle";
            this.IdDetalle.ReadOnly = true;
            this.IdDetalle.Visible = false;
            // 
            // idNotaDataGridViewTextBoxColumn
            // 
            this.idNotaDataGridViewTextBoxColumn.DataPropertyName = "idNota";
            this.idNotaDataGridViewTextBoxColumn.HeaderText = "idNota";
            this.idNotaDataGridViewTextBoxColumn.Name = "idNotaDataGridViewTextBoxColumn";
            this.idNotaDataGridViewTextBoxColumn.Visible = false;
            // 
            // idMateriaDataGridViewTextBoxColumn
            // 
            this.idMateriaDataGridViewTextBoxColumn.DataPropertyName = "idMateria";
            this.idMateriaDataGridViewTextBoxColumn.HeaderText = "idMateria";
            this.idMateriaDataGridViewTextBoxColumn.Name = "idMateriaDataGridViewTextBoxColumn";
            this.idMateriaDataGridViewTextBoxColumn.Visible = false;
            // 
            // alumno
            // 
            this.alumno.DataPropertyName = "nombre";
            this.alumno.HeaderText = "ALUMNO";
            this.alumno.Name = "alumno";
            this.alumno.ReadOnly = true;
            this.alumno.Width = 200;
            // 
            // lab1DataGridViewTextBoxColumn
            // 
            this.lab1DataGridViewTextBoxColumn.DataPropertyName = "lab1";
            this.lab1DataGridViewTextBoxColumn.HeaderText = "lab1";
            this.lab1DataGridViewTextBoxColumn.Name = "lab1DataGridViewTextBoxColumn";
            // 
            // lab2DataGridViewTextBoxColumn
            // 
            this.lab2DataGridViewTextBoxColumn.DataPropertyName = "lab2";
            this.lab2DataGridViewTextBoxColumn.HeaderText = "lab2";
            this.lab2DataGridViewTextBoxColumn.Name = "lab2DataGridViewTextBoxColumn";
            // 
            // parcialDataGridViewTextBoxColumn
            // 
            this.parcialDataGridViewTextBoxColumn.DataPropertyName = "parcial";
            this.parcialDataGridViewTextBoxColumn.HeaderText = "parcial";
            this.parcialDataGridViewTextBoxColumn.Name = "parcialDataGridViewTextBoxColumn";
            // 
            // nota_final
            // 
            this.nota_final.DataPropertyName = "nf";
            this.nota_final.HeaderText = "NF";
            this.nota_final.Name = "nota_final";
            this.nota_final.ReadOnly = true;
            // 
            // bindingSource1
            // 
            this.bindingSource1.DataMember = "dnotas";
            // 
            // bindingSource2
            // 
            this.bindingSource2.DataMember = "periodos";
            // 
            // bindingSource3
            // 
            this.bindingSource3.DataMember = "materias";
            // 
            // bindingSource4
            // 
            this.bindingSource4.DataMember = "notas";
            // 
            // idNotaLabel1
            // 
            this.idNotaLabel1.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.notasBindingSource, "IdNota", true));
            this.idNotaLabel1.Location = new System.Drawing.Point(140, 27);
            this.idNotaLabel1.Name = "idNotaLabel1";
            this.idNotaLabel1.Size = new System.Drawing.Size(200, 23);
            this.idNotaLabel1.TabIndex = 22;
            this.idNotaLabel1.Text = "label1";
            // 
            // fechaDateTimePicker
            // 
            this.fechaDateTimePicker.DataBindings.Add(new System.Windows.Forms.Binding("Value", this.notasBindingSource, "fecha", true));
            this.fechaDateTimePicker.Location = new System.Drawing.Point(140, 99);
            this.fechaDateTimePicker.Name = "fechaDateTimePicker";
            this.fechaDateTimePicker.Size = new System.Drawing.Size(200, 20);
            this.fechaDateTimePicker.TabIndex = 26;
            // 
            // fKnotasperiodoBindingSource
            // 
            this.fKnotasperiodoBindingSource.DataMember = "FK_notas_periodo";
            this.fKnotasperiodoBindingSource.DataSource = this.periodosBindingSource;
            // 
            // cboMateria
            // 
            this.cboMateria.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.materiasBindingSource, "nombre", true));
            this.cboMateria.DataBindings.Add(new System.Windows.Forms.Binding("SelectedValue", this.materiasBindingSource, "nombre", true));
            this.cboMateria.DataSource = this.materiasBindingSource;
            this.cboMateria.DisplayMember = "nombre";
            this.cboMateria.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboMateria.FormattingEnabled = true;
            this.cboMateria.Location = new System.Drawing.Point(143, 63);
            this.cboMateria.Name = "cboMateria";
            this.cboMateria.Size = new System.Drawing.Size(185, 21);
            this.cboMateria.TabIndex = 28;
            this.cboMateria.ValueMember = "IdMateria";
            this.cboMateria.SelectedIndexChanged += new System.EventHandler(this.cboMateria_SelectedValueChanged);
            this.cboMateria.SelectedValueChanged += new System.EventHandler(this.cboMateria_SelectedValueChanged);
            // 
            // periodoLabel
            // 
            periodoLabel.AutoSize = true;
            periodoLabel.Location = new System.Drawing.Point(451, 61);
            periodoLabel.Name = "periodoLabel";
            periodoLabel.Size = new System.Drawing.Size(45, 13);
            periodoLabel.TabIndex = 30;
            periodoLabel.Text = "periodo:";
            // 
            // cboPeriodo
            // 
            this.cboPeriodo.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.periodosBindingSource, "periodo", true));
            this.cboPeriodo.DataBindings.Add(new System.Windows.Forms.Binding("SelectedValue", this.periodosBindingSource, "periodo", true));
            this.cboPeriodo.DataSource = this.periodosBindingSource;
            this.cboPeriodo.DisplayMember = "periodo";
            this.cboPeriodo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboPeriodo.FormattingEnabled = true;
            this.cboPeriodo.Location = new System.Drawing.Point(515, 58);
            this.cboPeriodo.Name = "cboPeriodo";
            this.cboPeriodo.Size = new System.Drawing.Size(200, 21);
            this.cboPeriodo.TabIndex = 31;
            this.cboPeriodo.ValueMember = "IdPeriodo";
            this.cboPeriodo.SelectedIndexChanged += new System.EventHandler(this.cboPeriodo_SelectedValueChanged);
            this.cboPeriodo.SelectedValueChanged += new System.EventHandler(this.cboPeriodo_SelectedValueChanged);
            // 
            // frmNotas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(821, 487);
            this.Controls.Add(periodoLabel);
            this.Controls.Add(this.cboPeriodo);
            this.Controls.Add(this.cboMateria);
            this.Controls.Add(idAlumnoLabel);
            this.Controls.Add(idNotaLabel);
            this.Controls.Add(this.idNotaLabel1);
            this.Controls.Add(idPeriodoLabel);
            this.Controls.Add(fechaLabel);
            this.Controls.Add(this.fechaDateTimePicker);
            this.Controls.Add(this.btnGuardar);
            this.Controls.Add(this.dnotasDataGridView);
            this.Name = "frmNotas";
            this.Text = "Formulario Notas";
            ((System.ComponentModel.ISupportInitialize)(this.db_academicaDataSet1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.materiasBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.periodosBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.notasBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dnotasBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dnotasDataGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fKnotasperiodoBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private db_academicaDataSet1 db_academicaDataSet1;
        private System.Windows.Forms.BindingSource materiasBindingSource;
        private db_academicaDataSet1TableAdapters.materiasTableAdapter materiasTableAdapter;
        private System.Windows.Forms.BindingSource periodosBindingSource;
        private db_academicaDataSet1TableAdapters.periodosTableAdapter periodosTableAdapter;
        private System.Windows.Forms.BindingSource notasBindingSource;
        private db_academicaDataSet1TableAdapters.notasTableAdapter notasTableAdapter;
        private db_academicaDataSet1TableAdapters.TableAdapterManager tableAdapterManager;
        private db_academicaDataSet1TableAdapters.dnotasTableAdapter dnotasTableAdapter;
        private System.Windows.Forms.BindingSource dnotasBindingSource;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.DataGridView dnotasDataGridView;
        private System.Windows.Forms.BindingSource bindingSource1;
        private System.Windows.Forms.BindingSource bindingSource2;
        private System.Windows.Forms.BindingSource bindingSource3;
        private System.Windows.Forms.BindingSource bindingSource4;
        private System.Windows.Forms.DataGridViewTextBoxColumn idDetalleDataGridViewTextBoxColumn;
        private System.Windows.Forms.Label idNotaLabel1;
        private System.Windows.Forms.DateTimePicker fechaDateTimePicker;
        private System.Windows.Forms.ComboBox cboMateria;
        private System.Windows.Forms.DataGridViewTextBoxColumn IdDetalle;
        private System.Windows.Forms.DataGridViewTextBoxColumn idNotaDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn idMateriaDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn alumno;
        private System.Windows.Forms.DataGridViewTextBoxColumn lab1DataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn lab2DataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn parcialDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn nota_final;
        private System.Windows.Forms.BindingSource fKnotasperiodoBindingSource;
        private System.Windows.Forms.ComboBox cboPeriodo;
    }
}