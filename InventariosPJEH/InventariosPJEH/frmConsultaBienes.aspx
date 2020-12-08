<%@ Page Title="" Language="C#" MasterPageFile="~/Inventarios1.Master" AutoEventWireup="true" CodeBehind="frmConsultaBienes.aspx.cs" Inherits="InventariosPJEH.frmConsultaBienes" EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        /*Estilos generados automaticamente al agregar tablas*/
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
        <Triggers>
            <asp:PostBackTrigger ControlID="BtnExportar" />
        </Triggers>
        <ContentTemplate>
            <div id="bodyContenedorRegistro">
                <div id="DivFiltrosDeBusqueda" runat="server" visible="true">
                    <fieldset style="border-color: black; width: 95%; margin-left: 11px;">
                        <legend style="width: auto; color: darkblue; font-size: 12px;">Filtros de Búsqueda</legend>
                        <table style="margin-left: 100px" class="auto-style8">
                            <tr>
                                <td style="text-align: right;" class="auto-style6">
                                    <asp:Label ID="LablPropiedad" runat="server" Text="Propiedad:" Width="190px"></asp:Label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="DropDownListPropiedad" runat="server" CssClass="Drop"></asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: right;" class="auto-style6">
                                    <asp:Label ID="LablPartida" runat="server" Text="Partida: " Width="190px"></asp:Label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="DropDownListPartida" CssClass="Drop" runat="server"></asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: right;" class="auto-style6">
                                    <asp:Label ID="LablSubClase" runat="server" Text="SubClase: " Width="190px"></asp:Label>
                                </td>
                                <td class="auto-style36">
                                    <asp:DropDownList ID="DropDownListSubClase" AutoPostBack="true" runat="server" CssClass="Drop"></asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: right;" class="auto-style6">
                                    <asp:Label ID="LablNoDocumento" runat="server" Text="No. Documento: " Width="190px"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="TextBoxNoDocumento" runat="server" AutoPostBack="true" CssClass="TextBox"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: right;" class="auto-style6">
                                    <asp:Label ID="LablProveedor" runat="server" Text="Proveedor: " Width="190px"></asp:Label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="DropDownListProveedor" runat="server" CssClass="Drop"></asp:DropDownList>
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
                            <tr>
                                <td style="text-align: right;" class="auto-style6">
                                    <asp:Label ID="LblNoInventario" runat="server" Text="No. Inventario: " Width="190px"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="TxtNoInventario" runat="server" AutoPostBack="true" onkeypress="return Precio(event);" CssClass="TextBox"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: right;" class="auto-style6">
                                    <asp:Label ID="LablMarca" runat="server" Text="Marca: " Width="190px"></asp:Label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="DropDownListMarca" runat="server" CssClass="Drop">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: right;" class="auto-style6">
                                    <asp:Label ID="LablModelo" runat="server" Text="Modelo: " Width="190px"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="TextBoxModelo" CssClass="TextBox1" runat="server" onkeypress="return numbersonly(event);"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: right;" class="auto-style6">
                                    <asp:Label ID="LabelSerie" runat="server" Text="Serie: " Width="190px"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="TextBoxSerie" CssClass="TextBox1" runat="server" onkeypress="return numbersonly(event);"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: right;" class="auto-style6">
                                    <asp:Label ID="LabelGeneral" runat="server" Text="Tipo de área:" Width="190px"></asp:Label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="DropClasificacion" runat="server" CssClass="Drop" AutoPostBack="true" OnSelectedIndexChanged="ClasificacionSeleccionado"></asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: right;" class="auto-style6">
                                    <asp:Label ID="LabelDistrito" runat="server" Text="Distrito: " Width="190px" Visible="false"></asp:Label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="DropDistrito" runat="server" AutoPostBack="True" CssClass="Drop" Visible="false" OnSelectedIndexChanged="DropDistrito_SelectedIndexChanged"></asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: right;" class="auto-style6">
                                    <asp:Label ID="LabelUniAdmin" runat="server" Text="Área de resguardo:" Width="190px"></asp:Label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="DropUniAdmin" runat="server" CssClass="Drop" AutoPostBack="True" OnSelectedIndexChanged="DropUniAdmin_SelectedIndexChanged"></asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: right;" class="auto-style6">
                                    <asp:Label ID="LblPersona" runat="server" Text="Persona:" Visible="false" Width="190px"></asp:Label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="DropPersona" runat="server" Visible="false" CssClass="Drop" AutoPostBack="True"></asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: right;" class="auto-style6">
                                    <asp:Label ID="LblNoResguardo" runat="server" Text="Número de resguardo:" Width="190px"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="TxtNoResguardo" CssClass="TextBox" runat="server" onkeypress="return Precio(event);"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: right;" class="auto-style6">
                                    <asp:Label ID="LblPorAreaDeMantenimiento" runat="server" Text="Por área de mantenimiento:" Width="190px"></asp:Label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="DropAreaMantenimiento" runat="server" CssClass="Drop" AutoPostBack="True"></asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: right;" class="auto-style6">
                                    <asp:Label ID="LblEstatus" runat="server" Text="Estatus de Bienes:" Width="190px"></asp:Label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="DropEstatus" runat="server" CssClass="Drop" AutoPostBack="True"></asp:DropDownList>
                                </td>
                            </tr>
                        </table>
                        <br />
                        <table style="width: 50%; margin: 0 auto; text-align: center;">
                            <tr>
                                <td>
                                    <asp:Button ID="BtnBuscarBien" CssClass="Boton" runat="server" Text="Buscar" OnClick="BtnBuscarBien_Click" />
                                </td>
                                <td>
                                    <asp:Button ID="BtnLimpiar" CssClass="Boton" runat="server" Text="Limpiar" OnClick="BtnLimpiar_Click" />
                                </td>
                            </tr>
                        </table>
                    </fieldset>
                </div>
                <div id="DivResultados" runat="server" visible="true">
                    <br />
                    <br />
                    <div runat="server" style="text-align: center; overflow: scroll;" id="divGrid" visible="false">

                        <asp:GridView HorizontalAlign="Center" ID="GridModificar" runat="server" AutoGenerateColumns="false" CssClass="StyleGridV"
                            DataKeyNames="NumInventario" AllowPaging="true" PageSize="25" OnPageIndexChanging="GridModificar_PageIndexChanging">
                            <Columns>
                                <asp:BoundField DataField="NumInventario" HeaderText="No. Inventario" ItemStyle-Width="150" />
                                <asp:BoundField DataField="DescripcionBien" HeaderText="Descripción Bien" ItemStyle-Width="150" />
                                <asp:BoundField DataField="Descripcion" HeaderText="Marca" ItemStyle-Width="150" />
                                <asp:BoundField DataField="Modelo" HeaderText="Modelo" ItemStyle-Width="150" />
                                <asp:BoundField DataField="Serie" HeaderText="Serie" ItemStyle-Width="150" />
                                <asp:BoundField DataField="NumDocumento" HeaderText="No. Documento" ItemStyle-Width="150" />
                                <asp:BoundField DataField="FechaAdquisicion" HeaderText="Fecha de Adquisición" ItemStyle-Width="150" />
                                <asp:BoundField DataField="CostoTotal" HeaderText="Costo" ItemStyle-Width="150" />
                                <asp:BoundField DataField="Proveedor" HeaderText="Proveedor" ItemStyle-Width="150" />
                                <asp:BoundField DataField="IdPartida" HeaderText="IdPartida" ItemStyle-Width="150" />
                                <asp:BoundField DataField="Partida" HeaderText="Descripción de Partida" ItemStyle-Width="150" />
                                <asp:BoundField DataField="IdResguardo" HeaderText="No. Resguardo" ItemStyle-Width="150" />
                                <asp:BoundField DataField="NombrePersona" HeaderText="Nombre Persona" ItemStyle-Width="150" />
                                <asp:BoundField DataField="Nombre" HeaderText="Área de Ubicación del Bien" ItemStyle-Width="150" />
                                <asp:BoundField DataField="Propiedad" HeaderText="Propiedad" ItemStyle-Width="150" />
                            </Columns>
                        </asp:GridView>
                    </div>
                    <div runat="server" style="text-align: center; overflow: scroll;" id="div1" visible="false">

                        <asp:GridView HorizontalAlign="Center" ID="GridPrueba" runat="server" AutoGenerateColumns="false" CssClass="StyleGridV"
                            DataKeyNames="NumInventario">
                            <Columns>
                                <asp:BoundField DataField="NumInventario" HeaderText="No. Inventario" ItemStyle-Width="150" />
                                <asp:BoundField DataField="DescripcionBien" HeaderText="Descripción Bien" ItemStyle-Width="150" />
                                <asp:BoundField DataField="Descripcion" HeaderText="Marca" ItemStyle-Width="150" />
                                <asp:BoundField DataField="Modelo" HeaderText="Modelo" ItemStyle-Width="150" />
                                <asp:BoundField DataField="Serie" HeaderText="Serie" ItemStyle-Width="150" />
                                <asp:BoundField DataField="NumDocumento" HeaderText="No. Documento" ItemStyle-Width="150" />
                                <asp:BoundField DataField="FechaAdquisicion" HeaderText="Fecha de Adquisición" ItemStyle-Width="150" />
                                <asp:BoundField DataField="CostoTotal" HeaderText="Costo" ItemStyle-Width="150" />
                                <asp:BoundField DataField="Proveedor" HeaderText="Proveedor" ItemStyle-Width="150" />
                                <asp:BoundField DataField="IdPartida" HeaderText="IdPartida" ItemStyle-Width="150" />
                                <asp:BoundField DataField="Partida" HeaderText="Descripción de Partida" ItemStyle-Width="150" />
                                <asp:BoundField DataField="IdResguardo" HeaderText="No. Resguardo" ItemStyle-Width="150" />
                                <asp:BoundField DataField="NombrePersona" HeaderText="Nombre Persona" ItemStyle-Width="150" />
                                <asp:BoundField DataField="Nombre" HeaderText="Área de Ubicación del Bien" ItemStyle-Width="150" />
                                <asp:BoundField DataField="Propiedad" HeaderText="Propiedad" ItemStyle-Width="150" />
                            </Columns>
                        </asp:GridView>
                    </div>
                    <div id="Botones" runat="server" visible="false">
                        <table style="width: 50%; margin: 0 auto; text-align: center;">
                            <tr>
                                <td class="auto-style7">
                                    <asp:Button ID="BtnExportar" CssClass="Boton" runat="server" Text="Exportar a Excel" OnClick="BtnExportar_Click" />
                                </td>
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
        <asp:Label ID="LblLema" runat="server" Text="CONSULTA DE BIENES" Font-Size="Large" Font-Bold="True" Font-Italic="True" ForeColor="#6D252B"></asp:Label>
    </div>
</asp:Content>
