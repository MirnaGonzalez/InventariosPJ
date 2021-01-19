<%@ Page Title="" Language="C#" MasterPageFile="~/Inventarios1.Master" AutoEventWireup="true" CodeBehind="frmActaAdministrativa.aspx.cs" Inherits="InventariosPJEH.frmActaAdministrativa" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<script runat="server">
    public void BtnSetFechaFinConsulta()
    {
        this.calConFechaFin.SelectedDate = calConFechaIni.SelectedDate.Value.AddDays(1);
    }
</script>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        .auto-style19 {
            width: 826px;
        }

        .auto-style20 {
            text-transform: uppercase;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHTitulo" runat="server">
       <div style="width: auto; text-align: center; background-color: #d8d8d8;">
        <asp:Label ID="LblTituloActa" runat="server" Text="Generación y Consulta de Actas Administrativas" Font-Size="Large" Font-Bold="True" Font-Italic="True" ForeColor="#6D252B"/>
    </div>
</asp:Content>
   
<asp:Content ID="divGeneral" ContentPlaceHolderID="MainContent" runat="server">
    
   <asp:ScriptManager ID="ScriptManagerGenerarActa" runat="server"></asp:ScriptManager> 
    
    <asp:UpdatePanel ID="GAdministrativa" runat="server">
        <ContentTemplate>
            <div id="ContenerTodo">            
                <div id="tabControl" runat="server" visible="true" style="width: 95%; margin-left: 0px;">
                    <asp:RadioButton ID="tabGeneracionRadio" runat="server" Text="Generar Acta" AutoPostBack="true" GroupName="tabControlButtons" OnCheckedChanged="BtnActasRadio_CheckedChanged" Checked="true"/>
                    <asp:RadioButton ID="tabConsultaRadio" runat="server" Text="Consultar Acta" AutoPostBack="true" GroupName="tabControlButtons" OnCheckedChanged="BtnActasRadio_CheckedChanged"/>
                </div>
                <div id="divGeneracionActas" runat="server" visible="true">
                    <div id="FiltroB" style="width: 95%; margin-left: 0px;">
                    <fieldset style="border-color: black; width: 95%; margin-left: 11px;">
                        <legend style="width: auto; color: darkblue; font-size: 12px;">Filtro Búsqueda</legend>
                            <asp:Label ID="lblNoResguardo" runat="server" CssClass="LabelGeneral" Text="Número de Resguardo: "/>
                            <asp:TextBox ID="TxtFiltroB" runat="server" CssClass="TextBox" Height="22px" Width="183px"/>
                            <asp:Button  ID="BtnFiltroB" runat="server" CssClass="Boton" Height="28px" Text="Búsqueda" Width="100px" OnClick="BtnFiltroB_Click"/>                            
                    </fieldset>
                    <fieldset id="sectionBienes" style="border-color: black; width: 95%; margin-left: 11px;" visible ="false" runat="server">
                           <legend style="width: auto; color: darkblue; font-size: 12px;">Bienes no Localizados</legend>
                          <div>
                              <table style="width: 100%; margin-top: 30px;">
                                  <tr>
                                      <td>
                                          <asp:Label ID="lblNombreR" Font-Bold="true" runat="server" CssClass="LabelGeneral" Text="Nombre del Resguardante: "/>
                                      </td>
                                      <td>
                                          <asp:Label ID="lblNombreResul" runat="server" CssClass="LabelGeneral" Text=""/>                                          
                                      </td>
                                  </tr>
                                  <tr>
                                      <td>
                                          <asp:Label ID="lblCargoR" Font-Bold="true" runat="server" CssClass="LabelGeneral" Text="Cargo del Resguadante:"/>
                                      </td>
                                      <td>
                                          <asp:Label ID="lblCargoResul" runat="server" CssClass="LabelGeneral" Text=""/>                                          
                                      </td>
                                  </tr>
                                  <tr>
                                      <td>
                                          <asp:Label ID="lblAreaAdri" Font-Bold="true" runat="server" CssClass="LabelGeneral" Text="Area de Adcripción:"/>
                                      </td>
                                      <td>
                                          <asp:Label ID="lblAreaResul"  runat="server" CssClass="LabelGeneral" Text=""/>
                                          <asp:Label ID="lblIdResguardo"  runat="server" CssClass="LabelGeneral" Text="" Visible="false"/>
                                          <asp:Label ID="lblIdUniAdmin"  runat="server" CssClass="LabelGeneral" Text="" Visible="false"/>
                                      </td>
                                  </tr>
                              </table>
                          </div>

                           <div runat="server" id="DivTablaResultadosResguardo" style="width: 100%; text-align: center;" visible="false">

                            <fieldset style="height: auto">

                                <asp:GridView ID="gridResultadosBienes" CssClass="StyleGridV" runat="server" Height="142px" Width="100%"
                                    AutoGenerateColumns="False" HorizontalAlign="Center"
                                    DataKeyNames="idInventario">
                                    <Columns>                                                  
                                        <asp:BoundField DataField="idInventario" Visible="false" />
                                        <asp:BoundField DataField="txtOInventarioResguardo" HeaderText="Inventario" />
                                        <asp:BoundField DataField="txtONombreBienResguardo" HeaderText="Nombre del Bien" />
                                        <asp:BoundField DataField="txtOMarcaResguardo" HeaderText="Marca" />
                                        <asp:BoundField DataField="txtOModeloResguardo" HeaderText="Modelo" />
                                        <asp:BoundField DataField="txtOSerieResguardo" HeaderText="Serie" />

                                        
                                    </Columns>
                                </asp:GridView>
                            </fieldset>
                        </div>
                        <fieldset id="sectionGuardarActa" runat="server" visible="false" style="border-color: black; width: 95%; margin-left: 11px;">
                        <table style="margin-left: 100px" class="auto-style8">
                            <tr>
                                <td style="text-align: right;" class="auto-style6">
                                    <asp:Label ID="lblNumActa" runat="server" CssClass="LabelGeneral" Text="Número de Acta: "/>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtNumActa" runat="server" AutoPostBack="true" CssClass="TextBox" Height="22px" Width="183px"/>
                                </td>                        
                                <td style="text-align: right;" class="auto-style6">
                                    <asp:Label ID="LablFechaAdquisicion" runat="server" Text="Fecha adquisición:" Width="190px"/>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtFechaAdquisicion" runat="server" CssClass="TextBox" Enabled="false"/>
                                    <asp:Image ID="ImgFechaAdquisicion" runat="server" Height="40px" ImageUrl="~/Imagenes/Generales/Calendario.png" Width="50px" />                                    
                                        <cc1:CalendarExtender ID="FechaAdquisicion" runat="server" Format="dd/MM/yyyy" PopupButtonID="ImgFechaAdquisicion" TargetControlID="txtFechaAdquisicion" />
                                </td>
                            </tr>                            
                        </table>
                        </fieldset>                                                        
                        <table style="width: 50%; margin: 0 auto; text-align: center;">
                            <tr>
                                <td>
                                    <asp:Button ID="BtnInsertarActa" CssClass="Boton" runat="server" Text="Guardar Acta" OnClick="BtnInsertarActa_Click" />
                                </td>
                                <td>
                                    <asp:Button ID="BtnCancelarr" CssClass="Boton" runat="server" Text="Cancelar" OnClick="BtnCancelar_Click" />
                                </td>
                            </tr>
                        </table>                            
                    </fieldset>
                </div>
               </div>

                <div id="divConsultaActas" runat="server" visible="false">
                    <div id="FiltroBusquedaConsulta" style="width: 95%; margin-left: 0px;">
                    <fieldset style="border-color: black; width: 95%; margin-left: 11px;">
                        <legend style="width: auto; color: darkblue; font-size: 12px;">Filtro Búsqueda</legend>
                            <table style="margin-left: 100px" class="auto-style8">
                                <tr>
                                    <td>
                                        <asp:Label ID="lblConNumActa" runat="server" CssClass="LabelGeneral" Text="Número de Acta: "/>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtConNumActa" runat="server" AutoPostBack="true" CssClass="TextBox" Height="22px" Width="183px"/>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblConNumInventario" runat="server" CssClass="LabelGeneral" Text="Número de Inventario: "/>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtConNumInventario" runat="server" AutoPostBack="true" CssClass="TextBox" Height="22px" Width="183px"/>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:CheckBox ID="checkConPeriodo" runat="server" Text="Periodo" oncheckedchanged="BtnActivarPeriodo" AutoPostBack="true" />
                                    </td>
                                    <td runat="server" id="celdaConFechaIni" Visible="false">
                                        <asp:TextBox ID="txtConFechaIni" runat="server" CssClass="TextBox" Enabled="false"/>
                                        <asp:Image ID="imgConFechaIni" runat="server" Height="40px" ImageUrl="~/Imagenes/Generales/Calendario.png" Width="50px" />                                    
                                        <cc1:CalendarExtender ID="calConFechaIni" runat="server" Format="dd/MM/yyyy" PopupButtonID="imgConFechaIni" TargetControlID="txtConFechaIni"/>
                                    </td>
                                    <td runat="server" id="TceldaConFechaFin" Visible="false">
                                        <asp:TextBox ID="txtConFechaFin" runat="server" CssClass="TextBox" Enabled="false"/>
                                        <asp:Image ID="imgConFechaFin" runat="server" Height="40px" ImageUrl="~/Imagenes/Generales/Calendario.png" Width="50px" />                                    
                                        <cc1:CalendarExtender ID="calConFechaFin" runat="server" Format="dd/MM/yyyy" PopupButtonID="imgConFechaFin" TargetControlID="txtConFechaFin" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Button  ID="btnConBuscarActa" runat="server" CssClass="Boton" Height="28px" Text="Buscar" Width="100px" OnClick="BtnConsultarActa"/>                            
                                    </td>
                                    <td>
                                        <asp:Button  ID="btnConLimpiarBusqueda" runat="server" CssClass="Boton" Height="28px" Text="Limpiar" Width="100px" OnClick="BtnLimpiarActa"/>                            
                                    </td>
                                </tr>                                                                                                
                                
                            </table>
                    </fieldset>
                </div>
                <div runat="server" id="tablaConsultaActas" style="width: 100%;" visible="false">

                    <fieldset style="height: auto">

                        <asp:GridView ID="gridConsultaActas" CssClass="StyleGridV" runat="server" Height="142px" Width="100%"
                            AutoGenerateColumns="False" OnRowCommand="GridModificar_RowCommand"
                            DataKeyNames="idActa">
                            <Columns>                                                                        
                                <asp:BoundField DataField="strNumActa" HeaderText="No. Acta" />
                                <asp:BoundField DataField="strNombreResguardante" HeaderText="Nombre" />
                                <asp:BoundField DataField="strUniAdmin" HeaderText="Area" />                                        
                                <asp:BoundField DataField="strFechaActa" HeaderText="Fecha Acta" />                                        
                                <asp:BoundField DataField="strNumInventario" HeaderText="Inventario" />         
                                <asp:BoundField DataField="strNombreBien" HeaderText="Nombre del Bien" />     
                                <asp:BoundField DataField="strMarca" HeaderText="Marca" />                                        
                                <asp:BoundField DataField="strModelo" HeaderText="Modelo" />                                        
                                <asp:BoundField DataField="strSerie" HeaderText="Serie" />                                        
                                <asp:BoundField DataField="strNumResguardo" HeaderText="No. Resguardo" />
                                <asp:TemplateField HeaderText="Editar" ItemStyle-Width="50">
                                    <ItemTemplate>
                                        <asp:ImageButton runat="server" ID="btnEditar" Width="16" Height="16" ImageUrl="~/Imagenes/Generales/editar.png" CommandName="Editar" OnClick="btnEditar_Click" CommandArgument='<%# Container.DataItemIndex.ToString() %>' />
                                    </ItemTemplate>
                                    <ItemStyle Width="50px" />
                                </asp:TemplateField>                                                                                                                                                                                                        
                            </Columns>
                        </asp:GridView>
                    </fieldset>
                </div>

                <div id="divConsultaActasEditar" style="width: 95%; margin-left: 0px;" runat="server" visible="false">
                    <fieldset style="border-color: black; width: 95%; margin-left: 11px;">
                        <legend style="width: auto; color: darkblue; font-size: 12px;">Datos a Modificar</legend>
                            <table style="margin-left: 100px" class="auto-style8">
                                <tr>
                                    <td>
                                        <asp:Label ID="lblEditNumActa" runat="server" CssClass="LabelGeneral" Text="Número de Acta: "/>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtEditNumActa" runat="server" AutoPostBack="true" CssClass="TextBox" Height="22px" Width="183px" Enabled="false"/>
                                    </td>
                                    <td>
                                        <asp:CheckBox ID="checkConActasFechaMod" runat="server" Text="Fecha de Solución:" oncheckedchanged="BtnConActasFechaModificar" AutoPostBack="true" />
                                    </td>
                                    <td runat="server" id="celdaConActasFechaSolucion" Visible="false">
                                        <asp:TextBox ID="txtEditConActaFechaSol" runat="server" CssClass="TextBox" Enabled="false"/>
                                        <asp:Image ID="imgEditConActaFechaSol" runat="server" Height="40px" ImageUrl="~/Imagenes/Generales/Calendario.png" Width="50px" />                                    
                                        <cc1:CalendarExtender ID="calendarEditConActaFechaSol" runat="server" Format="dd/MM/yyyy" PopupButtonID="imgEditConActaFechaSol" TargetControlID="txtEditConActaFechaSol"/>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblConActaFechaActa" runat="server" CssClass="LabelGeneral" Text="Fecha Acta: "/>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtEditActaFechaActa" runat="server" AutoPostBack="true" CssClass="TextBox" Height="22px" Width="183px" Enabled="false"/>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblConActaEstatus" runat="server" CssClass="LabelGeneral" Text="Estatus: " Visible="false"/>
                                    </td>
                                    <td>
                                        <asp:dropdownlist runat="server" id="lstEstatus" Visible="false"> 
                                             <asp:listitem text="Seleccione un Valor de Estatus" value="-1" Selected="true"/>
                                             <asp:listitem text="Cancelación" value="C"/>
                                             <asp:listitem text="Reposición" value="R"/>
                                        </asp:dropdownlist>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblEditConActasDescripcion" runat="server" CssClass="LabelGeneral" Text="Descripción: " Visible="false" />
                                    </td>
                                    <th colspan="3">
                                        <asp:TextBox ID="txtEditConActaDescripcion" runat="server" TextMode="MultiLine" Rows="2" AutoPostBack="true" CssClass="TextBox" Width="100%" Height="100%" Visible="false"/>
                                    </th>
                                </tr>
                            </table>                               
                    </fieldset>
                    <table style="width: 50%; margin: 0 auto; text-align: center;">
                        <tr>
                            <td>
                                <asp:Button ID="btnGuardarActa" CssClass="Boton" runat="server" Text="Guardar Acta" OnClick="BtnGuardarEditActa_Click" />
                            </td>
                            <td>
                                <asp:Button ID="btnCancelarEdit" CssClass="Boton" runat="server" Text="Cancelar" OnClick="BtnCancelarEdit_Click" />
                            </td>
                        </tr>
                    </table>
                </div>

            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
