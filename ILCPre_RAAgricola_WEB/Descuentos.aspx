<%@ Page Title="" Language="C#" MasterPageFile="~/Inicio.Master" AutoEventWireup="true" CodeBehind="Descuentos.aspx.cs" Inherits="Cabana.Campo.RAAgricola.Pre.Web.Descuentos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContenedorEncabezado" runat="server">
        <h2>Descuentos Empleados</h2>
    <ol class="breadcrumb">
        <li>
            <a href="Home.aspx">Home</a>
        </li>
        <li>
            <a>Transacciones</a>
        </li>
        <li class="active">
            <strong>Descuentos</strong>
        </li>
    </ol>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

        <form role="form" id="form" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>

        <div class="wrapper wrapper-content  animated fadeInRight">
            <div class="row">


                <div class="col-sm-1 white-bg" style="display: none">
                    <div class="ibox">
                        <div class="ibox-content">

                            <h3>Frentes y Cuadrillas</h3>

                            <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                <ContentTemplate>
                                    <a data-toggle="tab" href="#tab-1">
                                        <asp:DropDownList ID="ddlistFrente" runat="server" class="form-control m-b" OnSelectedIndexChanged="ddlistFrentes_Change" AutoPostBack="true">
                                        </asp:DropDownList>
                                    </a>

                                    <asp:HiddenField ID="hddCuadrillaId" runat="server" Value="0" />
                                    <a data-toggle="tab" href="#tab-1"></a>
                                </ContentTemplate>
                            </asp:UpdatePanel>

                            <div class="clients-list">
                                <ul class="nav nav-tabs">
                                    <!--<span class="pull-right small text-muted">1406 Elements</span>-->
                                    <li class="active"><a data-toggle="tab" href="#tab-1"><i class="fa fa-user"></i>Emp</a></li>
                                    <li class=""><a data-toggle="tab" href="#tab-2"><i class="fa fa-briefcase"></i>Prov</a></li>
                                </ul>
                                <div class="tab-content">
                                    <div id="tab-1" class="tab-pane active">



                                        <div class="full-height-scroll">
                                            <div class="table-responsive">

                                                <div class="modalEmpleados">
                                                    <asp:UpdatePanel ID="UpdatePanel6" runat="server" UpdateMode="Conditional">
                                                        <ContentTemplate>
                                                            <div class="input-group">
                                                                <asp:TextBox ID="txtControlBuscarEmpleado" name="txtControlBuscarEmpleado" runat="server" Text="" class="input form-control txtBuscarEmpClass"></asp:TextBox>
                                                                <span class="input-group-btn">
                                                                    <asp:LinkButton ID="lbtnBuscarEmpleado" runat="server" class="btn btn btn-warning"
                                                                        
                                                                        CommandName="BuscarEmpleado"
                                                                        OnClientClick="if ( ! iniciarFiltrarTextoModal()) return false;"
                                                                        CommandArgument="Ascending"
                                                                        OnCommand="CommandBtn_Click">Emp <i class="fa fa-search"></i></asp:LinkButton>

                                                                </span>
                                                            </div>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                        <ContentTemplate>
                                                            <asp:GridView ID="gvEmpleados" class="table table-striped table-hover" GridLines="None" BorderStyle="None" BorderColor="White" runat="server" Width="100%" AllowPaging="True" OnPageIndexChanging="gvEmpleados_PageIndexChanging" AllowSorting="True"
                                                                AutoGenerateColumns="false" ShowFooter="true" ShowHeader="true"
                                                                ShowHeaderWhenEmpty="true"
                                                                OnRowCommand="gvEmployeeDetails_RowCommand"
                                                                OnRowDataBound="OnRowDataBound"
                                                                OnSelectedIndexChanged="OnSelectedIndexChanged" PageSize="30">
                                                                <Columns>


                                                                    <asp:TemplateField HeaderText="Opc">
                                                                        <ItemTemplate>
                                                                            <asp:CheckBox ID="chkSelect" runat="server" class="btn btn-outline btn-warning btn-sm" Visible="false" />
                                                                            <asp:LinkButton ID="lbtnSelect" runat="server" Text="Ver" class="btn btn-outline btn-warning btn-sm" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" CommandName="SELECTBYID"><i class="fa fa-check"></i></asp:LinkButton>
                                                                        </ItemTemplate>
                                                                        <HeaderTemplate>
                                                                            <asp:Label ID="Labe33" runat="server" Text="Opc" Font-Bold="true"></asp:Label><br>
                                                                        </HeaderTemplate>
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="ID">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblEmpId" runat="server" Text='<%#DataBinder.Eval(
            Container.DataItem,"EmpId") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderTemplate>
                                                                            <asp:Label ID="Labe3" runat="server" Text="ID" Font-Bold="true"></asp:Label><br>
                                                                        </HeaderTemplate>

                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="Nombre">
                                                                        <ItemTemplate>
                                                                            <asp:LinkButton ID="lbtnSelectNombre" runat="server"
                                                                                Text='<%#DataBinder.Eval( Container.DataItem, "EmpNombres") %>'
                                                                                class="btn btn-outline btn-warning btn-sm"
                                                                                CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"
                                                                                CommandName="lbtnSelectRow">
                                           
                                                                            </asp:LinkButton>
                                                                            <asp:Label Visible="false" ID="lblEmpNombres" runat="server" Text='<%#DataBinder.Eval(
            Container.DataItem, "EmpNombres") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="Apellidos">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblEmpApe" runat="server" Text='<%#DataBinder.Eval(
            Container.DataItem, "EmpApellidos") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="Dui">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblEmpDUI" runat="server" Text='<%#DataBinder.Eval(
            Container.DataItem, "EmpDUI") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="Tipo">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblTipoEmpDesc" runat="server" Text='<%#DataBinder.Eval(
            Container.DataItem, "TipoEmpDesc") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>


                                                                </Columns>
                                                                <PagerSettings FirstPageText="Inicio" LastPageText="Fin" Mode="Numeric" PageButtonCount="10" />
                                                                <PagerStyle HorizontalAlign="Right" Font-Bold="False" Font-Overline="False" Font-Size="Medium" Font-Strikeout="False" Font-Underline="True" Wrap="True" />
                                                            </asp:GridView>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </div>
                                            </div>
                                        </div>


                                    </div>
               
                                </div>

                            </div>
                        </div>
                    </div>
                </div>

                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>

                        <div class="col-sm-13">

                            <div class="ibox">

                                <div class="ibox-content">
                                    <div class="tab-content">

                                        <div class="ibox-content m-b-sm border-bottom ">
       <asp:UpdateProgress ID="UpdateProgress2" runat="server">
            <ProgressTemplate>
                Procesando espere...
                
                            <div class="spiner-example">
                                <div class="sk-spinner sk-spinner-three-bounce">
                                    <div class="sk-bounce1"></div>
                                    <div class="sk-bounce2"></div>
                                    <div class="sk-bounce3"></div>
                                </div>
                            </div>
                
            </ProgressTemplate>
        </asp:UpdateProgress>

                                            <p>LLenar formulario.
                                                <asp:Label ID="lblRangoQuincena" Text="15/10/2015 -15/10/2015" runat="server" class="input form-control" BorderColor="Green"></asp:Label></p>



                                            <div class="form-group">
                                                <label class="col-lg-2 control-label">Cuadrillas:</label>
                                                <div class="col-lg-10">
                                                    <asp:DropDownList ID="ddlistCuadrilla" runat="server" class="form-control m-b" OnSelectedIndexChanged="ddlistCuadrillas_Change" AutoPostBack="true">
                                                        <asp:ListItem Value="0"> Seleccionar Cuadrilla</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>

                                            <div class="form-group">
                                                <asp:LinkButton ID="LinkButton66" runat="server" class="col-lg-2 control-label" OnClientClick=" modalEmp();">Empleados:</asp:LinkButton>
                                                <div class="col-lg-10">
                                                    <asp:HiddenField ID="hddIdEmpleado" runat="server" />
                                                    <asp:Label ID="lblNombreEmpleado" runat="server" class="input form-control badge-primary"></asp:Label>
                                                    <span class="help-block m-b-none"></span>
                                                </div>
                                            </div>

                                            <div class="form-group">
                                                <label class="col-lg-2 control-label">Cantidad</label>

                                                <div class="col-lg-10">
                                                    <asp:TextBox ID="txtTareasCantidad" runat="server" class="input form-control" MaxLength="7" OnTextChanged="txtTareasCantidad_OnTextChanged" AutoPostBack="true"></asp:TextBox>
                                                    <span class="help-block m-b-none"></span>
                                                </div>
                                            </div>

                                            <div class="form-group">
                                                <label class="col-lg-2 control-label">Razon</label>

                                                <div class="col-lg-10">
                                                    <asp:TextBox ID="txtDescRazon" runat="server" class="input form-control" MaxLength="50"></asp:TextBox>
                                                </div>
                                            </div>

                                            <div class="form-group">
                                                <div class="col-lg-offset-2 col-lg-10">
                                                    <div class="i-checks">

                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <div class="col-lg-offset-2 col-lg-10">
                                                    <asp:LinkButton ID="lbtn_AddNewMoviPlanilla" runat="server" class="btn btn btn-primary" OnClick="btn_AddNewMoviPlanilla_Click">Guardar <i class="fa fa-save"></i></asp:LinkButton>
                                                    <span class="help-block m-b-none"></span>
                                                </div>
                                            </div>



                                        </div>

                                        <br />

                                        <div class="clients-list">
                                            <ul class="nav nav-tabs">
                                                <span class="pull-right small text-muted"></span>
                                                <li class="active"><a data-toggle="tab" href="#tab-3"><i class="fa fa-user"></i>Empleado</a></li>
                                                <!--<li class=""><a data-toggle="tab" href="#tab-4"><i class="fa fa-briefcase"></i> Todos </a></li>-->
                                            </ul>
                                            <div class="full-height-scroll">
                                                <div class="table-responsive">
                                         <asp:Label ID="lblTotalDiaAll" runat="server" Text='Total Dia: $ 0.0' class="input form-control" BorderColor="#339900"></asp:Label>

                                         <asp:Label ID="lblTotalDia" runat="server" Text='' class="input form-control" BorderColor="#339900"></asp:Label>
                                        <br />
                                                    <asp:GridView class="table table-striped table-hover" ID="gvMoviPlanilla" GridLines="None" BorderStyle="None" BorderColor="White" runat="server" Width="100%" AllowPaging="True" OnPageIndexChanging="gvMoviPlanilla_PageIndexChanging" AllowSorting="True"
                                                        AutoGenerateColumns="false" ShowFooter="true" ShowHeaderWhenEmpty="true"
                                                        OnRowCommand="gvEmployeeDetails_RowCommand"
                                                        OnRowDeleting="gvMoviPlanilla_RowDeleting"
                                                        OnRowCancelingEdit="gvMoviPlanilla_RowCancelingEdit"
                                                        OnRowEditing="gvMoviPlanilla_RowEditing"
                                                        OnRowUpdating="gvMoviPlanilla_RowUpdating">
                                                        <Columns>

                                                            <asp:TemplateField HeaderText="Opc">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="LinkButton5" runat="server" CommandName="Edit" class="btn btn-outline btn-warning btn-sm"> <i class="fa fa-edit"></i></asp:LinkButton>
                                                                    <asp:LinkButton ID="LinkButton2" runat="server" CommandName="Delete" class="btn btn-outline btn-danger btn-sm" OnClientClick="if ( ! UserDeleteConfirmation()) return false;"> <i class="fa fa-remove"></i></asp:LinkButton>

                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <asp:Button ID="imgbtnUpdate" runat="server" CommandName="Update" Text="Aceptar" class="btn btn-danger btn-sm" />
                                                                    <asp:Button ID="imgbtnCancel" runat="server" CommandName="Cancel" Text="Cancelar" class="btn btn-warning btn-sm" />
                                                                </EditItemTemplate>

                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Id" ControlStyle-BorderStyle="None">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblDescId" runat="server" Text='<%#DataBinder.Eval(
            Container.DataItem,"DescId") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <asp:Label ID="lblEditDescId" runat="server" Text='<%#DataBinder.Eval(
            Container.DataItem,"DescId") %>'></asp:Label>
                                                                </EditItemTemplate>
                                                                <ControlStyle BorderStyle="None"></ControlStyle>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="DescEmpNomCompleto">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblDescEmpNomCompleto" runat="server" Text='<%#DataBinder.Eval(
            Container.DataItem, "DescEmpNomCompleto") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Cantidad">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblDescCantidad" runat="server" Text='<%#DataBinder.Eval(
            Container.DataItem,"DescCantidad") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <asp:TextBox ID="txtEditDescCantidad" MaxLength="7" class="form-control" AutoPostBack="true" runat="server" Text='<%#DataBinder.Eval(
            Container.DataItem, "DescCantidad") %>'></asp:TextBox>
                                                                </EditItemTemplate>

                                                            </asp:TemplateField>


                                                            <asp:TemplateField HeaderText="DescRazon">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblDescRazon" runat="server" Text='<%#DataBinder.Eval(
            Container.DataItem,"DescRazon") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="UsuNombre">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblUsuNombre" runat="server" Text='<%#DataBinder.Eval(
            Container.DataItem,"UsuNombre") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                           <asp:TemplateField HeaderText="UsuId">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblUsuIdIngresa" runat="server" Text='<%#DataBinder.Eval(
            Container.DataItem,"DescUsuIdIngresa") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>


                                                        </Columns>
                                                        <PagerSettings FirstPageText="Inicio" LastPageText="Fin" Mode="Numeric" PageButtonCount="10" />
                                                        <PagerStyle HorizontalAlign="Right" Font-Bold="False" Font-Overline="False" Font-Size="Medium" Font-Strikeout="False" Font-Underline="True" Wrap="True" />
                                                    </asp:GridView>
                                                </div>
                                            </div>

                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>

            </div>
        </div>


            <div id="myModal" class="modal inmodal fade" tabindex="-1" role="dialog" aria-hidden="true">
            <div class="modal-dialog modal-lg">
                <div class="modal-content">

                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">&times;</span><span class="sr-only">Close</span></button>

                    </div>

                    <div class="modal-body myModal">
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-white" data-dismiss="modal">Close</button>
                    </div>
                </div>
            </div>
        </div>
        <div class="contentTemporalModal" style="display: none">
        </div>


        <asp:HiddenField ID="hddTextoFiltroTareas"  runat="server" />
        <asp:HiddenField ID="hddTextoFiltroEmp"     runat="server" />
        <asp:HiddenField ID="hddTextoFiltroLotes"   runat="server" />

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
    <!--
    <script src="jquery-1.7.1.min.js" type="text/javascript"></script>
    <script src="jquery-ui.min.js" type="text/javascript"></script>
    -->
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentCodigoJs" runat="server">
    <script>
        function UserDeleteConfirmation(descripcionAlerta) {
            return confirm("Esta seguro de eliminar el registro permanentemente");
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

            //INICIO RANGO_FECHA

            $('#data_5 .input-daterange').datepicker({
                keyboardNavigation: false,
                forceParse: false,
                autoclose: true
            });



            $('input[name="daterange"]').daterangepicker();


            $('#reportrange').daterangepicker({
                format: 'DD/MM/YYYY',
                startDate: moment().subtract(29, 'days'),
                endDate: moment(),
                minDate: '01/01/2012',
                maxDate: '31/12/2030',
                dateLimit: { days: 60 },
                showDropdowns: true,
                showWeekNumbers: true,
                timePicker: false,
                timePickerIncrement: 1,
                timePicker12Hour: true,
                ranges: {
                    'Today': [moment(), moment()],
                    'Yesterday': [moment().subtract(1, 'days'), moment().subtract(1, 'days')],
                    'Last 7 Days': [moment().subtract(6, 'days'), moment()],
                    'Last 30 Days': [moment().subtract(29, 'days'), moment()],
                    'This Month': [moment().startOf('month'), moment().endOf('month')],
                    'Last Month': [moment().subtract(1, 'month').startOf('month'), moment().subtract(1, 'month').endOf('month')]
                },
                opens: 'right',
                drops: 'down',
                buttonClasses: ['btn', 'btn-sm'],
                applyClass: 'btn-primary',
                cancelClass: 'btn-default',
                separator: ' to ',
                locale: {
                    applyLabel: 'Submit',
                    cancelLabel: 'Cancel',
                    fromLabel: 'From',
                    toLabel: 'To',
                    customRangeLabel: 'Custom',
                    daysOfWeek: ['Su', 'Mo', 'Tu', 'We', 'Th', 'Fr', 'Sa'],
                    monthNames: ['January', 'February', 'March', 'April', 'May', 'June', 'July', 'August', 'September', 'October', 'November', 'December'],
                    firstDay: 1
                }
            }, function (start, end, label) {
                console.log(start.toISOString(), end.toISOString(), label);
                $('#reportrange span').html(start.format('DDDD M, YYYY') + ' - ' + end.format('DDDD M, YYYY'));
            });

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



        function modalEmp() {
            moverContentTemporalModal();
            jQuery('.modalEmpleados').appendTo('.myModal');
            $('#myModal').modal();
        }


        function moverContentTemporalModal()
        {
            jQuery('.modalEmpleados').appendTo('.contentTemporalModal');
        }

        function cerrarMyModal() {
            $('#myModal').modal('hide');
        }

        $("#lblNombreEmpleado").click(function () {
            $('#myModalEmp').modal();
        });

        function tituloModal(data) {
            $('#tituloModal').text(data);
        }





        function iniciarFiltrarTextoModal() {


            var textoEmpleados = $(".txtBuscarEmpClass").val();
            $("#<%= hddTextoFiltroEmp.ClientID %>").val(textoEmpleados);

            return true;

        }

    </script>

    <script>

        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(InIEvent);
    </script></asp:Content>
