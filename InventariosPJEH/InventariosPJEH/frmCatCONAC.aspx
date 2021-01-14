<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Inventarios1.Master" CodeBehind="frmCatCONAC.aspx.cs" Inherits="InventariosPJEH.frmCatCONAC" %>


<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        .auto-style3 {
            width: 438px;
        }

        .auto-style4 {
            width: 116px;
        }

        .auto-style32 {
            width: 55px;
            height: 28px;
        }

        .auto-style34 {
            width: 53px;
            height: 28px;
        }

        .auto-style37 {
            width: 161px;
            height: 28px;
        }

        .auto-style42 {
            width: 170px;
            height: 30px;
        }

        .auto-style46 {
            width: 146px;
            height: 30px;
        }

        .auto-style47 {
            Font-Size: Medium;
            font-family: Verdana;
            font-size: small;
            font-weight: 400;
            border: 1px solid #663622;
        }

        </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

            <div id="bodyContenedorBda;" style="width: auto; height: auto; text-align: center">
                <asp:HiddenField ID="Rowindex" runat="server"></asp:HiddenField>

                <div style="width: auto;">
                    <fieldset style="width: auto; height: auto; border-color: #6D252B;">
                        <legend style="text-align: left; color: darkblue;">Buscar CONAC</legend>
                        <table style="width: 100%;">

                            <tr>
                                <td style="text-align: right;">
                                    <asp:Label ID="LbDescripcionBuscar" runat="server" Text="Descripción:" Font-Size="Medium" Style="font-family: Verdana; font-size: small; font-weight: 400; margin-left: 10px;"></asp:Label>
                                </td>
                                <td class="auto-style3">
                                    <asp:TextBox runat="server" ID="TxtDescripcionBuscar" CssClass="auto-style47" Height="20px" onkeypress="return ValidacionLetras(event);" Width="513px" ></asp:TextBox>
                                </td>
                                <td class="auto-style4" style="text-align: center">
                                    <asp:Button ID="BtnTipoPartida" runat="server" Text="Buscar" CssClass="Boton" OnClick="BtnBuscar_Click" Style="align-content: space-around; height: 28px;" />
                                </td>
                                <td>
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
                        AutoGenerateColumns="False" OnRowDeleting="GridBuscarCONAC_RowDeleting"
                        DataKeyNames="IdCONAC, IdClaveCONAC, Grupo, SubGrupo, Clase, Descripcion, TipoPartida"
                        OnRowCommand="GridBuscarCONAC_RowCommand" PageSize="25" OnPageIndexChanging="GridBuscar_PageIndexChanging">
                        <Columns>
                            <asp:BoundField DataField="IdCONAC" HeaderText="IdCONAC" ItemStyle-Width="10px" >
                               <ItemStyle Width="50px" />   </asp:BoundField>
                            <asp:BoundField DataField="IdClaveCONAC" HeaderText="Clave CONAC" ItemStyle-Width="10px" >
                               <ItemStyle Width="50px" />   </asp:BoundField>
                            <asp:BoundField DataField="Grupo" HeaderText="Grupo" ItemStyle-Width="40px" >
                                 <ItemStyle Width="40px" />   </asp:BoundField>
                            <asp:BoundField DataField="SubGrupo" HeaderText="SubGrupo" ItemStyle-Width="30px" >
                             <ItemStyle Width="30px" />   </asp:BoundField>
                            <asp:BoundField DataField="Clase" HeaderText="Clase" ItemStyle-Width="80px" >
                            <ItemStyle Width="40px" />  </asp:BoundField>
                            <asp:BoundField DataField="Descripcion" HeaderText="Descripción" ItemStyle-Width="80px" >
                             <ItemStyle Width="120px" />   </asp:BoundField>
                            <asp:TemplateField HeaderText="Tipo de partida" SortExpression="TipoPartida">
                                    <ItemTemplate>
                                        <asp:Label ID="Label6" runat="server" Text='<%# ((string)Eval("TipoPartida") == "1") ? "BIENES INFORMÁTICOS" : ((string)Eval("TipoPartida")== "2") ? "BIENES MUEBLES" : "OTROS" %>'></asp:Label>
                                    </ItemTemplate>
                                  <ItemStyle Width="70px" /> 
                                </asp:TemplateField>
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
                                <asp:Label id="IdCONAC" runat="server" Visible="false" > </asp:Label>
                            </td>
                             <td>
                                <asp:Label id="IdClaveCONAC" runat="server" Visible="False"></asp:Label>
                            </td>
                        </tr>
                        <tr style="text-align: center; margin-top: 15px;">
                            <td class="auto-style37" style="text-align: right;">
                                <asp:Label ID="LbGrupo" runat="server" Text="Grupo:" Style="font-family: Verdana; font-size: small; margin-left: 10px;"></asp:Label>

                            </td>
                            <td class="auto-style32">
                                <asp:TextBox ID="TxtGrupo" runat="server" CssClass="TxtGeneral" Style="font-family: Verdana; font-size: small; font-weight: 400;" Width="50px" Height="20px" onkeypress="return soloNumeros(event)" MaxLength="1"></asp:TextBox>
                            </td>
                            <td style="text-align: right;" class="auto-style37">
                                <asp:Label ID="LbSubGrupo" runat="server" Text="SubGrupo:" Style="font-family: Verdana; font-size: small; font-weight: 400;"></asp:Label>
                            </td>
                            <td class="auto-style34">
                                <asp:TextBox ID="TxtSubGrupo" runat="server" CssClass="TxtGeneral" Width="50px" Style="font-family: Verdana; font-size: small; font-weight: 400;" Height="20px" onkeypress="return soloNumeros(event)" MaxLength="1"></asp:TextBox>
                            </td>
                            <td style="text-align: right;" class="auto-style37">
                                <asp:Label ID="LbClase" runat="server" Text="Clase:" Style="font-family: Verdana; font-size: small; font-weight: 400;"></asp:Label>
                            </td>
                            <td class="auto-style34">
                                <asp:TextBox ID="TxtClase" runat="server" CssClass="TxtGeneral" Width="50px" Height="20px" onkeypress="return soloNumeros(event)" MaxLength="1"></asp:TextBox>
                            </td>
                        </tr>
                    </table>

                    <table style="margin: 0 auto; width: 70%;">
                        <tr>
                            <td style="text-align: right;" class="auto-style46">
                                <asp:Label ID="LbDescripcion" runat="server" Text="Descripción:" Style="font-family: Verdana; font-size: small;"></asp:Label>

                            </td>
                            <td class="auto-style42">
                                <asp:TextBox ID="TxtDescripcion" runat="server" CssClass="TxtGeneral" Height="20px" onkeypress="return ValidacionLetras(event);" Width="473px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align:right;">
                                <asp:Label ID="LbTipoPartida" runat="server" Text="Tipo de partida:" Style="font-family: Verdana; font-size: small;"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="DropTpoPartida" runat="server" AutoPostBack="true" Width="478px" Height="19px">
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
        <asp:Label ID="LblLema" runat="server" Text="CONAC" Font-Size="Large" Font-Bold="True" Font-Italic="True" ForeColor="#6D252B"></asp:Label>
    </div>
</asp:Content>




