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
    public partial class Descuentos : System.Web.UI.Page
    {

        public static Cabana.Campo.RAAgricola.Pre.Web.localhostWs.DS_ILC_Campo.PLwebQuincenasDataTable tablaQuincenas;
        public static Cabana.Campo.RAAgricola.Pre.Web.localhostWs.DS_ILC_Campo.PLwebEmpleadoDataTable tablaEmpleados;
        public static Cabana.Campo.RAAgricola.Pre.Web.localhostWs.DS_ILC_Campo.PLwebFrentesDataTable tablaFrentes;
        public static Cabana.Campo.RAAgricola.Pre.Web.localhostWs.DS_ILC_Campo.PLwebCuadrillasDataTable tablaCuadrillas;
        public static Cabana.Campo.RAAgricola.Pre.Web.localhostWs.DS_ILC_Campo.PLwebDescuentosEmpDataTable tablaDescuentosEmp;
        public static Cabana.Campo.RAAgricola.Pre.Web.localhostWs.DS_ILC_Campo.PLwebAccUsuCuadrillasDataTable tablaAccUsuCuadrillas;

        public static Cabana.Campo.RAAgricola.Pre.Web.localhostWs.WSRAAgricola client;
        ConfigSession configSession = new ConfigSession();
        //public static int EmprId = 0;
        //public static int UsuId;
        public static String mesajeAccion;
        
        public static DataTable distinctValues;

        String zafraQuincena = "";
        Double sumaTotalDia = 0.0;
        int QuinIdGet;

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!string.IsNullOrEmpty(Session["UsuId"] as string))
            {
                if (this.HddEmprId.Value == "")
                {
                    this.HddUsuId.Value = "" + Session["UsuId"];
                    this.HddEmprId.Value = "" + Session["EmprId"];
                    this.HddUsuNivelAcceso.Value = "" + Session["UsuNivelAcceso"]; // Int32.Parse(
                }

                if (!configSession.validarSession(getUsuNivelAcceso(), "Descuentos"))//NumeroDeAcceso, NombrePagina
                {
                    Response.Write("<script language='javascript'> window.location.replace('PaginaOut.aspx');</" + "script>");
                    Response.End();
                }
                else
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

        public int getUsuId()
        {
            return Int32.Parse("" + Session["UsuId"]);
        }
        public int getEmprId()
        {
            return Int32.Parse("" + Session["EmprId"]);
        }

        public int getUsuNivelAcceso() {
            return Int32.Parse("" + Session["UsuNivelAcceso"]);
        }


        protected void BindData()
        {

            tablaEmpleados = new Cabana.Campo.RAAgricola.Pre.Web.localhostWs.DS_ILC_Campo.PLwebEmpleadoDataTable();

            tablaFrentes = new Cabana.Campo.RAAgricola.Pre.Web.localhostWs.DS_ILC_Campo.PLwebFrentesDataTable();
            tablaFrentes = client.FrenteGet(null, getEmprId(), 1, "");
            // Hace el enlace al DataTable contenido en el DataSet
            this.ddlistFrente.DataSource = tablaFrentes;
            this.ddlistFrente.DataValueField = "FrenteId";
            this.ddlistFrente.DataTextField = "FrenteNombre";
            this.ddlistFrente.DataBind();
            this.ddlistFrente.Items.Insert(0, new ListItem("Seleccionar Frente", "0"));

            tablaAccUsuCuadrillas = new Cabana.Campo.RAAgricola.Pre.Web.localhostWs.DS_ILC_Campo.PLwebAccUsuCuadrillasDataTable();
            tablaAccUsuCuadrillas = client.AccUsuCuadrillasGet(null, getEmprId(), getUsuId(), null);
            this.ddlistCuadrilla.DataSource = tablaAccUsuCuadrillas;
            this.ddlistCuadrilla.DataValueField = "CuaId";
            this.ddlistCuadrilla.DataTextField = "CuaNombre";
            this.ddlistCuadrilla.DataBind();
            if (tablaAccUsuCuadrillas.Count == 0)
            {
                this.ddlistCuadrilla.Items.Insert(0, new ListItem("Seleccionar Cuadrilla", "0"));
                Response.Write("<script language='javascript'> alert('No tienenes cuadrillas asignadas');</" + "script>");

            }

            tablaEmpleados = client.EmpleadoGet(null, null, null, null, "", getEmprId(), "a");

            Cuadrillas_Change();
            BindDataMoviPlani();

        }

        protected void BindDataMoviPlani()
        {
            try
            {

                tablaDescuentosEmp = new Cabana.Campo.RAAgricola.Pre.Web.localhostWs.DS_ILC_Campo.PLwebDescuentosEmpDataTable();

                tablaDescuentosEmp = client.PLwebDescuentosEmpGet(null, null, QuinIdGet, null, Int32.Parse(this.ddlistCuadrilla.SelectedValue));
                object sumObjectAll;
                sumObjectAll = tablaDescuentosEmp.Compute("Sum(DescCantidad)", "");
                this.lblTotalDiaAll.Text = "Total Descuentos Cuadrilla: $ " + sumObjectAll.ToString();

                gvMoviPlanilla.DataSource = tablaDescuentosEmp;
                gvMoviPlanilla.DataBind();


                
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
            String QuinDechaDesde = "";
            String QuinDechaHasta = "";

            if (tablaQuincenas.Rows.Count > 0)
            {
                QuinId = Int32.Parse(tablaQuincenas.Rows[0]["QuinId"].ToString());
                QuinDechaDesde = tablaQuincenas.Rows[0]["QuinFechaDesde"].ToString();
                QuinDechaHasta = tablaQuincenas.Rows[0]["QuinFechaHasta"].ToString();
                zafraQuincena = tablaQuincenas.Rows[0]["QuinZAfra"].ToString();
                QuinDechaDesde = String.Format("{0:d}", QuinDechaDesde);//""+DateTime.ParseExact(QuinDechaDesde, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture).Date;
                this.lblRangoQuincena.Text = "" + QuinDechaDesde + " - " + DateTime.ParseExact(QuinDechaHasta, "dd/MM/yyyy hh:mm:ss tt", System.Globalization.CultureInfo.CurrentCulture).Date;


                List<DateTime> dates = new List<DateTime>();


                for (var dt = DateTime.ParseExact(QuinDechaDesde, "dd/MM/yyyy hh:mm:ss tt", System.Globalization.CultureInfo.CurrentCulture).Date;
                    dt <= DateTime.ParseExact(QuinDechaHasta, "dd/MM/yyyy hh:mm:ss tt", System.Globalization.CultureInfo.CurrentCulture).Date; 
                    dt = dt.AddDays(1))
                {
                    dates.Add(dt);

                }
                List<String> stringddd = dates.Select(date => String.Format("{0:d}", date)).ToList();

            }

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
                gvEmpleados.DataSource = tablaEmpleados;
                this.gvEmpleados.DataBind();
                Label lblEmpNombres = (Label)row.FindControl("lblEmpNombres");
                Label lblEmpApe = (Label)row.FindControl("lblEmpApe");
                Label lblEmpId = (Label)row.FindControl("lblEmpId");
                CheckBox chkSelect = (CheckBox)row.FindControl("chkSelect");
                LinkButton lbtnSelect = (LinkButton)row.FindControl("lbtnSelect");
                String nombreCompleto = "" + lblEmpNombres.Text + " " + lblEmpApe.Text;

                clicRowEmpleado(chkSelect, lbtnSelect, nombreCompleto, lblEmpId);

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Cerrar Modal", " cerrarMyModal(); ", true);
            }
            else if (e.CommandName.Equals("SELECTBYID"))
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = gvEmpleados.Rows[index];
                Label lblEmpNombres = (Label)row.FindControl("lblEmpNombres");
                Label lblEmpApe = (Label)row.FindControl("lblEmpApe");
                Label lblEmpId = (Label)row.FindControl("lblEmpId");
                CheckBox chkSelect = (CheckBox)row.FindControl("chkSelect");
                LinkButton lbtnSelect = (LinkButton)row.FindControl("lbtnSelect");
                String nombreCompleto = "" + lblEmpNombres.Text + " " + lblEmpApe.Text;

                clicRowEmpleado(chkSelect, lbtnSelect, nombreCompleto, lblEmpId);

            }

        }

        private void clicRowEmpleado(CheckBox chkSelect, LinkButton lbtnSelect, String nombreCompleto, Label lblEmpId) 
        {
            BindDataMoviPlani();
            try
            {

                if (chkSelect.Checked == true)
                {
                    chkSelect.Checked = false;
                    this.hddIdEmpleado.Value = "";
                    this.lblNombreEmpleado.Text = "";
                    lbtnSelect.Attributes["class"] = "btn btn-outline btn-warning btn-sm";
                    gvMoviPlanilla.DataSource = tablaDescuentosEmp;
                    this.gvMoviPlanilla.DataBind();
                }
                else
                {
                    chkSelect.Checked = true;
                    this.hddIdEmpleado.Value = lblEmpId.Text;
                    this.lblNombreEmpleado.Text = nombreCompleto;
                    lbtnSelect.Attributes["class"] = "btn btn-primary btn-sm";
                    gvMoviPlanilla.DataSource = tablaDescuentosEmp;
                    (this.gvMoviPlanilla.DataSource as DataTable).DefaultView.RowFilter = " DescEmpId = " + lblEmpId.Text + " ";

                    //object sumObject;
                    //sumObject = (gvMoviPlanilla.DataSource as DataTable).Compute("Sum(DescCantidad)", "");
                    //this.lblTotalDia.Text = "Total Dia Empleado: $ " + sumObject.ToString();
                    this.gvMoviPlanilla.DataBind();
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

                case "BuscarEmpleado":

                    try
                    {

                        String texto = this.hddTextoFiltroEmp.Value;

                        this.gvEmpleados.DataSource = tablaEmpleados;
                        int idCuadrilla = Convert.ToInt32(this.ddlistCuadrilla.SelectedValue);
                        (this.gvEmpleados.DataSource as DataTable).DefaultView.RowFilter = " EmpCuadrilla = " + idCuadrilla + " AND (EmpNombres LIKE '%" + texto + "%' OR EmpApellidos LIKE '%" + texto + "%' OR TipoEmpDesc LIKE '%" + texto + "%' OR EmpDUI LIKE '%" + texto + "%' ) "; 
                        this.gvEmpleados.DataBind();

                        this.txtControlBuscarEmpleado.Text = texto;
                    }
                    catch (System.IO.IOException er)
                    {
                        Console.WriteLine("Filtro BuscarEmpleado", er);
                    }
                    break;

                default:

                    break;

            }

        }


        protected void gvMoviPlanilla_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            BindDataMoviPlani();
            Label lblEditDescId = (Label)gvMoviPlanilla.Rows[e.RowIndex].FindControl("lblEditDescId");
            TextBox txtEditDescCantidad = (TextBox)gvMoviPlanilla.Rows[e.RowIndex].FindControl("txtEditDescCantidad");

            try
            {

                int idRegistro = Int32.Parse(lblEditDescId.Text);

                Label lblUsuIdIngresa = (Label)gvMoviPlanilla.Rows[e.RowIndex].FindControl("lblUsuIdIngresa");
                if (lblUsuIdIngresa.Text.Equals("" + getUsuId()) || getUsuNivelAcceso() == (ConfigSession.supervisorEmpr) || getUsuNivelAcceso() == (ConfigSession.gerenteEmpr))
                {
                    if (tablaDescuentosEmp.Rows.Count > 0)
                    {

                        DataRow rows = tablaDescuentosEmp.FindByDescId(idRegistro);


                        rows["DescCantidad"] = txtEditDescCantidad.Text;

                        client.PLwebDescuentosEmpSet(tablaDescuentosEmp);

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
            try
            {
                BindDataMoviPlani();
                Label lblUsuIdIngresa = (Label)gvMoviPlanilla.Rows[e.RowIndex].FindControl("lblUsuIdIngresa");
                if (lblUsuIdIngresa.Text.Equals("" + getUsuId()) || getUsuNivelAcceso() == (ConfigSession.supervisorEmpr) || getUsuNivelAcceso() == (ConfigSession.gerenteEmpr))
                {
                    Label lblDescId = (Label)gvMoviPlanilla.Rows[e.RowIndex].FindControl("lblDescId");

                    int idRegistro = Int32.Parse(lblDescId.Text);

                DataRow rows = tablaDescuentosEmp.FindByDescId(idRegistro);

                //rows.Delete();

                rows["DescEstadoDelete"] = "1";

                client.PLwebDescuentosEmpSet(tablaDescuentosEmp);

                BindDataMoviPlani();
                mesajeAccion = "Registro eliminado con exito";

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + mesajeAccion + "')", true);
                BindDataMoviPlani();
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('No estas autorizado a eliminar este registro')", true);
                }

            }
            catch (Exception error)
            {
                if (error.ToString().Contains("REFERENCE"))
                    mesajeAccion = "Este registro ya tiene datos vinculados.";
                else
                    mesajeAccion = "Informar de este error a soporte tecnico";

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + mesajeAccion + "')", true);
                BindDataMoviPlani();
            }


        }


        protected void gvMoviPlanilla_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            BindDataMoviPlani();
            gvMoviPlanilla.EditIndex = -1;
            gvMoviPlanilla.DataSource = tablaDescuentosEmp;
            gvMoviPlanilla.DataBind();
        }
        protected void gvMoviPlanilla_RowEditing(object sender, GridViewEditEventArgs e)
        {
            BindDataMoviPlani();
            gvMoviPlanilla.EditIndex = e.NewEditIndex;
            gvMoviPlanilla.DataSource = tablaDescuentosEmp;
            gvMoviPlanilla.DataBind();
        }


        protected void gvMoviPlanilla_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            BindDataMoviPlani();
            this.gvMoviPlanilla.PageIndex = e.NewPageIndex;
            this.gvMoviPlanilla.DataSource = tablaDescuentosEmp;
            this.gvMoviPlanilla.EditIndex = -1;
            this.gvMoviPlanilla.DataBind();

        }

        protected void gvEmpleados_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvEmpleados.PageIndex = e.NewPageIndex;
            gvEmpleados.DataSource = tablaEmpleados;
            gvEmpleados.EditIndex = -1;
            this.gvEmpleados.DataBind();
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
                if (this.txtTareasCantidad.Text != "")
                {
                    double TareasCantidad = Convert.ToDouble(this.txtTareasCantidad.Text.Replace('_', '0'));

                    double Total = TareasCantidad ;

                    if (TareasCantidad > 0 )
                    {
                        //this.txtTareasCantidad.Text = this.txtTareasCantidad.Text.Replace('_', '0');
                        this.txtTareasCantidad.Text = String.Format("{0:0.00}", Total);
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
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + mesajeAccion + "')", true);

            }
                
            
        }

        protected void ddlistFrentes_Change(object sender, EventArgs e)
        {
            try
            {
                //stuff that never gets hit
                int idFrente = Convert.ToInt32(this.ddlistFrente.SelectedValue);


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
                this.lblNombreEmpleado.Text = "";
            }
            catch (System.Web.Services.Protocols.SoapException i)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('A4010 Se perdio la comunicacion con el servidor intente nuevamente')", true);

            }

        }

        protected void ddlistCuadrillas_Change(object sender, EventArgs e)
        {
            Cuadrillas_Change();
            BindDataMoviPlani();
        }

        protected void Cuadrillas_Change() 
        {
            int idCuadrilla = Convert.ToInt32(this.ddlistCuadrilla.SelectedValue);
            hddCuadrillaId.Value = "" + idCuadrilla;

            this.gvEmpleados.DataSource = tablaEmpleados;
            (this.gvEmpleados.DataSource as DataTable).DefaultView.RowFilter = " EmpCuadrilla = " + idCuadrilla + " ";

            this.gvEmpleados.DataBind();

            this.gvMoviPlanilla.DataSource = null;
            this.gvMoviPlanilla.DataBind();

            this.hddIdEmpleado.Value = "";
            this.lblNombreEmpleado.Text = "";        
        }

        protected void btn_AddNewMoviPlanilla_Click(object sender, EventArgs e)
        {

            try
            {
                BindDataMoviPlani();
                int contadorChkFalse = 0;


                foreach (GridViewRow rows in gvEmpleados.Rows)
                {
                    CheckBox chk = (CheckBox)rows.FindControl("chkSelect");
                    Label lblEmpId = (Label)rows.FindControl("lblEmpId");
                    if (chk.Checked)
                    {

                        DataRow row = tablaDescuentosEmp.NewRow();

                        row["DescEmpId"]        = lblEmpId.Text;
                        row["DescEmprId"]       = "" + getEmprId();
                        row["DescUsuIdIngresa"] = "" + getUsuId();
                        row["DescCantidad"]     = this.txtTareasCantidad.Text;
                        row["DescRazon"]        = this.txtDescRazon.Text;//"Registro de Ejemplo";
                        row["DescQuinId"]       = QuinIdGet;
                        row["DescFechaIngreso"] = "01/01/1990";
                        row["DescEstadoDelete"] = "0";
  

                        tablaDescuentosEmp.Rows.Add(row);
  
                        contadorChkFalse= contadorChkFalse+1;
                    }
                    else
                    {

                        /*if (list.Contains(selectedKey)){  list.Remove(selectedKey);}*/
                    }
                }

                if (!hddIdEmpleado.Value.Equals(""))
                {
                    DataRow row = tablaDescuentosEmp.NewRow();

                    row["DescEmpId"]        = hddIdEmpleado.Value;
                    row["DescEmprId"]       = "" + getEmprId();
                    row["DescUsuIdIngresa"] = "" + getUsuId();
                    row["DescCantidad"]     = this.txtTareasCantidad.Text;
                    row["DescRazon"]        = this.txtDescRazon.Text;
                    row["DescQuinId"]       = QuinIdGet;
                    row["DescFechaIngreso"] = "01/01/1990";
                    row["DescEstadoDelete"] = "0";


                    tablaDescuentosEmp.Rows.Add(row);
                }
                else {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('C1010 Debe de seleccionar al menos un empleado')", true);

                }

                client.PLwebDescuentosEmpSet(tablaDescuentosEmp);

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
        /*
        protected void ddl_listadoFechas_Change(object sender, EventArgs e)
        {
            BindDataMoviPlani();
        }
         */ 
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