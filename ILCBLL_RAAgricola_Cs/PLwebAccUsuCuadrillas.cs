using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cabana.Campo.RAAgricola.BLL.Cs
{
    public class PLwebAccUsuCuadrillas
    {
        public Cabana.Campo.RAAgricola.DAL.Identity.DS_ILC_Campo.PLwebAccUsuCuadrillasDataTable GetData(int? AcCuaId, int? EmprId, int? UsuId,int? CuaId)
        {
            return new Cabana.Campo.RAAgricola.DAL.DS.DS_ILC_CampoTableAdapters.PLwebAccUsuCuadrillasTableAdapter().GetData(AcCuaId, EmprId, UsuId, CuaId);
        }

        public int UpdateData(Cabana.Campo.RAAgricola.DAL.Identity.DS_ILC_Campo.PLwebAccUsuCuadrillasDataTable tablaUsuariosByCuadrillas)
        {
            return new Cabana.Campo.RAAgricola.DAL.DS.DS_ILC_CampoTableAdapters.PLwebAccUsuCuadrillasTableAdapter().Update(tablaUsuariosByCuadrillas);
        }
    }
}
