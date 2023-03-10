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
    public partial class Empresa : System.Web.UI.Page
    {
        Service servicio = new Service();
        DataTable dt;
        string mensajeVal = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack == false)
            {
                InhabilitarCampos();
                CargarTabla();
            }
        }

        private void InhabilitarCampos(string opc = "")
        {
            txtCodigo.ReadOnly = opc == "C" ? false : true;
            txtRUC.ReadOnly = true;
            txtRazonSocial.ReadOnly = true;
            rbActivo.Enabled = false;
            rbInactivo.Enabled = false;
            txtDireccion.ReadOnly = true;
            btnAgregarDireccion.Enabled = false;
        }

        private void CargarTabla()
        {
            dt = new DataTable();

            dt.Columns.Add("Item");
            dt.Columns.Add("Direccion");

            Session.Add("Dt", dt);
        }

        protected void btnAgregarDireccion_Click(object sender, EventArgs e)
        {
            dt = Session["dt"] as DataTable;

            dt.Rows.Add(dt.Rows.Count + 1, txtDireccion.Text);
            gvDatos.DataSource = dt;
            gvDatos.DataBind();
            txtDireccion.Text = "";
        }

        protected void gvDatos_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            dt = Session["dt"] as DataTable;
            dt.Rows.RemoveAt(e.RowIndex);

            for (int x = 0; x < dt.Rows.Count; x++)
            {
                dt.Rows[x][0] = x + 1;
            }

            gvDatos.DataSource = dt;
            gvDatos.DataBind();
        }

        protected void lbNuevo_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
            HabilitarCampos();
        }

        private void LimpiarCampos()
        {
            txtCodigo.Text = "";
            txtRUC.Text = "";
            txtRazonSocial.Text = "";
            rbActivo.Checked = false;
            rbInactivo.Checked = false;
            CargarTabla();
            gvDatos.DataSource = dt;
            gvDatos.DataBind();
        }

        private void HabilitarCampos(string opc = "")
        {
            txtCodigo.ReadOnly = true;
            txtRUC.ReadOnly = false;
            txtRazonSocial.ReadOnly = false;
            rbActivo.Enabled = true;
            rbInactivo.Enabled = true;
            txtDireccion.ReadOnly = false;
            btnAgregarDireccion.Enabled = true;
        }

        protected void lbGuardar_Click(object sender, EventArgs e)
        {
            if (ValidarCampos() == false) { return; }

            Datos Emp = new Datos();

            Emp.num_ruc = txtRUC.Text;
            Emp.dsc_raz_social = txtRazonSocial.Text;
            Emp.flg_estado = rbActivo.Checked ? "A" : "I";

            Emp = servicio.RegistrarEmpresa<Datos>(Emp);

            if (Emp != null)
            {
                if (!Emp.dsc_raz_social.Contains("REGISTRAD"))
                {
                    dt = Session["dt"] as DataTable;

                    for (int x = 0; x < dt.Rows.Count; x++)
                    {
                        Datos Dir = new Datos();

                        Dir.cod_empresa = Emp.cod_empresa;
                        Dir.dsc_direccion = dt.Rows[x][1].ToString();

                        Dir = servicio.RegistrarDireccionEmpresa<Datos>(Dir);
                    }

                    txtCodigo.Text = Emp.cod_empresa.ToString();
                }
                else
                {

                }
            }
        }

        private bool ValidarCampos()
        {
            bool validar = true;

            if (txtRUC.Text.Length < 11) { validar = false; mensajeVal = "El RUC debe contener 11 caracteres."; return validar; }
            if (txtRazonSocial.Text == "") { validar = false; mensajeVal = "Debe ingresar una Razón Social."; return validar; }
            if (rbActivo.Checked == false && rbInactivo.Checked == false) { validar = false; mensajeVal = "Debe asignar un estado al Registro."; return validar; }
            if (gvDatos.Rows.Count == 0)
            {
                validar = false;
                mensajeVal = "Debe ingresar al menos una Dirección.";
            }
            else
            {
                for (int x = 0; x < gvDatos.Rows.Count; x++)
                {
                    if (gvDatos.Rows[x].Cells[1].Text.ToString() == "")
                    {
                        validar = false;
                        mensajeVal = "Debe ingresar al menos una Dirección.";
                        break;
                    }

                    if (x > 0)
                    {
                        if (gvDatos.Rows[x].Cells[1].Text.ToString() == gvDatos.Rows[x - 1].Cells[1].Text.ToString())
                        {
                            validar = false;
                            mensajeVal = "Las direcciones no pueden repetirse";
                            break;
                        }
                    }
                }
            }
            return validar;
        }

        protected void lbEliminar_Click(object sender, EventArgs e)
        {
            string mensaje = "";

            mensaje = servicio.Eliminar_Empresa_Direcciones("Empresa", int.Parse(txtCodigo.Text));

            if (mensaje == "OK")
            {
                LimpiarCampos();
            }
            else
            {

            }
        }

        protected void lbConsultar_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
            InhabilitarCampos("C");
        }

        protected void txtCodigo_TextChanged(object sender, EventArgs e)
        {
            DataTable dtTemp = servicio.ListarDireccionesxEmpresa(int.Parse(txtCodigo.Text));

            if (dtTemp.Rows.Count > 0)
            {
                txtRUC.Text = dtTemp.Rows[0][1].ToString();
                txtRazonSocial.Text = dtTemp.Rows[0][2].ToString();
                rbActivo.Checked = dtTemp.Rows[0][3].ToString() == "A" ? true : false;
                rbInactivo.Checked = dtTemp.Rows[0][3].ToString() == "I" ? true : false;

                CargarTabla();
                for (int x = 0; x < dtTemp.Rows.Count; x++)
                {
                    dt.Rows.Add(dtTemp.Rows[x][4].ToString(), dtTemp.Rows[x][5].ToString());
                }

                gvDatos.DataSource = dt;
                gvDatos.DataBind();

                txtCodigo.Enabled = false;
            }
            else
            {
                
            }
        }
    }
}