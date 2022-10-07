using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace Cabana.Campo.RAAgricola.Pre.Web
{
    public partial class UsuariosCuadrillas : System.Web.UI.Page
    {
        public static Cabana.Campo.RAAgricola.Pre.Web.localhostWs.DS_ILC_Campo.PLwebUsuariosEmprDataTable tablaUsuariosEmpr;

        public static Cabana.Campo.RAAgricola.Pre.Web.localhostWs.DS_ILC_Campo.PLwebAccUsuCuadrillasDataTable tablaAccUsuCuadrillas;
        public static Cabana.Campo.RAAgricola.Pre.Web.localhostWs.DS_ILC_Campo.PLwebCuadrillasDataTable tablaCuadrillas;
        public static Cabana.Campo.RAAgricola.Pre.Web.localhostWs.DS_ILC_Campo.PLwebFrentesDataTable tablaFrentes;


        public static Cabana.Campo.RAAgricola.Pre.Web.localhostWs.WSRAAgricola client;

        ConfigSession configSession = new ConfigSession();
        //int EmprId;
        //int UsuId;
        //int UsuNivelAcceso;
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
                    tablaAccUsuCuadrillas = new Cabana.Campo.RAAgricola.Pre.Web.localhostWs.DS_ILC_Campo.PLwebAccUsuCuadrillasDataTable();

                    BindDataUsuariosEmpr_ddl();
                    BindDataFrentes_ddl();
                    BindDataAccUsuCuadrillas();
                    tablaCuadrillas = client.CuadrillaGet(null, null, 1, getEmprId(), null);
                }
            }
            catch (System.Net.WebException ee)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('A5050 No es posible conectar con el servidor')", true);

            }

        }

        protected void BindDataAccUsuCuadrillas()
        {

            tablaAccUsuCuadrillas = client.AccUsuCuadrillasGet(null, getEmprId(), null, null);
            //this.gvAccesoUsuFincas.DataSource = tablaAccesoUsuFincas;

            if (!this.ddl_UsuariosEmpr.SelectedValue.Equals("0"))
            {
                this.gvAccesoUsuCuadrillas.DataSource = tablaAccUsuCuadrillas;
                (this.gvAccesoUsuCuadrillas.DataSource as DataTable).DefaultView.RowFilter = " UsuId = " + this.ddl_UsuariosEmpr.SelectedValue + " ";
            }
            else
            {
                this.gvAccesoUsuCuadrillas.DataSource = tablaAccUsuCuadrillas;
                (this.gvAccesoUsuCuadrillas.DataSource as DataTable).DefaultView.RowFilter = " UsuId = UsuId ";
            }

            this.gvAccesoUsuCuadrillas.DataBind();

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

        protected void BindDataFrentes_ddl()
        {
            tablaFrentes = new Cabana.Campo.RAAgricola.Pre.Web.localhostWs.DS_ILC_Campo.PLwebFrentesDataTable();

            tablaFrentes = client.FrenteGet(null, getEmprId(), 1, "");

            DataView view = new DataView(tablaFrentes);
            distinctValues = view.ToTable(true, "FrenteId", "FrenteNombre");
            this.ddl_Frentes.DataSource = distinctValues;
            this.ddl_Frentes.DataValueField = "FrenteId";
            this.ddl_Frentes.DataTextField = "FrenteNombre";
            this.ddl_Frentes.DataBind();
            this.ddl_Frentes.Items.Insert(0, new ListItem("Seleccionar Frente", "0"));
        }

        protected void BindDataCuadrillas_ddl()
        {
            tablaCuadrillas = new Cabana.Campo.RAAgricola.Pre.Web.localhostWs.DS_ILC_Campo.PLwebCuadrillasDataTable();

            tablaCuadrillas = client.CuadrillaGet(null, null, 1, getEmprId(), null);
            DataView view = new DataView(tablaCuadrillas);
            distinctValues = view.ToTable(true, "CuaId", "CuaNombre");
            this.ddl_Cuadrillas.DataSource = distinctValues;
            this.ddl_Cuadrillas.DataValueField = "CuaId";
            this.ddl_Cuadrillas.DataTextField = "CuaNombre";
            this.ddl_Cuadrillas.DataBind();
            this.ddl_UsuariosEmpr.Items.Insert(0, new ListItem("Seleccionar Cuadrilla", "0"));
        }



        protected void lbtn_AddNewAccesoUsuCuadrillas_Click(object sender, EventArgs e)
        {
            try
            {
                if (!this.ddl_Cuadrillas.SelectedValue.Equals("0"))
                {
                    DataRow row = tablaAccUsuCuadrillas.NewRow();

                    row["UsuId"] = this.ddl_UsuariosEmpr.SelectedValue;
                    row["CuaId"] = this.ddl_Cuadrillas.SelectedValue;
                    row["AcFecRegistro"] = "15/10/2000";
                    row["EmprId"] = getEmprId();
                    tablaAccUsuCuadrillas.Rows.Add(row);
                    client.AccUsuCuadrillasSet(tablaAccUsuCuadrillas);
                    this.gvAccesoUsuCuadrillas.EditIndex = -1;

                    BindDataAccUsuCuadrillas();
                    limpiarFormulario();
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('A1050 Debe asignar una Cuadrilla')", true);

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

        protected void lbtnBuscarAccesoUsuCuadrillas_Click(object sender, EventArgs e)
        {
            String textoBuscar = this.txtControlBuscarTareas.Text;
            this.gvAccesoUsuCuadrillas.DataSource = tablaAccUsuCuadrillas;
            (this.gvAccesoUsuCuadrillas.DataSource as DataTable).DefaultView.RowFilter = " UsuNombreCompleto LIKE '%" + textoBuscar + "%' OR CuaNombre LIKE '%" + textoBuscar + "%'  "; //

            limpiarFormulario();
            this.gvAccesoUsuCuadrillas.DataBind();
        }

        protected void gvAccesoUsuCuadrillas_RowCommand(object sender, GridViewCommandEventArgs e)
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
        protected void gvAccesoUsuCuadrillas_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {

            Label lblEditTareaId = (Label)gvAccesoUsuCuadrillas.Rows[e.RowIndex].FindControl("lblEditAccUsuId");

            try
            {
                int idRegistro = Int32.Parse(lblEditTareaId.Text);
                if (tablaAccUsuCuadrillas.Rows.Count > 0)
                {
                    DataRow rows = tablaAccUsuCuadrillas.FindByAcCuaId(idRegistro);
                    rows["UsuId"] = this.ddl_UsuariosEmpr.SelectedValue;
                    rows["CuaId"] = this.ddl_Cuadrillas.SelectedValue;

                    client.AccUsuCuadrillasSet(tablaAccUsuCuadrillas);
                    this.gvAccesoUsuCuadrillas.EditIndex = -1;

                    this.BindDataAccUsuCuadrillas();
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
        protected void gvAccesoUsuCuadrillas_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                Label lblTareaId = (Label)this.gvAccesoUsuCuadrillas.Rows[e.RowIndex].FindControl("lblAccUsuId");
                int idRegistro = Int32.Parse(lblTareaId.Text);
                DataRow rows = tablaAccUsuCuadrillas.FindByAcCuaId(idRegistro);
                rows.Delete();
                client.AccUsuCuadrillasSet(tablaAccUsuCuadrillas);
                this.BindDataAccUsuCuadrillas();
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
            this.BindDataAccUsuCuadrillas();

        }
        protected void gvAccesoUsuCuadrillas_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {

            this.gvAccesoUsuCuadrillas.EditIndex = -1;
            this.gvAccesoUsuCuadrillas.DataSource = tablaAccUsuCuadrillas;
            this.gvAccesoUsuCuadrillas.DataBind();

            limpiarFormulario();

        }
        protected void gvAccesoUsuCuadrillas_RowEditing(object sender, GridViewEditEventArgs e)
        {

            this.gvAccesoUsuCuadrillas.EditIndex = e.NewEditIndex;
            this.gvAccesoUsuCuadrillas.DataSource = tablaAccUsuCuadrillas;
            this.gvAccesoUsuCuadrillas.DataBind();

            Label IdRegistro = (Label)gvAccesoUsuCuadrillas.Rows[e.NewEditIndex].FindControl("lblEditAccUsuId");
            DataRow rows = tablaAccUsuCuadrillas.FindByAcCuaId(Int32.Parse(IdRegistro.Text));



            this.ddl_UsuariosEmpr.SelectedIndex = this.ddl_UsuariosEmpr.Items.IndexOf(this.ddl_UsuariosEmpr.Items.FindByValue(rows["UsuId"].ToString()));
            this.ddl_Cuadrillas.SelectedIndex = this.ddl_Cuadrillas.Items.IndexOf(this.ddl_Cuadrillas.Items.FindByValue(rows["CuaId"].ToString()));



            this.lbtn_AddNewTarea.Visible = false;

        }
        protected void gvAccesoUsuCuadrillas_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

            this.gvAccesoUsuCuadrillas.PageIndex = e.NewPageIndex;
            this.gvAccesoUsuCuadrillas.DataSource = tablaAccUsuCuadrillas;
            this.gvAccesoUsuCuadrillas.EditIndex = -1;
            this.gvAccesoUsuCuadrillas.DataBind();


        }

        protected void limpiarFormulario()
        {
            //this.ddl_UsuariosEmpr.SelectedIndex = this.ddl_UsuariosEmpr.Items.IndexOf(this.ddl_UsuariosEmpr.Items.FindByValue("0"));
            this.ddl_Cuadrillas.SelectedIndex = this.ddl_Cuadrillas.Items.IndexOf(this.ddl_Cuadrillas.Items.FindByValue("0")); ;

            this.lbtn_AddNewTarea.Visible = true;
            //this.gvAccesoUsuFincas.DataSource = tablaTareas;
            this.gvAccesoUsuCuadrillas.EditIndex = -1;
        }

        protected void ddl_UsuariosEmpr_Change(object sender, EventArgs e)
        {
            Cabana.Campo.RAAgricola.Pre.Web.localhostWs.DS_ILC_Campo.PLwebAccUsuCuadrillasDataTable tablaAccesoUsuCuadrillas_modificado;
            tablaAccesoUsuCuadrillas_modificado = tablaAccUsuCuadrillas;
            if (!this.ddl_UsuariosEmpr.SelectedValue.Equals("0"))
            {
                this.gvAccesoUsuCuadrillas.DataSource = tablaAccesoUsuCuadrillas_modificado;
                (this.gvAccesoUsuCuadrillas.DataSource as DataTable).DefaultView.RowFilter = " UsuId = " + this.ddl_UsuariosEmpr.SelectedValue + " ";
                //this.gvAccesoUsuFincas.DataBind();
            }
            else
            {
                this.gvAccesoUsuCuadrillas.DataSource = tablaAccUsuCuadrillas;
                (this.gvAccesoUsuCuadrillas.DataSource as DataTable).DefaultView.RowFilter = " UsuId = UsuId ";
                //this.gvAccesoUsuFincas.DataBind();        
            }
            this.gvAccesoUsuCuadrillas.DataBind();

        }

        protected void ddl_Frentes_Change(object sender, EventArgs e)
        {


            Cabana.Campo.RAAgricola.Pre.Web.localhostWs.DS_ILC_Campo.PLwebCuadrillasDataTable tablaCuadrillas_modificado;
            tablaCuadrillas_modificado = tablaCuadrillas;
            /*
            if (!this.ddl_Frentes.SelectedValue.Equals("0"))
            {*/
            //this.ddl_Cuadrillas.DataSource = tablaCuadrillas_modificado;
            tablaCuadrillas_modificado.DefaultView.RowFilter = " FrenteId = " + this.ddl_Frentes.SelectedValue + " ";
            //(this.ddl_Cuadrillas.DataSource as DataTable).DefaultView.RowFilter = " FrenteId = " + this.ddl_Frentes.SelectedValue + " ";
            //DataView view = new DataView(tablaCuadrillas_modificado);
            //distinctValues = view.ToTable(true, "CuaId", "CuaNombre");
            this.ddl_Cuadrillas.DataSource = tablaCuadrillas_modificado;
            this.ddl_Cuadrillas.DataValueField = "CuaId";
            this.ddl_Cuadrillas.DataTextField = "CuaNombre";
            this.ddl_Cuadrillas.DataBind();
            this.ddl_Cuadrillas.Items.Insert(0, new ListItem("Seleccionar Cuadrilla", "0"));
            /*
    }
    else
    {
        this.ddl_Cuadrillas.DataSource = tablaCuadrillas;
        this.ddl_Cuadrillas.DataValueField = "CuaId";
        this.ddl_Cuadrillas.DataTextField = "CuaNombre";
        this.ddl_Cuadrillas.DataBind();
        this.ddl_Cuadrillas.Items.Insert(0, new ListItem("Seleccionar Cuadrilla", "0"));
             * */

        }
        //this.ddl_Cuadrillas.DataBind();


    }
}