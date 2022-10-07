using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cabana.Campo.RAAgricola.BLL.Cs
{
    public class PLwebAccesoEmprFincas
    {
        public Cabana.Campo.RAAgricola.DAL.Identity.DS_ILC_Campo.PLwebAccesoEmprFincasDataTable GetData(int? AccEmprId, int? EmprId)
        {
            return new Cabana.Campo.RAAgricola.DAL.DS.DS_ILC_CampoTableAdapters.PLwebAccesoEmprFincasTableAdapter().GetData(AccEmprId, EmprId);
        }

        public int UpdateData(Cabana.Campo.RAAgricola.DAL.Identity.DS_ILC_Campo.PLwebAccesoEmprFincasDataTable tablaFincasByEmpresas)
        {
            return new Cabana.Campo.RAAgricola.DAL.DS.DS_ILC_CampoTableAdapters.PLwebAccesoEmprFincasTableAdapter().Update(tablaFincasByEmpresas);
        }

        public object InsertAllFincas(int? UsuAdmIdCrea, String CodProv, int? EmprId )
        {
            return new Cabana.Campo.RAAgricola.DAL.DS.DS_ILC_CampoTableAdapters.PLwebAccesoEmprFincasTableAdapter().Sub_PLwebAccesoEmprFincasInsertAll(UsuAdmIdCrea, CodProv, EmprId);
        }
    }
}
