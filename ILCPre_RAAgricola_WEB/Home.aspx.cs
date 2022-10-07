using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Cabana.Campo.RAAgricola.Pre.Web
{
    public partial class Home : System.Web.UI.Page
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
                    this.HddUsuNivelAcceso.Value = "" + Session["UsuNivelAcceso"]; // Int32.Parse(
                }
                if (!configSession.validarSession(getUsuNivelAcceso(), "Home"))//NumeroDeAcceso, NombrePagina
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

        protected void BindData()
        {
            client = new Cabana.Campo.RAAgricola.Pre.Web.localhostWs.WSRAAgricola();

            tabla = new Cabana.Campo.RAAgricola.Pre.Web.localhostWs.DS_ILC_Campo.PLwebQuincenasDataTable();
            tabla = client.QuincenaGet(null, getEmprId(), 1,"");
            //tabla = client.QuincenaGet(null, EmprId, 1, "");
            //tabla.Columns["QuinFechaDesde"].Convertir(val => DateTime.Parse(val.ToString()).ToString("dd/MMM/yyyy"));
            gvQuincenas.DataSource = tabla;
            gvQuincenas.DataBind();
        }

        protected void gvQuincenas_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            if (e.CommandName.Equals("ADD"))
            {

                try
                {

                }
                catch (System.IO.IOException er)
                {
                    Console.WriteLine("Error en el insert", er);
                }


            }
        }
        protected void gvQuincenas_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvQuincenas.PageIndex = e.NewPageIndex;
            gvQuincenas.DataSource = tabla;
            gvQuincenas.EditIndex = -1;
            this.gvQuincenas.DataBind();
        }

    }


}