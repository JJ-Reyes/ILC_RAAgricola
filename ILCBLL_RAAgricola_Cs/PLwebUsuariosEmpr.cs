using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cabana.Campo.RAAgricola.BLL.Cs
{
    public class PLwebUsuariosEmpr
    {
        public Cabana.Campo.RAAgricola.DAL.Identity.DS_ILC_Campo.PLwebUsuariosEmprDataTable GetData(int? UsuId, String UsuNombre, String UsuPass, int? EmprId, int? UsuEstatus, int? UsuNivelAcceso)
        {
            return new Cabana.Campo.RAAgricola.DAL.DS.DS_ILC_CampoTableAdapters.PLwebUsuariosEmprTableAdapter().GetData(UsuId, UsuNombre, UsuPass, EmprId, UsuEstatus, UsuNivelAcceso);
        }

        public int UpdateData(Cabana.Campo.RAAgricola.DAL.Identity.DS_ILC_Campo.PLwebUsuariosEmprDataTable tablaUsuariosEmpr)
        {
            return new Cabana.Campo.RAAgricola.DAL.DS.DS_ILC_CampoTableAdapters.PLwebUsuariosEmprTableAdapter().Update(tablaUsuariosEmpr);
        }
    }
}
