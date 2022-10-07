using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using Cabana.Campo.RAAgricola.WS.ServiceRefNav2018;
using System.Web.Script.Services;
using System.Text.RegularExpressions;
using System.Data.SqlClient;

namespace Cabana.Campo.RAAgricola.WS
{
    /// <summary>
    /// Descripción breve de WSRAAgricola_N2018
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
    // [System.Web.Script.Services.ScriptService]
    public class WSRAAgricola_N2018 : System.Web.Services.WebService
    {
        string connetionString = "Data Source=10.1.1.24;Initial Catalog=LCMAESTROZAF;User ID=su;Password=XqzhT16as$21Suu";
        //string connetionString2 = "Data Source=10.1.1.24;Initial Catalog=LACABANA;User ID=su;Password=XqzhT16as$21Suu";
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]//Specify return format.
        public String PLwebEncPedidoVenta_Fac(string cliente, string factura, decimal numReqProv, string numLote, string Description, string tipoDocumento)

        {
            string numero = "";
            string DocType = "";
            //string subtipo = "CCF-V";
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

                encabezado.SubType = tipoDocumento;
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
                string Mensaje2 = Convert.ToString(ex.InnerException).Replace('"', ' ');

                string Mensaje = Regex.Replace(Mensaje2, @"[^\w\d]", " ");
                return "{\"Numero\":\"" + Mensaje + "\"}";
                //return "{\"Numero\":\"fallo\"}" + ex;
            }

