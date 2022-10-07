<%@ Page Title="" Language="C#" MasterPageFile="~/Inicio.Master" AutoEventWireup="true" CodeBehind="Empleados.aspx.cs" Inherits="Cabana.Campo.RAAgricola.Pre.Web.Empleados" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContenedorEncabezado" runat="server">
    
                        <h2>Mantenimiento de Empleados</h2>
                    <ol class="breadcrumb">
                        <li>
                            <a href="Home.aspx">Inicio</a>
                        </li>
                        <li>
                            <a>Transacciones</a>
                        </li>
                        <li class="active">
                            <strong>Empleados</strong>
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
                                <asp:TextBox ID="txtControlBuscarEmpleado" runat="server" class="input form-control" OnTextChanged="lbtnBuscarEmpleado_Click" AutoPostBack="true" ></asp:TextBox>
                                <span class="input-group-btn">
                                <asp:LinkButton ID="lbtnBuscarNombreEmp" runat="server" class="btn btn btn-primary"  OnClick="lbtnBuscarEmpleado_Click"><i class="fa fa-search"></i></asp:LinkButton>
                                </span>
                            </div>
                            <div class="full-height-scroll">
                            <div class="table-responsive">


                           <asp:GridView ID="gvBuscarEmpleados" class="table table-striped table-hover"  GridLines="None" BorderStyle="None" BorderColor="White" runat="server" Width="100%" AllowPaging="True"  OnPageIndexChanging="gvBuscarEmpleados_PageIndexChanging"  AllowSorting="True"
                               AutoGenerateColumns="false" ShowFooter="true" ShowHeaderWhenEmpty="true"
                               onrowcommand="gvBuscarEmpleados_RowCommand"
                                onrowdeleting       ="gvBuscarEmpleados_RowDeleting"
                                onrowcancelingedit  ="gvBuscarEmpleados_RowCancelingEdit"
                                onrowediting        ="gvBuscarEmpleados_RowEditing"
                                onrowupdating       ="gvBuscarEmpleados_RowUpdating"                               
                               >
                            <Columns>
                               <asp:TemplateField HeaderText="Action">
                                    <ItemTemplate>
                                       <asp:LinkButton ID="gvlbtn_editEmpleado" runat="server" CommandName="Edit"  class="btn btn-outline btn-warning btn-sm"  > <i class="fa fa-edit"></i></asp:LinkButton>
                                   
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                       <asp:LinkButton ID="gvlbtn_deleteEmpleado" runat="server" CommandName="Delete"  class="btn btn-danger btn-sm"  OnClientClick="if ( ! UserConfirmation('Eliminar este registro')) return false;" >Eliminar <i class="fa fa-remove"></i></asp:LinkButton>
                                        <span class="help-block m-b-none"></span>
                                       <asp:LinkButton ID="gvlbtn_updateEmpleado" runat="server" CommandName="Update"  class="btn btn-primary btn-sm"  OnClientClick="if ( ! UserConfirmation('Actualizar este registro')) return false;" >Actualizar <i class="fa fa-check"></i></asp:LinkButton>
                                        <span class="help-block m-b-none"></span>
                                       <asp:Button ID="gvlbtn_cancelEmpleado" runat="server" CommandName="Cancel" Text="Cancelar" class="btn btn-warning btn-sm" />
                                    </EditItemTemplate>
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
                            <h5>Formulario para registro de empleados</h5>
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

                                <p>Empleado.</p>
                                <div class="form-group">   
                                    <asp:HiddenField ID="hddFrmEmpId" runat="server"/> 
                                    <label class="col-lg-2 control-label">Estatus*</label>
                                    <div class="col-lg-10">
                                        <asp:DropDownList ID="ddlFrmEmpEstatus" runat="server" class="form-control m-b">
                                            <asp:ListItem value ="a"> Activo</asp:ListItem>
                                            <asp:ListItem value ="i"> Inactivo</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                                                                                        
                                    <label class="col-lg-2 control-label">Frente*</label>
                                    <div class="col-lg-10">
                                        <asp:DropDownList ID="ddlFrmEmpFrente" runat="server" class="form-control m-b" OnSelectedIndexChanged="ddlistFrmEmpFrentes_Change"  AutoPostBack="true">
                                            <asp:ListItem value ="0"> Seleccionar Frente</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>

                                    <label class="col-lg-2 control-label">Cuadrilla*</label>
                                    <div class="col-lg-10">
                                        <asp:DropDownList ID="ddlFrmEmpCuadrilla" runat="server" class="form-control m-b">
                                            <asp:ListItem value ="0"> Seleccionar Cuadrilla</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>

                                    <label class="col-lg-2 control-label">Tipo*</label>
                                    <div class="col-lg-10">
                                        <asp:DropDownList ID="ddlFrmEmpTipo" runat="server" class="form-control m-b">
                                            <asp:ListItem value ="0"> Seleccionar Tipo Empleado</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <label class="col-lg-2 control-label">Nombres*</label>
                                    <div class="col-lg-10">
                                        <asp:TextBox ID="txtFrmEmpNombres" runat="server" class="form-control m-b" ></asp:TextBox> 
                                        <span class="help-block m-b-none"> </span>
                                    </div>
                                    <label class="col-lg-2 control-label">Apellidos*</label>
                                    <div class="col-lg-10">
                                        <asp:TextBox ID="txtFrmEmpApellidos" runat="server" class="form-control" ></asp:TextBox> 
                                        <span class="help-block m-b-none"> </span>
                                    </div>

                                    <label class="col-lg-2 control-label">Sexo*</label>
                                    <div class="col-lg-10">
                                        <asp:RadioButtonList ID="rblFrmEmpSexo" runat="server" class="radio radio-inline">
                                            <asp:ListItem Text="Masculino" Value="M" Selected="True"/>
                                            <asp:ListItem Text="Femenino"  Value="F" Selected="False"/>
                                        </asp:RadioButtonList>
                                        <span class="help-block m-b-none"> </span>
                                    </div>

                                    <label class="col-lg-2 control-label">Fecha*</label>
                                    <div class="col-lg-10">
                                        <asp:TextBox ID="txtFrmEmpFechaNacimiento" runat="server" placeholder="Fecha Nacimiento" class="form-control" value="" data-mask ="99/99/9999" />
                                        <span class="help-block m-b-none"> </span>
                                    </div>

                                    <label class="col-lg-2 control-label">Dui*</label>
                                    <div class="col-lg-10">
                                        <asp:TextBox ID="txtFrmEmpNumDui" runat="server" placeholder="Numero Dui" class="form-control" value="" data-mask ="99999999-9" />
                                        <span class="help-block m-b-none"> </span>
                                    </div>

                                  <label class="col-lg-2 control-label">Telefono</label>
                                    <div class="col-lg-10">
                                        <asp:TextBox ID="txtFrmEmpTelFijo" runat="server" placeholder="Telefono Casa" class="form-control" value="" data-mask ="9999-9999" />
                                        <span class="help-block m-b-none"> </span>
                                    </div>

                                  <label class="col-lg-2 control-label">Celular</label>
                                    <div class="col-lg-10">
                                        <asp:TextBox ID="txtFrmEmpTelCelular" runat="server" placeholder="Telefono Celular" class="form-control" value="" data-mask ="9999-9999" />
                                        <span class="help-block m-b-none"> </span>
                                    </div>

                                  <label class="col-lg-2 control-label">Emergencia</label>
                                    <div class="col-lg-10">
                                        <asp:TextBox ID="txtFrmEmpTelEmergencia" runat="server" placeholder="Telefono de Emergencia" class="form-control" value="" data-mask ="9999-9999" />
                                        <span class="help-block m-b-none"> </span>
                                    </div>

                                  <label class="col-lg-2 control-label">Email</label>
                                    <div class="col-lg-10">

                                       <asp:TextBox ID="txtFrmEmpEmail" runat="server" placeholder="Correo Electronico" class="form-control" value="" />
                                        <span class="help-block m-b-none"></span>

                                    </div>

                                  <label class="col-lg-2 control-label">NIT</label>
                                    <div class="col-lg-10">
                                        <asp:TextBox ID="txtFrmEmpNit" runat="server" placeholder="Numero de NIT" class="form-control" value="" data-mask ="9999-999999-999-9" />
                                        <span class="help-block m-b-none"> </span>
                                    </div>

                                  <label class="col-lg-2 control-label">ISSS</label>
                                    <div class="col-lg-10">
                                        <asp:TextBox ID="txtFrmEmpIsss" runat="server" placeholder="Numero de ISSS" class="form-control" value=""/>
                                        <span class="help-block m-b-none"> </span>
                                    </div>

                                  <label class="col-lg-2 control-label">AFP</label>
                                    <div class="col-lg-10">
                                        <asp:TextBox ID="txtFrmEmpAfp" runat="server" placeholder="Numero de AFP" class="form-control" value="" />
                                        <span class="help-block m-b-none"> </span>
                                    </div>

                                  <label class="col-lg-2 control-label">Direccion*</label>
                                    <div class="col-lg-10">
                                        <asp:TextBox ID="txtFrmEmpDireccion" runat="server" placeholder="Direccion" class="form-control" value="" TextMode="MultiLine" />
                                        <span class="help-block m-b-none"> </span>
                                    </div>
                                    

                                </div>


                                <div class="form-group">
                                </div>


                                <div class="form-group">
                                    <div class="col-lg-offset-2 col-lg-10">
                                        <!--<div class="i-checks"><label> <input type="checkbox"><i></i> Aceptar </label></div>-->
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="col-lg-offset-2 col-lg-10">
                                       <asp:LinkButton ID="lbtn_FrmEmpNuevo" runat="server" class="btn btn btn-primary"  OnClick="lbtn_FrmEmpNuevo_Click">Guardar <i class="fa fa-save"></i></asp:LinkButton>
                                    </div>
                                </div>
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
