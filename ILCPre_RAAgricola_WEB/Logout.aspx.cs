using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Cabana.Campo.RAAgricola.Pre.Web
{
    public partial class Logout : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //String variableCs = "ejemplo";
            Session.Abandon();
            //Response.Write("done");
            //Response.Redirect("WebForm1.aspx");
            //Response.Write("<script language='javascript'>alert('Especifique Usuario y Contraseña'); $('#login').val('" + variableCs + "')</" + "script>");

            Response.Write("<script language='javascript'> window.location.replace('Login.aspx');</" + "script>");

        }
    }
}