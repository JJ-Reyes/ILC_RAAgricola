using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace Cabana.Campo.RAAgricola.Pre.Web
{
    public partial class Session : System.Web.UI.Page
    {
        public static Cabana.Campo.RAAgricola.Pre.Web.localhostWs.DS_ILC_Campo.PLwebUsuariosEmprDataTable tablaUsuariosEmpr;

        public static Cabana.Campo.RAAgricola.Pre.Web.localhostWs.WSRAAgricola wsRAAgricola;
        protected void Page_Load(object sender, EventArgs e)
        {

            try
            {
                if (!IsPostBack && !string.IsNullOrEmpty(Request.Form["USUARIO"]) && !string.IsNullOrEmpty(Request.Form["PASS"]))
                {
                    String nombreUsuario = Request.Form["USUARIO"];
                    String passUsuario = Request.Form["PASS"];

                    wsRAAgricola = new Cabana.Campo.RAAgricola.Pre.Web.localhostWs.WSRAAgricola();
                    tablaUsuariosEmpr = new Cabana.Campo.RAAgricola.Pre.Web.localhostWs.DS_ILC_Campo.PLwebUsuariosEmprDataTable();
                    tablaUsuariosEmpr = wsRAAgricola.UsuarioEmprGet(null, nombreUsuario, passUsuario, null, 1, null);
                    if (tablaUsuariosEmpr.Rows.Count == 1)
                    {
                        String UsuNombreCompleto = tablaUsuariosEmpr.Rows[0]["UsuNombreCompleto"].ToString();
                        String UsuNivelAcceso = tablaUsuariosEmpr.Rows[0]["UsuNivelAcceso"].ToString();
                        String UsuId = tablaUsuariosEmpr.Rows[0]["UsuId"].ToString();
                        String UsuEmprId = tablaUsuariosEmpr.Rows[0]["EmprId"].ToString();
                        String UsuCargo = tablaUsuariosEmpr.Rows[0]["UsuCargo"].ToString();
                        String EmprNombre = tablaUsuariosEmpr.Rows[0]["EmprNombre_2"].ToString();

                        Session["UsuNombreCompleto"] = UsuNombreCompleto;
                        Session["UsuNivelAcceso"] = UsuNivelAcceso;
                        Session["UsuId"] = UsuId;
                        Session["EmprId"] = UsuEmprId;
                        Session["UsuCargo"] = UsuCargo;
                        Session["EmprNombre"] = EmprNombre;
                        Session["UsuSesion"] = tablaUsuariosEmpr.Rows[0]["UsuSesion"].ToString();


                        if (UsuNivelAcceso.Equals("1"))// USUARIO ADMINISTRADOR
                        {
                            Response.Write("<script language='javascript'> $('#login').val(''); $('#pass').val(''); window.location.replace('EmpresasAdm.aspx');</" + "script>");

                        }
                        else {
                            Response.Write("<script language='javascript'> $('#login').val(''); $('#pass').val(''); window.location.replace('home.aspx');</" + "script>");

                        }

                    }
                    else
                    {
                        Response.Write("<script language='javascript'>alert('Usuario o Contraseña incorrecto');  $('#pass').val('');</" + "script>");
                        Session.Abandon();

                    }
                }
                else 
                {
                    Response.Write("<script language='javascript'>alert('Acceso Incorrecto'); window.location.replace('Login.aspx'); </" + "script>");
                    Session.Abandon();
                
                }
                
            }
            catch (System.Net.WebException ee)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('A5050 No es posible conectar con el servidor')", true);

            }


            //String variableCs = "ejemplo";
            //Session.Abandon();
            //Response.Write("done");
            //Response.Redirect("WebForm1.aspx");
        }
    }
}