using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace Cabana.Campo.RAAgricola.Pre.Web
{
    public partial class UsuariosEmpr : System.Web.UI.Page
    {
        public static Cabana.Campo.RAAgricola.Pre.Web.localhostWs.DS_ILC_Campo.PLwebUsuariosEmprDataTable tablaUsuariosEmpr;
        public static Cabana.Campo.RAAgricola.Pre.Web.localhostWs.DS_ILC_Campo.PLwebEmprAdministrativasDataTable tablaEmpresasAdm;


        public static Cabana.Campo.RAAgricola.Pre.Web.localhostWs.WSRAAgricola client;

        ConfigSession configSession = new ConfigSession();
        //int EmprId;
        //int UsuId;
        //int UsuNivelAcceso;
        public static DataTable distinctValuesEmpresas;

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
                    if (!configSession.validarSession(getUsuNivelAcceso(), "UsuariosEmpr"))//NumeroDeAcceso, NombrePagina
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
                    tablaUsuariosEmpr = new Cabana.Campo.RAAgricola.Pre.Web.localhostWs.DS_ILC_Campo.PLwebUsuariosEmprDataTable();
                    BindDataEmpresasAdm_ddl();
                    BindDataTareas();

                }
            }
            catch (System.Net.WebException ee)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('A5050 No es posible conectar con el servidor')", true);

            }

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
        protected void BindDataTareas()
        {

            if (getUsuNivelAcceso() == 1)
            {
                tablaUsuariosEmpr = client.UsuarioEmprGet(null, "", "", null, null, null);
            }
            else
            {
                tablaUsuariosEmpr = client.UsuarioEmprGet(null, "", "", getEmprId(), null, null);
            }


            this.gvTareas.DataSource = tablaUsuariosEmpr;
            (this.gvTareas.DataSource as DataTable).DefaultView.RowFilter = " EmprId = " + this.ddl_Empresas.SelectedValue + " "; //
            this.gvTareas.DataBind();

        }
        protected void BindDataEmpresasAdm_ddl()
        {
            tablaEmpresasAdm = new Cabana.Campo.RAAgricola.Pre.Web.localhostWs.DS_ILC_Campo.PLwebEmprAdministrativasDataTable();
            if (getUsuNivelAcceso() == 1)
            {
                tablaEmpresasAdm = client.EmpresasAdmGet(null);
            }
            else
            {
                tablaEmpresasAdm = client.EmpresasAdmGet(getEmprId());
            }

            DataView view = new DataView(tablaEmpresasAdm);
            distinctValuesEmpresas = view.ToTable(true, "EmprId", "EmprNombre");
            this.ddl_Empresas.DataSource = distinctValuesEmpresas;
            this.ddl_Empresas.DataValueField = "EmprId";
            this.ddl_Empresas.DataTextField = "EmprNombre";
            this.ddl_Empresas.DataBind();
            //this.ddl_Empresas.Items.Insert(0, new ListItem("Seleccionar La empresa", "0"));
        }

        protected void lbtn_AddNewTarea_Click(object sender, EventArgs e)
        {
            try
            {

                DataRow row = tablaUsuariosEmpr.NewRow();

                if (!this.txtFrmUsuNombre.Text.Equals("")
                    && !this.txtFrmUsuPass.Text.Equals("")
                    && !this.txtFrmUsuNombreCompleto.Text.Equals("")
                    && !this.txtFrmUsuEmail.Text.Equals("")
                    && !this.txtFrmUsuCargo.Text.Equals(""))
                {


                    row["UsuNivelAcceso"] = this.ddl_UsuNivelAcceso.SelectedValue;
                    row["UsuNombre"] = this.txtFrmUsuNombre.Text;
                    row["UsuPass"] = this.txtFrmUsuPass.Text;
                    row["UsuNombreCompleto"] = this.txtFrmUsuNombreCompleto.Text;
                    row["UsuEmail"] = this.txtFrmUsuEmail.Text;
                    row["UsuCargo"] = this.txtFrmUsuCargo.Text;
                    row["UsuEstatus"] = this.ddlFrmUsuEstatus.SelectedValue;
                    row["EmprId"] = this.ddl_Empresas.SelectedValue;
                    row["UsuFechaRegistro"] = "15/10/2000";
                    tablaUsuariosEmpr.Rows.Add(row);
                    client.UsuarioEmprSet(tablaUsuariosEmpr);
                    gvTareas.EditIndex = -1;

                    BindDataTareas();
                    limpiarFormulario();
                }
                else {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Todos los campos son requeridos')", true);

                }

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
            this.gvTareas.DataSource = tablaUsuariosEmpr;
            (this.gvTareas.DataSource as DataTable).DefaultView.RowFilter = " UsuNombre LIKE '%" + textoBuscar + "%' OR UsuNombreCompleto LIKE '%" + textoBuscar + "%'  "; //

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
                if (tablaUsuariosEmpr.Rows.Count > 0)
                {
                    DataRow rows = tablaUsuariosEmpr.FindByUsuId(idRegistro);
                    rows["UsuNivelAcceso"] = this.ddl_UsuNivelAcceso.SelectedValue;
                    rows["UsuNombre"] = this.txtFrmUsuNombre.Text;
                    rows["UsuPass"] = this.txtFrmUsuPass.Text;
                    rows["UsuNombreCompleto"] = this.txtFrmUsuNombreCompleto.Text;
                    rows["UsuCargo"] = this.txtFrmUsuCargo.Text;
                    rows["UsuEmail"] = this.txtFrmUsuEmail.Text;
                    rows["UsuEstatus"] = this.ddlFrmUsuEstatus.SelectedValue;

                    client.UsuarioEmprSet(tablaUsuariosEmpr);
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
                DataRow rows = tablaUsuariosEmpr.FindByUsuId(idRegistro);
                rows.Delete();
                client.UsuarioEmprSet(tablaUsuariosEmpr);
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
            this.gvTareas.DataSource = tablaUsuariosEmpr;
            this.gvTareas.DataBind();

            limpiarFormulario();

        }
        protected void gvTareas_RowEditing(object sender, GridViewEditEventArgs e)
        {

            this.gvTareas.EditIndex = e.NewEditIndex;
            this.gvTareas.DataSource = tablaUsuariosEmpr;
            this.gvTareas.DataBind();

            Label IdRegistro = (Label)gvTareas.Rows[e.NewEditIndex].FindControl("lblEditUsuId");
            DataRow rows = tablaUsuariosEmpr.FindByUsuId(Int32.Parse(IdRegistro.Text));


            this.txtFrmUsuNombre.Text = rows["UsuNombre"].ToString();
            this.txtFrmUsuNombreCompleto.Text = rows["UsuNombreCompleto"].ToString();
            this.txtFrmUsuCargo.Text = rows["UsuCargo"].ToString();
            this.txtFrmUsuPass.Text = rows["UsuPass"].ToString();
            this.txtFrmUsuEmail.Text = rows["UsuEmail"].ToString();
            this.ddl_Empresas.SelectedIndex = this.ddl_Empresas.Items.IndexOf(this.ddl_Empresas.Items.FindByValue(rows["EmprId"].ToString()));

            this.ddl_UsuNivelAcceso.SelectedIndex = this.ddl_UsuNivelAcceso.Items.IndexOf(this.ddl_UsuNivelAcceso.Items.FindByValue(rows["UsuNivelAcceso"].ToString()));
            this.ddlFrmUsuEstatus.SelectedIndex = this.ddlFrmUsuEstatus.Items.IndexOf(this.ddlFrmUsuEstatus.Items.FindByValue(rows["UsuEstatus"].ToString()));


            this.lbtn_AddNewTarea.Visible = false;
            this.ddl_Empresas.Enabled = false;

        }
        protected void gvTareas_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

            this.gvTareas.PageIndex = e.NewPageIndex;
            this.gvTareas.DataSource = tablaUsuariosEmpr;
            this.gvTareas.EditIndex = -1;
            this.gvTareas.DataBind();

            //Session["Nombre"] = "Juan Jose Reyes";

        }

        protected void limpiarFormulario()
        {
            this.ddl_UsuNivelAcceso.SelectedIndex = this.ddl_UsuNivelAcceso.Items.IndexOf(this.ddl_UsuNivelAcceso.Items.FindByValue("0"));
            this.ddlFrmUsuEstatus.SelectedIndex = this.ddlFrmUsuEstatus.Items.IndexOf(this.ddlFrmUsuEstatus.Items.FindByValue("1")); ;
            this.txtFrmUsuNombre.Text = "";
            this.txtFrmUsuPass.Text = "";
            this.txtFrmUsuNombreCompleto.Text = "";
            this.txtFrmUsuCargo.Text = "";
            this.txtFrmUsuEmail.Text = "";
            this.lbtn_AddNewTarea.Visible = true;
            this.ddl_Empresas.Enabled = true;
            //this.gvTareas.DataSource = tablaTareas;
            this.gvTareas.EditIndex = -1;
        }

        protected void ddl_Empresas_Change(object sender, EventArgs e)
        {
            BindDataEmpresas_ddl();

        }

        protected void BindDataEmpresas_ddl()
        {
            this.gvTareas.DataSource = tablaUsuariosEmpr;
            (this.gvTareas.DataSource as DataTable).DefaultView.RowFilter = " EmprId = " + this.ddl_Empresas.SelectedValue + " "; //

            limpiarFormulario();
            this.gvTareas.DataBind();
        }
    }
}