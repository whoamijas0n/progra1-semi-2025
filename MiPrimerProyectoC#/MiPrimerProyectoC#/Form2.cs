using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MiPrimerProyectoC_
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void btnCalcular_Click(object sender, EventArgs e)
        {
            //no se por que en la computadora de la U no me dejo subir los cambios con mi cuenta de GitHub y los subio con otra cuenta
            double num1, num2, respuesta=0;
            num1 = double.Parse(txtNum1.Text);
            num2 = double.Parse(txtNum2.Text);
            if (optSuma.Checked)
            {
                respuesta = num1 + num2;
            }
            if (optResta.Checked)
            {
                respuesta = num1 - num2;
            }
            if (optMultiplicacion.Checked)
            {
                respuesta = num1 * num2;
            }
            if (optDivision.Checked)
            {
                respuesta = num1 / num2;
            }
            if (optExponenciacion.Checked){
                respuesta = Math.Pow(num1, num2);
            }
            if (optPorcentaje.Checked)
            {
                respuesta =  (num1 / num2) * 100;
            }
            if (optFactorial.Checked)
            {
                respuesta = 1;
                for (int i = 1; i <= num1; i++)
                {
                    respuesta *= i;
                }
            }
            if (optPrimo.Checked)
            {
                int i=1, acum = 0;
                while (i <= num1 && acum<3)
                {
                    if (num1 % i == 0)
                    {
                        acum++; //acumula los divisores 
                    }
                }
            }
            lblRespuesta.Text = "Resultado = " + respuesta;

            

        }
       //agregar procentaje funcional, primo y modulo

        private void optDivision_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void grbOpciones_Enter(object sender, EventArgs e)
        {

        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnCalcularOpciones_Click(object sender, EventArgs e)
        {
            double num1, num2, respuesta = 0;
            num1 = double.Parse(txtNum1.Text);
            num2 = double.Parse(txtNum2.Text);
            switch (cboOpciones.SelectedIndex) {
                case 0:
                    respuesta = num1 + num2;
                    break;
                case 1:
                    respuesta = num1 - num2;
                    break;
                case 2:
                    respuesta = num1 * num2;
                    break;
                case 3:
                    respuesta = num1 / num2;
                    break;
                    // Agregar mas casos para factorial y primo

            }
            lblRespuesta.Text = "Respuesta = " + respuesta;
        }
    }
}
