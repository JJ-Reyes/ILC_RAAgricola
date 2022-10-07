using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cabana.Campo.RAAgricola.BLL.Cs
{
    public class PLwebMoviPlani
    {
        public Cabana.Campo.RAAgricola.DAL.Identity.DS_ILC_Campo.PLwebMoviPlaniDataTable GetData( int? MovId, int? LoteId, int? EmprId, int? MovCierreDiario, int? QuincenaId, int? MovEstadoPlan, int? EmpId, String MovZafra, int? TareaId, DateTime MovFecha, String MovUsuarioIngresa, int? CcosId)
        {
            return new Cabana.Campo.RAAgricola.DAL.DS.DS_ILC_CampoTableAdapters.PLwebMoviPlaniTableAdapter().GetData(MovId, LoteId, EmprId, MovCierreDiario, QuincenaId, MovEstadoPlan, EmpId, MovZafra, TareaId, MovFecha, MovUsuarioIngresa);
        }


        public int UpdateData(Cabana.Campo.RAAgricola.DAL.Identity.DS_ILC_Campo.PLwebMoviPlaniDataTable tablaMoviPlani)
        {
            return new Cabana.Campo.RAAgricola.DAL.DS.DS_ILC_CampoTableAdapters.PLwebMoviPlaniTableAdapter().Update(tablaMoviPlani);
        }

    }
}
