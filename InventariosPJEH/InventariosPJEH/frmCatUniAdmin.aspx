<%@ Page Title="" Language="C#" MasterPageFile="~/Inventarios1.Master" AutoEventWireup="true" CodeBehind="frmCatUniAdmin.aspx.cs" Inherits="InventariosPJEH.frmCatUniAdmin" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        .auto-style5 {
            width: 82%;
            height: 43px;
        }
        .auto-style7 {
            width: 95%;
            height: 136px;
        }
        .auto-style8 {
            margin-top: 10px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager" runat="server"></asp:ScriptManager>
    <asp:Label ID="Lbld" runat="server"></asp:Label>

    <asp:UpdatePanel ID="UpdatePanel" runat="server">
        <ContentTemplate>
            <div id="Contenedor" style="width:auto; height:auto; text-align:center">
               
                <asp:HiddenField ID="Rowindex" runat="server" />

                <div id="DivUniAdminPres" runat="server" visible="true">
                    <div style="width: auto;">
                   
                        <fieldset class="auto-style7" style="border-color: black; margin-left: 11px;">
                            <legend style="width: auto; color: darkblue; font-size: 12px;">Buscar</legend>
                            <div class="auto-style5">
                                <table style="margin-left: 100px" class="auto-style8">
                                    <tr>
                                        <td class="auto-style13" style="text-align:right">
                                            <asp:Label ID="LabelClasif" runat="server" CssClass="LabelGeneral" Text="Clasificación: "></asp:Label>
                                        </td>
                                        <td style="text-align:left;">
                                            <asp:DropDownList ID="ddlTipo" runat="server" AutoPostBack="true" CssClass="DropGeneral" Height="20px" Width="420px" OnSelectedIndexChanged="ddlTipo_SelectedIndexChanged"></asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr id="ddlDistritoDiv" runat="server">
                                        <td class="auto-style13" style="text-align:right;">
                                            <asp:Label ID="LabelDistrito" runat="server" CssClass="LabelGeneral" Text="Distrito: " Visible="false"></asp:Label>
                                        </td>
                                        <td style="text-align:left;">
                                            <asp:DropDownList ID="ddlDistrito" runat="server" AutoPostBack="true" CssClass="DropGeneral" Visible="false" Height="20px" Width="420px" ></asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="auto-style8">
                                            <asp:Button ID="BtnBuscar" runat="server" CssClass="Boton" Style="margin-top: 15px; align-content: center;" Height="33px" Width="98px" Text="Buscar" OnClick="BtnBuscar_Click"/>
                                        </td>
                                        <td class="auto-style8" style="text-align:right;">
                                            <asp:Button ID="BtnNuevoR" runat="server" CssClass="Boton" Style="margin-top: 15px; align-content: center;" Height="33px" Width="161px" Text="Nuevo Registro" OnClick="BtnNuevoR_Click"/>
                                        </td>
                                    </tr>

                                </table>
                            </div>
                        </fieldset>
                    </div>

                    <div id="DivResultados" runat="server" style="width: 100%; text-align: center;" visible="false">
                        <fieldset style="height: auto">
                            <asp:GridView ID="GridTabla" runat="server" CssClass="StyleGridV" AutoGenerateColumns="False" HorizontalAlign="Center"
                                DataKeyNames="IdUniAdmin" 
                                OnRowCommand="GridModificar_RowCommand"
                                OnRowDeleting="GridBuscar_RowDeleting">
                                <Columns>
                                    <asp:BoundField DataField="UniAdmin" HeaderText="Unidad Administrativa"/>
                                    <asp:BoundField DataField="SubFondo" HeaderText="Subfondo"/>
                                    <asp:BoundField DataField="DescTipo" HeaderText="Tipo"/>
                                    <asp:BoundField DataField="Telefono" HeaderText="Teléfono"/>
                                    <asp:BoundField DataField="Email" HeaderText="Correo electrónico"/>
                                    <asp:BoundField DataField="DescClasific" HeaderText="Clasificación"/>                               
                                    <asp:TemplateField HeaderText="Editar" ItemStyle-Width="50">
                                        <ItemTemplate>
                                            <asp:ImageButton runat="server" ID="btnEditar" Width="30" Height="30" ImageUrl="~/Imagenes/Generales/editar.png" 
                                                CommandName="Editar" OnClick="btnEditar_Click" CommandArgument='<%# Container.DataItemIndex.ToString() %>' />
                                        </ItemTemplate>
                                        <ItemStyle Width="50px" />
                                    </asp:TemplateField>
                                    <asp:CommandField ButtonType="Image" HeaderText="Eliminar" DeleteImageUrl="~/Imagenes/Generales/eliminar.png" ShowDeleteButton="true" runat="server" >
                                      <ControlStyle Width="30px" />
                                    </asp:CommandField>                                   
                                    
                                </Columns>
                            </asp:GridView>
                        </fieldset>
                    </div>
               </div>
                <div id="DivNuevoReg" runat="server" visible="false" style="width: 100%; height: auto;">
                    <fieldset style="height: auto; border-color:black;">
                        <legend id="lgNuevoR" runat="server" style="text-align: left; color: darkblue;" >Nuevo Registro</legend>
                        <asp:TextBox ID="IdUniAdminTxt" runat="server" Visible="false" ></asp:TextBox>
                        <table style="margin-top: 15px; margin: 0 auto; width: 70%;">
                            <tr>
                                <td class="auto-style14" style="text-align: right;">
                                    <asp:Label ID="LblTipoA" runat="server" CssClass="LabelGeneral">Tipo de área</asp:Label>
                                </td>
                                <td style="text-align: left;">
                                    <asp:DropDownList ID="ddlTipoNuevo" runat="server" CssClass="DropGeneral" AutoPostBack="true" AppendDataBoundItems="true" OnSelectedIndexChanged="ddlTipoNuevo_SelectedIndexChanged"></asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td class="auto-style14" style="text-align: right;">
                                    <asp:Label ID="LblDistritoN" runat="server" Text="Distrito" CssClass="LabelGeneral" Visible="false"></asp:Label>
                                </td>
                                <td style="text-align: left;">
                                    <asp:DropDownList ID="ddlDistritoNuevo" runat="server" Visible="false" AutoPostBack="true" CssClass="DropGeneral"></asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td class="auto-style14" style="text-align: right;">
                                    <asp:Label ID="LblSubfondo" runat="server" Text="Subfondo" CssClass="LabelGeneral">SubFondo</asp:Label>
                                </td>
                                <td style="text-align: left;">
                                    <asp:DropDownList ID="ddlSubFondoN" runat="server" AutoPostBack="true" CssClass="DropGeneral"></asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td class="auto-style14" style="text-align: right;">
                                    <asp:Label ID="lblTipoSub" runat="server" Text="Tipo" CssClass="LabelGeneral">Tipo</asp:Label>
                                </td>
                                <td style="text-align: left;">
                                    <asp:DropDownList ID="ddlTipoSub" runat="server" AutoPostBack="true" CssClass="DropGeneral"></asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td class="auto-style14" style="text-align: right;">
                                    <asp:Label ID="lblNombreUniNuevo" runat="server" Text="Nombre Unidad Administrativa" CssClass="LabelGeneral"></asp:Label>
                                </td>
                                <td style="text-align: left;">
                                    <asp:TextBox ID="TxTNombreUniN" runat="server" CssClass="TxtGeneral" ></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="auto-style14" style="text-align: right;">
                                    <asp:Label ID="lblTelefonoNu" runat="server" CssClass="LabelGeneral" Text="Teléfono"></asp:Label>
                                </td>
                                <td style="text-align: left;">
                                    <asp:TextBox ID="TxtTelefonoNue" runat="server" CssClass="TxtGeneral" ></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="auto-style14" style="text-align: right;">
                                    <asp:Label ID="lblCorreoN" runat="server" Text="Correo Electrónico" CssClass="LabelGeneral"></asp:Label>
                                </td>
                                <td style="text-align: left;">
                                    <asp:TextBox ID="TxTCorreoN" runat="server" CssClass="TxtGeneral"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="auto-style8" style="text-align:left;">
                                    <asp:Button id="BtnGuardar" runat="server" Text="Guardar" CssClass="Boton" OnClick="BtnGuardar_Click"/>
                                </td>
                                <td class="auto-style8" style="text-align:right;">
                                    <asp:Button id="BtnCancelar" runat="server" Text="Cancelar" CssClass="Boton" OnClick="BtnCancelar_Click"/>
                                </td>
                            </tr>
                        </table>
                    </fieldset>
                </div>

                <div id="MostrarInfo" runat="server" visible="false">
                    <asp:Label ID="IdSubF" runat="server"></asp:Label>
                    <asp:Label ID="UniA" runat="server"></asp:Label>
                    <asp:Label ID="TipoNu" runat="server"></asp:Label>
                    <asp:Label ID="Tele" runat="server"></asp:Label>
                    <asp:Label ID="Emails" runat="server"></asp:Label>
                    <asp:Label ID="Clasifica" runat="server"></asp:Label>
                    <asp:Label ID="IdAbrevi" runat="server"></asp:Label>
                    <asp:Label ID="IdEmpleo" runat="server"></asp:Label>

                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>


</asp:Content>


<asp:Content ID="Content3" runat="server" ContentPlaceHolderID="CPHTitulo">
    <div style="width: auto; text-align: center; background-color: #d8d8d8;">
        <asp:Label ID="LblLema" runat="server" Text="Unidad Administrativa" Font-Size="Large" Font-Bold="True" Font-Italic="True" CssClass="LblLema"></asp:Label>
    </div>
</asp:Content>