            return "{\"Numero\":\"" + numero + "\", \"DocType\":\"" + DocType + "\"}";
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]//Specify return format.
        public String PLwebDetallePedidoVenta_Fac(string numero, string DocType, string cantidad, string precioUnidad, string codArticulo, string MovBodega, string CentroBeneficio, String ZonaServicio, String CodVariante)
        {
            try
            {
                //NAV nav = new NAV(new Uri("http://10.1.1.23:7048/DynamicsNAV80/OData/Company('LACABANA')"));
                //NAV nav = new NAV(new Uri("http://10.1.1.23:1048/CABANA2016/OData/Company('"+ Empresa + "')"));
                NAV nav = new NAV(new Uri("http://10.1.1.14:7048/DynamicsNav110/OData/Company('LACABANA')"));

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
                /*
                linea.Document_No = encabezado.No;
                linea.Document_Type = encabezado.Document_Type;
                linea.Type = "2"; //item
                linea.No = "16008";
                linea.Location_Code = "AUERBACH";
                linea.Variant_Code = "AUERBACH";
                linea.Description_2 = "Tanque Auerbach";
                linea.Quantity = Decimal.Parse(dtCombustible[i].CANTIDAD.ToString());
                linea.Unit_Price = dtCombustible[i].PUNIT;
                linea.ShortcutDimCode_x005B_3_x005D_ = "6110";
                linea.ShortcutDimCode_x005B_5_x005D_ = "7002";
                */
                nav.AddToPedidoVentaSalesLines(linea);
                nav.SaveChanges();

            }
            catch (Exception ex)
            {
                string Mensaje2 = Convert.ToString(ex.InnerException).Replace('"', ' ');

                string Mensaje = Regex.Replace(Mensaje2, @"[^\w\d]", " ");
                return "{\"Numero\":\"" + Mensaje + "\"}";
            }

            return "{\"Numero\":\"" + codArticulo + "\", \"DocType\":\"" + DocType + "\"}";
        }


        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]//Specify return format.
        public String PLwebEncFacServicios(string cliente, string factura, decimal numReqProv, String numLote, string tipoDocumento)
        {
            string numero = "";
            string DocType = "";
            try
            {
                //NAV nav = new NAV(new Uri("http://10.1.1.23:7048/DynamicsNAV80/OData/Company('LACABANA')"));
                //NAV nav = new NAV(new Uri("http://10.1.1.23:1048/CABANA2016/OData/Company('LACABANA')"));
                NAV nav = new NAV(new Uri("http://10.1.1.14:7048/DynamicsNav110/OData/Company('LACABANA')"));

                //nav.Credentials = CredentialCache.DefaultNetworkCredentials;
                nav.Credentials = new System.Net.NetworkCredential("nav.services", "Navucodonosor", "CABANA");
                PedidoVenta encabezado = new PedidoVenta();
                PedidoVentaSalesLines linea = new PedidoVentaSalesLines();
                encabezado.Sell_to_Customer_No = cliente;
                encabezado.No = factura;
                encabezado.Posting_Description = "Servicio de Maquinaria Agricola";
                encabezado.External_Document_No = factura;
                encabezado.ExtendDec1 = numReqProv;
                encabezado.ExtendTxt5 = numLote;
                encabezado.SubType = tipoDocumento;
                encabezado.Document_Type = "1";
                
                if (  cliente == "AAZUC001"
                   || cliente == "PBELE001"
                   || cliente == "BURUC001"
                   || cliente == "SOPTI001"
                   || cliente == "SSERV001"
                   || cliente == "SSERV041"
                   || cliente == "TANAT001"
                   || cliente == "TECOM001"
                   || cliente == "TETUN001"
                   || cliente == "ZZAFR001"
                    ) {
                    encabezado.Payment_Terms_Code = "60D";
                }
                

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
        public String PLwebDetalleFacServicios(string numero, string DocType, string cantidad, string precioUnidad, string codArticulo, string aaTrxDimCode, String ACTNUMST, String descItem, String codCliente)
        {
            String Codigo = "";
            try
            {
                //NAV nav = new NAV(new Uri("http://10.1.1.23:1048/CABANA2016/OData/Company('LACABANA')"));
                NAV nav = new NAV(new Uri("http://10.1.1.14:7048/DynamicsNav110/OData/Company('LACABANA')"));


                nav.Credentials = new System.Net.NetworkCredential("nav.services", "Navucodonosor", "CABANA");
                PedidoVenta encabezado = new PedidoVenta();
                PedidoVentaSalesLines linea = new PedidoVentaSalesLines();

                /////////////Ingresamos el Impuesto    
                linea.Document_No = numero;
                linea.Document_Type = DocType;
                linea.Type = "2"; //item
                linea.No = codArticulo;
                linea.Quantity = Decimal.Parse(cantidad);
                linea.Unit_Price = Decimal.Parse(precioUnidad);

                linea.ShortcutDimCode_x005B_3_x005D_ = aaTrxDimCode;//"6109";
                linea.ShortcutDimCode_x005B_4_x005D_ = ACTNUMST;//"6109";
                linea.ShortcutDimCode_x005B_5_x005D_ = "7002";
                Codigo = codCliente;
                
                string intercompany = getIntercompany(codCliente);
                if (!intercompany.Equals(""))
                {
                    linea.ShortcutDimCode_x005B_8_x005D_ = "15";
                }
                
                //linea.ShortcutDimCode_x005B_8_x005D_ = "15";

                linea.Description = descItem;
                nav.AddToPedidoVentaSalesLines(linea);
                nav.SaveChanges();

                if (!intercompany.Equals(""))
                {
                    setDimensionEncabezado(numero, "VENTA");
                }

            }
            catch (Exception ex)
            {
                return "{\"Numero\":\"fallo\"}" + ex;
            }

            return "{\"Numero\":\"" + codArticulo + "\", \"DocType\":\"" + DocType + "\"}";
        }


        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]//Specify return format.
        public String PLwebEncFacServicios_Zafra(string Puerto_Servidor, string EmpresaNav, string cliente, string factura, decimal numReqProv, String numLote, string tipoDocumento, string formaPago)
        {
            string numero = "";
            string DocType = "";
            try
            {

                NAV nav = new NAV(new Uri("http://10.1.1.14:" + Puerto_Servidor + "/OData/Company('" + EmpresaNav + "')"));

                //nav.Credentials = CredentialCache.DefaultNetworkCredentials;
                nav.Credentials = new System.Net.NetworkCredential("nav.services", "Navucodonosor", "CABANA");
                PedidoVenta encabezado = new PedidoVenta();
                PedidoVentaSalesLines linea = new PedidoVentaSalesLines();
                encabezado.Sell_to_Customer_No = cliente;
                encabezado.No = factura;
                encabezado.Posting_Description = "Servicio de Maquinaria Agricola";
                encabezado.External_Document_No = factura;
                encabezado.ExtendDec1 = numReqProv;
                encabezado.ExtendTxt5 = numLote;
                encabezado.SubType = tipoDocumento;
                encabezado.Payment_Method_Code = formaPago;
                encabezado.Document_Type = "1";

                if (cliente == "AAZUC001"
                   || cliente == "PBELE001"
                   || cliente == "BURUC001"
                   || cliente == "SOPTI001"
                   || cliente == "SSERV001"
                   || cliente == "SSERV041"
                   || cliente == "TANAT001"
                   || cliente == "TECOM001"
                   || cliente == "TETUN001"
                   || cliente == "ZZAFR001"
                    )
                {
                    encabezado.Payment_Terms_Code = "60D";
                }


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
        public String PLwebDetalleFacServicios_Zafra(string Puerto_Servidor, string EmpresaNav, string numero, string DocType, string cantidad, string precioUnidad, string codArticulo, string aaTrxDimCode, String CEBE, String descItem, String codCliente, string TypeLinea, string Dim05)
        {
            String Codigo = "";
            try
            {
                NAV nav = new NAV(new Uri("http://10.1.1.14:" + Puerto_Servidor + "/OData/Company('" + EmpresaNav + "')"));


                nav.Credentials = new System.Net.NetworkCredential("nav.services", "Navucodonosor", "CABANA");
                PedidoVenta encabezado = new PedidoVenta();
                PedidoVentaSalesLines linea = new PedidoVentaSalesLines();

                /////////////Ingresamos el Impuesto    
                linea.Document_No = numero;
                linea.Document_Type = DocType;
                linea.Type = TypeLinea; //item
                linea.No = codArticulo;
                linea.Quantity = Decimal.Parse(cantidad);
                linea.Unit_Price = Decimal.Parse(precioUnidad);

                string intercompany = getIntercompany(codCliente);

                if (EmpresaNav.Equals("LACABANA"))
                {
                    linea.ShortcutDimCode_x005B_3_x005D_ = aaTrxDimCode;//"6109";
                    linea.ShortcutDimCode_x005B_4_x005D_ = CEBE;//"6109";
                    linea.ShortcutDimCode_x005B_5_x005D_ = Dim05;
                    linea.ShortcutDimCode_x005B_6_x005D_ = intercompany;

                }
                else {
                    linea.ShortcutDimCode_x005B_3_x005D_ = aaTrxDimCode;//SERVICIO
                    linea.ShortcutDimCode_x005B_4_x005D_ = CEBE;//CEBE

                    //Codigo = codCliente;
                }

                if (!intercompany.Equals(""))
                {
                    linea.ShortcutDimCode_x005B_8_x005D_ = "15";
                }

                linea.Description = descItem;
                nav.AddToPedidoVentaSalesLines(linea);
                nav.SaveChanges();

                if (!intercompany.Equals(""))
                {
                    setDimensionEncabezado(numero, "VENTA");
                }


            }
            catch (Exception ex)
            {
                return "{\"Numero\":\"fallo\"}" + ex;
            }

            return "{\"Numero\":\"" + codArticulo + "\", \"DocType\":\"" + DocType + "\"}";
        }



        private String getIntercompany(string Cliente)
        {
            String Codigo = "";
            String sql = "exec getDimensionIntercompany '" + Cliente + "' ";
            SqlConnection cnn;
            SqlCommand command;
            // SqlDataReader dataReader;
            cnn = new SqlConnection(connetionString);
            try
            {
                cnn.Open();
                Console.WriteLine("Connection Open ! ");
                command = new SqlCommand(sql, cnn);

                Codigo = command.ExecuteScalar().ToString(); ;
                cnn.Close();

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error>>>>>>>>>>>>>>>>.! " + ex.Message);

            }
            return Codigo;
        }
        private String getTRelacionada(string Documento)
        {
            String Codigo = "";
            String sql = "exec getTRelacionada '" + Documento + "' ";
            SqlConnection cnn;
            SqlCommand command;
            // SqlDataReader dataReader;
            cnn = new SqlConnection(connetionString);
            try
            {
                cnn.Open();
                Console.WriteLine("Connection Open ! ");
                command = new SqlCommand(sql, cnn);

                Codigo = command.ExecuteScalar().ToString(); ;
                cnn.Close();

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error>>>>>>>>>>>>>>>>.! " + ex.Message);

            }
            return Codigo;
        }
        private String setDimensionEncabezado(string Documento, string Modulo)
        {
            String Codigo = "";
            String sql = "EXEC LACABANA..SP_UpdateEncDimensionSet   '" + Documento + "', '" + Modulo + "', 'LACABANA'     ";
            SqlConnection cnn;
            SqlCommand command;
            // SqlDataReader dataReader;
            cnn = new SqlConnection(connetionString);
            try
            {
                cnn.Open();
                Console.WriteLine("Connection Open ! ");
                command = new SqlCommand(sql, cnn);

                Codigo = command.ExecuteScalar().ToString(); ;
                cnn.Close();

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error>>>>>>>>>>>>>>>>.! " + ex.Message);

            }
            return Codigo;
        }


        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]//Specify return format.
        public String PLwebEncFac(string cliente, string factura, decimal numReqProv, String numLote, String Description)
        {
            string numero = "";
            string DocType = "";
            string tipoDocumento = "CCF-V";
            try
            {

                NAV nav = new NAV(new Uri("http://10.1.1.14:7048/DynamicsNav110/OData/Company('LACABANA')"));

                //nav.Credentials = CredentialCache.DefaultNetworkCredentials;
                nav.Credentials = new System.Net.NetworkCredential("nav.services", "Navucodonosor", "CABANA");
                PedidoVenta encabezado = new PedidoVenta();
                PedidoVentaSalesLines linea = new PedidoVentaSalesLines();
                encabezado.Sell_to_Customer_No = cliente;
                encabezado.No = factura;
                encabezado.External_Document_No = factura;
                encabezado.Posting_Description = Description;
                encabezado.ExtendDec1 = numReqProv;
                encabezado.ExtendTxt5 = numLote;
                encabezado.Document_Type = "1";
                encabezado.SubType = tipoDocumento;

                
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

                NAV nav = new NAV(new Uri("http://10.1.1.14:7048/DynamicsNav110/OData/Company('LACABANA')"));

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
        public String PedidoVenta_Enc(string Puerto_Servidor, string EmpresaNav, string cliente, string factura, string Description, string Document_Type, string SubType, string FechaRegistro)

        {
            string numero;
            string DocType;
            //string subtipo = "CCF-V";
            try
            {
                //NAV nav = new NAV(new Uri("http://10.1.1.14:7048/DynamicsNav110/OData/Company('" + EmpresaNav + "')"));
                NAV nav = new NAV(new Uri("http://10.1.1.14:" + Puerto_Servidor + "/OData/Company('" + EmpresaNav + "')"));

                //nav.Credentials = CredentialCache.DefaultNetworkCredentials;
                nav.Credentials = new System.Net.NetworkCredential("nav.services", "Navucodonosor", "CABANA");
                PedidoVenta encabezado = new PedidoVenta();
                PedidoVentaSalesLines linea = new PedidoVentaSalesLines();

                encabezado.Sell_to_Customer_No = cliente;
                encabezado.No = factura;
                encabezado.External_Document_No = "ASIGNARCCF";
                encabezado.Posting_Description = Description;
                encabezado.ExtendDec1 = 0;
                encabezado.ExtendTxt5 = "";
                encabezado.Document_Type = Document_Type;
                encabezado.SubType = SubType;
                encabezado.Posting_Date = DateTime.Parse(FechaRegistro);

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

    }
}
