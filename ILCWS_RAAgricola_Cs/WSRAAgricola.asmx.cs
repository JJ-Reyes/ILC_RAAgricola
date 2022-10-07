using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Script.Services;
using System.Web.Script.Serialization;
using Cabana.Campo.RAAgricola.DAL.Identity;
using Cabana.Campo.RAAgricola;
using Cabana.Campo.RAAgricola.WS.ServiceRefNav2016;
//using Cabana.Campo.RAAgricola.WS.ServiceRefNav2018;
using Cabana.Campo.RAAgricola.WS.RefInterfaceEAM;
using Cabana.Campo.RAAgricola.WS.WebReference;

using System.Net;
using System.Net.Mail;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using Cabana.Campo.RAAgricola.DAL.DS;
using Cabana.Campo.RAAgricola.DAL.DS.DS_SAIPLUSTableAdapters;
using System.Data.SqlClient;

namespace WebApplication2//Cabana.Campo.RAAgricola.WS
{
    /// <summary>
    /// Descripción breve de WSRAAgricola
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
    // [System.Web.Script.Services.ScriptService]
    public class WSRAAgricola : System.Web.Services.WebService
    {

        [WebMethod]
        public DS_ILC_Campo.PLwebQuincenasDataTable QuincenaGet(int? QuinId, int? EmprId, int? QuinEstatus, String Zafra)
        {
            return new Cabana.Campo.RAAgricola.BLL.Cs.PLwebQuincenas().GetData(QuinId, EmprId, QuinEstatus, Zafra);
        }
        [WebMethod]
        public int QuincenaSet(Cabana.Campo.RAAgricola.DAL.Identity.DS_ILC_Campo.PLwebQuincenasDataTable tabla)
        {
            return new Cabana.Campo.RAAgricola.BLL.Cs.PLwebQuincenas().UpdateData(tabla);
        }
        //Metodos servicios web Insumos

        [WebMethod]
        public DS_ILC_Campo.PL_InsumosDataTable InsumosGet(int? idInsumo, int? idEmpresa)
        {
            return new Cabana.Campo.RAAgricola.BLL.Cs.PL_Insumos().GetData(idInsumo, idEmpresa);
        }
        [WebMethod]
        public int InsumosSet(Cabana.Campo.RAAgricola.DAL.Identity.DS_ILC_Campo.PL_InsumosDataTable tabla)
        {
            return new Cabana.Campo.RAAgricola.BLL.Cs.PL_Insumos().UpdateData(tabla);
        }

        //Metodos Empleados

        [WebMethod]
        public DS_ILC_Campo.PLwebEmpleadoDataTable EmpleadoGet(int? EmpId, int? EmpTipo, int? EmpFrente, int? EmpCuadrilla, String EmpNombres, int? EmprId, String EmpEstatus)
        {
            return new Cabana.Campo.RAAgricola.BLL.Cs.PLwebEmpleado().GetData(EmpId, EmpTipo, EmpFrente, EmpCuadrilla, EmpNombres, EmprId, EmpEstatus);
        }
        [WebMethod]
        public int EmpleadoSet(Cabana.Campo.RAAgricola.DAL.Identity.DS_ILC_Campo.PLwebEmpleadoDataTable tabla)
        {
            return new Cabana.Campo.RAAgricola.BLL.Cs.PLwebEmpleado().UpdateData(tabla);
        }

        // Metodos Proveedores

        [WebMethod]
        public DS_ILC_Campo.VPLwebLotesProveedorDataTable FincasLotesGet(int? EmprId, String codLote, String codFinca, String codProv, String tipoConsulta, int? idLote,String nomProv)
        {
            return new Cabana.Campo.RAAgricola.BLL.Cs.VPLwebLotesProveedor().GetData(EmprId, codLote, codFinca, codProv, tipoConsulta, idLote, nomProv);
        }
        /*
        [WebMethod]
        public DS_ILC_Campo.VPLwebLotesProveedorDataTable ProveedoresGet(String codProv ,int? EmprId)
        {
            return new Cabana.Campo.RAAgricola.BLL.Cs.VPLwebLotesProveedor().GetDataProveedores(codProv, EmprId);
        }
        */
        //Metodos Frentes

        [WebMethod]
        public DS_ILC_Campo.PLwebFrentesDataTable FrenteGet(int? FrenteId, int? EmprId, int? FrenteEstatus, String FrenteNombre)
        {
            return new Cabana.Campo.RAAgricola.BLL.Cs.PLwebFrentes().GetData(FrenteId, EmprId, FrenteEstatus, FrenteNombre);
        }
        [WebMethod]
        public int FrenteSet(Cabana.Campo.RAAgricola.DAL.Identity.DS_ILC_Campo.PLwebFrentesDataTable tabla)
        {
            return new Cabana.Campo.RAAgricola.BLL.Cs.PLwebFrentes().UpdateData(tabla);
        }

        //Metodos Cuadrillas

        [WebMethod]
        public DS_ILC_Campo.PLwebCuadrillasDataTable CuadrillaGet(int? FrenteId, int? CuaId, int? CuaEstatus, int? EmprId, int? UsuId)
        {
            return new Cabana.Campo.RAAgricola.BLL.Cs.PLwebCuadrillas().GetData(FrenteId, CuaId, CuaEstatus, EmprId, UsuId);
        }
        [WebMethod]
        public int CuadrillaSet(Cabana.Campo.RAAgricola.DAL.Identity.DS_ILC_Campo.PLwebCuadrillasDataTable tabla)
        {
            return new Cabana.Campo.RAAgricola.BLL.Cs.PLwebCuadrillas().UpdateData(tabla);
        }

        //Metodos Tareas

        [WebMethod]
        public DS_ILC_Campo.PLwebTareasDataTable TareaGet(int? TareaId, String TareaDesc, int? TareaEstatus, int? EmprId)
        {
            return new Cabana.Campo.RAAgricola.BLL.Cs.PLwebTareas().GetData(TareaId, TareaDesc, TareaEstatus, EmprId);
        }
        [WebMethod]
        public int TareaSet(Cabana.Campo.RAAgricola.DAL.Identity.DS_ILC_Campo.PLwebTareasDataTable tabla)
        {
            return new Cabana.Campo.RAAgricola.BLL.Cs.PLwebTareas().UpdateData(tabla);
        }

        //Metodos para Movimiento Planilla

        [WebMethod]
        public DS_ILC_Campo.PLwebMoviPlaniDataTable MoviPlaniGet(int? MovId, int? LoteId, int? EmprId, int? MovCierreDiario, int? QuincenaId, int? MovEstadoPlan, int? EmpId, String MovZafra, int? TareaId, DateTime MovFecha, String MovUsuarioIngresa, int? CcostId)
        {
            return new Cabana.Campo.RAAgricola.BLL.Cs.PLwebMoviPlani().GetData(MovId, LoteId, EmprId, MovCierreDiario, QuincenaId, MovEstadoPlan, EmpId, MovZafra, TareaId, MovFecha, MovUsuarioIngresa, CcostId);
        }
        [WebMethod]
        public int MoviPlaniSet(Cabana.Campo.RAAgricola.DAL.Identity.DS_ILC_Campo.PLwebMoviPlaniDataTable tablaMoviPlani)
        {
            return new Cabana.Campo.RAAgricola.BLL.Cs.PLwebMoviPlani().UpdateData(tablaMoviPlani);
        }

        //Metodos para tipo de Empleados

        [WebMethod]
        public DS_ILC_Campo.PLwebTipoEmpleadoDataTable TipoEmpGet(int? TipoId, int? EmprId)
        {
            return new Cabana.Campo.RAAgricola.BLL.Cs.PLwebTipoEmpleado().GetData(TipoId, EmprId);
        }
        [WebMethod]
        public int TipoEmpSet(Cabana.Campo.RAAgricola.DAL.Identity.DS_ILC_Campo.PLwebTipoEmpleadoDataTable tablaTipoEmp)
        {
            return new Cabana.Campo.RAAgricola.BLL.Cs.PLwebTipoEmpleado().UpdateData(tablaTipoEmp);
        }

        //Metodos para Usuarios de empresas

        [WebMethod]
        public DS_ILC_Campo.PLwebUsuariosEmprDataTable UsuarioEmprGet(int? UsuId, String UsuNombre, String UsuPass, int? EmprId, int? UsuEstatus, int? UsuNivelAcceso)
        {
            return new Cabana.Campo.RAAgricola.BLL.Cs.PLwebUsuariosEmpr().GetData( UsuId, UsuNombre, UsuPass, EmprId, UsuEstatus, UsuNivelAcceso);
        }
        [WebMethod]
        public int UsuarioEmprSet(Cabana.Campo.RAAgricola.DAL.Identity.DS_ILC_Campo.PLwebUsuariosEmprDataTable tablaUsuarioEmpr)
        {
            return new Cabana.Campo.RAAgricola.BLL.Cs.PLwebUsuariosEmpr().UpdateData(tablaUsuarioEmpr);
        }

