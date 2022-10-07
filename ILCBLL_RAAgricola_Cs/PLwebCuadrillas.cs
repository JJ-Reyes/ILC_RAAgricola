using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cabana.Campo.RAAgricola.BLL.Cs
{
    public class PLwebCuadrillas
    {
        public Cabana.Campo.RAAgricola.DAL.Identity.DS_ILC_Campo.PLwebCuadrillasDataTable GetData(int? FrenteId, int? CuaId, int? CuaEstatus, int? EmprId, int? UsuId)
        {
            return new Cabana.Campo.RAAgricola.DAL.DS.DS_ILC_CampoTableAdapters.PLwebCuadrillasTableAdapter().GetData(FrenteId, CuaId, CuaEstatus, EmprId, UsuId);
        }

        public int UpdateData(Cabana.Campo.RAAgricola.DAL.Identity.DS_ILC_Campo.PLwebCuadrillasDataTable tablaCuadrillas)
        {
            return new Cabana.Campo.RAAgricola.DAL.DS.DS_ILC_CampoTableAdapters.PLwebCuadrillasTableAdapter().Update(tablaCuadrillas);
        }
    }
}
