<%@ Page Title="" Language="C#" MasterPageFile="~/Inicio.Master" AutoEventWireup="true" CodeBehind="IngresoPlanillas.aspx.cs" Inherits="Cabana.Campo.RAAgricola.Pre.Web.IngresoPlanillas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContenedorEncabezado" runat="server">

    <h2>Ingreso de Planillas</h2>
    <ol class="breadcrumb">
        <li>
            <a href="Home.aspx">Inicio</a>
        </li>
        <li>
            <a>Transacciones</a>
        </li>
        <li class="active">
            <strong>Planillas</strong>
        </li>
    </ol>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


    <form role="form" id="form" runat="server">
        <asp:HiddenField ID="HddUsuId" runat="server" />
        <asp:HiddenField ID="HddEmprId" runat="server" />
        <asp:HiddenField ID="HddUsuNivelAcceso" runat="server" />

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
                                    <li class="active"><a data-toggle="tab" href="#tab-1"><i class="fa fa-user"></i>Emp</a></li>
                                    <li class=""><a data-toggle="tab" href="#tab-2"><i class="fa fa-briefcase"></i>Prov</a></li>
                                </ul>
                                <div class="tab-content">
                                    <div id="tab-1" class="tab-pane active">



                                            <div class="table-responsive">

                                                <div class="modalEmpleados">
                                                   
                                                </div>
                                            </div>
                                        


                                    </div>
                                    <div id="tab-2" class="tab-pane">
                                        <div class="modalProveedores">
                                            <asp:UpdatePanel ID="UpdatePanel10" runat="server" UpdateMode="Conditional">
                                            <ContentTemplate>
                                                    <div class="input-group">
                                                        <asp:TextBox ID="txtControlBuscarProv" runat="server" class="input form-control txtBuscarProvClass" ></asp:TextBox>
                                                        <span class="input-group-btn">
                                                            <asp:LinkButton ID="lbtnBuscarProv" 
                                                                runat="server" 
                                                                class="btn btn btn-primary" 
                                                                CommandName="BuscarProv"
                                                                OnClientClick="if ( ! iniciarFiltrarTextoModal()) return false;"
                                                                CommandArgument="Ascending"
                                                                OnCommand="CommandBtn_Click">Prov <i class="fa fa-search"></i></asp:LinkButton>

                                                        </span>
                                                    </div>
                                            </ContentTemplate>
                                            </asp:UpdatePanel>
                                            <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                                <ContentTemplate>

                                                    <div class="full-height-scroll">
                                                        <div class="table-responsive">


                                                            <asp:GridView ID="gvProveedores" class="table table-striped table-hover" GridLines="None" BorderStyle="None" BorderColor="White" runat="server" Width="100%" AllowPaging="True" OnPageIndexChanging="gvProveedores_PageIndexChanging" AllowSorting="True"
                                                                AutoGenerateColumns="false" ShowFooter="true" ShowHeaderWhenEmpty="true"
                                                                OnRowCommand="gvEmployeeDetails_RowCommand">
                                                                <Columns>
                                                                    <asp:TemplateField HeaderText="Opc">
                                                                        <ItemTemplate>
                                                                            <asp:LinkButton ID="lbtnSelect" runat="server" Text="Ver" class="btn btn-outline btn-primary btn-sm" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" CommandName="SelectByCodigoProve"><i class="fa fa-check"></i></asp:LinkButton>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="CODIGO">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblcodProv" runat="server" Text='<%#DataBinder.Eval(
                                                                                        Container.DataItem,"gpId") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="Nombre Proveedor">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblnomProv" runat="server" Text='<%#DataBinder.Eval(
                                                                                        Container.DataItem, "gpNombre") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                                <PagerSettings FirstPageText="Inicio" LastPageText="Fin" Mode="Numeric" PageButtonCount="10" />
                                                                <PagerStyle HorizontalAlign="Right" Font-Bold="False" Font-Overline="False" Font-Size="Medium" Font-Strikeout="False" Font-Underline="True" Wrap="True" />
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
                </div>

                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>

                        <div class="col-sm-8">

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
                                            <div class="col-sm-5">
                                            <p>LLenar formulario.</p>
                                            <div class="form-group">
                                                <div class="col-lg-3">
                                                    <label class="control-label">Zafra:</label>
                                                </div>
                                                <div class="col-lg-9">
                                                    <asp:DropDownList ID="ddlFrmZafra" runat="server" class="form-control m-b"  AutoPostBack="true">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>

                                            <div class="form-group">
                                                <div class="col-lg-3">
                                                <label class="control-label">Fechas:</label>
                                                    </div>
                                                <div class="col-lg-9">
                                                    <asp:DropDownList ID="ddl_listadoFechas" runat="server" class="form-control m-b" OnSelectedIndexChanged="ddl_listadoFechas_Change" AutoPostBack="true">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>

                                            <div class="form-group">
                                                <div class="col-lg-3">
                                                <label class="control-label">Cuadrilla:</label>
                                                    </div>
                                                <div class="col-lg-9">
                                                    <asp:DropDownList ID="ddlistCuadrilla" runat="server" class="form-control m-b" OnSelectedIndexChanged="ddlistCuadrillas_Change" AutoPostBack="true">
                                                        <asp:ListItem Value="0"> Seleccionar Cuadrilla</asp:ListItem>
                                                    </asp:DropDownList>
                                                   <asp:HiddenField ID="hddIdEmpleado" runat="server" />
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <div class="col-lg-3">
                                                <label class="control-label">C.Costo.</label>
                                                    </div>
                                                <div class="col-lg-9">
                                                    <asp:DropDownList ID="ddlistCentroCosto" runat="server" class="form-control m-b"  AutoPostBack="true">
                                                        <asp:ListItem Value="0"> Seleccionar</asp:ListItem>
                                                    </asp:DropDownList>
                                                   <asp:HiddenField ID="HiddenField2" runat="server" />
                                                </div>
                                            </div>

                                            </div>
                                            <div class="col-sm-7">
                                            <div class="form-group">
                                                <div class="col-lg-2">
                                                <asp:LinkButton ID="LinkButton655" runat="server" class="control-label" OnClientClick=" modalProv();">Prov:</asp:LinkButton>
                                                <asp:LinkButton ID="LinkButton7" runat="server" class="control-label" OnClick="lbtnLimpiarProvFincaLote_Click">Limpiar</asp:LinkButton>

                                                </div>
                                                <div class="col-lg-10">
                                                    <asp:HiddenField ID="hddCodProveedor" runat="server" />
                                                    <asp:Label ID="lblCodNomProveedor" runat="server" class="input form-control" ></asp:Label>
                                                    <span class="help-block m-b-none"></span>
                                                </div>
                                            </div>

                                            <div class="form-group">
                                                <div class="col-lg-2">
                                                <asp:LinkButton ID="LinkButton3" runat="server" class="control-label" OnClientClick=" modalLotesProv();">Finca:</asp:LinkButton>
                                                </div>
                                                    <div class="col-lg-10">

                                                    <asp:Label ID="lblCodNomFinca" runat="server" class="input form-control"></asp:Label>
                                                    <span class="help-block m-b-none"></span>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <div class="col-lg-2">
                                                <asp:LinkButton ID="LinkButton4" runat="server" class="control-label" OnClientClick=" modalLotesProv();">Lote:</asp:LinkButton>
                                                </div>
                                                    <div class="col-lg-10">
                                                    <asp:HiddenField ID="hddIdLote" runat="server" />
                                                    <asp:Label ID="lblCodNomLote" runat="server" class="input form-control"></asp:Label>
                                                    <span class="help-block m-b-none"></span>
                                                </div>
                                            </div>

                                            <div class="form-group">
                                                <div class="col-lg-2">
                                                <asp:LinkButton ID="LinkButton1" runat="server" class="control-label" OnClientClick=" modalTareas();">Tarea:</asp:LinkButton>
                                                    </div>
                                                <div class="col-lg-10">
                                                    <asp:Label ID="lblTareaDesc" runat="server" class="input form-control"></asp:Label>
                                                    <span class="help-block m-b-none"></span>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <div class="col-lg-2">
                                                <label class="control-label">Cantidad</label>
                                                    </div>
                                                <div class="col-lg-10">
                                                    <asp:TextBox ID="txtTareasCantidad" runat="server" class="input form-control" MaxLength="7" OnTextChanged="txtTareasCantidad_OnTextChanged" AutoPostBack="true"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="form-group">

                                                <div class="col-lg-offset-2 col-lg-12">
                                                    <div class="i-checks">
                                                        <label>
                                                            <asp:HiddenField ID="hddTareaTarifaUnidad" runat="server" />
                                                            <asp:HiddenField ID="hddTareaId" runat="server" />Total: $      
                                                            <asp:Label ID="lblTareaTarifaTotal" runat="server"></asp:Label>
                                                        </label>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <div class="col-lg-offset-2 col-lg-12">
                                                    <asp:LinkButton ID="lbtn_AddNewMoviPlanilla" runat="server" class="btn btn btn-primary" OnClick="btn_AddNewMoviPlanilla_Click">Guardar <i class="fa fa-save"></i></asp:LinkButton>
                                                    <span class="help-block m-b-none"></span>
                                                </div>
                                            </div>
                                            </div>


                                        </div>
                                        <div class="clients-list col-lg-12">
                                            <ul class="nav nav-tabs">
                                                <span class="pull-right small text-muted"></span>
                                                <li class="active"><a data-toggle="tab" href="#tab-3"><i class="fa fa-user"></i>Empleado</a></li>
                                            </ul>
                                            <div class="full-height-scroll">
                                                <div class="table-responsive">
                                         <asp:Label ID="lblTotalDiaAll" runat="server" Text='Total Dia: $ 0.0' class="input form-control" BorderColor="#339900"></asp:Label>

                                         <asp:Label ID="lblTotalDia" runat="server" Text='Total Dia Empleado: $ 0.0' class="input form-control" BorderColor="#339900"></asp:Label>
                                        <br />
                                                    <asp:GridView class="table table-striped table-hover" ID="gvMoviPlanilla" GridLines="None" BorderStyle="None" BorderColor="White" runat="server" Width="100%" AllowPaging="True" OnPageIndexChanging="gvMoviPlanilla_PageIndexChanging" AllowSorting="True"
                                                        AutoGenerateColumns="false" ShowFooter="true" ShowHeaderWhenEmpty="true"
                                                        OnRowCommand="gvEmployeeDetails_RowCommand"
                                                        OnRowDeleting="gvMoviPlanilla_RowDeleting"
                                                        OnRowCancelingEdit="gvMoviPlanilla_RowCancelingEdit"
                                                        OnRowEditing="gvMoviPlanilla_RowEditing"
                                                        OnRowUpdating="gvMoviPlanilla_RowUpdating">
                                                        <Columns>

                                                            <asp:TemplateField HeaderText="Opc.">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="LinkButton5" runat="server" CommandName="Edit" class="btn btn-outline btn-warning btn-sm"> <i class="fa fa-edit"></i></asp:LinkButton>
                                                                    <asp:LinkButton ID="LinkButton2" runat="server" CommandName="Delete" class="btn btn-outline btn-danger btn-sm" OnClientClick="if ( ! UserDeleteConfirmation()) return false;"> <i class="fa fa-remove"></i></asp:LinkButton>
                                                                    <asp:Label ID="lblMovId" runat="server" Text='<%#DataBinder.Eval(
            Container.DataItem,"MovId") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <asp:Button ID="imgbtnUpdate" runat="server" CommandName="Update" Text="Aceptar" class="btn btn-danger btn-sm" />
                                                                    <asp:Button ID="imgbtnCancel" runat="server" CommandName="Cancel" Text="Cancelar" class="btn btn-warning btn-sm" />
                                                                    <asp:Label ID="lblEditMovId" runat="server" Text='<%#DataBinder.Eval(
            Container.DataItem, "MovId") %>'></asp:Label>                                                                
                                                                </EditItemTemplate>

                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Tarea">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblTareaDesc" runat="server" Text='<%#DataBinder.Eval(
            Container.DataItem, "TareaDesc") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Cant.">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblMovCantidad" runat="server" Text='<%#DataBinder.Eval(
            Container.DataItem,"MovCantidad") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <asp:TextBox ID="txtEditMovCantidad" MaxLength="7" class="form-control" AutoPostBack="true" runat="server" Text='<%#DataBinder.Eval(
            Container.DataItem, "MovCantidad") %>'></asp:TextBox>
                                                                </EditItemTemplate>

                                                            </asp:TemplateField>


                                                            <asp:TemplateField HeaderText="Tarifa">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblMovTarifa" runat="server" Text='<%#DataBinder.Eval(
            Container.DataItem,"MovTarifa") %>'></asp:Label>
                                                                </ItemTemplate>

                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Total">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblMovTotal" runat="server" Text='<%#DataBinder.Eval(
            Container.DataItem,"MovTotal") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Empleado">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblTareaTipo" runat="server" Text='<%#DataBinder.Eval(
            Container.DataItem,"EmpNombres") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Prov">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblCodProv" runat="server" Text='<%#DataBinder.Eval(
            Container.DataItem,"CodProv") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Finca">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblNomFinca" runat="server" Text='<%#DataBinder.Eval(
            Container.DataItem,"NomFinca") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Lote">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblNomLote" runat="server" Text='<%#DataBinder.Eval(
            Container.DataItem,"NomLote") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="User">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblUsuId" runat="server" Text='<%#DataBinder.Eval(
            Container.DataItem,"UsuNombre") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="C.Costo">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblCcDescripcion" runat="server" Text='<%#DataBinder.Eval(
            Container.DataItem,"CcDescripcion") %>'></asp:Label>
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

                <div class="col-sm-4">
                    <div class="ibox">
                        <div class="ibox-content">

                                                    <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                                                        <ContentTemplate>
     
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                        <ContentTemplate>
                                                            <div class="input-group">
                                                                <asp:TextBox ID="txtControlBuscarEmpleado" runat="server" class="input form-control" OnTextChanged="lbtnBuscarEmpleado_Click" AutoPostBack="true"></asp:TextBox>
                                                                <span class="input-group-btn">
                                                                <asp:LinkButton ID="lbtnBuscarNombreEmp" runat="server" class="btn btn btn-primary"  OnClick="lbtnBuscarEmpleado_Click"><i class="fa fa-search"></i></asp:LinkButton>

                                                                </span>
                                                            </div>


                                                            <div class="full-height-scroll">

                                                            <asp:GridView ID="gvEmpleados" class="table table-striped table-hover" GridLines="None" BorderStyle="None" BorderColor="White" runat="server" Width="100%" AllowPaging="True" OnPageIndexChanging="gvEmpleados_PageIndexChanging" AllowSorting="True"
                                                                AutoGenerateColumns="false" ShowFooter="true" ShowHeader="true"
                                                                ShowHeaderWhenEmpty="true"
                                                                OnRowCommand="gvEmployeeDetails_RowCommand"
                                                                OnRowDataBound="OnRowDataBound"
                                                                OnSelectedIndexChanged="OnSelectedIndexChanged" PageSize="3">
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
                                                                                Text='<%#DataBinder.Eval( Container.DataItem, "nombreCompleto") %>'
                                                                                class="btn btn-outline btn-warning btn-sm"
                                                                                CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"
                                                                                CommandName="lbtnSelectRow">
                                           
                                                                            </asp:LinkButton>
                                                                            <asp:Label Visible="false" ID="lblEmpNombres" runat="server" Text='<%#DataBinder.Eval(
                                                                                 Container.DataItem, "nombreCompleto") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="Dui">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblEmpDUI" runat="server" Text='<%#DataBinder.Eval(
                                                                                 Container.DataItem, "EmpDUI") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>



                                                                </Columns>
                                                                <PagerSettings FirstPageText="Inicio" LastPageText="Fin" Mode="Numeric" PageButtonCount="10" />
                                                                <PagerStyle HorizontalAlign="Right" Font-Bold="False" Font-Overline="False" Font-Size="Medium" Font-Strikeout="False" Font-Underline="True" Wrap="True" />
                                                            </asp:GridView>
                                                                </div>

                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>


         <asp:UpdatePanel ID="UpdatePanel11" runat="server">
         <ContentTemplate>
                <asp:HiddenField ID="HiddenField1" runat="server" Value="0"/>
        </ContentTemplate>
        </asp:UpdatePanel>                           
                            <div class="clients-list">

                            <div class="tab-content">
                <div id="tab-1" class="tab-pane active">
        <asp:UpdateProgress ID="UpdateProgress3" runat="server">
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
         <asp:UpdatePanel ID="UpdatePanel12" runat="server">
         <ContentTemplate> 
                <a data-toggle="tab"></a>

                            <div class="full-height-scroll">
                            <div class="table-responsive">


                           <asp:GridView ID="gvListaEmpSeleccionados" class="table table-striped table-hover"  GridLines="None" BorderStyle="None" BorderColor="White" runat="server" Width="100%" AllowPaging="True"  OnPageIndexChanging="gvEmpleados_PageIndexChanging"  AllowSorting="True"
                               AutoGenerateColumns="false" ShowFooter="true" ShowHeaderWhenEmpty="true"  
                                onrowdeleting       ="gvBuscarEmpleados_RowDeleting"
                                onrowupdating       ="gvBuscarEmpleados_RowUpdating"                                                             
                               >
                            <Columns>
                               <asp:TemplateField HeaderText="Action">
                                    <ItemTemplate>
                                       <asp:CheckBox ID="chkSelect" runat="server" class="btn btn-outline btn-warning btn-sm" Visible="false" />

                                       <asp:LinkButton ID="gvlbtn_deleteEmpleado" runat="server" CommandName="Delete"  class="btn btn-danger btn-sm"  OnClientClick="if ( ! UserConfirmation('Quitar este empleado de la lista')) return false;" ><i class="fa fa-remove"></i></asp:LinkButton>
                                        <span class="help-block m-b-none"></span>
                                       <asp:LinkButton ID="gvlbtn_updateEmpleado" runat="server" CommandName="Update"  class="btn btn-outline btn-warning btn-sm"  OnClientClick="if ( ! UserConfirmation('Si esta funcion esta activa solo se le ingresara tarea a este empleado')) return false;" ><i class="fa fa-check"></i></asp:LinkButton>                                   
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
            Container.DataItem, "nombreCompleto") %>'></asp:Label>
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




            </div>
        </div>

        <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1"></asp:UpdateProgress>

                        <div style="display: none">
                        <div class="modalFincasLotes">
                        <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                            <h3 class="modal-title">
                                <label id="tituloModal"></label>
                            </h3>
                                <div class="input-group">
                                    <asp:TextBox ID="txtControlFincasLotes" name="txtControlFincasLotes" runat="server" Text="" class="input form-control txtBuscarFincasLotesClass"></asp:TextBox>
                                    <span class="input-group-btn">
                                        <asp:LinkButton ID="lbtnBuscarFincasLotes" runat="server" class="btn btn btn-warning"
                                            CommandName="BuscarFincasLotes"
                                            OnClientClick="if ( ! iniciarFiltrarTextoModal()) return false;"
                                            CommandArgument="Ascending"
                                            OnCommand="CommandBtn_Click">Fincas Lotes <i class="fa fa-search"></i></asp:LinkButton>

                                    </span>
                                </div>
                        </ContentTemplate>
                        </asp:UpdatePanel>
                        <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                        <ContentTemplate>
                                <asp:GridView ID="gvFincasLotes" class="table table-striped table-hover" GridLines="None" BorderStyle="None" BorderColor="White" runat="server" Width="100%" AllowPaging="True" OnPageIndexChanging="gvFincasLotes_PageIndexChanging" AllowSorting="True"
                                    AutoGenerateColumns="false" ShowFooter="true" ShowHeaderWhenEmpty="true"
                                    OnRowCommand="gvEmployeeDetails_RowCommand">
                                    <Columns>

                                        <asp:TemplateField HeaderText="Opc">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lbtnSelect" runat="server" Text="Ver" class="btn btn-outline btn-primary btn-sm" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" CommandName="SelectByIdLote"><i class="fa fa-check"></i></asp:LinkButton>
                                                <asp:HiddenField ID="hddgv_idLote" runat="server" Value='<%#DataBinder.Eval(
            Container.DataItem,"LoteId") %>' />

                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="ID">
                                            <ItemTemplate>
                                                <asp:Label ID="lblFincaId" runat="server" Text='<%#DataBinder.Eval(
            Container.DataItem,"FincaId") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Nombre Finca">
                                            <ItemTemplate>
                                                                            <asp:LinkButton ID="lbtnSelectNomFinca" runat="server"
                                                                                Text='<%#DataBinder.Eval( Container.DataItem, "FincaNombre") %>'
                                                                                class="btn btn-outline btn-primary btn-sm"
                                                                                CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"
                                                                                CommandName="SelectByIdAllFinca">
                                           
                                                                            </asp:LinkButton>
                                                <asp:Label Visible="false" ID="lblnomFinca" runat="server" Text='<%#DataBinder.Eval(
            Container.DataItem, "FincaNombre") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="ID">
                                            <ItemTemplate>
                                                <asp:Label ID="lblcodLote" runat="server" Text='<%#DataBinder.Eval(
            Container.DataItem,"LoteId") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Nombre">
                                            <ItemTemplate>
                                                <asp:Label ID="lblnomLote" runat="server" Text='<%#DataBinder.Eval(
            Container.DataItem, "LoteNombre") %>'></asp:Label>
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


        <div id="myModalTareas" class="modal inmodal fade" tabindex="-1" role="dialog" aria-hidden="true">
            <div class="modal-dialog modal-lg">
                <div class="modal-content">

                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">&times;</span><span class="sr-only">Close</span></button>
                        <h3 class="modal-title">Tareas
                        </h3>
                    </div>
                            <div class="modal-body">
                            <asp:UpdatePanel ID="UpdatePanel9" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <div class="input-group">
                                    <asp:TextBox ID="txtBuscarTareas" name="txtBuscarTareas" runat="server" Text="" class="input form-control txtBuscarTareas"></asp:TextBox>
                                    <span class="input-group-btn">
                                        <asp:LinkButton ID="LinkButton6" runat="server" class="btn btn btn-warning"
                                            CommandName="BuscarTareas"
                                            OnClientClick="if ( ! iniciarFiltrarTextoModal()) return false;"
                                            CommandArgument="Ascending"
                                            OnCommand="CommandBtn_Click">Tareas<i class="fa fa-search"></i></asp:LinkButton>

                                    </span>
                                </div>
                                </ContentTemplate>
                                </asp:UpdatePanel>
                                <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                                <ContentTemplate>
                                <asp:GridView ID="gvTareas" class="table table-striped table-hover" GridLines="None" BorderStyle="None" BorderColor="White" runat="server" Width="100%" AllowPaging="True" OnPageIndexChanging="gvTareas_PageIndexChanging" AllowSorting="True"
                                    AutoGenerateColumns="false" ShowFooter="true" ShowHeader="true" ShowHeaderWhenEmpty="true"
                                    OnRowCommand="gvEmployeeDetails_RowCommand">

                                    <Columns>

                                        <asp:TemplateField HeaderText="Opc">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lbtnSelect" runat="server" Text="Ver" class="btn btn-outline btn-primary btn-sm" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" CommandName="SelectByIdTareas"><i class="fa fa-check"></i></asp:LinkButton>
                                                <asp:HiddenField ID="HFgv_TareaId" Value='<%#DataBinder.Eval(
            Container.DataItem,"TareaId") %>'
                                                    runat="server" />
                                            </ItemTemplate>

                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Tarea">
                                            <ItemTemplate>
                                                <asp:Label ID="lblTareaId" runat="server" Text='<%#DataBinder.Eval(
            Container.DataItem,"TareaId") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Nombre Tarea">
                                            <ItemTemplate>
                                                <asp:Label ID="lblTareaDesc" runat="server" Text='<%#DataBinder.Eval(
            Container.DataItem, "TareaDesc") %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderTemplate>
                                                <asp:Label ID="Label1" runat="server" Text="Descripcion" Font-Bold="true"></asp:Label><br></br>
                                            </HeaderTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Tipo">
                                            <ItemTemplate>
                                                <asp:Label ID="lblTareaTipo" runat="server" Text='<%#DataBinder.Eval(
            Container.DataItem,"TareaTipo") %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderTemplate>
                                                <asp:Label ID="Label2" runat="server" Text="Tipo" Font-Bold="true"></asp:Label><br></br>
                                            </HeaderTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Tarifa">
                                            <ItemTemplate>
                                                <asp:Label ID="lblTareaTarifa" runat="server" Text='<%#DataBinder.Eval(
            Container.DataItem, "TareaTarifa") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Centro Costo">
                                            <ItemTemplate>
                                                <asp:Label ID="lblCcNombre" runat="server" Text='<%#DataBinder.Eval(
            Container.DataItem, "CcNombre") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                    </Columns>
                                    <PagerSettings FirstPageText="Inicio" LastPageText="Fin" Mode="Numeric" PageButtonCount="10" />
                                    <PagerStyle HorizontalAlign="Right" Font-Bold="False" Font-Overline="False" Font-Size="Medium" Font-Strikeout="False" Font-Underline="True" Wrap="True" />
                                </asp:GridView>
                        </ContentTemplate>
                    </asp:UpdatePanel>

                            </div>

                            <div class="modal-footer">
                                <button type="button" class="btn btn-white" data-dismiss="modal">Close</button>
                            </div>

                </div>

            </div>
        </div>
        <!--<asp:LinkButton ID="lbtnBuscarFiltroTareas" class="btn btn btn-warning"  runat="server" CommandName="FiltroTareas" Text="Buscar" Width="100px" OnClientClick="if ( ! iniciarFiltrarTextoModal()) return false;" ></asp:LinkButton>-->



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


        <asp:HiddenField ID="hddTextoFiltroTareas" runat="server" />
        <asp:HiddenField ID="hddTextoFiltroEmp" runat="server" />
        <asp:HiddenField ID="hddTextoFiltroLotes" runat="server" />
        <asp:HiddenField ID="HddTextoFiltroProv" runat="server" />



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
            return confirm(descripcionAlerta);
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

        function explode() {
            //alert("Boom!");
            location.reload();
        }
        //setTimeout(explode, 30000);


        //FIN VALIDACION DE FORMULARIO
        function modalLotesProv() {
  
            moverContentTemporalModal();
            jQuery('.modalFincasLotes').appendTo('.myModal');
            $('#myModal').modal();
        }

        function modalEmp() {
            moverContentTemporalModal();
            jQuery('.modalEmpleados').appendTo('.myModal');
            $('#myModal').modal();
        }

        function modalProv() {
            moverContentTemporalModal();
            jQuery('.modalProveedores').appendTo('.myModal');
            $('#myModal').modal();
        }

        function moverContentTemporalModal()
        {
            jQuery('.modalProveedores').appendTo('.contentTemporalModal');
            jQuery('.modalEmpleados').appendTo('.contentTemporalModal');
            jQuery('.modalFincasLotes').appendTo('.contentTemporalModal');
        }

        function cerrarMyModal() {
            $('#myModal').modal('hide');
        }

        $("#lblNombreEmpleado").click(function () {
            $('#myModalEmp').modal();
        });
        /*
        function cerrarModalLotesProv() {
            $('#myModalLotesProv').modal('hide');
        }
        */
        function tituloModal(data) {
            $('#tituloModal').text(data);
        }

        function modalTareas() {
            $('#myModalTareas').modal();
        }

        function cerrarModalTareas() {
            $('#myModalTareas').modal('hide');
        }



        function iniciarFiltrarTextoModal() {

            var textoTareasFiltro = $(".txtBuscarTareas").val();
            $("#<%= hddTextoFiltroTareas.ClientID %>").val(textoTareasFiltro);

            var textoEmpleados = $(".txtBuscarEmpClass").val();
            $("#<%= hddTextoFiltroEmp.ClientID %>").val(textoEmpleados);

            var textoFincasLotes = $(".txtBuscarFincasLotesClass").val();
            $("#<%= hddTextoFiltroLotes.ClientID %>").val(textoFincasLotes);

            var textoProv = $(".txtBuscarProvClass").val();
            $("#<%= HddTextoFiltroProv.ClientID %>").val(textoProv);


            return true;

        }


    </script>

    <script>

        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(InIEvent);
    </script>
</asp:Content>