        //Metodos para Usuarios mandadores de cuadrillas
        /*
        [WebMethod]
        public DS_ILC_Campo.PLwebUsuariosCuadrillasDataTable UsuariosCuadrillasGet(int? ManId, int? UsuId, int? CuaId)
        {
            return new Cabana.Campo.RAAgricola.BLL.Cs.PLwebUsuariosCuadrillas().GetData(ManId, UsuId, CuaId);
        }
        [WebMethod]
        public int UsuariosCuadrillasSet(Cabana.Campo.RAAgricola.DAL.Identity.DS_ILC_Campo.PLwebUsuariosCuadrillasDataTable tablaUsuariosCuadrillas)
        {
            return new Cabana.Campo.RAAgricola.BLL.Cs.PLwebUsuariosCuadrillas().UpdateData(tablaUsuariosCuadrillas);
        }
        */
        //Metodos Fincas administras por empresas
        [WebMethod]
        public DS_ILC_Campo.VPLwebLotesProvDataTable ProveedoresAdminGet(String gpId, int? FincaId, int? LoteId)
        {
            int? v1 = null;
            int? v2 = null;
            if (FincaId != 0)
            {
                v1 = FincaId;
            }
            if (LoteId != 0)
            {
                v2 = LoteId;
            }
            return new Cabana.Campo.RAAgricola.BLL.Cs.VPLwebLotesProv().GetData(gpId, v1, v2);
        }

        [WebMethod]
        public DS_ILC_Campo.VPLwebLotesProvDataTable ProveedoresUsuariosGet(String gpId, int? FincaId, int? LoteId, int? UsuId, int? EmprId)
        {
            return new Cabana.Campo.RAAgricola.BLL.Cs.VPLwebLotesProv().GetDataByUsuId(gpId, FincaId, LoteId, UsuId, EmprId);
        }

        [WebMethod]
        public DS_ILC_Campo.VPLwebLotesProvDataTable ProveedoresEmpresasGet(String gpId, int? FincaId, int? LoteId)
        {
            return new Cabana.Campo.RAAgricola.BLL.Cs.VPLwebLotesProv().GetDataByEmprId(gpId, FincaId, LoteId);
        }

        //Metodos Usuarios By Fincas
        [WebMethod]
        public DS_ILC_Campo.PLwebAccesoUsuFincasDataTable AccesoUsuFincasGet(int? AccUsuId,int? UsuId,int? AccEmprId,int? EmprId)
        {
            return new Cabana.Campo.RAAgricola.BLL.Cs.PLwebAccesoUsuFincas().GetData(AccUsuId, UsuId, AccEmprId, EmprId);
        }

        [WebMethod]
        public int AccesoUsuFincasSet(Cabana.Campo.RAAgricola.DAL.Identity.DS_ILC_Campo.PLwebAccesoUsuFincasDataTable tablaUsuariosByFincas)
        {
            return new Cabana.Campo.RAAgricola.BLL.Cs.PLwebAccesoUsuFincas().UpdateData(tablaUsuariosByFincas);
        }

        //Metodos Empresas By Fincas
        [WebMethod]
        public DS_ILC_Campo.PLwebAccesoEmprFincasDataTable AccesoEmprFincasGet(int? AccEmprId, int? EmprId)
        {
            return new Cabana.Campo.RAAgricola.BLL.Cs.PLwebAccesoEmprFincas().GetData(AccEmprId, EmprId);
        }

        [WebMethod]
        public int AccesoEmprFincasSet(Cabana.Campo.RAAgricola.DAL.Identity.DS_ILC_Campo.PLwebAccesoEmprFincasDataTable tablaFincasByEmpr)
        {
            return new Cabana.Campo.RAAgricola.BLL.Cs.PLwebAccesoEmprFincas().UpdateData(tablaFincasByEmpr);
        }

        [WebMethod]
        public Object InsertAllFincas(int? UsuAdmIdCrea, String CodProv, int? EmprId)
        {
            return new Cabana.Campo.RAAgricola.BLL.Cs.PLwebAccesoEmprFincas().InsertAllFincas(UsuAdmIdCrea, CodProv, EmprId);
        }

        //Metodos Empresas Administrativas
        [WebMethod]
        public DS_ILC_Campo.PLwebEmprAdministrativasDataTable EmpresasAdmGet(int? EmprId)
        {
            return new Cabana.Campo.RAAgricola.BLL.Cs.PLwebEmprAdministrativas().GetData(EmprId);
        }

        [WebMethod]
        public int EmpresasAdmSet(Cabana.Campo.RAAgricola.DAL.Identity.DS_ILC_Campo.PLwebEmprAdministrativasDataTable tablaEmpresas)
        {
            return new Cabana.Campo.RAAgricola.BLL.Cs.PLwebEmprAdministrativas().UpdateData(tablaEmpresas);
        }

        //Metodos para Usuarios mandadores de cuadrillas
        
        [WebMethod]
        public DS_ILC_Campo.PLwebAccUsuCuadrillasDataTable AccUsuCuadrillasGet(int? AcCuaId, int? EmprId, int? UsuId, int? CuaId)
        {
            return new Cabana.Campo.RAAgricola.BLL.Cs.PLwebAccUsuCuadrillas().GetData(AcCuaId, EmprId, UsuId, CuaId);
        }
        [WebMethod]
        public int AccUsuCuadrillasSet(Cabana.Campo.RAAgricola.DAL.Identity.DS_ILC_Campo.PLwebAccUsuCuadrillasDataTable tablaUsuariosCuadrillas)
        {
            return new Cabana.Campo.RAAgricola.BLL.Cs.PLwebAccUsuCuadrillas().UpdateData(tablaUsuariosCuadrillas);
        }

        //metodos para reporte de planilla

        [WebMethod]
        public DS_ILC_Campo.Sub_PLwebConsultaPlanillaDataTable PlanillaGet(String dato1, String dato2, String dato3, String dato4)
        {
            return new Cabana.Campo.RAAgricola.BLL.Cs.PLwebConsultaPlanilla().GetData(dato1, dato2, dato3, dato4);
        }

        //Metodos Centro de Costos
        [WebMethod]
        public DS_ILC_Campo.PLwebCentroCostoTareasDataTable CentroCostosGet(int? CcId)
        {
            return new Cabana.Campo.RAAgricola.BLL.Cs.PLwebCentroCostoTareas().GetData(CcId);
        }

        [WebMethod]
        public int CentroCostosSet(Cabana.Campo.RAAgricola.DAL.Identity.DS_ILC_Campo.PLwebCentroCostoTareasDataTable tablaCentroCostos)
        {
            return new Cabana.Campo.RAAgricola.BLL.Cs.PLwebCentroCostoTareas().UpdateData(tablaCentroCostos);
        }
        //--------------------------- PLwebDescuentosEmp
        [WebMethod]
        public DS_ILC_Campo.PLwebDescuentosEmpDataTable PLwebDescuentosEmpGet(int? DescId, int? DescEmprId, int? DescQuinId, int? DescUsuIdIngresa, int? DescEmpId)
        {
            return new Cabana.Campo.RAAgricola.BLL.Cs.PLwebDescuentosEmp().GetData(DescId, DescEmprId, DescQuinId, DescUsuIdIngresa, DescEmpId);
        }

        [WebMethod]
        public int PLwebDescuentosEmpSet(Cabana.Campo.RAAgricola.DAL.Identity.DS_ILC_Campo.PLwebDescuentosEmpDataTable tablaDescuentosEmp)
        {
            return new Cabana.Campo.RAAgricola.BLL.Cs.PLwebDescuentosEmp().UpdateData(tablaDescuentosEmp);
        }

        //--------------------------- PLwebEmpCua tabla empleados por cuadrilla
        [WebMethod]
        public DS_ILC_Campo.PLwebEmpCuaDataTable PLwebEmpCuaEmpGet(int? UsuId, int? EmprId)
        {
            return new Cabana.Campo.RAAgricola.BLL.Cs.PLwebEmpCua().GetData(UsuId, EmprId);
        }

        [WebMethod]
        public int PLwebEmpCuaEmpSet(Cabana.Campo.RAAgricola.DAL.Identity.DS_ILC_Campo.PLwebEmpCuaDataTable tablaEmpCua)
        {
            return new Cabana.Campo.RAAgricola.BLL.Cs.PLwebEmpCua().UpdateData(tablaEmpCua);
        }

