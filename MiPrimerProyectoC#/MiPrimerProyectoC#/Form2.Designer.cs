
namespace MiPrimerProyectoC_
{
    partial class Form2
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
            this.lblNum1 = new System.Windows.Forms.Label();
            this.txtNum1 = new System.Windows.Forms.TextBox();
            this.btnCalcular = new System.Windows.Forms.Button();
            this.txtNum2 = new System.Windows.Forms.TextBox();
            this.lblNum2 = new System.Windows.Forms.Label();
            this.lblRespuesta = new System.Windows.Forms.Label();
            this.grbOpciones = new System.Windows.Forms.GroupBox();
            this.optPorcentaje = new System.Windows.Forms.RadioButton();
            this.optExponenciacion = new System.Windows.Forms.RadioButton();
            this.optDivision = new System.Windows.Forms.RadioButton();
            this.optMultiplicacion = new System.Windows.Forms.RadioButton();
            this.optResta = new System.Windows.Forms.RadioButton();
            this.optSuma = new System.Windows.Forms.RadioButton();
            this.cboOpciones = new System.Windows.Forms.ComboBox();
            this.btnCalcularOpciones = new System.Windows.Forms.Button();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.optPrimo = new System.Windows.Forms.RadioButton();
            this.optFactorial = new System.Windows.Forms.RadioButton();
            this.grbOpciones.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblNum1
            // 
            this.lblNum1.AutoSize = true;
            this.lblNum1.Location = new System.Drawing.Point(33, 34);
            this.lblNum1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblNum1.Name = "lblNum1";
            this.lblNum1.Size = new System.Drawing.Size(45, 16);
            this.lblNum1.TabIndex = 0;
            this.lblNum1.Text = "Num1:";
            // 
            // txtNum1
            // 
            this.txtNum1.Location = new System.Drawing.Point(92, 31);
            this.txtNum1.Margin = new System.Windows.Forms.Padding(4);
            this.txtNum1.Name = "txtNum1";
            this.txtNum1.Size = new System.Drawing.Size(132, 22);
            this.txtNum1.TabIndex = 1;
            // 
            // btnCalcular
            // 
            this.btnCalcular.Location = new System.Drawing.Point(95, 159);
            this.btnCalcular.Margin = new System.Windows.Forms.Padding(4);
            this.btnCalcular.Name = "btnCalcular";
            this.btnCalcular.Size = new System.Drawing.Size(100, 28);
            this.btnCalcular.TabIndex = 2;
            this.btnCalcular.Text = "Calcular";
            this.btnCalcular.UseVisualStyleBackColor = true;
            this.btnCalcular.Click += new System.EventHandler(this.btnCalcular_Click);
            // 
            // txtNum2
            // 
            this.txtNum2.Location = new System.Drawing.Point(92, 74);
            this.txtNum2.Margin = new System.Windows.Forms.Padding(4);
            this.txtNum2.Name = "txtNum2";
            this.txtNum2.Size = new System.Drawing.Size(132, 22);
            this.txtNum2.TabIndex = 4;
            // 
            // lblNum2
            // 
            this.lblNum2.AutoSize = true;
            this.lblNum2.Location = new System.Drawing.Point(33, 78);
            this.lblNum2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblNum2.Name = "lblNum2";
            this.lblNum2.Size = new System.Drawing.Size(45, 16);
            this.lblNum2.TabIndex = 3;
            this.lblNum2.Text = "Num2:";
            // 
            // lblRespuesta
            // 
            this.lblRespuesta.AutoSize = true;
            this.lblRespuesta.Location = new System.Drawing.Point(101, 121);
            this.lblRespuesta.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblRespuesta.Name = "lblRespuesta";
            this.lblRespuesta.Size = new System.Drawing.Size(86, 16);
            this.lblRespuesta.TabIndex = 5;
            this.lblRespuesta.Text = "Respuesta: ?";
            // 
            // grbOpciones
            // 
            this.grbOpciones.Controls.Add(this.optFactorial);
            this.grbOpciones.Controls.Add(this.optPrimo);
            this.grbOpciones.Controls.Add(this.optPorcentaje);
            this.grbOpciones.Controls.Add(this.optExponenciacion);
            this.grbOpciones.Controls.Add(this.optDivision);
            this.grbOpciones.Controls.Add(this.optMultiplicacion);
            this.grbOpciones.Controls.Add(this.optResta);
            this.grbOpciones.Controls.Add(this.optSuma);
            this.grbOpciones.Location = new System.Drawing.Point(290, 34);
            this.grbOpciones.Name = "grbOpciones";
            this.grbOpciones.Size = new System.Drawing.Size(150, 251);
            this.grbOpciones.TabIndex = 6;
            this.grbOpciones.TabStop = false;
            this.grbOpciones.Text = "Opciones";
            this.grbOpciones.Enter += new System.EventHandler(this.grbOpciones_Enter);
            // 
            // optPorcentaje
            // 
            this.optPorcentaje.AutoSize = true;
            this.optPorcentaje.Location = new System.Drawing.Point(16, 151);
            this.optPorcentaje.Name = "optPorcentaje";
            this.optPorcentaje.Size = new System.Drawing.Size(93, 20);
            this.optPorcentaje.TabIndex = 5;
            this.optPorcentaje.Text = "Porcentaje";
            this.optPorcentaje.UseVisualStyleBackColor = true;
            // 
            // optExponenciacion
            // 
            this.optExponenciacion.AutoSize = true;
            this.optExponenciacion.Location = new System.Drawing.Point(16, 125);
            this.optExponenciacion.Name = "optExponenciacion";
            this.optExponenciacion.Size = new System.Drawing.Size(124, 20);
            this.optExponenciacion.TabIndex = 4;
            this.optExponenciacion.Text = "Exponenciación";
            this.optExponenciacion.UseVisualStyleBackColor = true;
            this.optExponenciacion.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
            // 
            // optDivision
            // 
            this.optDivision.AutoSize = true;
            this.optDivision.Location = new System.Drawing.Point(16, 98);
            this.optDivision.Name = "optDivision";
            this.optDivision.Size = new System.Drawing.Size(76, 20);
            this.optDivision.TabIndex = 3;
            this.optDivision.Text = "Division";
            this.optDivision.UseVisualStyleBackColor = true;
            this.optDivision.CheckedChanged += new System.EventHandler(this.optDivision_CheckedChanged);
            // 
            // optMultiplicacion
            // 
            this.optMultiplicacion.AutoSize = true;
            this.optMultiplicacion.Location = new System.Drawing.Point(16, 72);
            this.optMultiplicacion.Name = "optMultiplicacion";
            this.optMultiplicacion.Size = new System.Drawing.Size(109, 20);
            this.optMultiplicacion.TabIndex = 2;
            this.optMultiplicacion.Text = "Multiplicación";
            this.optMultiplicacion.UseVisualStyleBackColor = true;
            // 
            // optResta
            // 
            this.optResta.AutoSize = true;
            this.optResta.Location = new System.Drawing.Point(16, 48);
            this.optResta.Name = "optResta";
            this.optResta.Size = new System.Drawing.Size(64, 20);
            this.optResta.TabIndex = 1;
            this.optResta.Text = "Resta";
            this.optResta.UseVisualStyleBackColor = true;
            // 
            // optSuma
            // 
            this.optSuma.AutoSize = true;
            this.optSuma.Checked = true;
            this.optSuma.Location = new System.Drawing.Point(16, 22);
            this.optSuma.Name = "optSuma";
            this.optSuma.Size = new System.Drawing.Size(63, 20);
            this.optSuma.TabIndex = 0;
            this.optSuma.TabStop = true;
            this.optSuma.Text = "Suma";
            this.optSuma.UseVisualStyleBackColor = true;
            // 
            // cboOpciones
            // 
            this.cboOpciones.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboOpciones.FormattingEnabled = true;
            this.cboOpciones.Items.AddRange(new object[] {
            "Suma",
            "Resta",
            "Multiplicaión",
            "Division",
            "Primo ",
            "Factorial"});
            this.cboOpciones.Location = new System.Drawing.Point(467, 34);
            this.cboOpciones.Name = "cboOpciones";
            this.cboOpciones.Size = new System.Drawing.Size(121, 24);
            this.cboOpciones.TabIndex = 6;
            this.cboOpciones.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // btnCalcularOpciones
            // 
            this.btnCalcularOpciones.Location = new System.Drawing.Point(467, 98);
            this.btnCalcularOpciones.Margin = new System.Windows.Forms.Padding(4);
            this.btnCalcularOpciones.Name = "btnCalcularOpciones";
            this.btnCalcularOpciones.Size = new System.Drawing.Size(121, 28);
            this.btnCalcularOpciones.TabIndex = 7;
            this.btnCalcularOpciones.Text = "Calcular";
            this.btnCalcularOpciones.UseVisualStyleBackColor = true;
            this.btnCalcularOpciones.Click += new System.EventHandler(this.btnCalcularOpciones_Click);
            // 
            // optPrimo
            // 
            this.optPrimo.AutoSize = true;
            this.optPrimo.Location = new System.Drawing.Point(16, 177);
            this.optPrimo.Name = "optPrimo";
            this.optPrimo.Size = new System.Drawing.Size(63, 20);
            this.optPrimo.TabIndex = 8;
            this.optPrimo.Text = "Primo";
            this.optPrimo.UseVisualStyleBackColor = true;
            // 
            // optFactorial
            // 
            this.optFactorial.AutoSize = true;
            this.optFactorial.Location = new System.Drawing.Point(16, 203);
            this.optFactorial.Name = "optFactorial";
            this.optFactorial.Size = new System.Drawing.Size(80, 20);
            this.optFactorial.TabIndex = 9;
            this.optFactorial.Text = "Factorial";
            this.optFactorial.UseVisualStyleBackColor = true;
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(624, 297);
            this.Controls.Add(this.btnCalcularOpciones);
            this.Controls.Add(this.cboOpciones);
            this.Controls.Add(this.grbOpciones);
            this.Controls.Add(this.lblRespuesta);
            this.Controls.Add(this.txtNum2);
            this.Controls.Add(this.lblNum2);
            this.Controls.Add(this.btnCalcular);
            this.Controls.Add(this.txtNum1);
            this.Controls.Add(this.lblNum1);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Form2";
            this.Text = "form2                                           j";
            this.Load += new System.EventHandler(this.Form2_Load);
            this.grbOpciones.ResumeLayout(false);
            this.grbOpciones.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblNum1;
        private System.Windows.Forms.TextBox txtNum1;
        private System.Windows.Forms.Button btnCalcular;
        private System.Windows.Forms.TextBox txtNum2;
        private System.Windows.Forms.Label lblNum2;
        private System.Windows.Forms.Label lblRespuesta;
        private System.Windows.Forms.GroupBox grbOpciones;
        private System.Windows.Forms.RadioButton optDivision;
        private System.Windows.Forms.RadioButton optMultiplicacion;
        private System.Windows.Forms.RadioButton optResta;
        private System.Windows.Forms.RadioButton optSuma;
        private System.Windows.Forms.RadioButton optExponenciacion;
        private System.Windows.Forms.RadioButton optPorcentaje;
        private System.Windows.Forms.ComboBox cboOpciones;
        private System.Windows.Forms.Button btnCalcularOpciones;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.RadioButton optFactorial;
        private System.Windows.Forms.RadioButton optPrimo;
    }
}