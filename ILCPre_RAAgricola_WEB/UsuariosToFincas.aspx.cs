using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace Cabana.Campo.RAAgricola.Pre.Web
{
    public partial class UsuariosToFincas : System.Web.UI.Page
    {
        public static Cabana.Campo.RAAgricola.Pre.Web.localhostWs.DS_ILC_Campo.PLwebAccesoUsuFincasDataTable tablaAccesoUsuFincas;
        public static Cabana.Campo.RAAgricola.Pre.Web.localhostWs.DS_ILC_Campo.PLwebUsuariosEmprDataTable tablaUsuariosEmpr;
        public static Cabana.Campo.RAAgricola.Pre.Web.localhostWs.DS_ILC_Campo.PLwebAccesoEmprFincasDataTable tablaAccesoEmprFincas;
        public static Cabana.Campo.RAAgricola.Pre.Web.localhostWs.WSRAAgricola client;

        ConfigSession configSession = new ConfigSession();
        public static DataTable distinctValues;

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
                    if (!configSession.validarSession(getUsuNivelAcceso(), "UsuariosToFincas"))//NumeroDeAcceso, NombrePagina
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
                    tablaAccesoUsuFincas = new Cabana.Campo.RAAgricola.Pre.Web.localhostWs.DS_ILC_Campo.PLwebAccesoUsuFincasDataTable();

                    BindDataUsuariosEmpr_ddl();
                    BindDataFincasEmpr_ddl();
                    BindDataAccesoUsuFincas();
                }
            }
            catch (System.Net.WebException ee)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('A5050 No es posible conectar con el servidor')", true);

            }

        }

        public int getUsuId()
        {
            return Int32.Parse("" + Session["UsuId"]);
        }

        public int getEmprId()
        {
            return Int32.Parse("" + Session["EmprId"]);
        }

        public int getUsuNivelAcceso()
        {
            return Int32.Parse("" + Session["UsuNivelAcceso"]);
        }

        protected void BindDataAccesoUsuFincas()
        {

            tablaAccesoUsuFincas = client.AccesoUsuFincasGet(null, null, null, getEmprId());

            if (!this.ddl_UsuariosEmpr.SelectedValue.Equals("0"))
            {
                this.gvAccesoUsuFincas.DataSource = tablaAccesoUsuFincas;
                (this.gvAccesoUsuFincas.DataSource as DataTable).DefaultView.RowFilter = " UsuId = " + this.ddl_UsuariosEmpr.SelectedValue + " ";
            }
            else
            {
                this.gvAccesoUsuFincas.DataSource = tablaAccesoUsuFincas;
                (this.gvAccesoUsuFincas.DataSource as DataTable).DefaultView.RowFilter = " UsuId = UsuId ";
            }

            this.gvAccesoUsuFincas.DataBind();

        }

        protected void BindDataUsuariosEmpr_ddl()
        {
            tablaUsuariosEmpr = new Cabana.Campo.RAAgricola.Pre.Web.localhostWs.DS_ILC_Campo.PLwebUsuariosEmprDataTable();
            tablaUsuariosEmpr = client.UsuarioEmprGet(null, "", "", getEmprId(), 1, null);

            this.ddl_UsuariosEmpr.DataSource = tablaUsuariosEmpr;
            this.ddl_UsuariosEmpr.DataValueField = "UsuId";
            this.ddl_UsuariosEmpr.DataTextField = "UsuNombreCompleto";
            this.ddl_UsuariosEmpr.DataBind();
            this.ddl_UsuariosEmpr.Items.Insert(0, new ListItem("Seleccionar Usuario", "0"));
        }

        protected void BindDataFincasEmpr_ddl()
        {
            int idUsuarioSeleccionado = Convert.ToInt32(this.ddl_UsuariosEmpr.SelectedValue);
            tablaAccesoEmprFincas = new Cabana.Campo.RAAgricola.Pre.Web.localhostWs.DS_ILC_Campo.PLwebAccesoEmprFincasDataTable();

            tablaAccesoEmprFincas = client.AccesoEmprFincasGet(idUsuarioSeleccionado, getEmprId());

            DataView view = new DataView(tablaAccesoEmprFincas);
            distinctValues = view.ToTable(true, "AccEmprId", "FincaNombre_3");
            this.ddl_FincasProv.DataSource = distinctValues;
            this.ddl_FincasProv.DataValueField = "AccEmprId";
            this.ddl_FincasProv.DataTextField = "FincaNombre_3";
            this.ddl_FincasProv.DataBind();
        }

        protected void lbtn_AddNewAccesoUsuFincas_Click(object sender, EventArgs e)
        {
            try
            {
                DataRow row = tablaAccesoUsuFincas.NewRow();

                row["UsuId"] = this.ddl_UsuariosEmpr.SelectedValue;
                row["UsuAdmIdCrea"] = getUsuId();
                row["AccEmprId"] = this.ddl_FincasProv.SelectedValue;
                row["AccUsuFechaCrea"] = "15/10/2000";
                row["EmprId"] = getEmprId();
                tablaAccesoUsuFincas.Rows.Add(row);
                client.AccesoUsuFincasSet(tablaAccesoUsuFincas);
                gvAccesoUsuFincas.EditIndex = -1;

                BindDataAccesoUsuFincas();
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

        protected void lbtnBuscarAccesoUsuFincas_Click(object sender, EventArgs e)
        {
            String textoBuscar = this.txtControlBuscarTareas.Text;
            this.gvAccesoUsuFincas.DataSource = tablaAccesoUsuFincas;
            (this.gvAccesoUsuFincas.DataSource as DataTable).DefaultView.RowFilter = " UsuNombreCompleto LIKE '%" + textoBuscar + "%' OR FincaNombre LIKE '%" + textoBuscar + "%'  "; //

            limpiarFormulario();
            this.gvAccesoUsuFincas.DataBind();
        }

        protected void gvAccesoUsuFincas_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            if (e.CommandName.Equals("ADD"))
            {

            }
        }
        protected void gvAccesoUsuFincas_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {

            Label lblEditTareaId = (Label)gvAccesoUsuFincas.Rows[e.RowIndex].FindControl("lblEditAccUsuId");

            try
            {
                int idRegistro = Int32.Parse(lblEditTareaId.Text);
                if (tablaAccesoUsuFincas.Rows.Count > 0)
                {
                    DataRow rows = tablaAccesoUsuFincas.FindByAccUsuId(idRegistro);
                    rows["UsuId"] = this.ddl_UsuariosEmpr.SelectedValue;
                    rows["AccEmprId"] = this.ddl_FincasProv.SelectedValue;

                    client.AccesoUsuFincasSet(tablaAccesoUsuFincas);
                    gvAccesoUsuFincas.EditIndex = -1;

                    BindDataAccesoUsuFincas();
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
        protected void gvAccesoUsuFincas_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                Label lblTareaId = (Label)this.gvAccesoUsuFincas.Rows[e.RowIndex].FindControl("lblAccUsuId");
                int idRegistro = Int32.Parse(lblTareaId.Text);
                DataRow rows = tablaAccesoUsuFincas.FindByAccUsuId(idRegistro);
                rows.Delete();
                client.AccesoUsuFincasSet(tablaAccesoUsuFincas);
                BindDataAccesoUsuFincas();
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
            BindDataAccesoUsuFincas();

        }
        protected void gvAccesoUsuFincas_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {

            this.gvAccesoUsuFincas.EditIndex = -1;
            this.gvAccesoUsuFincas.DataSource = tablaAccesoUsuFincas;
            this.gvAccesoUsuFincas.DataBind();

            limpiarFormulario();

        }
        protected void gvAccesoUsuFincas_RowEditing(object sender, GridViewEditEventArgs e)
        {

            this.gvAccesoUsuFincas.EditIndex = e.NewEditIndex;
            this.gvAccesoUsuFincas.DataSource = tablaAccesoUsuFincas;
            this.gvAccesoUsuFincas.DataBind();

            Label IdRegistro = (Label)gvAccesoUsuFincas.Rows[e.NewEditIndex].FindControl("lblEditAccUsuId");
            DataRow rows = tablaAccesoUsuFincas.FindByAccUsuId(Int32.Parse(IdRegistro.Text));



            this.ddl_UsuariosEmpr.SelectedIndex = this.ddl_UsuariosEmpr.Items.IndexOf(this.ddl_UsuariosEmpr.Items.FindByValue(rows["UsuId"].ToString()));
            this.ddl_FincasProv.SelectedIndex = this.ddl_FincasProv.Items.IndexOf(this.ddl_FincasProv.Items.FindByValue(rows["AccEmprId"].ToString()));



            this.lbtn_AddNewTarea.Visible = false;

        }
        protected void gvAccesoUsuFincas_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

            this.gvAccesoUsuFincas.PageIndex = e.NewPageIndex;
            this.gvAccesoUsuFincas.DataSource = tablaAccesoUsuFincas;
            this.gvAccesoUsuFincas.EditIndex = -1;
            this.gvAccesoUsuFincas.DataBind();

            //Session["Nombre"] = "Juan Jose Reyes";

        }

        protected void limpiarFormulario()
        {
            //this.ddl_UsuariosEmpr.SelectedIndex = this.ddl_UsuariosEmpr.Items.IndexOf(this.ddl_UsuariosEmpr.Items.FindByValue("0"));
            this.ddl_FincasProv.SelectedIndex = this.ddl_FincasProv.Items.IndexOf(this.ddl_FincasProv.Items.FindByValue("1")); ;

            this.lbtn_AddNewTarea.Visible = true;
            //this.gvAccesoUsuFincas.DataSource = tablaTareas;
            this.gvAccesoUsuFincas.EditIndex = -1;
            BindDataFincasEmpr_ddl();
        }

        protected void ddl_UsuariosEmpr_Change(object sender, EventArgs e)
        {
            Cabana.Campo.RAAgricola.Pre.Web.localhostWs.DS_ILC_Campo.PLwebAccesoUsuFincasDataTable tablaAccesoUsuFincas_modificado;
            tablaAccesoUsuFincas_modificado = tablaAccesoUsuFincas;
            if (!this.ddl_UsuariosEmpr.SelectedValue.Equals("0"))
            {
                this.gvAccesoUsuFincas.DataSource = tablaAccesoUsuFincas_modificado;
                (this.gvAccesoUsuFincas.DataSource as DataTable).DefaultView.RowFilter = " UsuId = " + this.ddl_UsuariosEmpr.SelectedValue + " ";
                //this.gvAccesoUsuFincas.DataBind();
            }
            else
            {
                this.gvAccesoUsuFincas.DataSource = tablaAccesoUsuFincas;
                (this.gvAccesoUsuFincas.DataSource as DataTable).DefaultView.RowFilter = " UsuId = UsuId ";
                //this.gvAccesoUsuFincas.DataBind();        
            }
            this.gvAccesoUsuFincas.DataBind();

            BindDataFincasEmpr_ddl();
        }
    }
}