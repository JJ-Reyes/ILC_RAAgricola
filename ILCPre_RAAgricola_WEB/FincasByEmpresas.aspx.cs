using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

//, Convert.ToDateTime("15/10/2000")

namespace Cabana.Campo.RAAgricola.Pre.Web
{
    public partial class FincasByEmpresas : System.Web.UI.Page
    {
        public static Cabana.Campo.RAAgricola.Pre.Web.localhostWs.DS_ILC_Campo.PLwebAccesoEmprFincasDataTable tablaAccesoEmprFincas;
        public static Cabana.Campo.RAAgricola.Pre.Web.localhostWs.DS_ILC_Campo.PLwebEmprAdministrativasDataTable tablaEmpresasAdm;
        public static Cabana.Campo.RAAgricola.Pre.Web.localhostWs.DS_ILC_Campo.VPLwebLotesProvDataTable tablaProveedores;


        public static Cabana.Campo.RAAgricola.Pre.Web.localhostWs.WSRAAgricola client;

        ConfigSession configSession = new ConfigSession();
        //int EmprId;
        //int UsuId;
        //int UsuNivelAcceso;
        public static DataTable distinctValues;
        public static DataTable distinctValuesProveedores;
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
                    if (!configSession.validarSession(getUsuNivelAcceso(), "FincasByEmpresas"))//NumeroDeAcceso, NombrePagina
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
                    tablaAccesoEmprFincas = new Cabana.Campo.RAAgricola.Pre.Web.localhostWs.DS_ILC_Campo.PLwebAccesoEmprFincasDataTable();
                    BindDataEmpresasAdm_ddl();
                    BindDataAccesoEmprFincas();
                    //BindDataFincasEmpr_ddl();
                    BindDataProveedores_ddl();
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

        protected void BindDataAccesoEmprFincas()
        {

            tablaAccesoEmprFincas = client.AccesoEmprFincasGet(null, null);

            this.gvAccesoEmprFincas.DataSource = tablaAccesoEmprFincas;
            (this.gvAccesoEmprFincas.DataSource as DataTable).DefaultView.RowFilter = " EmprId = " + this.ddl_Empresas.SelectedValue + " "; //
            this.gvAccesoEmprFincas.DataBind();

        }

        protected void BindDataEmpresasAdm_ddl()
        {
            tablaEmpresasAdm = new Cabana.Campo.RAAgricola.Pre.Web.localhostWs.DS_ILC_Campo.PLwebEmprAdministrativasDataTable();
            tablaEmpresasAdm = client.EmpresasAdmGet(null);
            DataView view = new DataView(tablaEmpresasAdm);
            distinctValuesEmpresas = view.ToTable(true, "EmprId", "EmprNombre");
            this.ddl_Empresas.DataSource = distinctValuesEmpresas;
            this.ddl_Empresas.DataValueField = "EmprId";
            this.ddl_Empresas.DataTextField = "EmprNombre";
            this.ddl_Empresas.DataBind();
            //this.ddl_Empresas.Items.Insert(0, new ListItem("Seleccionar La empresa", ""));
        }

        protected void BindDataFincasEmpr_ddl()
        {
 
            DataView view = new DataView(tablaProveedores);
            distinctValues = view.ToTable(true, "FincaId", "FincaNombre", "gpId");

            this.ddl_Fincas.DataSource = distinctValues;
            (this.ddl_Fincas.DataSource as DataTable).DefaultView.RowFilter = " gpId LIKE '" + this.ddl_Proveedores.SelectedValue + "'  "; //

            this.ddl_Fincas.DataValueField = "FincaId";
            this.ddl_Fincas.DataTextField = "FincaNombre";
            this.ddl_Fincas.DataBind();
            this.ddl_Fincas.Items.Insert(0, new ListItem("Todos", "0"));
        }

        protected void BindDataProveedores_ddl()
        {
            //try{
                tablaProveedores = new Cabana.Campo.RAAgricola.Pre.Web.localhostWs.DS_ILC_Campo.VPLwebLotesProvDataTable();
                tablaProveedores = client.ProveedoresAdminGet("", null, null);
                DataView view = new DataView(tablaProveedores);
                distinctValuesProveedores = view.ToTable(true, "gpId", "gpNombre");
                this.ddl_Proveedores.DataSource = distinctValuesProveedores;
                this.ddl_Proveedores.DataValueField = "gpId";
                this.ddl_Proveedores.DataTextField = "gpNombre";
                this.ddl_Proveedores.DataBind();
                this.ddl_Proveedores.Items.Insert(0, new ListItem("Seleccione un Proveedor", "0"));
            //}catch(Exception e){}
        }


