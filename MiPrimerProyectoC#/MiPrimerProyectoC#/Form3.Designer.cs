namespace MiPrimerProyectoC_
{
    partial class Form3
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
            this.lblSueldo = new System.Windows.Forms.Label();
            this.txtSueldo = new System.Windows.Forms.TextBox();
            this.btnCalcular = new System.Windows.Forms.Button();
            this.lblIsr = new System.Windows.Forms.Label();
            this.lblAfp = new System.Windows.Forms.Label();
            this.lblIsss = new System.Windows.Forms.Label();
            this.lblSueldoNeto = new System.Windows.Forms.Label();
            this.lblTotalDeducciones = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblSueldo
            // 
            this.lblSueldo.AutoSize = true;
            this.lblSueldo.Location = new System.Drawing.Point(51, 99);
            this.lblSueldo.Name = "lblSueldo";
            this.lblSueldo.Size = new System.Drawing.Size(50, 16);
            this.lblSueldo.TabIndex = 0;
            this.lblSueldo.Text = "Sueldo";
            // 
            // txtSueldo
            // 
            this.txtSueldo.Location = new System.Drawing.Point(118, 93);
            this.txtSueldo.Name = "txtSueldo";
            this.txtSueldo.Size = new System.Drawing.Size(100, 22);
            this.txtSueldo.TabIndex = 1;
            // 
            // btnCalcular
            // 
            this.btnCalcular.Location = new System.Drawing.Point(296, 170);
            this.btnCalcular.Name = "btnCalcular";
            this.btnCalcular.Size = new System.Drawing.Size(75, 23);
            this.btnCalcular.TabIndex = 2;
            this.btnCalcular.Text = "Calcular";
            this.btnCalcular.UseVisualStyleBackColor = true;
            this.btnCalcular.Click += new System.EventHandler(this.btnCalcular_Click);
            // 
            // lblIsr
            // 
            this.lblIsr.AutoSize = true;
            this.lblIsr.Location = new System.Drawing.Point(51, 271);
            this.lblIsr.Name = "lblIsr";
            this.lblIsr.Size = new System.Drawing.Size(29, 16);
            this.lblIsr.TabIndex = 3;
            this.lblIsr.Text = "ISR";
            // 
            // lblAfp
            // 
            this.lblAfp.AutoSize = true;
            this.lblAfp.Location = new System.Drawing.Point(51, 217);
            this.lblAfp.Name = "lblAfp";
            this.lblAfp.Size = new System.Drawing.Size(33, 16);
            this.lblAfp.TabIndex = 4;
            this.lblAfp.Text = "AFP";
            // 
            // lblIsss
            // 
            this.lblIsss.AutoSize = true;
            this.lblIsss.Location = new System.Drawing.Point(51, 161);
            this.lblIsss.Name = "lblIsss";
            this.lblIsss.Size = new System.Drawing.Size(37, 16);
            this.lblIsss.TabIndex = 5;
            this.lblIsss.Text = "ISSS";
            // 
            // lblSueldoNeto
            // 
            this.lblSueldoNeto.AutoSize = true;
            this.lblSueldoNeto.Location = new System.Drawing.Point(51, 314);
            this.lblSueldoNeto.Name = "lblSueldoNeto";
            this.lblSueldoNeto.Size = new System.Drawing.Size(138, 16);
            this.lblSueldoNeto.TabIndex = 6;
            this.lblSueldoNeto.Text = "Total de deducciones";
            // 
            // lblTotalDeducciones
            // 
            this.lblTotalDeducciones.AutoSize = true;
            this.lblTotalDeducciones.Location = new System.Drawing.Point(51, 349);
            this.lblTotalDeducciones.Name = "lblTotalDeducciones";
            this.lblTotalDeducciones.Size = new System.Drawing.Size(82, 16);
            this.lblTotalDeducciones.TabIndex = 7;
            this.lblTotalDeducciones.Text = "Sueldo Neto";
            // 
            // Form3
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(396, 450);
            this.Controls.Add(this.lblTotalDeducciones);
            this.Controls.Add(this.lblSueldoNeto);
            this.Controls.Add(this.lblIsss);
            this.Controls.Add(this.lblAfp);
            this.Controls.Add(this.lblIsr);
            this.Controls.Add(this.btnCalcular);
            this.Controls.Add(this.txtSueldo);
            this.Controls.Add(this.lblSueldo);
            this.Name = "Form3";
            this.Text = "Form3";
            this.Load += new System.EventHandler(this.Form3_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblSueldo;
        private System.Windows.Forms.TextBox txtSueldo;
        private System.Windows.Forms.Button btnCalcular;
        private System.Windows.Forms.Label lblIsr;
        private System.Windows.Forms.Label lblAfp;
        private System.Windows.Forms.Label lblIsss;
        private System.Windows.Forms.Label lblSueldoNeto;
        private System.Windows.Forms.Label lblTotalDeducciones;
    }
}