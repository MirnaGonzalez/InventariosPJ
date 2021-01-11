<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Inventarios1.Master" CodeBehind="frmCatProveedores.aspx.cs" Inherits="InventariosPJEH.frmCatProveedores" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

            <div id="bodyContenedorBda;" style="width: auto; height: auto; text-align: center">
                <asp:HiddenField id="Rowindex" runat="server"></asp:HiddenField>


                <fieldset style="width: auto; border-color: #0c2261;">
                    <legend style="text-align: left; color:darkblue;">Buscar registro</legend>

                    <table style="width: 100%;">                   
                        <tr>
                            <td style="text-align: right;">
                                <asp:Label ID="LabelClasificacion" runat="server" Text="Nombre del proveedor: " Font-Size="Medium" Style="font-family: Verdana; font-size: small; font-weight: 400; margin-left: 5px;"></asp:Label>
                            </td>
                            <td style="text-align: left;" class="auto-style7">
                               <asp:TextBox ID="TxtProveedorExistente" runat="server" CssClass="auto-style6" Width="429px" Height="20px" onkeypress="return ValidacionLetras(event);"></asp:TextBox>
                            </td>
                            <td style="text-align: left;" >
                                <asp:Button ID="BtnProveedor" runat="server" Text="Buscar" CssClass="Boton" OnClick="BtnBuscar_Click" Style="align-content: space-around; height: 28px;" />
                            </td>
                            <td style="text-align: right;" >
                                <asp:Button ID="BtnMostrarNuevoR" runat="server" Text="Nuevo Registro" CssClass="Boton" OnClick="BtnMostraOcultar" Style=" align-content: space-around; " Height="28px" Width="168px" />
                            </td>
                        </tr>

                    </table>
                </fieldset>

            </div>

            <div runat="server" id="DivTabla" style="width: 100%;" visible="false">

                <fieldset style="height: auto">

                    <asp:GridView ID="GridBuscarProveedor" CssClass="StyleGridV" runat="server" Height="100px" Width="100%"
                        AutoGenerateColumns="False" OnRowDeleting="GridBuscarProveedor_RowDeleting"
                        DataKeyNames="IdProveedor,Proveedor,Calle,Colonia,Estado,Municipio,Telefono,email,CP"
                        OnRowCommand="GridBuscarProveedor_RowCommand" PageSize="25" OnPageIndexChanging="GridBuscarProveedor_PageIndexChanging">
                        <Columns>

                            <asp:BoundField DataField="IdProveedor" HeaderText="Id Proveedor" Visible="false" />
                            <asp:BoundField DataField="Proveedor" HeaderText="Proveedor" />
                            <asp:BoundField DataField="Calle" HeaderText="Calle" />
                            <asp:BoundField DataField="Colonia" HeaderText="Colonia" />
                            <asp:BoundField DataField="Estado" HeaderText="Estado" />
                            <asp:BoundField DataField="Municipio" HeaderText="Municipio" DataFormatString="" />
                            <asp:BoundField DataField="Telefono" HeaderText="Teléfono" />
                            <asp:BoundField DataField="email" HeaderText="Correo electrónico" />
                            <asp:BoundField DataField="CP" HeaderText="Código postal" Visible="false" />
                            <asp:ButtonField ButtonType="Image" ImageUrl="~/Imagenes/Generales/editar.png" ControlStyle-Width="30px" HeaderText="Editar" Text="Editar"
                                CommandName="Prueba" ItemStyle-HorizontalAlign="Center" ItemStyle-Height="20px" ItemStyle-Width="30px" />
                            <asp:CommandField ButtonType="Image" HeaderText="Eliminar" DeleteImageUrl="~/Imagenes/Generales/eliminar.png" ShowDeleteButton="true">
                            <ControlStyle Width="30px" />
                            </asp:CommandField>
                        </Columns>
                    </asp:GridView>
                </fieldset>

               

            </div>

            <div runat="server" id="DivMostrarNuevoR" style="width: 100%; height: auto;" visible="false">

                <table style="width: 100%;">
                    <tr>
                        <td style="text-align: right;">
                            <asp:HiddenField ID="HiddenIdProveedor" runat="server" />
                        </td>
                        <td>
                            <asp:HiddenField ID="HiddenProveedor" runat="server" />
                        </td>

                    </tr>

                </table>


                <fieldset style="height: auto; border-color: #0c2261;"  runat="server">
                    <legend style="text-align:left; color:darkblue;" runat="server" id="lgNuevoRegistro" visible="false">Nuevo registro</legend>
                        <legend style="text-align: left;  color:darkblue;" runat="server" id="lgModificarRegistro" visible="false">Modificar Registro</legend>
                    <table style="width: 100%;">
                        <tr>
                            <td class="auto-style9">
                                <asp:Label ID="LbId" runat="server" Visible="false" Text="id" Style="font-family: Verdana; font-size: small; margin-left: 115px;  visibility: hidden"></asp:Label>
                            </td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td style="text-align: right;" class="auto-style9">
                                <asp:Label ID="LbProveedor" runat="server" Text="Proveedor :" Style="font-family: Verdana; font-size: small; margin-left: 115px; font-weight: 400;"></asp:Label>
                            </td>
                            <td style="text-align: left;">
                                <asp:TextBox ID="TxtProveedor" runat="server" CssClass="auto-style6" Width="546px" Height="18px" ></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right;" class="auto-style9">
                                <asp:Label ID="LbEstado" runat="server" Text="Estado: " Style="font-family: Verdana; font-size: small; font-weight: 400; margin-left: 142px;"></asp:Label>
                            </td>
                            <td style="text-align: left;">
                                <asp:DropDownList ID="DropEstados" runat="server" AutoPostBack="true" CssClass="DropGeneral" Width="552px" Height="28px" OnSelectedIndexChanged="DropMunicipio_SelectedIndexChanged"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right;" class="auto-style9">
                                <asp:Label ID="LbMunicipio" runat="server" Text="Municipio: " Style="font-family: Verdana; font-size: small; font-weight: 400; margin-left: 127px"></asp:Label>
                            </td>
                            <td style="text-align: left;">
                                <asp:DropDownList ID="DropMunicipio" runat="server" AutoPostBack="true" CssClass="DropGeneral" Width="553px" Height="29px"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right;" class="auto-style9">
                                <asp:Label ID="LbCalle" runat="server" Text="Calle: " Style="font-family: Verdana; font-size: small;  margin-left: 157px"></asp:Label>
                            </td>
                            <td style="text-align: left;">
                                <asp:TextBox ID="TxtCalle" runat="server" CssClass="auto-style6" Width="546px" Height="20px" onkeypress="return ValidacionLetras(event);"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right;" class="auto-style9">
                                <asp:Label ID="LbColonia" runat="server" Text="Colonia:  " Style="font-family: Verdana; font-size: small; font-weight: 400; margin-left: 142px"></asp:Label>
                            </td>
                            <td style="text-align: left;">
                                <asp:TextBox ID="TxtColonia" runat="server" CssClass="auto-style6" Width="546px" Height="20px" onkeypress="return ValidacionLetras(event);"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right;" class="auto-style9">
                                <asp:Label ID="LbCorreoElectronico" runat="server" Text="Correo electrónico:  " Style="font-family: Verdana; font-size: small; font-weight: 400;"></asp:Label>
                            </td>
                            <td style="text-align: left;">
                                <asp:TextBox ID="TxtCorreoElectronico" runat="server" CssClass="TxtGeneral" Width="550px" Height="20px" placeholder="Correo Electrónico"></asp:TextBox>
                                 <span id="emailOK"></span>
                                <a id='resultado' runat="server"></a>
                            </td>
                        </tr>

                    </table>

                    <table style="width: 100%;">
                        <tr>
                            <td style="text-align:right ;" class="auto-style7">
                                <asp:Label ID="LbCodigoPostal" runat="server" Text="Código Postal: " Style="font-family: Verdana; font-size: small; font-weight: 400;"></asp:Label>
                            </td>
                            <td class="auto-style4">
                                <asp:TextBox ID="TxtCodigoPostal" runat="server" CssClass="auto-style5" Width="201px" Height="20px"  onkeypress="return soloNumeros(event)" MaxLength="5"></asp:TextBox>
                            </td>
                            <td style="text-align: right;" class="auto-style1">
                                <asp:Label ID="LbTelefono" runat="server" Style="font-family: Verdana; font-size: small; font-weight: 400;" Text="Teléfono: "></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="TxtTelefono"  runat="server" CssClass="auto-style6" Width="204px" Height="20px" MaxLength="10"  onkeypress="return soloNumeros(event)" ></asp:TextBox>
                            </td>
                        </tr>

                    </table>

                </fieldset>

                <br />
            </div>
             <table style="margin-top: 10px; text-align: center;" class="auto-style10">
                        <tr>
                            <td>
                                <!-- <style="width: 94%; margin-top: 10px; height: 30px; float: left; margin-right: 10%">    -->
                                <asp:Button ID="BtnGuardar" runat="server" CssClass="Boton" OnClick="BtnGuardarNuevoProvedor" Text="Guardar" Visible="false" />

                            </td>
                            <td>
                                <asp:Button ID="BtnActualizar" runat="server" CssClass="Boton" OnClick="BtnActualizar_Click" Text="Actualizar" Visible="false" />
                            </td>
                            <td>
                                      <asp:Button ID="BtnLimpiar" runat="server" CssClass="Boton" OnClick="BtnLimpiar_Click" Text="Limpiar" Visible="false"/>
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
        <asp:Label ID="LblLema" runat="server" Text="PROVEEDORES" Font-Size="Large" Font-Bold="True" Font-Italic="True" ForeColor="#0C2261"></asp:Label>
    </div>
</asp:Content>




<asp:Content ID="Content4" runat="server" contentplaceholderid="HeadContent">
    <style type="text/css">
        .auto-style1 {
            width: 105px;
        }
        .auto-style4 {
            width: 233px;
        }
        .auto-style5 {
            Font-Size: Medium;
            font-family: Verdana;
            font-size: small;
            font-weight: 400;
            border: 1px solid #0c2261;
            margin-left: 4px;
        }
        .auto-style6 {
            Font-Size: Medium;
            font-family: Verdana;
            font-size: small;
            font-weight: 400;
            border: 1px solid #0c2261;
        }
        .auto-style7 {
            width: 231px;
        }
        .auto-style9 {
            width: 236px;
        }
        .auto-style10 {
            width: 100%;
        }
    </style>
</asp:Content>





