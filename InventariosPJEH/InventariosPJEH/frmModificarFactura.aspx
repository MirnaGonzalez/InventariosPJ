<%@ Page Title="" Language="C#" MasterPageFile="~/Inventarios1.Master" AutoEventWireup="true" CodeBehind="frmModificarFactura.aspx.cs" Inherits="InventariosPJEH.frmModificarFactura" EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        .auto-style3 {
            width: 314px;
        }

        .auto-style4 {
            width: 291px;
        }

        .auto-style6 {
            width: 128px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <asp:ScriptManager ID="ScriptManagerRegistro" runat="server"></asp:ScriptManager>

    <asp:UpdatePanel ID="DetalleDeFactura" runat="server">
        <ContentTemplate>
            <div id="bodyContenedorRegistro">
                <div id="Factura" runat="server" visible="true">
                    <fieldset style="border-color: black; width: 95%; margin-left: 11px;">
                        <legend style="width: auto; color: darkblue; font-size: 12px;">Detalle de Factura</legend>
                        <div id="dettaleDeFactura" runat="server">
                            <table style="width: 100%;">
                                <tr>
                                    <td style="text-align: right;" class="auto-style6">
                                        <asp:Label ID="LablNoDocumento" runat="server" Text="No. Documento: " Width="190px"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TextBoxNoDocumento" runat="server" AutoPostBack="true" OnTextChanged="TextBoxNoDocumento_TextChanged" CssClass="TextBox"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: right;" class="auto-style6">
                                        <asp:Label ID="LablProveedor" runat="server" Text="Proveedor: " Width="190px"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="DropDownListProveedor" runat="server" CssClass="DropGeneral">
                                        </asp:DropDownList>

                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: right;" class="auto-style6">
                                        <asp:Label ID="LablTipoDocumento" runat="server" Text="Tipo de documento: " Width="190px"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="DropDownListTipoDocumento" runat="server" CssClass="DropGeneral">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: right;" class="auto-style6">
                                        <asp:Label ID="LblTipoAdquisicion" runat="server" Text="Tipo de adquisición: " Width="190px"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="DropDownListTipoAdquisicion" runat="server" CssClass="DropGeneral">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: right;" class="auto-style6">
                                        <asp:Label ID="Label5" runat="server" Text="Detalle de adquisición: " Width="190px"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TxtDetalleAdquisicion" runat="server" onkeypress="return numbersonly(event);" CssClass="TextBox1"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: right;" class="auto-style6">
                                        <asp:Label ID="LablPropiedad" runat="server" Text="Propiedad: " Width="190px"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="DropDownListPropiedad" runat="server" CssClass="DropGeneral">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: right;" class="auto-style6">
                                        <asp:Label ID="LablFechaAdquisicion" runat="server" Text="Fecha adquisición:" Width="190px"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TxtFechaAdquisicion" runat="server" CssClass="TextBox" Enabled="false"></asp:TextBox>
                                        <asp:Image ID="ImgFechaAdquisicion" runat="server" Height="40px" ImageUrl="~/Imagenes/Generales/Calendario.png" Width="50px" />
                                        &nbsp;
                                        <cc1:CalendarExtender ID="FechaAdquisicion" runat="server" Format="dd/MM/yyyy" PopupButtonID="ImgFechaAdquisicion" TargetControlID="TxtFechaAdquisicion" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <br />
                        <div id="BotonesDetalleFactur" runat="server" style="text-align: center; margin-bottom: 15px; width: 100%; float: left;">
                            <table style="width: 80%; margin: 0 auto;">
                                <tr>
                                    <td class="auto-style3" style="text-align: center;">
                                        <asp:Button ID="BtnGuardarFactura" CssClass="Boton" runat="server" Text="Modificar Factura" OnClick="BtnGuardarFactura_Click" />
                                    </td>
                                    <td class="auto-style4" style="text-align: center;">
                                        <asp:Button ID="BtnCancelarFactura" CssClass="Boton" runat="server" OnClick="BtnCancelarFactura_Click" Text="Cancelar" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </fieldset>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content3" runat="server" ContentPlaceHolderID="CPHTitulo">
    <div style="width: auto; text-align: center; background-color: #d8d8d8;">
        <asp:Label ID="LblLema" runat="server" Text="Modificar Factura" Font-Size="Large" Font-Bold="True" Font-Italic="True" ForeColor="#0C2261"></asp:Label>
    </div>
</asp:Content>
