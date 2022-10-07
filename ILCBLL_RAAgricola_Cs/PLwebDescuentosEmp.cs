using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cabana.Campo.RAAgricola.BLL.Cs
{
    public class PLwebDescuentosEmp
    {
        public Cabana.Campo.RAAgricola.DAL.Identity.DS_ILC_Campo.PLwebDescuentosEmpDataTable GetData(int? DescId, int? DescEmprId, int? DescQuinId, int? DescUsuIdIngresa, int? DescEmpId)
        {
            return new Cabana.Campo.RAAgricola.DAL.DS.DS_ILC_CampoTableAdapters.PLwebDescuentosEmpTableAdapter().GetData(DescId, DescEmprId, DescQuinId, DescUsuIdIngresa, DescEmpId);
        }


        public int UpdateData(Cabana.Campo.RAAgricola.DAL.Identity.DS_ILC_Campo.PLwebDescuentosEmpDataTable tablaDescuentosEmp)
        {
            return new Cabana.Campo.RAAgricola.DAL.DS.DS_ILC_CampoTableAdapters.PLwebDescuentosEmpTableAdapter().Update(tablaDescuentosEmp);
        }
    }
}
