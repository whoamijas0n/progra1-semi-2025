using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
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
            double num1, num2, respuesta;
            num1 = double.Parse(txtNum1.Text);
            num2 = double.Parse(txtNum2.Text);
            respuesta = num1 + num2;
            lblRespuesta.Text = "Resultado = " + respuesta;
        }
    }
}
