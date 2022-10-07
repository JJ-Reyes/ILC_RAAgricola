using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cabana.Campo.RAAgricola.BLL.Cs
{
    public class PL_Quincenas
    {
        public Cabana.Campo.RAAgricola.DAL.Identity.DS_ILC_Campo.PL_QUINCENADataTable GetData(int? idQuincena, int? idEmpresa)
        {
            return new Cabana.Campo.RAAgricola.DAL.DS.DS_ILC_CampoTableAdapters.PL_QUINCENATableAdapter().GetData(idQuincena, idEmpresa);
        }

        public int UpdateData(Cabana.Campo.RAAgricola.DAL.Identity.DS_ILC_Campo.PL_QUINCENADataTable tablaQuincenas)
        {
            return new Cabana.Campo.RAAgricola.DAL.DS.DS_ILC_CampoTableAdapters.PL_QUINCENATableAdapter().Update(tablaQuincenas);
        }


    }
}
