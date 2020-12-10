<%@ Page Title="" Language="C#" MasterPageFile="~/Inventarios1.Master" AutoEventWireup="true" CodeBehind="frmBajaBien.aspx.cs" Inherits="InventariosPJEH.BajaTemporaloDefinitivaDeUnBien" EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <%-- Estilos generados automaticamente al agregar tablas --%>
    <style type="text/css">
        .auto-style6 {
            width: 128px;
        }

        .auto-style7 {
            width: 371px;
        }

        .auto-style8 {
            width: 63%;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManagerRegistro" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="DetalleDeFactura" runat="server">
        <ContentTemplate>
            <div id="bodyContenedorRegistro">
                <div id="DivFiltrosDeBusqueda" runat="server" visible="true">
                    <fieldset style="border-color: black; width: 95%; margin-left: 11px;">
                        <legend style="width: auto; color: darkblue; font-size: 12px;">Filtros de Búsqueda</legend>
                        <table style="width: 50%; margin: 0 auto; text-align: center;">
                            <tr>
                                <td style="text-align: right;" class="auto-style6">
                                    <asp:Label ID="LblNoInventario" runat="server" Text="Número de inventario: " Width="190px"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="TxtNoInventario" runat="server" onkeypress="return Precio(event);" Width="190"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                        <br />
                        <table style="width: 50%; margin: 0 auto; text-align: center;">
                            <tr>
                                <td>
                                    <asp:Button ID="BtnBuscarBien" CssClass="Boton" runat="server" Text="Agregar" OnClick="BtnBuscarBien_Click1" />
                                </td>
                            </tr>
                        </table>
                    </fieldset>
                </div>
                <br />
                <br />
                <div runat="server" style="text-align: center;" id="divGrid" visible="false">
                    <asp:GridView HorizontalAlign="Center" ID="GridBienes" DataKeyNames="NumInventario" OnRowCommand="GridBienes_RowCommand" runat="server" AutoGenerateColumns="false" CssClass="StyleGridV">
                        <Columns>
                            <asp:BoundField DataField="NumInventario" HeaderText="No. Inventario" ItemStyle-Width="150" />
                            <asp:BoundField DataField="DescripcionBien" HeaderText="Descripción Bien" ItemStyle-Width="150" />
                            <asp:BoundField DataField="Modelo" HeaderText="Modelo" ItemStyle-Width="150" />
                            <asp:BoundField DataField="Serie" HeaderText="Serie" ItemStyle-Width="150" />
                            <asp:BoundField DataField="Descripcion" HeaderText="Marca" ItemStyle-Width="150" />
                            <asp:BoundField DataField="FechaAdquisicion" HeaderText="Fecha Adquisición" ItemStyle-Width="150" />
                            <asp:TemplateField HeaderText="Baja Temporal" ItemStyle-Width="80">
                                <ItemTemplate>
                                    <asp:ImageButton runat="server" ID="btnBajaTemporal" Width="40" Height="40" ImageUrl="~/Imagenes/Generales/Temporal.png" OnClick="btnBajaTemporal_Click" CommandName="BajaTemporal" CommandArgument='<%# Container.DataItemIndex.ToString() %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Baja Definitiva" ItemStyle-Width="80">
                                <ItemTemplate>
                                    <asp:ImageButton runat="server" ID="btnBajaDefinitiva" Width="40" Height="40" ImageUrl="~/Imagenes/Generales/definitiva.png" OnClick="btnBajaDefinitiva_Click" CommandName="BajaDefinitiva" CommandArgument='<%# Container.DataItemIndex.ToString() %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                    <asp:Label runat="server" ID="Lblbien" Visible="false"></asp:Label>
                </div>
                <br />
                <div id="Botones" runat="server" visible="false">
                    <table style="width: 100%; text-align: center;">
                        <tr>
                            <td>
                                <asp:Button ID="BtnLimpiarBusqueda" CssClass="Boton" runat="server" Text="Limpiar" OnClick="BtnLimpiarBusqueda_Click" />
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content3" runat="server" ContentPlaceHolderID="CPHTitulo">
    <div style="width: auto; text-align: center; background-color: #d8d8d8;">
        <asp:Label ID="LblLema" runat="server" Text="BAJA DEFINITIVA/TEMPORAL DE BIENES" Font-Size="Large" Font-Bold="True" Font-Italic="True" ForeColor="#6D252B"></asp:Label>
    </div>
</asp:Content>
