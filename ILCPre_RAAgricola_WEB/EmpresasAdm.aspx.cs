using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace Cabana.Campo.RAAgricola.Pre.Web
{
    public partial class EmpresasAdm : System.Web.UI.Page
    {
        public static Cabana.Campo.RAAgricola.Pre.Web.localhostWs.DS_ILC_Campo.PLwebEmprAdministrativasDataTable tablaEmpresasAdm;

        public static Cabana.Campo.RAAgricola.Pre.Web.localhostWs.WSRAAgricola client;

        ConfigSession configSession = new ConfigSession();
        //int EmprId;
        //int UsuId;
        //int UsuNivelAcceso;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(Session["UsuId"] as string))
                {
                    if (this.HddEmprId.Value == "")
                    {
                        this.HddUsuId.Value = "" + Session["UsuId"];
                        this.HddEmprId.Value = "" + Session["EmprId"];
                        this.HddUsuNivelAcceso.Value = "" + Session["UsuNivelAcceso"]; // Int32.Parse(
                    }
                    if (!configSession.validarSession(getUsuNivelAcceso(), "EmpresasAdm"))//NumeroDeAcceso, NombrePagina
                    {
                        Response.Write("<script language='javascript'> window.location.replace('PaginaOut.aspx');</" + "script>");
                        Response.End();
                    }
                }
                else
                {
                    Response.Write("<script language='javascript'> window.location.replace('Login.aspx');</" + "script>");
                    Response.End();
                }


                if (!IsPostBack)
                {
                    client = new Cabana.Campo.RAAgricola.Pre.Web.localhostWs.WSRAAgricola();
                    tablaEmpresasAdm = new Cabana.Campo.RAAgricola.Pre.Web.localhostWs.DS_ILC_Campo.PLwebEmprAdministrativasDataTable();
                    BindDataTareas();
                }
            }
            catch (System.Net.WebException ee)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('A5050 No es posible conectar con el servidor')", true);

            }

        }

        protected void BindDataTareas()
        {

            tablaEmpresasAdm = client.EmpresasAdmGet(null);
            this.gvTareas.DataSource = tablaEmpresasAdm;
            this.gvTareas.DataBind();

        }

        public int getUsuId()
        {
            return Int32.Parse("" + this.HddUsuId.Value);
        }
        public int getEmprId()
        {
            return Int32.Parse("" + this.HddEmprId.Value);
        }

        public int getUsuNivelAcceso()
        {
            return Int32.Parse("" + this.HddUsuNivelAcceso.Value);
        }

        protected void lbtn_AddNewTarea_Click(object sender, EventArgs e)
        {
            try
            {

                DataRow row = tablaEmpresasAdm.NewRow();

                row["EmprNombre"] = this.txt_EmprNombre.Text;
                row["EmprAlias"] = this.txt_EmprAlias.Text;
                row["EmprDireccion"] = this.txt_EmprDireccion.Text;
                row["EmprTelefono"] = this.txt_EmprTelefono.Text;
                row["EmprNomContacto"] = this.txt_EmprNomContacto.Text;

                tablaEmpresasAdm.Rows.Add(row);
                client.EmpresasAdmSet(tablaEmpresasAdm);
                gvTareas.EditIndex = -1;

                BindDataTareas();
                limpiarFormulario();

            }
            catch (System.IO.IOException er)
            {
                Console.WriteLine("Error en el insert", er);
            }
            catch (System.ArgumentException arg)// Argumentos incorrectos o nullos
            {
                string correctString = arg.Message.ToString().Replace("'", " ");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('A1020 Todos los campos son requeridos " + correctString + "')", true);
            }
            catch (System.NullReferenceException argnull)// No se ha declarado o definido aun alguna variable o objeto en el sistema
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('A1010 Todos los campos son requeridos')", true);

            }
        }

        protected void lbtnBuscarTarea_Click(object sender, EventArgs e)
        {
            String textoBuscar = this.txtControlBuscarTareas.Text;
            this.gvTareas.DataSource = tablaEmpresasAdm;
            (this.gvTareas.DataSource as DataTable).DefaultView.RowFilter = " EmprNombre LIKE '%" + textoBuscar + "%' OR EmprAlias LIKE '%" + textoBuscar + "%'  "; //

            limpiarFormulario();
            this.gvTareas.DataBind();
        }

        protected void gvTareas_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            if (e.CommandName.Equals("ADD"))
            {
                /*
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = this.gvProveedores.Rows[index];
                Label lblcodProv = (Label)row.FindControl("lblcodProv");
                Label lblnomProv = (Label)row.FindControl("lblnomProv");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Mostrar Modal", " tituloModal('" + lblcodProv.Text + " - " + lblnomProv.Text + "'); modalLotesProv(); ", true);
                */
            }
        }
        protected void gvTareas_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {

            Label lblEditTareaId = (Label)gvTareas.Rows[e.RowIndex].FindControl("lblEditUsuId");

            try
            {
                int idRegistro = Int32.Parse(lblEditTareaId.Text);
                if (tablaEmpresasAdm.Rows.Count > 0)
                {
                    DataRow rows = tablaEmpresasAdm.FindByEmprId(idRegistro);

                    rows["EmprNombre"] = this.txt_EmprNombre.Text;
                    rows["EmprAlias"] = this.txt_EmprAlias.Text;
                    rows["EmprDireccion"] = this.txt_EmprDireccion.Text;
                    rows["EmprTelefono"] = this.txt_EmprTelefono.Text;
                    rows["EmprNomContacto"] = this.txt_EmprNomContacto.Text;

                    client.EmpresasAdmSet(tablaEmpresasAdm);
                    gvTareas.EditIndex = -1;

                    BindDataTareas();
                    limpiarFormulario();
                }
            }
            catch (System.IO.IOException er)
            {
                Console.WriteLine("Error en el insert", er);
            }

            catch (System.NullReferenceException argnull)// No se ha declarado o definido aun alguna variable o objeto en el sistema
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('A1010 Todos los campos son requeridos')", true);

            }

        }
        protected void gvTareas_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                Label lblTareaId = (Label)this.gvTareas.Rows[e.RowIndex].FindControl("lblUsuId");
                int idRegistro = Int32.Parse(lblTareaId.Text);
                DataRow rows = tablaEmpresasAdm.FindByEmprId(idRegistro);
                rows.Delete();
                client.EmpresasAdmSet(tablaEmpresasAdm);
                BindDataTareas();
                Session["mesajeAccion"] = "Registro eliminado con exito";
            }
            catch (Exception error)
            {
                if (error.ToString().Contains("REFERENCE"))
                    Session["mesajeAccion"] = "Este registro ya tiene datos vinculados.";
                else
                    Session["mesajeAccion"] = "Informar de este error a soporte tecnico";
            }
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + Session["mesajeAccion"].ToString() + "')", true);
            BindDataTareas();

        }
        protected void gvTareas_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {

            this.gvTareas.EditIndex = -1;
            this.gvTareas.DataSource = tablaEmpresasAdm;
            this.gvTareas.DataBind();

            limpiarFormulario();

        }
        protected void gvTareas_RowEditing(object sender, GridViewEditEventArgs e)
        {

            this.gvTareas.EditIndex = e.NewEditIndex;
            this.gvTareas.DataSource = tablaEmpresasAdm;
            this.gvTareas.DataBind();

            Label IdRegistro = (Label)gvTareas.Rows[e.NewEditIndex].FindControl("lblEditUsuId");
            DataRow rows = tablaEmpresasAdm.FindByEmprId(Int32.Parse(IdRegistro.Text));

            this.txt_EmprNombre.Text = rows["EmprNombre"].ToString();
            this.txt_EmprAlias.Text = rows["EmprAlias"].ToString();
            this.txt_EmprDireccion.Text = rows["EmprDireccion"].ToString();
            this.txt_EmprTelefono.Text = rows["EmprTelefono"].ToString();
            this.txt_EmprNomContacto.Text = rows["EmprNomContacto"].ToString();

            this.lbtn_AddNewTarea.Visible = false;

        }
        protected void gvTareas_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

            this.gvTareas.PageIndex = e.NewPageIndex;
            this.gvTareas.DataSource = tablaEmpresasAdm;
            this.gvTareas.EditIndex = -1;
            this.gvTareas.DataBind();

            //Session["Nombre"] = "Juan Jose Reyes";

        }

        protected void limpiarFormulario()
        {

            this.txt_EmprNombre.Text = "";
            this.txt_EmprAlias.Text = "";
            this.txt_EmprDireccion.Text = "";
            this.txt_EmprTelefono.Text = "";
            this.txt_EmprNomContacto.Text = "";
            this.lbtn_AddNewTarea.Visible = true;

            this.gvTareas.EditIndex = -1;
        }
    }
}