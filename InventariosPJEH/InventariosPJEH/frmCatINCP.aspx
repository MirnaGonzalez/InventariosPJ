<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Inventarios1.Master" CodeBehind="frmCatINCP.aspx.cs" Inherits="InventariosPJEH.frmCatINCP" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
   
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

            <div id="bodyContenedorBda;" style="width: auto; height: auto; text-align: center">
                <asp:HiddenField id="Rowindex" runat="server"></asp:HiddenField>

                <div style="width: auto;">
                    <fieldset style="width: auto; height: auto; border-color: #0C2261;">
                        <legend style="text-align: left;  color:darkblue;">Buscar INCP</legend>
                        <table style="width: 100%;">
                            <tr>
                                <td class="auto-style1"style="text-align: right;">
                                    <asp:Label ID="LbAnioExistente" runat="server" Text="Año: " Font-Size="Medium" Style="font-family: Verdana; font-size: small; font-weight: 400; "></asp:Label>
                                </td>
                                <td style="text-align:left;" class="auto-style3">
                                    <asp:DropDownList ID="DropINCP" runat="server" AutoPostBack="true" Height="20px" Width="569px"></asp:DropDownList>
                             
                                </td>
                                <td  style="text-align: left;" class="auto-style8">
                                    <asp:Button ID="BtnINCP" runat="server" Text="Buscar" CssClass="Boton" OnClick="BtnBuscar_Click" Style=" align-content: space-around; " Height="28px" />
                                </td>
                                <td style="text-align: right;" >
                                <asp:Button ID="BtnMostrarNuevoR" runat="server" Text="Nuevo Registro" CssClass="Boton" OnClick="BtnMostraOcultar" Style=" align-content: space-around; " Height="28px" Width="168px" />
                            </td>
                            </tr>
                        </table>
                </div>

                </fieldset>
            </div>


            <div runat="server" id="DivTabla" style="width: 100%;" visible="false">

                <fieldset style="height: auto">

                    <asp:GridView ID="GridBuscar" CssClass="StyleGridV" runat="server" Height="100px" Width="100%"
                        AutoGenerateColumns="False" OnRowDeleting="GridBuscarINCP_RowDeleting"
                        DataKeyNames="IdINCP,Anio,IVA,NumUMAS,ValorUMA,INCP"
                        OnRowCommand="GridBuscarINCP_RowCommand">
                        <Columns>

                            <asp:BoundField DataField="IdTipoDoc" HeaderText="Id" ItemStyle-Width="10px"  Visible="false">
                            <ItemStyle Width="10px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Anio" HeaderText="Año" ItemStyle-Width="80px" >
                            <ItemStyle Width="80px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="IVA" HeaderText="I. V. A." ItemStyle-Width="80px" DataFormatString="{0:N2}">
                            <ItemStyle Width="80px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="NumUMAS" HeaderText="Número de UMAS" ItemStyle-Width="80px" >
                            <ItemStyle Width="80px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="ValorUMA" HeaderText="Valor de UMA" ItemStyle-Width="80px" >
                            <ItemStyle Width="80px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="INCP" HeaderText="INCP" ItemStyle-Width="80px" >
                            <ItemStyle Width="80px" />
                            </asp:BoundField>
                            <asp:ButtonField ButtonType="Image" ImageUrl="~/Imagenes/Generales/editar.png" HeaderText="Editar" Text="Editar"
                                CommandName="Prueba" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10px" >
                           <ControlStyle Width="30px" />
                                <ItemStyle HorizontalAlign="Center" Width="30px" />
                            </asp:ButtonField>
                            <asp:CommandField ButtonType="Image" HeaderText="Eliminar" DeleteImageUrl="~/Imagenes/Generales/eliminar.png" ShowDeleteButton="true" ItemStyle-Width="10px" >
                                 <ControlStyle Width="30px" />
                                <ItemStyle Width="30px" HorizontalAlign="Center" />
                         
                            </asp:CommandField>

                        </Columns>
                    </asp:GridView>
                </fieldset>

            </div>

            <div runat="server" id="DivMostrarNuevoR" style="width: 100%; height: auto;" visible="false">

                <asp:HiddenField ID="HiddenId" runat="server" />

                <fieldset style="height: auto; border-color: #0C2261;">
                    <legend style="text-align: left; color:darkblue;" runat="server" id="lgNuevoRegistro" visible="false">Nuevo registro</legend>
                        <legend style="text-align: left;  color:darkblue;" runat="server" id="lgModificarRegistro" visible="false">Modificar Registro</legend>

                    <table style="width: 100%;">
                        <tr>
                            <td style="text-align: right;" class="auto-style9">
                                <asp:Label ID="LbId" runat="server" Visible="false"></asp:Label>
                            </td>
                            <td>&nbsp;</td>

                        </tr>
                        <tr>
                            <td style="text-align: right;" class="auto-style9">
                                <asp:Label ID="LbAnio" runat="server" Text="Año:" Style="font-family: Verdana; font-size: small; font-weight: 400;"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="TxtAnio" runat="server" CssClass="TxtGeneral" Width="624px" Height="20px" onkeypress="return soloNumeros(event)" MaxLength="4"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right;" class="auto-style9">
                                <asp:Label ID="LbIVA" runat="server" Text="I. V. A. :" Style="font-family: Verdana; font-size: small; font-weight: 400;" ToolTip="Incluir un 0 antes del punto, ejemplo: 0.16"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="TxtIVA" runat="server" CssClass="TxtGeneral" Width="624px" Height="20px" onkeypress="return numerosDecimalesIva(event, this);" OnTextChanged="TxtIVA_TextChanged"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right;" class="auto-style9">
                                <asp:Label ID="LbNumUmas" runat="server" Text="Número de UMAS:" Style="font-family: Verdana; font-size: small; font-weight: 400;"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="TxtNumUmas" runat="server" AutoPostBack="true" CssClass="TxtGeneral" Width="624px" Height="20px" onkeypress="return soloNumeros(event)" MaxLength="3" OnTextChanged="TxtNumUmas_TextChanged"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right;" class="auto-style11">
                                <asp:Label ID="LbValorUmas" runat="server" Text="Valor de UMA:" Style="font-family: Verdana; font-size: small; font-weight: 400;"></asp:Label>
                            </td>
                            <td class="auto-style12">
                                <asp:TextBox ID="TxtValorUma" runat="server" AutoPostBack="true" CssClass="TxtGeneral" Width="624px" Height="20px" onkeypress="return numerosDecimales(event, this);" OnTextChanged="TxtValorUma_TextChanged"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right;">
                                <asp:Label ID="LbINCPNuevo" runat="server" Text="INCP:" Style="font-family: Verdana; font-size: small; font-weight: 400;"></asp:Label>
                            </td>
                            <td class="auto-style5">
                                <asp:Label Text="$" runat="server"></asp:Label>
                                <asp:Label ID="LbINCP" runat="server" AutoPostBack="true" Style="font-family: Verdana; font-size: small;" Width="625px" ></asp:Label>
                             
                            </td>
                        </tr>
                    </table>


                </fieldset>

                <br />
            </div>
            
                    <table style="width: 100%; margin-top: 10px; text-align: center;">
                        <tr>
                            <td>
                                <asp:Button ID="BtnGuardar" runat="server" CssClass="Boton" OnClick="BtnGuardarNuevoINCP" Text="Guardar" Visible="false" />
                            </td>
                            <td>
                                <asp:Button ID="BtnActualizar" runat="server" CssClass="Boton" OnClick="BtnActualizar_Click" Text="Actualizar" Visible="false" />
                            </td>
                            <td>
                                      <asp:Button ID="BtnLimpiar" runat="server" CssClass="Boton" OnClick="BtnLimpiar_Click" Text="Limpiar" Visible="false"/>
                                </td>
                            <td>
                                    <asp:Button ID="BtnCancelar" runat="server" CssClass="Boton" OnClick="BtnCancelar_Click" Text="Cancelar" Visible="false"  />
                            </td>
                        </tr>
                    </table>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

<asp:Content ID="Content3" runat="server" ContentPlaceHolderID="CPHTitulo">
    <div style="width: auto; text-align: center; ">
        <asp:Label ID="LblLema" runat="server" Text="ÍNDICE NACIONAL DE PRECIOS AL CONSUMIDOR (INCP)" Font-Size="Large" Font-Bold="True" Font-Italic="True" ForeColor="#0C2261"></asp:Label>
    </div>
</asp:Content>
<asp:Content ID="Content4" runat="server" contentplaceholderid="HeadContent">
    <style type="text/css">
        .auto-style1 {
            width: 589px;
        }
        .auto-style3 {
            width: 569px;
        }
        .auto-style5 {
            height: 19px;
        }
        .auto-style8 {
            width: 452px;
        }
        .auto-style9 {
             width: 176px;
        }
        .auto-style11 {
            width: 176px;
            height: 28px;
        }
        .auto-style12 {
            height: 28px;
        }
    </style>
</asp:Content>
