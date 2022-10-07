using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Cabana.Campo.RAAgricola.Pre.Web
{
    public partial class EmpPorCuadrillas : System.Web.UI.Page
    {
        public static Cabana.Campo.RAAgricola.Pre.Web.localhostWs.DS_ILC_Campo.PLwebEmpleadoDataTable tablaEmpleados;
        public static Cabana.Campo.RAAgricola.Pre.Web.localhostWs.DS_ILC_Campo.PLwebFrentesDataTable tablaFrentes;
        public static Cabana.Campo.RAAgricola.Pre.Web.localhostWs.DS_ILC_Campo.PLwebAccUsuCuadrillasDataTable tablaCuadrillas;
        public static Cabana.Campo.RAAgricola.Pre.Web.localhostWs.DS_ILC_Campo.PLwebEmpCuaDataTable tablaEmpCua;

        public static Cabana.Campo.RAAgricola.Pre.Web.localhostWs.WSRAAgricola client;
        ConfigSession configSession = new ConfigSession();
        //public static int EmprId;
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
                if (!configSession.validarSession(getUsuNivelAcceso(), "EmpPorCuadrillas"))//NumeroDeAcceso, NombrePagina
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

                tablaFrentes = new Cabana.Campo.RAAgricola.Pre.Web.localhostWs.DS_ILC_Campo.PLwebFrentesDataTable();
                tablaEmpleados = new Cabana.Campo.RAAgricola.Pre.Web.localhostWs.DS_ILC_Campo.PLwebEmpleadoDataTable();

                client = new Cabana.Campo.RAAgricola.Pre.Web.localhostWs.WSRAAgricola();

                
                BindDataEmpCua();
                BindDataCuadrillaByIdDDL();
                BindDataEmpleados();

                cargarGridEmpCua();
                cargarGridListadoEmpleados();


            }
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

        protected void BindDataEmpCua()
        {
            tablaEmpCua = client.PLwebEmpCuaEmpGet(getUsuId(),getEmprId());
        }

        public void cargarGridEmpCua()
        {
            BindDataEmpCua();
            this.gvBuscarEmpleados.DataSource = tablaEmpCua;
            (this.gvBuscarEmpleados.DataSource as DataTable).DefaultView.RowFilter = " CuaId = '" + this.ddlistCuadrilla.SelectedValue + "'";
            this.gvBuscarEmpleados.DataBind();
        }

        protected void BindDataEmpleados()
        {
            tablaEmpleados = client.EmpleadoGet(null, null, null, Convert.ToInt32(this.ddlistCuadrilla.SelectedValue), "", getEmprId(), "");
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

        }

        protected void gvListadoEmpleados_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("insertarEmpCua"))
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

        protected void gvBuscarEmpleados_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            BindDataEmpCua();
            this.gvBuscarEmpleados.PageIndex = e.NewPageIndex;
            this.gvBuscarEmpleados.DataSource = tablaEmpCua;
            this.gvBuscarEmpleados.EditIndex = -1;
            this.gvBuscarEmpleados.DataBind();

        }

        protected void gvListadoEmpleados_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            BindDataEmpleados();
            this.gvListadoEmpleados.PageIndex = e.NewPageIndex;
            this.gvListadoEmpleados.DataSource = tablaEmpleados;
            this.gvListadoEmpleados.EditIndex = -1;
            this.gvListadoEmpleados.DataBind();

        }

        protected void lbtnBuscarNombreEmp_Click(object sender, EventArgs e)
        {

        }
        protected void gvListadoEmpleados_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            BindDataEmpleados();
            BindDataEmpCua();
            Label lblBuscarEmpId = (Label)this.gvListadoEmpleados.Rows[e.RowIndex].FindControl("lblBuscarEmpId");
            int idRegistro = Int32.Parse(lblBuscarEmpId.Text);
            DataRow rows = tablaEmpleados.FindByEmpId(idRegistro);

            //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('registro:"+  rows["EmpId"] + " "+ rows["EmpNombres"]+"')", true);
            DataRow row = tablaEmpCua.NewRow();
            if (setEmpCuaInsert(row, Convert.ToInt32(lblBuscarEmpId.Text), getUsuId()))
            {
                BindDataEmpCua();
                //cargarGridEmpCua();
                BindDataEmpleados();
                //cargarGridListadoEmpleados();
                buscarEmpleadosGrids();
            }
        }

        protected void ddlistCuadrillas_Change(object sender, EventArgs e)
        {
            cargarGridEmpCua();
            BindDataEmpleados();
            cargarGridListadoEmpleados();
        }


        public void cargarGridListadoEmpleados()
        {
            BindDataEmpleados();
            this.gvListadoEmpleados.DataSource = tablaEmpleados;
            this.gvListadoEmpleados.DataBind();
        }

        protected Boolean setEmpCuaInsert(DataRow row, int EmpId, int UsuId )
        {

            Boolean miRetorno = false;
            mesajeAccion = "No se puedo guardar el registro.";
            
                try
                {
                //BindDataEmpCua();
                row["CuaId"] = this.ddlistCuadrilla.SelectedValue;
                    row["EmpId"] = EmpId;
                    row["EcuUsuIdCrea"] = UsuId;

                    //if (tipo.Equals("INSERT"))
                        tablaEmpCua.Rows.Add(row);

                    client.PLwebEmpCuaEmpSet(tablaEmpCua);
                mesajeAccion = "Empleado guardado exitosamente";
                    miRetorno = true;
                
                }
                catch (System.Exception error)
                {
                    mesajeAccion = "No se puedo guardar el registro.";
                    miRetorno = false;
                }
            
            return miRetorno;
        }



        protected void gvBuscarEmpleados_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                BindDataEmpCua();
                HiddenField gvhdd_EcuId = (HiddenField)this.gvBuscarEmpleados.Rows[e.RowIndex].FindControl("gvhdd_EcuId");
                int EcuId = Int32.Parse(gvhdd_EcuId.Value);
                DataRow rows = tablaEmpCua.FindByEcuId(EcuId);
                rows.Delete();
                client.PLwebEmpCuaEmpSet(tablaEmpCua);

                mesajeAccion = "Registro eliminado con exito";
            }
            catch (Exception error)
            {
                if (error.ToString().Contains("REFERENCE"))
                    mesajeAccion = "Este registro ya tiene datos vinculados.";
                else
                    mesajeAccion = "Informar de este error a soporte tecnico";
            }

            BindDataEmpCua();
            BindDataEmpleados();
            buscarEmpleadosGrids();

        }
        protected void gvBuscarEmpleados_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            BindDataEmpCua();
            this.gvBuscarEmpleados.EditIndex = -1;
            this.gvBuscarEmpleados.DataSource = tablaEmpCua;
            this.gvBuscarEmpleados.DataBind();
        }

        protected void gvBuscarEmpleados_RowEditing(object sender, GridViewEditEventArgs e)
        {
            this.gvBuscarEmpleados.EditIndex = e.NewEditIndex;
            this.gvBuscarEmpleados.DataSource = tablaEmpCua;
            this.gvBuscarEmpleados.DataBind();
        }

        protected void lbtnBuscarEmpleado_Click(object sender, EventArgs e)
        {
            buscarEmpleadosGrids();
        }
        public void buscarEmpleadosGrids() {
            BindDataEmpleados();
            BindDataEmpCua();
            String textoBuscar = this.txtControlBuscarEmpleado.Text;
            this.gvBuscarEmpleados.DataSource = tablaEmpCua;
            (this.gvBuscarEmpleados.DataSource as DataTable).DefaultView.RowFilter = "CuaId = " + this.ddlistCuadrilla.SelectedValue + "  AND ( nombreCompleto LIKE '%" + textoBuscar + "%' OR EmpDUI LIKE '%" + textoBuscar + "%') ";
            this.gvBuscarEmpleados.DataBind();

            this.gvListadoEmpleados.DataSource = tablaEmpleados;
            (this.gvListadoEmpleados.DataSource as DataTable).DefaultView.RowFilter = "EmpNombres LIKE '%" + textoBuscar + "%' OR EmpApellidos LIKE '%" + textoBuscar + "%' ";
            this.gvListadoEmpleados.DataBind();
        }



    }
}