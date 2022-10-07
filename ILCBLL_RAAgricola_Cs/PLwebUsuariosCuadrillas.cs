using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cabana.Campo.RAAgricola.BLL.Cs
{
    public class PLwebUsuariosCuadrillas
    {
        public Cabana.Campo.RAAgricola.DAL.Identity.DS_ILC_Campo.PLwebUsuariosCuadrillasDataTable GetData(int? ManId, int? UsuId, int? CuaId)
        {
            return new Cabana.Campo.RAAgricola.DAL.DS.DS_ILC_CampoTableAdapters.PLwebUsuariosCuadrillasTableAdapter().GetData(ManId, UsuId, CuaId);
        }

        public int UpdateData(Cabana.Campo.RAAgricola.DAL.Identity.DS_ILC_Campo.PLwebUsuariosCuadrillasDataTable tablaUsuariosCuadrillas)
        {
            return new Cabana.Campo.RAAgricola.DAL.DS.DS_ILC_CampoTableAdapters.PLwebUsuariosCuadrillasTableAdapter().Update(tablaUsuariosCuadrillas);
        }
    }
}
