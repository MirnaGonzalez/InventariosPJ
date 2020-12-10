<%@ Page Title="" Language="C#" MasterPageFile="~/Inventarios1.Master" AutoEventWireup="true" CodeBehind="frmReparaBien.aspx.cs" Inherits="InventariosPJEH.frmReparaBien" EnableEventValidation="false" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.4000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">

<%--    <%--Estilos generados automáticamente al agregar tablas--%>
    <style type="text/css">
        
        .auto-style3 {
            width: 314px;
        }

        .auto-style4 {
            width: 291px;
        }

        #RecibirBienMant {            height: 343px;
        }
        .auto-style27 {
            width: 100%;
        }
        </style>
    <%-- ----------------------------------------------------------%>--%>
    <style type="text/css">
        .auto-style1 {
            -webkit-box-shadow: 3px 3px 0px #424242;
            -moz-box-shadow: 3px 3px 0px #525252;
            font-family: Verdana, Geneva, Tahoma, sans-serif;
            color: White;
            font-size: 18px;
            background: #808080;
            text-decoration: none;
            margin-left: 22px;
        }
    </style>
    <style type="text/css">
        .auto-style1 {
            margin-bottom: 0px;
        }
    </style>
    <style type="text/css">
        .auto-style1 {
            float: left;
            width: 66%;
        }
    </style>
    <style type="text/css">
        .auto-style1 {
            width: 523px;
        }
        .auto-style3 {
            margin-left: 250px;
        }
        .auto-style4 {
            height: 21px;
            margin-left: 250px;
        }
    </style>
    <style type="text/css">
        .auto-style1 {
            text-transform: uppercase;
        }
    </style>
    <style type="text/css">
        .auto-style1 {
            width: 165px;
        }
    </style>
    <style type="text/css">
        .auto-style1 {
            width: 161px;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManagerRegistro" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="RepararBien" runat="server">
            <ContentTemplate>
                <div id="bodyContenedorMantenimiento">
                    <%-- INTERFAZ REPARACIÓN --%>
                    <div id="detalleReparacion" runat="server">
                        <fieldset style="border-color: black; width: 95%; margin-left: 11px;">
                            <legend style="width: auto; color: darkblue; font-size: 12px;"  id="busqueda">Búsqueda del Bien </legend>
                            <%-- TextBox de Búsqueda --%>
                            <table style="width: 100%;">
                                <tr>
                                    <td style="text-align: right;" class="auto-style6">
                                        <asp:Label ID="LablNoInventario" runat="server" Text="No. Inventario: " Width="200px"></asp:Label>
                                    </td>                                    
                                    <td>
                                        <asp:TextBox ID="TextBoxNoInventario" runat="server" AutoPostBack="true" CssClass="TextBox"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: right;" class="auto-style6">
                                        <asp:Label ID="LablNoSerie" runat="server" Text="No. Serie: " Width="200px"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TextBoxSerie" runat="server" AutoPostBack="true" CssClass="TextBox"> </asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                            <br />
                            <%-- Grid de Datos de Busqueda --%>
                            <div id="datosBusqueda" runat="server" height="100px" width="100%" style="margin-bottom: 15px;  float: left; align-items:center ; overflow:auto "  visible="true" >
                                <table style="margin-left: 31px; align-items:center" aria-multiselectable="True">                                                   
                                </table>
                            </div>
                            <%-- Botón de Buscar --%>
                            <div id="BotonesBuscar" runat="server" style="margin-bottom: 15px; width: 100%; float: left;">
                                <table style="width: 100%;">
                                    <tr>
                                        <td class="auto-style4" style="text-align: center;">
                                            <asp:Button ID="BtnBuscar" runat="server" Text="Buscar" CssClass="Boton"  Style= "align-content: space-around; height: 28px;" OnClick="BtnBuscar_Click" />
                                        </td>
                                    </tr>
                                    <asp:Label ID="LbNuminventario" runat="server" Visible="false" Text="id" Style="font-family: Verdana; font-size: small; margin-left: 115px; visibility:hidden"></asp:Label>
                                    <asp:Label ID="LbIdinv" runat="server" Visible="false" Text="id" Style="font-family: Verdana; font-size: small; margin-left: 115px; visibility:hidden"></asp:Label>
                                </table>
                            </div>
                            <asp:UpdatePanel ID="UpdateGrid" runat="server">
                                <ContentTemplate>
                                    <asp:GridView ID="GridViewBusqueda" runat="server" AutoGenerateColumns="false" CssClass="StyleGridV" Height="140px" HorizontalAlign="Right" Width="100%"                         
                                        DataKeyNames="IdInventario,NumInventario,DescripcionBien,Marca,Modelo,Serie,NombrePersona,IdActividad,IdENSU,IdUsuario"  AllowPaging="false" PageSize="15" >
                                        <Columns>
                                            <asp:BoundField DataField="IdInventario" HeaderText="Id Inventario" ItemStyle-Width="20" Visible="false"/>
                                            <asp:BoundField DataField="NumInventario" HeaderText="No Inventario" ItemStyle-Width="50" />
                                            <asp:BoundField DataField="DescripcionBien" HeaderText="Descripción Bien" ItemStyle-Width="100" />
                                            <asp:BoundField DataField="Marca" HeaderText="Marca" ItemStyle-Width="50" />
                                            <asp:BoundField DataField="Modelo" HeaderText="Modelo" ItemStyle-Width="50" />
                                            <asp:BoundField DataField="Serie" HeaderText="Serie" ItemStyle-Width="50" />
                                            <asp:BoundField DataField="NombrePersona" HeaderText="Asignada a Resguardo" ItemStyle-Width="100" Visible="false" /> 
                                            <asp:BoundField DataField="IdActividad" HeaderText="Actividad" Visible="false" ItemStyle-Width="20" />                                    
                                            <asp:BoundField DataField="IdENSU" HeaderText="ENSU" Visible="false" ItemStyle-Width="20" />                                    
                                            <asp:BoundField DataField="IdUsuArio" HeaderText="Id Usuario" Visible="false" ItemStyle-Width="20" />                                                                        
                                            <asp:TemplateField HeaderText="Mantenimiento" ItemStyle-Width="50">
                                                <ItemTemplate>
                                                    <asp:ImageButton runat="server" ID="btn_Mantenimiento" Width="16" Height="16" ImageUrl="~/Imagenes/Generales/agregar.png" OnCommand="btn_Mantenimiento_Command" CommandName="check"  CommandArgument='<%# Container.DataItemIndex.ToString() %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Solicitar Piezas" ItemStyle-Width="50" >
                                                <ItemTemplate>
                                                    <asp:ImageButton runat="server" ID="btn_Piezas" Width="16" Height="16" style="align-items:center" ImageUrl="~/Imagenes/Generales/agregar.png"  OnCommand="btn_Piezas_Command" CommandName="pieza"  CommandArgument='<%# Container.DataItemIndex.ToString() %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                         <br />                     
                            <%-- Botones de Reparación --%>
                            <div id="BotonesReparacion" runat="server" style="margin-bottom: 15px; width: 100%; float: left;">
                            <br />
                            <table style="width: 100%;">
                                <tr>          
                                    <td class="auto-style4" style="text-align: center;">
                                        <asp:Button ID="BtnSinReparacion" CssClass="Boton" runat="server"  Text="Sin Reparación" visible="false" OnClick="EnviarBajaBien"/>
                                    </td>
                                </tr>
                            </table>
                            </div>
                        </fieldset>
                    </div>                    
                    <%-- INTERFAZ SALIDA DE MANTENIMIENTO --%>
                    <div id="salidaMant" runat="server" visible="false">
                        <fieldset style="border-color:black; width: 95%; margin-left: 11px;">
                            <legend style="width:auto; color: darkblue; font-size:12px;">Salida de Mantenimiento</legend>
                            <%-- TexBox Salida Manteniento --%>
                            <div id="div5" runat="server" style="margin-bottom: 15px; width: 100%; float: left;" visible="true">
                                <table style="width: 100%;" >
                                    <div id="divConcatena" runat="server" visible="false" style="margin-bottom: 15px; text-align: center; width: 100%; float: left;">                                                                              
                                        <caption>
                                            <tr>  
                                                <asp:Label ID="LbId" runat="server" Visible="false" Text="id" Style="font-family: Verdana; font-size: small; margin-left: 115px; visibility: hidden"></asp:Label>
                                                <asp:Label ID="LbENSU" runat="server" Visible="false" Text="ENSU" Style="font-family: Verdana; font-size: small; margin-left: 115px; visibility: hidden"></asp:Label>                                         
                                                <asp:Label ID="LbUsuario" runat="server" Visible="false" Text="Usuario" Style="font-family: Verdana; font-size: small; margin-left: 115px; visibility: hidden"></asp:Label>                                         
                                                <asp:Label ID="LbActividad" runat="server" Visible="false" Text="Usuario" Style="font-family: Verdana; font-size: small; margin-left: 115px; visibility: hidden"></asp:Label>                                                                                         
                                            </tr>
                                            <tr>
                                                <td class="auto-style25" style="text-align: right;">
                                                    <asp:Label ID="Label1" runat="server" style="text-align:center" Text="Numero de inventario:"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="TextBoxNumInventario" runat="server" style="align-content:center"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="auto-style25" style="text-align: right;">
                                                    <asp:Label ID="LablNombrRecibeBien" runat="server" Text="Nombre de quien recibe el Bien: "></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="TextBoxNombreRecibeBien" runat="server" AutoPostBack="true" CssClass="TextBox1" Width="414px"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="auto-style25" style="text-align: right;">
                                                    <asp:Label ID="LablNoOficio" runat="server" Text="Numero de Oficio: "></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="TextBoxNoOficio" runat="server" AutoPostBack="false" CssClass="TextBox1" Width="414px"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="auto-style25" style="text-align: right;">
                                                    <asp:Label ID="LabNomEntregaBien" runat="server" Text="Nombre de quien entrega el Bien: "></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="TextBoxNombreEntregaBien" runat="server" AutoPostBack="true" CssClass="TextBox1" onkeypress="return ValidacionLetras(event);" Width="414px"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="auto-style25" style="text-align: right;">
                                                    <asp:Label ID="LablSistemaOerativo" runat="server" Text="Sistema Operativo (CPU´S):"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="TextBoxSistemaOperativo" runat="server" AutoPostBack="true" CssClass="TextBox1" Width="414px"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="auto-style25" style="text-align: right;">
                                                    <asp:Label ID="LablCuentaOffice" runat="server" Text="Cuenta Office 365: "></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="TextBoxCuentaOffice" runat="server" CssClass="TextBox1" Width="414px"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="auto-style25" style="text-align: right;">
                                                    <asp:Label ID="LabelIp" runat="server" Text="IP: "></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="TextBoxIP" runat="server" CssClass="TextBox1" Width="414px"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="auto-style25" style="text-align: right;">
                                                    <asp:Label ID="FallaR" runat="server" Text="Falla Reportada: "></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="TextBoxFallaReportada" runat="server" CssClass="TextBox1" Height="38px" onkeypress="return numbersonly(event);" Width="414px"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="auto-style25" style="text-align: right;">
                                                    <asp:Label ID="LblDiagnostico" runat="server" Text="Diagnostico/Reparación: "></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="TextBoxDiagnostico" runat="server" CssClass="TextBox1" Height="40px" onkeypress="return numbersonly(event);" Width="414px"></asp:TextBox>
                                                </td>
                                                    <asp:Label ID="Label2" runat="server" Visible="false" Text="id" Style="font-family: Verdana; font-size: small; margin-left: 115px;" ></asp:Label>                                            
                                            </tr>
                                        </caption>                                                     
                                    </div>
                                </table>
                            </div>                                                                                         
                             <%-- Botones Salida Mantenimiento --%>
                            <div id="BotonesSalida" runat="server" style="margin-bottom: 15px; width: 100%; float: left;" visible="true">
                            <table style="width: 100%;">
                                <tr>
                                    <td class="auto-style4" style="text-align: center;">
                                        <asp:Button ID="guardar" runat="server" CssClass="Boton" Text="Guardar"  Visible="false" OnClick="BtnGuardar_Click"/>
                                    </td>
                                    <td class="auto-style4" style="text-align: center;">
                                        <asp:Button ID="cancelar" runat="server" CssClass="Boton" Text="Cancelar" visible="false" OnClick="BtnCancelar" />
                                    </td>
                                    <td class="auto-style4" style="text-align: center;">
                                        <asp:Button ID="ordenServicio" runat="server" CssClass="Boton" Text="Generar Orden de Servicio" Visible="false" OnClick="BtnOrdenServicio" />
                                    </td>
                                </tr>
                            </table>
                            <div id="gridMantenimiento" runat="server" visible="false">
                            <asp:GridView ID="Grid" runat="server" AutoGenerateColumns="False" CssClass="StyleGridV" Height="140px" HorizontalAlign="Right" Width="100%"                         
                                    DataKeyNames="IdRegMant,IdInventario,NombreRecibe,NumOficio,NombreEntrega,SistemaOperativo,CuentaOffice,Ip,FallaReportada,DiagnosticoReparacion"  AllowPaging="True"                                PageSize="15" DataSourceID="InventariosMantenimiento" EnablePersistedSelection="True">                                     
                                    <Columns>
                                        <asp:BoundField DataField="IdRegMant" HeaderText="id registro" ItemStyle-Width="50" />                                   
                                        <asp:BoundField DataField="IdInventario" HeaderText="inventario" ItemStyle-Width="50" />                                   
                                        <asp:BoundField DataField="NombreRecibe" HeaderText="Nombre R" ItemStyle-Width="50" />
                                        <asp:BoundField DataField="NumOficio" HeaderText="No Oficio" ItemStyle-Width="100" />
                                        <asp:BoundField DataField="NombreEntrega" HeaderText="Nombre E" ItemStyle-Width="50" />
                                        <asp:BoundField DataField="SistemaOperativo" HeaderText="S.O." ItemStyle-Width="50" />
                                        <asp:BoundField DataField="CuentaOffice" HeaderText="Cuenta" ItemStyle-Width="50" />
                                        <asp:BoundField DataField="Ip" HeaderText="IP" ItemStyle-Width="100" /> 
                                        <asp:BoundField DataField="FallaReportada" HeaderText="Falla" Visible="true" ItemStyle-Width="20" />                                    
                                        <asp:BoundField DataField="DiagnosticoReparacion" HeaderText="Diagnostico" Visible="true" ItemStyle-Width="20" />                                    
                                    </Columns>
                                </asp:GridView> 
                                <asp:SqlDataSource ID="InventariosMantenimiento" runat="server" ConnectionString="<%$ ConnectionStrings:InventariosConnectionString %>" SelectCommand="SELECT * FROM [Mantenimiento] WHERE ([IdInventario] = @IdInventario)">
                                    <SelectParameters>
                                         <asp:ControlParameter ControlID="LbId" Name="IdInventario" PropertyName="Text" Type="Int32" />
                                                                                                                                      </SelectParameters>
                                </asp:SqlDataSource>
                            </div>
                        </div>
                            <asp:Label ID="Lbidreg" runat="server" Visible="true"></asp:Label>                            
                            <asp:Label ID="Label3" runat="server" Visible="true"></asp:Label>                            
                            <asp:Label ID="LblNumLote2" runat="server" Visible="false"></asp:Label>                            
                            <asp:Label ID="LblNumLote1" runat="server" Visible="false"></asp:Label>                                    
                            <asp:HiddenField ID="HiddenFecha" runat="server" OnLoad="time" />                        
                    </fieldset> 
                  </div>         
                    <%-- INTERFAZ PIEZAS DE REPARACIÓN --%>
                    <div id="PiezasReparacion" runat="server" visible="false">
                        <fieldset style="border-color: black; width: 95%; margin-left: 11px;">
                            <legend style="width: auto; color: darkblue; font-size: 12px;">Solicitud de Piezas a Reparación</legend>
                                <%-- TextBox Piezas --%>
                                <div id="piezasRepara" runat="server" style="margin-bottom: 15px; width: 100%; float: left;" visible="false">                        
                                    <table style="width: 100%;">
                                        <tr>
                                             <td style="text-align: right;" class="auto-style6">
                                                <asp:Label ID="IdRegMante" runat="server" Visible="true" Text="idReg" Style="font-family: Verdana; font-size: small; margin-left: 115px; visibility:hidden"></asp:Label>
                                                <asp:Label ID="IdInventariio" runat="server" Visible="true" Text="idInve" Style="font-family: Verdana; font-size: small; margin-left: 115px; visibility:hidden"></asp:Label>                                        
                                            </td>  
                                        </tr>
                                        <tr>
                                            <td style="text-align: right;" class="auto-style6">
                                                <asp:Label ID="LabelNoInven" runat="server" Text="No. Inventario: " Width="200px"></asp:Label>
                                            </td>
                                            <td>                                         
                                                <asp:Label ID="TextBoxNoInven" runat="server" Text="NumInventario: " Width="200px"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: right;" class="auto-style6">
                                                <asp:Label ID="LabelTarjetaInf" runat="server" Text="No. Tarjeta informativa: " Width="200px"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TextBoxTarjetaInf" runat="server" AutoPostBack="true" CssClass="TextBox"> </asp:TextBox>
                                            </td>
                                        </tr> 
                                        <tr>
                                            <td style="text-align: right;" class="auto-style6">
                                                <asp:Label ID="NoRefaccion" runat="server" Text="Cantidad: " Width="200px"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TextBoxNumRefaccion" runat="server" AutoPostBack="true" CssClass="TextBox"> </asp:TextBox>
                                            </td>
                                        </tr>   
                                        <tr>
                                            <td style="text-align: right;" class="auto-style6">
                                                <asp:Label ID="DescRefacci" runat="server" Text="Descripción de la Refacción: " Width="200px"></asp:Label>
                                            </td>
                                            <td>      
                                                <asp:TextBox ID="TextBoxDescrRefac" runat="server" AutoPostBack="true" CssClass="TexBox" Width="160px"></asp:TextBox>                                    
                                            </td>
                                        </tr>                          
                                        <tr>
                                         <td style="text-align: center;">
                                             <br />
                                         </td>
                                     </tr>                
                                    </table>                        
                                </div>           
                                <br />
                                <%-- Grid Piezas Reparacion --%>
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                    <ContentTemplate>
                                        <asp:GridView ID="GridPiezas" runat="server" visible="False" AutoGenerateColumns="False" CssClass="StyleGridV" Height="140px" HorizontalAlign="Right" Width="100%"                         
                                            DataKeyNames="IdInventario,NumTarjetaInf,NumRefaccion,DescripcionRefaccion,IdRegMant"  AllowPaging="True" PageSize="15" DataSourceID="InventariosPiezas" >
                                                <Columns>                                                     
                                                     <asp:BoundField DataField="IdInventario" HeaderText="Id Inventario" Visible="false" ItemStyle-Width="20"/>
                                                     <asp:BoundField DataField="NumTarjetaInf" HeaderText="No Tarjeta" ItemStyle-Width="50" />
                                                     <asp:BoundField DataField="NumRefaccion" HeaderText="Cantidad" ItemStyle-Width="100" />
                                                     <asp:BoundField DataField="DescripcionRefaccion" HeaderText="Descripcion" ItemStyle-Width="50" />
                                                     <asp:BoundField DataField="IdRegMant" HeaderText="Id Registro"  visible="false" ItemStyle-Width="50" />                                                                                                                         
                                                     <asp:TemplateField HeaderText="Generar Tarjeta" ItemStyle-Width="50">
                                                        <ItemTemplate>
                                                             <asp:ImageButton runat="server" ID="btn_tarjeta_informativa" Width="16" Height="16" ImageUrl="~/Imagenes/Generales/agregar.png" OnCommand="btn_tarjetainf_Command" CommandName="check"  CommandArgument='<%# Container.DataItemIndex.ToString() %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                  </Columns>
                                        </asp:GridView>
                                        <asp:SqlDataSource ID="InventariosPiezas" runat="server" ConnectionString="<%$ ConnectionStrings:InventariosConnectionString %>" SelectCommand="SELECT * FROM [PiezaReparacion] WHERE ([IdInventario] = @IdInventario)">
                                            <SelectParameters>
                                                <asp:ControlParameter ControlID="LbId" Name="IdInventario" PropertyName="Text" Type="Int32" />
                                            </SelectParameters>
                                        </asp:SqlDataSource>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                                <%-- Botones Piezas --%>
                                <div id="botones" runat="server" style="margin-bottom: 15px; width: 100%; float: left; align-content:center; height: 72px;">
                                    <table style="width: 98%; height: 17px;">
                                        <tr>
                                            <td class="auto-style4" style="text-align: center;">
                                                <asp:Button ID="guardardatos" runat="server" Class="Boton" Text="Guardar Datos" OnClick="guardardatos_Click" Width="168px" />
                                            </td>
                                            <td class="auto-style4" style="text-align: center;">
                                                <asp:Button ID="cancelarpiezas" runat="server" class="Boton" Text="Cancelar" OnClick="BtnCancelar_Piezas" Width="168" />                                   
                                            </td>                                    
                                        </tr>
                                    </table>
                                </div>                        
                        </fieldset>
                   </div >                
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
</asp:Content>

<asp:Content ID="Content3" runat="server" ContentPlaceHolderID="CPHTitulo">
    <div style="width: auto; text-align: center; background-color: #d8d8d8;">
        <br />
        <asp:Label ID="LblLema" runat="server" Text="REPARAR UN BIEN" Font-Size="Large" Font-Bold="True" Font-Italic="True" ForeColor="#6D252B"></asp:Label>
    </div>
</asp:Content>