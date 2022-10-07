<%@ Page Title="" Language="C#" MasterPageFile="~/Inicio.Master" AutoEventWireup="true" CodeBehind="FrentesCuadrillas.aspx.cs" Inherits="Cabana.Campo.RAAgricola.Pre.Web.FrentesCuadrillas" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContenedorEncabezado" runat="server">
    
                        <h2>Mantenimiento frentes, Cuadrillas y Tipos de Empleado</h2>
                    <ol class="breadcrumb">
                        <li>
                            <a href="Home.aspx">Inicio</a>
                        </li>
                        <li>
                            <a>Transacciones</a>
                        </li>
                        <li class="active">
                            <strong>Mantenimiento frentes, Cuadrillas y Tipos de Empleado</strong>
                        </li>
                    </ol>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <form role="form" id="form" runat="server">
         <asp:ScriptManager ID="ScriptManager1" runat="server">
         </asp:ScriptManager>

        <div class="wrapper wrapper-content  animated fadeInRight">
            <div class="row">


                <div class="col-sm-10">
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
                                <li class="active"> <a data-toggle="tab" href="#tab-2"><i class="fa fa-user"></i>Frentes</a></li>
                                <li class="">       <a data-toggle="tab" href="#tab-3"><i class="fa fa-briefcase"></i>Cuadrillas</a></li>
                                <li class="">       <a data-toggle="tab" href="#tab-4"><i class="fa fa-briefcase"></i>Tipos de Empleado</a></li>
                            </ul>
                            <div class="tab-content">

                <div id="tab-3" class="tab-pane">
         <asp:UpdatePanel ID="UpdatePanel4" runat="server">
         <ContentTemplate>
                <div class="input-group"><p></p></div>
                <p><b>Frentes:</b></p>
                <a data-toggle="tab">
                <asp:DropDownList ID="ddlistFrente" runat="server" class="form-control m-b" OnSelectedIndexChanged="ddlistFrentes_Change" AutoPostBack="true">
                    <asp:ListItem value ="0"> Seleccionar Frente</asp:ListItem>
                </asp:DropDownList>
                </a>
                <div class="input-group"><p></p></div>
                <p><b>Nombre de la nueva Cuadrilla:</b></p>
                            <div class="input-group">
                                <asp:TextBox ID="txtAddNombreCuadrilla" runat="server" class="input form-control"></asp:TextBox>
                                <span class="input-group-btn">
                                    <asp:LinkButton ID="lbtnAddNuevoCuadrilla" runat="server" class="btn btn btn-primary"  OnClick="lbtnAddNuevoCuadrilla_Click"><i class="fa fa-save"></i></asp:LinkButton>
                                </span>
                            </div>
                            <div class="full-height-scroll">
                            <div class="table-responsive">


                           <asp:GridView ID="gvCuadrillas" class="table table-striped table-hover"  GridLines="None" BorderStyle="None" BorderColor="White" runat="server" Width="100%" AllowPaging="True"  OnPageIndexChanging="gvCuadrillas_PageIndexChanging"  AllowSorting="True"
                               AutoGenerateColumns="false" ShowFooter="true" ShowHeaderWhenEmpty="true"
                                onrowcommand        ="gvCuadrillas_RowCommand"
                                onrowdeleting       ="gvCuadrillas_RowDeleting"
                                onrowcancelingedit  ="gvCuadrillas_RowCancelingEdit"
                                onrowediting        ="gvCuadrillas_RowEditing"
                                onrowupdating       ="gvCuadrillas_RowUpdating"
                               >
                            <Columns>
                               <asp:TemplateField HeaderText="Action">
                                    <ItemTemplate>
                                       <asp:LinkButton ID="gvlbtn_editCuadrilla" runat="server" CommandName="Edit"  class="btn btn-outline btn-warning btn-sm"  > <i class="fa fa-edit"></i></asp:LinkButton>
                                       <asp:LinkButton ID="gvlbtn_deleteCuadrilla" runat="server" CommandName="Delete"  class="btn btn-outline btn-danger btn-sm"  OnClientClick="if ( ! UserDeleteConfirmation()) return false;" > <i class="fa fa-remove"></i></asp:LinkButton>
                                   
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                       <asp:LinkButton ID="gvlbtn_updateCuadrilla" runat="server" CommandName="Update"  class="btn btn-primary btn-sm"  OnClientClick="if ( ! UserDeleteConfirmation()) return false;" >Actualizar <i class="fa fa-check"></i></asp:LinkButton>

                                       <asp:Button ID="gvlbtn_cancelCuadrilla" runat="server" CommandName="Cancel" Text="Cancelar" class="btn btn-warning btn-sm" />
                                    </EditItemTemplate>
                                </asp:TemplateField>
                              
                                <asp:TemplateField HeaderText="ID">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCuaId" runat="server" Text='<%#DataBinder.Eval(
            Container.DataItem,"CuaId") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
					
                                <asp:TemplateField HeaderText="Nombre">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCuaNombre" runat="server" Text='<%#DataBinder.Eval(
            Container.DataItem, "CuaNombre") %>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>           
                                      <asp:TextBox  ID="txtEditCuaNombre" class="form-control" runat="server" Text='<%#DataBinder.Eval(
            Container.DataItem, "CuaNombre") %>'></asp:TextBox>    
                                    </EditItemTemplate>
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

                <div id="tab-4" class="tab-pane">
         <asp:UpdatePanel ID="UpdatePanel3" runat="server">
         <ContentTemplate>
                            <div class="input-group"><p></p></div>
                            <p><b>Nombre del Nuevo Tipo de Empleado</b></p>
                            <div class="input-group">
                                <asp:TextBox ID="txtAddNombreTipoEmp" runat="server" class="input form-control"></asp:TextBox>
                                <span class="input-group-btn">
                                    <asp:LinkButton ID="lbtnAddNuevoTipoEmp" runat="server" class="btn btn btn-primary"  OnClick="lbtnAddNuevoTipoEmp_Click"><i class="fa fa-save"></i></asp:LinkButton>
                                </span>
                            </div>
                            <div class="full-height-scroll">
                            <div class="table-responsive">


                           <asp:GridView ID="gvTipoEmpleado" class="table table-striped table-hover"  GridLines="None" BorderStyle="None" BorderColor="White" runat="server" Width="100%" AllowPaging="True"  OnPageIndexChanging="gvTipoEmpleado_PageIndexChanging"  AllowSorting="True"
                               AutoGenerateColumns="false" ShowFooter="true" ShowHeaderWhenEmpty="true"
                                onrowcommand        ="gvTipoEmpleado_RowCommand"
                                onrowdeleting       ="gvTipoEmpleado_RowDeleting"
                                onrowcancelingedit  ="gvTipoEmpleado_RowCancelingEdit"
                                onrowediting        ="gvTipoEmpleado_RowEditing"
                                onrowupdating       ="gvTipoEmpleado_RowUpdating"
                               >
                            <Columns>
                               <asp:TemplateField HeaderText="Action">
                                    <ItemTemplate>
                                       <asp:LinkButton ID="gvlbtn_editTipoEmpleado" runat="server" CommandName="Edit"  class="btn btn-outline btn-warning btn-sm"  > <i class="fa fa-edit"></i></asp:LinkButton>
                                       <asp:LinkButton ID="gvlbtn_deleteTipoEmpleado" runat="server" CommandName="Delete"  class="btn btn-outline btn-danger btn-sm"  OnClientClick="if ( ! UserDeleteConfirmation()) return false;" > <i class="fa fa-remove"></i></asp:LinkButton>
                                   
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                       <asp:Button ID="gvlbtn_updateTipoEmpleado" runat="server" CommandName="Update" Text="Aceptar" class="btn btn-danger btn-sm" />
                                       <asp:Button ID="gvlbtn_cancelTipoEmpleado" runat="server" CommandName="Cancel" Text="Cancelar" class="btn btn-warning btn-sm" />
                                    </EditItemTemplate>
                                </asp:TemplateField>
                              
                                <asp:TemplateField HeaderText="ID">
                                    <ItemTemplate>
                                        <asp:Label ID="lblTipoId" runat="server" Text='<%#DataBinder.Eval(
            Container.DataItem,"TipoId") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
					
                                <asp:TemplateField HeaderText="Nombre">
                                    <ItemTemplate>
                                        <asp:Label ID="lblTipoEmpDesc" runat="server" Text='<%#DataBinder.Eval(
            Container.DataItem, "TipoEmpDesc") %>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>           
                                      <asp:TextBox  ID="txtEditTipoEmpDesc" class="form-control" runat="server" Text='<%#DataBinder.Eval(
            Container.DataItem, "TipoEmpDesc") %>'></asp:TextBox>    
                                    </EditItemTemplate>
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

                <div id="tab-2" class="tab-pane active">
         <asp:UpdatePanel ID="UpdatePanel6" runat="server">
         <ContentTemplate>
                <div class="input-group"><p></p></div>
                <p><b>Nombre del Nuevo Frente:</b></p>
                            <div class="input-group">
                                <asp:TextBox ID="txtAddNombreFrente" runat="server" class="input form-control" ></asp:TextBox>
                                <span class="input-group-btn">
                                    <asp:LinkButton ID="lbtnAddNuevoFrente" runat="server" class="btn btn btn-warning"  OnClick="lbtnAddNuevoFrente_Click"><i class="fa fa-save"></i></asp:LinkButton>
                                </span>
                            </div>
                            <div class="full-height-scroll">
                            <div class="table-responsive">

                           <asp:GridView ID="gvFrentes" class="table table-striped table-hover"  GridLines="None" BorderStyle="None" BorderColor="White" runat="server" Width="100%" AllowPaging="True"  OnPageIndexChanging="gvFrentes_PageIndexChanging"  AllowSorting="True"
                               AutoGenerateColumns="false" ShowFooter="true" ShowHeader="true" ShowHeaderWhenEmpty="true"
                                onrowcommand        ="gvFrentes_RowCommand"
                                onrowdeleting       ="gvFrentes_RowDeleting"
                                onrowcancelingedit  ="gvFrentes_RowCancelingEdit"
                                onrowediting        ="gvFrentes_RowEditing"
                                onrowupdating       ="gvFrentes_RowUpdating"
                               >
                            <Columns> 
                               <asp:TemplateField HeaderText="Action">
                                    <ItemTemplate>
                                       <asp:LinkButton ID="gvlbtn_editFrentes" runat="server" CommandName="Edit"  class="btn btn-outline btn-warning btn-sm"  > <i class="fa fa-edit"></i></asp:LinkButton>
                                       <asp:LinkButton ID="gvlbtn_deleteFrentes" runat="server" CommandName="Delete"  class="btn btn-outline btn-danger btn-sm"  OnClientClick="if ( ! UserDeleteConfirmation()) return false;" > <i class="fa fa-remove"></i></asp:LinkButton>
                                   
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                       <asp:Button ID="gvlbtn_updateFrentes" runat="server" CommandName="Update" Text="Aceptar" class="btn btn-danger btn-sm" />
                                       <asp:Button ID="gvlbtn_cancelFrentes" runat="server" CommandName="Cancel" Text="Cancelar" class="btn btn-warning btn-sm" />
                                    </EditItemTemplate>
                                </asp:TemplateField>
                              
                                <asp:TemplateField HeaderText="ID">
                                    <ItemTemplate>
                                        <asp:Label ID="lblFrenteId" runat="server" Text='<%#DataBinder.Eval(
            Container.DataItem,"FrenteId") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderTemplate>
                                         <asp:Label ID="Labe3" runat="server" Text="ID" Font-Bold="true"></asp:Label><br></br>
                                    </HeaderTemplate>
                                </asp:TemplateField>
					
                                <asp:TemplateField HeaderText="Frente">
                                    <ItemTemplate>
                                        <asp:Label ID="lblFrenteNombre" runat="server" Text='<%#DataBinder.Eval(
            Container.DataItem, "FrenteNombre") %>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>           
                                      <asp:TextBox  ID="txtEditFrenteNombre" class="form-control" runat="server" Text='<%#DataBinder.Eval(
            Container.DataItem, "FrenteNombre") %>'></asp:TextBox>    
                                    </EditItemTemplate>
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

                <div class="col-sm-2 white-bg">




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
<script>
    function UserDeleteConfirmation(descripcionAlerta) {
        return confirm("Esta seguro de eliminar el registro permanentemente");
    }

    function UserConfirmation(descripcionAlerta) {
        return confirm("Esta seguro de eliminar el registro permanentemente ");
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