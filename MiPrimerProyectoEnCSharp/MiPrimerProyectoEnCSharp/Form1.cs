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

            Double num1 = Convert.ToDouble (txtNum1.Text);
            Double num2 = Convert.ToDouble(txtNum2.Text);
            Double total = num1 + num2;


            lblTotal.Text = "El total es " + total;
        }
    }
}
