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
    public partial class frmPeriodo : Form
    {
        public frmPeriodo()
        {
            InitializeComponent();
        }

        private void periodosBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.periodosBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.db_academicaDataSet1);

        }

        private void frmPeriodo_Load(object sender, EventArgs e)
        {
            // TODO: esta línea de código carga datos en la tabla 'db_academicaDataSet1.periodos' Puede moverla o quitarla según sea necesario.
            this.periodosTableAdapter.Fill(this.db_academicaDataSet1.periodos);

        }

        private void periodosBindingNavigator_RefreshItems(object sender, EventArgs e)
        {

        }
    }
}
