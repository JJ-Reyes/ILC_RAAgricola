<%@ Page Title="" Language="C#" MasterPageFile="~/Inicio.Master" AutoEventWireup="true" CodeBehind="EmpPorCuadrillas.aspx.cs" Inherits="Cabana.Campo.RAAgricola.Pre.Web.EmpPorCuadrillas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContenedorEncabezado" runat="server">
    
                        <h2>Mantenimiento de Empleados por Cuadrillas</h2>
                    <ol class="breadcrumb">
                        <li>
                            <a href="Home.aspx">Inicio</a>
                        </li>
                        <li>
                            <a>Transacciones</a>
                        </li>
                        <li class="active">
                            <strong>EmpCuadrillas</strong>
                        </li>
                    </ol>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <form role="form" id="form" runat="server">
         <asp:ScriptManager ID="ScriptManager1" runat="server">
         </asp:ScriptManager>

        <div class="wrapper wrapper-content  animated fadeInRight">
            <div class="row">


                <div class="col-sm-5">
                    <div class="ibox">
                        <div class="ibox-content">

                            <h3>Empleados</h3>

         <asp:UpdatePanel ID="UpdatePanel5" runat="server">
         <ContentTemplate>
                <asp:HiddenField ID="hddCuadrillaId" runat="server" Value="0"/>
        </ContentTemplate>
        </asp:UpdatePanel>                           

                            <div class="clients-list">
                            <ul class="nav nav-tabs">
                                <li class="active"> <a data-toggle="tab" href="#tab-1"><i class="fa fa-user"></i>Emp..</a></li>

                            </ul>
                            <div class="tab-content">
                <div id="tab-1" class="tab-pane active">
        <asp:UpdateProgress ID="UpdateProgress1" runat="server">
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
         <asp:UpdatePanel ID="UpdatePanel1" runat="server">
         <ContentTemplate> 
                <a data-toggle="tab">
                <asp:DropDownList ID="ddlistCuadrilla" runat="server" class="form-control m-b" OnSelectedIndexChanged="ddlistCuadrillas_Change"  AutoPostBack="true">
                    <asp:ListItem value ="0"> Seleccionar Cuadrilla</asp:ListItem>
                </asp:DropDownList>
                </a>
                            <div class="input-group">
                                <asp:TextBox ID="txtControlBuscarEmpleado" runat="server" class="input form-control" OnTextChanged="lbtnBuscarEmpleado_Click" AutoPostBack="true"></asp:TextBox>
                                <span class="input-group-btn">
                                    <asp:LinkButton ID="lbtnBuscarNombreEmp" runat="server" class="btn btn btn-primary"  OnClick="lbtnBuscarEmpleado_Click"><i class="fa fa-search"></i></asp:LinkButton>
                                </span>
                            </div>
                            <div class="full-height-scroll">
                            <div class="table-responsive">


                           <asp:GridView ID="gvBuscarEmpleados" class="table table-striped table-hover"  GridLines="None" BorderStyle="None" BorderColor="White" runat="server" Width="100%" AllowPaging="True"  OnPageIndexChanging="gvBuscarEmpleados_PageIndexChanging"  AllowSorting="True"
                               AutoGenerateColumns="false" ShowFooter="true" ShowHeaderWhenEmpty="true"
                                onrowcommand        ="gvBuscarEmpleados_RowCommand"
                                onrowdeleting       ="gvBuscarEmpleados_RowDeleting"
                                onrowcancelingedit  ="gvBuscarEmpleados_RowCancelingEdit"
                                onrowediting        ="gvBuscarEmpleados_RowEditing"
                               >
                            <Columns>
                               <asp:TemplateField HeaderText="Action">
                                    <ItemTemplate>
                                       <asp:LinkButton ID="gvlbtn_editEmpleado" runat="server" CommandName="Edit"  class="btn btn-outline btn-warning btn-sm"  > <i class="fa fa-edit"></i></asp:LinkButton>

                                    </ItemTemplate>
                                    <EditItemTemplate>
                                       <asp:LinkButton ID="gvlbtn_deleteEmpleado" runat="server" CommandName="Delete"  class="btn btn-danger btn-sm"  OnClientClick="if ( ! UserConfirmation('Eliminar este registro')) return false;" ><i class="fa fa-remove"></i></asp:LinkButton>

                                        <span class="help-block m-b-none"></span>
                                       <asp:Button ID="gvlbtn_cancelEmpleado" runat="server" CommandName="Cancel" Text="Cancelar" class="btn btn-warning btn-sm" />
                                    </EditItemTemplate>
                                </asp:TemplateField>
                              
                                <asp:TemplateField HeaderText="ID">
                                    <ItemTemplate>
                                        <asp:HiddenField ID="gvhdd_EcuId" runat="server" Value='<%#DataBinder.Eval( Container.DataItem,"EcuId") %>'/>

                                        <asp:Label ID="lblBuscarEmpId" runat="server" Text='<%#DataBinder.Eval(
            Container.DataItem,"EmpId") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
					
                                <asp:TemplateField HeaderText="Nombre">
                                    <ItemTemplate>
                                        <asp:Label ID="lblBuscarEmpNombre" runat="server" Text='<%#DataBinder.Eval(
            Container.DataItem, "nombreCompleto") %>'></asp:Label>

                                    </ItemTemplate>
                                </asp:TemplateField>

                               <asp:TemplateField HeaderText="DUI">
                                    <ItemTemplate>
                                        <asp:Label ID="lblEmpDui" runat="server" Text='<%#DataBinder.Eval(
            Container.DataItem,"EmpDUI") %>'></asp:Label>

                                    </ItemTemplate>
                                </asp:TemplateField>


                            </Columns>  
                              <PagerSettings FirstPageText="Inicio" LastPageText="Fin" Mode="Numeric" PageButtonCount="10" />           
                              <PagerStyle   HorizontalAlign = "Right" Font-Bold="False" Font-Overline="False" Font-Size="Medium" Font-Strikeout="False" Font-Underline="True" Wrap="True" />
                        </asp:GridView>

                        </div>
                        </div>             
             
        </ContentTemplate>
        </asp:UpdatePanel>

                </div>

                            </div>

                            </div>
                        </div>
                    </div>
                </div>

        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>

                <div class="col-sm-7 white-bg">

                    <div class="ibox float-e-margins">
                        <div class="ibox-title">
                            <h5>Listado de empleados disponibles</h5>
                            <div class="ibox-tools">
                                <a class="collapse-link">
                                    <i class="fa fa-chevron-up"></i>
                                </a>
                                <a class="dropdown-toggle" data-toggle="dropdown" href="#">
                                    <i class="fa fa-wrench"></i>
                                </a>
                                <ul class="dropdown-menu dropdown-user">
                                    <li><a href="#">Config option 1</a>
                                    </li>
                                    <li><a href="#">Config option 2</a>
                                    </li>
                                </ul>
                            </div>
                        </div>
                        <div class="ibox-content">

                                <p>Empleados.</p>


                            <div id="demo"></div>
                            
                           <asp:GridView ID="gvListadoEmpleados" class="table table-striped table-hover"  GridLines="None" BorderStyle="None" BorderColor="White" runat="server" Width="100%" AllowPaging="True"  OnPageIndexChanging="gvListadoEmpleados_PageIndexChanging"  AllowSorting="True"
                               AutoGenerateColumns="false" ShowFooter="true" ShowHeaderWhenEmpty="true"
                               onrowcommand     ="gvListadoEmpleados_RowCommand"
                               onrowupdating    ="gvListadoEmpleados_RowUpdating">

                            <Columns>
                               <asp:TemplateField HeaderText="Action">
                                    <ItemTemplate>
                                       <asp:LinkButton ID="gvlbtn_updateEmpleado" runat="server" CommandName="Update"  class="btn btn-primary btn-sm"  OnClientClick="if ( ! UserConfirmation('¿Desea agregar este empleado a la cuadrilla seleccionada?')) return false;" > <i class="fa fa-plus"></i></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                              
                                <asp:TemplateField HeaderText="ID">
                                    <ItemTemplate>
                                        <asp:Label ID="lblBuscarEmpId" runat="server" Text='<%#DataBinder.Eval(
            Container.DataItem,"EmpId") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
					
                                <asp:TemplateField HeaderText="Nombre">
                                    <ItemTemplate>
                                        <asp:Label ID="lblBuscarEmpNombre" runat="server" Text='<%#DataBinder.Eval(
            Container.DataItem, "EmpNombres") %>'></asp:Label>
                                        <asp:Label ID="Label1" runat="server" Text=' <%#DataBinder.Eval(
            Container.DataItem, "EmpApellidos") %>'></asp:Label>

                                    </ItemTemplate>
                                </asp:TemplateField>

                               <asp:TemplateField HeaderText="DUI">
                                    <ItemTemplate>
                                        <asp:Label ID="lblEmpDui" runat="server" Text='<%#DataBinder.Eval(
            Container.DataItem,"EmpDui") %>'></asp:Label>

                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="E..">
                                    <ItemTemplate>
                                        <asp:Label ID="lblEmpEstatus" runat="server" Text='<%#DataBinder.Eval(
            Container.DataItem,"EmpEstatus") %>'></asp:Label>

                                    </ItemTemplate>
                                </asp:TemplateField>

                            </Columns>  
                              <PagerSettings FirstPageText="Inicio" LastPageText="Fin" Mode="Numeric" PageButtonCount="10" />           
                              <PagerStyle   HorizontalAlign = "Right" Font-Bold="False" Font-Overline="False" Font-Size="Medium" Font-Strikeout="False" Font-Underline="True" Wrap="True" />
                        </asp:GridView>


                        </div>
                    </div>


                </div>
        </ContentTemplate>
        </asp:UpdatePanel>

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
    <!--
    <script src="jquery-1.7.1.min.js" type="text/javascript"></script>
    <script src="jquery-ui.min.js" type="text/javascript"></script>
    -->
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentCodigoJs" runat="server">

 <link href="../Content/css/datatables.min.css" rel="stylesheet" />
	<script src="js/libs/jquery.tablesorter.min.js"></script>
	<script src="js/libs/DataTables/jquery.dataTables.min.js"></script>
    <script src="js/libs/DataTables/jquery.dataTables.columnFilter.js"></script>
	<script src="js/libs/DataTableExportacion/jquery.dataTables.js"></script>
	<script src="js/libs/DataTableExportacion/dataTables.tableTools.js"></script>	



    <script>
    function UserDeleteConfirmation(descripcionAlerta) {
        return confirm("Esta seguro de eliminar el registro permanentemente");
    }

    function UserConfirmation(descripcionAlerta) {
        return confirm(""+descripcionAlerta);
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
        //FIN VALIDACION DE FORMULARIO 
        
    </script>

    <script>

        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(InIEvent);
    </script>
</asp:Content>