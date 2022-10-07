using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cabana.Campo.RAAgricola.BLL.Cs
{
    public class VPLwebLotesProveedor
    {
        public Cabana.Campo.RAAgricola.DAL.Identity.DS_ILC_Campo.VPLwebLotesProveedorDataTable GetData(int? EmprId, String codLote, String codFinca, String codProv, String tipoConsulta, int? idLote, String nomProv)
        {
            return new Cabana.Campo.RAAgricola.DAL.DS.DS_ILC_CampoTableAdapters.VPLwebLotesProveedorTableAdapter().GetData(EmprId, codLote, codFinca, codProv, tipoConsulta, idLote, nomProv);
        }
        /*
        public Cabana.Campo.RAAgricola.DAL.Identity.DS_ILC_Campo.VPLwebLotesProveedorDataTable GetDataProveedores(String codProv, int? EmprId)
        {
            return new Cabana.Campo.RAAgricola.DAL.DS.DS_ILC_CampoTableAdapters.VPLwebLotesProveedorTableAdapter().GetDataProveedores(codProv, EmprId);
        }
         * */

    }
}