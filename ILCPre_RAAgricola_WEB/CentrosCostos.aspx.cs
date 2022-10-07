using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;


namespace Cabana.Campo.RAAgricola.Pre.Web
{
    public partial class CentrosCostos : System.Web.UI.Page
    {
        public static Cabana.Campo.RAAgricola.Pre.Web.localhostWs.DS_ILC_Campo.PLwebEmprAdministrativasDataTable tablaEmpresasAdm;
        public static Cabana.Campo.RAAgricola.Pre.Web.localhostWs.DS_ILC_Campo.PLwebCentroCostosEmprDataTable tablaCentroCostosEmpr;

        public static Cabana.Campo.RAAgricola.Pre.Web.localhostWs.WSRAAgricola client;

        ConfigSession configSession = new ConfigSession();
        //int EmprId;
        //int UsuId;
        int UsuNivelAcceso;
        public static DataTable distinctValuesEmpresas;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(Session["UsuId"] as string))
                {
                    getValidarSesion();

                }
                else
                {
                    Response.Write("<script language='javascript'> window.location.replace('Login.aspx');</" + "script>");
                    Response.End();
                }


                if (!IsPostBack)
                {
                    client = new Cabana.Campo.RAAgricola.Pre.Web.localhostWs.WSRAAgricola();
                    tablaCentroCostosEmpr = new Cabana.Campo.RAAgricola.Pre.Web.localhostWs.DS_ILC_Campo.PLwebCentroCostosEmprDataTable();
                    BindDataEmpresasAdm_ddl();
                    BindDataTareas();

                }
            }
            catch (System.Net.WebException ee)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('A5050 No es posible conectar con el servidor')", true);

            }

        }

        public Boolean getValidarSesion() {
            UsuNivelAcceso = Int32.Parse("" + Session["UsuNivelAcceso"]);

            if (!configSession.validarSession(UsuNivelAcceso, "CentrosCostos"))//NumeroDeAcceso, NombrePagina
            {
                Response.Write("<script language='javascript'> window.location.replace('PaginaOut.aspx');</" + "script>");
                Response.End();
                return false;
            }
            else {
                return true;
            }
        }

        public int getUsuId()
        {
            getValidarSesion();            
            return Int32.Parse("" + Session["UsuId"]);
        }
        public int getEmprId()
        {
            getValidarSesion();
            return Int32.Parse("" + Session["EmprId"]);
        }

        protected void BindDataTareas()
        {
            UsuNivelAcceso = Int32.Parse("" + Session["UsuNivelAcceso"]);
            if (UsuNivelAcceso.Equals(1))
            {
                tablaCentroCostosEmpr = client.PLwebCentroCostosEmprGet(null,null);
            }
            else
            {
                tablaCentroCostosEmpr = client.PLwebCentroCostosEmprGet(null, getEmprId());
            }

            this.gvTareas.DataSource = tablaCentroCostosEmpr;
            (this.gvTareas.DataSource as DataTable).DefaultView.RowFilter = " CcEmprId = " + this.ddl_Empresas.SelectedValue + " ";
            this.gvTareas.DataBind();
        }
        protected void BindDataEmpresasAdm_ddl()
        {
            UsuNivelAcceso = Int32.Parse("" + Session["UsuNivelAcceso"]);
            tablaEmpresasAdm = new Cabana.Campo.RAAgricola.Pre.Web.localhostWs.DS_ILC_Campo.PLwebEmprAdministrativasDataTable();
            if (UsuNivelAcceso.Equals(1))
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
        }

        protected void lbtn_AddNewTarea_Click(object sender, EventArgs e)
        {
            try
            {

                DataRow row = tablaCentroCostosEmpr.NewRow();
                if (!this.txtFrmCcCodigo.Text.Equals("")
                    && !this.txtFrmCcDescripcion.Text.Equals("")
                    && !this.txtFrmCcComentario.Text.Equals(""))
                {

                    row["CcDescripcion"]    = this.txtFrmCcDescripcion.Text;
                    row["CcCodigo"]         = this.txtFrmCcCodigo.Text;
                    row["CcComentario"]     = this.txtFrmCcComentario.Text;
                    row["CcEmprId"]         = getEmprId();
                    row["CcUsuCrea"]        = getUsuId();
                    tablaCentroCostosEmpr.Rows.Add(row);
                    client.PLwebCentroCostosEmprSet(tablaCentroCostosEmpr);
                    gvTareas.EditIndex = -1;

                    BindDataTareas();
                    limpiarFormulario();
                }
                else
                {
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
            BindDataTareas();
            String textoBuscar = this.txtControlBuscarTareas.Text;
            this.gvTareas.DataSource = tablaCentroCostosEmpr;
            (this.gvTareas.DataSource as DataTable).DefaultView.RowFilter = " CcDescripcion LIKE '%" + textoBuscar + "%'  "; //

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
            BindDataTareas();
            Label lblEditTareaId = (Label)gvTareas.Rows[e.RowIndex].FindControl("lblEditUsuId");

            try
            {
                int idRegistro = Int32.Parse(lblEditTareaId.Text);
                if (tablaCentroCostosEmpr.Rows.Count > 0)
                {
                    DataRow rows = tablaCentroCostosEmpr.FindByCcosId(idRegistro);
                    rows["CcDescripcion"]   = this.txtFrmCcDescripcion.Text;
                    rows["CcCodigo"]        = this.txtFrmCcCodigo.Text;
                    rows["CcComentario"]    = this.txtFrmCcComentario.Text;


                    client.PLwebCentroCostosEmprSet(tablaCentroCostosEmpr);
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
            BindDataTareas();
            try
            {
                Label lblTareaId = (Label)this.gvTareas.Rows[e.RowIndex].FindControl("lblUsuId");
                int idRegistro = Int32.Parse(lblTareaId.Text);
                DataRow rows = tablaCentroCostosEmpr.FindByCcosId(idRegistro);
                rows.Delete();
                client.PLwebCentroCostosEmprSet(tablaCentroCostosEmpr);
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
            this.gvTareas.DataSource = tablaCentroCostosEmpr;
            this.gvTareas.DataBind();

            limpiarFormulario();

        }
        protected void gvTareas_RowEditing(object sender, GridViewEditEventArgs e)
        {

            this.gvTareas.EditIndex = e.NewEditIndex;
            this.gvTareas.DataSource = tablaCentroCostosEmpr;
            this.gvTareas.DataBind();

            Label IdRegistro = (Label)gvTareas.Rows[e.NewEditIndex].FindControl("lblEditUsuId");
            DataRow rows = tablaCentroCostosEmpr.FindByCcosId(Int32.Parse(IdRegistro.Text));


            
                        
            
            //this.txtFrmUsuNombre.Text           = rows["UsuNombre"].ToString();
            //this.txtFrmUsuNombreCompleto.Text   = rows["UsuNombreCompleto"].ToString();

            this.txtFrmCcCodigo.Text            = rows["CcCodigo"].ToString();
            this.txtFrmCcDescripcion.Text       = rows["CcDescripcion"].ToString();
            this.txtFrmCcComentario.Text        = rows["CcComentario"].ToString();
            this.ddl_Empresas.SelectedIndex = this.ddl_Empresas.Items.IndexOf(this.ddl_Empresas.Items.FindByValue(rows["CcEmprId"].ToString()));

            //this.ddl_UsuNivelAcceso.SelectedIndex = this.ddl_UsuNivelAcceso.Items.IndexOf(this.ddl_UsuNivelAcceso.Items.FindByValue(rows["UsuNivelAcceso"].ToString()));
            //this.ddlFrmUsuEstatus.SelectedIndex = this.ddlFrmUsuEstatus.Items.IndexOf(this.ddlFrmUsuEstatus.Items.FindByValue(rows["UsuEstatus"].ToString()));


            this.lbtn_AddNewTarea.Visible = false;
            this.ddl_Empresas.Enabled = false;

        }
        protected void gvTareas_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

            this.gvTareas.PageIndex = e.NewPageIndex;
            this.gvTareas.DataSource = tablaCentroCostosEmpr;
            this.gvTareas.EditIndex = -1;
            this.gvTareas.DataBind();

            //Session["Nombre"] = "Juan Jose Reyes";

        }

        protected void limpiarFormulario()
        {
            //this.ddl_UsuNivelAcceso.SelectedIndex = this.ddl_UsuNivelAcceso.Items.IndexOf(this.ddl_UsuNivelAcceso.Items.FindByValue("0"));
            //this.ddlFrmUsuEstatus.SelectedIndex = this.ddlFrmUsuEstatus.Items.IndexOf(this.ddlFrmUsuEstatus.Items.FindByValue("1")); ;

            this.txtFrmCcCodigo.Text        = "";
            this.txtFrmCcDescripcion.Text   = "";
            this.txtFrmCcComentario.Text    = "";
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
            this.gvTareas.DataSource = tablaCentroCostosEmpr;
            (this.gvTareas.DataSource as DataTable).DefaultView.RowFilter = " CcEmprId = " + this.ddl_Empresas.SelectedValue + " "; //

            limpiarFormulario();
            this.gvTareas.DataBind();
        }
    }
}