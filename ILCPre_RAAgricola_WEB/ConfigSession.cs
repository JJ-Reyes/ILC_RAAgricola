using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Cabana.Campo.RAAgricola.Pre.Web
{
    public class ConfigSession
    {
        public static int admPaginaWeb    = 1, 
                gerenteEmpr     = 2, 
                supervisorEmpr  = 3, 
                planilleroEmpr  = 4, 
                operarioEmpr    = 5;
        public Boolean validarSession(int UsuNivelAcceso, String NombrePagina) 
        {/*
           public static int admPaginaWeb    = 1, 
                gerenteEmpr     = 2, 
                supervisorEmpr  = 3, 
                planilleroEmpr  = 4, 
                operarioEmpr    = 5;
            */
            if (NombrePagina == "Quincenas" && (UsuNivelAcceso == gerenteEmpr || UsuNivelAcceso == supervisorEmpr))
            {
                return true;
            }
            else if (NombrePagina == "Tareas" && (UsuNivelAcceso == gerenteEmpr || UsuNivelAcceso == supervisorEmpr || UsuNivelAcceso == planilleroEmpr))
            {
                return true;
            }
            else if (NombrePagina == "CentrosCostos" && (UsuNivelAcceso == gerenteEmpr || UsuNivelAcceso == supervisorEmpr || UsuNivelAcceso == planilleroEmpr))
            {
                return true;
            }
            else if (NombrePagina == "EmpPorCuadrillas" && (UsuNivelAcceso == gerenteEmpr || UsuNivelAcceso == supervisorEmpr || UsuNivelAcceso == planilleroEmpr))
            {
                return true;
            }
            else if (NombrePagina == "Empleados" && (UsuNivelAcceso == gerenteEmpr || UsuNivelAcceso == supervisorEmpr || UsuNivelAcceso == planilleroEmpr))
            {
                return true;
            }
            else if (NombrePagina == "Home" && (UsuNivelAcceso == admPaginaWeb || UsuNivelAcceso == gerenteEmpr || UsuNivelAcceso == supervisorEmpr || UsuNivelAcceso == planilleroEmpr || UsuNivelAcceso == operarioEmpr))
            {
                return true;
            }
            else if (NombrePagina == "IngresoPlanillas" && (UsuNivelAcceso == gerenteEmpr || UsuNivelAcceso == supervisorEmpr || UsuNivelAcceso == planilleroEmpr || UsuNivelAcceso == operarioEmpr))
            {
                return true;
            }
            else if (NombrePagina == "Descuentos" && (UsuNivelAcceso == gerenteEmpr || UsuNivelAcceso == supervisorEmpr || UsuNivelAcceso == planilleroEmpr || UsuNivelAcceso == operarioEmpr))
            {
                return true;
            }
            else if (NombrePagina == "FincasByEmpresas" && UsuNivelAcceso == admPaginaWeb)
            {
                return true;
            }
            else if (NombrePagina == "EmpresasAdm" && UsuNivelAcceso == admPaginaWeb)
            {
                return true;
            }
            else if (NombrePagina == "UsuariosEmpr" && (UsuNivelAcceso == admPaginaWeb || UsuNivelAcceso == gerenteEmpr))
            {
                return true;
            }
            else if (NombrePagina == "UsuariosToFincas" && UsuNivelAcceso == gerenteEmpr)
            {
                return true;
            }
            else if (NombrePagina == "UsuariosCuadrillas" && UsuNivelAcceso == gerenteEmpr)
            {
                return true;
            }
            else if (NombrePagina == "FrentesCuadrillas" && UsuNivelAcceso == gerenteEmpr)
            {
                return true;
            }
            else
            {
                return false;

            }
        }
    }
}