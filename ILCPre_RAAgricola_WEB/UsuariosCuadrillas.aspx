<%@ Page Title="" Language="C#" MasterPageFile="~/Inicio.Master" AutoEventWireup="true" CodeBehind="UsuariosCuadrillas.aspx.cs" Inherits="Cabana.Campo.RAAgricola.Pre.Web.UsuariosCuadrillas" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContenedorEncabezado" runat="server">
                        <h2>Asignacion de Cuadrillas a Usuarios</h2>
                    <ol class="breadcrumb">
                        <li>
                            <a href="Home.aspx">Home</a>
                        </li>
                        <li class="active">
                            <strong>Usuarios</strong>
                        </li>
                    </ol>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


    <form role="form" id="form" runat="server">
         <asp:ScriptManager ID="ScriptManager1" runat="server">

         </asp:ScriptManager>

        <div class="wrapper wrapper-content  animated fadeInRight">
            <div class="row">


                <div class="col-sm-4">
                    <div class="ibox">
                        <div class="ibox-content">

                            <h3>Fincas</h3>                          

                            <div class="clients-list">
                            <ul class="nav nav-tabs">
                                <li class="active"><a data-toggle="tab" href="#tab-1"><i class="fa fa-user"></i>Registro para Usuarios</a></li>
                            </ul>
                            <div class="tab-content">
         <div id="tab-1" class="tab-pane active">



        <div class="full-height-scroll">
        <div class="table-responsive">
         <asp:UpdatePanel ID="UpdatePanel1" runat="server">
         <ContentTemplate>
                    <span class="help-block m-b-none"> </span> 
                <p>Usuario</p>
                <asp:DropDownList ID="ddl_UsuariosEmpr" OnSelectedIndexChanged="ddl_UsuariosEmpr_Change" AutoPostBack="true"  runat="server" class="form-control m-b" >
                    <asp:ListItem value ="0">Seleccione un Usuario</asp:ListItem>

                </asp:DropDownList>

                <p>Frentes</p>
                <asp:DropDownList ID="ddl_Frentes" OnSelectedIndexChanged="ddl_Frentes_Change" AutoPostBack="true" runat="server" class="form-control m-b" >
                    <asp:ListItem value ="0">Seleccione un Frente</asp:ListItem>

                </asp:DropDownList>

                <p>Cuadrillas</p>
                <asp:DropDownList ID="ddl_Cuadrillas" runat="server" class="form-control m-b" >
                    <asp:ListItem value ="0">Seleccione un Frente antes</asp:ListItem>

                </asp:DropDownList>

                    <span class="help-block m-b-none"> </span>

                <asp:LinkButton ID="lbtn_AddNewTarea" runat="server" class="btn btn btn-primary"  OnClick="lbtn_AddNewAccesoUsuCuadrillas_Click">Agregar<i class="fa fa-save"></i></asp:LinkButton>


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



                <div class="col-sm-8">

                    <div class="ibox">

                        <div class="ibox-content">
                            <div class="tab-content">

         <div class="ibox-content m-b-sm border-bottom ">
         <asp:UpdatePanel ID="UpdatePanel6" runat="server" UpdateMode="Conditional">
         <ContentTemplate>                           
                            <div class="input-group">

                                <asp:TextBox ID="txtControlBuscarTareas" runat="server" Text="" AutoPostBack="true" OnTextChanged="lbtnBuscarAccesoUsuCuadrillas_Click"  class="input form-control" ></asp:TextBox>
                                <span class="input-group-btn">
                                    <asp:LinkButton ID="lbtnBuscarTarea" runat="server" class="btn btn btn-warning"  OnClick="lbtnBuscarAccesoUsuCuadrillas_Click">Buscar <i class="fa fa-search"></i></asp:LinkButton>
      
                                </span>
                            </div>
        </ContentTemplate>
        </asp:UpdatePanel>
        </div>

                    
                            <br />
                            <div class="clients-list">
                                <span class="pull-right small text-muted">Listado de Permisos</span>
                            <ul class="nav nav-tabs">
                                <li class="active"><a data-toggle="tab" href="#tab-3"><i class="fa fa-user"></i>Cuadrillas ya asignadas</a></li>
                            </ul>
                                    <div class="full-height-scroll">
                                    <div class="table-responsive">
        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>
                           <asp:GridView class="table table-striped table-hover" ID="gvAccesoUsuCuadrillas" GridLines="None" BorderStyle="None" BorderColor="White" runat="server" Width="100%" AllowPaging="True"  OnPageIndexChanging="gvAccesoUsuCuadrillas_PageIndexChanging"  AllowSorting="True"
                                AutoGenerateColumns="false" ShowFooter="true" ShowHeaderWhenEmpty="true"
                                onrowcommand="gvAccesoUsuCuadrillas_RowCommand"
                                onrowdeleting="gvAccesoUsuCuadrillas_RowDeleting"
                                onrowcancelingedit="gvAccesoUsuCuadrillas_RowCancelingEdit"
                                onrowediting="gvAccesoUsuCuadrillas_RowEditing"
                                onrowupdating="gvAccesoUsuCuadrillas_RowUpdating"
                                >
                            <Columns> 

                               <asp:TemplateField HeaderText="Opciones">
                                    <ItemTemplate>
                                       <asp:LinkButton ID="LinkButton5" runat="server" CommandName="Edit"  class="btn btn-outline btn-warning btn-sm"  > <i class="fa fa-edit"></i></asp:LinkButton>
                                       <asp:LinkButton ID="LinkButton2" runat="server" CommandName="Delete"  class="btn btn-outline btn-danger btn-sm"  OnClientClick="if ( ! UserDeleteConfirmation()) return false;" > <i class="fa fa-remove"></i></asp:LinkButton>
                                   
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                       <asp:LinkButton ID="lbtnUpdate" runat="server" CommandName="Update"  class="btn btn-danger btn-sm"  OnClientClick="if ( ! UserDeleteConfirmation()) return false;" >Actualizar <i class="fa fa-save"></i></asp:LinkButton>
                                        <asp:LinkButton ID="lbtnCancel" runat="server" CommandName="Cancel"  class="btn btn-warning btn-sm"  >Cancelar <i class="fa fa-remove"></i></asp:LinkButton>
                                    </EditItemTemplate>

                                </asp:TemplateField>
                              
                                <asp:TemplateField HeaderText="ID" ControlStyle-BorderStyle="None">
                                    <ItemTemplate>
                                        <asp:Label ID="lblAccUsuId" runat="server" Text='<%#DataBinder.Eval(
            Container.DataItem,"AcCuaId") %>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>           
                                        <asp:Label ID="lblEditAccUsuId" runat="server" Text='<%#DataBinder.Eval(
            Container.DataItem, "AcCuaId") %>'></asp:Label>           
                                    </EditItemTemplate>

            <ControlStyle BorderStyle="None"></ControlStyle>
                                </asp:TemplateField>

					
                                <asp:TemplateField HeaderText="Nombre Completo">
                                    <ItemTemplate>
                                        <asp:Label ID="lblTareaTipo" runat="server" Text='<%#DataBinder.Eval(
            Container.DataItem,"UsuNombreCompleto") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
					
					
                                <asp:TemplateField HeaderText="ID Cuadrilla">
                                    <ItemTemplate>
                                        <asp:Label ID="lblTareaTarifa" runat="server" Text='<%#DataBinder.Eval(
            Container.DataItem,"CuaId") %>'></asp:Label>

                                    </ItemTemplate>

                                </asp:TemplateField>	
                                
                                <asp:TemplateField HeaderText="Cuadrilla">
                                    <ItemTemplate>
                                        <asp:Label ID="lblUsuNombreAcceso" runat="server" Text='<%#DataBinder.Eval(
            Container.DataItem,"CuaNombre") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>	
                                                                                                                                                                                              	                                                                			
				
                            </Columns>  
                              <PagerSettings FirstPageText="Inicio" LastPageText="Fin" Mode="Numeric" PageButtonCount="10" />           
                              <PagerStyle   HorizontalAlign = "Right" Font-Bold="False" Font-Overline="False" Font-Size="Medium" Font-Strikeout="False" Font-Underline="True" Wrap="True" />
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
        return confirm("Esta seguro de " + descripcionAlerta + " ");
    }



</script>
    <script>

        function InIEvent() {

            $('select[id*=ddlFrmTareaEstatus]').change(function () {


                $('label[id*=lblTareaEstatus]').val($(this).val());

            });

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