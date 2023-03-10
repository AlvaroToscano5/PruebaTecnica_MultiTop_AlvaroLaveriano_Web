using PruebaTecnica_MultiTop_AlvaroLaveriano;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PruebaTecnica_MultiTop_AlvaroLaveriano_Web.Vistas.Empresa
{
    public partial class Formulario : System.Web.UI.Page
    {
        DataTable dt;

        protected void Page_Load(object sender, EventArgs e)
        {
            CargarTabla();
        }

        private void CargarTabla()
        {
            dt = new DataTable();

            dt.Columns.Add("Item");
            dt.Columns.Add("Direccion");
        }

        protected void btnAgregarDir_Click(object sender, EventArgs e)
        {
            
        }
    }
}