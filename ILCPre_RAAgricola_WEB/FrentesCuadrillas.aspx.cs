using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace Cabana.Campo.RAAgricola.Pre.Web
{
    public partial class FrentesCuadrillas : System.Web.UI.Page
    {

        public static Cabana.Campo.RAAgricola.Pre.Web.localhostWs.DS_ILC_Campo.PLwebFrentesDataTable tablaFrentes;
        public static Cabana.Campo.RAAgricola.Pre.Web.localhostWs.DS_ILC_Campo.PLwebCuadrillasDataTable tablaCuadrillas;
        public static Cabana.Campo.RAAgricola.Pre.Web.localhostWs.DS_ILC_Campo.PLwebTipoEmpleadoDataTable tablaTipoEmp;
        public static Cabana.Campo.RAAgricola.Pre.Web.localhostWs.WSRAAgricola client;
        ConfigSession configSession = new ConfigSession();

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
                if (!configSession.validarSession(getUsuNivelAcceso(), "FrentesCuadrillas"))//NumeroDeAcceso, NombrePagina
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
                tablaCuadrillas = new Cabana.Campo.RAAgricola.Pre.Web.localhostWs.DS_ILC_Campo.PLwebCuadrillasDataTable();

                client = new Cabana.Campo.RAAgricola.Pre.Web.localhostWs.WSRAAgricola();

                BindDataFrentes();
                BindDataCuadrillas();
                BindDataTipoEmp();

                BindDataFrenteByIdDDL();


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

        protected void BindDataFrenteByIdDDL()
        {
            CargarTablaFrentes();
            // Hace el enlace al DataTable contenido en el DataSet
            this.ddlistFrente.DataSource = tablaFrentes;
            this.ddlistFrente.DataValueField = "FrenteId";
            this.ddlistFrente.DataTextField = "FrenteNombre";
            this.ddlistFrente.DataBind();
            this.ddlistFrente.Items.Insert(0, new ListItem("Seleccionar Frente", "0"));
        }


        protected void BindDataFrentes()
        {
            CargarTablaFrentes();
            gvFrentes.DataSource = tablaFrentes;
            gvFrentes.DataBind();
        }

        public void CargarTablaFrentes() {
            tablaFrentes = client.FrenteGet(null, getEmprId(), 1, "");
        }


        protected void BindDataCuadrillas()
        {
            
            tablaCuadrillas = client.CuadrillaGet(null, null, 1, getEmprId(), null);

        }


        protected void BindDataTipoEmp()
        {
            CargarTablaTipoEmpleado();
            this.gvTipoEmpleado.DataSource = tablaTipoEmp;
            this.gvTipoEmpleado.DataBind();
        }

        public void CargarTablaTipoEmpleado() {
            tablaTipoEmp = client.TipoEmpGet(null, getEmprId());
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
        protected void gvFrentes_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            Label lblFrenteId = (Label)gvFrentes.Rows[e.RowIndex].FindControl("lblFrenteId");
            TextBox txtEditFrenteNombre = (TextBox)gvFrentes.Rows[e.RowIndex].FindControl("txtEditFrenteNombre");
            try
            {
                CargarTablaFrentes();
                int idRegistro = Int32.Parse(lblFrenteId.Text);
                if (tablaFrentes.Rows.Count > 0)
                {
                    DataRow rows = tablaFrentes.FindByFrenteId(idRegistro);
                    rows["FrenteNombre"] = txtEditFrenteNombre.Text;
                    client.FrenteSet(tablaFrentes);
                    gvFrentes.EditIndex = -1;
                    BindDataFrentes();
                    BindDataFrenteByIdDDL();
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
        protected void gvFrentes_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                CargarTablaFrentes();
                Label lblFrenteId = (Label)this.gvFrentes.Rows[e.RowIndex].FindControl("lblFrenteId");
                int idRegistro = Int32.Parse(lblFrenteId.Text);
                DataRow rows = tablaFrentes.FindByFrenteId(idRegistro);
                rows.Delete();
                client.FrenteSet(tablaFrentes);
                BindDataFrentes();
                BindDataFrenteByIdDDL();
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
            BindDataFrentes();
            BindDataFrenteByIdDDL();
        }
        protected void gvFrentes_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            CargarTablaFrentes();
            this.gvFrentes.EditIndex = -1;
            this.gvFrentes.DataSource = tablaFrentes;
            this.gvFrentes.DataBind();
        }
        protected void gvFrentes_RowEditing(object sender, GridViewEditEventArgs e)
        {
            CargarTablaFrentes();
            this.gvFrentes.EditIndex = e.NewEditIndex;
            this.gvFrentes.DataSource = tablaFrentes;
            this.gvFrentes.DataBind();
        }
        protected void gvFrentes_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvFrentes.PageIndex = e.NewPageIndex;
            this.gvFrentes.DataSource = tablaFrentes;
            this.gvFrentes.EditIndex = -1;
            this.gvFrentes.DataBind();
            //Session["Nombre"] = "Juan Jose Reyes";

        }
        protected void lbtnAddNuevoFrente_Click(object sender, EventArgs e)
        {
            try
            {
                CargarTablaFrentes();
                DataRow row = tablaFrentes.NewRow();

                row["FrenteNombre"] = this.txtAddNombreFrente.Text;
                row["EmprId"] = getEmprId();
                tablaFrentes.Rows.Add(row);
                client.FrenteSet(tablaFrentes);
                gvFrentes.EditIndex = -1;
                this.txtAddNombreFrente.Text = "";
                BindDataFrentes();
                BindDataFrenteByIdDDL();

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
        protected void gvCuadrillas_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            Label lblCuaId = (Label)gvCuadrillas.Rows[e.RowIndex].FindControl("lblCuaId");
            TextBox txtEditCuaNombre = (TextBox)gvCuadrillas.Rows[e.RowIndex].FindControl("txtEditCuaNombre");
            try
            {
                BindDataCuadrillas();
                int idRegistro = Int32.Parse(lblCuaId.Text);
                if (tablaCuadrillas.Rows.Count > 0)
                {

                    DataRow rows = tablaCuadrillas.FindByCuaId(idRegistro);
                    rows["CuaNombre"] = txtEditCuaNombre.Text;

                    //String CuaNombre = rows.Field<String>("CuaNombre");

                    client.CuadrillaSet(tablaCuadrillas);
                    gvCuadrillas.EditIndex = -1;
                    BindDataCuadrillas();
                    this.gvCuadrillas.DataSource = tablaCuadrillas;
                    (this.gvCuadrillas.DataSource as DataTable).DefaultView.RowFilter = " FrenteId = '" + this.ddlistFrente.SelectedValue + "'";
                    this.gvCuadrillas.DataBind();
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
        protected void gvCuadrillas_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                BindDataCuadrillas();
                Label lblCuaId = (Label)this.gvCuadrillas.Rows[e.RowIndex].FindControl("lblCuaId");
                int idRegistro = Int32.Parse(lblCuaId.Text);
                DataRow rows = tablaCuadrillas.FindByCuaId(idRegistro);
                rows.Delete();
                client.CuadrillaSet(tablaCuadrillas);
                BindDataCuadrillas();
                this.gvCuadrillas.DataSource = tablaCuadrillas;
                (this.gvCuadrillas.DataSource as DataTable).DefaultView.RowFilter = " FrenteId = '" + this.ddlistFrente.SelectedValue + "'";
                this.gvCuadrillas.DataBind();
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
            BindDataCuadrillas();
            this.gvCuadrillas.DataSource = tablaCuadrillas;
            (this.gvCuadrillas.DataSource as DataTable).DefaultView.RowFilter = " FrenteId = '" + this.ddlistFrente.SelectedValue + "'";
            this.gvCuadrillas.DataBind();
        }
        protected void gvCuadrillas_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            BindDataCuadrillas();
            this.gvCuadrillas.EditIndex = -1;
            this.gvCuadrillas.DataSource = tablaCuadrillas;
            this.gvCuadrillas.DataBind();
        }
        protected void gvCuadrillas_RowEditing(object sender, GridViewEditEventArgs e)
        {
            BindDataCuadrillas();
            this.gvCuadrillas.EditIndex = e.NewEditIndex;
            this.gvCuadrillas.DataSource = tablaCuadrillas;
            this.gvCuadrillas.DataBind();
        }
        protected void gvCuadrillas_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            BindDataCuadrillas();
            this.gvCuadrillas.PageIndex = e.NewPageIndex;
            this.gvCuadrillas.DataSource = tablaCuadrillas;
            this.gvCuadrillas.EditIndex = -1;
            this.gvCuadrillas.DataBind();

            //Session["Nombre"] = "Juan Jose Reyes";

        }
        protected void lbtnAddNuevoCuadrilla_Click(object sender, EventArgs e)
        {
            try
            {
                BindDataCuadrillas();
                DataRow row = tablaCuadrillas.NewRow();

                //int idFrente = Convert.ToInt32(this.ddlistFrente.SelectedValue);
                if (this.ddlistFrente.SelectedValue.Equals("0") || this.txtAddNombreCuadrilla.Text.Equals(""))
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('A4040 se debe seleccionar un Frente')", true);
                }
                else
                {
                    row["CuaNombre"] = this.txtAddNombreCuadrilla.Text;
                    row["FrenteId"] = this.ddlistFrente.SelectedValue;
                    row["CuaEstatus"] = "1";
                    row["UsuIdCrea"] = getUsuId();
                    tablaCuadrillas.Rows.Add(row);
                    client.CuadrillaSet(tablaCuadrillas);
                    gvCuadrillas.EditIndex = -1;
                    this.txtAddNombreCuadrilla.Text = "";
                    BindDataCuadrillas();
                    this.gvCuadrillas.DataSource = tablaCuadrillas;
                    (this.gvCuadrillas.DataSource as DataTable).DefaultView.RowFilter = " FrenteId = '" + this.ddlistFrente.SelectedValue + "'";
                    this.gvCuadrillas.DataBind();
                }

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
        protected void gvTipoEmpleado_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            Label lblTipoId = (Label)gvTipoEmpleado.Rows[e.RowIndex].FindControl("lblTipoId");
            TextBox txtEditTipoEmpDesc = (TextBox)gvTipoEmpleado.Rows[e.RowIndex].FindControl("txtEditTipoEmpDesc");
            try
            {
                CargarTablaTipoEmpleado();
                int idRegistro = Int32.Parse(lblTipoId.Text);
                if (tablaTipoEmp.Rows.Count > 0)
                {
                    DataRow rows = tablaTipoEmp.FindByTipoId(idRegistro);
                    rows["TipoEmpDesc"] = txtEditTipoEmpDesc.Text;
                    client.TipoEmpSet(tablaTipoEmp);
                    gvTipoEmpleado.EditIndex = -1;
                    BindDataTipoEmp();
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
        protected void gvTipoEmpleado_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                CargarTablaTipoEmpleado();
                Label lblTipoId = (Label)this.gvTipoEmpleado.Rows[e.RowIndex].FindControl("lblTipoId");
                int idRegistro = Int32.Parse(lblTipoId.Text);
                DataRow rows = tablaTipoEmp.FindByTipoId(idRegistro);
                rows.Delete();
                client.TipoEmpSet(tablaTipoEmp);
                BindDataTipoEmp();
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
            BindDataTipoEmp();
        }
        protected void gvTipoEmpleado_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            CargarTablaTipoEmpleado();
            this.gvTipoEmpleado.EditIndex = -1;
            this.gvTipoEmpleado.DataSource = tablaTipoEmp;
            this.gvTipoEmpleado.DataBind();
        }
        protected void gvTipoEmpleado_RowEditing(object sender, GridViewEditEventArgs e)
        {
            CargarTablaTipoEmpleado();
            this.gvTipoEmpleado.EditIndex = e.NewEditIndex;
            this.gvTipoEmpleado.DataSource = tablaTipoEmp;
            this.gvTipoEmpleado.DataBind();
        }
        protected void gvTipoEmpleado_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvTipoEmpleado.PageIndex = e.NewPageIndex;
            this.gvTipoEmpleado.DataSource = tablaTipoEmp;
            this.gvTipoEmpleado.EditIndex = -1;
            this.gvTipoEmpleado.DataBind();
            //Session["Nombre"] = "Juan Jose Reyes";

        }
        protected void lbtnAddNuevoTipoEmp_Click(object sender, EventArgs e)
        {
            try
            {
                CargarTablaTipoEmpleado();
                DataRow row = tablaTipoEmp.NewRow();

                row["TipoEmpDesc"] = this.txtAddNombreTipoEmp.Text;
                row["EmprId"] = getEmprId();
                tablaTipoEmp.Rows.Add(row);
                client.TipoEmpSet(tablaTipoEmp);
                //gvCuadrillas.EditIndex = -1;
                this.txtAddNombreTipoEmp.Text = "";
                BindDataTipoEmp();

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


        //GRIDVIEW BUSCAR EMPLEADOS

        protected void gvBuscarEmpleados_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            if (e.CommandName.Equals("SelectByIdBuscarEmp"))
            {

                //int index = Convert.ToInt32(e.CommandArgument);
                //GridViewRow row = this.gvBuscarEmpleados.Rows[index];
                //Label lblBuscarEmpId = (Label)row.FindControl("lblBuscarEmpId");

                try
                {

                }
                catch (System.IO.IOException er)
                {
                    Console.WriteLine("Error en el insert", er);
                }
            }
        }

        protected void lbtnBuscarNombreEmp_Click(object sender, EventArgs e)
        {

        }

        //drop down list

        protected void ddlistFrentes_Change(object sender, EventArgs e)
        {
            BindDataCuadrillas();
            this.gvCuadrillas.DataSource = tablaCuadrillas;
            (this.gvCuadrillas.DataSource as DataTable).DefaultView.RowFilter = " FrenteId = '" + this.ddlistFrente.SelectedValue + "'";
            this.gvCuadrillas.DataBind();

        }



    }
}