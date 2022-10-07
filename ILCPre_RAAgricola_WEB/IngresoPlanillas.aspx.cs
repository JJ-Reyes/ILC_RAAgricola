using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Drawing;
using System.Web.Services.Description;

namespace Cabana.Campo.RAAgricola.Pre.Web
{
    public partial class IngresoPlanillas : System.Web.UI.Page
    {
        public static Cabana.Campo.RAAgricola.Pre.Web.localhostWs.DS_ILC_Campo.PLwebQuincenasDataTable tablaQuincenas;
        public static Cabana.Campo.RAAgricola.Pre.Web.localhostWs.DS_ILC_Campo.PLwebEmpleadoDataTable tablaEmpleados;
        public static Cabana.Campo.RAAgricola.Pre.Web.localhostWs.DS_ILC_Campo.VPLwebLotesProvDataTable tablaProveedores;
        public static Cabana.Campo.RAAgricola.Pre.Web.localhostWs.DS_ILC_Campo.VPLwebLotesProveedorDataTable tablaFincasLotes;
        public static Cabana.Campo.RAAgricola.Pre.Web.localhostWs.DS_ILC_Campo.PLwebFrentesDataTable tablaFrentes;
        public static Cabana.Campo.RAAgricola.Pre.Web.localhostWs.DS_ILC_Campo.PLwebCuadrillasDataTable tablaCuadrillas;
        public static Cabana.Campo.RAAgricola.Pre.Web.localhostWs.DS_ILC_Campo.PLwebTareasDataTable tablaTareas;
        public static Cabana.Campo.RAAgricola.Pre.Web.localhostWs.DS_ILC_Campo.PLwebMoviPlaniDataTable tablaMoviPlanilla;
        public static Cabana.Campo.RAAgricola.Pre.Web.localhostWs.DS_ILC_Campo.PLwebAccUsuCuadrillasDataTable tablaAccUsuCuadrillas;
        public static Cabana.Campo.RAAgricola.Pre.Web.localhostWs.DS_ILC_Campo.PLwebEmpCuaDataTable tablaEmpCua;
        public static Cabana.Campo.RAAgricola.Pre.Web.localhostWs.DS_ILC_Campo.PLwebCentroCostosEmprDataTable tablaCentroCostosEmpr;

        public static DataTable tableEmpSeleccionados;
        String mesajeAccion = "";

        public static Cabana.Campo.RAAgricola.Pre.Web.localhostWs.WSRAAgricola client;
        ConfigSession configSession = new ConfigSession();

        int QuinIdGet = 0;
        public static DataTable distinctValues;

        String zafraQuincena = "";


        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {

                if (!string.IsNullOrEmpty(Session["UsuId"] as string))
                {
                    /*
                    if (this.HddEmprId.Value == "")

                    {
                        this.HddUsuId.Value = "" + HttpContext.Current.Session["UsuId"];
                        this.HddEmprId.Value = "" + Session["EmprId"];
                        this.HddUsuNivelAcceso.Value = "" + Session["UsuNivelAcceso"]; // Int32.Parse(
                    }
                    */

                    if (Response.Cookies["EmprId"].Value == null) {

                        Response.Cookies["UsuId"].Value = "" + Session["UsuId"];
                        Response.Cookies["UsuId"].Expires = DateTime.Now.AddDays(1);

                        Response.Cookies["EmprId"].Value = "" + Session["EmprId"];
                        Response.Cookies["EmprId"].Expires = DateTime.Now.AddDays(1);

                        Response.Cookies["UsuNivelAcceso"].Value = "" + Session["UsuNivelAcceso"];
                        Response.Cookies["UsuNivelAcceso"].Expires = DateTime.Now.AddDays(1);
                    }
                    if (getValidarSesion())
                    {
                        if (!string.IsNullOrEmpty(Page.Request.QueryString["id"] as string))
                        {
                            QuinIdGet = Int32.Parse(Page.Request.QueryString["id"]);
                            if (!IsPostBack)
                            {
                                BindDataQuincenas();
                                BindData();

                            }
                        }
                        else
                        {
                            Response.Write("<script language='javascript'> window.location.replace('Home.aspx');</" + "script>");
                            Response.End();
                        }
                    }

                }
                else
                {

                    Response.Write("<script language='javascript'> window.location.replace('Login.aspx');</" + "script>");
                    Response.End();
                }
            }
            catch (System.NullReferenceException er) {
                Console.WriteLine("Filtro BuscarEmpleado", er);
            }
        }

