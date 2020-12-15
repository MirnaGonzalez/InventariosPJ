<%@ Page Title="" Language="C#" MasterPageFile="~/Inventarios1.Master" AutoEventWireup="true" CodeBehind="frmImpresionEtiquetas.aspx.cs" Inherits="InventariosPJEH.frmImpresionEtiquetas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        .auto-style9 {
            width: 285px;
        }

        .auto-style14 {
            width: 319px;
        }

        .auto-style18 {
            width: 248px;
        }
        .auto-style19 {
            Font-Size: Medium;
            font-family: Verdana;
            font-size: small;
            font-weight: 400;
            border: 1px solid #0c2261;
            color: black;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHTitulo" runat="server">
    <div style="width: auto; text-align: center; background-color: #d8d8d8;">
        <asp:Label ID="LblLema" runat="server" Text="Impresión de etiquetas" Font-Size="Large" Font-Bold="True" Font-Italic="True" ForeColor="#6D252B"></asp:Label>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

            <div id="bodyContenedorBda;" style="width: auto; height: auto; text-align: center">


                <div style="width: auto;">
                </div>


                <div runat="server" id="DivMostrarNuevo" style="width: 100%; height: auto;" visible="true">




                    <fieldset style="height: auto; border-color: #6D252B;" runat="server">

                        <legend style="text-align:left; color: darkblue;" runat="server" id="lgNuevoRegistro" visible="false">Filtros de búsqueda</legend>

                       <table style="width: 100%;">
                              <tr>
                                                                  <td class="auto-style18">

                                    <asp:RadioButton ID="RbDocumento" runat="server" GroupName="Gruptitular" Text="No. Documento" OnCheckedChanged="RbDocumento_CheckedChanged"   AutoPostBack="true" />
                                </td>

                                <td class="auto-style18">

                                    <asp:RadioButton  ID="RbInventario"  GroupName="Gruptitular" runat="server"  Text="No. Inventario"  OnCheckedChanged="RbInventario_CheckedChanged" AutoPostBack="true" />
                                </td>
                                <td class="auto-style8">

                                    <asp:RadioButton ID="RbResguardo" runat="server" GroupName="Gruptitular"  Text="No. Resguardo" OnCheckedChanged="RbResguardo_CheckedChanged"  AutoPostBack="true" />
                                </td>
                                <td class="auto-style8">

                                    <asp:RadioButton ID="RbArea" runat="server" GroupName="Gruptitular" Text="Área administrativa"  OnCheckedChanged="RbArea_CheckedChanged"   AutoPostBack="true"/>
                                </td>
                            </tr>
                       </table>



                        <table style="width: 100%;">

                         


                            <tr runat="server" id="GrupoInventario" visible="false">
                                <td class="auto-style14" style="text-align: right;">
                                    <asp:Label ID="LblNoInventario" runat="server" Text="No.Inventario: " CssClass="LabelGeneral" Visible="true"></asp:Label>
                                </td>
                                <td style="text-align: left;">
                                    <asp:TextBox ID="TxtNoInventario" runat="server" CssClass="auto-style19" onkeypress="return soloNumeros(event)"  Width="402px"></asp:TextBox>

                                </td>
                                                            </tr>
                            <tr runat="server" id="GrupoDocumento" visible="false">
                                <td class="auto-style14" style="text-align: right;">
                                    <asp:Label ID="LblNoDocumento" runat="server" Text="No.Documento" CssClass="LabelGeneral" Visible="true"></asp:Label>
                                </td>
                                <td style="text-align: left;">
                                    <asp:TextBox ID="TxtDocumento" runat="server" CssClass="TxtGeneral" onkeypress="return soloNumeros(event)"></asp:TextBox>
                                </td>

                            </tr>
                            <tr runat="server" id="GrupoResguardo" visible="false">
                                <td class="auto-style14" style="text-align: right;">
                                    <asp:Label ID="LblNoResguardo" runat="server" Text="No.Resguardo" CssClass="LabelGeneral" Visible="true"></asp:Label>
                                </td>
                                <td style="text-align: left;">
                                    <asp:TextBox ID="TxtNoResguardo" runat="server" CssClass="TxtGeneral" onkeypress="return soloNumeros(event)" MaxLength="4"></asp:TextBox>
                                </td>

                            </tr>

                            <tr runat="server" id="GrupoArea" visible="false">
                                <td class="auto-style14" style="text-align: right;">
                                    <asp:Label ID="LblTipoArea" runat="server" Visible="true" Text="Tipo de área"></asp:Label>
                                </td>
                                <td style="text-align: left;">
                                    <asp:DropDownList ID="DdlTipoArea" runat="server" AutoPostBack="true" CssClass="DropGeneral" OnSelectedIndexChanged="ClasificacionSeleccionado"  Style="margin-top: 10px;  ">
                                                                           </asp:DropDownList>
                                </td>
                                                            </tr>
                                               <tr runat="server" id="GrupoDistrito" visible="false">
                                <td class="auto-style14" style="text-align: right;">
                                    <asp:Label ID="LblDistrito" runat="server"  Visible="true" Text="Distrito"></asp:Label>
                                </td>
                                <td style="text-align: left;">
                                    <asp:DropDownList ID="DdlDistrito" runat="server" AutoPostBack="true" CssClass="DropGeneral"  OnSelectedIndexChanged="DdlDistrito_SelectedIndexChanged"  Style="margin-top: 10px;  ">
                                                                           </asp:DropDownList>
                                </td>
                                                            </tr>
                                                        <tr runat="server" id="GrupoUnidad" visible="false">
                                <td class="auto-style14" style="text-align: right;">
                                    <asp:Label ID="LblUnidad" runat="server" Visible="true" Text="Unidad administrativa"></asp:Label>
                                </td>
                                <td style="text-align: left;">
                                    <asp:DropDownList ID="DdlUnidad" runat="server" AutoPostBack="true" CssClass="DropGeneral" Style="margin-top: 10px;  ">
                                                                           </asp:DropDownList>
                                </td>
                                                            </tr>


                        </table>

                        <table style="width: 100%;">



                            <tr runat="server" id="GrupoBotones" visible="false">
                                <td class="auto-style18">
                                    <asp:Button ID="BtnGenerar" runat="server" CssClass="Boton"  Text="Generar reporte" OnClick="BtnGenerar_Click" Visible="true" />
                                </td>
                                <td class="auto-style18">
                                    <asp:Button ID="BtnLimpiar" runat="server" CssClass="Boton"  Text="Limpiar" OnClick="BtnLimpiar_Click" Visible ="true" Width="120px"  />
                                </td>
                               
                            </tr>

                        </table>



                    </fieldset>
                </div>
                <table style="width: 100%; margin-top: 10px;">
                   
                </table>
                <br />

            </div>

            </div>

        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>
