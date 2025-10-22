using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MiPrimerProyectoC_
{
    public partial class login : Form
    {
        public login()
        {
            InitializeComponent();
        }

        private void btnIniciar_Click(object sender, EventArgs e)
        {
  
            // Reemplaza con tu información de conexión a la base de datos
            string cadenaConexion = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\db_academica.mdf;Integrated Security=True";

            using (SqlConnection connection = new SqlConnection(cadenaConexion))
            {
                string query = "SELECT COUNT(*) FROM usuarios WHERE usuario = @usuario AND Clave = @clave";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@usuario", txtUsuario.Text);
                    command.Parameters.AddWithValue("@clave", txtContraseña.Text);

                    try
                    {
                        connection.Open();
                        int count = (int)command.ExecuteScalar();

                        if (count > 0)
                        {
                            MessageBox.Show("¡Inicio de sesión exitoso!");
                            frm_usuarios objAlumnos = new frm_usuarios();
                            objAlumnos.Show();
                        }
                        else
                        {
                            MessageBox.Show("Nombre de usuario o contraseña incorrectos.");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error al conectar o ejecutar la consulta: " + ex.Message);
                    }
                }
            }
        }
    }
    }

