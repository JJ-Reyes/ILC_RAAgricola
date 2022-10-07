using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

using System.Web.Script.Services;
using Cabana.Campo.RAAgricola.DAL.DS;
using Cabana.Campo.RAAgricola.DAL.DS.DS_SAIPLUSTableAdapters;
using System.Data.SqlClient;
using Cabana.Campo.RAAgricola.WS.ServiceRefNav2018;
//using Cabana.Campo.RAAgricola.WS.SalesLinesTableAdapters;
using System.Data;

namespace Cabana.Campo.RAAgricola.WS
{
    /// <summary>
    /// Descripción breve de SaiPlus_ServMaq
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
    // [System.Web.Script.Services.ScriptService]
    public class SaiPlus_ServMaq : System.Web.Services.WebService
    {
        string connetionString = "Data Source=10.1.1.24;Initial Catalog=LCMAESTROZAF;User ID=su;Password=XqzhT16as$21Suu";
        String DBDestino = "LACABANA";
        String URLNav = "";

        [WebMethod]
        public string HelloWorld()
        {
            return "Hola a todos";
        }

        /*Esta web services sube insumos de fertilizantes*/
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]//Specify return format.
        public String PLwebFacturaCompraIC_SAUL(string Puerto_Servidor, string EmpresaNav, string db, string Documento, string usuario, string Buy_from_Vendor_No, string Pay_to_Vendor_No)
        {
            string url = "";
            //string db = "";
            //string empresaNAV = "";

            //  DS_SAIPLUS.SAIPLUS_SubirFacturaCompraDataTable fc = new DS_SAIPLUS.SAIPLUS_SubirFacturaCompraDataTable();
            //  SAIPLUS_SubirFacturaCompraTableAdapter ta = new SAIPLUS_SubirFacturaCompraTableAdapter();

            // ta.Fill(fc, Documento);
            //string facturaEmpresa = "";
            String nuevoDocInterno;
            /*
            SalesLines.SalesLinesDataTable lines = new SalesLines.SalesLinesDataTable();
            SalesLinesTableAdapter taLines = new SalesLinesTableAdapter();
            taLines.Fill(lines, Documento);
            */
            /*
            SalesLines.InfoSAIPlusEmpresaDataTable empresa = new SalesLines.InfoSAIPlusEmpresaDataTable();
            InfoSAIPlusEmpresaTableAdapter taEmpresa = new InfoSAIPlusEmpresaTableAdapter();
            

            taEmpresa.Fill(empresa, Documento);
            empresaNAV = empresa[0].EmpresaNav;
            */
            /*
            if (entorno == "Produccion")
            {
                db = "LACABANA";
                url = "http://10.1.1.14:7048/DynamicsNAV110/OData/Company('" + EmpresaNav + "')";
            }
            if (entorno == "Taller")
            {
                db = "LACABANATALLER";
                url = "http://10.1.1.14:7058/taller/OData/Company('" + EmpresaNav + "')";
            }
            */

            url = "http://10.1.1.14:"+ Puerto_Servidor + "/OData/Company('" + EmpresaNav + "')";

            /*
            SalesLines.LACABANAVendorDataTable vendor = new SalesLines.LACABANAVendorDataTable();
            LACABANAVendorTableAdapter tavendor = new LACABANAVendorTableAdapter();
            tavendor.Fill(vendor,"INGENI001");*/
            try
            {

                NAV nav = new NAV(new Uri(url));
                //Producion NAV nav = new NAV(new Uri("http://10.1.1.23:1048/CABANA2016/OData/Company('LACABANA')"));
                nav.Credentials = new System.Net.NetworkCredential("nav.services", "Navucodonosor", "CABANA");
                InvoicePurchase encabezado = new InvoicePurchase();
                //facturaEmpresa = empresa[0].FacturaCompra;
                //encabezado.No = "PEDIDOSD";//empresa[0].FacturaComp;
                encabezado.Buy_from_Vendor_No = Buy_from_Vendor_No;//empresa[0].CABANA;
                encabezado.Vendor_Invoice_No = Documento;
                encabezado.Document_Type = "2";
                encabezado.Pay_to_Vendor_No = Pay_to_Vendor_No;//empresa[0].CABANA;
                encabezado.Unic_No = Documento;
                encabezado.Posting_Description = "";
                encabezado.SubType = "CCF-C";
                nav.AddToInvoicePurchase(encabezado);
                nav.SaveChanges();

                nuevoDocInterno = encabezado.No;
                agregarLineas(db, nuevoDocInterno, Documento, EmpresaNav);

            }
            catch (Exception ex)
            {
                return "{\"Documento\":\"" + Documento + "\", \"Subido\":\"" + "0" + "\", \"Mensaje\":\"" + ex.Message + "\"}";
            }

            return "{\"Documento\":\"" + Documento + " " + "\", \"Subido\":\"" + "1" + "\", \"Mensaje\":\"" + "OK" + "\"}";
        }

