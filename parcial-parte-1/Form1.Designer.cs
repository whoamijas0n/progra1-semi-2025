namespace parcial_parte_1
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            lblTitulo = new Label();
            lblIngreso = new Label();
            txtMonto = new TextBox();
            lblTotal = new Label();
            btnCalcular = new Button();
            SuspendLayout();
            // 
            // lblTitulo
            // 
            lblTitulo.AutoSize = true;
            lblTitulo.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTitulo.Location = new Point(298, 26);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(245, 21);
            lblTitulo.TabIndex = 0;
            lblTitulo.Text = "IMPUESTO A LAS ACTIVIDADES";
            // 
            // lblIngreso
            // 
            lblIngreso.AutoSize = true;
            lblIngreso.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblIngreso.Location = new Point(80, 92);
            lblIngreso.Name = "lblIngreso";
            lblIngreso.Size = new Size(143, 21);
            lblIngreso.TabIndex = 1;
            lblIngreso.Text = "Ingrese el monto:";
            // 
            // txtMonto
            // 
            txtMonto.Location = new Point(248, 89);
            txtMonto.Name = "txtMonto";
            txtMonto.Size = new Size(222, 23);
            txtMonto.TabIndex = 2;
            // 
            // lblTotal
            // 
            lblTotal.AutoSize = true;
            lblTotal.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTotal.Location = new Point(80, 164);
            lblTotal.Name = "lblTotal";
            lblTotal.Size = new Size(207, 21);
            lblTotal.TabIndex = 3;
            lblTotal.Text = "El valor total a pagar es : ?";
            // 
            // btnCalcular
            // 
            btnCalcular.Location = new Point(556, 81);
            btnCalcular.Name = "btnCalcular";
            btnCalcular.Size = new Size(173, 104);
            btnCalcular.TabIndex = 4;
            btnCalcular.Text = "Calcular";
            btnCalcular.UseVisualStyleBackColor = true;
            btnCalcular.Click += btnCalcular_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(825, 308);
            Controls.Add(btnCalcular);
            Controls.Add(lblTotal);
            Controls.Add(txtMonto);
            Controls.Add(lblIngreso);
            Controls.Add(lblTitulo);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblTitulo;
        private Label lblIngreso;
        private TextBox txtMonto;
        private Label lblTotal;
        private Button btnCalcular;
    }
}
