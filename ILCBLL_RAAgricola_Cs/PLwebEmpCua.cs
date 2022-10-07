using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cabana.Campo.RAAgricola.BLL.Cs
{
    public class PLwebEmpCua
    {

        public Cabana.Campo.RAAgricola.DAL.Identity.DS_ILC_Campo.PLwebEmpCuaDataTable GetData(int? UsuId, int? EmprId)
        {
            return new Cabana.Campo.RAAgricola.DAL.DS.DS_ILC_CampoTableAdapters.PLwebEmpCuaTableAdapter().GetData(UsuId, EmprId);
        }

        public int UpdateData(Cabana.Campo.RAAgricola.DAL.Identity.DS_ILC_Campo.PLwebEmpCuaDataTable tablaPLwebEmpCua)
        {
            return new Cabana.Campo.RAAgricola.DAL.DS.DS_ILC_CampoTableAdapters.PLwebEmpCuaTableAdapter().Update(tablaPLwebEmpCua);
        }
    }
}
