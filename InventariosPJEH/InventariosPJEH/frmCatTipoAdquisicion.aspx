<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Inventarios1.Master" CodeBehind="frmCatTipoAdquisicion.aspx.cs" Inherits="InventariosPJEH.frmCatTipoAdquisicion" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

            <div id="bodyContenedorBda;" style="width: auto; height: auto; text-align: center">
                <asp:HiddenField ID="Rowindex" runat="server"></asp:HiddenField>

                <div style="width: auto;">
                    <fieldset style="width: auto; height: auto; border-color: #6D252B;">
                        <legend style="text-align: left; color: darkblue;">Buscar Tipo de adquisición</legend>

                        <table style="width: 100%;">
                            <tr>
                                <td style="text-align: right;">
                                    <asp:Label ID="LbTipoAdquisicion" runat="server" Text="Tipo de adquisición: " Font-Size="Medium" Style="font-family: Verdana; font-size: small; font-weight: 400; margin-left: 10px;"></asp:Label>
                                </td>
                                <td style="text-align: left;" class="auto-style7">
                                    <asp:TextBox ID="TxtTipoAdquisicionExistente" runat="server" CssClass="auto-style6" Height="20px" onkeypress="return ValidacionLetras(event);" Width="429px"></asp:TextBox>
                                </td>
                                <td style="text-align: left;" class="auto-style8">
                                    <asp:Button ID="BtnTipoAdquisicion" runat="server" Text="Buscar" CssClass="Boton" OnClick="BtnBuscar_Click" Style="align-content: space-around; height: 28px;" />
                                </td>
                                <td style="text-align: right;">
                                    <asp:Button ID="BtnMostrarNuevoR" runat="server" CssClass="Boton" OnClick="BtnMostraOcultar" Style="align-content: space-around;" Text="Nuevo Registro" Width="149px" />
                                </td>
                            </tr>

                        </table>
                </div>

                </fieldset>
            </div>

            <div runat="server" id="DivTabla" style="width: 100%;" visible="false">

                <fieldset style="height: auto">

                    <asp:GridView ID="GridBuscarTipoAquisicion" CssClass="StyleGridV" runat="server" Height="100px" Width="98%"
                        AutoGenerateColumns="False" OnRowDeleting="GridBuscarTipoAdquisicion_RowDeleting"
                        DataKeyNames="IdTipoAdqui,TipoAdqui"
                        OnRowCommand="GridBuscarTipoAdquisicion_RowCommand" PageSize="25" OnPageIndexChanging="GridBuscarTipoAquisicion_PageIndexChanging">
                        <Columns>

                            <asp:BoundField DataField="IdTipoAdqui" HeaderText="Id" ItemStyle-Width="10px" />
                            <asp:BoundField DataField="TipoAdqui" HeaderText="Tipo de adquisición" ItemStyle-Width="80px" />
                            <asp:ButtonField ButtonType="Image" ImageUrl="~/Imagenes/Generales/editar.png" ControlStyle-Width="30px" HeaderText="Editar" Text="Editar"
                                CommandName="Prueba" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10px" />
                            <asp:CommandField ButtonType="Image" HeaderText="Eliminar" DeleteImageUrl="~/Imagenes/Generales/eliminar.png" ShowDeleteButton="true" ItemStyle-Width="10px">
                                <ControlStyle Width="30px" />

                            </asp:CommandField>
                        </Columns>
                    </asp:GridView>
                </fieldset>


            </div>
            <div runat="server" id="DivMostrarNuevoR" style="width: 100%; height: auto;" visible="false">

                <asp:HiddenField ID="HiddenId" runat="server" />

                <fieldset style="height: auto; border-color: #6D252B;">
                    <legend style="text-align: left; color: darkblue;" runat="server" id="lgNuevoRegistro" visible="false">Nuevo registro</legend>
                    <legend style="text-align: left; color: darkblue;" runat="server" id="lgModificarRegistro" visible="false">Modificar Registro</legend>

                    <table style="width: 100%;">
                        <tr>
                            <td>
                                <asp:Label ID="LbId" runat="server" Visible="false" Style="margin-left: 120px;"></asp:Label>
                            </td>
                            <td>&nbsp;</td>

                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="LbRegistroTipoAdqui" runat="server" Text="Nombre de adquisición:" Style="font-family: Verdana; font-size: small; margin-left: 10px; font-weight: 400;"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="TxtTipoAdquisicion" runat="server" Style="margin-left: 0px" CssClass="TxtGeneral" Width="700px" Height="20px" onkeypress="return ValidacionLetras(event);"></asp:TextBox>
                            </td>

                        </tr>

                    </table>

                </fieldset>

                <br />
            </div>
            <table style="width: 100%; margin-top: 10px; text-align: center;">
                <tr>
                    <td>
                        <asp:Button ID="BtnGuardar" runat="server" CssClass="Boton" OnClick="BtnGuardarNuevoProvedor" Text="Guardar" Visible="false" />

                    </td>
                    <td>
                        <asp:Button ID="BtnActualizar" runat="server" CssClass="Boton" OnClick="BtnActualizar_Click" Text="Actualizar" Visible="false" />
                    </td>
                    <td>

                        <asp:Button ID="BtnLimpiar" runat="server" CssClass="Boton" OnClick="BtnLimpiar_Click" Text="Limpiar" Visible="false" />
                    </td>
                    <td>

                        <asp:Button ID="BtnCancelar" runat="server" CssClass="Boton" OnClick="BtnCancelar_Click" Text="Cancelar" Visible="false" />
                    </td>
                </tr>
            </table>

            </div>

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>



<asp:Content ID="Content3" runat="server" ContentPlaceHolderID="CPHTitulo">
    <div style="width: auto; text-align: center; background-color: #d8d8d8;">
        <asp:Label ID="LblLema" runat="server" Text="TIPO DE ADQUISICIÓN" Font-Size="Large" Font-Bold="True" Font-Italic="True" ForeColor="#6D252B"></asp:Label>
    </div>
</asp:Content>





