namespace MiPrimerProyectoEnCSharp
{
    partial class Form1
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblsueldo = new System.Windows.Forms.Label();
            this.lblISS = new System.Windows.Forms.Label();
            this.lblAFP = new System.Windows.Forms.Label();
            this.lblSueldoNeto = new System.Windows.Forms.Label();
            this.lblTotalDeducciones = new System.Windows.Forms.Label();
            this.lblISR = new System.Windows.Forms.Label();
            this.txtSueldo = new System.Windows.Forms.TextBox();
            this.btnCalcular = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblsueldo
            // 
            this.lblsueldo.AutoSize = true;
            this.lblsueldo.Location = new System.Drawing.Point(63, 32);
            this.lblsueldo.Name = "lblsueldo";
            this.lblsueldo.Size = new System.Drawing.Size(43, 13);
            this.lblsueldo.TabIndex = 0;
            this.lblsueldo.Text = "Sueldo:";
            this.lblsueldo.Click += new System.EventHandler(this.label1_Click);
            // 
            // lblISS
            // 
            this.lblISS.AutoSize = true;
            this.lblISS.Location = new System.Drawing.Point(66, 71);
            this.lblISS.Name = "lblISS";
            this.lblISS.Size = new System.Drawing.Size(43, 13);
            this.lblISS.TabIndex = 1;
            this.lblISS.Text = "ISSS: ?";
            // 
            // lblAFP
            // 
            this.lblAFP.AutoSize = true;
            this.lblAFP.Location = new System.Drawing.Point(66, 112);
            this.lblAFP.Name = "lblAFP";
            this.lblAFP.Size = new System.Drawing.Size(39, 13);
            this.lblAFP.TabIndex = 2;
            this.lblAFP.Text = "AFP: ?";
            // 
            // lblSueldoNeto
            // 
            this.lblSueldoNeto.AutoSize = true;
            this.lblSueldoNeto.Location = new System.Drawing.Point(66, 223);
            this.lblSueldoNeto.Name = "lblSueldoNeto";
            this.lblSueldoNeto.Size = new System.Drawing.Size(78, 13);
            this.lblSueldoNeto.TabIndex = 5;
            this.lblSueldoNeto.Text = "Sueldo Neto: ?";
            // 
            // lblTotalDeducciones
            // 
            this.lblTotalDeducciones.AutoSize = true;
            this.lblTotalDeducciones.Location = new System.Drawing.Point(66, 182);
            this.lblTotalDeducciones.Name = "lblTotalDeducciones";
            this.lblTotalDeducciones.Size = new System.Drawing.Size(109, 13);
            this.lblTotalDeducciones.TabIndex = 4;
            this.lblTotalDeducciones.Text = "Total Deducciones: ?";
            // 
            // lblISR
            // 
            this.lblISR.AutoSize = true;
            this.lblISR.Location = new System.Drawing.Point(66, 145);
            this.lblISR.Name = "lblISR";
            this.lblISR.Size = new System.Drawing.Size(37, 13);
            this.lblISR.TabIndex = 3;
            this.lblISR.Text = "ISR: ?";
            // 
            // txtSueldo
            // 
            this.txtSueldo.Location = new System.Drawing.Point(159, 29);
            this.txtSueldo.Name = "txtSueldo";
            this.txtSueldo.Size = new System.Drawing.Size(153, 20);
            this.txtSueldo.TabIndex = 6;
            // 
            // btnCalcular
            // 
            this.btnCalcular.Location = new System.Drawing.Point(358, 101);
            this.btnCalcular.Name = "btnCalcular";
            this.btnCalcular.Size = new System.Drawing.Size(164, 94);
            this.btnCalcular.TabIndex = 7;
            this.btnCalcular.Text = "CALCULAR";
            this.btnCalcular.UseVisualStyleBackColor = true;
            this.btnCalcular.Click += new System.EventHandler(this.btnCalcular_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(746, 332);
            this.Controls.Add(this.btnCalcular);
            this.Controls.Add(this.txtSueldo);
            this.Controls.Add(this.lblSueldoNeto);
            this.Controls.Add(this.lblTotalDeducciones);
            this.Controls.Add(this.lblISR);
            this.Controls.Add(this.lblAFP);
            this.Controls.Add(this.lblISS);
            this.Controls.Add(this.lblsueldo);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblsueldo;
        private System.Windows.Forms.Label lblISS;
        private System.Windows.Forms.Label lblAFP;
        private System.Windows.Forms.Label lblSueldoNeto;
        private System.Windows.Forms.Label lblTotalDeducciones;
        private System.Windows.Forms.Label lblISR;
        private System.Windows.Forms.TextBox txtSueldo;
        private System.Windows.Forms.Button btnCalcular;
    }
}

