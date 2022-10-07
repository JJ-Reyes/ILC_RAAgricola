<%@ Page Title="" Language="C#" MasterPageFile="~/Inicio.Master" AutoEventWireup="true" CodeBehind="Quincenas.aspx.cs" Inherits="Cabana.Campo.RAAgricola.Pre.Web.Quincenas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContenedorEncabezado" runat="server">

    <h2>Mantenimiento Fechas Planillas</h2>
    <ol class="breadcrumb">
        <li>
            <a href="Home.aspx">Inicio</a>
        </li>
        <li>
            <a>Mantenimiento</a>
        </li>
        <li class="active">
            <strong>Catorcena</strong>
        </li>
    </ol>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <form role="form" id="form" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>

        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div class="ibox-content m-b-sm border-bottom">
                    <div class="row">
                        <div class="col-sm-4">
                            <div class="form-group" id="data_5">
                                <label class="control-label">Rango de fechas para Planilla catorcenal</label>
                                <div class="input-daterange input-group" id="datepicker">
                                    <asp:TextBox ID="txtFechaInicial" runat="server" data-date-format="dd-mm-yyyy" placeholder="Fecha Inicio" class="input-sm form-control txtFechaInicial id-txtFechaInicial" name="start" value="" data-mask="99/99/9999" />
                                    <span class="input-group-addon">Hasta</span>

                                    <asp:TextBox ID="txtFechaFinal" runat="server" data-date-format="dd-mm-yyyy" placeholder="Fecha Final" class="input-sm form-control txtFechaInicial id-txtFechaFinal" name="end" value="" data-mask="99/99/9999" />

                                </div>
                                <br />
                                <asp:DropDownList ID="ddlFrmZafra" runat="server" class="form-control m-b" OnSelectedIndexChanged="ddl_Zafra_Change" AutoPostBack="true">
                                    <asp:ListItem Value="0">Seleccionar Zafra</asp:ListItem>
                                    <asp:ListItem Value="2017-2018">2017-2018</asp:ListItem>
                                    <asp:ListItem Value="2016-2017">2016-2017</asp:ListItem>
                                    <asp:ListItem Value="2015-2016">2015-2016</asp:ListItem>
                                    <asp:ListItem Value="2014-2015">2014-2015</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-sm-4">
                            <div class="form-group">
                                <div class="form-group">
                                    <asp:Button ID="btnAgregarQuincena" runat="server" class="btn btn-primary" Text="Agregar Nueva Planilla" OnClick="btnAgregarQuincena_Click"></asp:Button>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="row">
                    <div class="col-lg-12">
                        <div class="ibox">
                            <div class="ibox-content table-responsive">
                                <!--<asp:HyperLink id="hyperlink2" runat="server"  Text="Consultar"  class="btn btn-outline btn-primary btn-sm" NavigateUrl='url'/>-->

                                <asp:GridView class="table table-striped table-hover" GridLines="None" BorderStyle="None"
                                    BorderColor="White" ID="gvEmployeeDetails" runat="server" Width="100%" AllowPaging="True"
                                    OnPageIndexChanging="GridView1_PageIndexChanging" AllowSorting="True"
                                    AutoGenerateColumns="false" ShowFooter="true"
                                    OnRowCommand="gvEmployeeDetails_RowCommand"
                                    OnRowDeleting="gvEmployeeDetails_RowDeleting"
                                    OnRowCancelingEdit="gvEmployeeDetails_RowCancelingEdit"
                                    OnRowEditing="gvEmployeeDetails_RowEditing"
                                    OnRowUpdating="gvEmployeeDetails_RowUpdating"
                                    OnRowDataBound="gvQuincenas_RowDataBound">
                                    <Columns>

                                        <asp:TemplateField HeaderText="Tareas">
                                            <ItemTemplate>
                                                <asp:HyperLink ID="hyperlink1" runat="server" Text="Trabajar" class="btn btn-outline btn-primary btn-sm" NavigateUrl='<%# "IngresoPlanillas.aspx?id="+ DataBinder.Eval(Container.DataItem,"QuinId") %>' />
                                                <asp:HyperLink ID="hyperlink4" runat="server" Text="Descuentos" class="btn btn-outline btn-primary btn-sm" NavigateUrl='<%# "Descuentos.aspx?id="+ DataBinder.Eval(Container.DataItem,"QuinId") %>' />

                                                <asp:HyperLink id="hyperlink2" runat="server"  Text="ConsLocal"  class="btn btn-outline btn-primary btn-sm" NavigateUrl='<%# "http://10.1.1.13:8080/ingcampo/planillaroza/planilla.php?QuinId="+ DataBinder.Eval(Container.DataItem,"QuinId") %>'/>
                                                <asp:HyperLink id="hyperlink3" runat="server"  Text="ConsFuera"  class="btn btn-outline btn-primary btn-sm" NavigateUrl='<%# "http://190.86.180.195:8080/ingcampo/planillaroza/planilla.php?QuinId="+ DataBinder.Eval(Container.DataItem,"QuinId") %>'/>

                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="ID" ControlStyle-BorderStyle="None">
                                            <ItemTemplate>
                                                <asp:Label ID="lblQuincenaId" runat="server" Text='<%#DataBinder.Eval(
                                                            Container.DataItem,"QuinId") %>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:Label ID="lblEditQuincenaId" runat="server" Text='<%#DataBinder.Eval(
                                                            Container.DataItem, "QuinId") %>'></asp:Label>
                                            </EditItemTemplate>
                                            <ControlStyle BorderStyle="None"></ControlStyle>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Fecha Inicio">
                                            <ItemTemplate>
                                                <asp:Label ID="lblFECHADESDE" data-date-format="dd-mm-yyyy" runat="server" Text='<%#DataBinder.Eval(
                                                            Container.DataItem, "QuinFechaDesde", "{0:dd/MM/yyyy}") %>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtEditFECHADESDE" class="form-control fechasP" data-mask="99/99/9999" runat="server" Text='<%#DataBinder.Eval(
                                                              Container.DataItem,"QuinFechaDesde", "{0:dd/MM/yyyy}") %>'></asp:TextBox>
                                            </EditItemTemplate>

                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Fecha Final">
                                            <ItemTemplate>
                                                <asp:Label ID="lblFECHAHASTA" runat="server" Text='<%#DataBinder.Eval(
                                                            Container.DataItem,"QuinFechaHasta", "{0:dd/MM/yyyy}") %>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtEditFECHAHASTA" class="form-control fechasP" data-mask="99/99/9999" runat="server" Text='<%#DataBinder.Eval(
                                                             Container.DataItem, "QuinFechaHasta", "{0:dd/MM/yyyy}") %>'></asp:TextBox>
                                            </EditItemTemplate>

                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Zafra">
                                            <ItemTemplate>
                                                <asp:Label ID="lblZafra" runat="server" Text='<%#DataBinder.Eval(
                                                            Container.DataItem,"QuinZafra") %>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:Label ID="lblEditZafra" runat="server" Text='<%#DataBinder.Eval(
                                                            Container.DataItem, "QuinZafra") %>'></asp:Label>
                                            </EditItemTemplate>

                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="">
                                            <ItemTemplate>
                                                <asp:Label ID="lblQuinEstatus" runat="server" Text='<%#DataBinder.Eval(
                                                            Container.DataItem,"QuinEstatus") %>'
                                                    Visible="false"></asp:Label>

                                                <asp:LinkButton ID="lbtnEstatus" runat="server" class="btn btn-outline btn-warning btn-sm" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" CommandName="EstatusById">
                                                    <asp:Image ID="imgestado" runat="server" Height="20px" /></asp:LinkButton>

                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Action">
                                            <ItemTemplate>
                                                <asp:Button ID="imgbtnEdit" runat="server" CommandName="Edit" Text="Editar" class="btn btn-outline btn-warning  btn-sm " />
                                                <asp:Button ID="imgbtnDelete" runat="server" CommandName="Delete" Text="Eliminar" class="btn btn-outline btn-danger btn-sm" OnClientClick="if ( ! UserDeleteConfirmation('Eliminar')) return false;" />
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:Button ID="imgbtnUpdate" runat="server" CommandName="Update" Text="Aceptar" class="btn btn-primary btn-sm" OnClientClick="if ( ! UserDeleteConfirmation('Actualizar')) return false;" />
                                                <asp:Button ID="imgbtnCancel" runat="server" CommandName="Cancel" Text="Cancelar" class="btn btn-warning btn-sm" />
                                            </EditItemTemplate>

                                        </asp:TemplateField>

                                    </Columns>
                                    <PagerSettings FirstPageText="Inicio" LastPageText="Fin" Mode="Numeric" PageButtonCount="10" />
                                    <PagerStyle HorizontalAlign="Right" Font-Bold="False" Font-Overline="False" Font-Size="Medium" Font-Strikeout="False" Font-Underline="True" Wrap="True" />
                                </asp:GridView>

                            </div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1"></asp:UpdateProgress>


        <asp:HiddenField ID="HddUsuId" runat="server" />
        <asp:HiddenField ID="HddEmprId" runat="server" />
        <asp:HiddenField ID="HddUsuNivelAcceso" runat="server" />

    </form>



