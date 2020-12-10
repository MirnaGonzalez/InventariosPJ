<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Inventarios1.Master" CodeBehind="frmCatSubClases_Partidas.aspx.cs" Inherits="InventariosPJEH.frmCatSubClases_Partidas" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

            <div id="bodyContenedorBda;" style="width: auto; height: auto; text-align: center">
                <asp:HiddenField ID="Rowindex" runat="server"></asp:HiddenField>

                <div style="width: auto;">
                    <fieldset style="width: auto; height: auto; border-color: #6D252B;">
                        <legend style="text-align: left; color: darkblue;">Buscar partidas</legend>
                        <table style="width: 100%;">


                            <tr>
                                <td style="text-align: right;" class="auto-style47">
                                    <asp:Label ID="LbPartidasBuscar" runat="server" Text="Nombre de la partida:" Font-Size="Medium" Style="font-family: Verdana; font-size: small; font-weight: 400; margin-left: 10px;"></asp:Label>
                                </td>
                                <td class="auto-style3">
                                    <asp:TextBox runat="server" ID="TxtPartidaBuscar" CssClass="TxtGeneral" Height="20px" onkeypress="return ValidacionLetras(event);" Width="600px"></asp:TextBox>
                                </td>
                                <td class="auto-style4" style="text-align: center">
                                    <asp:Button ID="BtnTipoPMarca" runat="server" Text="Buscar" CssClass="Boton" OnClick="BtnBuscar_Click" Style="align-content: space-around; height: 28px;" />
                                </td>
                            </tr>

                        </table>
                </div>

                </fieldset>
            </div>


            <div runat="server" id="DivTabla" style="width: 100%;" visible="false">
                <table style="width: 100%;">
                    <tr>
                        <td style="text-align: center;">
                            <asp:Button ID="BtnMostrarNuevoR" runat="server" Text="Agregar partidas" CssClass="Boton" OnClick="BtnMostraOcultar" Style="align-content: space-around;" Height="28px" Width="168px" />
                        </td>
                    </tr>

                </table>
                <fieldset style="height: auto">
                    <legend style="text-align: center; color: darkblue;" runat="server" id="Legend1" visible="true">PARTIDAS</legend>

                    <asp:GridView ID="GridBuscar" CssClass="StyleGridV" runat="server" Height="100px" Width="100%"
                        AutoGenerateColumns="False" OnRowDeleting="GridBuscarPartidas_RowDeleting"
                        DataKeyNames="IdPartida,Partida,TipoPartida,NumPartida"
                        OnRowCommand="GridBuscarPartidas_RowCommand" HorizontalAlign="Center"
                        OnSelectedIndexChanging="GridBuscar_SelectedIndexChanging">
                        <Columns>

                            <asp:BoundField DataField="IdPartida" HeaderText="Clave Partida" ItemStyle-Width="20px">
                                <ItemStyle Width="20px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Partida" HeaderText="Partida" ItemStyle-Width="40px">
                                <ItemStyle Width="60px" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="Tipo de partida" SortExpression="TipoPartida">
                                <ItemTemplate>
                                    <asp:Label ID="Label6" runat="server" Text='<%# ((string)Eval("TipoPartida") == "1") ? "BIENES INFORMÁTICOS" : ((string)Eval("TipoPartida")== "2") ? "BIENES MUEBLES" : "OTROS" %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Width="70px" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="NumPartida" HeaderText="Número de partida" ItemStyle-Width="80px">
                                <ItemStyle Width="60px" />
                            </asp:BoundField>

                            <asp:ButtonField ButtonType="Image" ImageUrl="~/Imagenes/Generales/editar.png" ControlStyle-Width="30px" HeaderText="Editar" Text="Editar"
                                CommandName="Prueba" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10px">
                                <ControlStyle Width="30px" />
                                <ItemStyle HorizontalAlign="Center" Width="30px" />
                            </asp:ButtonField>

                            <asp:CommandField ButtonType="Image" HeaderText="Eliminar" DeleteImageUrl="~/Imagenes/Generales/eliminar.png" ShowDeleteButton="true">
                                <ControlStyle Width="30px" />
                                <ItemStyle Width="30px" HorizontalAlign="Center" />
                            </asp:CommandField>

                            <asp:CommandField ButtonType="Image" ShowSelectButton="true" HeaderText="Consultar subclase" SelectImageUrl="~/Imagenes/Generales/consultar.png">
                                <ControlStyle Width="30px" />
                                <ItemStyle Width="30px" HorizontalAlign="Center" />
                            </asp:CommandField>

                        </Columns>
                    </asp:GridView>
                </fieldset>

                <table style="width: 100%;">
                    <tr>
                        <td style="text-align: center;">
                            <asp:Button ID="BtnMostrarNuevoR2" runat="server" Text="Agregar subclases" CssClass="Boton" OnClick="BtnMostraOcultar2" Style="align-content: space-around;" Height="28px" Width="175px" />

                        </td>
                    </tr>
                </table>
            </div>

            <div runat="server" id="DivMostrarNuevoR" style="width: 100%; height: auto;" visible="false">

                <asp:HiddenField ID="HiddenId" runat="server" />

                <fieldset style="height: auto; border-color: #6D252B;">
                    <legend style="text-align: left; color: darkblue;" runat="server" id="lgNuevoRegistro" visible="false">Nuevo registro</legend>
                    <legend style="text-align: left; color: darkblue;" runat="server" id="lgModificarRegistro" visible="false">Modificar registro</legend>


                    <table style="margin-top: 15px; margin: 0 auto; width: 70%;">
                        <tr>
                            <td>
                                <asp:Label ID="lbPartida" runat="server" Visible="false"> </asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="LbSubClase" runat="server" Visible="false"> </asp:Label>
                            </td>
                        </tr>

                    </table>

                    <table style="margin: 0 auto; width: 70%;">
                        <tr>
                            <td style="text-align: right;" class="auto-style46">
                                <asp:Label ID="LbNombrePartidaNuevo" runat="server" Text="Nombre de partida:" Style="font-family: Verdana; font-size: small;"></asp:Label>

                            </td>
                            <td class="auto-style42">
                                <asp:TextBox ID="TxtPartidaNuevo" runat="server" CssClass="TxtGeneral" Height="20px" onkeypress="return ValidacionLetras(event);" Width="473px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right;" class="auto-style46">
                                <asp:Label ID="LbNumPartidaNuevo" runat="server" Text="Número de partida:" Style="font-family: Verdana; font-size: small;"></asp:Label>

                            </td>
                            <td class="auto-style42">
                                <asp:TextBox ID="TxtNumPartidaNuevo" runat="server" CssClass="TxtGeneral" Height="20px" onkeypress="return soloNumeros(event)" MaxLength="6" Width="473px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right;" class="auto-style1">
                                <asp:Label ID="LbTipoPartidaNuevo" runat="server" Text="Tipo de partida:" Style="font-family: Verdana; font-size: small;"></asp:Label>
                            </td>
                            <td class="auto-style1">
                                <asp:DropDownList ID="DropTipoPartida" runat="server" AutoPostBack="true" Width="478px" Height="19px">
                                    <asp:ListItem Text="Seleccionar"></asp:ListItem>
                                    <asp:ListItem Value="1">BIENES INFORMÁTICOS</asp:ListItem>
                                    <asp:ListItem Value="2">BIENES MUEBLES</asp:ListItem>
                                    <asp:ListItem Value="9">OTROS</asp:ListItem>
                                </asp:DropDownList>
                            </td>

                        </tr>

                    </table>

                </fieldset>

                <br />
            </div>

            <table style="width: 100%; margin-top: 10px; text-align: center;">
                <tr>
                    <td>
                        <asp:Button ID="BtnGuardar" runat="server" CssClass="Boton" OnClick="Guardar_Click" Text="Guardar" Visible="false" />

                    </td>
                    <td>
                        <asp:Button ID="BtnActualizar" runat="server" CssClass="Boton" OnClick="BtnActualizar_Click" Text="Actualizar" Visible="false" />
                    </td>

                </tr>
            </table>

            <div runat="server" id="DivTabla2" style="width: 100%;" visible="false">

                <fieldset style="height: auto;">
                    <legend style="text-align: center; color: darkblue;" runat="server" id="LgSubClases" visible="true">SUBCLASES</legend>

                    <asp:GridView ID="GridBuscar2" CssClass="StyleGridV" runat="server" Height="100px" Width="100%"
                        AutoGenerateColumns="False" OnRowDeleting="GridBuscarSubClases_RowDeleting"
                        DataKeyNames="IdSubClase, SubClase, IdPartida, Partida, Folio"
                        OnRowCommand="GridBuscarSubClases_RowCommand" HorizontalAlign="Center">
                        <Columns>

                            <asp:BoundField DataField="IdSubClase" HeaderText="Clave subclase" ItemStyle-Width="20px">
                                <ItemStyle Width="20px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="SubClase" HeaderText="Subclase" ItemStyle-Width="40px">
                                <ItemStyle Width="60px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="IdPartida" HeaderText="Clave Partida" ItemStyle-Width="30px">
                                <ItemStyle Width="30px" />
                            </asp:BoundField>

                            <asp:BoundField DataField="Partida" HeaderText="Partida" ItemStyle-Width="80px">
                                <ItemStyle Width="60px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Folio" HeaderText="Folio" ItemStyle-Width="80px">
                                <ItemStyle Width="60px" />
                            </asp:BoundField>
                            <asp:ButtonField ButtonType="Image" ImageUrl="~/Imagenes/Generales/editar.png" ControlStyle-Width="30px" HeaderText="Editar" Text="Editar"
                                CommandName="Prueba" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10px">
                                <ControlStyle Width="30px" />
                                <ItemStyle HorizontalAlign="Center" Width="30px" />
                            </asp:ButtonField>

                            <asp:CommandField ButtonType="Image" HeaderText="Eliminar" DeleteImageUrl="~/Imagenes/Generales/eliminar.png" ShowDeleteButton="true">
                                <ControlStyle Width="30px" />
                                <ItemStyle Width="30px" HorizontalAlign="Center" />
                            </asp:CommandField>


                        </Columns>
                    </asp:GridView>
                </fieldset>

                <table style="width: 100%;">
                    <tr>
                        <td style="text-align: center;"></td>
                    </tr>

                </table>

            </div>

            <div runat="server" id="DivMostrarNuevoR2" style="width: 100%; height: auto;" visible="false">

                <asp:HiddenField ID="Rowindex2" runat="server" />

                <fieldset style="height: auto; border-color: #6D252B;">
                    <legend style="text-align: left; color: darkblue;" runat="server" id="lgNuevoRegistro2" visible="false">Nuevo registro</legend>
                    <legend style="text-align: left; color: darkblue;" runat="server" id="lgModificarRegistro2" visible="false">Modificar registro</legend>


                    <table style="margin-top: 15px; margin: 0 auto; width: 70%;">
                        <tr>
                            <td>
                                <asp:Label ID="LbSubClaseN" runat="server" Visible="false"> </asp:Label>
                            </td>

                        </tr>

                    </table>

                    <table style="margin: 0 auto; width: 70%;">
                        <tr>
                            <td style="text-align: right;" class="auto-style2">
                                <asp:Label ID="LbSubClaseNuevo" runat="server" Text="Clave subclase:" Style="font-family: Verdana; font-size: small;"></asp:Label>

                            </td>
                            <td class="auto-style2">
                                <asp:TextBox ID="TxtClaveSubClase" runat="server" CssClass="TxtGeneral" Height="20px" onkeypress="return soloNumeros(event)" MaxLength="4" Width="473px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right;" class="auto-style46">
                                <asp:Label ID="LbnNombreSubClaseNuevo" runat="server" Text="Subclase:" Style="font-family: Verdana; font-size: small;"></asp:Label>

                            </td>
                            <td class="auto-style42">
                                <asp:TextBox ID="TxtSubClaseNuevo" runat="server" CssClass="TxtGeneral" Height="20px" onkeypress="return ValidacionLetras(event);" Width="473px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right;" class="auto-style46">
                                <asp:Label ID="LbFlioNuevo" runat="server" Text="Número de folio:" Style="font-family: Verdana; font-size: small;"></asp:Label>

                            </td>
                            <td class="auto-style42">
                                <asp:TextBox ID="TxtFolioNuevo" runat="server" CssClass="TxtGeneral" Height="20px" onkeypress="return soloNumeros(event)" MaxLength="4" Width="473px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right;" class="auto-style1">
                                <asp:Label ID="LbPartidaNuevoSubClase" runat="server" Text="Partida:" Style="font-family: Verdana; font-size: small;"></asp:Label>
                            </td>
                            <td class="auto-style1">
                                <asp:DropDownList ID="DropPartidaNuevo" runat="server" AutoPostBack="true" Width="478px" Height="19px"></asp:DropDownList>
                            </td>

                        </tr>

                    </table>

                </fieldset>

                <br />
            </div>

            <table style="width: 100%; margin-top: 10px; text-align: center;">
                <tr>
                    <td>
                        <asp:Button ID="BtnGuardar2" runat="server" CssClass="Boton" OnClick="GuardarSubClase_Click" Text="Guardar" Visible="false" />

                    </td>
                    <td>
                        <asp:Button ID="BtnActualizar2" runat="server" CssClass="Boton" OnClick="BtnActualizarSubClase_Click" Text="Actualizar" Visible="false" />
                    </td>
                    <td>
                        <asp:Button ID="BtnLimpiar" runat="server" CssClass="Boton" OnClick="BtnLimpiar_Click" Text="Limpiar" Visible="false" />
                    </td>
                    <td>
                        <asp:Button ID="BtnCancelar" runat="server" CssClass="Boton" OnClick="BtnCancelar_Click" Text="Cancelar" Visible="false" />
                    </td>
                </tr>
            </table>



        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

<asp:Content ID="Content3" runat="server" ContentPlaceHolderID="CPHTitulo">
    <div style="width: auto; text-align: center; background-color: #d8d8d8;">
        <asp:Label ID="LblLema" runat="server" Text="SUBCLASES Y PARTIDAS" Font-Size="Large" Font-Bold="True" Font-Italic="True" ForeColor="#6D252B"></asp:Label>
    </div>
</asp:Content>



<asp:Content ID="Content4" runat="server" ContentPlaceHolderID="HeadContent">
    <style type="text/css">
        .auto-style1 {
            height: 24px;
        }

        .auto-style2 {
            height: 28px;
        }
    </style>
</asp:Content>




