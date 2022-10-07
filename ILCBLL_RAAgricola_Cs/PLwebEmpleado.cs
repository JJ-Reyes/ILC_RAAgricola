using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cabana.Campo.RAAgricola.BLL.Cs
{
    public class PLwebEmpleado
    {
        public Cabana.Campo.RAAgricola.DAL.Identity.DS_ILC_Campo.PLwebEmpleadoDataTable GetData(int? EmpId, int? EmpTipo, int? EmpFrente, int? EmpCuadrilla, String EmpNombres, int? EmprId, String EmpEstatus)
        {
            return new Cabana.Campo.RAAgricola.DAL.DS.DS_ILC_CampoTableAdapters.PLwebEmpleadoTableAdapter().GetData(EmpId, EmpTipo, EmpFrente, EmpCuadrilla, EmpNombres, EmprId, EmpEstatus);
        }

        public int UpdateData(Cabana.Campo.RAAgricola.DAL.Identity.DS_ILC_Campo.PLwebEmpleadoDataTable tablaEmpleados)
        {
            return new Cabana.Campo.RAAgricola.DAL.DS.DS_ILC_CampoTableAdapters.PLwebEmpleadoTableAdapter().Update(tablaEmpleados);
        }
    }
}