        private DataTable dataTableEmpSeleccionados()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add(new DataColumn("EmpId", typeof(string)));
            dt.Columns.Add(new DataColumn("nombreCompleto", typeof(string)));
            tableEmpSeleccionados = dt;
            return dt;
        }

        public void cargarTableEmpSeleccionados(String EmpId, String nombreCompleto) {

            DataRow dr = null;
            dr = tableEmpSeleccionados.NewRow();

            dr["EmpId"] = EmpId;
            dr["nombreCompleto"] = nombreCompleto;
            tableEmpSeleccionados.Rows.Add(dr);
            cargarGridEmpSeleccionados();

        }

        protected void gvBuscarEmpleados_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {

            cargarGridEmpSeleccionados();
            Label lblBuscarEmpId = (Label)this.gvListaEmpSeleccionados.Rows[e.RowIndex].FindControl("lblBuscarEmpId");
            LinkButton gvlbtn_updateEmpleado = (LinkButton)this.gvListaEmpSeleccionados.Rows[e.RowIndex].FindControl("gvlbtn_updateEmpleado");
            gvlbtn_updateEmpleado.Attributes["class"] = "btn btn-primary btn-sm";

            this.hddIdEmpleado.Value = lblBuscarEmpId.Text;
            BindDataMoviPlani();
        }

        protected void gvBuscarEmpleados_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            Label lblBuscarEmpId = (Label)this.gvListaEmpSeleccionados.Rows[e.RowIndex].FindControl("lblBuscarEmpId");

            for (int i = tableEmpSeleccionados.Rows.Count - 1; i >= 0; i--)
            {
                DataRow dr = tableEmpSeleccionados.Rows[i];
                if (dr["EmpId"].ToString() == lblBuscarEmpId.Text)
                    dr.Delete();
            }
            this.hddIdEmpleado.Value = "";
            cargarGridEmpSeleccionados();

        }


        public Boolean getValidarSesion()
        {
            //Response.Write("<script language='javascript'> alert('Prueba: " + Request.Cookies["EmprId"].Value + "');</" + "script>");

            if (!configSession.validarSession(getUsuNivelAcceso(), "IngresoPlanillas"))
            {
                Response.Write("<script language='javascript'> window.location.replace('PaginaOut.aspx');</" + "script>");
                Response.End();
                return false;
            }
            else
            {
                return true;
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


        public void cargarDatos() {
            cargarProveedor();
            cargarTareas();
            cargarFrentes();
            cargarCentroCostosEmpr();
            cargarAccUsuCuadrillas();
            cargarEmpleados();
        }

        public void cargarAccUsuCuadrillas() {
            tablaAccUsuCuadrillas = client.AccUsuCuadrillasGet(null, getEmprId(), getUsuId(), null);
        }

        public void cargarEmpleados() {
            tablaEmpleados = client.EmpleadoGet(null, null, null, null, "", getEmprId(), "a");
        }

        public void cargarProveedor() {
            tablaProveedores = client.ProveedoresUsuariosGet("", null, null, getUsuId(), getEmprId());
            DataView view = new DataView(tablaProveedores);
            distinctValues = view.ToTable(true, "gpId", "gpNombre");
        }
        public void cargarTareas() {
            tablaTareas = client.TareaGet(null, "", 1, getEmprId());
        }
        public void cargarFrentes() {
            tablaFrentes = client.FrenteGet(null, getEmprId(), 1, "");
        }
        public void cargarCentroCostosEmpr() {
            tablaCentroCostosEmpr = client.PLwebCentroCostosEmprGet(null, getEmprId());
        }

        protected void BindData()
        {
            tablaEmpleados = new Cabana.Campo.RAAgricola.Pre.Web.localhostWs.DS_ILC_Campo.PLwebEmpleadoDataTable();
            tablaProveedores = new Cabana.Campo.RAAgricola.Pre.Web.localhostWs.DS_ILC_Campo.VPLwebLotesProvDataTable();
            tablaTareas = new Cabana.Campo.RAAgricola.Pre.Web.localhostWs.DS_ILC_Campo.PLwebTareasDataTable();
            tablaFrentes = new Cabana.Campo.RAAgricola.Pre.Web.localhostWs.DS_ILC_Campo.PLwebFrentesDataTable();
            tablaCentroCostosEmpr = new Cabana.Campo.RAAgricola.Pre.Web.localhostWs.DS_ILC_Campo.PLwebCentroCostosEmprDataTable();
            tablaAccUsuCuadrillas = new Cabana.Campo.RAAgricola.Pre.Web.localhostWs.DS_ILC_Campo.PLwebAccUsuCuadrillasDataTable();


            cargarDatos();

            this.gvProveedores.DataSource = distinctValues;
            this.gvProveedores.DataBind();

            this.gvTareas.DataSource = tablaTareas;
            this.gvTareas.DataBind();
            
     // Hace el enlace al DataTable contenido en el DataSet
            this.ddlistFrente.DataSource = tablaFrentes;
            this.ddlistFrente.DataValueField = "FrenteId";
            this.ddlistFrente.DataTextField = "FrenteNombre";
            this.ddlistFrente.DataBind();
            this.ddlistFrente.Items.Insert(0, new ListItem("Seleccionar Frente", "0"));

            
            // Hace el enlace al DataTable contenido en el DataSet
            this.ddlistCentroCosto.DataSource = tablaCentroCostosEmpr;
            this.ddlistCentroCosto.DataValueField = "CcosId";
            this.ddlistCentroCosto.DataTextField  = "CcDescripcion";
            this.ddlistCentroCosto.DataBind();
            this.ddlistCentroCosto.Items.Insert(0, new ListItem("Seleccionar", "0"));

            this.ddlistCuadrilla.DataSource = tablaAccUsuCuadrillas;
            this.ddlistCuadrilla.DataValueField = "CuaId";
            this.ddlistCuadrilla.DataTextField = "CuaNombre";
            this.ddlistCuadrilla.DataBind();
            if (tablaAccUsuCuadrillas.Count == 0)
            {
                this.ddlistCuadrilla.Items.Insert(0, new ListItem("Seleccionar Cuadrilla", "0"));
                Response.Write("<script language='javascript'> alert('No tienenes cuadrillas asignadas');</" + "script>");

            }


            dataTableEmpSeleccionados();
            //cargarTableEmpSeleccionados("123", "Juan Jose Reyes");
            WsGetDataEmpCua();

            Cuadrillas_Change();
        }

        protected void BindDataTareas()//prueba
        {
            tablaCentroCostosEmpr = client.PLwebCentroCostosEmprGet(null, getEmprId());

            this.gvTareas.DataSource = tablaCentroCostosEmpr;
            //(this.gvTareas.DataSource as DataTable).DefaultView.RowFilter = " CcEmprId = " + this.ddl_Empresas.SelectedValue + " ";
            this.gvTareas.DataBind();
        }

        protected void WsGetDataEmpCua()
        {
            tablaEmpCua = client.PLwebEmpCuaEmpGet(getUsuId(), getEmprId());
        }

        public void cargarGridEmpleados(String textoBuscar)
        {

            WsGetDataEmpCua();
            this.gvEmpleados.DataSource = tablaEmpCua;
            (this.gvEmpleados.DataSource as DataTable).DefaultView.RowFilter = "CuaId = " + this.ddlistCuadrilla.SelectedValue + "  AND ( CONVERT ( EmpId , System.String ) LIKE '" + textoBuscar + "' OR EmpDUI LIKE '%" + textoBuscar + "%' OR nombreCompleto LIKE '%" + textoBuscar + "%' ) "; //
            this.gvEmpleados.DataBind();

            cargarGridEmpSeleccionados();
        }

        public void cargarGridEmpSeleccionados()
        {
            DataView view = new DataView(tableEmpSeleccionados);
            distinctValues = view.ToTable(true, "EmpId", "nombreCompleto");
            this.gvListaEmpSeleccionados.DataSource = distinctValues;
            this.gvListaEmpSeleccionados.DataBind();
        }

        protected void lbtnBuscarEmpleado_Click(object sender, EventArgs e)
        {

                String textoBuscar = this.txtControlBuscarEmpleado.Text;

                cargarGridEmpleados(textoBuscar);

            this.txtControlBuscarEmpleado.Focus();
            this.gvEmpleados.DataBind();

        }

        protected void lbtnLimpiarProvFincaLote_Click(object sender, EventArgs e)
        {

            limpiarProvFincaLote();

        }

        protected void BindDataMoviPlani()
        {
            try
            {
                int? IdEmpleado = null ;
                if (this.hddIdEmpleado.Value.Equals("")) { }//IdEmpleado = 0;
                else
                    IdEmpleado = Convert.ToInt32(this.hddIdEmpleado.Value);

                tablaMoviPlanilla = new Cabana.Campo.RAAgricola.Pre.Web.localhostWs.DS_ILC_Campo.PLwebMoviPlaniDataTable();
                DateTime fechaQuincena = DateTime.ParseExact(this.ddl_listadoFechas.SelectedItem.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture).Date;

                /*
                if (IdEmpleado > 0)
                {
                    tablaMoviPlanilla = client.MoviPlaniGet(null, null, getEmprId(), null, null, 1, IdEmpleado, "", null, fechaQuincena, "" + this.ddlistCuadrilla.SelectedValue,0);//CcosId
                }
                else
                {
                    tablaMoviPlanilla = client.MoviPlaniGet(null, null, getEmprId(), null, null, 1, null, "", null, fechaQuincena, "" + this.ddlistCuadrilla.SelectedValue,0);//CcosId
                    object sumObjectAll;
                    sumObjectAll = tablaMoviPlanilla.Compute("Sum(MovTotal)", "");
                    this.lblTotalDiaAll.Text = "Total Dia: $ " + sumObjectAll.ToString();
                }
                */

                tablaMoviPlanilla = client.MoviPlaniGet(null, null, getEmprId(), null, null, 1, IdEmpleado, "", null, fechaQuincena, "" + this.ddlistCuadrilla.SelectedValue, 0);//CcosId
                object sumObjectAll;
                sumObjectAll = tablaMoviPlanilla.Compute("Sum(MovTotal)", "");
                this.lblTotalDiaAll.Text = "Total Dia: $ " + sumObjectAll.ToString();

                gvMoviPlanilla.DataSource = tablaMoviPlanilla;
                gvMoviPlanilla.DataBind();


                object sumObject;
                sumObject = (this.gvMoviPlanilla.DataSource as DataTable).Compute("Sum(MovTotal)", "");
                this.lblTotalDia.Text = "Total Dia Empleado: $ " + sumObject.ToString();

            }
            catch (System.FormatException formato)
            {
                //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('A8080 Debe de seleccionar un empleado  para iniciar una busqueda')", true);

            }
        }

        protected void BindDataQuincenas()
        {
            client = new Cabana.Campo.RAAgricola.Pre.Web.localhostWs.WSRAAgricola();
            tablaQuincenas = new Cabana.Campo.RAAgricola.Pre.Web.localhostWs.DS_ILC_Campo.PLwebQuincenasDataTable();
            tablaQuincenas = client.QuincenaGet(QuinIdGet, getEmprId(), 1,"");

            int QuinId;
            //String QuinDechaDesde = "28/01/2016 12:00:00 a. m.";
            //String QuinDechaHasta = "10/02/2016 12:00:00 a. m.";
            String QuinDechaDesde = "";
            String QuinDechaHasta = "";

            if (tablaQuincenas.Rows.Count > 0)
            {
                QuinId = Int32.Parse(tablaQuincenas.Rows[0]["QuinId"].ToString());
                QuinDechaDesde = tablaQuincenas.Rows[0]["QuinFechaDesde"].ToString();
                QuinDechaHasta = tablaQuincenas.Rows[0]["QuinFechaHasta"].ToString();
                zafraQuincena = tablaQuincenas.Rows[0]["QuinZAfra"].ToString();
                QuinDechaDesde = String.Format("{0:d}", QuinDechaDesde);
                //this.lblRangoQuincena.Text = "" + QuinDechaDesde + " - " + DateTime.ParseExact(QuinDechaHasta, "dd/MM/yyyy hh:mm:ss tt", System.Globalization.CultureInfo.CurrentCulture).Date;


                List<DateTime> dates = new List<DateTime>();


                for (var dt = DateTime.ParseExact(QuinDechaDesde, "dd/MM/yyyy hh:mm:ss tt", System.Globalization.CultureInfo.CurrentCulture).Date;
                    dt <= DateTime.ParseExact(QuinDechaHasta, "dd/MM/yyyy hh:mm:ss tt", System.Globalization.CultureInfo.CurrentCulture).Date; 
                    dt = dt.AddDays(1))
                {
                    dates.Add(dt);

                }
                List<String> stringddd = dates.Select(date => String.Format("{0:d}", date)).ToList();
                this.ddl_listadoFechas.DataSource = stringddd; //String.Format("{0:MM/dd/yy}", dates);
                this.ddl_listadoFechas.DataBind();
                this.ddl_listadoFechas.Items.Insert(0, new ListItem("Seleccionar", "0"));


                this.ddlFrmZafra.DataBind();
                this.ddlFrmZafra.Items.Insert(0, new ListItem(zafraQuincena, zafraQuincena));
                this.ddlFrmZafra.Items.Insert(1, new ListItem("2014-2015", "2014-2015"));
                this.ddlFrmZafra.Items.Insert(2, new ListItem("2015-2016", "2015-2016"));
                this.ddlFrmZafra.Items.Insert(3, new ListItem("2016-2017", "2016-2017"));
                this.ddlFrmZafra.Items.Insert(4, new ListItem("2017-2018", "2017-2018"));
                this.ddlFrmZafra.Items.Insert(5, new ListItem("2018-2019", "2018-2019"));
            }

        }

        protected void limpiarProvFincaLote()
        {
            this.hddCodProveedor.Value      = "";
            this.lblCodNomProveedor.Text    = "";
            this.lblCodNomFinca.Text        = "";
            this.lblCodNomLote.Text         = "";
            this.lblCodNomLote.BorderColor  = System.Drawing.Color.LightGray;
            this.hddIdLote.Value            = "";
        }


        protected void gvEmployeeDetails_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            if (e.CommandName.Equals("ADD"))
            {

            }
            else if (e.CommandName.Equals("lbtnSelectRow"))
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = gvEmpleados.Rows[index];

                Label lblEmpNombres = (Label)row.FindControl("lblEmpNombres");
                Label lblEmpId = (Label)row.FindControl("lblEmpId");
                CheckBox chkSelect = (CheckBox)row.FindControl("chkSelect");
                LinkButton lbtnSelect = (LinkButton)row.FindControl("lbtnSelect");

                clicRowEmpleado(chkSelect, lbtnSelect, lblEmpNombres.Text, lblEmpId);

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Cerrar Modal", " cerrarMyModal(); ", true);
            }
            else if (e.CommandName.Equals("SELECTBYID"))
            {
                int index           = Convert.ToInt32(e.CommandArgument);
                GridViewRow row     = gvEmpleados.Rows[index];
                Label lblEmpNombres = (Label)row.FindControl("lblEmpNombres");
                Label lblEmpId      = (Label)row.FindControl("lblEmpId");
                CheckBox chkSelect  = (CheckBox)row.FindControl("chkSelect");
                LinkButton lbtnSelect = (LinkButton)row.FindControl("lbtnSelect");

                cargarTableEmpSeleccionados(lblEmpId.Text, lblEmpNombres.Text);

                clicRowEmpleado(chkSelect, lbtnSelect, lblEmpNombres.Text, lblEmpId);

            }
            else if (e.CommandName.Equals("SelectByCodigoProve"))
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = this.gvProveedores.Rows[index];
                Label lblcodProv = (Label)row.FindControl("lblcodProv");
                Label lblnomProv = (Label)row.FindControl("lblnomProv");
                cargarProveedor();
                //tablaFincasLotes = client.FincasLotesGet(EmprId, "", "", lblcodProv.Text, "LOTES", null, "");
                this.gvFincasLotes.DataSource = tablaProveedores;//tablaFincasLotes;
                (this.gvFincasLotes.DataSource as DataTable).DefaultView.RowFilter = " gpId = '" + lblcodProv.Text + "'  "; //string.Format("TareaId = '{0}'", "2");

                this.gvFincasLotes.DataBind();
                this.hddCodProveedor.Value = lblcodProv.Text;
                this.lblCodNomProveedor.Text = lblcodProv.Text + " - " + lblnomProv.Text;
                this.lblCodNomFinca.Text = "";
                this.lblCodNomLote.Text = "";
                this.hddIdLote.Value = "";

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Mostrar Modal", " tituloModal('" + lblcodProv.Text + " - " + lblnomProv.Text + "'); modalLotesProv(); ", true);
            }
            else if (e.CommandName.Equals("SelectByIdLote"))
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = this.gvFincasLotes.Rows[index];
                Label lblnomFinca = (Label)row.FindControl("lblnomFinca");
                Label lblcodLote = (Label)row.FindControl("lblcodLote");
                Label lblnomLote = (Label)row.FindControl("lblnomLote");
                Label lblLoteId = (Label)row.FindControl("lblcodLote");
                HiddenField hddgv_idLote = (HiddenField)row.FindControl("hddgv_idLote");

                this.lblCodNomFinca.Text = lblnomFinca.Text;
                this.lblCodNomLote.Text = lblcodLote.Text + " - " + lblnomLote.Text;
                this.lblCodNomLote.BorderColor = System.Drawing.Color.LightGray;
                this.hddIdLote.Value = lblLoteId.Text;

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Mostrar Modal", "cerrarMyModal(); ", true);


            }
            else if (e.CommandName.Equals("SelectByIdAllFinca"))
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = this.gvFincasLotes.Rows[index];
                //Label lblcodFinca = (Label)row.FindControl("lblcodFinca");
                Label lblnomFinca = (Label)row.FindControl("lblnomFinca");
                Label lblcodLote = (Label)row.FindControl("lblcodLote");
                Label lblnomLote = (Label)row.FindControl("lblnomLote");
                Label lblLoteId = (Label)row.FindControl("lblcodLote");
                HiddenField hddgv_idLote = (HiddenField)row.FindControl("hddgv_idLote");

                this.lblCodNomFinca.Text = lblnomFinca.Text;
                this.lblCodNomLote.Text = "Varios";
                this.lblCodNomLote.BorderColor = System.Drawing.Color.Red;
                this.hddIdLote.Value = lblLoteId.Text;


                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Mostrar Modal", "cerrarMyModal(); ", true);


            }
            else if (e.CommandName.Equals("SelectByIdTareas"))
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = this.gvTareas.Rows[index];
                Label lblTareaId = (Label)row.FindControl("lblTareaId");
                Label lblTareaDesc = (Label)row.FindControl("lblTareaDesc");
                Label lblTareaTarifa = (Label)row.FindControl("lblTareaTarifa");

                this.lblTareaTarifaTotal.Text = lblTareaTarifa.Text;
                this.lblTareaDesc.Text = "$ " + lblTareaTarifa.Text + " / " + lblTareaDesc.Text +" - "+ lblTareaId.Text;
                this.hddTareaTarifaUnidad.Value = lblTareaTarifa.Text;
                this.hddTareaId.Value = lblTareaId.Text;
                this.txtTareasCantidad.Text = "1.00";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Mostrar Modal", "cerrarModalTareas(); ", true);
            }

        }

        private void clicRowEmpleado(CheckBox chkSelect, LinkButton lbtnSelect, String nombreCompleto, Label lblEmpId) 
        {
            try
            {
                this.hddIdEmpleado.Value = lblEmpId.Text;

                this.gvMoviPlanilla.DataSource = null;
                this.gvMoviPlanilla.DataBind();


                if (!this.ddl_listadoFechas.SelectedItem.Text.Equals("Seleccionar")){
                    BindDataMoviPlani();
                }
            }
            catch (Exception error) 
            { 
            }
        }

        public void CommandBtn_Click(Object sender, CommandEventArgs e)
        {

            switch (e.CommandName)
            {
                case "BuscarProv":

                    try
                    {
                        cargarProveedor();
                        String textoBuscar = this.HddTextoFiltroProv.Value;//this.txtControlBuscarProv.Text;
                        this.gvProveedores.DataSource = distinctValues;
                        (this.gvProveedores.DataSource as DataTable).DefaultView.RowFilter = " gpId LIKE '%" + textoBuscar + "%'  OR gpNombre LIKE '%" + textoBuscar + "%'"; //string.Format("TareaId = '{0}'", "2");

                        this.gvProveedores.DataBind();

                        this.txtControlBuscarProv.Text = textoBuscar;
                    }
                    catch (System.IO.IOException er)
                    {
                        Console.WriteLine("Filtro BuscarEmpleado", er);
                    }
                    break;

                case "BuscarEmpleado":

                    try
                    {

                        String texto = this.hddTextoFiltroEmp.Value;
                        cargarGridEmpleados(texto);
                        
                        /*this.gvEmpleados.DataSource = tablaEmpleados;
                        int idCuadrilla = Convert.ToInt32(this.ddlistCuadrilla.SelectedValue);
                        (this.gvEmpleados.DataSource as DataTable).DefaultView.RowFilter = " EmpCuadrilla = " + idCuadrilla + " AND (EmpNombres LIKE '%" + texto + "%' OR EmpApellidos LIKE '%" + texto + "%' OR TipoEmpDesc LIKE '%" + texto + "%' OR EmpDUI LIKE '%" + texto + "%' ) "; 
                        this.gvEmpleados.DataBind();
                        */

                        this.txtControlBuscarEmpleado.Text = texto;
                    }
                    catch (System.IO.IOException er)
                    {
                        Console.WriteLine("Filtro BuscarEmpleado", er);
                    }
                    break;

                case "BuscarFincasLotes":

                    try
                    {
                        String texto = this.hddTextoFiltroLotes.Value;
                        cargarProveedor();
                        this.gvFincasLotes.DataSource = tablaProveedores;
                        (this.gvFincasLotes.DataSource as DataTable).DefaultView.RowFilter = " gpId = '" + this.hddCodProveedor.Value + "' AND ( LoteNombre LIKE '%" + texto + "%' OR FincaNombre LIKE '%" + texto + "%' ) ";
                        this.gvFincasLotes.DataBind();

                        this.txtControlFincasLotes.Text = texto;
                    }
                    catch (System.IO.IOException er)
                    {
                        Console.WriteLine("Filtro BuscarFincasLotes", er);
                    }
                    break;

                case "BuscarTareas":
                    try
                    {
                        String texto = this.hddTextoFiltroTareas.Value;
                        cargarTareas();
                        this.gvTareas.DataSource = tablaTareas;
                        (this.gvTareas.DataSource as DataTable).DefaultView.RowFilter = " TareaTipo LIKE '%" + texto + "%' OR TareaDesc LIKE '%" + texto + "%' OR CcNombre LIKE '%" + texto + "%' "; //string.Format("TareaId = '{0}'", "2");
                        this.gvTareas.DataBind();

                        this.txtBuscarTareas.Text = texto;
                    }
                    catch (System.IO.IOException er)
                    {
                        Console.WriteLine("Filtro BuscarTareas", er);
                    }
                    break;


                default:

                    break;

            }

        }


        protected void gvMoviPlanilla_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            Label lblEditMovId = (Label)gvMoviPlanilla.Rows[e.RowIndex].FindControl("lblEditMovId");
            TextBox txtEditMovCantidad = (TextBox)gvMoviPlanilla.Rows[e.RowIndex].FindControl("txtEditMovCantidad");

            try
            {

                int idRegistro = Int32.Parse(lblEditMovId.Text);

                Label lblUsuId = (Label)gvMoviPlanilla.Rows[e.RowIndex].FindControl("lblUsuId");
                if (lblUsuId.Text.Equals("" + getUsuId()) || getUsuNivelAcceso() == (ConfigSession.supervisorEmpr) || getUsuNivelAcceso() == (ConfigSession.gerenteEmpr))
                {
                    if (tablaMoviPlanilla.Rows.Count > 0)
                    {

                        DataRow rows = tablaMoviPlanilla.FindByMovId(idRegistro);

                        rows["MovCantidad"] = txtEditMovCantidad.Text;
                        rows["MovUsuarioModi"] = "" + getUsuId();

                        client.MoviPlaniSet(tablaMoviPlanilla);

                        gvMoviPlanilla.EditIndex = -1;


                        BindDataMoviPlani();
                    }
                }
                else {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('No estas autorizado a modificar este registro')", true);
                }
            }
            catch (System.IO.IOException er)
            {
                Console.WriteLine("Error en el insert", er);
            }
            catch (System.ArgumentException arg)// Argumentos incorrectos o nullos
            {

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('A1020 EL campo cantidad es requerido')", true);
            }
            catch (System.NullReferenceException argnull)// No se ha declarado o definido aun alguna variable o objeto en el sistema
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('A1010 Todos los campos son requeridos')", true);

            }


        }

        protected void gvMoviPlanilla_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            //try
            //{
                Label lblUsuId = (Label)gvMoviPlanilla.Rows[e.RowIndex].FindControl("lblUsuId");
                if (lblUsuId.Text.Equals("" + getUsuId()) || getUsuNivelAcceso() == (ConfigSession.supervisorEmpr) || getUsuNivelAcceso() == (ConfigSession.gerenteEmpr))
                {
                Label lblMovId = (Label)gvMoviPlanilla.Rows[e.RowIndex].FindControl("lblMovId");

                int idRegistro = Int32.Parse(lblMovId.Text);

                DataRow rows = tablaMoviPlanilla.FindByMovId(idRegistro);

                //rows.Delete(); //MovEstadoPlan
                rows["MovEstadoPlan"] = "0";
                rows["MovUsuarioModi"] = "" + getUsuId();
                client.MoviPlaniSet(tablaMoviPlanilla);

                BindDataMoviPlani();
                mesajeAccion = "Registro eliminado con exito";

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + mesajeAccion.ToString() + "')", true);
                BindDataMoviPlani();
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('No estas autorizado a eliminar este registro')", true);
                }

        }


        protected void gvMoviPlanilla_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvMoviPlanilla.EditIndex = -1;
            gvMoviPlanilla.DataSource = tablaMoviPlanilla;
            gvMoviPlanilla.DataBind();
        }
        protected void gvMoviPlanilla_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvMoviPlanilla.EditIndex = e.NewEditIndex;
            gvMoviPlanilla.DataSource = tablaMoviPlanilla;
            gvMoviPlanilla.DataBind();
        }


        protected void gvMoviPlanilla_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvMoviPlanilla.PageIndex = e.NewPageIndex;
            this.gvMoviPlanilla.DataSource = tablaMoviPlanilla;
            this.gvMoviPlanilla.EditIndex = -1;
            this.gvMoviPlanilla.DataBind();

        }

        protected void gvEmpleados_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvEmpleados.PageIndex = e.NewPageIndex;
            //gvEmpleados.DataSource = tabla;
            cargarGridEmpleados("");
            gvEmpleados.EditIndex = -1;
            this.gvEmpleados.DataBind();
        }

        protected void gvProveedores_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvProveedores.PageIndex = e.NewPageIndex;
            gvProveedores.DataSource = distinctValues;
            gvProveedores.EditIndex = -1;
            this.gvProveedores.DataBind();
        }

        protected void gvTareas_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvTareas.PageIndex = e.NewPageIndex;
            gvTareas.DataSource = tablaTareas;
            gvTareas.EditIndex = -1;
            this.gvTareas.DataBind();

            String texto = this.hddTextoFiltroTareas.Value;

            this.txtBuscarTareas.Text = texto;
        }

        protected void gvFincasLotes_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvFincasLotes.PageIndex = e.NewPageIndex;
            gvFincasLotes.DataSource = tablaProveedores;
            gvFincasLotes.EditIndex = -1;
            this.gvFincasLotes.DataBind();
        }

        protected void lbtnCodNomLote_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Mostrar Modal", " modalLotesProv(); ", true);

        }

        protected void lbtnProv_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Mostrar Modal", " modalProv(); ", true);

        }

        protected void lbtnEmpleados_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Mostrar Modal", " modalEmp(); ", true);

        }

        protected void lbtnTareas_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Mostrar Modal", " modalTareas(); ", true);

        }

        protected void txtTareasCantidad_OnTextChanged(object sender, EventArgs e)
        {
            try
            {
                if (this.hddTareaTarifaUnidad.Value != "" && this.txtTareasCantidad.Text != "")
                {
                    double TareasCantidad = Convert.ToDouble(this.txtTareasCantidad.Text.Replace('_', '0'));
                    double TareaTarifaUnidad = Convert.ToDouble(this.hddTareaTarifaUnidad.Value);

                    double Total = TareasCantidad * TareaTarifaUnidad;

                    if (TareasCantidad > 0 && TareaTarifaUnidad > 0)
                    {
                        this.txtTareasCantidad.Text = this.txtTareasCantidad.Text.Replace('_', '0');
                        this.lblTareaTarifaTotal.Text = String.Format("{0:0.00}", Total);
                    }
                }
                else
                {

                    this.txtTareasCantidad.Text = "";
                }
            }
            catch (Exception error) {
                if (error.ToString().Contains("FormatException"))
                    mesajeAccion = "Revise los campos, esta ingresando un valor no valido.";
                else
                    mesajeAccion = "Informar de este error a soporte tecnico";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + mesajeAccion.ToString() + "')", true);

            }
                
            
        }

        protected void lbtn_FrmEmpNuevo_Click(object sender, EventArgs e)
        {
            DataRow row = tablaEmpleados.NewRow();

        }

        protected void ddlistFrentes_Change(object sender, EventArgs e)
        {
            try
            {
                //stuff that never gets hit
                int idFrente = Convert.ToInt32(this.ddlistFrente.SelectedValue);

                cargarAccUsuCuadrillas();
                Cabana.Campo.RAAgricola.Pre.Web.localhostWs.DS_ILC_Campo.PLwebAccUsuCuadrillasDataTable tablaCuadrillas_modificado;
                tablaCuadrillas_modificado = tablaAccUsuCuadrillas;
                tablaCuadrillas_modificado.DefaultView.RowFilter = " FrenteId = " + idFrente + " ";


                this.ddlistCuadrilla.DataSource = tablaCuadrillas_modificado;
                this.ddlistCuadrilla.DataValueField = "CuaId";
                this.ddlistCuadrilla.DataTextField = "CuaNombre";
                this.ddlistCuadrilla.DataBind();
                this.ddlistCuadrilla.Items.Insert(0, new ListItem("Seleccionar Cuadrilla", "0"));

                this.gvEmpleados.DataSource = null;
                this.gvEmpleados.DataBind();

                this.gvMoviPlanilla.DataSource = null;
                this.gvMoviPlanilla.DataBind();


                this.hddIdEmpleado.Value = "";
            }
            catch (System.Web.Services.Protocols.SoapException i)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('A4010 Se perdio la comunicacion con el servidor intente nuevamente')", true);

            }

        }

        protected void ddlistCuadrillas_Change(object sender, EventArgs e)
        {
            dataTableEmpSeleccionados();
            cargarGridEmpSeleccionados();
            Cuadrillas_Change();
            BindDataMoviPlani();
        }

        protected void Cuadrillas_Change() 
        {

            int idCuadrilla = Convert.ToInt32(this.ddlistCuadrilla.SelectedValue);
            hddCuadrillaId.Value = "" + idCuadrilla;

            cargarGridEmpleados("");

            this.gvMoviPlanilla.DataSource = null;
            this.gvMoviPlanilla.DataBind();

            this.hddIdEmpleado.Value = "";
        }

        protected void lbtnBuscarProv_Click(object sender, EventArgs e)
        {
            String textoBuscar = this.txtControlBuscarProv.Text;
            cargarProveedor();
            this.gvProveedores.DataSource = distinctValues;
            (this.gvProveedores.DataSource as DataTable).DefaultView.RowFilter = " gpId LIKE '%" + textoBuscar + "%'  OR gpNombre LIKE '%" + textoBuscar + "%'"; //string.Format("TareaId = '{0}'", "2");

            this.gvProveedores.DataBind();
        }

        protected void btn_AddNewMoviPlanilla_Click(object sender, EventArgs e)
        {

            try
            {
                int contadorChkFalse = 0;


                foreach (GridViewRow rows in gvListaEmpSeleccionados.Rows)
                {
                    Label lblEmpId = (Label)rows.FindControl("lblBuscarEmpId");


                        DataRow row = tablaMoviPlanilla.NewRow();

                        int ingresoAll = 0;
                        if (this.lblCodNomLote.BorderColor.Equals(System.Drawing.Color.Red))
                        {
                            ingresoAll = 1;
                            //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Alerta", "alert('color rojo confirmado'); ", true);
                        }
                    String idLote = this.hddIdLote.Value;
                    if (idLote.Equals("")) {
                        idLote = "0";
                    }
                    if (idLote.Equals("0") && this.ddlistCentroCosto.SelectedItem.Value.Equals("0")) {
                        idLote = "";
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Se debe seleccionar un centro de costos o una propiedad')", true);

                    }

                        row["EmpId"]            = lblEmpId.Text;
                        row["TareaId"]          = this.hddTareaId.Value;
                        row["MovTarifa"]        = this.hddTareaTarifaUnidad.Value;
                        row["MovCantidad"]      = this.txtTareasCantidad.Text;
                        row["MovUsuarioIngresa"]= "" + getUsuId();
                        row["QuincenaId"]       = QuinIdGet;
                        row["LoteId"]           = idLote;
                        row["EmprId"]           = "" + getEmprId();
                        row["MovFecha"]         = this.ddl_listadoFechas.SelectedItem.Text;
                        row["MovZafra"]         = this.ddlFrmZafra.SelectedItem.Text;
                        row["MovTodaLaFinca"]   = ingresoAll;
                        row["CcosId"]           = this.ddlistCentroCosto.SelectedItem.Value;
                        row["CuaId"]            = this.ddlistCuadrilla.SelectedItem.Value;

                    tablaMoviPlanilla.Rows.Add(row);
  
                        contadorChkFalse= contadorChkFalse+1;
                    //}
                    
                }
                if (contadorChkFalse <= 0)
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('C1010 Debe de seleccionar al menos un empleado')", true);
                }
                else {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Se ingresaron "+contadorChkFalse+" activiades')", true);
              
                }

                client.MoviPlaniSet(tablaMoviPlanilla);

                gvMoviPlanilla.EditIndex = -1;

                BindDataMoviPlani();
                
            }
            catch (System.IO.IOException er)
            {
                Console.WriteLine("Error en el insert", er);
            }
            catch (System.ArgumentException arg)// Argumentos incorrectos o nullos
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('A1020 Todos los campos son requeridos')", true);
            }
            catch (System.NullReferenceException argnull)// No se ha declarado o definido aun alguna variable o objeto en el sistema
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('A1010 Todos los campos son requeridos')", true);

            }
        }

        protected void ddl_listadoFechas_Change(object sender, EventArgs e)
        {

            BindDataMoviPlani();
        }

        protected void OnSelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (GridViewRow row in gvEmpleados.Rows)
            {
                if (row.RowIndex == gvEmpleados.SelectedIndex)
                {
                    row.BackColor = ColorTranslator.FromHtml("#A1DCF2");
                    row.ToolTip = string.Empty;
                }
                else
                {
                    row.BackColor = ColorTranslator.FromHtml("#FFFFFF");
                    row.ToolTip = "Click to select this row.";
                }
            }
        }



        protected void OnRowDataBound(object sender, System.Web.UI.WebControls.GridViewRowEventArgs e)
        {   

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(gvEmpleados, "Select$" + e.Row.RowIndex);
                e.Row.ToolTip = "Click to select this row.";
            }

            try
            {
                switch (e.Row.RowType)
                {
                    case DataControlRowType.Header:
                        //...
                        break;
                    case DataControlRowType.DataRow:
                        e.Row.Attributes.Add("onmouseover", "this.style.backgroundColor='#93A3B0'; this.style.color='White'; this.style.cursor='pointer'");
                        if (e.Row.RowState == DataControlRowState.Alternate)
                        {
                            e.Row.Attributes.Add("onmouseout", String.Format("this.style.color='Black';this.style.backgroundColor='#FFFFFF';", gvEmpleados.AlternatingRowStyle.BackColor.ToKnownColor()));
                        }
                        else
                        {
                            e.Row.Attributes.Add("onmouseout", String.Format("this.style.color='Black';this.style.backgroundColor='#FFFFFF';", gvEmpleados.RowStyle.BackColor.ToKnownColor()));
                        }
                        break;
                }
            }
            catch
            {
                //...throw
            }
        }

    }
}