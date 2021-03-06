﻿<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Inventarios1.Master" CodeBehind="frmCatPropiedades.aspx.cs" Inherits="InventariosPJEH.frmCatPropiedades" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

            <div id="bodyContenedorBda;" style="width: auto; height: auto; text-align: center">
                 <asp:HiddenField id="Rowindex" runat="server"></asp:HiddenField>

                <div style="width: auto;">
                    <fieldset style="width: auto; height: auto; border-color: #6D252B;">
                        <legend style="text-align: left;  color:darkblue;">Buscar Propiedades</legend>
                        <table style="width: 100%;">
                           
                            <tr>
                                <td style="text-align: right;">
                                    <asp:Label ID="LbPropiedades" runat="server" Text="Propiedad:" Font-Size="Medium" Style="font-family: Verdana; font-size: small; font-weight: 400; margin-left: 10px;"></asp:Label>
                                </td>
                                <td  class="auto-style3">
                                    <asp:TextBox ID="TxtPropiedadExistente" runat="server" CssClass="auto-style6" Width="429px" Height="20px" onkeypress="return ValidacionLetras(event);"> </asp:TextBox>
   
                                </td>
                                <td class="auto-style4" style="text-align:left">
                                    <asp:Button ID="BtnTipoPartida" runat="server" Text="Buscar" CssClass="Boton" OnClick="BtnBuscar_Click" Style=" align-content: space-around; height: 28px;" />
                                </td>
                                <td style="text-align: left;">
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
                        AutoGenerateColumns="False" OnRowDeleting="GridBuscarPropiedad_RowDeleting"
                        DataKeyNames="IdPropiedad, Propiedad"
                        OnRowCommand="GridBuscarPropiedad_RowCommand" PageSize="25" OnPageIndexChanging="GridBuscar_PageIndexChanging">
                        <Columns>
                            <asp:BoundField DataField="IdPropiedad" HeaderText="Id" ItemStyle-Width="10px" /> 
                            <asp:BoundField DataField="Propiedad" HeaderText="Propiedad" ItemStyle-Width="80px" />
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
                   <legend style="text-align: left; color:darkblue;" runat="server" id="lgNuevoRegistro" visible="false">Nuevo registro</legend>
                        <legend style="text-align: left;  color:darkblue;" runat="server" id="lgModificarRegistro" visible="false">Modificar Registro</legend>

                    <table style="width:100%;">
                        <tr>
                            <td style="text-align: right;" class="auto-style2">
                                <asp:Label ID="LbId" runat="server" Visible="false"></asp:Label>
                            </td>
                            <td>&nbsp;</td>

                        </tr>
                    
                        <tr>
                            <td style="text-align: right;">
                                 <asp:Label ID="LbPropiedadNuevo" runat="server" Text="Propiedad:" Style="font-family: Verdana; font-size: small; font-weight: 400;"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="TxtPropiedad" runat="server" CssClass="TxtGeneral" Width="645px" Height="20px" onkeypress="return ValidacionLetras(event);"></asp:TextBox>
                            </td>
                        </tr>

                    </table>

                </fieldset>

                <br />
            </div>
             <table style="width: 100%; margin-top: 10px; text-align: center;">
                        <tr>
                            <td>
                                <!-- <style="width: 94%; margin-top: 10px; height: 30px; float: left; margin-right: 10%">    -->
                                <asp:Button ID="BtnGuardar" runat="server" CssClass="Boton" OnClick="BtnGuardarNuevo" Text="Insertar" Visible="false" />

                            </td>
                            <td>
                                <asp:Button ID="BtnActualizar" runat="server" CssClass="Boton" OnClick="BtnActualizar_Click" Text="Actualizar" Visible="false" />
                            </td>
                            <td>
                                      <asp:Button ID="BtnLimpiar" runat="server" CssClass="Boton" OnClick="BtnLimpiar_Click" Text="Limpiar" Visible="false"/>
                                </td>
                            <td>
                                <p id="ejemplo">
                                    <asp:Button ID="BtnCancelar" runat="server" CssClass="Boton" OnClick="BtnCancelar_Click" Text="Cancelar" Visible="false"/>
                            </td>
                        </tr>
                    </table>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

<asp:Content ID="Content3" runat="server" ContentPlaceHolderID="CPHTitulo">
    <div style="width: auto; text-align: center; background-color: #d8d8d8;">
        <asp:Label ID="LblLema" runat="server" Text="PROPIEDADES" Font-Size="Large" Font-Bold="True" Font-Italic="True" ForeColor="#6D252B"></asp:Label>
    </div>
</asp:Content>



