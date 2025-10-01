namespace MiPrimerProyectoC_
{
    partial class frmPeriodos
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPeriodos));
            System.Windows.Forms.Label idNotasLabel;
            System.Windows.Forms.Label periodoLabel;
            System.Windows.Forms.Label fechaLabel;
            this.db_academicaDataSet = new MiPrimerProyectoC_.db_academicaDataSet();
            this.periodosBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.periodosTableAdapter = new MiPrimerProyectoC_.db_academicaDataSetTableAdapters.periodosTableAdapter();
            this.tableAdapterManager = new MiPrimerProyectoC_.db_academicaDataSetTableAdapters.TableAdapterManager();
            this.periodosBindingNavigator = new System.Windows.Forms.BindingNavigator(this.components);
            this.bindingNavigatorMoveFirstItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorMovePreviousItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.bindingNavigatorPositionItem = new System.Windows.Forms.ToolStripTextBox();
            this.bindingNavigatorCountItem = new System.Windows.Forms.ToolStripLabel();
            this.bindingNavigatorSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.bindingNavigatorMoveNextItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorMoveLastItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.bindingNavigatorAddNewItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorDeleteItem = new System.Windows.Forms.ToolStripButton();
            this.periodosBindingNavigatorSaveItem = new System.Windows.Forms.ToolStripButton();
            this.idNotasLabel1 = new System.Windows.Forms.Label();
            this.periodoTextBox = new System.Windows.Forms.TextBox();
            this.fechaDateTimePicker = new System.Windows.Forms.DateTimePicker();
            idNotasLabel = new System.Windows.Forms.Label();
            periodoLabel = new System.Windows.Forms.Label();
            fechaLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.db_academicaDataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.periodosBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.periodosBindingNavigator)).BeginInit();
            this.periodosBindingNavigator.SuspendLayout();
            this.SuspendLayout();
            // 
            // db_academicaDataSet
            // 
            this.db_academicaDataSet.DataSetName = "db_academicaDataSet";
            this.db_academicaDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // periodosBindingSource
            // 
            this.periodosBindingSource.DataMember = "periodos";
            this.periodosBindingSource.DataSource = this.db_academicaDataSet;
            // 
            // periodosTableAdapter
            // 
            this.periodosTableAdapter.ClearBeforeFill = true;
            // 
            // tableAdapterManager
            // 
            this.tableAdapterManager.alumnosTableAdapter = null;
            this.tableAdapterManager.BackupDataSetBeforeUpdate = false;
            this.tableAdapterManager.docentesTableAdapter = null;
            this.tableAdapterManager.materiasTableAdapter = null;
            this.tableAdapterManager.periodosTableAdapter = this.periodosTableAdapter;
            this.tableAdapterManager.UpdateOrder = MiPrimerProyectoC_.db_academicaDataSetTableAdapters.TableAdapterManager.UpdateOrderOption.InsertUpdateDelete;
            // 
            // periodosBindingNavigator
            // 
            this.periodosBindingNavigator.AddNewItem = this.bindingNavigatorAddNewItem;
            this.periodosBindingNavigator.BindingSource = this.periodosBindingSource;
            this.periodosBindingNavigator.CountItem = this.bindingNavigatorCountItem;
            this.periodosBindingNavigator.DeleteItem = this.bindingNavigatorDeleteItem;
            this.periodosBindingNavigator.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.periodosBindingNavigator.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.bindingNavigatorMoveFirstItem,
            this.bindingNavigatorMovePreviousItem,
            this.bindingNavigatorSeparator,
            this.bindingNavigatorPositionItem,
            this.bindingNavigatorCountItem,
            this.bindingNavigatorSeparator1,
            this.bindingNavigatorMoveNextItem,
            this.bindingNavigatorMoveLastItem,
            this.bindingNavigatorSeparator2,
            this.bindingNavigatorAddNewItem,
            this.bindingNavigatorDeleteItem,
            this.periodosBindingNavigatorSaveItem});
            this.periodosBindingNavigator.Location = new System.Drawing.Point(0, 0);
            this.periodosBindingNavigator.MoveFirstItem = this.bindingNavigatorMoveFirstItem;
            this.periodosBindingNavigator.MoveLastItem = this.bindingNavigatorMoveLastItem;
            this.periodosBindingNavigator.MoveNextItem = this.bindingNavigatorMoveNextItem;
            this.periodosBindingNavigator.MovePreviousItem = this.bindingNavigatorMovePreviousItem;
            this.periodosBindingNavigator.Name = "periodosBindingNavigator";
            this.periodosBindingNavigator.PositionItem = this.bindingNavigatorPositionItem;
            this.periodosBindingNavigator.Size = new System.Drawing.Size(376, 31);
            this.periodosBindingNavigator.TabIndex = 0;
            this.periodosBindingNavigator.Text = "bindingNavigator1";
            // 
            // bindingNavigatorMoveFirstItem
            // 
            this.bindingNavigatorMoveFirstItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMoveFirstItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveFirstItem.Image")));
            this.bindingNavigatorMoveFirstItem.Name = "bindingNavigatorMoveFirstItem";
            this.bindingNavigatorMoveFirstItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMoveFirstItem.Size = new System.Drawing.Size(29, 22);
            this.bindingNavigatorMoveFirstItem.Text = "Mover primero";
            // 
            // bindingNavigatorMovePreviousItem
            // 
            this.bindingNavigatorMovePreviousItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMovePreviousItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMovePreviousItem.Image")));
            this.bindingNavigatorMovePreviousItem.Name = "bindingNavigatorMovePreviousItem";
            this.bindingNavigatorMovePreviousItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMovePreviousItem.Size = new System.Drawing.Size(29, 22);
            this.bindingNavigatorMovePreviousItem.Text = "Mover anterior";
            // 
            // bindingNavigatorSeparator
            // 
            this.bindingNavigatorSeparator.Name = "bindingNavigatorSeparator";
            this.bindingNavigatorSeparator.Size = new System.Drawing.Size(6, 25);
            // 
            // bindingNavigatorPositionItem
            // 
            this.bindingNavigatorPositionItem.AccessibleName = "Posición";
            this.bindingNavigatorPositionItem.AutoSize = false;
            this.bindingNavigatorPositionItem.Name = "bindingNavigatorPositionItem";
            this.bindingNavigatorPositionItem.Size = new System.Drawing.Size(50, 27);
            this.bindingNavigatorPositionItem.Text = "0";
            this.bindingNavigatorPositionItem.ToolTipText = "Posición actual";
            // 
            // bindingNavigatorCountItem
            // 
            this.bindingNavigatorCountItem.Name = "bindingNavigatorCountItem";
            this.bindingNavigatorCountItem.Size = new System.Drawing.Size(48, 20);
            this.bindingNavigatorCountItem.Text = "de {0}";
            this.bindingNavigatorCountItem.ToolTipText = "Número total de elementos";
            // 
            // bindingNavigatorSeparator1
            // 
            this.bindingNavigatorSeparator1.Name = "bindingNavigatorSeparator";
            this.bindingNavigatorSeparator1.Size = new System.Drawing.Size(6, 6);
            // 
            // bindingNavigatorMoveNextItem
            // 
            this.bindingNavigatorMoveNextItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMoveNextItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveNextItem.Image")));
            this.bindingNavigatorMoveNextItem.Name = "bindingNavigatorMoveNextItem";
            this.bindingNavigatorMoveNextItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMoveNextItem.Size = new System.Drawing.Size(29, 24);
            this.bindingNavigatorMoveNextItem.Text = "Mover siguiente";
            // 
            // bindingNavigatorMoveLastItem
            // 
            this.bindingNavigatorMoveLastItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMoveLastItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveLastItem.Image")));
            this.bindingNavigatorMoveLastItem.Name = "bindingNavigatorMoveLastItem";
            this.bindingNavigatorMoveLastItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMoveLastItem.Size = new System.Drawing.Size(29, 24);
            this.bindingNavigatorMoveLastItem.Text = "Mover último";
            // 
            // bindingNavigatorSeparator2
            // 
            this.bindingNavigatorSeparator2.Name = "bindingNavigatorSeparator";
            this.bindingNavigatorSeparator2.Size = new System.Drawing.Size(6, 6);
            // 
            // bindingNavigatorAddNewItem
            // 
            this.bindingNavigatorAddNewItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorAddNewItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorAddNewItem.Image")));
            this.bindingNavigatorAddNewItem.Name = "bindingNavigatorAddNewItem";
            this.bindingNavigatorAddNewItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorAddNewItem.Size = new System.Drawing.Size(29, 28);
            this.bindingNavigatorAddNewItem.Text = "Agregar nuevo";
            // 
            // bindingNavigatorDeleteItem
            // 
            this.bindingNavigatorDeleteItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorDeleteItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorDeleteItem.Image")));
            this.bindingNavigatorDeleteItem.Name = "bindingNavigatorDeleteItem";
            this.bindingNavigatorDeleteItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorDeleteItem.Size = new System.Drawing.Size(29, 24);
            this.bindingNavigatorDeleteItem.Text = "Eliminar";
            // 
            // periodosBindingNavigatorSaveItem
            // 
            this.periodosBindingNavigatorSaveItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.periodosBindingNavigatorSaveItem.Image = ((System.Drawing.Image)(resources.GetObject("periodosBindingNavigatorSaveItem.Image")));
            this.periodosBindingNavigatorSaveItem.Name = "periodosBindingNavigatorSaveItem";
            this.periodosBindingNavigatorSaveItem.Size = new System.Drawing.Size(23, 23);
            this.periodosBindingNavigatorSaveItem.Text = "Guardar datos";
            this.periodosBindingNavigatorSaveItem.Click += new System.EventHandler(this.periodosBindingNavigatorSaveItem_Click);
            // 
            // idNotasLabel
            // 
            idNotasLabel.AutoSize = true;
            idNotasLabel.Location = new System.Drawing.Point(20, 81);
            idNotasLabel.Name = "idNotasLabel";
            idNotasLabel.Size = new System.Drawing.Size(60, 16);
            idNotasLabel.TabIndex = 1;
            idNotasLabel.Text = "Id Notas:";
            // 
            // idNotasLabel1
            // 
            this.idNotasLabel1.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.periodosBindingSource, "IdNotas", true));
            this.idNotasLabel1.Location = new System.Drawing.Point(86, 81);
            this.idNotasLabel1.Name = "idNotasLabel1";
            this.idNotasLabel1.Size = new System.Drawing.Size(200, 23);
            this.idNotasLabel1.TabIndex = 2;
            this.idNotasLabel1.Text = "label1";
            // 
            // periodoLabel
            // 
            periodoLabel.AutoSize = true;
            periodoLabel.Location = new System.Drawing.Point(20, 110);
            periodoLabel.Name = "periodoLabel";
            periodoLabel.Size = new System.Drawing.Size(57, 16);
            periodoLabel.TabIndex = 3;
            periodoLabel.Text = "periodo:";
            // 
            // periodoTextBox
            // 
            this.periodoTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.periodosBindingSource, "periodo", true));
            this.periodoTextBox.Location = new System.Drawing.Point(86, 107);
            this.periodoTextBox.Name = "periodoTextBox";
            this.periodoTextBox.Size = new System.Drawing.Size(200, 22);
            this.periodoTextBox.TabIndex = 4;
            // 
            // fechaLabel
            // 
            fechaLabel.AutoSize = true;
            fechaLabel.Location = new System.Drawing.Point(20, 139);
            fechaLabel.Name = "fechaLabel";
            fechaLabel.Size = new System.Drawing.Size(43, 16);
            fechaLabel.TabIndex = 5;
            fechaLabel.Text = "fecha:";
            // 
            // fechaDateTimePicker
            // 
            this.fechaDateTimePicker.DataBindings.Add(new System.Windows.Forms.Binding("Value", this.periodosBindingSource, "fecha", true));
            this.fechaDateTimePicker.Location = new System.Drawing.Point(86, 135);
            this.fechaDateTimePicker.Name = "fechaDateTimePicker";
            this.fechaDateTimePicker.Size = new System.Drawing.Size(200, 22);
            this.fechaDateTimePicker.TabIndex = 6;
            // 
            // frmPeriodos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(376, 226);
            this.Controls.Add(idNotasLabel);
            this.Controls.Add(this.idNotasLabel1);
            this.Controls.Add(periodoLabel);
            this.Controls.Add(this.periodoTextBox);
            this.Controls.Add(fechaLabel);
            this.Controls.Add(this.fechaDateTimePicker);
            this.Controls.Add(this.periodosBindingNavigator);
            this.Name = "frmPeriodos";
            this.Text = "frmPeriodos";
            this.Load += new System.EventHandler(this.frmPeriodos_Load);
            ((System.ComponentModel.ISupportInitialize)(this.db_academicaDataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.periodosBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.periodosBindingNavigator)).EndInit();
            this.periodosBindingNavigator.ResumeLayout(false);
            this.periodosBindingNavigator.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private db_academicaDataSet db_academicaDataSet;
        private System.Windows.Forms.BindingSource periodosBindingSource;
        private db_academicaDataSetTableAdapters.periodosTableAdapter periodosTableAdapter;
        private db_academicaDataSetTableAdapters.TableAdapterManager tableAdapterManager;
        private System.Windows.Forms.BindingNavigator periodosBindingNavigator;
        private System.Windows.Forms.ToolStripButton bindingNavigatorAddNewItem;
        private System.Windows.Forms.ToolStripLabel bindingNavigatorCountItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorDeleteItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveFirstItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMovePreviousItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator;
        private System.Windows.Forms.ToolStripTextBox bindingNavigatorPositionItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator1;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveNextItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveLastItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator2;
        private System.Windows.Forms.ToolStripButton periodosBindingNavigatorSaveItem;
        private System.Windows.Forms.Label idNotasLabel1;
        private System.Windows.Forms.TextBox periodoTextBox;
        private System.Windows.Forms.DateTimePicker fechaDateTimePicker;
    }
}