<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Inventarios1.Master" CodeBehind="frmCatMarca.aspx.cs" Inherits="InventariosPJEH.frmCatMarca" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

            <div id="bodyContenedorBda;" style="width: auto; height: auto; text-align: center">
                <asp:HiddenField ID="Rowindex" runat="server"></asp:HiddenField>

                <div style="width: auto;">
                    <fieldset style="width: auto; height: auto; border-color: #6D252B;">
                        <legend style="text-align: left; color: darkblue;">Buscar marca</legend>
                        <table style="width: 100%;">

                            <tr>
                                <td class="auto-style47" style="text-align:right;">
                                    <asp:Label ID="LbSubClaseBuscar" runat="server" Text="Nombre de la subclase:" Font-Size="Medium" Style="font-family: Verdana; font-size: small; font-weight: 400; "></asp:Label>
                                </td>
                                <td>
                                    <asp:DropDownList runat="server" ID="DropSubClaseBuscar" AutoPostBack="true" Height="20px" Width="605px"></asp:DropDownList>
                                </td>
                                <td class="auto-style4" style="text-align: center">
                                    <asp:Button ID="BtnTipoPMarca" runat="server" Text="Buscar" CssClass="Boton" OnClick="BtnBuscar_Click" Style="align-content: space-around; height: 28px;" />
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: right;" class="auto-style47">
                                    <asp:Label ID="LbMarcaBuscar" runat="server" Text="Nombre de la marca:" Font-Size="Medium" Style="font-family: Verdana; font-size: small; font-weight: 400; margin-left: 10px;"></asp:Label>
                                </td>
                                <td class="auto-style3">
                                    <asp:TextBox  runat="server" ID="TxtMarcaBuscar" CssClass="TxtGeneral" Height="20px" onkeypress="return ValidacionLetras(event);" Width="600px"></asp:TextBox>
                                    </td>
                            </tr>

                        </table>
                         <table style="width: 100%;">
                    <tr>
                        
                        <td style="text-align: center;">
                            <asp:Button ID="BtnMostrarNuevoR" runat="server" Text="Nuevo Registro" CssClass="Boton" OnClick="BtnMostraOcultar" Style="align-content: space-around;" Height="28px" Width="168px" />
                        </td>
                    </tr>

                </table>
                </div>

               
                </fieldset>
            </div>


            <div runat="server" id="DivTabla" style="width: 100%;" visible="false">

                <fieldset style="height: auto">

                    <asp:GridView ID="GridBuscar" CssClass="StyleGridV" runat="server" Height="100px" Width="100%"
                        AutoGenerateColumns="False" OnRowDeleting="GridBuscarMarcas_RowDeleting"
                        DataKeyNames="IdMarca, Descripcion, IdSubclase, Subclase, IdPartida, Partida, IdMarSub"
                        OnRowCommand="GridBuscarMarcas_RowCommand" HorizontalAlign="Center" PageSize="25" OnPageIndexChanging="GridBuscar_PageIndexChanging">
                        <Columns>

                            <asp:BoundField DataField="IdMarca" HeaderText="Id marca" ItemStyle-Width="20px"  Visible="false">
                               <ItemStyle Width="20px" />   </asp:BoundField>
                            <asp:BoundField DataField="Descripcion" HeaderText="Marca" ItemStyle-Width="40px" >
                                 <ItemStyle Width="60px" />   </asp:BoundField>
                            <asp:BoundField DataField="IdSubclase" HeaderText="IdSubClase" ItemStyle-Width="30px" Visible="false" >
                             <ItemStyle Width="30px" />   </asp:BoundField>
                            <asp:BoundField DataField="SubClase" HeaderText="SubClase" ItemStyle-Width="80px" >
                            <ItemStyle Width="60px" />  </asp:BoundField>
                            <asp:BoundField DataField="IdPartida" HeaderText="IdPartida" ItemStyle-Width="30px" Visible="false" >
                             <ItemStyle Width="30px" />   </asp:BoundField>
                            <asp:BoundField DataField="Partida" HeaderText="Partida" ItemStyle-Width="80px" >
                            <ItemStyle Width="60px" />  </asp:BoundField>
                            <asp:BoundField DataField="IdMarSub" HeaderText="MarSub" ItemStyle-Width="80px" Visible="false" >
                            <ItemStyle Width="60px" />  </asp:BoundField>
                            <asp:ButtonField ButtonType="Image" ImageUrl="~/Imagenes/Generales/editar.png" ControlStyle-Width="30px" HeaderText="Editar" Text="Editar"
                                CommandName="Prueba" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10px">
                                <ControlStyle Width="30px" />
                                <ItemStyle HorizontalAlign="Center" Width="30px" />
                            </asp:ButtonField>

                            <asp:CommandField ButtonType="Image" HeaderText="Eliminar" DeleteImageUrl="~/Imagenes/Generales/eliminar.png" ShowDeleteButton="true" >
                                <ControlStyle Width="30px" />
                                <ItemStyle Width="30px" HorizontalAlign="Center" />
                            </asp:CommandField>
                           
                            
                        </Columns>
                    </asp:GridView>
                </fieldset>

                

            </div>

            <div runat="server" id="DivMostrarNuevoR" style="width: 100%; height: auto;" visible="false">

                <asp:HiddenField ID="HiddenId" runat="server" />

                <fieldset style="height: auto; border-color: #6D252B;">
                    <legend style="text-align: left; color: darkblue;" runat="server" id="lgNuevoRegistro" visible="false">Nuevo registro</legend>
                    <legend style="text-align: left; color: darkblue;" runat="server" id="lgModificarRegistro" visible="false">Modificar registro</legend>


                    <table style="margin-top: 15px; margin: 0 auto; width: 70%;">
                        <tr>
                            <td>
                                <asp:Label id="lbMarca" runat="server"  Visible="false" > </asp:Label>
                            </td>
                            <td>
                                <asp:Label id="LbMarSub" runat="server"  Visible="false"> </asp:Label>
                            </td>
                        </tr>
                        
                    </table>

                    <table style="margin: 0 auto; width: 70%;">
                        <tr>
                            <td style="text-align:right;">
                                <asp:Label ID="LbSubClaseNuevo" runat="server" Text="Subclase:" Style="font-family: Verdana; font-size: small;"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="DropSubClase" runat="server" AutoPostBack="true" Width="478px" Height="19px">  </asp:DropDownList>
                            </td>
                         
                        </tr>
                        <tr>
                               <td style="text-align: right;" class="auto-style46">
                                <asp:Label ID="LbMarcaNuevo" runat="server" Text="Nombre de la marca:" Style="font-family: Verdana; font-size: small;"></asp:Label>

                            </td>
                            <td class="auto-style42">
                                <asp:TextBox ID="TxtDescripcion" runat="server" CssClass="TxtGeneral" Height="20px" onkeypress="return ValidacionLetras(event);" Width="473px"></asp:TextBox>
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
        <asp:Label ID="LblLema" runat="server" Text="MARCAS Y SUBCLASES" Font-Size="Large" Font-Bold="True" Font-Italic="True" ForeColor="#6D252B"></asp:Label>
    </div>
</asp:Content>








