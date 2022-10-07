using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cabana.Campo.RAAgricola.BLL.Cs
{
    public class PL_Insumos
    {
        
        public Cabana.Campo.RAAgricola.DAL.Identity.DS_ILC_Campo.PL_InsumosDataTable GetData(int? idInsumo, int? idEmpresa)
        {
            return new Cabana.Campo.RAAgricola.DAL.DS.DS_ILC_CampoTableAdapters.PL_InsumosTableAdapter().GetData(idInsumo, idEmpresa); 
 
        }

        public int UpdateData(Cabana.Campo.RAAgricola.DAL.Identity.DS_ILC_Campo.PL_InsumosDataTable tablaInsumos )
        {
            return new Cabana.Campo.RAAgricola.DAL.DS.DS_ILC_CampoTableAdapters.PL_InsumosTableAdapter().Update(tablaInsumos);
        }

        public int UpdateRow(int idInsumo)
        {
            return new Cabana.Campo.RAAgricola.DAL.DS.DS_ILC_CampoTableAdapters.PL_InsumosTableAdapter().Sub_PL_InsumosDeleteById(idInsumo);

        }

    }
}
