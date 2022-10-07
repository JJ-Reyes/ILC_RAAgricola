using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using Cabana.Campo.RAAgricola.WS.ServiceReferenceNav;
//using IntegracionesNav.ServiceReferenceNav;
namespace Cabana.Campo.RAAgricola.WS
{
    /// <summary>
    /// Descripción breve de WSDitribucionGastos
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
    // [System.Web.Script.Services.ScriptService]
    public class WSDitribucionGastos : System.Web.Services.WebService
    {

        [WebMethod]
        public string HelloWorld()
        {




            /*
            using (LACABANAEntities2 DBF = new LACABANAEntities2()){

                SAI_Purchase_Line linea = new SAI_Purchase_Line
                {

                };
            }
            */
                return "Hola a todos";
        }
        /*

        public List<SP_ReintegroSelect_Result> consultarEncabezado(int id) {
            var contexto = new ILC_PresupuestoEntities();
            return contexto.SP_ReintegroSelect(1).FirstOrDefault;
        }
        
        int corte;
        String zafra;
        NAV nav;
        public void Compra(NAV nav, String zafra)
        {
            this.nav = nav;
            this.zafra = zafra;
        }
        
        public resultadoIntegracion[] facturaCompraDiaria(int tipoTransaccion, int? diaZafra, string sufijoLote, string proveedor)
        {
            resultadoIntegracion r = new resultadoIntegracion();
            List<resultadoIntegracion> r2 = new List<resultadoIntegracion>();

            try
            {
                bool crearNuevaCabecera = true;
                //Preparativos de conexiones
                ServiceReferenceNav.InvoicePurchase encabezado = new ServiceReferenceNav.InvoicePurchase(); ;
                DSCabana ds = new DSCabana();
                System.Data.DataView view;
                int j = 0;
                //DSCabanaTableAdapters.ChequesAbonosNavTableAdapter ChequesAbonosNavTableAdapter = new DSCabanaTableAdapters.ChequesAbonosNavTableAdapter();
                DSCabanaTableAdapters.DatosCanaDiariaTableAdapter datosCanaDiariaTableAdapter = new DSCabanaTableAdapters.DatosCanaDiariaTableAdapter();
                ChequesAbonosNavTableAdapter.Fill(ds.ChequesAbonosNav, tipoTransaccion, diaZafra, sufijoLote, proveedor, null);
                //chequesAbonosCuentas.Fill(ds.ChequesAbonosCuentas, tipoTransaccion, diaZafra, null, null, null);
                using (LACABANAEntities2 db = new LACABANAEntities2())
                {

                    for (int i = 0; i < ds.ChequesAbonosNav.Count; i++)
                    {
                        resultadoIntegracion r3 = new resultadoIntegracion();
                        try
                        {
                            NAV nav = new NAV(new Uri("http://10.1.1.23:1048/CABANA2016/OData/Company('LACABANA')"));


                            nav.Credentials = new System.Net.NetworkCredential("nav.services", "Navucodonosor", "CABANA");
                            
                            if (crearNuevaCabecera) encabezado = new ServiceReferenceNav.InvoicePurchase();

                            //DSCabana.ChequesAbonosCuentasRow item = (DSCabana.ChequesAbonosCuentasRow ) element;
                            datosCanaDiariaTableAdapter.Fill(ds.DatosCanaDiaria, diaZafra, zafra, ds.ChequesAbonosNav[i].codclie);

                            encabezado.Buy_from_Vendor_No = ds.ChequesAbonosNav[i].codclie;// "ACRUZ002";
                            encabezado.Posting_Description = ds.ChequesAbonosNav[i].concep;// "PROV. DE 9102 LBS AZUC";
                            encabezado.Vendor_Invoice_No = ds.ChequesAbonosNav[i].lote;// +"-" + ds.ChequesAbonosNav[i].codclie;// "CHCANAC13Z1516-ACRUZ002 5";                        
                            encabezado.Purchaser_Code = ds.ChequesAbonosNav[i].tipo;//"P.CANA";                        
                            encabezado.Posting_Date = ds.DatosCanaDiaria[0].FECMOV;
                            encabezado.Document_Date = encabezado.Posting_Date;
                            encabezado.Ship_to_Contact = ds.ChequesAbonosNav[i].ncorte;

                            if (crearNuevaCabecera)
                                nav.AddToInvoicePurchase(encabezado);
                            nav.SaveChanges();

                            //view = new System.Data.DataView(ds.ChequesAbonosCuentas, " codclie = '" + ds.ChequesAbonosNav[i].codclie.Trim() + "'", "", System.Data.DataViewRowState.CurrentRows);
                            j = 0;
                            while (j < 1)
                            {

                                SAI_Purchase_Line linea = new SAI_Purchase_Line();
                                linea.ini(ds.ChequesAbonosNav[i].tonelad, ds.ChequesAbonosNav[i].valor, encabezado.Pay_to_Vendor_No, "M.PRIMA", encabezado.Vendor_Posting_Group, "SIN-I", 0);
                                linea.Document_No_ = encabezado.No;
                                linea.Line_No_ = (j + 1) * 10000;
                                linea.Type = 2;
                                linea.No_ = "45876";
                                linea.Brand = ds.ChequesAbonosNav[i].ncorte.ToString();
                                linea.Description = "CAÑA DE AZUCAR";
                                linea.Location_Code = "VIRT.AZUC";
                                linea.Dimension_Set_ID = 33926;
                                linea.Net_Weight = ds.DatosCanaDiaria[0].RENDIANTE;
                                linea.Gross_Weight = ds.DatosCanaDiaria[0].RENDIM85;
                                db.SAI_Purchase_Line.Add(linea);
                                db.SaveChanges();
                                j++;
                            }
                            r3.Correcto = true;
                            r3.Descripcion = "La factura N° " + encabezado.No + " se subió exitosamente";
                            crearNuevaCabecera = true;
                        }
                        catch (Exception error)
                        {
                            string str = error.ToString();
                            int first = str.IndexOf("<m:message") + "<m:message".Length;
                            int last = str.LastIndexOf("</m:message></m:error>");
                            string str2 = "";
                            if (last < 0)
                                str2 = str;
                            else
                            {
                                str2 = str.Substring(first, last - first);
                                first = str2.IndexOf(">") + ">".Length;
                                str2 = str2.Substring(first);
                            }
                            r3.Correcto = false;
                            r3.Descripcion = str2;
                            crearNuevaCabecera = false;
                        }
                        r2.Add(r3);
                    } //end heders for
                }// fin using
            }
            catch (Exception exep)
            {

            }//fin catch

            return r2.ToArray();
        }
    */


    }
}
