<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Inventarios1.Master" CodeBehind="frmCatEdNiSec.aspx.cs" Inherits="InventariosPJEH.frmCatEdNiSec" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

            <div id="DivFiltrosBusqueda;" style="width: auto; height: auto; text-align: center">
                <asp:HiddenField ID="Rowindex" runat="server"></asp:HiddenField>
                <asp:HiddenField ID="HiddenMunicipio" runat="server"></asp:HiddenField>
                <div style="width: auto;">
                    <fieldset style="width: auto; height: auto; border-color: #6D252B;">
                        <legend style="text-align: left; color: darkblue;">Buscar edificios </legend>
                        <table class="auto-style8">
                            <tr>
                                <td style="text-align: right;" class="auto-style3">
                                    <asp:Label ID="LbMunicipiosBuscar" runat="server" Text="Municipio:" Font-Size="Medium" Style="font-family: Verdana; font-size: small; font-weight: 400; margin-left: 10px;"></asp:Label>
                                </td>
                                <td class="auto-style3" style="text-align: left;">
                                    <asp:DropDownList ID="ddlMunicipio" runat="server" AutoPostBack="true" Width="600px" OnSelectedIndexChanged="BuscarEdificio"></asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: right;" class="auto-style3">
                                    <asp:Label ID="LbEdificiosBuscar" runat="server" Text="Edificio:" Font-Size="Medium" Style="font-family: Verdana; font-size: small; font-weight: 400; margin-left: 10px;"></asp:Label>
                                </td>
                                <td class="auto-style3" style="text-align: left;">
                                    <asp:DropDownList ID="ddlEdificio" runat="server" AutoPostBack="true" Width="600px" ></asp:DropDownList>
                                </td>
                            </tr>
                        </table>
                        <table style="width: 100%;">
                            <tr>
                                <td class="auto-style9">
                                    <asp:Button ID="BtnBuscar" runat="server" Text="Buscar" CssClass="Boton" OnClick="BtnBuscar_Click" Style="align-content: space-around; height: 28px;" />
                                </td>
                                <td>
                                    <asp:Button ID="BtnMostrarNuevoR" runat="server" Text="Agregar edificio" CssClass="Boton" OnClick="BtnMostraOcultar" Style="align-content: space-around;" Height="28px" Width="168px" />
                                </td>
                            </tr>
                       </table>
                </div>
                </fieldset>
            </div>


            <div runat="server" id="DivEdificios" visible="false" class="auto-style6">

                <fieldset style="height: auto">
                    <legend style="text-align: center; color: darkblue;" runat="server" id="Legend4" visible="true">Edificios</legend>

                    <asp:GridView ID="GridEdificios" CssClass="StyleGridV" runat="server" Height="100px" Width="100%"
                        AutoGenerateColumns="False" 
                        DataKeyNames="IdEdificio, Edificio,Calle,Colonia, CP, IdMunicipio"
                        OnRowCommand="GridBuscarEdificios_RowCommand" HorizontalAlign="Center"
                        OnSelectedIndexChanging="GridBuscar_SelectedIndexChanging">
                        <Columns>

                            <asp:BoundField DataField="IdEdificio" HeaderText="Clave" ItemStyle-Width="20px" >
                               <ItemStyle Width="20px" />   </asp:BoundField>
                            <asp:BoundField DataField="Edificio" HeaderText="Edificio" ItemStyle-Width="40px" >
                                 <ItemStyle Width="60px" />   </asp:BoundField>
                            <asp:BoundField DataField="Calle" HeaderText="Calle" ItemStyle-Width="40px" >
                                 <ItemStyle Width="60px" />   </asp:BoundField>
                            <asp:BoundField DataField="Colonia" HeaderText="Colonia" ItemStyle-Width="40px" >
                                 <ItemStyle Width="60px" />   </asp:BoundField>
                            <asp:BoundField DataField="CP" HeaderText="CP" ItemStyle-Width="40px" >
                                 <ItemStyle Width="60px" />   </asp:BoundField>
                            <asp:BoundField DataField="IdMunicipio" HeaderText="clave municipio"  Visible="false" >
                            <ItemStyle Width="60px" />  </asp:BoundField>
                            <asp:BoundField DataField="Municipio" HeaderText="Municipio" ItemStyle-Width="80px" >
                            <ItemStyle Width="60px" />  </asp:BoundField>
                            
                            <asp:ButtonField ButtonType="Image" ImageUrl="~/Imagenes/Generales/editar.png" ControlStyle-Width="30px" HeaderText="Editar" Text="Editar"
                                CommandName="Editar" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10px">
                                <ControlStyle Width="30px" />
                                <ItemStyle HorizontalAlign="Center" Width="30px" />
                            </asp:ButtonField>

                            <asp:ButtonField ButtonType="Image" ImageUrl="~/Imagenes/Generales/eliminar.png" ControlStyle-Width="30px" HeaderText="Eliminar" Text="Eliminar"
                                  CommandName="Eliminar" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10px" >
                                <ControlStyle Width="30px" />
                                <ItemStyle HorizontalAlign="Center" Width="30px" />
                            </asp:ButtonField>

                            <asp:ButtonField ButtonType="Image" ImageUrl="~/Imagenes/Generales/agregar.png" ControlStyle-Width="30px" HeaderText="Agregar" Text="Agregar"
                                CommandName="Agregar" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10px">
                                <ControlStyle Width="30px" />
                                <ItemStyle HorizontalAlign="Center" Width="30px" />
                            </asp:ButtonField>

                          

                            <asp:ButtonField ButtonType="Image" ImageUrl="~/Imagenes/Generales/consultar.png" ControlStyle-Width="30px" HeaderText="Consultar Niveles" Text="Consultar"
                                CommandName="Consultar" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10px">
                                <ControlStyle Width="30px" />
                                <ItemStyle HorizontalAlign="Center" Width="30px" />
                            </asp:ButtonField>
                            
                    
                        </Columns>
                    </asp:GridView>
                    
                </fieldset>
                <table style="width: 100%;">
                    <tr>
                        <td style="text-align: center;">
                             <asp:Button ID="btnOcultarEdificios" runat="server" CssClass="Boton" OnClick="OcultarEdificios_Click" Text="Ocultar edificios" Visible="false" Width="162px" />
                        </td>
                    </tr>
               </table>
                      

            </div>

            <div runat="server" id="DivNuevoEdificio" style="width: 100%; height: auto;" visible="false">

                <asp:HiddenField ID="HiddenIdEdificio" runat="server" />
                <asp:HiddenField ID="HiddenEdificioNuevo" runat="server" />
                <fieldset style="height: auto; border-color: #6D252B;">
                    <legend style="text-align: left; color: darkblue;" runat="server" id="lgNuevoRegistro" visible="false">Nuevo edificio</legend>
                    <legend style="text-align: left; color: darkblue;" runat="server" id="lgModificarRegistro" visible="false">Modificar edificio</legend>


                    <table style="margin-top: 15px; margin: 0 auto; width: 70%;">
                        <tr>
                            <td>
                                <asp:Label id="lbIdEdificios" runat="server"  Visible="false"> </asp:Label>
                            </td>
                      
                        </tr>
                        
                    </table>

                    <table style="margin: 0 auto; width: 70%;">
                         <tr>
                               <td style="text-align: right;" class="auto-style5">
                                <asp:Label ID="LbEdificioNuevo" runat="server" Text="Nombre del edificio:" Style="font-family: Verdana; font-size: small;"></asp:Label>

                            </td>
                            <td class="auto-style42">
                                <asp:TextBox ID="TxtEdificioNuevo" runat="server" CssClass="TxtGeneral" Height="20px" onkeypress="return ValidacionLetras(event);" Width="473px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                               <td style="text-align: right;" class="auto-style5">
                                <asp:Label ID="LbCalleNuevo" runat="server" Text="Calle:" Style="font-family: Verdana; font-size: small;"></asp:Label>

                            </td>
                            <td class="auto-style42">
                                <asp:TextBox ID="TxtCalleNuevo" runat="server" CssClass="TxtGeneral" Height="20px"  Width="473px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right;" class="auto-style5">
                                <asp:Label ID="LbColoniaNuevo" runat="server" Text="Colonia:" Style="font-family: Verdana; font-size: small;"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="TxtColoniaNuevo" runat="server" CssClass="TxtGeneral" Height="20px"  Width="473px"></asp:TextBox>
                            </td>
                        </tr>
                         <tr>
                            <td style="text-align: right;" class="auto-style5">
                                <asp:Label ID="LbCPNuevo" runat="server" Text="Código postal:" Style="font-family: Verdana; font-size: small;"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="TxtCodigoPostalNuevo" runat="server" CssClass="TxtGeneral" Height="20px" onkeypress="return soloNumeros(event)" MaxLength="5" Width="473px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align:right;" class="auto-style5">
                                <asp:Label ID="LbMunicipio" runat="server" Text="Municipio:" Style="font-family: Verdana; font-size: small;"></asp:Label>
                            </td>
                            <td class="auto-style1">
                                <asp:DropDownList ID="DropMunicipioNuevo" runat="server" AutoPostBack="true" Width="478px" Height="19px"> </asp:DropDownList>
                            </td>
                         
                        </tr>
                       
                    </table>

                </fieldset>

                <br />
            

      
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

          </div>

           <div runat="server" id="DivNiveles" style="width: 100%;" visible="false">

                <fieldset  style="height: auto; ">
                    <legend style="text-align: center; " runat="server" id="LgEdiNivel" visible="false" class="auto-style4">Niveles</legend>

                    <asp:GridView ID="GridNiveles" CssClass="StyleGridV" runat="server" Height="100px" Width="100%" Visible="false"
                        AutoGenerateColumns="False" OnRowDeleting="GridBuscarNiveles_RowDeleting"
                        DataKeyNames="IdNivel, Edificio, Nivel,IdEdificio"
                        OnRowCommand="GridBuscarNiveles_RowCommand" HorizontalAlign="Center"  OnSelectedIndexChanging="GridBuscar2_SelectedIndexChanging">
                        <Columns>

                            <asp:BoundField DataField="IdNivel" HeaderText="Clave nivel" ItemStyle-Width="20px" >
                               <ItemStyle Width="20px" />   </asp:BoundField>
                            <asp:BoundField DataField="IdEdificio" HeaderText="Id Edificio" ItemStyle-Width="20px" visible="false">
                               <ItemStyle Width="20px" />   </asp:BoundField>           
                            <asp:BoundField DataField="Edificio" HeaderText="Edificio" ItemStyle-Width="40px" >
                                 <ItemStyle Width="60px" />   </asp:BoundField>

                            <asp:BoundField DataField="Nivel" HeaderText="Nivel" ItemStyle-Width="30px">
                             <ItemStyle Width="30px" />   </asp:BoundField>              
                            

                            <asp:ButtonField ButtonType="Image" ImageUrl="~/Imagenes/Generales/editar.png" ControlStyle-Width="30px" HeaderText="Editar" Text="Editar"
                                CommandName="Prueba" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10px">
                                <ControlStyle Width="30px" />
                                <ItemStyle HorizontalAlign="Center" Width="30px" />
                            </asp:ButtonField>

                            <asp:CommandField ButtonType="Image" HeaderText="Eliminar" DeleteImageUrl="~/Imagenes/Generales/eliminar.png" ShowDeleteButton="true" >
                                <ControlStyle Width="30px" />
                                <ItemStyle Width="30px" HorizontalAlign="Center" />
                            </asp:CommandField>

                            <asp:ButtonField ButtonType="Image" ImageUrl="~/Imagenes/Generales/agregar.png" ControlStyle-Width="30px" HeaderText="Agregar sección" Text="Agregar"
                                CommandName="Agregar" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10px">
                                <ControlStyle Width="30px" />
                                <ItemStyle HorizontalAlign="Center" Width="30px" />
                            </asp:ButtonField>

                            <asp:ButtonField ButtonType="Image" ImageUrl="~/Imagenes/Generales/consultar.png" ControlStyle-Width="30px" HeaderText="Consultar secciones " Text="Consultar"
                                CommandName="Consultar" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10px">
                                <ControlStyle Width="30px" />
                                <ItemStyle HorizontalAlign="Center" Width="30px" />
                            </asp:ButtonField>
                            
                        </Columns>
                    </asp:GridView>
                <table style="width: 100%;">
                    <tr>
                        <td style="text-align: center;">
                             <asp:Button ID="btnOcultarNiveles" runat="server" CssClass="Boton" OnClick="OcultarNiveles_Click" Text="Ocultar niveles" Visible="false" Width="162px" />
                        </td>
                    </tr>

                </table>
            </div>

            <div runat="server" id="DivNuevoNivel" visible="false" class="auto-style7">

                <asp:HiddenField ID="Rowindex2" runat="server" />
                <asp:HiddenField ID="HiddenIdNivel" runat="server" />
                <asp:HiddenField ID="HiddenEdificio" runat="server" />
                <fieldset style="height: auto; border-color: #6D252B;">
                    <legend style="text-align: left; color: darkblue;" runat="server" id="lgNuevoRegistro2" visible="false">Nuevo nivel</legend>
                    <legend style="text-align: left; color: darkblue;" runat="server" id="lgModificarRegistro2" visible="false">Modificar nivel</legend>


                    <table style="margin-top: 15px; margin: 0 auto; width: 70%;">
                        <tr>
                            <td>
                                <asp:Label id="LbIdNiveles" runat="server"  Visible="false"> </asp:Label>
                            </td>
                          
                        </tr>
                        
                    </table>

                    <table style="margin: 0 auto; width: 70%;">
                         <tr>
                               <td style="text-align: right;" class="auto-style2">
                                <asp:Label ID="LbNombreNivelNuevo" runat="server" Text="Nombre del nivel:" Style="font-family: Verdana; font-size: small;"></asp:Label>

                            </td>
                            <td class="auto-style2">
                                <asp:TextBox ID="TxtNivelNuevo" runat="server" CssClass="TxtGeneral" Height="20px" onkeypress="return ValidacionLetras(event);" Width="473px"></asp:TextBox>
                            </td>

                        </tr>
                        <tr>
                             <td style="text-align: right;" class="auto-style2">
                                <asp:Label ID="LbEdificio" runat="server" Text="Edificio:" Style="font-family: Verdana; font-size: small;"></asp:Label>

                            </td>
                            <td class="auto-style2">
                                <asp:DropDownList ID="DropEdificiosNuevo" runat="server" AutoPostBack="true"  Width="478px" Height="20px"></asp:DropDownList>
                            </td>
                        </tr>

                    </table>

                </fieldset>

                <br />
      



            

            <table style="width: 100%; margin-top: 10px; text-align: center;">
                <tr>
                    <td>
                        <asp:Button ID="btnGuardarNivel" runat="server" CssClass="Boton" OnClick="GuardarNivel_Click" Text="Guardar" Visible="false" />

                    </td>
                    <td>
                        <asp:Button ID="BtnActualizar2" runat="server" CssClass="Boton" OnClick="BtnActualizarNivel_Click" Text="Actualizar" Visible="false" />
                    </td>
                 
                    
                </tr>
            </table>

 </div>
            
             <div runat="server" id="DivSecciones" style="width: 100%;" visible="false">

                <fieldset style="height: auto; ">
                    <legend style="text-align: center; color: darkblue;" runat="server" id="LgEdiNivelSeccion" visible="true">Secciones</legend>

                    <asp:GridView ID="GridSecciones" CssClass="StyleGridV" runat="server" Height="100px" Width="100%"
                        AutoGenerateColumns="False" OnRowDeleting="GridBuscarSecciones_RowDeleting"
                        DataKeyNames="IdNivel, IdEdificio,  IdSeccion, Edificio, Nivel,  Nombre,Tipo, IdENSU"
                        OnRowCommand="GridBuscarSecciones_RowCommand" HorizontalAlign="Center" >
                        <Columns>

                            <asp:BoundField DataField="IdNivel" HeaderText="Id Nivel" ItemStyle-Width="20px" visible="false">
                               <ItemStyle Width="20px" />   </asp:BoundField>
                            <asp:BoundField DataField="IdEdificio" HeaderText="Id Edificio" ItemStyle-Width="20px" visible="false">
                               <ItemStyle Width="20px" />   </asp:BoundField>
                            <asp:BoundField DataField="IdSeccion" HeaderText="Clave Secciones" ItemStyle-Width="20px" >
                               <ItemStyle Width="20px" />   </asp:BoundField>
                            <asp:BoundField DataField="Edificio" HeaderText="Edificio" ItemStyle-Width="40px" >
                                 <ItemStyle Width="60px" />   </asp:BoundField>
                            <asp:BoundField DataField="Nivel" HeaderText="Nivel" ItemStyle-Width="30px">
                             <ItemStyle Width="30px" />   </asp:BoundField>  
                            <asp:BoundField DataField="Nombre" HeaderText="Sección" ItemStyle-Width="30px">
                             <ItemStyle Width="30px" />   </asp:BoundField>  
                           <asp:TemplateField HeaderText="Tipo" SortExpression="Tipo">
                                    <ItemTemplate>
                                        <asp:Label ID="Label5" runat="server" Text='<%# ((string)Eval("Tipo") =="T") ? "TRABAJO" : "RESGUARDO" %>'></asp:Label>
                                    </ItemTemplate>
                               <ItemStyle Width="30px" /> 
                                </asp:TemplateField>

                            <asp:BoundField DataField="IdENSU" HeaderText="Clave Ubicación" ItemStyle-Width="40px">
                                 <ItemStyle Width="60px" />   </asp:BoundField>
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

                  <table style="width: 100%;">
                    <tr>
                        <td style="text-align: center;">
                             <asp:Button ID="btnOcultarSecciones" runat="server" CssClass="Boton" OnClick="OcultarSecciones_Click" Text="Ocultar secciones" Visible="false" Width="162px" />
                        </td>
                    </tr>

                </table>

            </div>

            <div runat="server" id="DivNuevaSeccion" style="width: 100%; height: auto;" visible="false">

                <asp:HiddenField ID="Rowindex3" runat="server" />
                <asp:HiddenField ID="HiddenIdENSU" runat="server" />

                <fieldset style="height: auto; border-color: #6D252B;">
                    <legend style="text-align: left; color: darkblue;" runat="server" id="lgNuevaSeccion" visible="false">Nueva sección</legend>
                    <legend style="text-align: left; color: darkblue;" runat="server" id="lgModificarRegistro3" visible="false">Modificar sección</legend>


                    <table style="margin-top: 15px; margin: 0 auto; width: 70%;">
                        <tr>
                            <td>
                                <asp:Label id="LbIdSecciones" runat="server"  Visible="false"> </asp:Label>
                            </td>
                          <td>
                                <asp:Label id="LbIdENSU" runat="server"  Visible="false"> </asp:Label>
                            </td>
                        </tr>
                        
                    </table>

                    <table style="margin: 0 auto; width: 70%;">
                         <tr>
                               <td style="text-align: right;" class="auto-style2">
                                <asp:Label ID="LbSeccionesNuevo" runat="server" Text="Nombre de sección:" Style="font-family: Verdana; font-size: small;"></asp:Label>

                            </td>
                            <td class="auto-style2">
                                <asp:TextBox ID="TxtSeccionesNuevo" runat="server" CssClass="TxtGeneral" Height="20px" onkeypress="return ValidacionLetras(event);" Width="473px"></asp:TextBox>
                            </td>
                        </tr>    
         
                        <tr>
                            <td style="text-align:right;" class="auto-style1">
                                <asp:Label ID="LbTipoNuevo" runat="server" Text="Tipo:" Style="font-family: Verdana; font-size: small;"></asp:Label>
                            </td>
                            <td class="auto-style1">
                                <asp:DropDownList ID="DropTipoNuevo" runat="server" AutoPostBack="true" Width="478px" Height="19px"> 
                                   <asp:ListItem Text="Seleccionar"></asp:ListItem>
                                        <asp:ListItem Value="T">TRABAJO</asp:ListItem>
                                        <asp:ListItem Value="R">RESGUARDO</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                         
                        </tr>
                       
                    </table>

                </fieldset>

                <br />
           
            <table style="width: 100%; margin-top: 10px; text-align: center;">
                <tr>
                    <td>
                        <asp:Button ID="btnGuadarSeccion" runat="server" CssClass="Boton" OnClick="GuardarSecciones_Click" Text="Guardar" Visible="false" />

                    </td>
                    <td>
                        <asp:Button ID="BtnActualizar3" runat="server" CssClass="Boton" OnClick="BtnActualizarSecciones_Click" Text="Actualizar" Visible="false" />
                    </td>

                    <td>
                            <asp:Button ID="BtnCancelar" runat="server" CssClass="Boton" OnClick="BtnCancelar_Click" Text="Cancelar" Visible="false" />
                    </td>
                </tr>
            </table>

      </div>
              <table style="width: 100%; margin-top: 10px;">
                            <tr>
                                <td aria-orientation="vertical">

                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </input>

                                    <input onclick="window.location.reload()" type="button" value="Limpiar" class="Boton" dir="ltr"></input></td>
                            </tr>
                 </table>


        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

<asp:Content ID="Content3" runat="server" ContentPlaceHolderID="CPHTitulo">
    <div style="width: auto; text-align: center; background-color: #d8d8d8;">
        <asp:Label ID="LblLema" runat="server" Text="EDIFICIOS - NIVELES - SECCIONES" Font-Size="Large" Font-Bold="True" Font-Italic="True" ForeColor="#6D252B"></asp:Label>
    </div>
</asp:Content>

<asp:Content ID="Content4" runat="server" contentplaceholderid="HeadContent">
    <style type="text/css">
        .auto-style1 {
            width: 92px;
        }
        .auto-style2 {
            width: 168px;
        }
        .auto-style3 {
            width: 172px;
        }
        .auto-style4 {
            color: darkblue;
        }
        .auto-style5 {
            width: 135px;
        }
        .auto-style6 {
            width: 100%;
        }
        .auto-style7 {
            width: 100%;
            height: auto;
        }
        .auto-style8 {
            width: 903px;
            height: 20px;
        }
        .auto-style9 {
            height: 20px;
        }
    </style>
</asp:Content>


