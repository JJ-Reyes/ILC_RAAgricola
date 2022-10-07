using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Cabana.Campo.RAAgricola.Pre.Web
{
    public partial class index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Write("<script language='javascript'> window.location.replace('Home.aspx');</" + "script>");
        }
    }
}