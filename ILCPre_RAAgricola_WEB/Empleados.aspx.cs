using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace Cabana.Campo.RAAgricola.Pre.Web
{
    public partial class Empleados : System.Web.UI.Page
    {
        public static Cabana.Campo.RAAgricola.Pre.Web.localhostWs.DS_ILC_Campo.PLwebEmpleadoDataTable tablaEmpleados;
        public static Cabana.Campo.RAAgricola.Pre.Web.localhostWs.DS_ILC_Campo.PLwebFrentesDataTable tablaFrentes;
        public static Cabana.Campo.RAAgricola.Pre.Web.localhostWs.DS_ILC_Campo.PLwebAccUsuCuadrillasDataTable tablaCuadrillas;
        public static Cabana.Campo.RAAgricola.Pre.Web.localhostWs.DS_ILC_Campo.PLwebTipoEmpleadoDataTable tablaTipoEmp;
        public static Cabana.Campo.RAAgricola.Pre.Web.localhostWs.DS_ILC_Campo.PLwebEmpCuaDataTable tablaEmpCua;

        public static Cabana.Campo.RAAgricola.Pre.Web.localhostWs.WSRAAgricola client;
        ConfigSession configSession = new ConfigSession();
        //public static String EmprId = "";
        //public static int UsuId;
        //public static int UsuNivelAcceso;
        public static String mesajeAccion;

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
                getValidarSesion();
            }
            else
            {
                Response.Write("<script language='javascript'> window.location.replace('Login.aspx');</" + "script>");
                Response.End();
            }

            if (!IsPostBack)
            {

                tablaFrentes = new Cabana.Campo.RAAgricola.Pre.Web.localhostWs.DS_ILC_Campo.PLwebFrentesDataTable();
                tablaEmpleados = new Cabana.Campo.RAAgricola.Pre.Web.localhostWs.DS_ILC_Campo.PLwebEmpleadoDataTable();
                this.txtFrmEmpEmail.Attributes["type"] = "email";
                this.txtFrmEmpIsss.Attributes["type"] = "number";
                this.txtFrmEmpAfp.Attributes["type"] = "number";

                client = new Cabana.Campo.RAAgricola.Pre.Web.localhostWs.WSRAAgricola();

                BindDataEmpleados();
                BindDataEmpCua();
                BindDataCuadrillaByIdDDL();

                BindDataFrmFrenteByIdDDL();
                BindDataFrmTipoEmpByIdDDL();
                cargarGridEmpleados();
            }
        }



        public Boolean getValidarSesion()
        {

            if (!configSession.validarSession(getUsuNivelAcceso(), "Empleados"))//NumeroDeAcceso, NombrePagina
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

        protected void BindDataEmpCua()
        {
            tablaEmpCua = client.PLwebEmpCuaEmpGet(getUsuId(), getEmprId());
        }

    
        protected void BindDataCuadrillaByIdDDL()
        {
            tablaCuadrillas = new Cabana.Campo.RAAgricola.Pre.Web.localhostWs.DS_ILC_Campo.PLwebAccUsuCuadrillasDataTable();
            tablaCuadrillas = client.AccUsuCuadrillasGet(null, getEmprId(), getUsuId(), null);

            this.ddlistCuadrilla.DataSource = tablaCuadrillas;
            this.ddlistCuadrilla.DataValueField = "CuaId";
            this.ddlistCuadrilla.DataTextField = "CuaNombre";
            this.ddlistCuadrilla.DataBind();

            if (tablaCuadrillas.Count == 0)
            {
                this.ddlistCuadrilla.Items.Insert(0, new ListItem("Seleccionar Cuadrilla", "0"));
                Response.Write("<script language='javascript'> alert('No tienenes cuadrillas asignadas');</" + "script>");
            }
        }


        protected void BindDataFrmTipoEmpByIdDDL()
        {
            tablaTipoEmp = client.TipoEmpGet(null, getEmprId());

            this.ddlFrmEmpTipo.DataSource = tablaTipoEmp;
            this.ddlFrmEmpTipo.DataValueField = "TipoId";
            this.ddlFrmEmpTipo.DataTextField = "TipoEmpDesc";
            this.ddlFrmEmpTipo.DataBind();
            this.ddlFrmEmpTipo.Items.Insert(0, new ListItem("Seleccionar Tipo Empleado", "0"));
        }

        protected void BindDataFrmFrenteByIdDDL()
        {
            tablaFrentes = client.FrenteGet(null, getEmprId(), 1, "");

            this.ddlFrmEmpFrente.DataSource = tablaFrentes;
            this.ddlFrmEmpFrente.DataValueField = "FrenteId";
            this.ddlFrmEmpFrente.DataTextField = "FrenteNombre";
            this.ddlFrmEmpFrente.DataBind();
            this.ddlFrmEmpFrente.Items.Insert(0, new ListItem("Seleccionar Frente", "0"));
        }

        protected void BindDataEmpleados()
        {
            tablaEmpleados = client.EmpleadoGet(null, null, null, null, "", getEmprId(),"");
        }


        protected void gvFrentes_RowCommand(object sender, GridViewCommandEventArgs e)
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

        // CUADRILLAS

        protected void gvCuadrillas_RowCommand(object sender, GridViewCommandEventArgs e)
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

        // GRID TIPO EMPLEADO

        protected void gvTipoEmpleado_RowCommand(object sender, GridViewCommandEventArgs e)
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


        //GRIDVIEW BUSCAR EMPLEADOS

        protected void gvBuscarEmpleados_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            
            if (e.CommandName.Equals("SelectByIdBuscarEmp"))
            {

                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = this.gvBuscarEmpleados.Rows[index];
                Label lblBuscarEmpId = (Label)row.FindControl("lblBuscarEmpId");

                try
                {
                    BindDataCuadrillaByIdDDL();
                    BindDataEmpleados();
                    int idRegistro = Int32.Parse(lblBuscarEmpId.Text);
                    if (tablaCuadrillas.Rows.Count > 0)
                    {

                        DataRow rows = tablaEmpleados.FindByEmpId(idRegistro);

                        String valueSeleccionadoFrente = rows.Field<Int32>("EmpFrente").ToString();
                        String valueSeleccionadoCuadrilla = rows.Field<Int32>("EmpCuadrilla").ToString();
                        String valueSeleccionadoTipoEmp = rows.Field<Int32>("EmpTipoEmp").ToString();
                        String valueSeleccionadoSexo = rows.Field<String>("EmpSexo").ToString();
                        String valueSeleccionadoEstatus = rows.Field<String>("EmpEstatus").ToString();

                        this.ddlFrmEmpEstatus.SelectedIndex = this.ddlFrmEmpEstatus.Items.IndexOf(this.ddlFrmEmpEstatus.Items.FindByValue(valueSeleccionadoEstatus));


                        this.ddlFrmEmpFrente.SelectedIndex = this.ddlFrmEmpFrente.Items.IndexOf(this.ddlFrmEmpFrente.Items.FindByValue(valueSeleccionadoFrente));
                        //ddlistFrmEmpFrentes_Change();
                        this.ddlFrmEmpCuadrilla.SelectedIndex = this.ddlFrmEmpCuadrilla.Items.IndexOf(this.ddlFrmEmpCuadrilla.Items.FindByValue(valueSeleccionadoCuadrilla));
                        this.ddlFrmEmpTipo.SelectedIndex = this.ddlFrmEmpTipo.Items.IndexOf(this.ddlFrmEmpTipo.Items.FindByValue(valueSeleccionadoTipoEmp));
                        this.rblFrmEmpSexo.SelectedIndex = this.rblFrmEmpSexo.Items.IndexOf(this.rblFrmEmpSexo.Items.FindByValue(valueSeleccionadoSexo));

                        this.hddFrmEmpId.Value = rows.Field<Int32>("EmpId").ToString();
                        this.txtFrmEmpApellidos.Text = rows.Field<String>("EmpApellidos");
                        this.txtFrmEmpNombres.Text = rows.Field<String>("EmpNombres");
                        this.txtFrmEmpFechaNacimiento.Text = rows.Field<DateTime>("EmpFechaNaci").ToString("dd/MM/yyyy");
                        this.txtFrmEmpTelFijo.Text = rows.Field<String>("EmpTelCasa");
                        this.txtFrmEmpTelEmergencia.Text = rows.Field<String>("EmpTelCasoEmerg");
                        this.txtFrmEmpTelCelular.Text = rows.Field<String>("EmpCelular");
                        this.txtFrmEmpDireccion.Text = rows.Field<String>("EmpDireccionPar");
                        this.txtFrmEmpEmail.Text = rows.Field<String>("EmpCorreoElectronico");
                        this.txtFrmEmpNumDui.Text = rows.Field<String>("EmpDUI");
                        this.txtFrmEmpNit.Text = rows.Field<String>("EmpNIT");
                        this.txtFrmEmpIsss.Text = rows.Field<String>("EmpISSS");
                        this.txtFrmEmpAfp.Text = rows.Field<String>("EmpAFP");

                    }
                }
                catch (System.IO.IOException er)
                {
                    Console.WriteLine("Error en el insert", er);
                }
            }
        }
        protected void gvBuscarEmpleados_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            //BindDataEmpCua();
            this.gvBuscarEmpleados.PageIndex = e.NewPageIndex;
            this.gvBuscarEmpleados.DataSource = tablaEmpCua;
            this.gvBuscarEmpleados.EditIndex = -1;
            this.gvBuscarEmpleados.DataBind();

        }
        protected void lbtnBuscarNombreEmp_Click(object sender, EventArgs e)
        {

        }


        protected void ddlistCuadrillas_Change(object sender, EventArgs e)
        {      
            cargarGridEmpleados();
        }

        public void cargarGridEmpleados() 
        {
            BindDataEmpCua();
            String textoBuscar = this.txtControlBuscarEmpleado.Text;
            this.gvBuscarEmpleados.DataSource = tablaEmpCua;
            (this.gvBuscarEmpleados.DataSource as DataTable).DefaultView.RowFilter = "CuaId = " + this.ddlistCuadrilla.SelectedValue + "  AND ( CONVERT ( EmpId , System.String ) LIKE '" + textoBuscar + "' OR EmpDUI LIKE '%" + textoBuscar + "%' OR nombreCompleto LIKE '%" + textoBuscar + "%' ) "; //
            this.gvBuscarEmpleados.DataBind();
        }

        protected void ddlistFrmEmpFrentes_Change(object sender, EventArgs e)
        {
            ddlistFrmEmpFrentes_Change();
        }
        protected void ddlistFrmEmpFrentes_Change()
        {
            BindDataCuadrillaByIdDDL();
            this.ddlFrmEmpCuadrilla.DataSource = tablaCuadrillas;
            (this.ddlFrmEmpCuadrilla.DataSource as DataTable).DefaultView.RowFilter = " FrenteId = '" + this.ddlFrmEmpFrente.SelectedValue + "'";

            this.ddlFrmEmpCuadrilla.DataValueField = "CuaId";
            this.ddlFrmEmpCuadrilla.DataTextField = "CuaNombre";
            this.ddlFrmEmpCuadrilla.DataBind();
            this.ddlFrmEmpCuadrilla.Items.Insert(0, new ListItem("Seleccionar Cuadrilla", "0"));
        }

        protected void lbtn_FrmEmpNuevo_Click(object sender, EventArgs e)
        {
            //BindDataEmpleados();
            DataRow row = tablaEmpleados.NewRow();
            if(setEmpleados(row, "INSERT")){

                limpiarCamposEmp();
                BindDataEmpleados();
                BindDataEmpCua();
                cargarGridEmpleados();
            }
        }

        protected Boolean setEmpleados(DataRow row, String tipo)
        {
            Boolean miRetorno = false;
            if (this.ddlFrmEmpTipo.SelectedValue.Equals("0") ||
                this.ddlFrmEmpFrente.SelectedValue.Equals("0") ||
                this.ddlFrmEmpCuadrilla.SelectedValue.Equals("0") ||
                this.txtFrmEmpApellidos.Text.Equals("") ||
                this.txtFrmEmpNombres.Text.Equals("") ||
                this.txtFrmEmpFechaNacimiento.Text.Equals("") ||
                this.txtFrmEmpDireccion.Text.Equals("") ||
                this.txtFrmEmpNumDui.Text.Equals("")
                )
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('A4040 Todos los campos con * son Necesarios')", true);
            }
            else
            {

                mesajeAccion = "No se puedo guardar el registro.";
                try
                {
                    row["EmpTipoEmp"] = this.ddlFrmEmpTipo.SelectedValue;
                    row["EmpFrente"] = this.ddlFrmEmpFrente.SelectedValue;
                    row["EmpCuadrilla"] = this.ddlFrmEmpCuadrilla.SelectedValue;
                    row["EmpApellidos"] = this.txtFrmEmpApellidos.Text;
                    row["EmpNombres"] = this.txtFrmEmpNombres.Text;
                    row["EmpProfesion"] = "";
                    row["EmpDuiExtenEn"] = "";
                    row["EmpDuiEl"] = "30/01/2001";
                    row["EmpFechaNaci"] = this.txtFrmEmpFechaNacimiento.Text;
                    row["EmpTelCasa"] = this.txtFrmEmpTelFijo.Text;
                    row["EmpTelCasoEmerg"] = this.txtFrmEmpTelEmergencia.Text;
                    row["EmpCelular"] = this.txtFrmEmpTelCelular.Text;
                    row["EmpDireccionLab"] = "";
                    row["EmpDireccionPar"] = this.txtFrmEmpDireccion.Text;
                    row["EmpCorreoElectronico"] = this.txtFrmEmpEmail.Text;
                    row["EmpDUI"] = this.txtFrmEmpNumDui.Text;
                    row["EmpNIT"] = this.txtFrmEmpNit.Text;
                    row["EmpISSS"] = this.txtFrmEmpIsss.Text;
                    row["EmpAFP"] = this.txtFrmEmpAfp.Text;
                    row["EmpNup"] = "";
                    row["EmpLicencia"] = "";
                    row["EmpRutaFoto"] = "";
                    row["EmpSexo"] = this.rblFrmEmpSexo.SelectedValue;
                    row["EmprId"] = getEmprId();
                    row["EmpEstatus"] = this.ddlFrmEmpEstatus.SelectedValue;

                    if (tipo.Equals("INSERT"))
                        tablaEmpleados.Rows.Add(row);

                    client.EmpleadoSet(tablaEmpleados);
                    mesajeAccion = "Empleado guardado exitosamente";
                    miRetorno = true;

                }
                catch (System.Exception error)
                {
                    if (error.ToString().Contains("EmpFechaNaci. El tipo esperado"))
                        mesajeAccion = "Fecha Incorrecta.";
                    else
                        mesajeAccion = "No se puedo guardar el registro.";
                    miRetorno = false;
                }
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + mesajeAccion + "')", true);
            }
            return miRetorno;
        }

        protected void gvBuscarEmpleados_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            //BindDataEmpleados();
            int idRegistro = Int32.Parse(this.hddFrmEmpId.Value);
            DataRow rows = tablaEmpleados.FindByEmpId(idRegistro);


            if (setEmpleados(rows, "UPDATE"))
            {
                gvBuscarEmpleados.EditIndex = -1;
                BindDataEmpleados();
                BindDataEmpCua();
                cargarGridEmpleados();
                limpiarCamposEmp();
            }


        }
        protected void gvBuscarEmpleados_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                //BindDataEmpleados();
                Label lblBuscarEmpId = (Label)this.gvBuscarEmpleados.Rows[e.RowIndex].FindControl("lblBuscarEmpId");
                int idRegistro = Int32.Parse(lblBuscarEmpId.Text);
                DataRow rows = tablaEmpleados.FindByEmpId(idRegistro);
                rows.Delete();
                client.EmpleadoSet(tablaEmpleados);
                mesajeAccion = "Registro eliminado con exito";
            }
            catch (Exception error)
            {
                if (error.ToString().Contains("REFERENCE"))
                    mesajeAccion = "Este registro ya tiene datos vinculados.";
                else
                    mesajeAccion = "Informar de este error a soporte tecnico";
            }
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + mesajeAccion + "')", true);
            gvBuscarEmpleados.EditIndex = -1;
            BindDataEmpleados();
            BindDataEmpCua();
            cargarGridEmpleados();

            limpiarCamposEmp();

        }
        protected void gvBuscarEmpleados_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            //BindDataEmpCua();
            this.gvBuscarEmpleados.EditIndex = -1;
            this.gvBuscarEmpleados.DataSource = tablaEmpCua;
            this.gvBuscarEmpleados.DataBind();

            limpiarCamposEmp();

        }
        protected void gvBuscarEmpleados_RowEditing(object sender, GridViewEditEventArgs e)
        {

            this.gvBuscarEmpleados.EditIndex = e.NewEditIndex;
            this.gvBuscarEmpleados.DataSource = tablaEmpCua;
            this.gvBuscarEmpleados.DataBind();

            Label lblBuscarEmpId = (Label)gvBuscarEmpleados.Rows[e.NewEditIndex].FindControl("lblBuscarEmpId");

            try
            {
                int idRegistro = Int32.Parse(lblBuscarEmpId.Text);
                if (tablaCuadrillas.Rows.Count > 0)
                {

                    DataRow rows = tablaEmpleados.FindByEmpId(idRegistro);


                    String valueSeleccionadoFrente = rows.Field<Int32>("EmpFrente").ToString();
                    String valueSeleccionadoCuadrilla = rows.Field<Int32>("EmpCuadrilla").ToString();
                    String valueSeleccionadoTipoEmp = rows.Field<Int32>("EmpTipoEmp").ToString();
                    String valueSeleccionadoSexo = rows.Field<String>("EmpSexo").ToString();
                    String valueSeleccionadoEstatus = rows.Field<String>("EmpEstatus").ToString();

                    this.ddlFrmEmpEstatus.SelectedIndex = this.ddlFrmEmpEstatus.Items.IndexOf(this.ddlFrmEmpEstatus.Items.FindByValue(valueSeleccionadoEstatus));

                    this.ddlFrmEmpFrente.SelectedIndex = this.ddlFrmEmpFrente.Items.IndexOf(this.ddlFrmEmpFrente.Items.FindByValue(valueSeleccionadoFrente));
                    ddlistFrmEmpFrentes_Change();
                    this.ddlFrmEmpCuadrilla.SelectedIndex = this.ddlFrmEmpCuadrilla.Items.IndexOf(this.ddlFrmEmpCuadrilla.Items.FindByValue(valueSeleccionadoCuadrilla));
                    this.ddlFrmEmpTipo.SelectedIndex = this.ddlFrmEmpTipo.Items.IndexOf(this.ddlFrmEmpTipo.Items.FindByValue(valueSeleccionadoTipoEmp));
                    this.rblFrmEmpSexo.SelectedIndex = this.rblFrmEmpSexo.Items.IndexOf(this.rblFrmEmpSexo.Items.FindByValue(valueSeleccionadoSexo));

                    this.hddFrmEmpId.Value = rows.Field<Int32>("EmpId").ToString();
                    this.txtFrmEmpApellidos.Text = rows.Field<String>("EmpApellidos");
                    this.txtFrmEmpNombres.Text = rows.Field<String>("EmpNombres");
                    this.txtFrmEmpFechaNacimiento.Text = rows.Field<DateTime>("EmpFechaNaci").ToString("dd/MM/yyyy");
                    this.txtFrmEmpTelFijo.Text = rows.Field<String>("EmpTelCasa");
                    this.txtFrmEmpTelEmergencia.Text = rows.Field<String>("EmpTelCasoEmerg");
                    this.txtFrmEmpTelCelular.Text = rows.Field<String>("EmpCelular");
                    this.txtFrmEmpDireccion.Text = rows.Field<String>("EmpDireccionPar");
                    this.txtFrmEmpEmail.Text = rows.Field<String>("EmpCorreoElectronico");
                    this.txtFrmEmpNumDui.Text = rows.Field<String>("EmpDUI");
                    this.txtFrmEmpNit.Text = rows.Field<String>("EmpNIT");
                    this.txtFrmEmpIsss.Text = rows.Field<String>("EmpISSS");
                    this.txtFrmEmpAfp.Text = rows.Field<String>("EmpAFP");

                    this.lbtn_FrmEmpNuevo.Visible = false;
                }
            }
            catch (System.IO.IOException er)
            {
                Console.WriteLine("Error en el insert", er);
            }
        }

        protected void lbtnBuscarEmpleado_Click(object sender, EventArgs e)
        {
            cargarGridEmpleados();
            limpiarCamposEmp();
        }

        protected void limpiarCamposEmp()
        {
            this.txtFrmEmpApellidos.Text = "";
            this.txtFrmEmpNombres.Text = "";
            this.txtFrmEmpFechaNacimiento.Text = "";
            this.txtFrmEmpTelFijo.Text = "";
            this.txtFrmEmpTelEmergencia.Text = "";
            this.txtFrmEmpTelCelular.Text = "";
            this.txtFrmEmpDireccion.Text = "";
            this.txtFrmEmpEmail.Text = "";
            this.txtFrmEmpNumDui.Text = "";
            this.txtFrmEmpNit.Text = "";
            this.txtFrmEmpIsss.Text = "";
            this.txtFrmEmpAfp.Text = "";

            this.ddlFrmEmpFrente.SelectedIndex = this.ddlFrmEmpFrente.Items.IndexOf(this.ddlFrmEmpFrente.Items.FindByValue("0"));
            this.ddlFrmEmpEstatus.SelectedIndex = this.ddlFrmEmpEstatus.Items.IndexOf(this.ddlFrmEmpEstatus.Items.FindByValue("a"));

            ddlistFrmEmpFrentes_Change();
            this.ddlFrmEmpCuadrilla.SelectedIndex = this.ddlFrmEmpCuadrilla.Items.IndexOf(this.ddlFrmEmpCuadrilla.Items.FindByValue("0"));
            this.ddlFrmEmpTipo.SelectedIndex = this.ddlFrmEmpTipo.Items.IndexOf(this.ddlFrmEmpTipo.Items.FindByValue("0"));
            this.rblFrmEmpSexo.SelectedIndex = this.rblFrmEmpSexo.Items.IndexOf(this.rblFrmEmpSexo.Items.FindByValue("M"));
            this.lbtn_FrmEmpNuevo.Visible = true;
            this.gvBuscarEmpleados.EditIndex = -1;
        }
    }
}