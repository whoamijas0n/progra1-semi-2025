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
    public partial class FrmPrincipal : Form
    {
        public FrmPrincipal()
        {
            InitializeComponent();
        }

        

        private void alumnosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form5 objAlumnos = new Form5();
            objAlumnos.MdiParent = this;
            objAlumnos.Show();
        }

        private void docentesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Docentes objDocentes = new Docentes();
            objDocentes.MdiParent = this;
            objDocentes.Show();
        }

        private void materiasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Materias objMaterias = new Materias();
            objMaterias.MdiParent = this;
            objMaterias.Show();
        }

        private void periodosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmPeriodos objPeriodos = new frmPeriodos();
            objPeriodos.MdiParent = this;
            objPeriodos.Show();
        }

        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void notasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmNotas objnotas = new frmNotas();
            objnotas.MdiParent = this;
            objnotas.Show();
        }
    }
}
