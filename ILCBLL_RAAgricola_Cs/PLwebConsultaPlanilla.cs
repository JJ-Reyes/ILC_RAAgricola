using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cabana.Campo.RAAgricola.BLL.Cs
{
    public class PLwebConsultaPlanilla
    {
        public Cabana.Campo.RAAgricola.DAL.Identity.DS_ILC_Campo.Sub_PLwebConsultaPlanillaDataTable GetData(String dato1,String dato2,String dato3,String dato4)
        {
            return new Cabana.Campo.RAAgricola.DAL.DS.DS_ILC_CampoTableAdapters.Sub_PLwebConsultaPlanillaTableAdapter().GetData( dato1, dato2, dato3, dato4);
        }
    }
}
