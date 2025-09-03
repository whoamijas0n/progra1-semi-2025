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
            this.lblTitulo = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lblTipoMoneda = new System.Windows.Forms.Label();
            this.lblConvertirMoneda = new System.Windows.Forms.Label();
            this.lblTotalMoneda = new System.Windows.Forms.Label();
            this.cboTipoMoneda = new System.Windows.Forms.ComboBox();
            this.cboConvertirMoneda = new System.Windows.Forms.ComboBox();
            this.cboConvertirMasa = new System.Windows.Forms.ComboBox();
            this.cboTipoMasa = new System.Windows.Forms.ComboBox();
            this.lblTotalMasa = new System.Windows.Forms.Label();
            this.lblConvertirMasa = new System.Windows.Forms.Label();
            this.lblTipoMasa = new System.Windows.Forms.Label();
            this.cboConvertirVolumen = new System.Windows.Forms.ComboBox();
            this.cboTipoVolumen = new System.Windows.Forms.ComboBox();
            this.lblTotalVolumen = new System.Windows.Forms.Label();
            this.lblConvertirVolumen = new System.Windows.Forms.Label();
            this.lblTipoVolumen = new System.Windows.Forms.Label();
            this.cboConvertirLongitud = new System.Windows.Forms.ComboBox();
            this.cboTipoLongitud = new System.Windows.Forms.ComboBox();
            this.lblTotalLongitud = new System.Windows.Forms.Label();
            this.lblConvertirLongitud = new System.Windows.Forms.Label();
            this.lblTipoLongitud = new System.Windows.Forms.Label();
            this.btnCalcular = new System.Windows.Forms.Button();
            this.lblValor = new System.Windows.Forms.Label();
            this.txtValor = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // lblTitulo
            // 
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Location = new System.Drawing.Point(315, 9);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(107, 13);
            this.lblTitulo.TabIndex = 0;
            this.lblTitulo.Text = "Conversor de valores";
            this.lblTitulo.Click += new System.EventHandler(this.label1_Click_1);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(50, 67);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 13);
            this.label1.TabIndex = 1;
            // 
            // lblTipoMoneda
            // 
            this.lblTipoMoneda.AutoSize = true;
            this.lblTipoMoneda.Location = new System.Drawing.Point(53, 66);
            this.lblTipoMoneda.Name = "lblTipoMoneda";
            this.lblTipoMoneda.Size = new System.Drawing.Size(150, 13);
            this.lblTipoMoneda.TabIndex = 2;
            this.lblTipoMoneda.Text = "Seleccione el tipo de moneda:";
            // 
            // lblConvertirMoneda
            // 
            this.lblConvertirMoneda.AutoSize = true;
            this.lblConvertirMoneda.Location = new System.Drawing.Point(336, 67);
            this.lblConvertirMoneda.Name = "lblConvertirMoneda";
            this.lblConvertirMoneda.Size = new System.Drawing.Size(61, 13);
            this.lblConvertirMoneda.TabIndex = 3;
            this.lblConvertirMoneda.Text = "Convertir a:";
            // 
            // lblTotalMoneda
            // 
            this.lblTotalMoneda.AutoSize = true;
            this.lblTotalMoneda.Location = new System.Drawing.Point(533, 66);
            this.lblTotalMoneda.Name = "lblTotalMoneda";
            this.lblTotalMoneda.Size = new System.Drawing.Size(34, 13);
            this.lblTotalMoneda.TabIndex = 4;
            this.lblTotalMoneda.Text = "Total:";
            // 
            // cboTipoMoneda
            // 
            this.cboTipoMoneda.FormattingEnabled = true;
            this.cboTipoMoneda.Items.AddRange(new object[] {
            "Dólar estadounidense (USD)",
            "Euro (EUR) ",
            "Yen japonés (JPY)",
            "Libra esterlina (GBP) ",
            "Peso mexicano (MXN) ",
            "Real brasileño (BRL) ",
            "Won surcoreano (KRW) ",
            "Yuan renminbi (CNY) ",
            "Rupia india (INR) ",
            "Colón salvadoreño (SVC)"});
            this.cboTipoMoneda.Location = new System.Drawing.Point(209, 63);
            this.cboTipoMoneda.Name = "cboTipoMoneda";
            this.cboTipoMoneda.Size = new System.Drawing.Size(121, 21);
            this.cboTipoMoneda.TabIndex = 5;
            // 
            // cboConvertirMoneda
            // 
            this.cboConvertirMoneda.FormattingEnabled = true;
            this.cboConvertirMoneda.Items.AddRange(new object[] {
            "Dólar estadounidense (USD)",
            "Euro (EUR) ",
            "Yen japonés (JPY)",
            "Libra esterlina (GBP) ",
            "Peso mexicano (MXN) ",
            "Real brasileño (BRL) ",
            "Won surcoreano (KRW) ",
            "Yuan renminbi (CNY) ",
            "Rupia india (INR) ",
            "Colón salvadoreño (SVC)"});
            this.cboConvertirMoneda.Location = new System.Drawing.Point(403, 63);
            this.cboConvertirMoneda.Name = "cboConvertirMoneda";
            this.cboConvertirMoneda.Size = new System.Drawing.Size(121, 21);
            this.cboConvertirMoneda.TabIndex = 6;
            // 
            // cboConvertirMasa
            // 
            this.cboConvertirMasa.FormattingEnabled = true;
            this.cboConvertirMasa.Items.AddRange(new object[] {
            "Kilogramo (kg)",
            "Gramo (g)",
            "Miligramo (mg)",
            "Microgramo (µg)",
            "Tonelada (t)",
            "Libra (lb)",
            "Onza (oz)",
            "Quintal (qq)",
            "Stone (st)",
            "Dalton (Da)"});
            this.cboConvertirMasa.Location = new System.Drawing.Point(403, 97);
            this.cboConvertirMasa.Name = "cboConvertirMasa";
            this.cboConvertirMasa.Size = new System.Drawing.Size(121, 21);
            this.cboConvertirMasa.TabIndex = 11;
            // 
            // cboTipoMasa
            // 
            this.cboTipoMasa.FormattingEnabled = true;
            this.cboTipoMasa.Items.AddRange(new object[] {
            "Kilogramo (kg)",
            "Gramo (g)",
            "Miligramo (mg)",
            "Microgramo (µg)",
            "Tonelada (t)",
            "Libra (lb)",
            "Onza (oz)",
            "Quintal (qq)",
            "Stone (st)",
            "Dalton (Da)"});
            this.cboTipoMasa.Location = new System.Drawing.Point(209, 97);
            this.cboTipoMasa.Name = "cboTipoMasa";
            this.cboTipoMasa.Size = new System.Drawing.Size(121, 21);
            this.cboTipoMasa.TabIndex = 10;
            // 
            // lblTotalMasa
            // 
            this.lblTotalMasa.AutoSize = true;
            this.lblTotalMasa.Location = new System.Drawing.Point(533, 100);
            this.lblTotalMasa.Name = "lblTotalMasa";
            this.lblTotalMasa.Size = new System.Drawing.Size(34, 13);
            this.lblTotalMasa.TabIndex = 9;
            this.lblTotalMasa.Text = "Total:";
            // 
            // lblConvertirMasa
            // 
            this.lblConvertirMasa.AutoSize = true;
            this.lblConvertirMasa.Location = new System.Drawing.Point(336, 101);
            this.lblConvertirMasa.Name = "lblConvertirMasa";
            this.lblConvertirMasa.Size = new System.Drawing.Size(61, 13);
            this.lblConvertirMasa.TabIndex = 8;
            this.lblConvertirMasa.Text = "Convertir a:";
            // 
            // lblTipoMasa
            // 
            this.lblTipoMasa.AutoSize = true;
            this.lblTipoMasa.Location = new System.Drawing.Point(53, 97);
            this.lblTipoMasa.Name = "lblTipoMasa";
            this.lblTipoMasa.Size = new System.Drawing.Size(137, 13);
            this.lblTipoMasa.TabIndex = 7;
            this.lblTipoMasa.Text = "Seleccione el tipo de masa:";
            // 
            // cboConvertirVolumen
            // 
            this.cboConvertirVolumen.FormattingEnabled = true;
            this.cboConvertirVolumen.Items.AddRange(new object[] {
            "Metro cúbico (m³)",
            "Decímetro cúbico (dm³)",
            "Centímetro cúbico (cm³ o cc)",
            "Milímetro cúbico (mm³)",
            "Litro (L)",
            "Mililitro (mL)",
            "Galón (gal)",
            "Pinta (pt) ",
            "Cuarto (qt o quart) ",
            "Onza líquida (fl oz)"});
            this.cboConvertirVolumen.Location = new System.Drawing.Point(403, 134);
            this.cboConvertirVolumen.Name = "cboConvertirVolumen";
            this.cboConvertirVolumen.Size = new System.Drawing.Size(121, 21);
            this.cboConvertirVolumen.TabIndex = 16;
            // 
            // cboTipoVolumen
            // 
            this.cboTipoVolumen.FormattingEnabled = true;
            this.cboTipoVolumen.Items.AddRange(new object[] {
            "Metro cúbico (m³)",
            "Decímetro cúbico (dm³)",
            "Centímetro cúbico (cm³ o cc)",
            "Milímetro cúbico (mm³)",
            "Litro (L)",
            "Mililitro (mL)",
            "Galón (gal)",
            "Pinta (pt) ",
            "Cuarto (qt o quart) ",
            "Onza líquida (fl oz)"});
            this.cboTipoVolumen.Location = new System.Drawing.Point(209, 134);
            this.cboTipoVolumen.Name = "cboTipoVolumen";
            this.cboTipoVolumen.Size = new System.Drawing.Size(121, 21);
            this.cboTipoVolumen.TabIndex = 15;
            // 
            // lblTotalVolumen
            // 
            this.lblTotalVolumen.AutoSize = true;
            this.lblTotalVolumen.Location = new System.Drawing.Point(533, 137);
            this.lblTotalVolumen.Name = "lblTotalVolumen";
            this.lblTotalVolumen.Size = new System.Drawing.Size(34, 13);
            this.lblTotalVolumen.TabIndex = 14;
            this.lblTotalVolumen.Text = "Total:";
            // 
            // lblConvertirVolumen
            // 
            this.lblConvertirVolumen.AutoSize = true;
            this.lblConvertirVolumen.Location = new System.Drawing.Point(336, 138);
            this.lblConvertirVolumen.Name = "lblConvertirVolumen";
            this.lblConvertirVolumen.Size = new System.Drawing.Size(61, 13);
            this.lblConvertirVolumen.TabIndex = 13;
            this.lblConvertirVolumen.Text = "Convertir a:";
            // 
            // lblTipoVolumen
            // 
            this.lblTipoVolumen.AutoSize = true;
            this.lblTipoVolumen.Location = new System.Drawing.Point(53, 134);
            this.lblTipoVolumen.Name = "lblTipoVolumen";
            this.lblTipoVolumen.Size = new System.Drawing.Size(152, 13);
            this.lblTipoVolumen.TabIndex = 12;
            this.lblTipoVolumen.Text = "Seleccione el tipo de volumen:";
            // 
            // cboConvertirLongitud
            // 
            this.cboConvertirLongitud.FormattingEnabled = true;
            this.cboConvertirLongitud.Items.AddRange(new object[] {
            "Kilómetro (km)",
            "Metro (m)",
            "Decímetro (dm)",
            "Centímetro (cm)",
            "Milímetro (mm)",
            "Micrómetro (µm o micra)",
            "Nanómetro (nm)",
            "Milla (mi)",
            "Yarda (yd)",
            "Pulgada (in)"});
            this.cboConvertirLongitud.Location = new System.Drawing.Point(403, 165);
            this.cboConvertirLongitud.Name = "cboConvertirLongitud";
            this.cboConvertirLongitud.Size = new System.Drawing.Size(121, 21);
            this.cboConvertirLongitud.TabIndex = 21;
            // 
            // cboTipoLongitud
            // 
            this.cboTipoLongitud.FormattingEnabled = true;
            this.cboTipoLongitud.Items.AddRange(new object[] {
            "Kilómetro (km)",
            "Metro (m)",
            "Decímetro (dm)",
            "Centímetro (cm)",
            "Milímetro (mm)",
            "Micrómetro (µm o micra)",
            "Nanómetro (nm)",
            "Milla (mi)",
            "Yarda (yd)",
            "Pulgada (in)"});
            this.cboTipoLongitud.Location = new System.Drawing.Point(209, 165);
            this.cboTipoLongitud.Name = "cboTipoLongitud";
            this.cboTipoLongitud.Size = new System.Drawing.Size(121, 21);
            this.cboTipoLongitud.TabIndex = 20;
            this.cboTipoLongitud.SelectedIndexChanged += new System.EventHandler(this.cboTipoLongitud_SelectedIndexChanged);
            // 
            // lblTotalLongitud
            // 
            this.lblTotalLongitud.AutoSize = true;
            this.lblTotalLongitud.Location = new System.Drawing.Point(533, 168);
            this.lblTotalLongitud.Name = "lblTotalLongitud";
            this.lblTotalLongitud.Size = new System.Drawing.Size(34, 13);
            this.lblTotalLongitud.TabIndex = 19;
            this.lblTotalLongitud.Text = "Total:";
            // 
            // lblConvertirLongitud
            // 
            this.lblConvertirLongitud.AutoSize = true;
            this.lblConvertirLongitud.Location = new System.Drawing.Point(336, 169);
            this.lblConvertirLongitud.Name = "lblConvertirLongitud";
            this.lblConvertirLongitud.Size = new System.Drawing.Size(61, 13);
            this.lblConvertirLongitud.TabIndex = 18;
            this.lblConvertirLongitud.Text = "Convertir a:";
            // 
            // lblTipoLongitud
            // 
            this.lblTipoLongitud.AutoSize = true;
            this.lblTipoLongitud.Location = new System.Drawing.Point(53, 165);
            this.lblTipoLongitud.Name = "lblTipoLongitud";
            this.lblTipoLongitud.Size = new System.Drawing.Size(149, 13);
            this.lblTipoLongitud.TabIndex = 17;
            this.lblTipoLongitud.Text = "Seleccione el tipo de longitud:";
            // 
            // btnCalcular
            // 
            this.btnCalcular.Location = new System.Drawing.Point(248, 297);
            this.btnCalcular.Name = "btnCalcular";
            this.btnCalcular.Size = new System.Drawing.Size(239, 48);
            this.btnCalcular.TabIndex = 32;
            this.btnCalcular.Text = "CALCULAR";
            this.btnCalcular.UseVisualStyleBackColor = true;
            this.btnCalcular.Click += new System.EventHandler(this.btnCalcular_Click);
            // 
            // lblValor
            // 
            this.lblValor.AutoSize = true;
            this.lblValor.Location = new System.Drawing.Point(53, 30);
            this.lblValor.Name = "lblValor";
            this.lblValor.Size = new System.Drawing.Size(82, 13);
            this.lblValor.TabIndex = 33;
            this.lblValor.Text = "Escriba el valor:";
            // 
            // txtValor
            // 
            this.txtValor.Location = new System.Drawing.Point(157, 27);
            this.txtValor.Name = "txtValor";
            this.txtValor.Size = new System.Drawing.Size(100, 20);
            this.txtValor.TabIndex = 34;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(797, 418);
            this.Controls.Add(this.txtValor);
            this.Controls.Add(this.lblValor);
            this.Controls.Add(this.btnCalcular);
            this.Controls.Add(this.cboConvertirLongitud);
            this.Controls.Add(this.cboTipoLongitud);
            this.Controls.Add(this.lblTotalLongitud);
            this.Controls.Add(this.lblConvertirLongitud);
            this.Controls.Add(this.lblTipoLongitud);
            this.Controls.Add(this.cboConvertirVolumen);
            this.Controls.Add(this.cboTipoVolumen);
            this.Controls.Add(this.lblTotalVolumen);
            this.Controls.Add(this.lblConvertirVolumen);
            this.Controls.Add(this.lblTipoVolumen);
            this.Controls.Add(this.cboConvertirMasa);
            this.Controls.Add(this.cboTipoMasa);
            this.Controls.Add(this.lblTotalMasa);
            this.Controls.Add(this.lblConvertirMasa);
            this.Controls.Add(this.lblTipoMasa);
            this.Controls.Add(this.cboConvertirMoneda);
            this.Controls.Add(this.cboTipoMoneda);
            this.Controls.Add(this.lblTotalMoneda);
            this.Controls.Add(this.lblConvertirMoneda);
            this.Controls.Add(this.lblTipoMoneda);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblTitulo);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblTipoMoneda;
        private System.Windows.Forms.Label lblConvertirMoneda;
        private System.Windows.Forms.Label lblTotalMoneda;
        private System.Windows.Forms.ComboBox cboTipoMoneda;
        private System.Windows.Forms.ComboBox cboConvertirMoneda;
        private System.Windows.Forms.ComboBox cboConvertirMasa;
        private System.Windows.Forms.ComboBox cboTipoMasa;
        private System.Windows.Forms.Label lblTotalMasa;
        private System.Windows.Forms.Label lblConvertirMasa;
        private System.Windows.Forms.Label lblTipoMasa;
        private System.Windows.Forms.ComboBox cboConvertirVolumen;
        private System.Windows.Forms.ComboBox cboTipoVolumen;
        private System.Windows.Forms.Label lblTotalVolumen;
        private System.Windows.Forms.Label lblConvertirVolumen;
        private System.Windows.Forms.Label lblTipoVolumen;
        private System.Windows.Forms.ComboBox cboConvertirLongitud;
        private System.Windows.Forms.ComboBox cboTipoLongitud;
        private System.Windows.Forms.Label lblTotalLongitud;
        private System.Windows.Forms.Label lblConvertirLongitud;
        private System.Windows.Forms.Label lblTipoLongitud;
        private System.Windows.Forms.Button btnCalcular;
        private System.Windows.Forms.Label lblValor;
        private System.Windows.Forms.TextBox txtValor;
    }
}

