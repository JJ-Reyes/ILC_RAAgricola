using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace Cabana.Campo.RAAgricola.Pre.Web
{

    public partial class Quincenas : System.Web.UI.Page
    {
        public static Cabana.Campo.RAAgricola.Pre.Web.localhostWs.DS_ILC_Campo.PLwebQuincenasDataTable tabla;
        public static Cabana.Campo.RAAgricola.Pre.Web.localhostWs.WSRAAgricola client;
        ConfigSession configSession = new ConfigSession();
        //int EmprId;
        //int UsuId;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Session["UsuId"] as string))
            {
                if (this.HddEmprId.Value == "")
                {
                    this.HddUsuId.Value = "" + Session["UsuId"];
                    this.HddEmprId.Value = "" + Session["EmprId"];
                    this.HddUsuNivelAcceso.Value = "" + Session["UsuNivelAcceso"];
                }
                if (!configSession.validarSession(getUsuNivelAcceso(), "Quincenas"))
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
                BindData();
            }
        }

        protected void ddl_Zafra_Change(object sender, EventArgs e)
        {
            BindData();
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
        protected void BindData()
        {

            client = new Cabana.Campo.RAAgricola.Pre.Web.localhostWs.WSRAAgricola();

            tabla = new Cabana.Campo.RAAgricola.Pre.Web.localhostWs.DS_ILC_Campo.PLwebQuincenasDataTable();
            //tabla = client.QuincenaGet(null, getEmprId(), null, this.ddlFrmZafra.SelectedValue.ToString());
            CargarQuincenas();


            int i = 0;
            String d;
            foreach (DataRow dr in tabla.Rows)
            {
                d = ((DateTime)dr["QuinFechaDesde"]).ToString("dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture);
                tabla.Rows[i]["QuinFechaDesde"] = d;
                i++;
            }
            this.gvEmployeeDetails.DataSource = tabla;

            gvEmployeeDetails.DataBind();


        }

        public void CargarQuincenas() {
            tabla = client.QuincenaGet(null, getEmprId(), null, this.ddlFrmZafra.SelectedValue.ToString());
        }

        protected void LimpiarTablaQuincenas()
        {
            CargarQuincenas();
            gvEmployeeDetails.DataSource = tabla;
            gvEmployeeDetails.DataBind();
        }

        protected void gvEmployeeDetails_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            CargarQuincenas();
            if (e.CommandName.Equals("ADD"))
            {
                TextBox txtAddInsId = (TextBox)gvEmployeeDetails.FooterRow.FindControl("txtAddInsId");
                TextBox txtAddInsNombre = (TextBox)gvEmployeeDetails.FooterRow.FindControl("txtAddInsNombre");
                TextBox txtAddInsCodigo = (TextBox)gvEmployeeDetails.FooterRow.FindControl("txtAddInsCodigo");
                TextBox txtAddInsPrecio = (TextBox)gvEmployeeDetails.FooterRow.FindControl("txtAddInsPrecio");

                try
                {
                    
                    DataRow row = tabla.NewRow();

                    if (tabla.Rows.Count > 0)
                    {
                        row["InsId"] = "0";
                        row["InsNombre"] = txtAddInsNombre.Text;
                        row["InsCodigo"] = txtAddInsCodigo.Text;
                        row["InsPrecio"] = txtAddInsPrecio.Text;
                        row["EmprId"]    = getEmprId();

                        tabla.Rows.Add(row);

                        client.QuincenaSet(tabla);

                        gvEmployeeDetails.EditIndex = -1;

                        BindData();
                    }
                }
                catch (System.IO.IOException er)
                {
                    Console.WriteLine("Error en el insert", er);
                }


            }
            else if (e.CommandName.Equals("EstatusById"))
            {

                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = gvEmployeeDetails.Rows[index];

                Label lblQuinEstatus = (Label)row.FindControl("lblQuinEstatus");
                Label lblQuincenaId = (Label)row.FindControl("lblQuincenaId");
                int idRegistro = Int32.Parse(lblQuincenaId.Text);

                if (lblQuinEstatus.Text == "1")
                {

                    if (tabla.Rows.Count > 0)
                    {

                        DataRow rows = tabla.FindByQuinId(idRegistro);

                        rows["QuinEstatus"] = "0";
                        if (client.QuincenaSet(tabla) > 0)
                        {
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Estatus cambiado exitosamente')", true);

                        }
                        BindData();
                        //gvEmployeeDetails.DataSource = tabla;
                        //gvEmployeeDetails.DataBind();
                    }

                }
                else
                    if (lblQuinEstatus.Text == "0")
                    {
                        if (tabla.Rows.Count > 0)
                        {

                            DataRow rows = tabla.FindByQuinId(idRegistro);

                            rows["QuinEstatus"] = "1";

                            if (client.QuincenaSet(tabla) > 0)
                            {
                                //imagen.ImageUrl = "open.png";
                                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Estatus cambiado exitosamente')", true);

                            }
                            BindData();

                        }


                    }


            }

        }


        protected void gvEmployeeDetails_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            CargarQuincenas();
            Label lblEditQuincenaId = (Label)gvEmployeeDetails.Rows[e.RowIndex].FindControl("lblEditQuincenaId");
            TextBox txtEditFECHADESDE = (TextBox)gvEmployeeDetails.Rows[e.RowIndex].FindControl("txtEditFECHADESDE");
            TextBox txtEditFECHAHASTA = (TextBox)gvEmployeeDetails.Rows[e.RowIndex].FindControl("txtEditFECHAHASTA");

            try
            {

                int idRegistro = Int32.Parse(lblEditQuincenaId.Text);
                if (tabla.Rows.Count > 0)
                {

                    DataRow rows = tabla.FindByQuinId(idRegistro);

                    rows["QuinFechaDesde"] = txtEditFECHADESDE.Text;
                    rows["QuinFechaHasta"] = txtEditFECHAHASTA.Text;

                    client.QuincenaSet(tabla);

                    gvEmployeeDetails.EditIndex = -1;


                    BindData();
                }
            }
            catch (System.IO.IOException er)
            {
                Console.WriteLine("Error en el insert", er);
            }
        }

        protected void gvEmployeeDetails_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            CargarQuincenas();
            try
            {

                Label lblQuincenaId = (Label)gvEmployeeDetails.Rows[e.RowIndex].FindControl("lblQuincenaId");
                int idRegistro = Int32.Parse(lblQuincenaId.Text);
                DataRow rows = tabla.FindByQuinId(idRegistro);
                rows.Delete();
                client.QuincenaSet(tabla);

                //BindData();
                Session["mesajeAccion"] = "Registro eliminado con exito";
            }
            catch (System.Exception error)
            {

                if (error.ToString().Contains("REFERENCE"))
                    Session["mesajeAccion"] = "Este registro ya tiene datos vinculados.";
                else
                    Session["mesajeAccion"] = "Informar de este error a soporte tecnico";

            }
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + Session["mesajeAccion"].ToString() + "')", true);
            BindData();
        }


        protected void gvEmployeeDetails_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            CargarQuincenas();
            gvEmployeeDetails.EditIndex = -1;
            gvEmployeeDetails.DataSource = tabla;
            gvEmployeeDetails.DataBind();
        }
        protected void gvEmployeeDetails_RowEditing(object sender, GridViewEditEventArgs e)
        {
            CargarQuincenas();
            gvEmployeeDetails.EditIndex = e.NewEditIndex;
            gvEmployeeDetails.DataSource = tabla;
            gvEmployeeDetails.DataBind();

        }

        protected void btnAgregarQuincena_Click(object sender, EventArgs e)
        {
            CargarQuincenas();
            if (!this.ddlFrmZafra.SelectedValue.Equals("0"))
            {
                try
                {
                    DataRow row = tabla.NewRow();

                    row["QuinFechaDesde"] = DateTime.ParseExact(this.txtFechaInicial.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture);
                    row["QuinFechaHasta"] = DateTime.ParseExact(this.txtFechaFinal.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture);
                    row["QuinEstatus"] = 1;
                    row["QuinZafra"] = this.ddlFrmZafra.SelectedValue;
                    row["EmprId"] = getEmprId();

                    tabla.Rows.Add(row);

                    client.QuincenaSet(tabla);

                    gvEmployeeDetails.EditIndex = -1;

                    BindData();
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Quincena creada exitosamente')", true);


                }
                catch (System.Exception er)
                {
                    Console.WriteLine("Error al insertar la quincena", er);
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('No se pudo crear la quincena')", true);

                }
            }
            else {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Seleccione una Zafra')", true);
        
            }


        }


        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            CargarQuincenas();
            this.gvEmployeeDetails.PageIndex = e.NewPageIndex;
            gvEmployeeDetails.DataSource = tabla;
            gvEmployeeDetails.EditIndex = -1;
            this.gvEmployeeDetails.DataBind();
        }


        protected void gvQuincenas_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                Label lblQuinEstatus = (Label)e.Row.FindControl("lblQuinEstatus");

                System.Web.UI.WebControls.Image imagen = (System.Web.UI.WebControls.Image)e.Row.FindControl("imgestado");

                if (lblQuinEstatus.Text == "1")
                {
                    imagen.ImageUrl = "open.png";

                }
                if (lblQuinEstatus.Text == "0")
                {
                    imagen.ImageUrl = "close.png";

                }
                //gvEmployeeDetails.Rows.Count > 0 &&   
                /*if (l != null) l.Text = country.Name;
                }
                }*/
                //e.Row.Cells[1].Text = "<i>" + e.Row.Cells[1].Text + "</i>";

            }
        }
        protected void imgestado_OnClick(object sender, EventArgs e)
        {

        }




    }
}