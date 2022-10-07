using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace Cabana.Campo.RAAgricola.Pre.Web
{
    public partial class Tareas : System.Web.UI.Page
    {
        public static Cabana.Campo.RAAgricola.Pre.Web.localhostWs.DS_ILC_Campo.PLwebTareasDataTable tablaTareas;
        public static Cabana.Campo.RAAgricola.Pre.Web.localhostWs.DS_ILC_Campo.PLwebCentroCostoTareasDataTable tablaCentroCostos;
        public static Cabana.Campo.RAAgricola.Pre.Web.localhostWs.WSRAAgricola client;

        ConfigSession configSession = new ConfigSession();

        //public static String EmprId = "";
        //public static int UsuId = 0;
        public static String mesajeAccion = "";
        //public static int UsuNivelAcceso  = 0;

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

                    if (!configSession.validarSession(getUsuNivelAcceso(), "Tareas"))//NumeroDeAcceso, NombrePagina
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
                    tablaTareas = new Cabana.Campo.RAAgricola.Pre.Web.localhostWs.DS_ILC_Campo.PLwebTareasDataTable();
                    tablaCentroCostos = new Cabana.Campo.RAAgricola.Pre.Web.localhostWs.DS_ILC_Campo.PLwebCentroCostoTareasDataTable();
                    BindDataTareas();
                    BindDataCentroCostos();
                }
            }
            catch (System.Net.WebException ee)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('A5050 No es posible conectar con el servidor')", true);

            }

        }

        protected void BindDataTareas()
        {
            CargarDataTareas();
            this.gvTareas.DataSource = tablaTareas;
            this.gvTareas.DataBind();

        }

        public void CargarDataTareas() {
            tablaTareas = client.TareaGet(null, "", null, getEmprId());
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

        protected void BindDataCentroCostos()
        {
            tablaCentroCostos = client.CentroCostosGet(null);
            this.ddlFrmCentroCostos.DataSource = tablaCentroCostos;
            this.ddlFrmCentroCostos.DataValueField = "CcId";
            this.ddlFrmCentroCostos.DataTextField = "CcNombre";
            this.ddlFrmCentroCostos.DataBind();
            this.ddlFrmCentroCostos.Items.Insert(0, new ListItem("Seleccionar Centro de Costos", "0"));




        }

        protected void lbtn_AddNewTarea_Click(object sender, EventArgs e)
        {
            CargarDataTareas();
            try
            {

                DataRow row = tablaTareas.NewRow();

                row["TareaDesc"]            = this.txtFrmTareaNombre.Text;
                row["TareaTarifa"]          = this.txtFrmTareaTarifa.Text;
                row["TareaTipo"]            = this.ddlFrmTareaTipo.SelectedValue;
                row["TareaEstatus"]         = this.ddlFrmTareaEstatus.SelectedValue;
                row["EmprId"]               = getEmprId();
                row["Ccid"]                 = this.ddlFrmCentroCostos.SelectedValue;

                row["ValDiaDescanso"]       = this.txtValDiaDescanso.Text; 
                row["ValAlimentacion"]      = this.txtValAlimentacion.Text;
                row["BonoPorCumplimiento"]  = this.txtBonoPorCumplimiento.Text;
                tablaTareas.Rows.Add(row);
                client.TareaSet(tablaTareas);
                gvTareas.EditIndex = -1;

                BindDataTareas();
                limpiarFormulario();

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

        protected void lbtnBuscarTarea_Click(object sender, EventArgs e)
        {
            CargarDataTareas();
            String textoBuscar = this.txtControlBuscarTareas.Text;
            this.gvTareas.DataSource = tablaTareas;
            (this.gvTareas.DataSource as DataTable).DefaultView.RowFilter = " TareaDesc LIKE '%" + textoBuscar + "%' OR TareaTipo LIKE '%" + textoBuscar + "%'  "; //

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
            //CargarDataTareas();
            Label lblEditTareaId = (Label)gvTareas.Rows[e.RowIndex].FindControl("lblEditTareaId");

            try
            {
                int idRegistro = Int32.Parse(lblEditTareaId.Text);
                if (tablaTareas.Rows.Count > 0)
                {
                    DataRow rows = tablaTareas.FindByTareaId(idRegistro);
                    rows["TareaTipo"] = this.ddlFrmTareaTipo.SelectedValue;
                    rows["TareaEstatus"] = this.ddlFrmTareaEstatus.SelectedValue;
                    rows["TareaDesc"] = this.txtFrmTareaNombre.Text;
                    rows["TareaTarifa"] = this.txtFrmTareaTarifa.Text;
                    rows["CcId"] = this.ddlFrmCentroCostos.SelectedValue;

                    rows["ValDiaDescanso"] = this.txtValDiaDescanso.Text;
                    rows["ValAlimentacion"] = this.txtValAlimentacion.Text;
                    rows["BonoPorCumplimiento"] = this.txtBonoPorCumplimiento.Text;
                    client.TareaSet(tablaTareas);
                    gvTareas.EditIndex = -1;

                    BindDataTareas();
                    limpiarFormulario();
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
        protected void gvTareas_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            //CargarDataTareas();
            try
            {

                Label lblTareaId = (Label)this.gvTareas.Rows[e.RowIndex].FindControl("lblTareaId");
                int idRegistro = Int32.Parse(lblTareaId.Text);
                DataRow rows = tablaTareas.FindByTareaId(idRegistro);
                rows.Delete();
                client.TareaSet(tablaTareas);
                BindDataTareas();
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
            BindDataTareas();

        }
        protected void gvTareas_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            CargarDataTareas();
            this.gvTareas.EditIndex = -1;
            this.gvTareas.DataSource = tablaTareas;
            this.gvTareas.DataBind();

            limpiarFormulario();

        }
        protected void gvTareas_RowEditing(object sender, GridViewEditEventArgs e)
        {
            //CargarDataTareas();
            this.gvTareas.EditIndex = e.NewEditIndex;
            this.gvTareas.DataSource = tablaTareas;
            this.gvTareas.DataBind();

            Label lblTareaTipo = (Label)gvTareas.Rows[e.NewEditIndex].FindControl("lblTareaTipo");
            String tipo = lblTareaTipo.Text;
            this.ddlFrmTareaTipo.SelectedIndex = this.ddlFrmTareaTipo.Items.IndexOf(this.ddlFrmTareaTipo.Items.FindByValue(tipo));

            Label lblTareaEstatus = (Label)gvTareas.Rows[e.NewEditIndex].FindControl("lblTareaEstatus");
            String estatus = lblTareaEstatus.Text;
            this.ddlFrmTareaEstatus.SelectedIndex = this.ddlFrmTareaEstatus.Items.IndexOf(this.ddlFrmTareaEstatus.Items.FindByValue(estatus));

            Label lblCcId = (Label)gvTareas.Rows[e.NewEditIndex].FindControl("lblCcId");
            this.ddlFrmCentroCostos.SelectedIndex = this.ddlFrmCentroCostos.Items.IndexOf(this.ddlFrmCentroCostos.Items.FindByValue(lblCcId.Text));

            Label lblTareaDesc = (Label)gvTareas.Rows[e.NewEditIndex].FindControl("lblTareaDesc");
            Label lblTareaTarifa = (Label)gvTareas.Rows[e.NewEditIndex].FindControl("lblTareaTarifa");

            Label lblValDiaDescanso = (Label)gvTareas.Rows[e.NewEditIndex].FindControl("lblValDiaDescanso");
            Label lblValAlimentacion = (Label)gvTareas.Rows[e.NewEditIndex].FindControl("lblValAlimentacion");
            Label lblBonoPorCumplimiento = (Label)gvTareas.Rows[e.NewEditIndex].FindControl("lblBonoPorCumplimiento");


            this.txtFrmTareaNombre.Text = lblTareaDesc.Text;
            this.txtFrmTareaTarifa.Text = lblTareaTarifa.Text;
            this.txtValDiaDescanso.Text = lblValDiaDescanso.Text;
            this.txtValAlimentacion.Text = lblValAlimentacion.Text;
            this.txtBonoPorCumplimiento.Text = lblBonoPorCumplimiento.Text;
            this.lbtn_AddNewTarea.Visible = false;

        }
        protected void gvTareas_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            //CargarDataTareas();
            this.gvTareas.PageIndex = e.NewPageIndex;
            this.gvTareas.DataSource = tablaTareas;
            this.gvTareas.EditIndex = -1;
            this.gvTareas.DataBind();


        }

        protected void limpiarFormulario()
        {
            this.ddlFrmTareaTipo.SelectedIndex = this.ddlFrmTareaTipo.Items.IndexOf(this.ddlFrmTareaTipo.Items.FindByValue("0"));
            this.ddlFrmTareaEstatus.SelectedIndex = this.ddlFrmTareaEstatus.Items.IndexOf(this.ddlFrmTareaEstatus.Items.FindByValue("1")); ;
            this.ddlFrmCentroCostos.SelectedIndex = this.ddlFrmCentroCostos.Items.IndexOf(this.ddlFrmCentroCostos.Items.FindByValue("0")); ;
            this.txtFrmTareaNombre.Text = "";
            this.txtFrmTareaTarifa.Text = "";
            this.lbtn_AddNewTarea.Visible = true;
            this.gvTareas.EditIndex = -1;
        }
    }
}