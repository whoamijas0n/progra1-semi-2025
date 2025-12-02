namespace parcial_parte_1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnCalcular_Click(object sender, EventArgs e)
        {
            double ingreso = double.Parse(txtMonto.Text);
            double respuesta, a1, a2 = 0;
            if (ingreso <= 500)
            {
                a1 = (ingreso - 0.01);
                a2 = (a1 / 1000);
                respuesta = (a2 * 0) + 1.5;
                lblTotal.Text = "El valor total a pagar es : " + respuesta;
            }

            if (ingreso >= 500.01)
            {
                a1 = (ingreso - 500.01);
                a2 = (a1 / 1000);
                respuesta = (a2 * 3) + 1.5;
                lblTotal.Text = "El valor total a pagar es : " + respuesta;
            }

            if (ingreso >= 1000.01)
            {
                a1 = (ingreso - 1000.01);
                a2 = (a1 / 1000);
                respuesta = (a2 * 3) + 3;
                lblTotal.Text = "El valor total a pagar es : " + respuesta;
            }

            if (ingreso >= 2000.01)
            {
                a1 = (ingreso - 2000.01);
                a2 = (a1 / 1000);
                respuesta = (a2 * 3) + 6;
                lblTotal.Text = "El valor total a pagar es : " + respuesta;
            }

            if (ingreso >= 3000.01)
            {
                a1 = (ingreso - 3000.01);
                a2 = (a1 / 1000);
                respuesta = (a2 * 2) + 9;
                lblTotal.Text = "El valor total a pagar es : " + respuesta;
            }

            if (ingreso >= 8000.01)
            {
                a1 = (ingreso - 8000.01);
                a2 = (a1 / 1000);
                respuesta = (a2 * 2) + 15;
                lblTotal.Text = "El valor total a pagar es : " + respuesta;
            }

            if (ingreso >= 18000.01)
            {
                a1 = (ingreso - 18000.01);
                a2 = (a1 / 1000);
                respuesta = (a2 * 2) + 39;
                lblTotal.Text = "El valor total a pagar es : " + respuesta;
            }

            if (ingreso >= 30000.01)
            {
                a1 = (ingreso - 30000.01);
                a2 = (a1 / 1000);
                respuesta = (a2 * 1) + 63;
                lblTotal.Text = "El valor total a pagar es : " + respuesta;
            }

            if (ingreso >= 60000.01)
            {
                a1 = (ingreso - 60000.01);
                a2 = (a1 / 1000);
                respuesta = (a2 * 0.8) + 93;
                lblTotal.Text = "El valor total a pagar es : " + respuesta;
            }

            if (ingreso >= 100000.01)
            {
                a1 = (ingreso - 100000.01);
                a2 = (a1 / 1000);
                respuesta = (a2 * 0.7) + 125;
                lblTotal.Text = "El valor total a pagar es : " + respuesta;
            }

            if (ingreso >= 200000.01)
            {
                a1 = (ingreso - 200000.01);
                a2 = (a1 / 1000);
                respuesta = (a2 * 0.6) + 195;
                lblTotal.Text = "El valor total a pagar es : " + respuesta;
            }

            if (ingreso >= 300000.01)
            {
                a1 = (ingreso - 200000.01);
                a2 = (a1 / 1000);
                respuesta = (a2 * 0.45) + 255;
                lblTotal.Text = "El valor total a pagar es : " + respuesta;
            }

            if (ingreso >= 400000.01)
            {
                a1 = (ingreso - 400000.01);
                a2 = (a1 / 1000);
                respuesta = (a2 * 0.4) + 300;
                lblTotal.Text = "El valor total a pagar es : " + respuesta;
            }

            if (ingreso >= 500000.01)
            {
                a1 = (ingreso - 500000.01);
                a2 = (a1 / 1000);
                respuesta = (a2 * 0.30) + 340;
                lblTotal.Text = "El valor total a pagar es : " + respuesta;
            }

            if (ingreso >= 1000000.01)
            {
                a1 = (ingreso - 1000000.01);
                a2 = (a1 / 1000);
                respuesta = (a2 * 0.18) + 490;
                lblTotal.Text = "El valor total a pagar es : " + respuesta;
            }


        }
    }
}
