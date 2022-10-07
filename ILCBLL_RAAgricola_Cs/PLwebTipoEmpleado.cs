using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cabana.Campo.RAAgricola.BLL.Cs
{
    public class PLwebTipoEmpleado
    {
        public Cabana.Campo.RAAgricola.DAL.Identity.DS_ILC_Campo.PLwebTipoEmpleadoDataTable GetData(int? TipoId, int? EmprId)
        {
            return new Cabana.Campo.RAAgricola.DAL.DS.DS_ILC_CampoTableAdapters.PLwebTipoEmpleadoTableAdapter().GetData(TipoId, EmprId);
        }

        public int UpdateData(Cabana.Campo.RAAgricola.DAL.Identity.DS_ILC_Campo.PLwebTipoEmpleadoDataTable tablaTipoEmp)
        {
            return new Cabana.Campo.RAAgricola.DAL.DS.DS_ILC_CampoTableAdapters.PLwebTipoEmpleadoTableAdapter().Update(tablaTipoEmp);
        }
    }
}