</asp:Content>

<asp:Content ID="Content3" runat="server" ContentPlaceHolderID="ContentLibreriasJs">
    <script type="text/javascript" src="//cdn.jsdelivr.net/momentjs/latest/moment-with-locales.min.js"></script>

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


<asp:Content ID="Content4" runat="server" ContentPlaceHolderID="ContentCodigoJs">

    <script>
        function UserDeleteConfirmation(descripcionAlerta) {
            return confirm("Esta seguro de " + descripcionAlerta + " esta quincena?");
        }

    </script>

    <script>
        function InIEvent() {
            //INICIO TEXT_FECHA
            $('.footable').footable();

            $('.fechasP').datepicker({
                format: 'dd/mm/yyyy',
                todayBtn: "linked",
                keyboardNavigation: false,
                forceParse: false,
                calendarWeeks: true,
                autoclose: true
            });
            //FIN TEXT_FECHA
            //Array para dar formato en español
            $.fn.datepicker.dates['en'] = {
                days: ["Domingo", "Lunes", "Martes", "Miercoles", "Jueves", "Viernes", "Sabado", "Domingo"],
                daysShort: ["Dom", "Lun", "Mar", "Mie", "Jue", "Vie", "Sab", "Dom"],
                daysMin: ["Do", "Lu", "Ma", "Mi", "Ju", "Vi", "Sa", "Do"],
                months: ["Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio", "Julio", "Agosto", "Septiembre", "Octubre", "Noviembre", "Diciembre"],
                monthsShort: ["Ene", "Feb", "Mar", "Abr", "May", "Jun", "Jul", "Ago", "Sep", "Oct", "Nov", "Dic"]
            };
            /*
            $.fn.datepicker()
            .click(dateChanged)
                .on('changeDate', dateChanged);
                */
            
            $(".id-txtFechaInicial").datepicker('format', 'dd/mm/yyyy').click(dateChanged)
                .on('changeDate', dateChanged);
            $(".id-txtFechaFinal").datepicker('format', 'dd/mm/yyyy');
            
            function dateChanged(ev) {
                //$(this).datepicker('hide');
                var fecha;
                var input = $('.id-txtFechaInicial').val().split('-');;
                fecha = new Date(input[2], input[1] - 1, input[0]);
                var date = sumarDias(fecha, 13)
                
                $('.id-txtFechaFinal').val(getFormattedDate(date));
                /*
                if ($('#startdate').val() != '' && $('#enddate').val() != '') {
                    alert("jose1 " + fecha + " " + fechaFinal);
                    
                } else {
                    alert("jose2");
                }*/
            }
            function getFormattedDate(date) {
                var year = date.getFullYear();
                var month = (1 + date.getMonth()).toString();
                month = month.length > 1 ? month : '0' + month;
                var day = date.getDate().toString();
                day = day.length > 1 ? day : '0' + day;
                return day + '/' + month + '/' + year;
            }

            function sumarDias(fecha, dias) {
                fecha.setDate(fecha.getDate() + dias);
                return fecha;
            }

            //FIN RANGO_FECHA

            $('.demo3').click(function () {
                swal({
                    title: "Are you sure?",
                    text: "You will not be able to recover this imaginary file!",
                    type: "warning",
                    showCancelButton: true,
                    confirmButtonColor: "#DD6B55",
                    confirmButtonText: "Yes, delete it!",
                    closeOnConfirm: false
                }, function () {
                    swal("Deleted!", "Your imaginary file has been deleted.", "success");
                });
            });

        };
        $(document).ready(InIEvent);

        //INICIO VALIDACION DE FORMULARIO

        $("#form").validate({
            rules: {
                start: {
                    required: true,
                    date: true
                },

                end: {
                    required: true,
                    date: true
                }
            }
        });
        //FIN VALIDACION DE FORMULARIO


    </script>
    <script>
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(InIEvent);
    </script>
</asp:Content>



