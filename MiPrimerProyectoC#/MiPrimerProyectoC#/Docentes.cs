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
    public partial class Docentes : Form
    {
        public Docentes()
        {
            InitializeComponent();
        }
       

        Claseconexion objConexion = new Claseconexion();
        DataSet objDs = new DataSet();
        DataTable objDT = new DataTable();
        public int posicion = 0;
        public string accion = "nuevo";

        private void ActulizarDs()
        {
            objDs.Clear();//Limpiar Dataset
            objDs = objConexion.obtenerDatosDocentes();
            objDT = objDs.Tables["docentes"];
            objDT.PrimaryKey = new DataColumn[] { objDT.Columns["IdDocentes"] };

            grdDocentes.DataSource = objDT.DefaultView;
            mostrarDatos();
        }
        private void mostrarDatos()
        {
            if (objDT.Rows.Count > 0)
            {
                idDocente.Text = objDT.Rows[posicion]["IdDocentes"].ToString();
                txtCodigoDocente.Text = objDT.Rows[posicion]["codigo"].ToString();
                txtNombreDocente.Text = objDT.Rows[posicion]["nombre"].ToString();
                txtDireccionDocente.Text = objDT.Rows[posicion]["direccion"].ToString();
                txtTelefonoDocente.Text = objDT.Rows[posicion]["telefono"].ToString();
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
                MessageBox.Show("Estas en el ultimo registro.", "Navegacion de Alumnos", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                MessageBox.Show("Estas en el primer registro.", "Navegacion de Alumnos", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            grbDatosDocente.Enabled = estado;
            grbNavegacionDocentes.Enabled = !estado;
            btnEliminarDocente.Enabled = !estado;
        }
        private void limpiarControles()
        {
            idDocente.Text = "";
            txtCodigoDocente.Text = "";
            txtNombreDocente.Text = "";
            txtDireccionDocente.Text = "";
            txtTelefonoDocente.Text = "";
        }

        private void btnAgregarDocente_Click(object sender, EventArgs e)
        {
            if (btnAgregarDocente.Text == "Nuevo")
            {
                btnAgregarDocente.Text = "Guardar";
                btnModificarDocente.Text = "Cancelar";
                estadoControles(true);
                accion = "nuevo";
                limpiarControles();
            }
            else
            {//Guardar
                String[] docentes = {
            idDocente.Text, txtCodigoDocente.Text, txtNombreDocente.Text, txtDireccionDocente.Text,
            txtTelefonoDocente.Text
        };
                String respuesta = objConexion.administrarDatosDocentes(docentes, accion);
                if (respuesta != "1")
                {
                    MessageBox.Show(respuesta, "Error al guardar docente.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    estadoControles(false);
                    btnAgregarDocente.Text = "Nuevo";
                    btnModificarDocente.Text = "Modificar";
                    ActulizarDs();
                }
            }
        }

        private void btnModificarDocente_Click(object sender, EventArgs e)
        {
            if (btnModificarDocente.Text == "Modificar")
            {
                btnAgregarDocente.Text = "Guardar";
                btnModificarDocente.Text = "Cancelar";
                estadoControles(true);
                accion = "modificar";

            }
            else
            {//Cancelar
                mostrarDatos();
                estadoControles(false);
                btnAgregarDocente.Text = "Nuevo";
                btnModificarDocente.Text = "Modificar";
            }
        }

        private void btnEliminarDocente_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Esta seguro de eliminar a " + txtNombreDocente.Text,
      "Eliminando docente", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                String respuesta = objConexion.administrarDatosDocentes(
                    new String[] { idDocente.Text, "", "", "", "" }, "eliminar"
                );
                if (respuesta != "1")
                {
                    MessageBox.Show(respuesta, "Error al eliminar Docente.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    posicion = 0;
                    ActulizarDs();
                }
            }
        }

        private void txtBuscarDocentes_KeyUp(object sender, KeyEventArgs e)
        {
            filtrarDatos(txtBuscarDocentes.Text);
        }
        private void filtrarDatos(String valor)
        {
            try
            {
                DataView objDv = objDT.DefaultView;
                objDv.RowFilter = "codigo like '%" + valor + "%' OR nombre like '" + valor + "%'";
                grdDocentes.DataSource = objDv;
                seleccionarDocente();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
        private void seleccionarDocente()
        {
            try
            {
                if (grdDocentes.CurrentRow == null)
                {
                    MessageBox.Show("No hay filas");
                    return;
                }
                string id = grdDocentes.CurrentRow.Cells["IdDocentes"].Value.ToString();
                posicion = objDT.Rows.IndexOf(objDT.Rows.Find(id));
                mostrarDatos();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
        private void grdDocentes_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            seleccionarDocente();
        }

        private void Docentes_Load(object sender, EventArgs e)
        {
            ActulizarDs();
        }
    }
}
