using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cabana.Campo.RAAgricola.BLL.Cs
{
    public class PLwebFrentes
    {
        public Cabana.Campo.RAAgricola.DAL.Identity.DS_ILC_Campo.PLwebFrentesDataTable GetData(int? FrenteId, int? EmprId, int? FrenteEstatus, String FrenteNombre)
        {
            return new Cabana.Campo.RAAgricola.DAL.DS.DS_ILC_CampoTableAdapters.PLwebFrentesTableAdapter().GetData(FrenteId, EmprId, FrenteEstatus, FrenteNombre);
        }

        public int UpdateData(Cabana.Campo.RAAgricola.DAL.Identity.DS_ILC_Campo.PLwebFrentesDataTable tablaFrentes)
        {
            return new Cabana.Campo.RAAgricola.DAL.DS.DS_ILC_CampoTableAdapters.PLwebFrentesTableAdapter().Update(tablaFrentes);
        }
    }
}
