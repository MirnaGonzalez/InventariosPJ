<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Inventarios1.Master" CodeBehind="frmRecibirBien.aspx.cs" Inherits="InventariosPJEH.RecibirBien" EnableEventValidation="false" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="CPHTitulo">
    <div style="width: auto; text-align: center; background-color: #d8d8d8;">
        <asp:Label ID="LblLema" runat="server" Text="Recepción de Bienes a Mantenimiento" Font-Size="Large" Font-Bold="True" Font-Italic="True" ForeColor="#6D252B"></asp:Label>
    </div>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

    <asp:UpdatePanel ID="DetalleDeFactura" runat="server">
        <ContentTemplate>

            <div id="Principal1" runat="server" visible="true">
                    <fieldset style="width: auto; border-color: #0c2261;">
                    <legend style="text-align: left; color:darkblue;">Buscar registro</legend>
            <table style="margin-left: 200px" class="auto-style1">
                <tr>
                    <td style="text-align: right;" class="auto-style3">
                        <asp:Label ID="LblNumInventario" runat="server" Text="Número Inventario: " Width="167px"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="TxtNumInventario" runat="server" AutoPostBack="true" CssClass="TextBox"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style3" style="text-align: right;">
                        <asp:Label ID="LblNoSerie" runat="server" Text="Número de Serie: " Width="153px"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="TextBox2" runat="server" AutoPostBack="true" CssClass="TextBox"></asp:TextBox>
                    </td>
                </tr>
            </table>
         
            <div id="Buscar" runat="server" visible="true"> 
                <table style="margin: 0 auto; text-align: center;" class="auto-style6">
                    <tr>
                    <td class="auto-style3">
                        <asp:Button ID="BtnBuscar" runat="server" CssClass="Boton" Text="Buscar" Height="29px" Width="137px" OnClick="BtnBuscar_Click" />
                    </td>
                    </tr>
                 </table>
            </div> 
            </div>

            <div id="div1" runat="server" style="width: 100%;" visible="false">
                <asp:GridView ID="GridPrueba" runat="server" CssClass="StyleGridV" Height="100px" Width="100%" AutoGenerateColumns="false" HorizontalAlign="Center"
                        DataKeyNames="IdRegMant,IdInventario,NumInventario,DescripcionBien,Marca,Modelo,Serie,NombrePersona,IdActividad,IdENSU,IdUsuario" AllowPaging="false"
                        OnRowCommand="GridPrueba_RowCommand"  PageSize="15" OnPageIndexChanging="GridPrueba_PageIndexChanging">

                    <Columns>
                        <asp:BoundField DataField="IdRegMant" HeaderText="Id Registro" Visible="false" />
                        <asp:BoundField DataField="IdInventario" HeaderText="Id Inventario" Visible="false" />
                        <asp:BoundField DataField="NumInventario" HeaderText="No. Inventario" ItemStyle-Width="150"/>
                        <asp:BoundField DataField="DescripcionBien" HeaderText="Descripcion Bien" ItemStyle-Width="150" />
                        <asp:BoundField DataField="Marca" HeaderText="Marca" ItemStyle-Width="150" />
                        <asp:BoundField DataField="Modelo" HeaderText="Modelo" ItemStyle-Width="150" />
                        <asp:BoundField DataField="Serie" HeaderText="Serie" ItemStyle-Width="150" />
                        <asp:BoundField DataField="NombrePersona" HeaderText="Persona Asignada a Resguardo" ItemStyle-Width="155" />
                        <asp:BoundField DataField="IdActividad" HeaderText="Id Actividad" Visible="false" ItemStyle-Width="30" />                                    
                        <asp:BoundField DataField="IdENSU" HeaderText="ENSU" Visible="false" ItemStyle-Width="30" />                                    
                        <asp:BoundField DataField="IdUsuario" HeaderText="Id Usuario" Visible="false" ItemStyle-Width="30" /> 
                        <asp:TemplateField HeaderText="Check" ItemStyle-Width="150">
                            <ItemTemplate>
                                <asp:ImageButton runat="server" ID="btnEditar" Width="16" Height="16" ImageUrl="~/Imagenes/Generales/agregar.png" CommandName="Editar" CommandArgument='<%# Container.DataItemIndex.ToString() %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
            <%--Registro de Mantenimiento--%>
            <div id="bodyContenedorRecibir" runat="server" visible="false">
                <fieldset style="border-color: black; width: 95%; margin-left: 11px;">
                    <legend style="width: auto; color: darkblue; font-size: 12px;">Registro de Mantenimiento</legend>
                    <table style="margin-left: 200px" class="auto-style7">
                         <tr>  
                            <td>
                                <asp:Label ID="LblNumLote1" runat="server" Visible="false"></asp:Label>                                   
                                        </td>
                                        <td>                                        
                                         <asp:HiddenField Id="HiddenFecha" runat="server" OnLoad="time"/>
                                         </td>
                                          <tr>
                                         <td class="auto-style8">
                                         <asp:Label ID="LbIdregmant" runat="server" Visible="false" Text="idregmant" Style="font-family: Verdana; font-size: small; margin-left: 115px;" ></asp:Label>
                                         <asp:Label ID="LbId" runat="server" Visible="false" Text="id" Style="font-family: Verdana; font-size: small; margin-left: 115px; visibility:hidden " ></asp:Label>
                                         </td>
                                         </tr>
                                         <tr>
                                         <td>
                                         <asp:Label ID="LbENSU" runat="server" Visible="false" Text="ENSU" Style="font-family: Verdana; font-size: small; margin-left: 115px;"></asp:Label>
                                         <asp:Label ID="LbUsuario" runat="server" Visible="false" Text="Usuario" Style="font-family: Verdana; font-size: small; margin-left: 115px;"  ></asp:Label> 
                                         </td>
                                         </tr>
                                         <tr>
                                         <td>
                                         <asp:Label ID="LbActividad" runat="server" Visible="false" Text="Actividad" Style="font-family: Verdana; font-size: small; margin-left: 115px;"  ></asp:Label>
                                         </td>
                                </tr>
                                <tr>
                                    <td class="auto-style3" style="text-align: right;">
                                        <asp:Label ID="LblNoInventario" runat="server" Text="No. Inventario: " Width="249px"></asp:Label>
                                     </td>
                                     <td>
                                        <asp:Label ID="LbNumInventario" runat="server" style="align-content:center"></asp:Label>
                                     </td>
                                </tr>
                                <tr>
                                    <td class="auto-style3" style="text-align: right;">
                                        <asp:Label ID="LblNomRecibeBien" runat="server" Text="Nombre de quien recibe el Bien: " Width="272px"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TextBoxNomRecibeBien" runat="server" CssClass="auto-style4" Width="249px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="auto-style3" style="text-align: right;">
                                        <asp:Label ID="LblNoOficio" runat="server" Text="Número de Oficio: " Width="155px"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TextBoxNoOficio" runat="server" CssClass="auto-style4" Width="249px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="auto-style3" style="text-align: right;">
                                        <asp:Label ID="LblNomEntregaBien" runat="server" Text="Nombre de quien entrega el Bien: " Width="290px"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TextBoxNomEntregaBien" runat="server" CssClass="auto-style4" Width="249px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="auto-style3" style="text-align: right;">
                                        <asp:Label ID="LabelSO" runat="server" Text="Sistema Operativo (CPU´s): " Width="255px"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TextBoxSO" runat="server" CssClass="auto-style4" Width="249px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="auto-style3" style="text-align: right;">
                                        <asp:Label ID="LblCuenta" runat="server" Text="Cuenta Office 365: " Width="255px"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TextBoxCuenta" runat="server" CssClass="auto-style4" Width="249px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="auto-style3" style="text-align: right;">
                                        <asp:Label ID="LblIP" runat="server" Text="IP: " Width="255px"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TextBoxIP" runat="server" CssClass="auto-style4" Width="249px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="auto-style3" style="text-align: right;">
                                        <asp:Label ID="LblFallaReportada" runat="server" Text="Falla Reportada: " Width="255px"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TextBoxFallaReportada" runat="server" CssClass="auto-style4" Height="80px" Width="250px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="auto-style3" style="text-align: right;">
                                        <asp:Label ID="LblDiagReparacion" runat="server" Text="Diagnostico/Reparación: " Width="255px"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TextBoxDiag" runat="server" CssClass="auto-style4" Height="80px" Width="250px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Button ID="BtnGuardar" runat="server" CssClass="Boton" Height="29px" OnClick="BtnGuardar_Click" Text="Guardar" Width="137px" />
                                    </td>
                                    <div id="BtnCancelar1" runat="server" visible="true">
                                    <td>
                                        <asp:Button ID="BtnCancelar" runat="server" CssClass="Boton" Height="29px" OnClick="BtnCancelar_Click" Text="Cancelar" Width="137px" />
                                    </td>
                                    </div>
                                </tr>
                    </table>
                </fieldset>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

<%--Estilos Generados automaticamente--%>
<asp:Content ID="Content3" runat="server" contentplaceholderid="HeadContent">
    <style type="text/css">
        .auto-style1 {
            width: 155px;
        }
        .auto-style3 {
            width: 376px;
        }
        .auto-style4 {
            text-transform: uppercase;
        }
        .auto-style6 {
            width: 57%;
        }
        .auto-style7 {
            width: 520px;
        }
        .auto-style8 {
            width: 972px;
        }
    </style>
</asp:Content>