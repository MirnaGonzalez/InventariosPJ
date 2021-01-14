<%@ Page Title="" Language="C#" MasterPageFile="~/Inventarios1.Master" AutoEventWireup="true" CodeBehind="frmCatPersonal.aspx.cs" Inherits="InventariosPJEH.frmCatPersonal" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        
        .auto-style5 {
            width: 323px;
            height: 5px;
        }

        .auto-style6 {
            height: 5px;
        }

        .auto-style7 {
            width: auto;
            height: 286px;
        }

        .auto-style8 {
            width: 450px;
            height: 10px;
        }

        .auto-style9 {
            width: 285px;
        }

        .auto-style13 {
            width: 323px;
        }
        .auto-style14 {
            width: 319px;
        }
        .auto-style15 {
            float: left;
            height: 298px;
            width: 100%;
        }
        .auto-style16 {
            height: 10px;
        }
        .auto-style17 {
            width: 100%;
            height: 7px;
        }
        .auto-style18 {
            width: 248px;
        }
        .auto-style19 {
            Font-Size: Medium;
            font-family: Verdana;
            font-size: small;
            font-weight: 400;
            border: 1px solid #663622;
        }
        .auto-style20 {
            margin-top: 10px;
        }
        .auto-style21 {
            width: 323px;
            height: 24px;
        }
        .auto-style22 {
            height: 24px;
        }
    </style>
 
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">


    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>

    <asp:Label ID="LbId" runat="server"></asp:Label>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

            <div id="bodyContenedorBda;" style="width: auto; height: auto; text-align: center">

                <asp:HiddenField id="Rowindex" runat="server"></asp:HiddenField>

                <div style="width: auto;">
                    <fieldset class="auto-style7" style="border-color: #6D252B; ">
                        <legend style="text-align: left;  color:darkblue;" >Buscar personal </legend>
                        <div class="auto-style15">

                            <table style="width: 100%; margin-top: 30px;">
                                <tr>
                                    <td class="auto-style13" style="text-align: right;">
                                        <asp:Label ID="LabelGeneral" runat="server" CssClass="LabelGenera" Text="Tipo de área: "></asp:Label>
                                    </td>
                                    <td style="text-align: left;">
                                        <asp:DropDownList ID="ddlClasificacion" runat="server" AutoPostBack="true" CssClass="DropGeneral" OnSelectedIndexChanged="ClasificacionSeleccionado" Width="420px">
                                        </asp:DropDownList>
                                    </td>

                                </tr>
                                <tr>
                                    <td class="auto-style13" style="text-align: right;">
                                        <asp:Label ID="LabelDistrito" runat="server" CssClass="LabelGeneral" Text="Distrito: " Visible="False"></asp:Label>
                                    </td>
                                    <td style="text-align: left;">
                                        <asp:DropDownList ID="ddlDistrito" runat="server" AutoPostBack="True" CssClass="DropGeneral" OnSelectedIndexChanged="DropDistrito_SelectedIndexChanged" Visible="False" Height="20px" Width="420px">
                                        </asp:DropDownList>
                                    </td>

                                </tr>
                                <tr>
                                    <td style="text-align: right;" class="auto-style13">
                                        <asp:Label ID="LabelUniAdmin" runat="server" CssClass="LabelGeneral" Text="Unidad administrativa: "></asp:Label>

                                    </td>
                                    <td style="text-align: left;">

                                        <asp:DropDownList ID="ddlUniAdmin" runat="server" AutoPostBack="True" CssClass="auto-style20" Height="20px" Width="420px"></asp:DropDownList>

                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: right;" class="auto-style21">
                                        <asp:Label ID="LbClaveEmpleado" runat="server" Text="Clave del empleado: " CssClass="LabelGeneral"></asp:Label>

                                    </td>
                                    <td style="text-align: left;" class="auto-style22">
                                        <asp:TextBox ID="txtClaveEmpleado" runat="server" CssClass="TxtGeneral" onkeypress="return soloNumeros(event)" MaxLength="4" Width="410px"></asp:TextBox>

                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: right;" class="auto-style13">
                                        <asp:Label ID="LabelNombre" runat="server" Text="Nombre: " CssClass="LabelGeneral"></asp:Label>

                                    </td>
                                    <td style="text-align: left;">
                                        <asp:TextBox ID="txtNombre" runat="server" CssClass="TxtGeneral" onkeypress="return  ValidacionLetras(event);" Width="410px"></asp:TextBox>

                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: right;" class="auto-style13">
                                        <asp:Label ID="LabelAPaterno" runat="server" Text="Apellido paterno: " CssClass="LabelGeneral"></asp:Label>

                                    </td>
                                    <td style="text-align: left;">
                                        <asp:TextBox ID="txtAPaterno" runat="server" CssClass="TxtGeneral" onkeypress="return ValidacionLetras(event);" Width="410px"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td style="text-align: right;" class="auto-style5">
                                        <asp:Label ID="LabelAMaterno" runat="server" Text="Apellido materno:  "></asp:Label>

                                    </td>
                                    <td style="text-align: left;" class="auto-style6">
                                        <asp:TextBox ID="txtAMaterno" runat="server" CssClass="TxtGeneral" onkeypress="return ValidacionLetras(event);" Width="410px"></asp:TextBox>

                                    </td>
                                </tr>


                            </table>

                            <table class="auto-style17">
                                <tr>
                                    <td class="auto-style8">
                                        <asp:Button ID="btnEntrar" runat="server" Text="Buscar" CssClass="Boton" OnClick="BtnBuscar_Click" Style="margin-top: 15px; align-content: center;" Height="33px" Width="98px" />
                                        <br />
                                    </td>
                                    <td class="auto-style16">
                                        <asp:Button ID="btnMostrarNuevoR" runat="server" Text="Nuevo Registro" CssClass="Boton" OnClick="BtnMostraOcultar" Style="margin-top: 15px; align-content: space-around; " Height="33px" Width="153px" />
                                        <br />
                                    </td>

                                </tr>

                            </table>
                        </div>


                    </fieldset>
                </div>


                <div runat="server" id="DivTabla" style="width: 100%;" visible="false">

                    <fieldset style="height: auto">

                        <asp:GridView ID="gridBuscar" CssClass="StyleGridV" runat="server" Height="142px" Width="100%"
                            AutoGenerateColumns="False"
                            DataKeyNames="IdEmpleado,ClaveEmpleado,Nombre,APaterno,AMaterno,Titular,EstatusPer,EstatusInv,Nomina,Descripcion,UniAdmin,Cargo,Distrito"
                            OnRowDeleting="GridBuscar_RowDeleting"
                            OnRowCommand="GridBuscar_RowCommand" PageSize="25" OnPageIndexChanging="GridBuscar_PageIndexChanging">
                            <Columns>
                                <asp:BoundField DataField="IdEmpleado" HeaderText="Id" Visible="false" />
                                <asp:BoundField DataField="ClaveEmpleado" HeaderText="Clave" />
                                <asp:BoundField DataField="Nombre" HeaderText="Nombre" />
                                <asp:BoundField DataField="APaterno" HeaderText="A. Paterno" />
                                <asp:BoundField DataField="AMaterno" HeaderText="A. Materno" />

                                <asp:TemplateField HeaderText="Titular" SortExpression="Titular">
                                    <ItemTemplate>
                                        <asp:Label ID="Label5" runat="server" Text='<%# ((string)Eval("Titular") =="S") ? "Si" : "No" %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Activo / Inactivo" SortExpression="EstatusPer">
                                    <ItemTemplate>
                                        <asp:Label ID="Label6" runat="server" Text='<%# ((string)Eval("EstatusPer") == "I") ? "Inactivo" : "Activo" %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Estatus Inventario" SortExpression="EstatusInv">
                                    <ItemTemplate>
                                        <asp:Label ID="LbEstatusInv" runat="server" Text='<%# ((string)Eval("EstatusInv") == "SP") ? "Sin Pendiente" : "Pendiente" %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Nómina" SortExpression="Nomina">
                                    <ItemTemplate>
                                        <asp:Label ID="LbNomina" runat="server" Text='<%# ((string)Eval("Nomina") == "S") ? "SINDICALIZADO" : ((string)Eval("Nomina") == "H") ? "HONORARIOS" : "CONFIANZA" %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:BoundField DataField="Descripcion" HeaderText="Descripcion" Visible="false" />
                                <asp:BoundField DataField="UniAdmin" HeaderText="Adscripción"/>
                                <asp:BoundField DataField="Cargo" HeaderText="Cargo" Visible="false" />
                                <asp:BoundField DataField="Distrito" HeaderText="Distrito" Visible="false" />

                                <asp:ButtonField ButtonType="Image" ImageUrl="~/Imagenes/Generales/editar.png" ControlStyle-Width="30px" HeaderText="Editar" Text="Editar" runat="server"
                                    CommandName="Prueba" ItemStyle-HorizontalAlign="Center" ItemStyle-Height="20px" ItemStyle-Width="30px" >
                                <ControlStyle Width="30px" />
                                <ItemStyle Height="20px" HorizontalAlign="Center" Width="30px" />
                                </asp:ButtonField>
                                <asp:CommandField ButtonType="Image" HeaderText="Eliminar" DeleteImageUrl="~/Imagenes/Generales/eliminar.png" ShowDeleteButton="true" runat="server" >
                                  <ControlStyle Width="30px" />
                                </asp:CommandField>
                            </Columns>
                        </asp:GridView>
                    </fieldset>
                </div>


                <div runat="server" id="DivMostrarNuevo" style="width: 100%; height: auto;" visible="false">
                   
                              <asp:HiddenField ID="HiddenTitular" runat="server" />
                              <asp:HiddenField ID="HiddenClaveEmpleado" runat="server" />
                              <asp:HiddenField ID="HiddenStatusPersona" runat="server" />
                              <asp:HiddenField ID="HiddenStatusInventario" runat="server" />
                              <asp:HiddenField ID="HiddenDescripcion" runat="server" />
                        

                    <fieldset style="height: auto; border-color: #6D252B;"  runat="server" >
                      
                        <legend style="text-align: left; color:darkblue;" runat="server" id="lgNuevoRegistro" visible="false">Datos del personal</legend>
                           
                        <table style="width: 100%;">
                            <tr>
                                <td class="auto-style14" style="text-align: right;">
                                    <asp:Label ID="SubFondo1" runat="server" Text="Tipo de área: " CssClass="LabelGeneral"></asp:Label>
                                </td>
                                <td style="text-align: left;">
                                    <asp:DropDownList ID="DropClasificacionNuevo" runat="server" CssClass="DropGeneral" AutoPostBack="True" AppendDataBoundItems="True" OnSelectedIndexChanged="ClasificacionSeleccionadoNuevo"></asp:DropDownList>
                                </td>

                            </tr>
                            <tr>
                                <td class="auto-style14" style="text-align: right;">
                                    <asp:Label ID="Distrito1" runat="server" Text="Distrito: " CssClass="LabelGeneral" Visible="False"></asp:Label>
                                </td>
                                <td style="text-align: left;">
                                    <asp:DropDownList ID="DropDistritoNuevo" OnSelectedIndexChanged="DropDistritoNuevo_SelectedIndexChanged" runat="server" AutoPostBack="True" Visible="False" CssClass="DropGeneral"></asp:DropDownList>
                                </td>

                            </tr>
                            <tr>
                                <td class="auto-style14" style="text-align: right;">
                                    <asp:Label ID="UniAdmin1" runat="server" Text="Unidad administrativa: " CssClass="LabelGeneral"></asp:Label>
                                </td>
                                <td style="text-align: left;">
                                    <asp:DropDownList ID="DropUniAdminNuevo" runat="server" CssClass="DropGeneral" AutoPostBack="True"></asp:DropDownList>
                                </td>

                            </tr>
                            <tr>
                                <td class="auto-style14" style="text-align: right;">
                                    <asp:Label ID="LbCargo" runat="server" Text="Cargo: " CssClass="LabelGeneral"></asp:Label>
                                </td>
                                <td style="text-align: left;">
                                    <asp:DropDownList ID="DropCargoNuevo" runat="server" CssClass="DropGeneral" AutoPostBack="true"></asp:DropDownList>
                                </td>

                            </tr>
                            <tr>
                                <td class="auto-style14" style="text-align: right;">
                                    <asp:Label ID="ClaveEmpleado1" runat="server" Text="Clave del empleado:"></asp:Label>
                                </td>
                                <td style="text-align: left;">
                                    <asp:TextBox ID="TxtClaveEmpleadoNuevo" runat="server" CssClass="TxtGeneral" onkeypress="return soloNumeros(event)" MaxLength="4"></asp:TextBox>
                                </td>

                            </tr>
                            <tr>
                                <td class="auto-style14" style="text-align: right;">
                                    <asp:Label ID="Nombre" runat="server" Text="Nombre: "></asp:Label>
                                </td>
                                <td style="text-align: left;">
                                    <asp:TextBox ID="TxtNombreNuevo" runat="server" type="text" CssClass="TxtGeneral" onkeypress="return ValidacionLetras(event);"></asp:TextBox>
                                </td>

                            </tr>
                            <tr>
                                <td class="auto-style14" style="text-align: right;">
                                    <asp:Label ID="APaterno1" runat="server" Text="Apellido paterno:  " CssClass="LabelGeneral"></asp:Label>
                                </td>
                                <td style="text-align: left;">
                                    <asp:TextBox ID="TxtAPaternoNuevo" runat="server" CssClass="TxtGeneral" onkeypress="return ValidacionLetras(event);"></asp:TextBox>
                                </td>

                            </tr>
                            <tr>
                                <td class="auto-style14" style="text-align: right;">
                                    <asp:Label ID="AMaterno1" runat="server" Text="Apellido materno: "></asp:Label>
                                </td>
                                <td style="text-align: left;">
                                    <asp:TextBox ID="TxtAMaternoNuevo" runat="server" CssClass="TxtGeneral" onkeypress="return ValidacionLetras(event);"></asp:TextBox>
                                </td>

                            </tr>
                        
                            <tr>
                                <td class="auto-style14" style="text-align: right;">
                                    <asp:Label ID="LBNomina" runat="server" Text="Nómina: "></asp:Label>
                                </td>
                                <td style="text-align: left;">
                                    <asp:DropDownList ID="DropNomina" runat="server" AutoPostBack="true" CssClass="DropGeneral" Style="margin-top: 10px;">
                                        <asp:ListItem Text="Seleccionar"></asp:ListItem>
                                        <asp:ListItem Value="S">SINDICALIZADO</asp:ListItem>
                                        <asp:ListItem Value="H">HONORARIOS</asp:ListItem>
                                        <asp:ListItem Value="C">CONFIANZA</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                        </table>

                        <table style="width: 100%;">
                           
                            <tr>
                                <td class="auto-style18">
                                    
                                    <asp:Label ID="LbTitular" runat="server" Text="Titular: " Style="margin-left: 30px;" CssClass="LabelGeneral"></asp:Label>
                                    <asp:RadioButton ID="rbTitularNo" runat="server" GroupName="Gruptitular" Text="No" OnCheckedChanged="RbTitularNo_CheckedChanged" />
                                    <asp:RadioButton ID="rbTitularSi" runat="server" GroupName="Gruptitular" Text="Si" OnCheckedChanged="RbTitularSi_CheckedChanged" Checked="True" />
                                </td>
                                <td class="auto-style9">
                                    
                                    <asp:Label ID="LbEstatusPer" runat="server" CssClass="LabelGeneral" Text="Estatus Personal: "></asp:Label>
                                    <asp:RadioButton ID="rbEstatusPerA" runat="server" GroupName="GrupEstatusPersona" OnCheckedChanged="RbEstatusPerA_CheckedChanged" Text="Activo" Checked="True" />
                                    <asp:RadioButton ID="rbEstatusPerII" runat="server" GroupName="GrupEstatusPersona" OnCheckedChanged="RbEstatusPerI_CheckedChanged" Text="Inactivo" />
                                </td>
                                <td style="text-align: left;">
                                     
                                    <asp:Label ID="LbEstatusInve" runat="server" CssClass="LabelGeneral" Text="Estatus inventario: "></asp:Label>
                                    <asp:RadioButton ID="rbSPendiente" runat="server" GroupName="GrupEstatusInventario" Text="Sin Pendientes" OnCheckedChanged="RadioButton1_CheckedChanged" Checked="True" />
                                    <asp:RadioButton ID="rbPendiente" runat="server" GroupName="GrupEstatusInventario" Text="Pendiente" Style="margin-left: 10px" OnCheckedChanged="RadioButton2_CheckedChanged" />
                                </td>
                            </tr>

                            <tr>
                                <td class="auto-style18">
                                    <asp:Button ID="btnGuardar" runat="server" CssClass="Boton" OnClick="BtnGuardarNuevoPersonal" Text="Guardar" Visible="false" />
                                </td>
                                <td class="auto-style9">
                                    <asp:Button ID="btnActualizar" runat="server" CssClass="Boton" OnClick="BtnActualizar_Click" Text="Actualizar" Visible="false" Width="120px" />
                                </td>
                                <td style="text-align: center;">
                                    <asp:Button ID="btnCancelar" runat="server" CssClass="Boton" OnClick="BtnCancelar_Click" Text="Cancelar" Visible="false" />
                                </td>
                            </tr>

                        </table>

                       

                        </fieldset>
                </div>
                <table style="width: 100%; margin-top: 10px;">
                            <tr>
                                <td>

                                    <input onclick="window.location.reload()" type="button" value="Limpiar" class="Boton"> </input>

                                </td>
                            </tr>
                 </table>
                <br />

            </div>

            </div>

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>


<asp:Content ID="Content3" runat="server" ContentPlaceHolderID="CPHTitulo">
    <div style="width: auto; text-align: center; background-color: #d8d8d8;">
        <asp:Label ID="LblLema" runat="server" Text="PERSONAL" Font-Size="Large" Font-Bold="True" Font-Italic="True" CssClass="LblLema"></asp:Label>
    </div>
</asp:Content>





