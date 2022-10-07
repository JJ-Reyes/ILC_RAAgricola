using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cabana.Campo.RAAgricola.BLL.Cs
{
    public class VPLwebLotesProv
    {
        public Cabana.Campo.RAAgricola.DAL.Identity.DS_ILC_Campo.VPLwebLotesProvDataTable GetData(String gpId, int? FincaId, int? LoteId)
        {
            return new Cabana.Campo.RAAgricola.DAL.DS.DS_ILC_CampoTableAdapters.VPLwebLotesProvTableAdapter().GetData(gpId, FincaId, LoteId);
        }

        public Cabana.Campo.RAAgricola.DAL.Identity.DS_ILC_Campo.VPLwebLotesProvDataTable GetDataByUsuId(String gpId, int? FincaId, int? LoteId, int? UsuId, int? EmprId)
        {
            return new Cabana.Campo.RAAgricola.DAL.DS.DS_ILC_CampoTableAdapters.VPLwebLotesProvTableAdapter().GetDataByUsuId(gpId, FincaId, LoteId, UsuId, EmprId);
        }

        public Cabana.Campo.RAAgricola.DAL.Identity.DS_ILC_Campo.VPLwebLotesProvDataTable GetDataByEmprId(String gpId, int? FincaId, int? LoteId)
        {
            return new Cabana.Campo.RAAgricola.DAL.DS.DS_ILC_CampoTableAdapters.VPLwebLotesProvTableAdapter().GetDataByEmprId(gpId, FincaId, LoteId);
        }
        
    }
}
