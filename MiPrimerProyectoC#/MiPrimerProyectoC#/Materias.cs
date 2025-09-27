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
    public partial class Materias : Form
    {
        public Materias()
        {
            InitializeComponent();
        }

        private void Materias_Load(object sender, EventArgs e)
        {
            ActulizarDs();
            cboBuscarMaterias.SelectedIndex = 1;//buscar por materia
        }

        Claseconexion objConexion = new Claseconexion();
        DataSet objDs = new DataSet();
        DataTable objDT = new DataTable();
        public int posicion = 0;
        public string accion = "nuevo";

        private void ActulizarDs()
        {
            objDs.Clear();//Limpiar Dataset
            objDs = objConexion.obtenerDatos();
            objDT = objDs.Tables["materias"];
            objDT.PrimaryKey = new DataColumn[] { objDT.Columns["IdMaterias"] };

            grdMaterias.DataSource = objDT.DefaultView;
            mostrarDatos();
        }
        private void mostrarDatos()
        {
            if (objDT.Rows.Count > 0)
            {
                idMateria.Text = objDT.Rows[posicion]["IdMaterias"].ToString();
                txtCodigoMateria.Text = objDT.Rows[posicion]["codigo"].ToString();
                txtNombreMateria.Text = objDT.Rows[posicion]["nombre"].ToString();
                txtUVMateria.Text = objDT.Rows[posicion]["uv"].ToString();
                lblnRegistrosAlumno.Text = (posicion + 1) + " de " + objDT.Rows.Count;
            }
        }

        private void btnSiguiente_Click(object sender, EventArgs e)
        {
            if (posicion < objDT.Rows.Count - 1)
            {
                posicion++;// posicion=posicion+1
                mostrarDatos();
            }
            else
            {
                MessageBox.Show("Estas en el ultimo registro.", "Navegacion de Materias", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnAnterior_Click(object sender, EventArgs e)
        {

            if (posicion > 0)
            {
                posicion--;// posicion=posicion-1
                mostrarDatos();
            }
            else
            {
                MessageBox.Show("Estas en el primer registro.", "Navegacion de Materias", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnPrimero_Click(object sender, EventArgs e)
        {
            posicion = 0;
            mostrarDatos();
        }

        private void btnUltimo_Click(object sender, EventArgs e)
        {
            posicion = objDT.Rows.Count - 1;
            mostrarDatos();
        }
        private void estadoControles(Boolean estado)
        {
            grbDatosMateria.Enabled = estado;
            grbNavegacionMateria.Enabled = !estado;
            btnEliminarMateria.Enabled = !estado;
        }
        private void limpiarControles()
        {
            idMateria.Text = "";
            txtCodigoMateria.Text = "";
            txtNombreMateria.Text = "";
            txtUVMateria.Text = "";
        }

        private void btnAgregarMateria_Click(object sender, EventArgs e)
        {
            if (btnAgregarMateria.Text == "Nuevo")
            {
                btnAgregarMateria.Text = "Guardar";
                btnModificarMateria.Text = "Cancelar";
                estadoControles(true);
                accion = "nuevo";
                limpiarControles();
            }
            else
            {//Guardar
                String[] Materia = {
                    idMateria.Text, txtCodigoMateria.Text, txtNombreMateria.Text,txtUVMateria.Text
                };
                String respuesta = objConexion.administrarDatosMaterias(Materia, accion);
                if (respuesta != "1")
                {
                    MessageBox.Show(respuesta, "Error al guardar Materias.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    estadoControles(false);
                    btnAgregarMateria.Text = "Nuevo";
                    btnModificarMateria.Text = "Modificar";
                    ActulizarDs();
                }
            }
        }

        private void btnModificarMateria_Click(object sender, EventArgs e)
        {
            if (btnModificarMateria.Text == "Modificar")
            {
                btnAgregarMateria.Text = "Guardar";
                btnModificarMateria.Text = "Cancelar";
                estadoControles(true);
                accion = "modificar";

            }
            else
            {//Cancelar
                mostrarDatos();
                estadoControles(false);
                btnAgregarMateria.Text = "Nuevo";
                btnModificarMateria.Text = "Modificar";
            }
        }

        private void btnEliminarMateria_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Esta seguro de eliminar a " + txtNombreMateria.Text,
              "Eliminando Materia", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                String respuesta = objConexion.administrarDatosAlumnos(
                    new String[] { idMateria.Text, "", "", ""}, "eliminar"
                );
                if (respuesta != "1")
                {
                    MessageBox.Show(respuesta, "Error al eliminar Materia.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    posicion = 0;
                    ActulizarDs();
                }
            }
        }

        private void txtBuscarAlumnos_KeyUp(object sender, KeyEventArgs e)
        {
            filtrarDatos(txtBuscarMateria.Text );
        }
        private void filtrarDatos(String valor)
        {
            try
            {
                DataView objDv = objDT.DefaultView;
                switch (cboBuscarMaterias.SelectedIndex)
                {
                    case 0: //codigo
                        objDv.RowFilter = "codigo = " + valor;
                        break;

                    case 1: //nombre
                        objDv.RowFilter = "nombre like '%" + valor + "%'";
                        break;
                }
                grdMaterias.DataSource = objDv;
                seleccionarMateria();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
        private void seleccionarMateria()
        {
            try
            {
                if (grdMaterias.CurrentRow == null)
                {
                    return;
                }
                string id = grdMaterias.CurrentRow.Cells["id"].Value.ToString();
                posicion = objDT.Rows.IndexOf(objDT.Rows.Find(id));
                mostrarDatos();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
        private void grdMaterias_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            seleccionarMateria();
        }

        
    }
}
