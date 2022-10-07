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
    /// Descripción breve de ServicioTelefonia_Sai
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
    // [System.Web.Script.Services.ScriptService]
    public class ServicioTelefonia_Sai : System.Web.Services.WebService
    {
        string connetionString = "Data Source=10.1.1.24;Initial Catalog=LCMAESTROZAF;User ID=su;Password=XqzhT16as$21Suu";
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

                encabezado.Sell_to_Customer_No  = cliente;
                encabezado.No                   = factura;
                encabezado.External_Document_No = "ASIGNARCCF";
                encabezado.Posting_Description  = Description;
                encabezado.ExtendDec1           = 0;
                encabezado.ExtendTxt5           = "";
                encabezado.Document_Type        = Document_Type;
                encabezado.SubType              = SubType;
                encabezado.Posting_Date         = DateTime.Parse(FechaRegistro);

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
                return "{\"Numero\":\"fallo\", \"Mensaje\":\"" + Mensaje + "\", \"EmpresaNav\":\"" + EmpresaNav + "\"}";
            }

            return "{\"Numero\":\"" + numero + "\", \"DocType\":\"" + DocType + "\", \"EmpresaNav\":\"" + EmpresaNav + "\"}";
        }



        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]//Specify return format.
        public String PedidoVenta_Detalle(string Puerto_Servidor, string EmpresaNav, string Document_No, string Document_Type, string No, string Location_Code,
            string Quantity, string Unit_Price, string Description, String Qty_to_Invoice, String Qty_to_Ship, string CEBE, String TypeLine)
        {
            try
            {
                NAV nav = new NAV(new Uri("http://10.1.1.14:" + Puerto_Servidor + "/OData/Company('" + EmpresaNav + "')"));

                //nav.Credentials = CredentialCache.DefaultNetworkCredentials;
                nav.Credentials = new System.Net.NetworkCredential("nav.services", "Navucodonosor", "CABANA");
                PedidoVenta encabezado = new PedidoVenta();
                PedidoVentaSalesLines linea = new PedidoVentaSalesLines();


                /////////////Ingresamos el Impuesto    
                linea.Document_No       = Document_No;
                linea.Document_Type     = Document_Type;
                linea.Type              = TypeLine; //item
                linea.No                = No;
                linea.Location_Code     = Location_Code;
                linea.Quantity          = Decimal.Parse(Quantity);
                linea.Unit_Price        = Decimal.Parse(Unit_Price);//0.30m;
                linea.Description       = Description;
                linea.Unit_of_Measure   = "UNIDAD";
                linea.VAT_Prod_Posting_Group = "IVA-B";
                linea.Qty_to_Invoice    = Decimal.Parse(Qty_to_Invoice);
                linea.Qty_to_Ship       = Decimal.Parse(Qty_to_Ship);

                string intercompany = getIntercompany(encabezado.Sell_to_Customer_No);
                string trelacionadas = getTRelacionada(encabezado.No);

                if (EmpresaNav == "LACABANA")
                {
                    linea.ShortcutDimCode_x005B_3_x005D_ = CEBE;
                    linea.ShortcutDimCode_x005B_5_x005D_ = "7002";
                    linea.ShortcutDimCode_x005B_6_x005D_ = intercompany;
                    linea.ShortcutDimCode_x005B_8_x005D_ = trelacionadas;//juan
                }
                else
                {
                    linea.ShortcutDimCode_x005B_4_x005D_ = CEBE;
                    linea.ShortcutDimCode_x005B_6_x005D_ = "001";
                    linea.ShortcutDimCode_x005B_6_x005D_ = intercompany;
                    linea.ShortcutDimCode_x005B_8_x005D_ = trelacionadas;
                }

                nav.AddToPedidoVentaSalesLines(linea);
                nav.SaveChanges();

                if (!intercompany.Equals(""))
                {
                    setDimensionEncabezado(Document_No, "VENTA", EmpresaNav);
                }

            }
            catch (Exception ex)
            {
                string Mensaje = Convert.ToString(ex).Replace('"', ' ');
                return "{\"Numero\":\"fallo\", \"Mensaje\":\"" + Mensaje + "\"}";
            }

            return "{\"Numero\":\"" + No + "\", \"DocType\":\"" + Document_Type + "\", \"Document_No\":\"" + Document_No + "\"}";
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
        private String setDimensionEncabezado(string Documento, string Modulo, string EmpresaNav)
        {
            String Codigo = "";
            String sql = "EXEC LACABANA..SP_UpdateEncDimensionSet   '" + Documento + "', '" + Modulo + "', '" + EmpresaNav + "'  ";
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
        public String FacturaCompra_Enc(string Puerto_Servidor, string EmpresaNav, string Document_Type, string No, string Buy_from_Vendor_No, string Pay_to_Vendor_No, string Posting_Description, string Vendor_Invoice_No, string Unic_No, string SubType)
        {
            string numero;
            string DocType;
            try
            {
                NAV nav = new NAV(new Uri("http://10.1.1.14:" + Puerto_Servidor + "/OData/Company('" + EmpresaNav + "')"));

                //nav.Credentials = CredentialCache.DefaultNetworkCredentials;
                nav.Credentials = new System.Net.NetworkCredential("nav.services", "Navucodonosor", "CABANA");
                //PedidoVenta encabezado = new PedidoVenta();
                InvoicePurchase encabezado = new InvoicePurchase();

                //PedidoVentaSalesLines linea = new PedidoVentaSalesLines();   
                encabezado.Document_Type = Document_Type;
                encabezado.No = No;
                encabezado.Buy_from_Vendor_No = Buy_from_Vendor_No;
                encabezado.Pay_to_Vendor_No = Pay_to_Vendor_No;
                encabezado.Posting_Description = Posting_Description;
                encabezado.Vendor_Invoice_No = Vendor_Invoice_No;
                encabezado.Unic_No = Unic_No;
                encabezado.SubType = SubType;


                nav.AddToInvoicePurchase(encabezado);

                nav.SaveChanges();  //con esto obtengo el numero de factura
                numero = encabezado.No;
                DocType = encabezado.Document_Type;
            }

            catch (Exception ex)
            {
                string Mensaje2 = Convert.ToString(ex.InnerException).Replace('"', ' ');
                string Mensaje = Regex.Replace(Mensaje2, @"[^\w\d]", " ");
                return "{\"Numero\":\"fallo\", \"Mensaje\":\"" + Mensaje + "\", \"EmpresaNav\":\"" + EmpresaNav + "\"}";
            }

            return "{\"Numero\":\"" + numero + "\", \"DocType\":\"" + DocType + "\", \"EmpresaNav\":\"" + EmpresaNav + "\"}";

        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]//Specify return format.
        public String FacturaCompra_Lineas(string Puerto_Servidor, string EmpresaNav, string Document_Type, string Document_No, string Type, string No,
            string Location_Code, string Quantity, string Direct_Unit_Cost, string Unit_Cost_LCY, string Line_Amount, string Shortcut_Dimension_1_Code,
            string Shortcut_Dimension_2_Code, String Dim_SERVICIO, String Dim_TPRODUC)
        {
            string numero;
            //string DocType;
            try
            {
                NAV nav = new NAV(new Uri("http://10.1.1.14:" + Puerto_Servidor + "/OData/Company('" + EmpresaNav + "')"));

                //nav.Credentials = CredentialCache.DefaultNetworkCredentials;
                nav.Credentials = new System.Net.NetworkCredential("nav.services", "Navucodonosor", "CABANA");
                //PedidoVenta encabezado = new PedidoVenta();
                InvoicePurchase encabezado = new InvoicePurchase();
                InvoicePurchasePurchLines lineas = new InvoicePurchasePurchLines();
   
                lineas.Document_Type = Document_Type;
                lineas.Document_No = Document_No;
                lineas.Type = Type;
                lineas.No = No;
                lineas.Location_Code = Location_Code;
                lineas.Quantity = Decimal.Parse(Quantity);
                lineas.Direct_Unit_Cost = Decimal.Parse(Direct_Unit_Cost);
                lineas.Unit_Cost_LCY = Decimal.Parse(Unit_Cost_LCY);
                lineas.Line_Amount = Decimal.Parse(Line_Amount);
                lineas.Shortcut_Dimension_1_Code = Shortcut_Dimension_1_Code;
                lineas.Shortcut_Dimension_2_Code = Shortcut_Dimension_2_Code;
                           
                lineas.ShortcutDimCode_x005B_3_x005D_ = Dim_SERVICIO;
                lineas.ShortcutDimCode_x005B_6_x005D_ = Dim_TPRODUC;

                string intercompany = getIntercompany(encabezado.Buy_from_Vendor_No);
                string trelacionadas = getTRelacionada(encabezado.No);
                if (EmpresaNav == "LACABANA")
                {
                    lineas.ShortcutDimCode_x005B_6_x005D_ = intercompany;
                    lineas.ShortcutDimCode_x005B_8_x005D_ = trelacionadas;
                }
                else
                {
                    lineas.ShortcutDimCode_x005B_5_x005D_ = intercompany;
                    lineas.ShortcutDimCode_x005B_8_x005D_ = trelacionadas;
                }

                //lineas.s
                //lineas.Description = "PRUEBA";

                nav.AddToInvoicePurchasePurchLines(lineas);

                nav.SaveChanges();  //con esto obtengo el numero de factura
                numero = lineas.No;

                if (!intercompany.Equals(""))
                {
                    setDimensionEncabezado(Document_No, "COMPRA", EmpresaNav);
                }
            }


            catch (Exception ex)
            {
                string Mensaje = Convert.ToString(ex.InnerException).Replace('"', ' ');
                return "{\"Numero\":\"fallo\", \"Mensaje\":\"" + Mensaje + "\", \"EmpresaNav\":\"" + EmpresaNav + "\"}";
            }

            return "{\"Numero\":\"" + numero + "\", \"DocType\":\"" + EmpresaNav + "\", \"EmpresaNav\":\"" + EmpresaNav + "\"}";
        }
    }
}
