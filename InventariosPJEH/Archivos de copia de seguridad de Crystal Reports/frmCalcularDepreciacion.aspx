<%@ Page Title="" Language="C#" MasterPageFile="~/Inventarios1.Master" AutoEventWireup="true" CodeBehind="frmCalcularDepreciacion.aspx.cs" Inherits="InventariosPJEH.frmCalcularDepreciacion" %>

    
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server" EnableViewState="True">
    <style type="text/css">

        .auto-style5 {
            Font-Size: Medium;
            font-family: Verdana;
            font-size: small;
            font-weight: 400;
            border: 1px solid #0c2261;
            margin-left: 4px;
        }
        .auto-style6 {}
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHTitulo" runat="server">
      <div style="width: auto; text-align: center; background-color: #d8d8d8;">
        <asp:Label ID="LblTitulo" runat="server" Text="REPORTE DEPRECIACIÓN POR TRIMESTRE" Font-Size="Large" Font-Bold="True" Font-Italic="True" ForeColor="#0C2261"></asp:Label>
       </div>
</asp:Content> 

<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
      <div class="auto-style1">
                    <fieldset style="border-color: black; width: 95%; margin-left: 11px;">
                        <legend style="width: auto; color: darkblue; font-size: 12px;">Intruducir parametros para realizar calculo</legend>
                        <div>
                            <div  class="auto-style6" >
                                <asp:Label ID="LblTitTrimestre" align="right" runat="server" Text="Trimestre a calcular:" Width="190px" CssClass="auto-style6"></asp:Label>
                                <asp:DropDownList ID="DdlTrimestre" runat="server" CssClass="DropGeneral" AutoPostBack="True" Height="20px" Width="404px" OnSelectedIndexChanged="DdlTrimestre_SelectedIndexChanged" >
                                    <asp:ListItem Value="0">---Seleccionar-----</asp:ListItem>
                                    <asp:ListItem Value="1">Primer Trimestre</asp:ListItem>
                                    <asp:ListItem Value="2">Segunto Trimestre</asp:ListItem>
                                    <asp:ListItem Value="3">Tercer Trimestre</asp:ListItem>
                                    <asp:ListItem Value="4">Cuarto Trimestre</asp:ListItem>
                                </asp:DropDownList>             
                                                                                 
                                     <asp:Button ID="BtnCalcular" runat="server" Text="Generar calculo" Width="202px" CssClass="Boton Marginleft" OnClick="BtnCalcular_Click"   />
                                                    
                           </div>                            
                            <br />
                            <div class="auto-style6">
                                <asp:Label ID="LblTitAño" runat="server" align="right" Text="Año a calcular:" Width="190px" CssClass="auto-style6"></asp:Label>
                                <asp:TextBox ID="TxtAnio" runat="server" CssClass="auto-style5" Width="125px" Height="21px"  onkeypress="return soloNumeros(event)" MaxLength="4"></asp:TextBox>
                                <asp:CheckBox ID="CB_Inactivo"  runat="server"  Text="Bienes Inactivos" CssClass="Marginleft" OnCheckedChanged="CB_Inactivo_CheckedChanged" AutoPostBack="True"  />
                            </div>
                            <br />
                            <div class="auto-style6" id="DivInactivo" runat="server" visible="false" >
                                <asp:Label ID="LblTitBaja" runat="server" Text="Baja:" align="right" Width="190px" ></asp:Label>
                                <asp:DropDownList ID="DdlBaja" runat="server" CssClass="DropGeneral" AutoPostBack="true" Height="20px" Width="404px">
                                    <asp:ListItem Value="0">---Seleccionar-----</asp:ListItem>
                                </asp:DropDownList>
               
                                    
                        
                             
                            </div>
                            <br />
                            
                        </div>
                    </fieldset>
                </div>


    <!-- mostrar datos tabla -->
        <div id="DivMostrar" runat="server" visible="false">
                       <fieldset style="border-color: black; width: 95%; margin-left: 11px;">
                        <legend style="width: auto; color: darkblue; font-size: 12px;">Intruducir parametros para realizar calculo</legend>
                              <br />            
                           <div class="auto-style2" >
                                <asp:Label ID="LblTitTotal" runat="server" Text="Total de Bienes:" Width="129px" CssClass="auto-style6"></asp:Label>
                                                    
                                <asp:Label ID="LblTotal" runat="server" Width="166px" CssClass="auto-style6" Font-Bold="True"></asp:Label>
                                     </div>
                           <div  style="float:right; width:35%; height:20px;" >
                               <asp:Button ID="BtnImprimir" runat="server" Text="Imprimir" CssClass="Boton Marginleft" />
                           </div>
                             <br />  
                       <div class="auto-style2" >
                                <asp:Label ID="LblTitCosto" runat="server" Text="Costo Total:" Width="130px" CssClass="auto-style6"></asp:Label>
                                <asp:Label ID="LblCostoTotal" runat="server" Width="180px" CssClass="auto-style6" Font-Bold="True"></asp:Label>
                       </div>
                           <div>

                           <br />
                           
                            
                    <asp:GridView ID="GridDepreciacion" CssClass="StyleGridV" runat="server" Height="100px" Width="100%"
                        AutoGenerateColumns="False"
                       >
                        <Columns>

                            <asp:BoundField DataField="NumInventario" HeaderText="Inventario" />
                            <asp:BoundField DataField="Descripcionbien" HeaderText="Nombre " />
                            <asp:BoundField DataField="Moi" HeaderText="MOI" DataFormatString="{0:$ ##,###.##}" />
                            <asp:BoundField DataField="FechaAdquisicion" HeaderText="Fecha Adquicición" DataFormatString="{0:dd-MM-yyyy}" >
                            <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="DepreciacionTrimestre" HeaderText="Depreciación de Trimestre" DataFormatString="{0:$ ##,###.##}"/>
                            <asp:BoundField DataField="Saldoacumulado" HeaderText="Saldo Acumulado" DataFormatString="{0:$ ##,###.##}"/>
                            <asp:BoundField DataField="SaldoXDepreciar" HeaderText="Saldo por Depreciar" DataFormatString="{0:$ ##,###.##}" />
                            <asp:BoundField DataField="Añocompra" HeaderText="Año" Visible="False" />
                        </Columns>
                    </asp:GridView>
                           
                            </div>

        
                    </fieldset>

            </div>
<div>
    <asp:HiddenField ID="HFTipoPartida" runat="server" />
    <asp:HiddenField ID="HFStatus" Value="9" runat="server" />
</div>
    
</asp:Content>