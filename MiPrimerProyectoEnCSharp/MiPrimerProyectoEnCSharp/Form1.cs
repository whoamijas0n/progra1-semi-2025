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

        private void btnSaludo_Click(object sender, EventArgs e)
        {

            Double num1, num2, total = 0;
            num1 = double.Parse(txtNum1.Text);
            num2 = double.Parse(txtNum2.Text);

            if (optSuma.Checked)
            {
                total = num1 + num2;
            }

            if (optResta.Checked)
            {
                total = num1 - num2;
            }

            if (optMultiplicacion.Checked)
            {
                total = num1 * num2;
            }

            if (optDivision.Checked)
            {
                total = num1 / num2;
            }

            if (optExponente.Checked)
            {
                total = Math.Pow(num1, num2);
            }

            if (optPorcentaje.Checked)
            {
                double porcentaje = num2 / 100;


                total = num1 * porcentaje;
            }


            if (optFactorial.Checked)
            {
                total = (int)num1;
                for (int i = (int)num1 - 1; i > 1; i--)
                {
                    total *= i;
                }
            }

            if (optModulo.Checked)
            {
                total = num1 % num2;
            }



            lblTotal.Text = "El total es " + total;

        

            if (optPrimo.Checked)
            {
                int i = 1, acum=0;
                while (i <= num1 && acum<3)
                {
                    if (num1%i==0)
                    {
                        acum++;
                    }
                    i++;
                }
                if (acum <= 2)
                {
                    lblTotal.Text = "Respuesta : " + num1 + " es primo";
                }
                else
                {
                    lblTotal.Text = "Respuesta : " + num1 + " no es primo";
                }

            }




        }

        private void optSuma_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void btnCalcularOpciones_Click(object sender, EventArgs e)
        {
            double num1, num2, total=0;
            num1 = double.Parse(txtNum1.Text);
            num2 = double.Parse(txtNum1.Text);

            switch (cboOpciones.SelectedIndex)
            {
                case 0:
                    total = num1 + num2;
                    break;

                case 1:
                    total = num1 - num2;
                    break;

                case 2:
                    total = num1 * num2;
                    break;

                case 3:
                    total = num1 / num2;
                    break;

                case 4:
                    total = Math.Pow(num1, num2);
                    break;

                case 5:
                    double porcentaje = num2 / 100;
                    total = num1 * porcentaje;
                    break;

                case 6:
                    total = (int)num1;
                    for (int l = (int)num1 - 1; l > 1; l--)
                    {
                        total *= l;
                    }
                    break;

                case 7:

                    total = num1 % num2;

                    break;

            }

            lblTotal.Text = "El total es " + total;


            //Para los numeros primos

            switch (cboOpciones.SelectedIndex)
            {
                case 8:

                    int i = 1, acum = 0;
                    while (i <= num1 && acum < 3)
                    {
                        if (num1 % i == 0)
                        {
                            acum++;
                        }
                        i++;
                    }
                    if (acum <= 2)
                    {
                        lblTotal.Text = "Respuesta : " + num1 + " es primo";
                    }
                    else
                    {
                        lblTotal.Text = "Respuesta : " + num1 + " no es primo";
                    }


                    break;
            }


        }


        }
}
