using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Cabana.Campo.RAAgricola.Pre.Web
{
    public partial class InicioAdm : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void onclick_pwd(object sender, EventArgs e)
        {
            if (this.hyperlink1.Visible == true)
                hyperlink1.Visible = true;
            else
                hyperlink1.Visible = false;
        }
    }
}