using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cabana.Campo.RAAgricola.BLL.Cs
{
    public class PLwebQuincenas
    {
        public Cabana.Campo.RAAgricola.DAL.Identity.DS_ILC_Campo.PLwebQuincenasDataTable GetData(int? QuinId, int? EmprId, int? QuinEstatus, String Zafra)
        {
            return new Cabana.Campo.RAAgricola.DAL.DS.DS_ILC_CampoTableAdapters.PLwebQuincenasTableAdapter().GetData(QuinId, EmprId, QuinEstatus, Zafra);
        }

        public int UpdateData(Cabana.Campo.RAAgricola.DAL.Identity.DS_ILC_Campo.PLwebQuincenasDataTable tablaQuincenas)
        {
            return new Cabana.Campo.RAAgricola.DAL.DS.DS_ILC_CampoTableAdapters.PLwebQuincenasTableAdapter().Update(tablaQuincenas);
        }
    }
}