        protected void lbtn_AddNewAccesoEmprFincas_Click(object sender, EventArgs e)
        {
            try
            {
                if (!this.ddl_Fincas.SelectedValue.Equals("0"))
                {
                    DataRow row = tablaAccesoEmprFincas.NewRow();

                    row["UsuAdmIdCrea"] = getUsuId(); //this.ddl_Empresas.SelectedValue;
                    row["FincaId"] = this.ddl_Fincas.SelectedValue;
                    row["EmprId"] = this.ddl_Empresas.SelectedValue;
                    row["AccEmprFechaCrea"] = "15/10/2000";

                    tablaAccesoEmprFincas.Rows.Add(row);
                    client.AccesoEmprFincasSet(tablaAccesoEmprFincas);
                    gvAccesoEmprFincas.EditIndex = -1;
                }
                else
                {
                    client.InsertAllFincas(getUsuId(), this.ddl_Proveedores.SelectedValue, Int32.Parse(this.ddl_Empresas.SelectedValue));
                }
                BindDataAccesoEmprFincas();
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

        protected void lbtnBuscarAccesoEmprFincas_Click(object sender, EventArgs e)
        {
            String textoBuscar = this.txtControlBuscarTareas.Text;
            this.gvAccesoEmprFincas.DataSource = tablaAccesoEmprFincas;
            (this.gvAccesoEmprFincas.DataSource as DataTable).DefaultView.RowFilter = " EmprId = " + this.ddl_Empresas.SelectedValue + " AND (EmprNombre_2 LIKE '%" + textoBuscar + "%' OR FincaNombre_3 LIKE '%" + textoBuscar + "%' OR gpId_3 LIKE '%" + textoBuscar + "%' )  "; //

            limpiarFormulario();
            this.gvAccesoEmprFincas.DataBind();
        }

        protected void lbtnBuscarProv_Click(object sender, EventArgs e)
        {
            String textoBuscar = this.txtBuscarProv.Text;
            this.ddl_Proveedores.DataSource = distinctValuesProveedores;
            //this.ddl_Proveedores.Items.Insert(0, new ListItem("Seleccionar Proveedor", "0"));
            (this.ddl_Proveedores.DataSource as DataTable).DefaultView.RowFilter = " gpNombre LIKE '%" + textoBuscar + "%'  OR gpId LIKE '%" + textoBuscar + "%'  ";
            this.ddl_Proveedores.DataBind();

            BindDataFincasEmpr_ddl();


        }


        protected void gvAccesoEmprFincas_RowCommand(object sender, GridViewCommandEventArgs e)
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

        protected void gvAccesoEmprFincas_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                Label lblTareaId = (Label)this.gvAccesoEmprFincas.Rows[e.RowIndex].FindControl("lblAccEmprId");
                int idRegistro = Int32.Parse(lblTareaId.Text);
                DataRow rows = tablaAccesoEmprFincas.FindByAccEmprId(idRegistro);
                rows.Delete();
                client.AccesoEmprFincasSet(tablaAccesoEmprFincas);
                BindDataAccesoEmprFincas();
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
            BindDataAccesoEmprFincas();

        }
        /*
        protected void gvAccesoEmprFincas_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {

            this.gvAccesoEmprFincas.EditIndex = -1;
            this.gvAccesoEmprFincas.DataSource = tablaAccesoEmprFincas;
            this.gvAccesoEmprFincas.DataBind();

            limpiarFormulario();

        }
        protected void gvAccesoEmprFincas_RowEditing(object sender, GridViewEditEventArgs e)
        {

            this.gvAccesoEmprFincas.EditIndex = e.NewEditIndex;
            this.gvAccesoEmprFincas.DataSource = tablaAccesoEmprFincas;
            this.gvAccesoEmprFincas.DataBind();

            Label IdRegistro = (Label)gvAccesoEmprFincas.Rows[e.NewEditIndex].FindControl("lblEditAccEmprId");
            DataRow rows = tablaAccesoEmprFincas.FindByAccEmprId(Int32.Parse(IdRegistro.Text));



            this.ddl_Empresas.SelectedIndex = this.ddl_Empresas.Items.IndexOf(this.ddl_Empresas.Items.FindByValue(rows["UsuId"].ToString()));
            this.ddl_Fincas.SelectedIndex = this.ddl_Fincas.Items.IndexOf(this.ddl_Fincas.Items.FindByValue(rows["AccEmprId"].ToString()));



            this.lbtn_AddNewTarea.Visible = false;

        }
         */
        protected void gvAccesoEmprFincas_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

            this.gvAccesoEmprFincas.PageIndex = e.NewPageIndex;
            this.gvAccesoEmprFincas.DataSource = tablaAccesoEmprFincas;
            this.gvAccesoEmprFincas.EditIndex = -1;
            this.gvAccesoEmprFincas.DataBind();

            //Session["Nombre"] = "Juan Jose Reyes";

        }

        protected void limpiarFormulario()
        {
            //this.ddl_Empresas.SelectedIndex = this.ddl_Empresas.Items.IndexOf(this.ddl_Empresas.Items.FindByValue("0"));
            this.ddl_Fincas.SelectedIndex = this.ddl_Fincas.Items.IndexOf(this.ddl_Fincas.Items.FindByValue("1")); ;

            this.lbtn_AddNewTarea.Visible = true;
            //this.gvAccesoEmprFincas.DataSource = tablaTareas;
            this.gvAccesoEmprFincas.EditIndex = -1;
        }

        protected void ddl_Proveedores_Change(object sender, EventArgs e)
        {
            BindDataFincasEmpr_ddl();

        }

        protected void ddl_Empresas_Change(object sender, EventArgs e)
        {
            BindDataEmpresas_ddl();

        }

        protected void BindDataEmpresas_ddl()
        {

            this.gvAccesoEmprFincas.DataSource = tablaAccesoEmprFincas;
            (this.gvAccesoEmprFincas.DataSource as DataTable).DefaultView.RowFilter = " EmprId = " + this.ddl_Empresas.SelectedValue + " "; //

            //limpiarFormulario();
            this.gvAccesoEmprFincas.DataBind();

        }
    }
}