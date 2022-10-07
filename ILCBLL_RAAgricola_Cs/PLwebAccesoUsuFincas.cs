using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cabana.Campo.RAAgricola.BLL.Cs
{
    public class PLwebAccesoUsuFincas
    {
        public Cabana.Campo.RAAgricola.DAL.Identity.DS_ILC_Campo.PLwebAccesoUsuFincasDataTable GetData(int? AccUsuId,int? UsuId,int? AccEmprId,int? EmprId)
        {
            return new Cabana.Campo.RAAgricola.DAL.DS.DS_ILC_CampoTableAdapters.PLwebAccesoUsuFincasTableAdapter().GetData(AccUsuId, UsuId, AccEmprId, EmprId);
        }

        public int UpdateData(Cabana.Campo.RAAgricola.DAL.Identity.DS_ILC_Campo.PLwebAccesoUsuFincasDataTable tablaUsuariosByFincas)
        {
            return new Cabana.Campo.RAAgricola.DAL.DS.DS_ILC_CampoTableAdapters.PLwebAccesoUsuFincasTableAdapter().Update(tablaUsuariosByFincas);
        }
    }
}
