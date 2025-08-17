using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MiPrimerProyectoEnCSharp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

      

        private void optSuma_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }

        private void cboTipoLongitud_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cboTipoTiempo_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnCalcular_Click(object sender, EventArgs e)
        {
            double valor = double.Parse(txtValor.Text);
            double total= 0;

            switch (cboTipoMoneda.SelectedIndex)
            {

                //PARA LAS MONEDAS ESTADOUNIDENSEEEESS
                case 0:
                    switch (cboConvertirMoneda.SelectedIndex)
                    {
                        case 0:
                            total = valor;
                        break;

                        case 1:
                            total = valor * 0.854;
                        break;
                        case 2:
                            total = valor * 147.5;
                        break;

                        case 3:
                            total = valor * 0.737;
                        break;

                        case 4:
                            total = valor * 18.7;
                        break;
                        case 5:
                            total = valor * 5.44;
                            break;
                        case 6:
                            total = valor * 1384;
                        break;

                        case 7:
                            total = valor * 7.18;
                            break;
                        case 8:
                            total = valor * 87.6;
                            break;
                        case 9:
                            total = valor * 8.75;
                            break;
                    }
                        
                break;

                //Para los euros
                case 1:

                    switch (cboConvertirMoneda.SelectedIndex)
                    {
                        case 0:
                            total = valor * 1.1705;
                            break;

                        case 1:
                            total = valor;
                            break;
                        case 2:
                            total = valor * 172.72;
                            break;

                        case 3:
                            total = valor * 0.0862;
                            break;

                        case 4:
                            total = valor * 21.90;
                            break;
                        case 5:
                            total = valor * 6.315;
                            break;
                        case 6:
                            total = valor * 1625.77;
                            break;

                        case 7:
                            total = valor * 8.4065;
                            break;
                        case 8:
                            total = valor * 102.43;
                            break;
                        case 9:
                            total = valor * 8.75;
                            break;
                    }

                    break;



                case 2:


                    switch (cboConvertirMoneda.SelectedIndex)
                    {
                        case 0:
                            total = valor * 0.0068;
                            break;

                        case 1:
                            total = valor * 0.0058;
                            break;
                        case 2:
                            total = valor;
                            break;

                        case 3:
                            total = valor * 0.0049;
                            break;

                        case 4:
                            total = valor * 0.1261;
                            break;
                        case 5:
                            total = valor * 0.0376;
                            break;
                        case 6:
                            total = valor * 10.64;
                            break;

                        case 7:
                            total = valor * 0.0483;
                            break;
                        case 8:
                            total = valor * 0.5789;
                            break;
                        case 9:
                            total = valor * 0.059;
                            break;
                    }

                    break;

                case 3:


                    switch (cboConvertirMoneda.SelectedIndex)
                    {
                        case 0:
                            total = valor * 1.3549;
                            break;

                        case 1:
                            total = valor * 1.20161;
                            break;
                        case 2:
                            total = valor * 200.0;
                            break;

                        case 3:
                            total = valor;
                            break;

                        case 4:
                            total = valor * 24.843;
                            break;
                        case 5:
                            total = valor * 7.309;
                            break;
                        case 6:
                            total = valor * 1867;
                            break;

                        case 7:
                            total = valor * 9.734;
                            break;
                        case 8:
                            total = valor * 118.12;
                            break;
                        case 9:
                            total = valor * 8.75;
                            break;
                    }

                    break;

                case 4: // Peso mexicano (MXN)
                    switch (cboConvertirMoneda.SelectedIndex)
                    {
                        case 0:
                            total = valor * 0.0535; 
                            break;  // USD
                        case 1:
                            total = valor * 0.0457;
                            break;  // EUR
                        case 2:
                            total = valor * 7.92;
                            break;    // JPY
                        case 3: 
                            total = valor * 0.0402; 
                            break;  // GBP
                        case 4: 
                            total = valor;
                            break;           // MXN
                        case 5:
                            total = valor * 0.294;
                            break;   // BRL
                        case 6:
                            total = valor * 57.0; 
                            break;    // KRW
                        case 7:
                            total = valor * 0.384; 
                            break;   // CNY
                        case 8: 
                            total = valor * 5.19;
                            break;    // INR
                        case 9:
                            total = valor * 0.468;
                            break;   // SVC
                    }
                    break;

                case 5: // Real brasileño (BRL)
                    switch (cboConvertirMoneda.SelectedIndex)
                    {
                        case 0:
                            total = valor * 0.184;
                            break;   // USD
                        case 1:
                            total = valor * 0.159;
                            break;   // EUR
                        case 2:
                            total = valor * 27.3; break;    // JPY
                        case 3:
                            total = valor * 0.137; break;   // GBP
                        case 4:
                            total = valor * 3.40; break;    // MXN
                        case 5:
                            total = valor; break;           // BRL
                        case 6:
                            total = valor * 194.0; break;   // KRW
                        case 7:
                            total = valor * 1.33; break;    // CNY
                        case 8:
                            total = valor * 17.7; break;    // INR
                        case 9:
                            total = valor * 1.38; break;    // SVC
                    }
                    break;

                case 6: // Won surcoreano (KRW)
                    switch (cboConvertirMoneda.SelectedIndex)
                    {
                        case 0:
                            total = valor * 0.00072; break;  // USD
                        case 1:
                            total = valor * 0.00058; break;  // EUR
                        case 2:
                            total = valor * 0.097; break;    // JPY
                        case 3:
                            total = valor * 0.00054; break;  // GBP
                        case 4:
                            total = valor * 0.0175; break;   // MXN
                        case 5:
                            total = valor * 0.00515; break;  // BRL
                        case 6:
                            total = valor; break;            // KRW
                        case 7:
                            total = valor * 0.00517; break;  // CNY
                        case 8:
                            total = valor * 0.060; break;    // INR
                        case 9:
                            total = valor * 0.00538; break;  // SVC
                    }
                    break;

                case 7: // Yuan renminbi (CNY)
                    switch (cboConvertirMoneda.SelectedIndex)
                    {
                        case 0: 
                            total = valor * 0.14; break;     // USD
                        case 1:
                            total = valor * 0.119; break;    // EUR
                        case 2:
                            total = valor * 20.5; break;     // JPY
                        case 3:
                            total = valor * 0.103; break;    // GBP
                        case 4:
                            total = valor * 2.60; break;     // MXN
                        case 5:
                            total = valor * 0.75; break;     // BRL
                        case 6:
                            total = valor * 193.5; break;    // KRW
                        case 7:
                            total = valor; break;            // CNY
                        case 8:
                            total = valor * 13.3; break;     // INR
                        case 9:
                            total = valor * 1.04; break;     // SVC
                    }
                    break;

                case 8: // Rupia india (INR)
                    switch (cboConvertirMoneda.SelectedIndex)
                    {
                        case 0:
                            total = valor * 0.0114; break;  // USD
                        case 1:
                            total = valor * 0.00975; break; // EUR
                        case 2:
                            total = valor * 1.73; break;    // JPY
                        case 3: 
                            total = valor * 0.0085; break;  // GBP
                        case 4:
                            total = valor * 0.193; break;   // MXN
                        case 5:
                            total = valor * 0.0565; break;  // BRL
                        case 6:
                            total = valor * 16.4; break;    // KRW
                        case 7:
                            total = valor * 0.075; break;   // CNY
                        case 8:
                            total = valor; break;           // INR
                        case 9:
                            total = valor * 0.085; break;   // SVC
                    }
                    break;

                case 9: // Colón salvadoreño (SVC)
                    switch (cboConvertirMoneda.SelectedIndex)
                    {
                        case 0:
                            total = valor * 0.114; break; // USD (fijo)
                        case 1:
                            total = valor * 0.093; break; // EUR
                        case 2:
                            total = valor * 17.0; break;  // JPY
                        case 3:
                            total = valor * 0.114; break; // GBP
                        case 4:
                            total = valor * 2.14; break;  // MXN
                        case 5:
                            total = valor * 0.724; break; // BRL
                        case 6:
                            total = valor * 186.0; break; // KRW
                        case 7:
                            total = valor * 0.962; break; // CNY
                        case 8:
                            total = valor * 11.8; break;  // INR
                        case 9:
                            total = valor; break;         
                    }
                    break;


            }

            lblTotalMoneda.Text = "Total : " + total;

            switch (cboTipoMasa.SelectedIndex)
            {
                case 0:
                    switch (cboConvertirMasa.SelectedIndex)
                    {
                        case 0: total = valor; break;
                        case 1: total = valor * 1000; break;
                        case 2: total = valor * 1e6; break;
                        case 3: total = valor * 1e9; break;
                        case 4: total = valor / 0.001; break;
                        case 5: total = valor / 0.453592; break;
                        case 6: total = valor / 0.0283495; break;
                        case 7: total = valor / 100; break;
                        case 8: total = valor / 6.35029; break;
                        case 9: total = valor / 1.660539e-27; break;
                    }
                    break;

                case 1:
                    switch (cboConvertirMasa.SelectedIndex)
                    {
                        case 0: total = valor / 1000; break;
                        case 1: total = valor; break;
                        case 2: total = valor * 1000; break;
                        case 3: total = valor * 1e6; break;
                        case 4: total = valor / 1e6; break;
                        case 5: total = valor / 453.592; break;
                        case 6: total = valor / 28.3495; break;
                        case 7: total = valor / 100000; break;
                        case 8: total = valor / 6350.29; break;
                        case 9: total = valor / 1.660539e-24; break;
                    }
                    break;

                case 2:
                    switch (cboConvertirMasa.SelectedIndex)
                    {
                        case 0: total = valor / 1e6; break;
                        case 1: total = valor / 1000; break;
                        case 2: total = valor; break;
                        case 3: total = valor * 1000; break;
                        case 4: total = valor / 1e9; break;
                        case 5: total = valor / 453592; break;
                        case 6: total = valor / 28349.5; break;
                        case 7: total = valor / 1e8; break;
                        case 8: total = valor / 6.35029e6; break;
                        case 9: total = valor / 1.660539e-21; break;
                    }
                    break;

                case 3:
                    switch (cboConvertirMasa.SelectedIndex)
                    {
                        case 0: total = valor / 1e9; break;
                        case 1: total = valor / 1e6; break;
                        case 2: total = valor / 1000; break;
                        case 3: total = valor; break;
                        case 4: total = valor / 1e12; break;
                        case 5: total = valor / 4.53592e8; break;
                        case 6: total = valor / 2.83495e7; break;
                        case 7: total = valor / 1e11; break;
                        case 8: total = valor / 6.35029e9; break;
                        case 9: total = valor / 1.660539e-18; break;
                    }
                    break;

                case 4:
                    switch (cboConvertirMasa.SelectedIndex)
                    {
                        case 0: total = valor * 1000; break;
                        case 1: total = valor * 1e6; break;
                        case 2: total = valor * 1e9; break;
                        case 3: total = valor * 1e12; break;
                        case 4: total = valor; break;
                        case 5: total = valor * 2204.62; break;
                        case 6: total = valor * 35273.96; break;
                        case 7: total = valor * 10; break;
                        case 8: total = valor * 157.473; break;
                        case 9: total = valor / 1.660539e-24; break;
                    }
                    break;

                case 5:
                    switch (cboConvertirMasa.SelectedIndex)
                    {
                        case 0: total = valor * 0.453592; break;
                        case 1: total = valor * 453.592; break;
                        case 2: total = valor * 453592; break;
                        case 3: total = valor * 4.53592e8; break;
                        case 4: total = valor * 0.000453592; break;
                        case 5: total = valor; break;
                        case 6: total = valor * 16; break;
                        case 7: total = valor * 0.0453592; break;
                        case 8: total = valor * 0.0714286; break;
                        case 9: total = valor / 2.743e-27; break;
                    }
                    break;

                case 6:
                    switch (cboConvertirMasa.SelectedIndex)
                    {
                        case 0: total = valor * 0.0283495; break;
                        case 1: total = valor * 28.3495; break;
                        case 2: total = valor * 28349.5; break;
                        case 3: total = valor * 2.83495e7; break;
                        case 4: total = valor * 0.0000283495; break;
                        case 5: total = valor / 16; break;
                        case 6: total = valor; break;
                        case 7: total = valor * 0.00283495; break;
                        case 8: total = valor * 0.0044643; break;
                        case 9: total = valor / 5.827e-29; break;
                    }
                    break;

                case 7:
                    switch (cboConvertirMasa.SelectedIndex)
                    {
                        case 0: total = valor * 100; break;
                        case 1: total = valor * 100000; break;
                        case 2: total = valor * 1e8; break;
                        case 3: total = valor * 1e11; break;
                        case 4: total = valor * 10; break;
                        case 5: total = valor * 220.462; break;
                        case 6: total = valor * 3527.396; break;
                        case 7: total = valor; break;
                        case 8: total = valor * 15.7473; break;
                        case 9: total = valor / 1.660539e-25; break;
                    }
                    break;

                case 8:
                    switch (cboConvertirMasa.SelectedIndex)
                    {
                        case 0: total = valor * 6.35029; break;
                        case 1: total = valor * 6350.29; break;
                        case 2: total = valor * 6.35029e6; break;
                        case 3: total = valor * 6.35029e9; break;
                        case 4: total = valor * 0.00635029; break;
                        case 5: total = valor * 14; break;
                        case 6: total = valor * 224; break;
                        case 7: total = valor * 0.0635029; break;
                        case 8: total = valor; break;
                        case 9: total = valor / 1.660539e-26; break;
                    }
                    break;

                case 9:
                    switch (cboConvertirMasa.SelectedIndex)
                    {
                        case 0: total = valor * 1.660539e-27; break;
                        case 1: total = valor * 1.660539e-24; break;
                        case 2: total = valor * 1.660539e-21; break;
                        case 3: total = valor * 1.660539e-18; break;
                        case 4: total = valor * 1.660539e-30; break;
                        case 5: total = valor * 7.507e-27; break;
                        case 6: total = valor * 1.660539e-28; break;
                        case 7: total = valor * 1.660539e-29; break;
                        case 8: total = valor * 1.660539e-28; break;
                        case 9: total = valor; break;
                    }
                    break;


            }

            lblTotalMasa.Text = "Total : " + total;

            switch (cboTipoVolumen.SelectedIndex)
            {
                case 0:
                    switch (cboConvertirVolumen.SelectedIndex)
                    {
                        case 0: total = valor; break;
                        case 1: total = valor * 1000; break;
                        case 2: total = valor * 1e6; break;
                        case 3: total = valor * 1e9; break;
                        case 4: total = valor * 1000; break;
                        case 5: total = valor * 1e6; break;
                        case 6: total = valor / 0.00378541; break;
                        case 7: total = valor / 0.000473176; break;
                        case 8: total = valor / 0.000946353; break;
                        case 9: total = valor / 2.9574e-5; break;
                    }
                    break;

                case 1:
                    switch (cboConvertirVolumen.SelectedIndex)
                    {
                        case 0: total = valor / 1000; break;
                        case 1: total = valor; break;
                        case 2: total = valor * 1000; break;
                        case 3: total = valor * 1e6; break;
                        case 4: total = valor; break;
                        case 5: total = valor * 1000; break;
                        case 6: total = valor / 3.78541; break;
                        case 7: total = valor / 0.473176; break;
                        case 8: total = valor / 0.946353; break;
                        case 9: total = valor / 0.029574; break;
                    }
                    break;

                case 2:
                    switch (cboConvertirVolumen.SelectedIndex)
                    {
                        case 0: total = valor / 1e6; break;
                        case 1: total = valor / 1000; break;
                        case 2: total = valor; break;
                        case 3: total = valor * 1000; break;
                        case 4: total = valor / 1000; break;
                        case 5: total = valor; break;
                        case 6: total = valor / 3785.41; break;
                        case 7: total = valor / 473.176; break;
                        case 8: total = valor / 946.353; break;
                        case 9: total = valor / 29.574; break;
                    }
                    break;

                case 3:
                    switch (cboConvertirVolumen.SelectedIndex)
                    {
                        case 0: total = valor / 1e9; break;
                        case 1: total = valor / 1e6; break;
                        case 2: total = valor / 1000; break;
                        case 3: total = valor; break;
                        case 4: total = valor / 1e6; break;
                        case 5: total = valor / 1000; break;
                        case 6: total = valor / 3.78541e6; break;
                        case 7: total = valor / 473176; break;
                        case 8: total = valor / 946353; break;
                        case 9: total = valor / 29574; break;
                    }
                    break;

                case 4:
                    switch (cboConvertirVolumen.SelectedIndex)
                    {
                        case 0: total = valor / 1000; break;
                        case 1: total = valor; break;
                        case 2: total = valor * 1000; break;
                        case 3: total = valor * 1e6; break;
                        case 4: total = valor; break;
                        case 5: total = valor * 1000; break;
                        case 6: total = valor / 3.78541; break;
                        case 7: total = valor / 0.473176; break;
                        case 8: total = valor / 0.946353; break;
                        case 9: total = valor / 0.029574; break;
                    }
                    break;

                case 5:
                    switch (cboConvertirVolumen.SelectedIndex)
                    {
                        case 0: total = valor / 1e6; break;
                        case 1: total = valor / 1000; break;
                        case 2: total = valor; break;
                        case 3: total = valor * 1000; break;
                        case 4: total = valor / 1000; break;
                        case 5: total = valor; break;
                        case 6: total = valor / 3785.41; break;
                        case 7: total = valor / 473.176; break;
                        case 8: total = valor / 946.353; break;
                        case 9: total = valor / 29.574; break;
                    }
                    break;

                case 6:
                    switch (cboConvertirVolumen.SelectedIndex)
                    {
                        case 0: total = valor * 0.00378541; break;
                        case 1: total = valor * 3.78541; break;
                        case 2: total = valor * 3785.41; break;
                        case 3: total = valor * 3.78541e6; break;
                        case 4: total = valor * 3.78541; break;
                        case 5: total = valor * 3785.41; break;
                        case 6: total = valor; break;
                        case 7: total = valor * 8; break;
                        case 8: total = valor * 4; break;
                        case 9: total = valor * 128; break;
                    }
                    break;

                case 7:
                    switch (cboConvertirVolumen.SelectedIndex)
                    {
                        case 0: total = valor * 0.000473176; break;
                        case 1: total = valor * 0.473176; break;
                        case 2: total = valor * 473.176; break;
                        case 3: total = valor * 473176; break;
                        case 4: total = valor * 0.473176; break;
                        case 5: total = valor * 473.176; break;
                        case 6: total = valor / 8; break;
                        case 7: total = valor; break;
                        case 8: total = valor / 2; break;
                        case 9: total = valor * 16; break;
                    }
                    break;

                case 8:
                    switch (cboConvertirVolumen.SelectedIndex)
                    {
                        case 0: total = valor * 0.000946353; break;
                        case 1: total = valor * 0.946353; break;
                        case 2: total = valor * 946.353; break;
                        case 3: total = valor * 946353; break;
                        case 4: total = valor * 0.946353; break;
                        case 5: total = valor * 946.353; break;
                        case 6: total = valor / 4; break;
                        case 7: total = valor * 2; break;
                        case 8: total = valor; break;
                        case 9: total = valor * 32; break;
                    }
                    break;

                case 9:
                    switch (cboConvertirVolumen.SelectedIndex)
                    {
                        case 0: total = valor * 2.9574e-5; break;
                        case 1: total = valor * 2.9574e-2; break;
                        case 2: total = valor * 29.574; break;
                        case 3: total = valor * 29574; break;
                        case 4: total = valor * 0.029574; break;
                        case 5: total = valor * 29.574; break;
                        case 6: total = valor / 128; break;
                        case 7: total = valor / 16; break;
                        case 8: total = valor / 32; break;
                        case 9: total = valor; break;
                    }
                    break;
            }

            lblTotalVolumen.Text = "Total : " + total;

            switch (cboTipoLongitud.SelectedIndex)
            {
                case 0:
                    switch (cboConvertirLongitud.SelectedIndex)
                    {
                        case 0: total = valor; break;
                        case 1: total = valor * 1000; break;
                        case 2: total = valor * 10000; break;
                        case 3: total = valor * 100000; break;
                        case 4: total = valor * 1e6; break;
                        case 5: total = valor * 1e9; break;
                        case 6: total = valor * 1e12; break;
                        case 7: total = valor / 0.621371; break;
                        case 8: total = valor * 1093.61; break;
                        case 9: total = valor * 39370.1; break;
                    }
                    break;

                case 1:
                    switch (cboConvertirLongitud.SelectedIndex)
                    {
                        case 0: total = valor / 1000; break;
                        case 1: total = valor; break;
                        case 2: total = valor * 10; break;
                        case 3: total = valor * 100; break;
                        case 4: total = valor * 1000; break;
                        case 5: total = valor * 1e6; break;
                        case 6: total = valor * 1e9; break;
                        case 7: total = valor / 1609.34; break;
                        case 8: total = valor / 0.9144; break;
                        case 9: total = valor / 0.0254; break;
                    }
                    break;

                case 2:
                    switch (cboConvertirLongitud.SelectedIndex)
                    {
                        case 0: total = valor / 10000; break;
                        case 1: total = valor / 10; break;
                        case 2: total = valor; break;
                        case 3: total = valor * 10; break;
                        case 4: total = valor * 100; break;
                        case 5: total = valor * 10000; break;
                        case 6: total = valor * 1e7; break;
                        case 7: total = valor / 160934; break;
                        case 8: total = valor / 0.09144; break;
                        case 9: total = valor / 0.00254; break;
                    }
                    break;

                case 3:
                    switch (cboConvertirLongitud.SelectedIndex)
                    {
                        case 0: total = valor / 100000; break;
                        case 1: total = valor / 100; break;
                        case 2: total = valor / 10; break;
                        case 3: total = valor; break;
                        case 4: total = valor * 10; break;
                        case 5: total = valor * 10000; break;
                        case 6: total = valor * 1e7; break;
                        case 7: total = valor / 1609340; break;
                        case 8: total = valor / 0.009144; break;
                        case 9: total = valor / 0.000254; break;
                    }
                    break;

                case 4:
                    switch (cboConvertirLongitud.SelectedIndex)
                    {
                        case 0: total = valor / 1e6; break;
                        case 1: total = valor / 1000; break;
                        case 2: total = valor / 100; break;
                        case 3: total = valor / 10; break;
                        case 4: total = valor; break;
                        case 5: total = valor * 1000; break;
                        case 6: total = valor * 1e6; break;
                        case 7: total = valor / 1.609e6; break;
                        case 8: total = valor / 914.4; break;
                        case 9: total = valor / 25.4; break;
                    }
                    break;

                case 5:
                    switch (cboConvertirLongitud.SelectedIndex)
                    {
                        case 0: total = valor / 1e9; break;
                        case 1: total = valor / 1e6; break;
                        case 2: total = valor / 10000; break;
                        case 3: total = valor / 1000; break;
                        case 4: total = valor / 1000; break;
                        case 5: total = valor; break;
                        case 6: total = valor * 1000; break;
                        case 7: total = valor / 1.609e9; break;
                        case 8: total = valor / 914400; break;
                        case 9: total = valor / 25400; break;
                    }
                    break;

                case 6:
                    switch (cboConvertirLongitud.SelectedIndex)
                    {
                        case 0: total = valor / 1e12; break;
                        case 1: total = valor / 1e9; break;
                        case 2: total = valor / 1e7; break;
                        case 3: total = valor / 1e6; break;
                        case 4: total = valor / 1e6; break;
                        case 5: total = valor / 1000; break;
                        case 6: total = valor; break;
                        case 7: total = valor / 1.609e12; break;
                        case 8: total = valor / 9.144e8; break;
                        case 9: total = valor / 2.54e7; break;
                    }
                    break;

                case 7:
                    switch (cboConvertirLongitud.SelectedIndex)
                    {
                        case 0: total = valor * 1.60934; break;
                        case 1: total = valor * 1609.34; break;
                        case 2: total = valor * 16093.4; break;
                        case 3: total = valor * 160934; break;
                        case 4: total = valor * 1609.34; break;
                        case 5: total = valor * 1.609e6; break;
                        case 6: total = valor * 1.609e9; break;
                        case 7: total = valor; break;
                        case 8: total = valor * 1760; break;
                        case 9: total = valor * 63360; break;
                    }
                    break;

                case 8:
                    switch (cboConvertirLongitud.SelectedIndex)
                    {
                        case 0: total = valor * 0.9144; break;
                        case 1: total = valor * 0.9144; break;
                        case 2: total = valor * 9.144; break;
                        case 3: total = valor * 91.44; break;
                        case 4: total = valor * 0.9144; break;
                        case 5: total = valor * 914.4; break;
                        case 6: total = valor * 9.144e5; break;
                        case 7: total = valor / 1760; break;
                        case 8: total = valor; break;
                        case 9: total = valor * 36; break;
                    }
                    break;

                case 9:
                    switch (cboConvertirLongitud.SelectedIndex)
                    {
                        case 0: total = valor * 0.0254; break;
                        case 1: total = valor * 0.0254; break;
                        case 2: total = valor * 0.254; break;
                        case 3: total = valor * 2.54; break;
                        case 4: total = valor * 0.0254; break;
                        case 5: total = valor * 25.4; break;
                        case 6: total = valor * 2.54e7; break;
                        case 7: total = valor / 63360; break;
                        case 8: total = valor / 36; break;
                        case 9: total = valor; break;
                    }
                    break;
            }
                lblTotalLongitud.Text = "Total : " + total;

            switch (cboTipoAlmacenamiento.SelectedIndex)
            {
                case 0:
                    switch (cboConvertirAlmacenamiento.SelectedIndex)
                    {
                        case 0: total = valor; break;
                        case 1: total = valor / 8; break;
                        case 2: total = valor / (8 * 1000); break;
                        case 3: total = valor / (8 * 1024); break;
                        case 4: total = valor / (8 * 1e6); break;
                        case 5: total = valor / (8 * 1024 * 1024); break;
                        case 6: total = valor / (8 * 1e9); break;
                        case 7: total = valor / (8L * 1024 * 1024 * 1024); break;
                        case 8: total = valor / (8 * 1e12); break;
                        case 9: total = valor / (8 * 1024L * 1024 * 1024 * 1024); break;
                    }
                    break;

                case 1:
                    switch (cboConvertirAlmacenamiento.SelectedIndex)
                    {
                        case 0: total = valor * 8; break;
                        case 1: total = valor; break;
                        case 2: total = valor / 1000; break;
                        case 3: total = valor / 1024; break;
                        case 4: total = valor / 1e6; break;
                        case 5: total = valor / (1024 * 1024); break;
                        case 6: total = valor / 1e9; break;
                        case 7: total = valor / (1024 * 1024 * 1024); break;
                        case 8: total = valor / 1e12; break;
                        case 9: total = valor / (1024L * 1024 * 1024 * 1024); break;
                    }
                    break;

                case 2:
                    switch (cboConvertirAlmacenamiento.SelectedIndex)
                    {
                        case 0: total = valor * 8000; break;
                        case 1: total = valor * 1000; break;
                        case 2: total = valor; break;
                        case 3: total = valor * 1000 / 1024; break;
                        case 4: total = valor / 1000; break;
                        case 5: total = valor / (1024); break;
                        case 6: total = valor / 1e6; break;
                        case 7: total = valor / (1024 * 1024); break;
                        case 8: total = valor / 1e9; break;
                        case 9: total = valor / (1024 * 1024 * 1024); break;
                    }
                    break;

                case 3:
                    switch (cboConvertirAlmacenamiento.SelectedIndex)
                    {
                        case 0: total = valor * 8192; break;
                        case 1: total = valor * 1024; break;
                        case 2: total = valor * 1024 / 1000; break;
                        case 3: total = valor; break;
                        case 4: total = valor / 1024; break;
                        case 5: total = valor / (1024 * 1024); break;
                        case 6: total = valor / (1024 * 1024 * 1024); break;
                        case 7: total = valor / (1024 * 1024 * 1024); break;
                        case 8: total = valor / (1024L * 1024 * 1024 * 1024); break;
                        case 9: total = valor / (1024L * 1024 * 1024 * 1024); break;
                    }
                    break;

                case 4:
                    switch (cboConvertirAlmacenamiento.SelectedIndex)
                    {
                        case 0: total = valor * 8e6; break;
                        case 1: total = valor * 1e6; break;
                        case 2: total = valor * 1000; break;
                        case 3: total = valor * 1024; break;
                        case 4: total = valor; break;
                        case 5: total = valor / 1.04858; break;
                        case 6: total = valor / 125; break;
                        case 7: total = valor / 131.072; break;
                        case 8: total = valor / 1.25e6; break;
                        case 9: total = valor / 1.34218e6; break;
                    }
                    break;

                case 5:
                    switch (cboConvertirAlmacenamiento.SelectedIndex)
                    {
                        case 0: total = valor * 8388608; break;
                        case 1: total = valor * 1048576; break;
                        case 2: total = valor * 1024; break;
                        case 3: total = valor * 1024 * 1024; break;
                        case 4: total = valor * 1.04858; break;
                        case 5: total = valor; break;
                        case 6: total = valor / 128; break;
                        case 7: total = valor / 1024; break;
                        case 8: total = valor / 128000; break;
                        case 9: total = valor / 131072; break;
                    }
                    break;

                case 6:
                    switch (cboConvertirAlmacenamiento.SelectedIndex)
                    {
                        case 0: total = valor * 8e9; break;
                        case 1: total = valor * 1e9; break;
                        case 2: total = valor * 1e6; break;
                        case 3: total = valor * 1024 * 1024 * 1024; break;
                        case 4: total = valor * 125; break;
                        case 5: total = valor * 128; break;
                        case 6: total = valor; break;
                        case 7: total = valor / 1.07374; break;
                        case 8: total = valor / 1000; break;
                        case 9: total = valor / 1099.51; break;
                    }
                    break;

                case 7:
                    switch (cboConvertirAlmacenamiento.SelectedIndex)
                    {
                        case 0: total = valor * 8.58993e9; break;
                        case 1: total = valor * 1.07374e9; break;
                        case 2: total = valor * 1.04858e6; break;
                        case 3: total = valor * 1.07374e6; break;
                        case 4: total = valor * 134.217; break;
                        case 5: total = valor * 1024; break;
                        case 6: total = valor * 1.07374; break;
                        case 7: total = valor; break;
                        case 8: total = valor / 1024; break;
                        case 9: total = valor / 1099.51; break;
                    }
                    break;

                case 8:
                    switch (cboConvertirAlmacenamiento.SelectedIndex)
                    {
                        case 0: total = valor * 8e12; break;
                        case 1: total = valor * 1e12; break;
                        case 2: total = valor * 1e9; break;
                        case 3: total = valor * 1.07374e9; break;
                        case 4: total = valor * 1.25e6; break;
                        case 5: total = valor * 128000; break;
                        case 6: total = valor * 1000; break;
                        case 7: total = valor * 1024; break;
                        case 8: total = valor; break;
                        case 9: total = valor / 1.09951; break;
                    }
                    break;

                case 9:
                    switch (cboConvertirAlmacenamiento.SelectedIndex)
                    {
                        case 0: total = valor * 8.79609e12; break;
                        case 1: total = valor * 1.09951e12; break;
                        case 2: total = valor * 1.07374e9; break;
                        case 3: total = valor * 1.09951e9; break;
                        case 4: total = valor * 1.34218e6; break;
                        case 5: total = valor * 131072; break;
                        case 6: total = valor * 1099.51; break;
                        case 7: total = valor * 1099.51; break;
                        case 8: total = valor * 1.09951; break;
                        case 9: total = valor; break;
                    }
                    break;

            }
            lblTotalAlmacenamiento.Text = "Total : " + total;

            switch (cboTipoTiempo.SelectedIndex)
            {
                case 0:
                    switch (cboConvertirTiempo.SelectedIndex)
                    {
                        case 0: total = valor; break;
                        case 1: total = valor * 1000; break;
                        case 2: total = valor * 1e6; break;
                        case 3: total = valor * 1e9; break;
                        case 4: total = valor / 60; break;
                        case 5: total = valor / 3600; break;
                        case 6: total = valor / 86400; break;
                        case 7: total = valor / 604800; break;
                        case 8: total = valor / 2592000; break;
                        case 9: total = valor / 31536000; break;
                    }
                    break;

                case 1:
                    switch (cboConvertirTiempo.SelectedIndex)
                    {
                        case 0: total = valor / 1000; break;
                        case 1: total = valor; break;
                        case 2: total = valor * 1000; break;
                        case 3: total = valor * 1e6; break;
                        case 4: total = valor / 60000; break;
                        case 5: total = valor / 3.6e6; break;
                        case 6: total = valor / 8.64e7; break;
                        case 7: total = valor / 6.048e8; break;
                        case 8: total = valor / 2.592e9; break;
                        case 9: total = valor / 3.1536e10; break;
                    }
                    break;

                case 2:
                    switch (cboConvertirTiempo.SelectedIndex)
                    {
                        case 0: total = valor / 1e6; break;
                        case 1: total = valor / 1000; break;
                        case 2: total = valor; break;
                        case 3: total = valor * 1000; break;
                        case 4: total = valor / 6e7; break;
                        case 5: total = valor / 3.6e9; break;
                        case 6: total = valor / 8.64e10; break;
                        case 7: total = valor / 6.048e11; break;
                        case 8: total = valor / 2.592e12; break;
                        case 9: total = valor / 3.1536e13; break;
                    }
                    break;

                case 3:
                    switch (cboConvertirTiempo.SelectedIndex)
                    {
                        case 0: total = valor / 1e9; break;
                        case 1: total = valor / 1e6; break;
                        case 2: total = valor / 1000; break;
                        case 3: total = valor; break;
                        case 4: total = valor / 6e10; break;
                        case 5: total = valor / 3.6e12; break;
                        case 6: total = valor / 8.64e13; break;
                        case 7: total = valor / 6.048e14; break;
                        case 8: total = valor / 2.592e15; break;
                        case 9: total = valor / 3.1536e16; break;
                    }
                    break;

                case 4:
                    switch (cboConvertirTiempo.SelectedIndex)
                    {
                        case 0: total = valor * 60; break;
                        case 1: total = valor * 60000; break;
                        case 2: total = valor * 6e7; break;
                        case 3: total = valor * 6e10; break;
                        case 4: total = valor; break;
                        case 5: total = valor / 60; break;
                        case 6: total = valor / 1440; break;
                        case 7: total = valor / 10080; break;
                        case 8: total = valor / 43200; break;
                        case 9: total = valor / 525600; break;
                    }
                    break;

                case 5:
                    switch (cboConvertirTiempo.SelectedIndex)
                    {
                        case 0: total = valor * 3600; break;
                        case 1: total = valor * 3.6e6; break;
                        case 2: total = valor * 3.6e9; break;
                        case 3: total = valor * 3.6e12; break;
                        case 4: total = valor * 60; break;
                        case 5: total = valor; break;
                        case 6: total = valor * 24; break;
                        case 7: total = valor * 168; break;
                        case 8: total = valor * 720; break;
                        case 9: total = valor * 8760; break;
                    }
                    break;

                case 6:
                    switch (cboConvertirTiempo.SelectedIndex)
                    {
                        case 0: total = valor * 86400; break;
                        case 1: total = valor * 8.64e7; break;
                        case 2: total = valor * 8.64e10; break;
                        case 3: total = valor * 8.64e13; break;
                        case 4: total = valor * 1440; break;
                        case 5: total = valor / 24; break;
                        case 6: total = valor; break;
                        case 7: total = valor * 7; break;
                        case 8: total = valor * 30; break;
                        case 9: total = valor * 365; break;
                    }
                    break;

                case 7:
                    switch (cboConvertirTiempo.SelectedIndex)
                    {
                        case 0: total = valor * 604800; break;
                        case 1: total = valor * 6.048e8; break;
                        case 2: total = valor * 6.048e11; break;
                        case 3: total = valor * 6.048e14; break;
                        case 4: total = valor * 10080; break;
                        case 5: total = valor / 168; break;
                        case 6: total = valor / 7; break;
                        case 7: total = valor; break;
                        case 8: total = valor * 4.2857; break;
                        case 9: total = valor * 52; break;
                    }
                    break;

                case 8:
                    switch (cboConvertirTiempo.SelectedIndex)
                    {
                        case 0: total = valor * 2592000; break;
                        case 1: total = valor * 2.592e9; break;
                        case 2: total = valor * 2.592e12; break;
                        case 3: total = valor * 2.592e15; break;
                        case 4: total = valor * 43200; break;
                        case 5: total = valor / 720; break;
                        case 6: total = valor / 30; break;
                        case 7: total = valor / 4.2857; break;
                        case 8: total = valor; break;
                        case 9: total = valor * 12.167; break;
                    }
                    break;

                case 9:
                    switch (cboConvertirTiempo.SelectedIndex)
                    {
                        case 0: total = valor * 31536000; break;
                        case 1: total = valor * 3.1536e10; break;
                        case 2: total = valor * 3.1536e13; break;
                        case 3: total = valor * 3.1536e16; break;
                        case 4: total = valor * 525600; break;
                        case 5: total = valor / 8760; break;
                        case 6: total = valor / 365; break;
                        case 7: total = valor / 52; break;
                        case 8: total = valor / 12.167; break;
                        case 9: total = valor; break;
                    }
                    break;

            }
            lblTotalTiempo.Text = "Total : " + total;
         }
    }
}
