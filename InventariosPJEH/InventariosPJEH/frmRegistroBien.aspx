<%@ Page Title="" Language="C#" MasterPageFile="~/Inventarios1.Master" AutoEventWireup="true" CodeBehind="frmRegistroBien.aspx.cs" Inherits="InventariosPJEH.frmRegistroBien" EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">

    <%--Estilos generados automáticamente al agregar tablas--%>

    <style type="text/css">
        .auto-style1 {
            width: 209px;
        }

        .auto-style3 {
            width: 314px;
        }

        .auto-style4 {
            width: 291px;
        }

        .auto-style6 {
            width: 128px;
        }

        .auto-style7 {
            width: 209px;
            height: 20px;
        }

        .auto-style8 {
            height: 20px;
        }

        .auto-style13 {
            width: 230px;
        }

        .auto-style21 {
            width: 218px;
            text-align: center;
        }

        .auto-style25 {
            width: 168px;
        }

        .auto-style33 {
            width: 68px;
        }

        .auto-style34 {
            width: 184px;
        }
    </style>
    <%-- ----------------------------------------------------------%>
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
                                        <asp:DropDownList ID="ddlProveedor" runat="server" CssClass="Drop">
                                        </asp:DropDownList>

                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: right;" class="auto-style6">
                                        <asp:Label ID="LablTipoDocumento" runat="server" Text="Tipo de documento: " Width="190px"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlTipoDocumento" runat="server" CssClass="Drop">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: right;" class="auto-style6">
                                        <asp:Label ID="LblTipoAdquisicion" runat="server" Text="Tipo de adquisición: " Width="190px"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlTipoAdquisicion" runat="server" CssClass="Drop">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: right;" class="auto-style6">
                                        <asp:Label ID="Label5" runat="server" Text="Detalle de adquisición: " Width="190px"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtDetalleAdquisicion" runat="server" onkeypress="return numbersonly(event);" CssClass="TextBox1"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: right;" class="auto-style6">
                                        <asp:Label ID="LablPropiedad" runat="server" Text="Propiedad: " Width="190px"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlPropiedad" runat="server" CssClass="Drop">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: right;" class="auto-style6">
                                        <asp:Label ID="LablFechaAdquisicion" runat="server" Text="Fecha adquisición:" Width="190px"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtFechaAdquisicion" runat="server" CssClass="TextBox" Enabled="false"></asp:TextBox>
                                        <asp:Image ID="ImgFechaAdquisicion" runat="server" Height="40px" ImageUrl="~/Imagenes/Generales/Calendario.png" Width="50px" />
                                        &nbsp;
                                        <cc1:CalendarExtender ID="FechaAdquisicion" runat="server" Format="dd/MM/yyyy" PopupButtonID="ImgFechaAdquisicion" TargetControlID="TxtFechaAdquisicion" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <br />
                        <div id="divGridDetalleFactura" runat="server" style="margin-bottom: 15px; width: 100%; float: left;">
                            <asp:GridView ID="gridFactura" HorizontalAlign="Center" runat="server" AutoGenerateColumns="false" CssClass="StyleGridV">
                                <Columns>
                                    <asp:BoundField DataField="id" HeaderText="Id" ItemStyle-Width="30" />
                                    <asp:BoundField DataField="Detalle" HeaderText="Detalle" ItemStyle-Width="150" />
                                    <asp:BoundField DataField="Importe" HeaderText="Importe" ItemStyle-Width="150" />
                                </Columns>
                            </asp:GridView>
                        </div>
                        <div id="BotonesDetalleFactur" runat="server" style="margin-bottom: 15px; width: 100%; float: left;">
                            <table style="width: 50%; align-content: center; margin: 0 auto;">
                                <tr>
                                    <td class="auto-style7" style="text-align: right;">
                                        <asp:Label ID="LblSubT" runat="server" Text="SubTotal = " Width="90px"></asp:Label>
                                    </td>
                                    <td class="auto-style8">
                                        <asp:Label ID="LblSubTo" runat="server" Width="50%"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="auto-style1" style="text-align: right;">
                                        <asp:Label ID="LblIva" runat="server" Text="IVA = " Width="90px"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="LblIva1" runat="server" Width="50%"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="auto-style1" style="text-align: right;">
                                        <asp:Label ID="LblTotal" runat="server" Text="Total = " Width="90px"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="LblTotal1" runat="server" Width="50%"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                            <br />
                            <table style="width: 100%;">
                                <tr>
                                    <td class="auto-style3" style="text-align: center;">
                                        <asp:Button ID="btnGuardarFactura" CssClass="Boton" runat="server" Text="Guardar factura" OnClick="BtnGuardarFactura_Click" />
                                    </td>
                                    <td class="auto-style4" style="text-align: center;">
                                        <asp:Button ID="btnCancelarFactura" CssClass="Boton" runat="server" OnClick="BtnCancelarFactura_Click" Text="Limpiar" />
                                    </td>
                                    <td class="auto-style4" style="text-align: center;">
                                        <asp:Button ID="btnNuevoRegistro" CssClass="Boton" runat="server" OnClick="BtnNuevoRegistro_Click" Text="Registrar Bien" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </fieldset>
                </div>
                <br />
                <div id="Bien" runat="server" visible="false">
                    <fieldset style="border-color: black; width: 95%; margin-left: 11px;">
                        <legend style="width: auto; color: darkblue; font-size: 12px;">Registro de Bienes</legend>
                        <div id="div0" runat="server" style="width: auto; text-align: center;">
                            <br />
                            <table style="width: 100%;">
                                <tr>
                                    <td>
                                        <asp:RadioButton ID="rbtnRegistroIndividual" runat="server" AutoPostBack="true" GroupName="Registro" OnCheckedChanged="RdRegistroIndividual_CheckedChanged" Text="Registro Individual" />
                                    </td>
                                    <td>
                                        <asp:RadioButton ID="rbtnRegistroLote" runat="server" AutoPostBack="true" GroupName="Registro" OnCheckedChanged="RdRegistroLote_CheckedChanged" Text="Registro por Lote" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <br />
                        <div id="InfoGeneral" runat="server" visible="false">
                            <fieldset style="border-color: black; width: 95%; margin-left: 11px;">
                                <legend style="width: auto; color: darkblue; font-size: 12px;">Información General</legend>
                                <div id="div4" runat="server" visible="false" style="margin-bottom: 15px; width: 100%; float: left;">

                                    <div id="divConcatena" runat="server" visible="false" style="margin-bottom: 15px; width: 100%; float: left;">
                                        <table style="width: 100%;">
                                            <tr>
                                                <td class="auto-style21">
                                                    <asp:Label ID="LblConcatena" runat="server" Width="700px"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                    <div id="div5" runat="server" visible="false" style="margin-bottom: 15px; width: 100%; float: left;">
                                        <table style="width: 100%;">
                                            <tr>
                                                <td style="text-align: right;" class="auto-style25">
                                                    <asp:Label ID="LablCantidadDeBienes" runat="server" Text="Cantidad de Bienes: " Visible="false"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="TextBoxCantidadDeBienes" runat="server" AutoPostBack="true" CssClass="TextBox" onkeypress="return Precio(event);" Visible="false"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="auto-style25" style="text-align: right;">
                                                    <asp:Label ID="LablNombreDelBien" runat="server" Text="Nombre del Bien: "></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtNombreBien" runat="server" AutoPostBack="true" CssClass="TextBox1" onkeypress="return numbersonly(event);"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: right;" class="auto-style25">
                                                    <asp:Label ID="LablPartida" runat="server" Text="Partida: "></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlPartida" CssClass="Drop" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DropDownListPartida_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: right;" class="auto-style25">
                                                    <asp:Label ID="LablSubClase" runat="server" Text="SubClase: "></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlSubClase" AutoPostBack="true" runat="server" CssClass="Drop">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: right;" class="auto-style25">
                                                    <asp:Label ID="LablCatCONAC" runat="server" Text="Catálogo CONAC:"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlCatCONAC" CssClass="Drop" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DropDownListCatCONAC_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: right;" class="auto-style25">
                                                    <asp:Label ID="LablClaveCONAC" runat="server" Text="Clave CONAC: "></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="LblGrupo1" runat="server" Text="Grupo: " Visible="false"></asp:Label>
                                                    &nbsp;
                                            <asp:Label ID="LabelGrupo" runat="server"></asp:Label>
                                                    &nbsp;&nbsp;
                                            <asp:Label ID="LblSubGrupo1" runat="server" Text="SubGrupo: " Visible="false"></asp:Label>
                                                    &nbsp;
                                            <asp:Label ID="LabelSubGrupo" runat="server"></asp:Label>
                                                    &nbsp;&nbsp;
                                            <asp:Label ID="LblClase" runat="server" Text="Clase: " Visible="false"></asp:Label>
                                                    &nbsp;
                                            <asp:Label ID="LabelClase" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: right;" class="auto-style25">
                                                    <asp:Label ID="LablMarca" runat="server" Text="Marca: "></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlMarca" runat="server" CssClass="Drop">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: right;" class="auto-style25">
                                                    <asp:Label ID="LablModelo" runat="server" Text="Modelo: "></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtModelo" CssClass="TextBox1" runat="server" onkeypress="return numbersonly(event);"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: right;" class="auto-style25">
                                                    <asp:Label ID="LabelSerie" runat="server" Text="Serie: "></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtSerie" CssClass="TextBox1" runat="server" onkeypress="return numbersonly(event);"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: right;" class="auto-style25">
                                                    <asp:Label ID="LblDetalle" runat="server" Text="Detalle: "></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtDetalle" CssClass="TextBox1" runat="server" onkeypress="return numbersonly(event);"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: right;" class="auto-style25">
                                                    <asp:Label ID="LablAreaDeResguardoInicial" runat="server" Text="Área de Resguardo Inicial: "></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlResguardoInicial" runat="server" CssClass="Drop">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                        </table>
                                        <div id="div8" runat="server" visible="false" style="margin-bottom: 15px; width: 100%; float: left;">
                                            <table style="width: 100%;">
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="LablFrente" runat="server" Text="Frente: "></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="TextBoxFrente" CssClass="TextBox" runat="server" onkeypress="return Precio(event);"></asp:TextBox>
                                                        &nbsp;
                                                <asp:Label ID="LablmtsFrente" runat="server" Text="mts "></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="Lablaltura" runat="server" Text="Altura: "></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="TextBoxAltura" CssClass="TextBox" runat="server" onkeypress="return Precio(event);"></asp:TextBox>
                                                        &nbsp;
                                                <asp:Label ID="LablmtsAltura" runat="server" Text="mts "></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="LablFondo" runat="server" Text="Fondo: "></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="TextBoxFondo" CssClass="TextBox" runat="server" onkeypress="return Precio(event);"></asp:TextBox>
                                                        &nbsp;
                                                <asp:Label ID="LablmtsFondo" runat="server" Text="mts "></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="LabelDiametro" runat="server" Text="Diámetro: "></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="TextBoxDiametro" CssClass="TextBox" runat="server" onkeypress="return Precio(event);"></asp:TextBox>
                                                        &nbsp;
                                                <asp:Label ID="LablmtsDiametro" runat="server" Text="mts "></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                    </div>
                                </div>
                            </fieldset>
                        </div>
                        <div id="MontoOriginalDeLaInversion" runat="server" visible="false">
                            <fieldset style="border-color: black; width: 95%; margin-left: 11px;">
                                <legend style="width: auto; color: darkblue; font-size: 12px;">Monto Original de la Inversión</legend>
                                <div id="div2" runat="server" visible="false" style="margin-bottom: 15px; width: 100%; float: left;">
                                    <table style="width: 100%;">
                                        <tr>
                                            <td style="text-align: right;" class="auto-style33">
                                                <asp:Label ID="LablPrecioUnitario" runat="server" Text="Precio Unitario: "></asp:Label>
                                            </td>
                                            <td class="auto-style13">
                                                <asp:TextBox ID="txtPUnitario" runat="server" AutoPostBack="true" onkeypress="return ValidarDecimales(event, this)" CssClass="TextBox" OnTextChanged="TextBoxPrecioUnitario_TextChanged" Placeholder="$"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: right;" class="auto-style33">
                                                <asp:CheckBox ID="checkboxIVA" runat="server" AutoPostBack="true" OnCheckedChanged="checkboxIVA_CheckedChanged" Text="I.V.A = " />
                                            </td>
                                            <td class="auto-style13">
                                                <asp:Label ID="labelIVA" runat="server" Visible="false"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: right;" class="auto-style33">
                                                <asp:Label ID="LabelSubtotal" runat="server" Text="Subtotal = "></asp:Label>
                                            </td>
                                            <td class="auto-style13">
                                                <asp:Label ID="LabelSubTotal1" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: right;" class="auto-style33">
                                                <asp:CheckBox ID="checkboxDescuento" runat="server" AutoPostBack="true" OnCheckedChanged="checkboxDescuento_CheckedChanged" Text="Descuento = " />
                                            </td>
                                            <td class="auto-style13">
                                                <asp:TextBox ID="txtDescuento" runat="server" AutoPostBack="true" onkeypress="return ValidarDecimales(event, this)" CssClass="TextBox" OnTextChanged="TextBoxDescuento_TextChanged" Placeholder="$" Visible="false"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: right;" class="auto-style33">
                                                <asp:Label ID="LblCostoTotal" runat="server" Text="Costo Total = "></asp:Label>
                                            </td>
                                            <td class="auto-style13">
                                                <asp:Label ID="labelCostototal" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: right;" class="auto-style33">
                                                <asp:CheckBox ID="cbxAjustarCosto" runat="server" AutoPostBack="true" OnCheckedChanged="cbxAjustarCosto_CheckedChanged" Text="Ajustar Costo" />
                                            </td>
                                            <td class="auto-style13">
                                                <asp:TextBox ID="txtAjustarCosto" runat="server" CssClass="TextBox" onkeypress="return ValidarDecimales(event, this)" Placeholder="$" Visible="false"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                                <div id="div9" runat="server" visible="false" style="margin-bottom: 15px; width: 100%; float: left;">
                                    <table style="width: 100%;">
                                        <tr>
                                            <td style="text-align: right;" class="auto-style34">
                                                <asp:Label ID="LablVidaUtil" runat="server" Text="Vida Útil: "></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtVidaUtil" runat="server" CssClass="TextBox" onkeypress="return Precio(event);"></asp:TextBox>
                                                &nbsp;
                                                <asp:Label ID="LablAños" runat="server" Text="años"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </fieldset>
                        </div>
                        <div id="Botones" runat="server" visible="false" style="margin-bottom: 15px; width: 100%; float: left;">
                            <table style="width: 100%; text-align: center;">
                                <tr>
                                    <td>
                                        <asp:Button ID="btnGuardar" CssClass="Boton" runat="server" Text="Guardar" OnClick="BtnGuardar_Click" />
                                    </td>
                                    <td>
                                        <asp:Button ID="btnLimpiar" CssClass="Boton" runat="server" OnClick="btnLimpiar_Click" Text="Limpiar" />
                                    </td>
                                    <td>
                                        <asp:Button ID="btnCancelar" CssClass="Boton" runat="server" OnClick="Cancel_Click" Text="Cancelar" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                </div>
                </fieldset>
            </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content3" runat="server" ContentPlaceHolderID="CPHTitulo">
    <div style="width: auto; text-align: center; background-color: #d8d8d8;">
        <asp:Label ID="LblLema" runat="server" Text="REGISTRO DE UN BIEN" Font-Size="Large" Font-Bold="True" Font-Italic="True" ForeColor="#6D252B"></asp:Label>
    </div>
</asp:Content>
