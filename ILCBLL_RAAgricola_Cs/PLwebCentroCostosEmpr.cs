using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cabana.Campo.RAAgricola.BLL.Cs
{
    public class PLwebCentroCostosEmpr
    {
        public Cabana.Campo.RAAgricola.DAL.Identity.DS_ILC_Campo.PLwebCentroCostosEmprDataTable GetData(int? CcosId, int? CcEmprId)
        {
            return new Cabana.Campo.RAAgricola.DAL.DS.DS_ILC_CampoTableAdapters.PLwebCentroCostosEmprTableAdapter().GetData(CcosId, CcEmprId);
        }

        public int UpdateData(Cabana.Campo.RAAgricola.DAL.Identity.DS_ILC_Campo.PLwebCentroCostosEmprDataTable tablaCentroCostosEmpr)
        {
            return new Cabana.Campo.RAAgricola.DAL.DS.DS_ILC_CampoTableAdapters.PLwebCentroCostosEmprTableAdapter().Update(tablaCentroCostosEmpr);
        }
    }
}
