<%@ Page Title="" Language="C#" MasterPageFile="~/Inventarios1.Master" AutoEventWireup="true" CodeBehind="frmBajaBienSoporte.aspx.cs" Inherits="InventariosPJEH.frmBajaBienSoporte" EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManagerRegistro" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="RecibirBien" runat="server">
            
            <ContentTemplate>
                <div id="bodyContenedorMantenimiento">
                 <asp:HiddenField id="Rowindex" runat="server"></asp:HiddenField>
                    <%-- Interfaz Baja de Soporte  --%>                    
                    <div id="SinReparacion" runat="server" visible ="true">
                       <fieldset style="border-color:black; width:95%; margin-left:11px;">
                           <legend style="width:auto; color:darkBlue; font-size: 12px;"> Filtro de Búsqueda</legend>
                           <div id="BotonesBuscar" runat="server" style="margin-bottom: 15px; width: 100%; float: left;" visible="true" autopostback="true">
                               <table style="width: 100%;">
                                   <tr>
                                       <td style="text-align: right;" class="auto-style6">
                                           <asp:Label ID="NumeroInventario" runat="server" Text="No. Inventario: " Width="200px"></asp:Label>
                                       </td>                                    
                                       <td>
                                           <asp:TextBox ID="TextBoxNumInventario" runat="server"  AutoPostBack="true" CssClass="TextBox"></asp:TextBox>                                          
                                       </td>                                      
                                   </tr>
                                   <bn></bn>
                                   <tr>
                                       <td class="auto-style4" style="text-align: right;">
                                           <asp:Button ID="BotonBuscar" runat="server" Text="Buscar" CssClass="Boton"  Style= "align-content: space-around; height: 28px;" OnClick="BtnBuscar" />
                                       </td>
                                   </tr>                                                                                        
                               </table>                          
                           </div> 
                           <div runat="server" id="DivTabla" style="width: 100%;" visible="true">
                           <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional"> 
                               <Triggers>
                <asp:PostBackTrigger ControlID="BtnImprimirLista" />
            </Triggers>
                               <ContentTemplate>
                                   <asp:Label Id="LblTotalBienes" runat="server" Text="Total de Bienes:"  Visible="false"> </asp:Label>
                                   <asp:Label Id="TextBoxContador" runat="server" Visible="false"> </asp:Label>
                                  
                                   <asp:GridView ID="GridViewBuscar" runat="server" AutoGenerateColumns="false" CssClass="StyleGridV" Height="140px" HorizontalAlign="Right" Width="100%"                         
                                       DataKeyNames="IdInventario,NumInventario,DescripcionBien,Marca,Modelo,Serie,FechaAdquisicion,IdActividad,IdUsuario,IdENSU,TipoPartida"  AllowPaging="false"
                                       OnRowDeleting="GridViewBuscar_RowDeleting" PageSize="15"  > 
                                       <Columns>
                                           <asp:BoundField DataField="IdInventario" HeaderText="Id Inventario" ItemStyle-Width="5" Visible="false"/>
                                           <asp:BoundField DataField="NumInventario" HeaderText="No Inventario" ItemStyle-Width="50" />
                                           <asp:BoundField DataField="DescripcionBien" HeaderText="Descripción Bien" ItemStyle-Width="100" />
                                           <asp:BoundField DataField="Marca" HeaderText="Marca" ItemStyle-Width="80" />
                                           <asp:BoundField DataField="Modelo" HeaderText="Modelo" ItemStyle-Width="80" />
                                           <asp:BoundField DataField="Serie" HeaderText="Serie" ItemStyle-Width="80" />                                  
                                           <asp:BoundField DataField="FechaAdquisicion" HeaderText="Fecha" Visible="true" ItemStyle-Width="30" />
                                           <asp:BoundField DataField="IdActividad" HeaderText="Id Act" Visible="false" ItemStyle-Width="5" />  
                                           <asp:BoundField DataField="IdUsuario" HeaderText="Id usu" Visible="false" ItemStyle-Width="5" />
                                           <asp:BoundField DataField="IdENSU" HeaderText="ENSU" Visible="false" ItemStyle-Width="5" />
                                           <asp:BoundField DataField="TipoPartida" HeaderText="Tipo Partida" Visible="true" ItemStyle-Width="5" />  
                                                                                      
                                           <asp:CommandField ButtonType="Image" HeaderText="Eliminar" DeleteImageUrl="~/Imagenes/Generales/eliminar.png" ShowDeleteButton="true" >
                                               <ControlStyle Width="30px" />
                                               <ItemStyle Width="30px" HorizontalAlign="Center" />
                                           </asp:CommandField>                                                                                                     
                                        </Columns>
                                   </asp:GridView>
                          
                               </div>
                           <div id="DivEnviarlistas" runat="server" style="margin-bottom: 15px; width: 100%; float: left;" visible="false">
                               <table style="width: 100%;">
                                   <caption>
                                   </caption>
                                   <tr>
                                       <td class="auto-style4" style="text-align: center;">                                           
                                           <asp:Button ID="BtnEnviarlista" runat="server" Text="Enviar Lista" CssClass="Boton"  Style= "align-content: center; height: 28px;" OnClick="BtnEnviarListas_Click"/>
                                       </td>
                                       <td class="auto-style4" style="text-align: center;">                                           
                                           <asp:Button ID="BtnImprimirLista" runat="server" Text="Imprimir Lista" CssClass="Boton"  Style= "align-content: center; " OnClick="BtnImprimirListas_Click" Height="30px" Width="94px"/>    
                                        </td>
                                       <td class="auto-style4" style="text-align: center;">
                                           <asp:Button ID="Regresar" runat="server" Text="Regresar" CssClass="Boton"  Style= "align-content: center; height: 28px;" OnClick="BtnRegresar_Click"/>
                                       </td>
                                   </tr>
                               </table>                              
                               <asp:Label ID="IdInv"  runat="server" Visible="false" Text="inventario" Style="font-family: Verdana; font-size: small; margin-left: 115px; visibility:hidden "></asp:Label>
                               <asp:Label ID="IdAct"  runat="server" Visible="false" Text="actividad" Style="font-family: Verdana; font-size: small; margin-left: 115px; visibility:hidden"></asp:Label>
                               <asp:Label ID="IdUsu"  runat="server" Visible="false" Text="usuario" Style="font-family: Verdana; font-size: small; margin-left: 115px; visibility:hidden"></asp:Label>                               
                               <asp:Label ID="IdENSU" runat="server" Visible="false" Text="ENSU" Style="font-family: Verdana; font-size: small; margin-left: 115px; visibility:hidden"></asp:Label>                               
                               <asp:Label ID="fecha"  runat="server" Visible="false" Text="fecha" OnLoad="time" Style="visibility:hidden"></asp:Label>                                                                                                       
                           </div>
                                </ContentTemplate>
                             
                           </asp:UpdatePanel> 
                       </fieldset>
                    </div>

                </div>
            </ContentTemplate>
        </asp:UpdatePanel>  
    
</asp:Content>
<asp:Content ID="Content3" runat="server" ContentPlaceHolderID="CPHTitulo">
    <div style="width: auto; text-align: center; background-color: #d8d8d8;">
        <br />
        <asp:Label ID="LblLema" runat="server" Text="LISTA DE BIENES SIN REPARAR" Font-Size="Large" Font-Bold="True" Font-Italic="True" ForeColor="#0C2261"></asp:Label>
    </div>
</asp:Content>