        //--------------------------- Tabla Centros de Costo
        [WebMethod]
        public DS_ILC_Campo.PLwebCentroCostosEmprDataTable PLwebCentroCostosEmprGet(int? CcosId, int? CcEmprId)
        {
            return new Cabana.Campo.RAAgricola.BLL.Cs.PLwebCentroCostosEmpr().GetData(CcosId, CcEmprId);
        }

        [WebMethod]
        public int PLwebCentroCostosEmprSet(Cabana.Campo.RAAgricola.DAL.Identity.DS_ILC_Campo.PLwebCentroCostosEmprDataTable tablaPLwebCentroCostosEmpr)
        {
            return new Cabana.Campo.RAAgricola.BLL.Cs.PLwebCentroCostosEmpr().UpdateData(tablaPLwebCentroCostosEmpr);
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]//Specify return format.
        public String PLwebEncFac(string cliente, string factura, decimal numReqProv, String numLote, String Description)
        {
            string numero = "";
            string DocType = "";
            try
            {
                //NAV nav = new NAV(new Uri("http://10.1.1.23:7048/DynamicsNAV80/OData/Company('LACABANA')"));
                NAV nav = new NAV(new Uri("http://10.1.1.23:1048/CABANA2016/OData/Company('LACABANA')"));

                //nav.Credentials = CredentialCache.DefaultNetworkCredentials;
                nav.Credentials = new System.Net.NetworkCredential("nav.services", "Navucodonosor", "CABANA");
                PedidoVenta encabezado = new PedidoVenta();
                PedidoVentaSalesLines linea = new PedidoVentaSalesLines();
                encabezado.Sell_to_Customer_No = cliente;
                encabezado.External_Document_No = factura;
                encabezado.Posting_Description = Description;
                encabezado.ExtendDec1 = numReqProv;
                encabezado.ExtendTxt5 = numLote;

                nav.AddToPedidoVenta(encabezado);
                nav.SaveChanges();  //con esto obtengo el numero de factura
                numero = encabezado.No;
                DocType = encabezado.Document_Type;
            }
            catch (Exception ex)
            {
                return "{\"Numero\":\"fallo\"}" + ex;
            }

            return "{\"Numero\":\"" + numero + "\", \"DocType\":\"" + DocType + "\"}";
        }


        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]//Specify return format.
        public String PLwebEncPedidoVenta(string Empresa, string cliente, string factura, decimal numReqProv, String numLote, String Description)
        {
            string numero = "";
            string DocType = "";
            string subtipo = "CCF-V";
            try
            {
                //NAV nav = new NAV(new Uri("http://10.1.1.23:7048/DynamicsNAV80/OData/Company('LACABANA')"));
                //NAV nav = new NAV(new Uri("http://10.1.1.23:1048/CABANA2016/OData/Company('"+ Empresa + "')"));
                NAV nav = new NAV(new Uri("http://10.1.1.14:7048/DynamicsNav110/OData/Company('LACABANA')"));

                //nav.Credentials = CredentialCache.DefaultNetworkCredentials;
                nav.Credentials = new System.Net.NetworkCredential("nav.services", "Navucodonosor", "CABANA");
                PedidoVenta encabezado = new PedidoVenta();
                PedidoVentaSalesLines linea = new PedidoVentaSalesLines();

                /*
                                  if (dtCombustible[i].DOCID == "CCF")
                                        subtipo = "CCF-V";
                                        subtipo = "FAC-V";
               */

                encabezado.Sell_to_Customer_No = cliente;
                encabezado.No = factura;
                encabezado.External_Document_No = factura;
                encabezado.Posting_Description = Description;
                encabezado.ExtendDec1 = numReqProv;
                encabezado.ExtendTxt5 = numLote;

                encabezado.SubType = subtipo;
                encabezado.Document_Type = "1";

                //encabezado.Posting_Date = fechaRegistro;
                //encabezado.Payment_Method_Code = "LIQ.DIESEL";

                nav.AddToPedidoVenta(encabezado);
                nav.SaveChanges();  //con esto obtengo el numero de factura
                numero = encabezado.No;
                DocType = encabezado.Document_Type;
            }
            catch (Exception ex)
            {
                return "{\"Numero\":\"fallo\"}" + ex;
            }

            return "{\"Numero\":\"" + numero + "\", \"DocType\":\"" + DocType + "\"}";
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]//Specify return format.
        public String PLwebDetallePedidoVenta(string Empresa, string numero, string DocType, string cantidad, string precioUnidad, string codArticulo, string MovBodega, string CentroBeneficio, String ZonaServicio, String CodVariante)
        {
            try
            {
                //NAV nav = new NAV(new Uri("http://10.1.1.23:7048/DynamicsNAV80/OData/Company('LACABANA')"));
                NAV nav = new NAV(new Uri("http://10.1.1.23:1048/CABANA2016/OData/Company('"+ Empresa + "')"));

                //nav.Credentials = CredentialCache.DefaultNetworkCredentials;
                nav.Credentials = new System.Net.NetworkCredential("nav.services", "Navucodonosor", "CABANA");
                PedidoVenta encabezado = new PedidoVenta();
                PedidoVentaSalesLines linea = new PedidoVentaSalesLines();

                /////////////Ingresamos el Impuesto    
                linea.Document_No = numero;
                linea.Document_Type = DocType;
                linea.Type = "2"; //item
                linea.No = codArticulo;
                linea.Location_Code = MovBodega;
                linea.Description_2 = ZonaServicio;
                linea.Variant_Code = CodVariante;// "ATP";
                linea.Quantity = Decimal.Parse(cantidad);
                linea.Unit_Price = Decimal.Parse(precioUnidad);//0.30m;

                linea.ShortcutDimCode_x005B_3_x005D_ = CentroBeneficio;//"6109";
                linea.ShortcutDimCode_x005B_5_x005D_ = "7002";

                nav.AddToPedidoVentaSalesLines(linea);
                nav.SaveChanges();

            }
            catch (Exception ex)
            {
                return "{\"Numero\":\"fallo\"}" + ex;
            }

            return "{\"Numero\":\"" + codArticulo + "\", \"DocType\":\"" + DocType + "\"}";
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]//Specify return format.
        public String PLwebEncFacServicios(string cliente, string factura, decimal numReqProv, String numLote)
        {
            string numero = "";
            string DocType = "";
            try
            {
                //NAV nav = new NAV(new Uri("http://10.1.1.23:7048/DynamicsNAV80/OData/Company('LACABANA')"));
                NAV nav = new NAV(new Uri("http://10.1.1.23:1048/CABANA2016/OData/Company('LACABANA')"));

                //nav.Credentials = CredentialCache.DefaultNetworkCredentials;
                nav.Credentials = new System.Net.NetworkCredential("nav.services", "Navucodonosor", "CABANA");
                PedidoVenta encabezado = new PedidoVenta();
                PedidoVentaSalesLines linea = new PedidoVentaSalesLines();
                encabezado.Sell_to_Customer_No = cliente;
                encabezado.Posting_Description = "Servicio de Maquinaria Agricola";
                encabezado.External_Document_No = factura;
                encabezado.ExtendDec1 = numReqProv;
                encabezado.ExtendTxt5 = numLote;

                nav.AddToPedidoVenta(encabezado);
                nav.SaveChanges();  //con esto obtengo el numero de factura
                numero = encabezado.No;
                DocType = encabezado.Document_Type;
            }
            catch (Exception ex)
            {
                return "{\"Numero\":\"fallo\"}" + ex;
            }

            return "{\"Numero\":\"" + numero + "\", \"DocType\":\"" + DocType + "\"}";
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]//Specify return format.
        public String PLwebDetalleFac(string numero, string DocType, string cantidad, string precioUnidad, string codArticulo, string MovBodega, string CentroBeneficio, String ZonaServicio, String CodVariante)
        {
            try
            {
                //NAV nav = new NAV(new Uri("http://10.1.1.23:7048/DynamicsNAV80/OData/Company('LACABANA')"));
                NAV nav = new NAV(new Uri("http://10.1.1.23:1048/CABANA2016/OData/Company('LACABANA')"));

                //nav.Credentials = CredentialCache.DefaultNetworkCredentials;
                nav.Credentials = new System.Net.NetworkCredential("nav.services", "Navucodonosor", "CABANA");
                PedidoVenta encabezado = new PedidoVenta();
                PedidoVentaSalesLines linea = new PedidoVentaSalesLines();

                /////////////Ingresamos el Impuesto    
                linea.Document_No   = numero;
                linea.Document_Type = DocType;
                linea.Type          = "2"; //item
                linea.No            = codArticulo;
                linea.Location_Code = MovBodega;
                linea.Description_2 = ZonaServicio;
                linea.Variant_Code  = CodVariante;// "ATP";
                linea.Quantity      = Decimal.Parse(cantidad);
                linea.Unit_Price    = Decimal.Parse(precioUnidad);//0.30m;

                linea.ShortcutDimCode_x005B_3_x005D_ = CentroBeneficio;//"6109";
                linea.ShortcutDimCode_x005B_5_x005D_ = "7002";
     
                nav.AddToPedidoVentaSalesLines(linea);
                nav.SaveChanges();
      
            }
            catch (Exception ex)
            {
                return "{\"Numero\":\"fallo\"}"+ ex;
            }

            return "{\"Numero\":\"" + codArticulo + "\", \"DocType\":\"" + DocType + "\"}";
        }


        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]//Specify return format.
        public String PLwebDetalleFacServicios(string numero, string DocType, string cantidad, string precioUnidad, string codArticulo, string aaTrxDimCode,  String ACTNUMST, String descItem)
        {
            try
            {
                NAV nav = new NAV(new Uri("http://10.1.1.23:1048/CABANA2016/OData/Company('LACABANA')"));


                nav.Credentials = new System.Net.NetworkCredential("nav.services", "Navucodonosor", "CABANA");
                PedidoVenta encabezado = new PedidoVenta();
                PedidoVentaSalesLines linea = new PedidoVentaSalesLines();


                /////////////Ingresamos el Impuesto    
                linea.Document_No = numero;
                linea.Document_Type = DocType;
                linea.Type = "2"; //item
                linea.No = codArticulo;
                linea.Quantity = Decimal.Parse(cantidad);
                linea.Unit_Price = Decimal.Parse(precioUnidad);//0.30m;
                //linea.Location_Code = MovBodega;
                //linea.Description_2 = ZonaServicio;
                //linea.Variant_Code = CodVariante;// "ATP";
                linea.ShortcutDimCode_x005B_3_x005D_ = aaTrxDimCode;//"6109";
                linea.ShortcutDimCode_x005B_4_x005D_ = ACTNUMST;//"6109";
                linea.ShortcutDimCode_x005B_5_x005D_ = "7002";
                linea.Description = descItem;
                nav.AddToPedidoVentaSalesLines(linea);
                nav.SaveChanges();

            }
            catch (Exception ex)
            {
                return "{\"Numero\":\"fallo\"}" + ex;
            }

            return "{\"Numero\":\"" + codArticulo + "\", \"DocType\":\"" + DocType + "\"}";
        }


        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]//Specify return format.
        public String PLwebDiarioProducto( String cantidad, String costoart, string codArticulo, string MovBodega, String UnidadMedida)
        {
            try
            {
                //NAV nav = new NAV(new Uri("http://10.1.1.23:1048/CABANA2016/OData/Company('LACABANA')"));
                NAV nav = new NAV(new Uri("http://10.1.1.23:1048/CABANA2016/OData/Company('LACABANA-TEST')"));


                //nav.Credentials = CredentialCache.DefaultNetworkCredentials;
                nav.Credentials = new System.Net.NetworkCredential("nav.services", "Navucodonosor", "CABANA");
                DiarioProducto linea = new DiarioProducto();
                DateTime Hoy = DateTime.Today;
                string fecha_actual = Hoy.ToString("dd-MM-yyyy");
                DateTime esteDia = DateTime.Today;
                decimal Discount_Amount = 0; 

                /////////////Ingresamos el Impuesto    
                linea.Journal_Template_Name = "PRODUCTO";
                linea.Journal_Batch_Name    = "ABREQINS";
                linea.Line_No               = 10000;//LINEA
                linea.Item_No               = "15937"; //Me.Dgdetrequisi.Rows(I).Cells(2).Value
                linea.Posting_Date          = esteDia;
                linea.Entry_Type            = "3";
       
                linea.Document_No           = "PPACON01PRUEBA";//PACON
                linea.Description           = "DIESEL";//Mid(Dgdetrequisi.Rows(I).Cells(1).Value, 1, 50)
                linea.Location_Code         = "AR";//RECEPCION

                linea.Quantity              = (Convert.ToDecimal(cantidad)) * -1;//no resta
                linea.Unit_Amount           = Convert.ToDecimal(costoart);
                linea.Unit_Cost             = Convert.ToDecimal(costoart);
                linea.Amount                = Convert.ToDecimal(costoart) * Convert.ToDecimal(cantidad);
                linea.Discount_Amount       = Discount_Amount;
                linea.Salespers_Purch_Code  = "";

                linea.Applies_to_Entry      = 0;
                linea.Shortcut_Dimension_1_Code = "4412";//Me.Dgdetrequisi.Rows(I).Cells(6).Value
                linea.Shortcut_Dimension_2_Code = "M2017";

                linea.Reason_Code           = "";
                linea.Transaction_Type      = "";
                linea.Transport_Method      = "";
                linea.Country_Region_Code   = "";
                linea.Gen_Bus_Posting_Group = "";
                linea.Gen_Prod_Posting_Group = "PA-CL";//Me.Dgdetrequisi.Rows(I).Cells(11).Value

                linea.Document_Date         = esteDia;
                linea.External_Document_No  = "REQUI" +1;
                linea.Variant_Code          = "";
                linea.Bin_Code              = "";
                linea.Unit_of_Measure_Code  = UnidadMedida;//Unidad de medida
                linea.Applies_from_Entry    = 0;

                nav.AddToDiarioProducto(linea);
                nav.SaveChanges();
            }
            catch (Exception ex)
            {
                return "{\"Numero\":\"fallo\"}" + ex;
            }

            return "{\"codArticulo\":\"" + codArticulo + "\", \"DocType\":\"" + codArticulo + "\"}";
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]//Specify return format.
        public String PLwebFacturaCompraIC(String Documento, string usuario)
        {

            DS_SAIPLUS.SAIPLUS_SubirFacturaCompraDataTable fc = new DS_SAIPLUS.SAIPLUS_SubirFacturaCompraDataTable();
            SAIPLUS_SubirFacturaCompraTableAdapter ta = new SAIPLUS_SubirFacturaCompraTableAdapter();

            ta.Fill(fc, Documento);

            String nuevoDocInterno;

            try
            {
                if(fc.Count > 0)
                {//se verifca si devolvio registros ya que el numero de lineas se toma en cuenta desde el SP que genera las lineas


                
                NAV nav = new NAV(new Uri("http://10.1.1.23:1048/CABANA2016/OData/Company('"+fc[0].EmpresaNav+"')"));
                //NAV nav = new NAV(new Uri("http://10.1.1.23:1048/CABANA2016/OData/Company('LACABANA')"));
              
                    nav.Credentials = new System.Net.NetworkCredential("nav.services", "Navucodonosor", "CABANA");

                    InvoicePurchase encabezado = new InvoicePurchase();
                    encabezado.Buy_from_Vendor_No = fc[0].CABANA;
                    encabezado.Vendor_Invoice_No = fc[0].External_Document_No_;
                    nav.AddToInvoicePurchase(encabezado);         
                    nav.SaveChanges();
                    nuevoDocInterno = encabezado.No;
                    comandoSql( insertarLineas(nuevoDocInterno, fc[0].External_Document_No_, fc[0].EmpresaNav));
                    ta.SaiPlus_OrdenesCompra_Insert(fc[0].External_Document_No_, encabezado.Buy_from_Vendor_No, fc[0].EmpresaNav, DateTime.Today, usuario, encabezado.No);

                }
            }
            catch (Exception ex)
            {
                return "{\"Documento\":\"" + Documento + "\", \"Subido\":\"" + "0" + "\", \"Mensaje\":\"" + ex.Message + "\"}";
            }

            return "{\"Documento\":\"" + Documento + "\", \"Subido\":\"" + "1" + "\", \"Mensaje\":\"" + "OK" + "\"}";
        }

        private string insertarLineas(string nuevoDocInterno, string external_Document_No_, string empresaNav)
        {

     string sql = "INSERT INTO LACABANA.[dbo].["+empresaNav+"$Purchase Line] "
         +" ([Document Type]                                       "
         +" ,[Document No_]                                        "
         +" ,[Line No_]                                            "
         +" ,[Buy-from Vendor No_]                               "
         +" ,[Type]                                                "
         +" ,[No_]                                                 "
         +" ,[Location Code]                                       "
         +" ,[Posting Group]                                       "
         +" ,[Expected Receipt Date]                               "
         +" ,[Description]                                         "
         +" ,[Description 2]                                       "
         +" ,[Unit of Measure]                                     "
         +" ,[Quantity]                                            "
         +" ,[Outstanding Quantity]                                "
         +" ,[Qty_ to Invoice]                                     "
         +" ,[Qty_ to Receive]                                     "
         +" ,[Direct Unit Cost]                                    "
         +" ,[Unit Cost (LCY)]                                      "
         +" ,[VAT _]                                               "
         +" ,[Line Discount _]                                     "
         +" ,[Line Discount Amount]                                "
         +" ,[Amount]                                              "
         +" ,[Amount Including VAT]                                "
         +" ,[Unit Price (LCY)]                                     "
         +" ,[Allow Invoice Disc_]                                 "
         +" ,[Gross Weight]                                        "
         +" ,[Net Weight]                                          "
         +" ,[Units per Parcel]                                    "
         +" ,[Unit Volume]                                         "
         +" ,[Appl_-to Item Entry]                               "
         +" ,[Shortcut Dimension 1 Code]                           "
         +" ,[Shortcut Dimension 2 Code]                           "
         +" ,[Job No_]                                             "
         +" ,[Indirect Cost _]                                     "
         +" ,[Recalculate Invoice Disc_]                           "
         +" ,[Outstanding Amount]                                  "
         +" ,[Qty_ Rcd_ Not Invoiced]                              "
         +" ,[Amt_ Rcd_ Not Invoiced]                              "
         +" ,[Quantity Received]                                   "
         +" ,[Quantity Invoiced]                                   "
         +" ,[Receipt No_]                                         "
         +" ,[Receipt Line No_]                                    "
         +" ,[Profit _]                                            "
         +" ,[Pay-to Vendor No_]                                 "
         +" ,[Inv_ Discount Amount]                                "
         +" ,[Vendor Item No_]                                     "
         +" ,[Sales Order No_]                                     "
         +" ,[Sales Order Line No_]                                "
         +" ,[Drop Shipment]                                       "
         +" ,[Gen_ Bus_ Posting Group]                             "
         +" ,[Gen_ Prod_ Posting Group]                            "
         +" ,[VAT Calculation Type]                                "
         +" ,[Transaction Type]                                    "
         +" ,[Transport Method]                                    "
         +" ,[Attached to Line No_]                                "
         +" ,[Entry Point]                                         "
         +" ,[Area]                                                "
         +" ,[Transaction Specification]                           "
         +" ,[Tax Area Code]                                       "
         +" ,[Tax Liable]                                          "
         +" ,[Tax Group Code]                                      "
         +" ,[Use Tax]                                             "
         +" ,[VAT Bus_ Posting Group]                              "
         +" ,[VAT Prod_ Posting Group]                             "
         +" ,[Currency Code]                                       "
         +" ,[Outstanding Amount (LCY)]                             "
         +" ,[Amt_ Rcd_ Not Invoiced (LCY)]                         "
         +" ,[Blanket Order No_]                                   "
         +" ,[Blanket Order Line No_]                              "
         +" ,[VAT Base Amount]                                     "
         +" ,[Unit Cost]                                           "
         +" ,[System-Created Entry]                              "
         +" ,[Line Amount]                                         "
         +" ,[VAT Difference]                                      "
         +" ,[Inv_ Disc_ Amount to Invoice]                        "
         +" ,[VAT Identifier]                                      "
         +" ,[IC Partner Ref_ Type]                                "
         +" ,[IC Partner Reference]                                "
         +" ,[Prepayment _]                                        "
         +" ,[Prepmt_ Line Amount]                                 "
         +" ,[Prepmt_ Amt_ Inv_]                                   "
         +" ,[Prepmt_ Amt_ Incl_ VAT]                              "
         +" ,[Prepayment Amount]                                   "
         +" ,[Prepmt_ VAT Base Amt_]                               "
         +" ,[Prepayment VAT _]                                    "
         +" ,[Prepmt_ VAT Calc_ Type]                              "
         +" ,[Prepayment VAT Identifier]                           "
         +" ,[Prepayment Tax Area Code]                            "
         +" ,[Prepayment Tax Liable]                               "
         +" ,[Prepayment Tax Group Code]                           "
         +" ,[Prepmt Amt to Deduct]                                "
         +" ,[Prepmt Amt Deducted]                                 "
         +" ,[Prepayment Line]                                     "
         +" ,[Prepmt_ Amount Inv_ Incl_ VAT]                       "
         +" ,[Prepmt_ Amount Inv_ (LCY)]                            "
         +" ,[IC Partner Code]                                     "
         +" ,[Prepmt_ VAT Amount Inv_ (LCY)]                        "
         +" ,[Prepayment VAT Difference]                           "
         +" ,[Prepmt VAT Diff_ to Deduct]                          "
         +" ,[Prepmt VAT Diff_ Deducted]                           "
         +" ,[Outstanding Amt_ Ex_ VAT (LCY)]                       "
         +" ,[A_ Rcd_ Not Inv_ Ex_ VAT (LCY)]                       "
         +" ,[Dimension Set ID]                                    "
         +" ,[Job Task No_]                                        "
         +" ,[Job Line Type]                                       "
         +" ,[Job Unit Price]                                      "
         +" ,[Job Total Price]                                     "
         +" ,[Job Line Amount]                                     "
         +" ,[Job Line Discount Amount]                            "
         +" ,[Job Line Discount _]                                 "
         +" ,[Job Unit Price (LCY)]                                 "
         +" ,[Job Total Price (LCY)]                                "
         +" ,[Job Line Amount (LCY)]                                "
         +" ,[Job Line Disc_ Amount (LCY)]                          "
         +" ,[Job Currency Factor]                                 "
         +" ,[Job Currency Code]                                   "
         +" ,[Job Planning Line No_]                               "
         +" ,[Job Remaining Qty_]                                  "
         +" ,[Job Remaining Qty_ (Base)]                            "
         +" ,[Deferral Code]                                       "
         +" ,[Returns Deferral Start Date]                         "
         +" ,[Prod_ Order No_]                                     "
         +" ,[Variant Code]                                        "
         +" ,[Bin Code]                                            "
         +" ,[Qty_ per Unit of Measure]                            "
         +" ,[Unit of Measure Code]                                "
         +" ,[Quantity (Base)]                                      "
         +" ,[Outstanding Qty_ (Base)]                              "
         +" ,[Qty_ to Invoice (Base)]                               "
         +" ,[Qty_ to Receive (Base)]                               "
         +" ,[Qty_ Rcd_ Not Invoiced (Base)]                        "
         +" ,[Qty_ Received (Base)]                                 "
         +" ,[Qty_ Invoiced (Base)]                                 "
         +" ,[FA Posting Date]                                     "
         +" ,[FA Posting Type]                                     "
         +" ,[Depreciation Book Code]                              "
         +" ,[Salvage Value]                                       "
         +" ,[Depr_ until FA Posting Date]                         "
         +" ,[Depr_ Acquisition Cost]                              "
         +" ,[Maintenance Code]                                    "
         +" ,[Insurance No_]                                       "
         +" ,[Budgeted FA No_]                                     "
         +" ,[Duplicate in Depreciation Book]                      "
         +" ,[Use Duplication List]                                "
         +" ,[Responsibility Center]                               "
         +" ,[Cross-Reference No_]                               "
         +" ,[Unit of Measure (Cross Ref_)]                         "
         +" ,[Cross-Reference Type]                              "
         +" ,[Cross-Reference Type No_]                          "
         +" ,[Item Category Code]                                  "
         +" ,[Nonstock]                                            "
         +" ,[Purchasing Code]                                     "
         +" ,[Product Group Code]                                  "
         +" ,[Special Order]                                       "
         +" ,[Special Order Sales No_]                             "
         +" ,[Special Order Sales Line No_]                        "
         +" ,[Completely Received]                                 "
         +" ,[Requested Receipt Date]                              "
         +" ,[Promised Receipt Date]                               "
         +" ,[Lead Time Calculation]                               "
         +" ,[Inbound Whse_ Handling Time]                         "
         +" ,[Planned Receipt Date]                                "
         +" ,[Order Date]                                          "
         +" ,[Allow Item Charge Assignment]                        "
         +" ,[Return Qty_ to Ship]                                 "
         +" ,[Return Qty_ to Ship (Base)]                           "
         +" ,[Return Qty_ Shipped Not Invd_]                       "
         +" ,[Ret_ Qty_ Shpd Not Invd_(Base)]                      "
         +" ,[Return Shpd_ Not Invd_]                              "
         +" ,[Return Shpd_ Not Invd_ (LCY)]                         "
         +" ,[Return Qty_ Shipped]                                 "
         +" ,[Return Qty_ Shipped (Base)]                           "
         +" ,[Return Shipment No_]                                 "
         +" ,[Return Shipment Line No_]                            "
         +" ,[Return Reason Code]                                  "
         +" ,[Tax To Be Expensed]                                  "
         +" ,[Provincial Tax Area Code]                            "
         +" ,[IRS 1099 Liable]                                     "
         +" ,[GST_HST]                                             "
         +" ,[SubSource]                                           "
         +" ,[Transport Cost Entry No]                             "
         +" ,[Document Line No_]                                   "
         +" ,[Original Quantity]                                   "
         +" ,[Allow Quantity Change]                               "
         +" ,[Supply Chain Group Code]                             "
         +" ,[Lot No_]                                             "
         +" ,[Creation Date]                                       "
         +" ,[Supplier Lot No_]                                    "
         +" ,[Alt_ Qty_ Transaction No_]                           "
         +" ,[Quantity (Alt_)]                                      "
         +" ,[Qty_ to Receive (Alt_)]                               "
         +" ,[Qty_ Received (Alt_)]                                 "
         +" ,[Qty_ to Invoice (Alt_)]                               "
         +" ,[Qty_ Invoiced (Alt_)]                                 "
         +" ,[Return Qty_ to Ship (Alt_)]                           "
         +" ,[Return Qty_ Shipped (Alt_)]                           "
         +" ,[Alt_ Qty_ Update Required]                           "
         +" ,[Accrual Amount (Cost)]                                "
         +" ,[Accrual Plan Type]                                   "
         +" ,[Accrual Source No_]                                  "
         +" ,[Accrual Source Doc_ Type]                            "
         +" ,[Accrual Source Doc_ No_]                             "
         +" ,[Accrual Source Doc_ Line No_]                        "
         +" ,[Scheduled Accrual No_]                               "
         +" ,[Qty_ to Ship_Recv_ (Cont_)]                           "
         +" ,[Receiving Reason Code]                               "
         +" ,[Farm]                                                "
         +" ,[Brand]                                               "
         +" ,[Country_Region of Origin Code]                       "
         +" ,[Commodity Manifest No_]                              "
         +" ,[Commodity Manifest Line No_]                         "
         +" ,[Commodity Received Date]                             "
         +" ,[Comm_ Payment Class Code]                            "
         +" ,[Commodity Cost Calculated]                           "
         +" ,[Commodity Unit Cost]                                 "
         +" ,[Commodity Amount]                                    "
         +" ,[Commodity Received Lot No_]                          "
         +" ,[Commodity P_O_ Type]                                 "
         +" ,[Producer Zone Code]                                  "
         +" ,[Rejection Action]                                    "
         +" ,[Label Unit of Measure Code]                          "
         +" ,[Work Order No_]                                      "
         +" ,[Maintenance Entry Type]                              "
         +" ,[Maintenance Trade Code]                              "
         +" ,[Hours]                                               "
         +" ,[Part No_]                                            "
         +" ,[Routing No_]                                         "
         +" ,[Operation No_]                                       "
         +" ,[Work Center No_]                                     "
         +" ,[Finished]                                            "
         +" ,[Prod_ Order Line No_]                                "
         +" ,[Overhead Rate]                                       "
         +" ,[MPS Order]                                           "
         +" ,[Planning Flexibility]                                "
         +" ,[Safety Lead Time]                                    "
         +" ,[Routing Reference No_])                              "
      +"   SELECT [Document Type]                                  "
		+"	,'"+nuevoDocInterno +"'  as [Document No_]               "
		+"	,e.Linea * 1000                                        "
		+"	,[Buy-from Vendor No_]                                 "
		+"	,[Type]                                                "
		+"	,e.CUENTA                                              "
		+"	,[Location Code]                                       "
		+"	,[Posting Group]                                       "
		+"	,[Expected Receipt Date]                               "
		+"	,[Description]                                         "
		+"	,[Description 2]                                       "
		+"	,[Unit of Measure]                                     "
		+"	,[Quantity]                                            "
		+"	,[Outstanding Quantity]                                "
		+"	,[Qty_ to Invoice]                                     "
		+"	,[Qty_ to Receive]                                     "
		+"	,e.Monto[Direct Unit Cost]                             "
		+"	,e.Monto[Unit Cost (LCY)]                               "
		+"	,[VAT _]                                               "
		+"	,[Line Discount _]                                     "
		+"	,[Line Discount Amount]                                "
		+"	,e.Monto[Amount]                                       "
		+"	,e.montoIVA[Amount Including VAT]                      "
		+"	,[Unit Price (LCY)]                                     "
		+"	,[Allow Invoice Disc_]                                 "
		+"	,[Gross Weight]                                        "
		+"	,[Net Weight]                                          "
		+"	,[Units per Parcel]                                    "
		+"	,[Unit Volume]                                         "
		+"	,[Appl_-to Item Entry]                                 "
		+"	,e.CECO[Shortcut Dimension 1 Code]                     "
		+"	,[Shortcut Dimension 2 Code]                           "
		+"	,[Job No_]                                             "
		+"	,[Indirect Cost _]                                     "
		+"	,[Recalculate Invoice Disc_]                           "
		+"	,e.montoIVA[Outstanding Amount]                        "
		+"	,[Qty_ Rcd_ Not Invoiced]                              "
		+"	,[Amt_ Rcd_ Not Invoiced]                              "
		+"	,[Quantity Received]                                   "
		+"	,[Quantity Invoiced]                                   "
		+"	,[Receipt No_]                                         "
		+"	,[Receipt Line No_]                                    "
		+"	,[Profit _]                                            "
		+"	,[Pay-to Vendor No_]                                   "
		+"	,[Inv_ Discount Amount]                                "
		+"	,[Vendor Item No_]                                     "
		+"	,[Sales Order No_]                                     "
		+"	,[Sales Order Line No_]                                "
		+"	,[Drop Shipment]                                       "
		+"	,[Gen_ Bus_ Posting Group]                             "
		+"	,[Gen_ Prod_ Posting Group]                            "
		+"	,[VAT Calculation Type]                                "
		+"	,[Transaction Type]                                    "
		+"	,[Transport Method]                                    "
		+"	,[Attached to Line No_]                                "
		+"	,[Entry Point]                                         "
		+"	,[Area]                                                "
		+"	,[Transaction Specification]                           "
		+"	,[Tax Area Code]                                       "
		+"	,[Tax Liable]                                          "
		+"	,[Tax Group Code]                                      "
		+"	,[Use Tax]                                             "
		+"	,[VAT Bus_ Posting Group]                              "
		+"	,[VAT Prod_ Posting Group]                             "
		+"	,[Currency Code]                                       "
		+"	,e.montoIVA[Outstanding Amount (LCY)]                   "
		+"	,[Amt_ Rcd_ Not Invoiced (LCY)]                         "
		+"	,[Blanket Order No_]                                   "
		+"	,[Blanket Order Line No_]                              "
		+"	,e.Monto[VAT Base Amount]                              "
		+"	,e.monto[Unit Cost]                                    "
		+"	,[System-Created Entry]                                "
		+"	,e.Monto[Line Amount]                                  "
		+"	,[VAT Difference]                                      "
		+"	,[Inv_ Disc_ Amount to Invoice]                        "
		+"	,[VAT Identifier]                                      "
		+"	,[IC Partner Ref_ Type]                                "
		+"	,[IC Partner Reference]                                "
		+"	,[Prepayment _]                                        "
		+"	,[Prepmt_ Line Amount]                                 "
		+"	,[Prepmt_ Amt_ Inv_]                                   "
		+"	,[Prepmt_ Amt_ Incl_ VAT]                              "
		+"	,[Prepayment Amount]                                   "
		+"	,[Prepmt_ VAT Base Amt_]                               "
		+"	,[Prepayment VAT _]                                    "
		+"	,[Prepmt_ VAT Calc_ Type]                              "
		+"	,[Prepayment VAT Identifier]                           "
		+"	,[Prepayment Tax Area Code]                            "
		+"	,[Prepayment Tax Liable]                               "
		+"	,[Prepayment Tax Group Code]                           "
		+"	,[Prepmt Amt to Deduct]                                "
		+"	,[Prepmt Amt Deducted]                                 "
		+"	,[Prepayment Line]                                     "
		+"	,[Prepmt_ Amount Inv_ Incl_ VAT]                       "
		+"	,[Prepmt_ Amount Inv_ (LCY)]                            "
		+"	,[IC Partner Code]                                     "
		+"	,[Prepmt_ VAT Amount Inv_ (LCY)]                        "
		+"	,[Prepayment VAT Difference]                           "
		+"	,[Prepmt VAT Diff_ to Deduct]                          "
		+"	,[Prepmt VAT Diff_ Deducted]                           "
		+"	,e.Monto[Outstanding Amt_ Ex_ VAT (LCY)]                "
		+"	,[A_ Rcd_ Not Inv_ Ex_ VAT (LCY)]                       "
		+"	,[Dimension Set ID]                                    "
		+"	,[Job Task No_]                                        "
		+"	,[Job Line Type]                                       "
		+"	,[Job Unit Price]                                      "
		+"	,[Job Total Price]                                     "
		+"	,[Job Line Amount]                                     "
		+"	,[Job Line Discount Amount]                            "
		+"	,[Job Line Discount _]                                 "
		+"	,[Job Unit Price (LCY)]                                 "
		+"	,[Job Total Price (LCY)]                                "
		+"	,[Job Line Amount (LCY)]                                "
		+"	,[Job Line Disc_ Amount (LCY)]                          "
		+"	,[Job Currency Factor]                                 "
		+"	,[Job Currency Code]                                   "
		+"	,[Job Planning Line No_]                               "
		+"	,[Job Remaining Qty_]                                  "
		+"	,[Job Remaining Qty_ (Base)]                            "
		+"	,[Deferral Code]                                       "
		+"	,[Returns Deferral Start Date]                         "
		+"	,[Prod_ Order No_]                                     "
		+"	,[Variant Code]                                        "
		+"	,[Bin Code]                                            "
		+"	,[Qty_ per Unit of Measure]                            "
		+"	,[Unit of Measure Code]                                "
		+"	,[Quantity (Base)]                                      "
		+"	,[Outstanding Qty_ (Base)]                              "
		+"	,[Qty_ to Invoice (Base)]                               "
		+"	,[Qty_ to Receive (Base)]                               "
		+"	,[Qty_ Rcd_ Not Invoiced (Base)]                        "
		+"	,[Qty_ Received (Base)]                                 "
		+"	,[Qty_ Invoiced (Base)]                                 "
		+"	,[FA Posting Date]                                     "
		+"	,[FA Posting Type]                                     "
		+"	,[Depreciation Book Code]                              "
		+"	,[Salvage Value]                                       "
		+"	,[Depr_ until FA Posting Date]                         "
		+"	,[Depr_ Acquisition Cost]                              "
		+"	,[Maintenance Code]                                    "
		+"	,[Insurance No_]                                       "
		+"	,[Budgeted FA No_]                                     "
		+"	,[Duplicate in Depreciation Book]                      "
		+"	,[Use Duplication List]                                "
		+"	,[Responsibility Center]                               "
		+"	,[Cross-Reference No_]                                 "
		+"	,[Unit of Measure (Cross Ref_)]                         "
		+"	,[Cross-Reference Type]                                "
		+"	,[Cross-Reference Type No_]                            "
		+"	,[Item Category Code]                                  "
		+"	,[Nonstock]                                            "
		+"	,[Purchasing Code]                                     "
		+"	,[Product Group Code]                                  "
		+"	,[Special Order]                                       "
		+"	,[Special Order Sales No_]                             "
		+"	,[Special Order Sales Line No_]                        "
		+"	,[Completely Received]                                 "
		+"	,[Requested Receipt Date]                              "
		+"	,[Promised Receipt Date]                               "
		+"	,[Lead Time Calculation]                               "
		+"	,[Inbound Whse_ Handling Time]                         "
		+"	,[Planned Receipt Date]                                "
		+"	,[Order Date]                                          "
		+"	,[Allow Item Charge Assignment]                        "
		+"	,[Return Qty_ to Ship]                                 "
		+"	,[Return Qty_ to Ship (Base)]                           "
		+"	,[Return Qty_ Shipped Not Invd_]                       "
		+"	,[Ret_ Qty_ Shpd Not Invd_(Base)]                      "
		+"	,[Return Shpd_ Not Invd_]                              "
		+"	,[Return Shpd_ Not Invd_ (LCY)]                         "
		+"	,[Return Qty_ Shipped]                                 "
		+"	,[Return Qty_ Shipped (Base)]                           "
		+"	,[Return Shipment No_]                                 "
		+"	,[Return Shipment Line No_]                            "
		+"	,[Return Reason Code]                                  "
		+"	,[Tax To Be Expensed]                                  "
		+"	,[Provincial Tax Area Code]                            "
		+"	,[IRS 1099 Liable]                                     "
		+"	,[GST_HST]                                             "
		+"	,[SubSource]                                           "
		+"	,[Transport Cost Entry No]                             "
		+"	,[Document Line No_]                                   "
		+"	,[Original Quantity]                                   "
		+"	,[Allow Quantity Change]                               "
		+"	,[Supply Chain Group Code]                             "
		+"	,[Lot No_]                                              "
		+"  ,[Creation Date]                                        "
		+"  ,[Supplier Lot No_]                                     "
		+"  ,[Alt_ Qty_ Transaction No_]                            "
		+"  ,[Quantity (Alt_)]                                       "
		+"  ,[Qty_ to Receive (Alt_)]                                "
		+"  ,[Qty_ Received (Alt_)]                                  "
		+"  ,[Qty_ to Invoice (Alt_)]                                "
		+"  ,[Qty_ Invoiced (Alt_)]                                  "
		+"  ,[Return Qty_ to Ship (Alt_)]                            "
		+"  ,[Return Qty_ Shipped (Alt_)]                            "
		+"  ,[Alt_ Qty_ Update Required]                            "
		+"  ,[Accrual Amount (Cost)]                                 "
		+"  ,[Accrual Plan Type]                                    "
		+"  ,[Accrual Source No_]                                   "
		+"  ,[Accrual Source Doc_ Type]                             "
		+"  ,[Accrual Source Doc_ No_]                              "
		+"  ,[Accrual Source Doc_ Line No_]                         "
		+"  ,[Scheduled Accrual No_]                                "
		+"  ,[Qty_ to Ship_Recv_ (Cont_)]                            "
		+"  ,[Receiving Reason Code]                                "
		+"  ,[Farm]                                                 "
		+"  ,[Brand]                                                "
		+"  ,[Country_Region of Origin Code]                        "
		+"  ,[Commodity Manifest No_]                               "
		+"  ,[Commodity Manifest Line No_]                          "
		+"  ,[Commodity Received Date]                              "
		+"  ,[Comm_ Payment Class Code]                             "
		+"  ,[Commodity Cost Calculated]                            "
		+"  ,[Commodity Unit Cost]                                  "
		+"  ,[Commodity Amount]                                     "
		+"  ,[Commodity Received Lot No_]                           "
		+"  ,[Commodity P_O_ Type]                                  "
		+"  ,[Producer Zone Code]                                   "
		+"  ,[Rejection Action]                                     "
		+"  ,[Label Unit of Measure Code]                           "
		+"  ,[Work Order No_]                                       "
		+"  ,[Maintenance Entry Type]                               "
		+"  ,[Maintenance Trade Code]                               "
		+"  ,[Hours]                                                "
		+"  ,[Part No_]                                             "
		+"  ,[Routing No_]                                          "
		+"  ,[Operation No_]                                        "
		+"  ,[Work Center No_]                                      "
		+"  ,[Finished]                                             "
		+"  ,[Prod_ Order Line No_]                                 "
		+"  ,[Overhead Rate]                                        "
		+"  ,[MPS Order]                                            "
		+"  ,[Planning Flexibility]                                 "
		+"  ,[Safety Lead Time]                                     "
		+"  ,[Routing Reference No_]                                "
        +"  FROM [LACABANA]..["+empresaNav+"$Purchase Line] H       "           
        +"  INNER JOIN(                                             "
        +"     SELECT                                               "
        +"      C.CUENTA                                            "
        +"     , G.CECO                                             "
        +"     , A.[External Document No_]                          "
        +"     , E.FacturaCompra                                    "
            +"   , ROW_NUMBER() OVER (PARTITION BY  A.[External Document No_]    ORDER BY  A.[External Document No_] ASC) Linea                  "
			+"	,sum(B.Amount) Monto                                                                                                             "
			+"	,sum(B.Amount)*1.13 MontoIVA                                                                                                     "
            +"   FROM LACABANA..[LACABANA$Sales Invoice Header] A                                                                                "
            +"   INNER JOIN LACABANA..[LACABANA$Sales Invoice Line] B ON A.No_=B.[Document No_]                                                  "
            +"   INNER JOIN RCONTA..TARIFASERV C                       ON B.No_=C.ITEMNMBR COLLATE Latin1_General_100_CS_AS                      "
            +"   INNER JOIN RCONTA..SERVMAQ AS D ON  D.FACTURA = a.[External Document No_] COLLATE Latin1_General_100_CS_AS                      "
            +"   INNER JOIN LCMAESTROZAF..SAIPLUS_EmpresaNav E ON E.CodClie = b.[Sell-to Customer No_] COLLATE Latin1_General_100_CS_AS        "
            +"   INNER JOIN LCMAESTROZAF..ZAFLOTE AS F ON F.LOTE = D.LOTEID                                                                      "
            +"   INNER JOIN (SELECT DISTINCT CodLote, CECO, EmpresaNav FROM LCMAESTROZAF..SAIPLUS_BOM ) AS G ON g.CodLote = F.CODLOTE_UNICO_ID   "
            +"  WHERE                                                                                                                            "
            +"      A.[Customer Posting Group]= 'CLIEN-AFIL'                                                                                     "
            +"  AND A.[Posting Date] > '25/05/2020'                                                                                              "
            +"  AND E.EmpresaNav = '"+empresaNav+"'                                                                                               "
            +"  AND a.[External Document No_] = '"+ external_Document_No_ +"'                                                                                "
            +"  GROUP BY                                                                                                                         "
            +"   C.CUENTA                                                                                                                        "
            +"  , CECO                                                                                                                           "
            +"  , A.[External Document No_]                                                                                                      "
            +"  , E.FacturaCompra                                                                                                                "
            +")  as E on h.[Document No_]= E.FacturaCompra COLLATE Latin1_General_100_CS_AS";

            return (sql);
        }

        private bool comandoSql(string sql)
        {
            string connetionString = null;
            SqlConnection cnn;
            SqlCommand command;
            //SqlDataReader dataReader;
            connetionString = "Data Source=10.1.1.24;Initial Catalog=LCMAESTROZAF;User ID=su;Password=20IlcSql13";
            cnn = new SqlConnection(connetionString);
            try
            {
                cnn.Open();
                Console.WriteLine("Connection Open ! ");
                command = new SqlCommand(sql, cnn);

                command.ExecuteNonQuery();
                cnn.Close();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error>>>>>>>>>>>>>>>>.! " + ex.Message);
                
            }
            return false;
        }

        [WebMethod]                                                                                                                              
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]//Specify return format.
        public String PLwebIngresoManoObra(String OrdenTrabajo, String Recurso, String totalHoras, String CodActivo, String Fase)//Decimal cantidad, Decimal costoart, string codArticulo, string MovBodega, String UnidadMedida
        {
            try
            {
        
                InterfaceEAM interfaseEAM = new InterfaceEAM();

                //interfaseEAM.Credentials = CredentialCache.DefaultNetworkCredentials;//ECLA0023
                interfaseEAM.Credentials = new System.Net.NetworkCredential("nav.services", "Navucodonosor", "CABANA");
                DateTime now = DateTime.Now;
                //Console.WriteLine(now);

                interfaseEAM.InsertDetailsServices(OrdenTrabajo, 2, Recurso, Convert.ToDecimal(totalHoras), "AR", CodActivo, Fase, now, now, 10000,"");//, now, now
                //interfaseEAM.InsertDetailsServices("SM-000022", 2, "RE-941", Convert.ToDecimal(22.22), "AR", CodActivo, "FASE  DE EJEMPLO");

            }
            catch (Exception ex)
            {
                return "{\"Numero\":\"0\"}" + ex;
            }

            return "{\"codArticulo\":\"1\", \"DocType\":\"1\"}";
        }


        [WebMethod]
        public string send()
        {
            // Indica el mensaje que se le va a retornar al usuario al final de la
            // operación. "OK" significa que el envío del mensaje fue exitoso.
            string response = "OK";

            //Desarrollo de un servicio web para el envío de correos utilizando C# y su correspondiente cliente consumidor utilizando Java,
            // Se crea el objeto para el envío de correos electrónicos indicándole
            // la dirección del servidor SMTP a utilizarse.
            //SmtpClient client = new SmtpClient(smtpServer);
            string smtpAddress = "smtp.bizmail.yahoo.com";//"smtp.bizmail.yahoo.com";
            int portNumber = 25;
            bool enableSSL = true;
            SmtpClient client = new SmtpClient(smtpAddress, portNumber);

            client.Credentials = new System.Net.NetworkCredential("notificaciones@ilcabana.com", "Cabana+1");
            client.EnableSsl = enableSSL;

            // Se especifica la dirección del remitente del mensaje.
            MailAddress from = new MailAddress("notificaciones@ilcabana.com");

            // Se especifica la dirección del receptor del mensaje.
            MailAddress to = new MailAddress("jreyes@ilcabana.com");
            // Se especifica el contenido del mensaje.
            MailMessage message = new MailMessage();
            //MailMessage message = new MailMessage(from, to);
            message.From = new MailAddress("notificaciones@ilcabana.com");
            message.To.Add("jreyes@ilcabana.com");
            message.Body = "cuerpo de mensaje" + Environment.NewLine + Environment.NewLine;
            message.Body += "Mensaje enviado utilizando el servicio web MailService.";
            message.BodyEncoding = System.Text.Encoding.UTF8;
            // Se especifica el tema del mensaje.
            message.Subject = "subject";
            message.SubjectEncoding = System.Text.Encoding.UTF8;
            // Se realiza el envío el mensaje creado anteriormente.
            try
            {
                client.Send(message);
            }
            catch (SmtpException smtpe)
            {
                // En caso de suceder problemas enviando el mensaje, la descripción del
                // problema sucedido es enviada al cliente que consumió el servicio.
                response = smtpe.Message;
            }
            // Se realiza la limpieza de los recursos.
            message.Dispose();
            // Se retorna el mensaje de respuesta: "OK" o el mensaje de error sucedido.
            return response;
        }


        [WebMethod]
        public string enviarCorreoElectronicos(String emailEnviarA, String subject, String bodyTitulo, String bodyMensaje)
        {
            // Indica el mensaje que se le va a retornar al usuario al final de la
            // operación. "OK" significa que el envío del mensaje fue exitoso.
            string response = "OK";

            // Se crea el objeto para el envío de correos electrónicos indicándole
            // la dirección del servidor SMTP a utilizarse.
            //SmtpClient client = new SmtpClient(smtpServer);
            /*
                        string smtpAddress = "smtp.bizmail.yahoo.com";//"smtp.bizmail.yahoo.com";
                        int portNumber = 25;
                        bool enableSSL = true;
                        SmtpClient client = new SmtpClient(smtpAddress, portNumber);
                        client.Credentials = new System.Net.NetworkCredential("notificaciones@ilcabana.com", "Cabana+1");
            */
            string smtpAddress = "smtp.gmail.com";
            int portNumber = 587;
            bool enableSSL = true;
            SmtpClient client = new SmtpClient(smtpAddress, portNumber);
            client.Credentials = new System.Net.NetworkCredential("navisionilc@gmail.com", "cjctipcqjkunnbfy");
            client.EnableSsl = enableSSL;

            ServicePointManager.ServerCertificateValidationCallback =
            delegate (object s, X509Certificate certificate,
              X509Chain chain, SslPolicyErrors sslPolicyErrors)
            { return true; };


            // Se especifica el contenido del mensaje.
            MailMessage message = new MailMessage();
            //MailMessage message = new MailMessage(from, to);
            message.From = new MailAddress("notificaciones@ilcabana.com");
            message.To.Add(""+ emailEnviarA);//jreyes@ilcabana.com
            message.Body = ""+ bodyTitulo + Environment.NewLine + Environment.NewLine;
            message.Body += ""+ bodyMensaje;//Mensaje enviado utilizando el servicio web MailService.
            message.BodyEncoding = System.Text.Encoding.UTF8;
            message.IsBodyHtml = true;


            // Se especifica el tema del mensaje.
            message.Subject = ""+ subject;
            message.SubjectEncoding = System.Text.Encoding.UTF8;
            // Se realiza el envío el mensaje creado anteriormente.
            try
            {
                client.Send(message);
            }
            catch (SmtpException smtpe)
            {
                // En caso de suceder problemas enviando el mensaje, la descripción del
                // problema sucedido es enviada al cliente que consumió el servicio.
                response = smtpe.Message;
            }
            // Se realiza la limpieza de los recursos.
            message.Dispose();
            // Se retorna el mensaje de respuesta: "OK" o el mensaje de error sucedido.
            return response;
        }


    }
}
