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

        private double[][] tablaIsr = {
         new Double[] {0.01, 550, 0, 0},
         new Double[] {550.01, 895.24, 0.10, 17.67},
         new Double[] {895.25, 2038.10, 0.20, 60},
         new Double[] {2038.11, 9999999, 0.30, 288.57}
 };
        private double calcularDeducciones(double sueldo, double porcentaje)
        {
            return sueldo * porcentaje;
        }
        private double calcularIsr(double sueldo)
        {
            double isr = 0;
            for (int i = 0; i < tablaIsr.Length; i++)
            {
                if (sueldo >= tablaIsr[i][0] && sueldo <= tablaIsr[i][1])
                {
                    isr = (sueldo - tablaIsr[i][0]) * tablaIsr[i][2] + tablaIsr[i][3];
                }
            }
            return isr;
        }
        private void btnCalcular_Click(object sender, EventArgs e)
        {
            double sueldo = 0, isss = 0, afp = 0, isr = 0, sueldoNeto = 0;
            sueldo = double.Parse(txtSueldo.Text);

            isss = calcularDeducciones(sueldo, 0.03); // 3% de ISSS -> 3/100=0.03
            afp = calcularDeducciones(sueldo, 0.0725); // 7.25% de AFP -> 7.25/100=0.0725
            isr = calcularIsr(sueldo - isss - afp); // Calcular ISR

            sueldoNeto = sueldo - isss - afp - isr; // Calcular sueldo neto

            lblISS.Text = "ISSS: " + isss.ToString("C2");
            lblAFP.Text = "AFP: " + afp.ToString("C2");
            lblISR.Text = "ISR: " + isr.ToString("C2");
            lblTotalDeducciones.Text = "Total Deducciones: " + (isss + afp + isr).ToString("C2");
            lblSueldoNeto.Text = "Sueldo Neto: " + sueldoNeto.ToString("C2");
        }
    }
}