        public bool agregarLineas(string db, string nuevoDocInterno, string external_Document_No_, string empresaNav)
        {
            SqlConnection cnn;
            SqlCommand command;
            SqlDataReader dataReader;
            cnn = new SqlConnection(connetionString);
            try
            {
                using (SqlConnection con = new SqlConnection(connetionString))
                {
                    using (SqlCommand cmd = new SqlCommand("RCONTA..Sp_CabanaVirol_FacturasCompraLineas_Insert", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("@empresaNav", SqlDbType.VarChar).Value = empresaNav;
                        cmd.Parameters.Add("@DocumentoVenta", SqlDbType.VarChar).Value = external_Document_No_;
                        cmd.Parameters.Add("@DocumentoCompra", SqlDbType.VarChar).Value = nuevoDocInterno;
                        cmd.Parameters.Add("@db", SqlDbType.VarChar).Value = db;

                        con.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error>>>>>>>>>>>>>>>>.! " + ex.Message);

            }
            return false;
        }

        /*Esta web services sube insumos de fertilizantes*/
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
                if (fc.Count > 0)
                {//se verifca si devolvio registros ya que el numero de lineas se toma en cuenta desde el SP que genera las lineas



                    NAV nav = new NAV(new Uri("http://10.1.1.14:7048/DynamicsNav110/OData/Company('" + fc[0].EmpresaNav + "')"));
                    //NAV nav = new NAV(new Uri("http://10.1.1.23:1048/CABANA2016/OData/Company('LACABANA')"));

                    nav.Credentials = new System.Net.NetworkCredential("nav.services", "Navucodonosor", "CABANA");

                    InvoicePurchase encabezado = new InvoicePurchase();


                    encabezado.Buy_from_Vendor_No = fc[0].CABANA;
                    encabezado.Vendor_Invoice_No = fc[0].External_Document_No_;
                    nav.AddToInvoicePurchase(encabezado);
                    nav.SaveChanges();
                    nuevoDocInterno = encabezado.No;
                    comandoSql(insertarLineas(nuevoDocInterno, fc[0].External_Document_No_, fc[0].EmpresaNav));
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
            string sql = "INSERT INTO LACABANATALLER.[dbo].[" + empresaNav + "$Purchase Line] "
               + " ([Document Type]                                       "
               + " ,[Document No_]                                        "
               + " ,[Line No_]                                            "
               + " ,[Buy-from Vendor No_]                               "
               + " ,[Type]                                                "
               + " ,[No_]                                                 "
               + " ,[Location Code]                                       "
               + " ,[Posting Group]                                       "
               + " ,[Expected Receipt Date]                               "
               + " ,[Description]                                         "
               + " ,[Description 2]                                       "
               + " ,[Unit of Measure]                                     "
               + " ,[Quantity]                                            "
               + " ,[Outstanding Quantity]                                "
               + " ,[Qty_ to Invoice]                                     "
               + " ,[Qty_ to Receive]                                     "
               + " ,[Direct Unit Cost]                                    "
               + " ,[Unit Cost (LCY)]                                      "
               + " ,[VAT _]                                               "
               + " ,[Line Discount _]                                     "
               + " ,[Line Discount Amount]                                "
               + " ,[Amount]                                              "
               + " ,[Amount Including VAT]                                "
               + " ,[Unit Price (LCY)]                                     "
               + " ,[Allow Invoice Disc_]                                 "
               + " ,[Gross Weight]                                        "
               + " ,[Net Weight]                                          "
               + " ,[Units per Parcel]                                    "
               + " ,[Unit Volume]                                         "
               + " ,[Appl_-to Item Entry]                               "
               + " ,[Shortcut Dimension 1 Code]                           "
               + " ,[Shortcut Dimension 2 Code]                           "
               + " ,[Job No_]                                             "
               + " ,[Indirect Cost _]                                     "
               + " ,[Recalculate Invoice Disc_]                           "
               + " ,[Outstanding Amount]                                  "
               + " ,[Qty_ Rcd_ Not Invoiced]                              "
               + " ,[Amt_ Rcd_ Not Invoiced]                              "
               + " ,[Quantity Received]                                   "
               + " ,[Quantity Invoiced]                                   "
               + " ,[Receipt No_]                                         "
               + " ,[Receipt Line No_]                                    "
               + " ,[Profit _]                                            "
               + " ,[Pay-to Vendor No_]                                 "
               + " ,[Inv_ Discount Amount]                                "
               + " ,[Vendor Item No_]                                     "
               + " ,[Sales Order No_]                                     "
               + " ,[Sales Order Line No_]                                "
               + " ,[Drop Shipment]                                       "
               + " ,[Gen_ Bus_ Posting Group]                             "
               + " ,[Gen_ Prod_ Posting Group]                            "
               + " ,[VAT Calculation Type]                                "
               + " ,[Transaction Type]                                    "
               + " ,[Transport Method]                                    "
               + " ,[Attached to Line No_]                                "
               + " ,[Entry Point]                                         "
               + " ,[Area]                                                "
               + " ,[Transaction Specification]                           "
               + " ,[Tax Area Code]                                       "
               + " ,[Tax Liable]                                          "
               + " ,[Tax Group Code]                                      "
               + " ,[Use Tax]                                             "
               + " ,[VAT Bus_ Posting Group]                              "
               + " ,[VAT Prod_ Posting Group]                             "
               + " ,[Currency Code]                                       "
               + " ,[Outstanding Amount (LCY)]                             "
               + " ,[Amt_ Rcd_ Not Invoiced (LCY)]                         "
               + " ,[Blanket Order No_]                                   "
               + " ,[Blanket Order Line No_]                              "
               + " ,[VAT Base Amount]                                     "
               + " ,[Unit Cost]                                           "
               + " ,[System-Created Entry]                              "
               + " ,[Line Amount]                                         "
               + " ,[VAT Difference]                                      "
               + " ,[Inv_ Disc_ Amount to Invoice]                        "
               + " ,[VAT Identifier]                                      "
               + " ,[IC Partner Ref_ Type]                                "
               + " ,[IC Partner Reference]                                "
               + " ,[Prepayment _]                                        "
               + " ,[Prepmt_ Line Amount]                                 "
               + " ,[Prepmt_ Amt_ Inv_]                                   "
               + " ,[Prepmt_ Amt_ Incl_ VAT]                              "
               + " ,[Prepayment Amount]                                   "
               + " ,[Prepmt_ VAT Base Amt_]                               "
               + " ,[Prepayment VAT _]                                    "
               + " ,[Prepmt_ VAT Calc_ Type]                              "
               + " ,[Prepayment VAT Identifier]                           "
               + " ,[Prepayment Tax Area Code]                            "
               + " ,[Prepayment Tax Liable]                               "
               + " ,[Prepayment Tax Group Code]                           "
               + " ,[Prepmt Amt to Deduct]                                "
               + " ,[Prepmt Amt Deducted]                                 "
               + " ,[Prepayment Line]                                     "
               + " ,[Prepmt_ Amount Inv_ Incl_ VAT]                       "
               + " ,[Prepmt_ Amount Inv_ (LCY)]                            "
               + " ,[IC Partner Code]                                     "
               + " ,[Prepmt_ VAT Amount Inv_ (LCY)]                        "
               + " ,[Prepayment VAT Difference]                           "
               + " ,[Prepmt VAT Diff_ to Deduct]                          "
               + " ,[Prepmt VAT Diff_ Deducted]                           "
               + " ,[Outstanding Amt_ Ex_ VAT (LCY)]                       "
               + " ,[A_ Rcd_ Not Inv_ Ex_ VAT (LCY)]                       "
               + " ,[Dimension Set ID]                                    "
               + " ,[Job Task No_]                                        "
               + " ,[Job Line Type]                                       "
               + " ,[Job Unit Price]                                      "
               + " ,[Job Total Price]                                     "
               + " ,[Job Line Amount]                                     "
               + " ,[Job Line Discount Amount]                            "
               + " ,[Job Line Discount _]                                 "
               + " ,[Job Unit Price (LCY)]                                 "
               + " ,[Job Total Price (LCY)]                                "
               + " ,[Job Line Amount (LCY)]                                "
               + " ,[Job Line Disc_ Amount (LCY)]                          "
               + " ,[Job Currency Factor]                                 "
               + " ,[Job Currency Code]                                   "
               + " ,[Job Planning Line No_]                               "
               + " ,[Job Remaining Qty_]                                  "
               + " ,[Job Remaining Qty_ (Base)]                            "
               + " ,[Deferral Code]                                       "
               + " ,[Returns Deferral Start Date]                         "
               + " ,[Prod_ Order No_]                                     "
               + " ,[Variant Code]                                        "
               + " ,[Bin Code]                                            "
               + " ,[Qty_ per Unit of Measure]                            "
               + " ,[Unit of Measure Code]                                "
               + " ,[Quantity (Base)]                                      "
               + " ,[Outstanding Qty_ (Base)]                              "
               + " ,[Qty_ to Invoice (Base)]                               "
               + " ,[Qty_ to Receive (Base)]                               "
               + " ,[Qty_ Rcd_ Not Invoiced (Base)]                        "
               + " ,[Qty_ Received (Base)]                                 "
               + " ,[Qty_ Invoiced (Base)]                                 "
               + " ,[FA Posting Date]                                     "
               + " ,[FA Posting Type]                                     "
               + " ,[Depreciation Book Code]                              "
               + " ,[Salvage Value]                                       "
               + " ,[Depr_ until FA Posting Date]                         "
               + " ,[Depr_ Acquisition Cost]                              "
               + " ,[Maintenance Code]                                    "
               + " ,[Insurance No_]                                       "
               + " ,[Budgeted FA No_]                                     "
               + " ,[Duplicate in Depreciation Book]                      "
               + " ,[Use Duplication List]                                "
               + " ,[Responsibility Center]                               "
               + " ,[Cross-Reference No_]                               "
               + " ,[Unit of Measure (Cross Ref_)]                         "
               + " ,[Cross-Reference Type]                              "
               + " ,[Cross-Reference Type No_]                          "
               + " ,[Item Category Code]                                  "
               + " ,[Nonstock]                                            "
               + " ,[Purchasing Code]                                     "
               + " ,[Product Group Code]                                  "
               + " ,[Special Order]                                       "
               + " ,[Special Order Sales No_]                             "
               + " ,[Special Order Sales Line No_]                        "
               + " ,[Completely Received]                                 "
               + " ,[Requested Receipt Date]                              "
               + " ,[Promised Receipt Date]                               "
               + " ,[Lead Time Calculation]                               "
               + " ,[Inbound Whse_ Handling Time]                         "
               + " ,[Planned Receipt Date]                                "
               + " ,[Order Date]                                          "
               + " ,[Allow Item Charge Assignment]                        "
               + " ,[Return Qty_ to Ship]                                 "
               + " ,[Return Qty_ to Ship (Base)]                           "
               + " ,[Return Qty_ Shipped Not Invd_]                       "
               + " ,[Ret_ Qty_ Shpd Not Invd_(Base)]                      "
               + " ,[Return Shpd_ Not Invd_]                              "
               + " ,[Return Shpd_ Not Invd_ (LCY)]                         "
               + " ,[Return Qty_ Shipped]                                 "
               + " ,[Return Qty_ Shipped (Base)]                           "
               + " ,[Return Shipment No_]                                 "
               + " ,[Return Shipment Line No_]                            "
               + " ,[Return Reason Code]                                  "
               + " ,[Tax To Be Expensed]                                  "
               + " ,[Provincial Tax Area Code]                            "
               + " ,[IRS 1099 Liable]                                     "
               + " ,[GST_HST]                                             "
               + " ,[Document Line No_]                                   "
               + " ,[Original Quantity]                                   "
               + " ,[Routing No_]                                         "
               + " ,[Operation No_]                                       "
               + " ,[Work Center No_]                                     "
               + " ,[Finished]                                            "
               + " ,[Prod_ Order Line No_]                                "
               + " ,[Overhead Rate]                                       "
               + " ,[MPS Order]                                           "
               + " ,[Planning Flexibility]                                "
               + " ,[Safety Lead Time]                                    "
               + " ,[Routing Reference No_])                              "
            + "   SELECT [Document Type]                                  "
              + "	,'" + nuevoDocInterno + "'  as [Document No_]               "
              + "	,E.Linea * 1000                                        "
              + "	,[Buy-from Vendor No_]                                 "
              + "	,[Type]                                                "
              + "	,E.Cuenta                                              "
              + "	,[Location Code]                                       "
              + "	,[Posting Group]                                       "
              + "	,[Expected Receipt Date]                               "
              + "	,[Description]                                         "
              + "	,[Description 2]                                       "
              + "	,[Unit of Measure]                                     "
              + "	,[Quantity]                                            "
              + "	,[Outstanding Quantity]                                "
              + "	,[Qty_ to Invoice]                                     "
              + "	,[Qty_ to Receive]                                     "
              + "	,E.Monto[Direct Unit Cost]                             "
              + "	,E.Monto[Unit Cost (LCY)]                               "
              + "	,[VAT _]                                               "
              + "	,[Line Discount _]                                     "
              + "	,[Line Discount Amount]                                "
              + "	,E.Monto[Amount]                                       "
              + "	,E.montoIVA[Amount Including VAT]                      "
              + "	,[Unit Price (LCY)]                                     "
              + "	,[Allow Invoice Disc_]                                 "
              + "	,[Gross Weight]                                        "
              + "	,[Net Weight]                                          "
              + "	,[Units per Parcel]                                    "
              + "	,[Unit Volume]                                         "
              + "	,[Appl_-to Item Entry]                                 "
/*e.CECO*/    + "	,[Shortcut Dimension 1 Code]                     "
              + "	,[Shortcut Dimension 2 Code]                           "
              + "	,[Job No_]                                             "
              + "	,[Indirect Cost _]                                     "
              + "	,[Recalculate Invoice Disc_]                           "
              + "	,E.montoIVA[Outstanding Amount]                        "
              + "	,[Qty_ Rcd_ Not Invoiced]                              "
              + "	,[Amt_ Rcd_ Not Invoiced]                              "
              + "	,[Quantity Received]                                   "
              + "	,[Quantity Invoiced]                                   "
              + "	,[Receipt No_]                                         "
              + "	,[Receipt Line No_]                                    "
              + "	,[Profit _]                                            "
              + "	,[Pay-to Vendor No_]                                   "
              + "	,[Inv_ Discount Amount]                                "
              + "	,[Vendor Item No_]                                     "
              + "	,[Sales Order No_]                                     "
              + "	,[Sales Order Line No_]                                "
              + "	,[Drop Shipment]                                       "
              + "	,[Gen_ Bus_ Posting Group]                             "
              + "	,[Gen_ Prod_ Posting Group]                            "
              + "	,[VAT Calculation Type]                                "
              + "	,[Transaction Type]                                    "
              + "	,[Transport Method]                                    "
              + "	,[Attached to Line No_]                                "
              + "	,[Entry Point]                                         "
              + "	,[Area]                                                "
              + "	,[Transaction Specification]                           "
              + "	,[Tax Area Code]                                       "
              + "	,[Tax Liable]                                          "
              + "	,[Tax Group Code]                                      "
              + "	,[Use Tax]                                             "
              + "	,[VAT Bus_ Posting Group]                              "
              + "	,[VAT Prod_ Posting Group]                             "
              + "	,[Currency Code]                                       "
              + "	,E.montoIVA[Outstanding Amount (LCY)]                   "
              + "	,[Amt_ Rcd_ Not Invoiced (LCY)]                         "
              + "	,[Blanket Order No_]                                   "
              + "	,[Blanket Order Line No_]                              "
              + "	,E.Monto[VAT Base Amount]                              "
              + "	,E.monto[Unit Cost]                                    "
              + "	,[System-Created Entry]                                "
              + "	,E.Monto[Line Amount]                                  "
              + "	,[VAT Difference]                                      "
              + "	,[Inv_ Disc_ Amount to Invoice]                        "
              + "	,[VAT Identifier]                                      "
              + "	,[IC Partner Ref_ Type]                                "
              + "	,[IC Partner Reference]                                "
              + "	,[Prepayment _]                                        "
              + "	,[Prepmt_ Line Amount]                                 "
              + "	,[Prepmt_ Amt_ Inv_]                                   "
              + "	,[Prepmt_ Amt_ Incl_ VAT]                              "
              + "	,[Prepayment Amount]                                   "
              + "	,[Prepmt_ VAT Base Amt_]                               "
              + "	,[Prepayment VAT _]                                    "
              + "	,[Prepmt_ VAT Calc_ Type]                              "
              + "	,[Prepayment VAT Identifier]                           "
              + "	,[Prepayment Tax Area Code]                            "
              + "	,[Prepayment Tax Liable]                               "
              + "	,[Prepayment Tax Group Code]                           "
              + "	,[Prepmt Amt to Deduct]                                "
              + "	,[Prepmt Amt Deducted]                                 "
              + "	,[Prepayment Line]                                     "
              + "	,[Prepmt_ Amount Inv_ Incl_ VAT]                       "
              + "	,[Prepmt_ Amount Inv_ (LCY)]                            "
              + "	,[IC Partner Code]                                     "
              + "	,[Prepmt_ VAT Amount Inv_ (LCY)]                        "
              + "	,[Prepayment VAT Difference]                           "
              + "	,[Prepmt VAT Diff_ to Deduct]                          "
              + "	,[Prepmt VAT Diff_ Deducted]                           "
              + "	,E.Monto[Outstanding Amt_ Ex_ VAT (LCY)]                "
              + "	,[A_ Rcd_ Not Inv_ Ex_ VAT (LCY)]                       "
              + "	,[Dimension Set ID]                                    "
              + "	,[Job Task No_]                                        "
              + "	,[Job Line Type]                                       "
              + "	,[Job Unit Price]                                      "
              + "	,[Job Total Price]                                     "
              + "	,[Job Line Amount]                                     "
              + "	,[Job Line Discount Amount]                            "
              + "	,[Job Line Discount _]                                 "
              + "	,[Job Unit Price (LCY)]                                 "
              + "	,[Job Total Price (LCY)]                                "
              + "	,[Job Line Amount (LCY)]                                "
              + "	,[Job Line Disc_ Amount (LCY)]                          "
              + "	,[Job Currency Factor]                                 "
              + "	,[Job Currency Code]                                   "
              + "	,[Job Planning Line No_]                               "
              + "	,[Job Remaining Qty_]                                  "
              + "	,[Job Remaining Qty_ (Base)]                            "
              + "	,[Deferral Code]                                       "
              + "	,[Returns Deferral Start Date]                         "
              + "	,[Prod_ Order No_]                                     "
              + "	,[Variant Code]                                        "
              + "	,[Bin Code]                                            "
              + "	,[Qty_ per Unit of Measure]                            "
              + "	,[Unit of Measure Code]                                "
              + "	,[Quantity (Base)]                                      "
              + "	,[Outstanding Qty_ (Base)]                              "
              + "	,[Qty_ to Invoice (Base)]                               "
              + "	,[Qty_ to Receive (Base)]                               "
              + "	,[Qty_ Rcd_ Not Invoiced (Base)]                        "
              + "	,[Qty_ Received (Base)]                                 "
              + "	,[Qty_ Invoiced (Base)]                                 "
              + "	,[FA Posting Date]                                     "
              + "	,[FA Posting Type]                                     "
              + "	,[Depreciation Book Code]                              "
              + "	,[Salvage Value]                                       "
              + "	,[Depr_ until FA Posting Date]                         "
              + "	,[Depr_ Acquisition Cost]                              "
              + "	,[Maintenance Code]                                    "
              + "	,[Insurance No_]                                       "
              + "	,[Budgeted FA No_]                                     "
              + "	,[Duplicate in Depreciation Book]                      "
              + "	,[Use Duplication List]                                "
              + "	,[Responsibility Center]                               "
              + "	,[Cross-Reference No_]                                 "
              + "	,[Unit of Measure (Cross Ref_)]                         "
              + "	,[Cross-Reference Type]                                "
              + "	,[Cross-Reference Type No_]                            "
              + "	,[Item Category Code]                                  "
              + "	,[Nonstock]                                            "
              + "	,[Purchasing Code]                                     "
              + "	,[Product Group Code]                                  "
              + "	,[Special Order]                                       "
              + "	,[Special Order Sales No_]                             "
              + "	,[Special Order Sales Line No_]                        "
              + "	,[Completely Received]                                 "
              + "	,[Requested Receipt Date]                              "
              + "	,[Promised Receipt Date]                               "
              + "	,[Lead Time Calculation]                               "
              + "	,[Inbound Whse_ Handling Time]                         "
              + "	,[Planned Receipt Date]                                "
              + "	,[Order Date]                                          "
              + "	,[Allow Item Charge Assignment]                        "
              + "	,[Return Qty_ to Ship]                                 "
              + "	,[Return Qty_ to Ship (Base)]                           "
              + "	,[Return Qty_ Shipped Not Invd_]                       "
              + "	,[Ret_ Qty_ Shpd Not Invd_(Base)]                      "
              + "	,[Return Shpd_ Not Invd_]                              "
              + "	,[Return Shpd_ Not Invd_ (LCY)]                         "
              + "	,[Return Qty_ Shipped]                                 "
              + "	,[Return Qty_ Shipped (Base)]                           "
              + "	,[Return Shipment No_]                                 "
              + "	,[Return Shipment Line No_]                            "
              + "	,[Return Reason Code]                                  "
              + "	,[Tax To Be Expensed]                                  "
              + "	,[Provincial Tax Area Code]                            "
              + "	,[IRS 1099 Liable]                                     "
              + "	,[GST_HST]                                             "
              + "	,[Document Line No_]                                   "
              + "	,[Original Quantity]                                   "
              + "  ,[Routing No_]                                          "
              + "  ,[Operation No_]                                        "
              + "  ,[Work Center No_]                                      "
              + "  ,[Finished]                                             "
              + "  ,[Prod_ Order Line No_]                                 "
              + "  ,[Overhead Rate]                                        "
              + "  ,[MPS Order]                                            "
              + "  ,[Planning Flexibility]                                 "
              + "  ,[Safety Lead Time]                                     "
              + "  ,[Routing Reference No_]                                "
              + "  FROM LACABANA..[" + empresaNav + "$Purchase Line] H       "
              + "  INNER JOIN(                                             "
              + "     SELECT                                               "
              + "      C.Cuenta                                            "
              + "     , A.[External Document No_]                          "
                  + "   , ROW_NUMBER() OVER (PARTITION BY  A.[External Document No_]    ORDER BY  A.[External Document No_] ASC) Linea                  "
                  + "	,(B.Amount) Monto                                                                                                             "
                  + "	,(B.Amount)*1.13 MontoIVA                                                                                                     "
                   + "	,B.[Sell-to Customer No_]                                                                                                     "
                   + "  , E.FacturaCompra                                    "
                   + "   FROM LACABANA..[LACABANA$Sales Invoice Header] A                                                                                "
                  + "   INNER JOIN LACABANA..[LACABANA$Sales Invoice Line] B ON A.No_=B.[Document No_]                                                  "
                  + "   INNER JOIN RCONTA..DetalleItems C  ON B.No_=C.ItemNo COLLATE Latin1_General_100_CS_AS                      "
                  + "   INNER JOIN LCMAESTROZAF..SAIPLUS_EmpresaNav E ON E.CodClie = B.[Sell-to Customer No_] COLLATE Latin1_General_100_CS_AS        "
                  + "  WHERE "
                  + "  A.[External Document No_] = '" + external_Document_No_ + "'                                                                                "
                  + " )  as E on h.[Document No_]= E.FacturaCompra COLLATE Latin1_General_100_CS_AS ";

            return (sql);
        }
        private string insertarLineasFertilizante(string nuevoDocInterno, string external_Document_No_, string empresaNav)
        {

            string sql = "INSERT INTO LACABANA.[dbo].[" + empresaNav + "$Purchase Line] "
                + " ([Document Type]                                       "
                + " ,[Document No_]                                        "
                + " ,[Line No_]                                            "
                + " ,[Buy-from Vendor No_]                               "
                + " ,[Type]                                                "
                + " ,[No_]                                                 "
                + " ,[Location Code]                                       "
                + " ,[Posting Group]                                       "
                + " ,[Expected Receipt Date]                               "
                + " ,[Description]                                         "
                + " ,[Description 2]                                       "
                + " ,[Unit of Measure]                                     "
                + " ,[Quantity]                                            "
                + " ,[Outstanding Quantity]                                "
                + " ,[Qty_ to Invoice]                                     "
                + " ,[Qty_ to Receive]                                     "
                + " ,[Direct Unit Cost]                                    "
                + " ,[Unit Cost (LCY)]                                      "
                + " ,[VAT _]                                               "
                + " ,[Line Discount _]                                     "
                + " ,[Line Discount Amount]                                "
                + " ,[Amount]                                              "
                + " ,[Amount Including VAT]                                "
                + " ,[Unit Price (LCY)]                                     "
                + " ,[Allow Invoice Disc_]                                 "
                + " ,[Gross Weight]                                        "
                + " ,[Net Weight]                                          "
                + " ,[Units per Parcel]                                    "
                + " ,[Unit Volume]                                         "
                + " ,[Appl_-to Item Entry]                               "
                + " ,[Shortcut Dimension 1 Code]                           "
                + " ,[Shortcut Dimension 2 Code]                           "
                + " ,[Job No_]                                             "
                + " ,[Indirect Cost _]                                     "
                + " ,[Recalculate Invoice Disc_]                           "
                + " ,[Outstanding Amount]                                  "
                + " ,[Qty_ Rcd_ Not Invoiced]                              "
                + " ,[Amt_ Rcd_ Not Invoiced]                              "
                + " ,[Quantity Received]                                   "
                + " ,[Quantity Invoiced]                                   "
                + " ,[Receipt No_]                                         "
                + " ,[Receipt Line No_]                                    "
                + " ,[Profit _]                                            "
                + " ,[Pay-to Vendor No_]                                 "
                + " ,[Inv_ Discount Amount]                                "
                + " ,[Vendor Item No_]                                     "
                + " ,[Sales Order No_]                                     "
                + " ,[Sales Order Line No_]                                "
                + " ,[Drop Shipment]                                       "
                + " ,[Gen_ Bus_ Posting Group]                             "
                + " ,[Gen_ Prod_ Posting Group]                            "
                + " ,[VAT Calculation Type]                                "
                + " ,[Transaction Type]                                    "
                + " ,[Transport Method]                                    "
                + " ,[Attached to Line No_]                                "
                + " ,[Entry Point]                                         "
                + " ,[Area]                                                "
                + " ,[Transaction Specification]                           "
                + " ,[Tax Area Code]                                       "
                + " ,[Tax Liable]                                          "
                + " ,[Tax Group Code]                                      "
                + " ,[Use Tax]                                             "
                + " ,[VAT Bus_ Posting Group]                              "
                + " ,[VAT Prod_ Posting Group]                             "
                + " ,[Currency Code]                                       "
                + " ,[Outstanding Amount (LCY)]                             "
                + " ,[Amt_ Rcd_ Not Invoiced (LCY)]                         "
                + " ,[Blanket Order No_]                                   "
                + " ,[Blanket Order Line No_]                              "
                + " ,[VAT Base Amount]                                     "
                + " ,[Unit Cost]                                           "
                + " ,[System-Created Entry]                              "
                + " ,[Line Amount]                                         "
                + " ,[VAT Difference]                                      "
                + " ,[Inv_ Disc_ Amount to Invoice]                        "
                + " ,[VAT Identifier]                                      "
                + " ,[IC Partner Ref_ Type]                                "
                + " ,[IC Partner Reference]                                "
                + " ,[Prepayment _]                                        "
                + " ,[Prepmt_ Line Amount]                                 "
                + " ,[Prepmt_ Amt_ Inv_]                                   "
                + " ,[Prepmt_ Amt_ Incl_ VAT]                              "
                + " ,[Prepayment Amount]                                   "
                + " ,[Prepmt_ VAT Base Amt_]                               "
                + " ,[Prepayment VAT _]                                    "
                + " ,[Prepmt_ VAT Calc_ Type]                              "
                + " ,[Prepayment VAT Identifier]                           "
                + " ,[Prepayment Tax Area Code]                            "
                + " ,[Prepayment Tax Liable]                               "
                + " ,[Prepayment Tax Group Code]                           "
                + " ,[Prepmt Amt to Deduct]                                "
                + " ,[Prepmt Amt Deducted]                                 "
                + " ,[Prepayment Line]                                     "
                + " ,[Prepmt_ Amount Inv_ Incl_ VAT]                       "
                + " ,[Prepmt_ Amount Inv_ (LCY)]                            "
                + " ,[IC Partner Code]                                     "
                + " ,[Prepmt_ VAT Amount Inv_ (LCY)]                        "
                + " ,[Prepayment VAT Difference]                           "
                + " ,[Prepmt VAT Diff_ to Deduct]                          "
                + " ,[Prepmt VAT Diff_ Deducted]                           "
                + " ,[Outstanding Amt_ Ex_ VAT (LCY)]                       "
                + " ,[A_ Rcd_ Not Inv_ Ex_ VAT (LCY)]                       "
                + " ,[Dimension Set ID]                                    "
                + " ,[Job Task No_]                                        "
                + " ,[Job Line Type]                                       "
                + " ,[Job Unit Price]                                      "
                + " ,[Job Total Price]                                     "
                + " ,[Job Line Amount]                                     "
                + " ,[Job Line Discount Amount]                            "
                + " ,[Job Line Discount _]                                 "
                + " ,[Job Unit Price (LCY)]                                 "
                + " ,[Job Total Price (LCY)]                                "
                + " ,[Job Line Amount (LCY)]                                "
                + " ,[Job Line Disc_ Amount (LCY)]                          "
                + " ,[Job Currency Factor]                                 "
                + " ,[Job Currency Code]                                   "
                + " ,[Job Planning Line No_]                               "
                + " ,[Job Remaining Qty_]                                  "
                + " ,[Job Remaining Qty_ (Base)]                            "
                + " ,[Deferral Code]                                       "
                + " ,[Returns Deferral Start Date]                         "
                + " ,[Prod_ Order No_]                                     "
                + " ,[Variant Code]                                        "
                + " ,[Bin Code]                                            "
                + " ,[Qty_ per Unit of Measure]                            "
                + " ,[Unit of Measure Code]                                "
                + " ,[Quantity (Base)]                                      "
                + " ,[Outstanding Qty_ (Base)]                              "
                + " ,[Qty_ to Invoice (Base)]                               "
                + " ,[Qty_ to Receive (Base)]                               "
                + " ,[Qty_ Rcd_ Not Invoiced (Base)]                        "
                + " ,[Qty_ Received (Base)]                                 "
                + " ,[Qty_ Invoiced (Base)]                                 "
                + " ,[FA Posting Date]                                     "
                + " ,[FA Posting Type]                                     "
                + " ,[Depreciation Book Code]                              "
                + " ,[Salvage Value]                                       "
                + " ,[Depr_ until FA Posting Date]                         "
                + " ,[Depr_ Acquisition Cost]                              "
                + " ,[Maintenance Code]                                    "
                + " ,[Insurance No_]                                       "
                + " ,[Budgeted FA No_]                                     "
                + " ,[Duplicate in Depreciation Book]                      "
                + " ,[Use Duplication List]                                "
                + " ,[Responsibility Center]                               "
                + " ,[Cross-Reference No_]                               "
                + " ,[Unit of Measure (Cross Ref_)]                         "
                + " ,[Cross-Reference Type]                              "
                + " ,[Cross-Reference Type No_]                          "
                + " ,[Item Category Code]                                  "
                + " ,[Nonstock]                                            "
                + " ,[Purchasing Code]                                     "
                + " ,[Product Group Code]                                  "
                + " ,[Special Order]                                       "
                + " ,[Special Order Sales No_]                             "
                + " ,[Special Order Sales Line No_]                        "
                + " ,[Completely Received]                                 "
                + " ,[Requested Receipt Date]                              "
                + " ,[Promised Receipt Date]                               "
                + " ,[Lead Time Calculation]                               "
                + " ,[Inbound Whse_ Handling Time]                         "
                + " ,[Planned Receipt Date]                                "
                + " ,[Order Date]                                          "
                + " ,[Allow Item Charge Assignment]                        "
                + " ,[Return Qty_ to Ship]                                 "
                + " ,[Return Qty_ to Ship (Base)]                           "
                + " ,[Return Qty_ Shipped Not Invd_]                       "
                + " ,[Ret_ Qty_ Shpd Not Invd_(Base)]                      "
                + " ,[Return Shpd_ Not Invd_]                              "
                + " ,[Return Shpd_ Not Invd_ (LCY)]                         "
                + " ,[Return Qty_ Shipped]                                 "
                + " ,[Return Qty_ Shipped (Base)]                           "
                + " ,[Return Shipment No_]                                 "
                + " ,[Return Shipment Line No_]                            "
                + " ,[Return Reason Code]                                  "
                + " ,[Tax To Be Expensed]                                  "
                + " ,[Provincial Tax Area Code]                            "
                + " ,[IRS 1099 Liable]                                     "
                + " ,[GST_HST]                                             "
                + " ,[Document Line No_]                                   "
                + " ,[Original Quantity]                                   "
                + " ,[Routing No_]                                         "
                + " ,[Operation No_]                                       "
                + " ,[Work Center No_]                                     "
                + " ,[Finished]                                            "
                + " ,[Prod_ Order Line No_]                                "
                + " ,[Overhead Rate]                                       "
                + " ,[MPS Order]                                           "
                + " ,[Planning Flexibility]                                "
                + " ,[Safety Lead Time]                                    "
                + " ,[Routing Reference No_])                              "
             + "   SELECT [Document Type]                                  "
               + "	,'" + nuevoDocInterno + "'  as [Document No_]               "
               + "	,e.Linea * 1000                                        "
               + "	,[Buy-from Vendor No_]                                 "
               + "	,[Type]                                                "
               + "	,e.CUENTA                                              "
               + "	,[Location Code]                                       "
               + "	,[Posting Group]                                       "
               + "	,[Expected Receipt Date]                               "
               + "	,[Description]                                         "
               + "	,[Description 2]                                       "
               + "	,[Unit of Measure]                                     "
               + "	,[Quantity]                                            "
               + "	,[Outstanding Quantity]                                "
               + "	,[Qty_ to Invoice]                                     "
               + "	,[Qty_ to Receive]                                     "
               + "	,e.Monto[Direct Unit Cost]                             "
               + "	,e.Monto[Unit Cost (LCY)]                               "
               + "	,[VAT _]                                               "
               + "	,[Line Discount _]                                     "
               + "	,[Line Discount Amount]                                "
               + "	,e.Monto[Amount]                                       "
               + "	,e.montoIVA[Amount Including VAT]                      "
               + "	,[Unit Price (LCY)]                                     "
               + "	,[Allow Invoice Disc_]                                 "
               + "	,[Gross Weight]                                        "
               + "	,[Net Weight]                                          "
               + "	,[Units per Parcel]                                    "
               + "	,[Unit Volume]                                         "
               + "	,[Appl_-to Item Entry]                                 "
               + "	,e.CECO[Shortcut Dimension 1 Code]                     "
               + "	,[Shortcut Dimension 2 Code]                           "
               + "	,[Job No_]                                             "
               + "	,[Indirect Cost _]                                     "
               + "	,[Recalculate Invoice Disc_]                           "
               + "	,e.montoIVA[Outstanding Amount]                        "
               + "	,[Qty_ Rcd_ Not Invoiced]                              "
               + "	,[Amt_ Rcd_ Not Invoiced]                              "
               + "	,[Quantity Received]                                   "
               + "	,[Quantity Invoiced]                                   "
               + "	,[Receipt No_]                                         "
               + "	,[Receipt Line No_]                                    "
               + "	,[Profit _]                                            "
               + "	,[Pay-to Vendor No_]                                   "
               + "	,[Inv_ Discount Amount]                                "
               + "	,[Vendor Item No_]                                     "
               + "	,[Sales Order No_]                                     "
               + "	,[Sales Order Line No_]                                "
               + "	,[Drop Shipment]                                       "
               + "	,[Gen_ Bus_ Posting Group]                             "
               + "	,[Gen_ Prod_ Posting Group]                            "
               + "	,[VAT Calculation Type]                                "
               + "	,[Transaction Type]                                    "
               + "	,[Transport Method]                                    "
               + "	,[Attached to Line No_]                                "
               + "	,[Entry Point]                                         "
               + "	,[Area]                                                "
               + "	,[Transaction Specification]                           "
               + "	,[Tax Area Code]                                       "
               + "	,[Tax Liable]                                          "
               + "	,[Tax Group Code]                                      "
               + "	,[Use Tax]                                             "
               + "	,[VAT Bus_ Posting Group]                              "
               + "	,[VAT Prod_ Posting Group]                             "
               + "	,[Currency Code]                                       "
               + "	,e.montoIVA[Outstanding Amount (LCY)]                   "
               + "	,[Amt_ Rcd_ Not Invoiced (LCY)]                         "
               + "	,[Blanket Order No_]                                   "
               + "	,[Blanket Order Line No_]                              "
               + "	,e.Monto[VAT Base Amount]                              "
               + "	,e.monto[Unit Cost]                                    "
               + "	,[System-Created Entry]                                "
               + "	,e.Monto[Line Amount]                                  "
               + "	,[VAT Difference]                                      "
               + "	,[Inv_ Disc_ Amount to Invoice]                        "
               + "	,[VAT Identifier]                                      "
               + "	,[IC Partner Ref_ Type]                                "
               + "	,[IC Partner Reference]                                "
               + "	,[Prepayment _]                                        "
               + "	,[Prepmt_ Line Amount]                                 "
               + "	,[Prepmt_ Amt_ Inv_]                                   "
               + "	,[Prepmt_ Amt_ Incl_ VAT]                              "
               + "	,[Prepayment Amount]                                   "
               + "	,[Prepmt_ VAT Base Amt_]                               "
               + "	,[Prepayment VAT _]                                    "
               + "	,[Prepmt_ VAT Calc_ Type]                              "
               + "	,[Prepayment VAT Identifier]                           "
               + "	,[Prepayment Tax Area Code]                            "
               + "	,[Prepayment Tax Liable]                               "
               + "	,[Prepayment Tax Group Code]                           "
               + "	,[Prepmt Amt to Deduct]                                "
               + "	,[Prepmt Amt Deducted]                                 "
               + "	,[Prepayment Line]                                     "
               + "	,[Prepmt_ Amount Inv_ Incl_ VAT]                       "
               + "	,[Prepmt_ Amount Inv_ (LCY)]                            "
               + "	,[IC Partner Code]                                     "
               + "	,[Prepmt_ VAT Amount Inv_ (LCY)]                        "
               + "	,[Prepayment VAT Difference]                           "
               + "	,[Prepmt VAT Diff_ to Deduct]                          "
               + "	,[Prepmt VAT Diff_ Deducted]                           "
               + "	,e.Monto[Outstanding Amt_ Ex_ VAT (LCY)]                "
               + "	,[A_ Rcd_ Not Inv_ Ex_ VAT (LCY)]                       "
               + "	,[Dimension Set ID]                                    "
               + "	,[Job Task No_]                                        "
               + "	,[Job Line Type]                                       "
               + "	,[Job Unit Price]                                      "
               + "	,[Job Total Price]                                     "
               + "	,[Job Line Amount]                                     "
               + "	,[Job Line Discount Amount]                            "
               + "	,[Job Line Discount _]                                 "
               + "	,[Job Unit Price (LCY)]                                 "
               + "	,[Job Total Price (LCY)]                                "
               + "	,[Job Line Amount (LCY)]                                "
               + "	,[Job Line Disc_ Amount (LCY)]                          "
               + "	,[Job Currency Factor]                                 "
               + "	,[Job Currency Code]                                   "
               + "	,[Job Planning Line No_]                               "
               + "	,[Job Remaining Qty_]                                  "
               + "	,[Job Remaining Qty_ (Base)]                            "
               + "	,[Deferral Code]                                       "
               + "	,[Returns Deferral Start Date]                         "
               + "	,[Prod_ Order No_]                                     "
               + "	,[Variant Code]                                        "
               + "	,[Bin Code]                                            "
               + "	,[Qty_ per Unit of Measure]                            "
               + "	,[Unit of Measure Code]                                "
               + "	,[Quantity (Base)]                                      "
               + "	,[Outstanding Qty_ (Base)]                              "
               + "	,[Qty_ to Invoice (Base)]                               "
               + "	,[Qty_ to Receive (Base)]                               "
               + "	,[Qty_ Rcd_ Not Invoiced (Base)]                        "
               + "	,[Qty_ Received (Base)]                                 "
               + "	,[Qty_ Invoiced (Base)]                                 "
               + "	,[FA Posting Date]                                     "
               + "	,[FA Posting Type]                                     "
               + "	,[Depreciation Book Code]                              "
               + "	,[Salvage Value]                                       "
               + "	,[Depr_ until FA Posting Date]                         "
               + "	,[Depr_ Acquisition Cost]                              "
               + "	,[Maintenance Code]                                    "
               + "	,[Insurance No_]                                       "
               + "	,[Budgeted FA No_]                                     "
               + "	,[Duplicate in Depreciation Book]                      "
               + "	,[Use Duplication List]                                "
               + "	,[Responsibility Center]                               "
               + "	,[Cross-Reference No_]                                 "
               + "	,[Unit of Measure (Cross Ref_)]                         "
               + "	,[Cross-Reference Type]                                "
               + "	,[Cross-Reference Type No_]                            "
               + "	,[Item Category Code]                                  "
               + "	,[Nonstock]                                            "
               + "	,[Purchasing Code]                                     "
               + "	,[Product Group Code]                                  "
               + "	,[Special Order]                                       "
               + "	,[Special Order Sales No_]                             "
               + "	,[Special Order Sales Line No_]                        "
               + "	,[Completely Received]                                 "
               + "	,[Requested Receipt Date]                              "
               + "	,[Promised Receipt Date]                               "
               + "	,[Lead Time Calculation]                               "
               + "	,[Inbound Whse_ Handling Time]                         "
               + "	,[Planned Receipt Date]                                "
               + "	,[Order Date]                                          "
               + "	,[Allow Item Charge Assignment]                        "
               + "	,[Return Qty_ to Ship]                                 "
               + "	,[Return Qty_ to Ship (Base)]                           "
               + "	,[Return Qty_ Shipped Not Invd_]                       "
               + "	,[Ret_ Qty_ Shpd Not Invd_(Base)]                      "
               + "	,[Return Shpd_ Not Invd_]                              "
               + "	,[Return Shpd_ Not Invd_ (LCY)]                         "
               + "	,[Return Qty_ Shipped]                                 "
               + "	,[Return Qty_ Shipped (Base)]                           "
               + "	,[Return Shipment No_]                                 "
               + "	,[Return Shipment Line No_]                            "
               + "	,[Return Reason Code]                                  "
               + "	,[Tax To Be Expensed]                                  "
               + "	,[Provincial Tax Area Code]                            "
               + "	,[IRS 1099 Liable]                                     "
               + "	,[GST_HST]                                             "
               + "	,[Document Line No_]                                   "
               + "	,[Original Quantity]                                   "
               + "  ,[Routing No_]                                          "
               + "  ,[Operation No_]                                        "
               + "  ,[Work Center No_]                                      "
               + "  ,[Finished]                                             "
               + "  ,[Prod_ Order Line No_]                                 "
               + "  ,[Overhead Rate]                                        "
               + "  ,[MPS Order]                                            "
               + "  ,[Planning Flexibility]                                 "
               + "  ,[Safety Lead Time]                                     "
               + "  ,[Routing Reference No_]                                "
               + "  FROM [LACABANA]..[" + empresaNav + "$Purchase Line] H       "
               + "  INNER JOIN(                                             "
               + "     SELECT                                               "
               + "      C.CUENTA                                            "
               + "     , G.CECO                                             "
               + "     , A.[External Document No_]                          "
               + "     , E.FacturaCompra                                    "
                   + "   , ROW_NUMBER() OVER (PARTITION BY  A.[External Document No_]    ORDER BY  A.[External Document No_] ASC) Linea                  "
                   + "	,sum(B.Amount) Monto                                                                                                             "
                   + "	,sum(B.Amount)*1.13 MontoIVA                                                                                                     "
                   + "   FROM LACABANA..[LACABANA$Sales Invoice Header] A                                                                                "
                   + "   INNER JOIN LACABANA..[LACABANA$Sales Invoice Line] B ON A.No_=B.[Document No_]                                                  "
                   + "   INNER JOIN RCONTA..TARIFASERV C                       ON B.No_=C.ITEMNMBR COLLATE Latin1_General_100_CS_AS                      "
                   + "   INNER JOIN RCONTA..SERVMAQ AS D ON  D.FACTURA = a.[External Document No_] COLLATE Latin1_General_100_CS_AS                      "
                   + "   INNER JOIN LCMAESTROZAF..SAIPLUS_EmpresaNav E ON E.CodClie = b.[Sell-to Customer No_] COLLATE Latin1_General_100_CS_AS        "
                   + "   INNER JOIN LCMAESTROZAF..ZAFLOTE AS F ON F.LOTE = D.LOTEID                                                                      "
                   + "   INNER JOIN (SELECT DISTINCT CodLote, CECO, EmpresaNav FROM LCMAESTROZAF..SAIPLUS_BOM ) AS G ON g.CodLote = F.CODLOTE_UNICO_ID   "
                   + "  WHERE                                                                                                                            "
                   + "      A.[Customer Posting Group]= 'CLIEN-AFIL'                                                                                     "
                   + "  AND A.[Posting Date] > '25/05/2020'                                                                                              "
                   + "  AND E.EmpresaNav = '" + empresaNav + "'                                                                                               "
                   + "  AND a.[External Document No_] = '" + external_Document_No_ + "'                                                                                "
                   + "  GROUP BY                                                                                                                         "
                   + "   C.CUENTA                                                                                                                        "
                   + "  ,G.CECO                                                                                                                           "
                   + "  , A.[External Document No_]                                                                                                      "
                   + "  , E.FacturaCompra                                                                                                                "
                   + ")  as E on h.[Document No_]= E.FacturaCompra COLLATE Latin1_General_100_CS_AS";

            return (sql);
        }
        private bool comandoSql(string sql)
        {

            SqlConnection cnn;
            SqlCommand command;
            SqlDataReader dataReader;
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

    }
}

