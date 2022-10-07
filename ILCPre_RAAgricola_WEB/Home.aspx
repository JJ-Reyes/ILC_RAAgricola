<%@ Page Title="" Language="C#" MasterPageFile="~/Inicio.Master" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="Cabana.Campo.RAAgricola.Pre.Web.Home" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContenedorEncabezado" runat="server">
                        <h2>Catorcenas</h2>
                    <ol class="breadcrumb">
                        <li>
                            <a href="Home.aspx">Inicio</a>
                        </li>

                        <li class="active">
                            <strong>Catorcenas</strong>
                        </li>
                    </ol>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
         <form role="form" id="form" runat="server">
<div class="row">
                <div class="col-lg-12">
                    <div class="ibox">
                        <div class="ibox-content table-responsive">

                           <asp:GridView class="table table-striped table-hover"  GridLines="None" BorderStyle="None" BorderColor="White"  ID="gvQuincenas" runat="server" Width="100%" AllowPaging="True"  OnPageIndexChanging="gvQuincenas_PageIndexChanging"  AllowSorting="True"
                                AutoGenerateColumns="false" ShowFooter="true"
                                onrowcommand="gvQuincenas_RowCommand">
                            <Columns> 
                                  <asp:TemplateField HeaderText="Tareas">
                                    <ItemTemplate>
                                       <asp:HyperLink id="hyperlink1" runat="server"  Text="Trabajar"  class="btn btn-outline btn-primary btn-sm" NavigateUrl='<%# "IngresoPlanillas.aspx?id="+ DataBinder.Eval(Container.DataItem,"QuinId") %>'/>
                                       <asp:HyperLink id="hyperlink4" runat="server"  Text="Descuentos"  class="btn btn-outline btn-primary btn-sm" NavigateUrl='<%# "Descuentos.aspx?id="+ DataBinder.Eval(Container.DataItem,"QuinId") %>'/>

                                       <asp:HyperLink id="hyperlink2" runat="server"  Text="ConsLocal"  class="btn btn-outline btn-primary btn-sm" NavigateUrl='<%# "http://10.1.1.13:8080/ingcampo/planillaroza/planilla.php?QuinId="+ DataBinder.Eval(Container.DataItem,"QuinId")+"&sesion="+Session["UsuSesion"] %>'/>
                                       <asp:HyperLink id="hyperlink3" runat="server"  Text="ConsFuera"  class="btn btn-outline btn-primary btn-sm" NavigateUrl='<%# "http://190.86.180.195:8080/ingcampo/planillaroza/planilla.php?QuinId="+ DataBinder.Eval(Container.DataItem,"QuinId")+"&sesion="+Session["UsuSesion"] %>'/>

                                    
                                    </ItemTemplate>
                                </asp:TemplateField>
                              
                                <asp:TemplateField HeaderText="ID" ControlStyle-BorderStyle="None">
                                    <ItemTemplate>
                                        <asp:Label ID="lblQuincenaId" runat="server" Text='<%#DataBinder.Eval(
            Container.DataItem,"QuinId") %>'></asp:Label>
                                    </ItemTemplate>

            <ControlStyle BorderStyle="None"></ControlStyle>
                                </asp:TemplateField>
					
                                <asp:TemplateField HeaderText="Fecha Inicio">
                                    <ItemTemplate>
                                        <asp:Label ID="lblFECHADESDE" runat="server" Text='<%#DataBinder.Eval(
            Container.DataItem, "QuinFechaDesde" , "{0:dd/MM/yyyy}") %>'></asp:Label>
                                    </ItemTemplate>

                                </asp:TemplateField>
					
                                <asp:TemplateField HeaderText="Fecha Final">
                                    <ItemTemplate>
                                        <asp:Label ID="lblFECHAHASTA" runat="server" Text='<%#DataBinder.Eval(
            Container.DataItem,"QuinFechaHasta", "{0:dd/MM/yyyy}") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>      
                                <asp:TemplateField HeaderText="Zafra">
                                    <ItemTemplate>
                                        <asp:Label ID="lblZafra" runat="server" Text='<%#DataBinder.Eval(
            Container.DataItem,"QuinZafra") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>                           		
				
                            </Columns>  
                              <PagerSettings FirstPageText="Inicio" LastPageText="Fin" Mode="Numeric" PageButtonCount="10" />           
                              <PagerStyle   HorizontalAlign = "Right" Font-Bold="False" Font-Overline="False" Font-Size="Medium" Font-Strikeout="False" Font-Underline="True" Wrap="True" />
                        </asp:GridView>

                        </div>
                    </div>
                </div>
            </div>


        <asp:HiddenField ID="HddUsuId" runat="server" />
        <asp:HiddenField ID="HddEmprId" runat="server" />
        <asp:HiddenField ID="HddUsuNivelAcceso" runat="server" />

        </form>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentLibreriasJs" runat="server">
    <!-- Data picker -->

    <script src="js/plugins/datapicker/bootstrap-datepicker.js"></script>

    <!-- FooTable -->
    <script src="js/plugins/footable/footable.all.min.js"></script>

    <!-- Date range use moment.js same as full calendar plugin -->
    <script src="js/plugins/fullcalendar/moment.min.js"></script>

    <!-- Date range picker -->
    <script src="js/plugins/daterangepicker/daterangepicker.js"></script>

    <!-- Sweet alert -->
    <script src="js/plugins/sweetalert/sweetalert.min.js"></script>

    <!-- Jquery Validate -->
    <script src="js/plugins/validate/jquery.validate.min.js"></script>

   <!-- Input Mask-->
    <script src="js/plugins/jasny/jasny-bootstrap.min.js"></script>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentCodigoJs" runat="server">
</asp:Content>
