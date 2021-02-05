<%@ Page Title="" Language="C#" MasterPageFile="~/Inventarios1.Master" AutoEventWireup="true" CodeBehind="frmModificacionDeUnBien.aspx.cs" Inherits="InventariosPJEH.frmModificacionDeUnBien" EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <%-- Estilos generados utomaticamente de las tablas --%>
    <style type="text/css">
        .auto-style6 {
            width: 128px;
        }

        .auto-style13 {
            width: 230px;
        }

        .auto-style25 {
            width: 168px;
        }

        .auto-style34 {
            width: 408px;
            height: 36px;
        }
        .auto-style37 {
            width: 56%;
        }
        .auto-style38 {
            width: 174px;
        }
        .auto-style39 {
            width: 39%;
            float: left;
        }
        .auto-style40 {
            height: 36px;
        }
        .auto-style41 {
            width: 118%;
            height: 21px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManagerRegistro" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="DetalleDeFactura" runat="server">
        <ContentTemplate>
            <div id="bodyContenedorRegistro">
                <div id="DivFiltrosModificacion" runat="server" visible="true">
                    <fieldset style="border-color: black; width: 95%; margin-left: 11px;">
                        <legend style="width: auto; color: darkblue; font-size: 12px;">Filtros de Modificación</legend>
                        <table style="width: 50%; margin: 0 auto;">
                            <tr>
                                <td style="text-align: right;" class="auto-style6">
                                    <asp:Label ID="LblNoDoc" runat="server" Text="Número de documento: " Width="190px"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtNumDoc" runat="server" CssClass="TextBox"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: right;" class="auto-style6">
                                    <asp:Label ID="LblNoLote" runat="server" Text="Número de lote: " Width="190px"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtNumLote" runat="server" onkeypress="return Precio(event);" CssClass="TextBox"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: right;" class="auto-style6">
                                    <asp:Label ID="LblNoInventario" runat="server" Text="Número de inventario: " Width="190px"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtNumInv" runat="server" onkeypress="return Precio(event);" CssClass="TextBox"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                        <table style="width: 100%; text-align: center;">
                            <tr>
                                <td>
                                    <asp:Button ID="btnBuscarBien" CssClass="Boton" runat="server" Text="Buscar" OnClick="BtnBuscarBien_Click" />
                                </td>
                            </tr>
                        </table>
                        <br />
                        <br />
                        <div id="div1" runat="server" style="margin-bottom: 15px; width: 100%; float: left;">
                            <table style="width: 50%; margin: 0 auto;">
                                <tr>
                                    <td style="text-align: right;" class="auto-style6">
                                        <asp:Label ID="LblNumLote" runat="server" Text="Número de lote: " Width="190px" Visible="false"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="LblNumLote1" runat="server" Visible="false"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: right;" class="auto-style6">
                                        <asp:Label ID="LblNumDoc" runat="server" Text="Número de documento: " Width="190px" Visible="false"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="LblNumDoc1" runat="server" Visible="false"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                            <div runat="server" style="text-align: center;" id="divGrid" visible="false">
                                <asp:GridView HorizontalAlign="Center" ID="GridModificar" runat="server" AutoGenerateColumns="False" CssClass="StyleGridV"
                                    DataKeyNames="NumInventario" OnRowCommand="GridModificar_RowCommand" AllowPaging="True" OnPageIndexChanging="GridModificar_PageIndexChanging" OnSelectedIndexChanged="GridModificar_SelectedIndexChanged">
                                    <Columns>

                                        <asp:BoundField DataField="NumInventario" HeaderText="Número de inventario" ItemStyle-Width="150" >
                                        <ItemStyle Width="150px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="DescripcionBien" HeaderText="Descripción" ItemStyle-Width="150" >
                                        <ItemStyle Width="150px" />
                                        </asp:BoundField>
                                        <asp:TemplateField HeaderText="Asignar No. Serie">
                                            <ItemTemplate>
                                                <asp:TextBox ID="TxtNoSerie" runat="server" Text='<%# Eval("Serie") %>' onkeypress="return numbersonly(event);"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Editar" ItemStyle-Width="50">
                                            <ItemTemplate>
                                                <asp:ImageButton runat="server" ID="btnEditar" Width="16" Height="16" ImageUrl="~/Imagenes/Generales/editar.png" CommandName="Editar" OnClick="btnEditar_Click" CommandArgument='<%# Container.DataItemIndex.ToString() %>' />
                                            </ItemTemplate>
                                            <ItemStyle Width="50px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Guardar" ItemStyle-Width="50">
                                            <ItemTemplate>
                                                <asp:ImageButton runat="server" ID="btnGuardarSerie" Width="16" Height="16" ImageUrl="~/Imagenes/Generales/guardar.png" CommandName="Guardar" OnClick="btnGuardarSerie_Click" CommandArgument='<%# Container.DataItemIndex.ToString() %>' />
                                            </ItemTemplate>
                                            <ItemStyle Width="50px" />
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                        <div id="BotonLimpiar" runat="server" visible="false">
                            <table style="width: 100%; text-align: center;">
                                <tr>
                                    <td>
                                        <asp:Button ID="btnLimpiarBusqueda" CssClass="Boton" runat="server" Text="Limpiar" OnClick="BtnLimpiarBusqueda_Click" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </fieldset>
                </div>
                <div id="Modificacion" runat="server" visible="false">
                    <fieldset style="border-color: black; width: 95%; margin-left: 11px;">
                        <legend style="width: auto; color: darkblue; font-size: 12px;">Modificar datos del Bien:
                            <asp:Label runat="server" ID="Lblbien"></asp:Label></legend>
                        <div id="InfoGeneral" runat="server" visible="false">
                            <fieldset style="border-color: black; width: 95%; margin-left: 11px;">
                                <legend style="width: auto; color: darkblue; font-size: 12px;">Información General</legend>
                                <div id="div4" runat="server" visible="false" style="margin-bottom: 15px; width: 100%; float: left;">

                                    <div id="divConcatena" runat="server" visible="false" style="margin-bottom: 15px; text-align: center; width: 100%; float: left;">
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
                                                <td class="auto-style25" style="text-align: right;">
                                                    <asp:Label ID="LablNombreDelBien" runat="server" Text="Nombre del Bien: "></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtNombreDelBien" runat="server" AutoPostBack="true" CssClass="TextBox1" onkeypress="return numbersonly(event);" Width="490px"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: right;" class="auto-style25">
                                                    <asp:Label ID="LablPartida" runat="server" Text="Partida: "></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlPartida" CssClass="DropGeneral" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DropDownListPartida_SelectedIndexChanged" Width="490px">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: right;" class="auto-style25">
                                                    <asp:Label ID="LablSubClase" runat="server" Text="SubClase: "></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlSubClase" AutoPostBack="true" runat="server" CssClass="DropGeneral" Width="490px">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: right;" class="auto-style25">
                                                    <asp:Label ID="LablCatCONAC" runat="server" Text="Catálogo CONAC:"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlCatCONAC" CssClass="DropGeneral" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DropDownListCatCONAC_SelectedIndexChanged" Height="22px" Width="490px">
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
                                                    <asp:DropDownList ID="ddlMarca" runat="server" CssClass="DropGeneral" Width="490px">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: right;" class="auto-style25">
                                                    <asp:Label ID="LablModelo" runat="server" Text="Modelo: "></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtModelo" CssClass="TextBox1" runat="server" onkeypress="return numbersonly(event);" Width="490px"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: right;" class="auto-style25">
                                                    <asp:Label ID="LabelSerie" runat="server" Text="Serie: "></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtSerie" CssClass="TextBox1" runat="server" onkeypress="return numbersonly(event);" Width="490px"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: right;" class="auto-style25">
                                                    <asp:Label ID="LblDetalle" runat="server" Text="Detalle: "></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtDetalle" CssClass="TextBox1" runat="server" onkeypress="return numbersonly(event);" Width="490px"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: right;" class="auto-style25">
                                                    <asp:Label ID="LablAreaDeResguardoInicial" runat="server" Text="Área de Resguardo Inicial: "></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlResguardoInicial" runat="server" CssClass="DropGeneral" Width="490px">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                        </table>
                                        <div id="div8" runat="server" visible="false" style="margin-bottom: 15px; width: 100%; float: left;">
                                            <table style="width: 100%;">
                                                <tr>
                                                    <td>
                                                        &nbsp;</td>
                                                    <td>
                                                        &nbsp;</td>
                                                    <td>
                                                        &nbsp;</td>
                                                    <td>
                                                        &nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="LablFrente" runat="server" Text="Frente: " Visible="False"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtFrente" runat="server" CssClass="TextBox" onkeypress="return Precio(event);" Visible="False"></asp:TextBox>
                                                        &nbsp;
                                                        <asp:Label ID="LablmtsFrente" runat="server" Text="mts " Visible="False"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="Lablaltura" runat="server"  Visible="False" Text="Altura: "></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtAltura" runat="server"  Visible="False" CssClass="TextBox" onkeypress="return Precio(event);"></asp:TextBox>
                                                        &nbsp;
                                                        <asp:Label ID="LablmtsAltura" runat="server"  Visible="False" Text="mts "></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="LablFondo" runat="server" Text="Fondo: "  Visible="False"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtFondo" CssClass="TextBox" runat="server"  Visible="False" onkeypress="return Precio(event);"></asp:TextBox>
                                                        &nbsp;
                                                <asp:Label ID="LablmtsFondo" runat="server"  Visible="False" Text="mts "></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="LabelDiametro" runat="server"  Visible="False" Text="Diámetro: "></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtDiametro" CssClass="TextBox"  Visible="False" runat ="server" onkeypress="return Precio(event);"></asp:TextBox>
                                                        &nbsp;
                                                <asp:Label ID="LablmtsDiametro" runat="server"  Visible="False" Text="mts "></asp:Label>
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
                                    <table class="auto-style37">
                                        <tr>
                                            <td class="auto-style38" style="text-align: right;">
                                                <asp:Label ID="LblPrecioUnitario" runat="server" Text="Precio Unitario = " ></asp:Label>
                                            </td>
                                            <td class="auto-style13" >
                                                <asp:TextBox ID="txtPUnitario" runat="server" AutoPostBack="true" onkeypress="return ValidarDecimales(event, this)" CssClass="TextBox" OnTextChanged="TextBoxPrecioUnitario_TextChanged" Placeholder="$" Width="150px"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="auto-style38" style="text-align: right;">
                                                <asp:CheckBox ID="checkboxIVA" runat="server" AutoPostBack="true" OnCheckedChanged="checkboxIVA_CheckedChanged" Text="I.V.A = " />
                                            </td>
                                            <td class="auto-style13" style="text-align: left;">
                                                <asp:Label ID="labelIVA" runat="server" Visible="False" Width="170px"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: right;" class="auto-style38">
                                                <asp:Label ID="LabelSubtotal" runat="server" Text="Subtotal = "></asp:Label>
                                            </td>
                                            <td class="auto-style13" style="text-align: left;">
                                                <asp:Label ID="LabelSubTotal1" runat="server" Width="170px"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="auto-style38" style="text-align: right;">
                                                <asp:CheckBox ID="checkboxDescuento" runat="server" AutoPostBack="true" OnCheckedChanged="checkboxDescuento_CheckedChanged" Text="Descuento = " />
                                            </td>
                                            <td class="auto-style13" >
                                                <asp:TextBox ID="txtDescuento" runat="server" AutoPostBack="true" onkeypress="return ValidarDecimales(event, this)" CssClass="TextBox" OnTextChanged="TextBoxDescuento_TextChanged" Placeholder="$" Visible="false" Width="150px"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: right;" class="auto-style38">
                                                <asp:Label ID="LblCostoTotal" runat="server" Text="Costo Total = "></asp:Label>
                                            </td>
                                            <td class="auto-style13">
                                                <asp:Label ID="labelCostototal" runat="server" Width="170px"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: right;" class="auto-style38">
                                                <asp:CheckBox ID="cbxAjustarCosto" runat="server" AutoPostBack="true" OnCheckedChanged="cbxAjustarCosto_CheckedChanged" Text="Ajustar Costo =" />
                                            </td>
                                            <td class="auto-style13">
                                                <asp:TextBox ID="txtAjustarCosto" runat="server" CssClass="TextBox" onkeypress="return ValidarDecimales(event, this)" Placeholder="$" Visible="false" Width="150px"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                                <div id="div9" runat="server" visible="false" style="margin-bottom: 15px; " class="auto-style39">
                                    <table class="auto-style41">
                                        <tr>
                                            <td style="text-align: right;" class="auto-style34">
                                                <asp:Label ID="LablVidaUtil" runat="server" Text="Vida útil en años="></asp:Label>
                                            </td>
                                            <td class="auto-style40">
                                                <asp:TextBox ID="txtVidautil" runat="server" CssClass="TextBox" onkeypress="return Precio(event);"></asp:TextBox>
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
                    </fieldset>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content3" runat="server" ContentPlaceHolderID="CPHTitulo">
    <div style="width: auto; text-align: center; background-color: #d8d8d8;">
        <asp:Label ID="LblLema" runat="server" Text="MODIFICACIÓN DE UN BIEN" Font-Size="Large" Font-Bold="True" Font-Italic="True" ForeColor="#0C2261"></asp:Label>
    </div>
</asp:Content>
