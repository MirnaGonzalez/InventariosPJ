<%@ Page Title="" Language="C#" MasterPageFile="~/Inventarios1.Master" AutoEventWireup="true" CodeBehind="frmHistoricoPersonal.aspx.cs" Inherits="InventariosPJEH.frmHistoricoPersonal" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
     <style type="text/css">
        .auto-style5 {
            Font-Size: Medium;
            font-family: Verdana;
            font-size: small;
            font-weight: 400;
            border: 1px solid #0c2261;
            margin-left: 0px;
        }
        .auto-style6 {            margin-left: 65px;
        }
         .auto-style7 {
             margin-left: 65px;
             width: 257px;
         }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="CPHTitulo" runat="server">
    <div style="width: auto; text-align: center; background-color: #d8d8d8;">
        <asp:Label ID="LblTitulo" runat="server" Text="Histórico Personal" Font-Size="Large" Font-Bold="True" Font-Italic="True" ForeColor="#0C2261"></asp:Label>
    </div>
</asp:Content>
    
    <asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
        <asp:ScriptManager ID="ScriptManagerHistoricoPersonal" runat="server"></asp:ScriptManager> 
        <asp:UpdatePanel ID="GAdministrativa" runat="server">
            <ContentTemplate>                
                <div class="auto-style1">
                    <fieldset style="border-color: black; width: 95%; margin-left: 11px;">
                        <legend style="width: auto; color: darkblue; font-size: 12px;">Introducir parámetros de búsqueda</legend>
                        <div>
                            <div class="auto-style6">
                                <asp:Label ID="LblNomP" align="right" runat="server" Text="Nombre:" Width="151px" CssClass="auto-style6" Height="24px"></asp:Label>
                                <asp:TextBox ID="TxtNomP"  runat="server" CssClass="auto-style5" Width="249px" Height="21px"></asp:TextBox>
                            </div>
                            <div class="auto-style6">
                                <asp:Label ID="Label1" align="right" runat="server" Text="Apellido Paterno:" Width="151px" CssClass="auto-style6" Height="24px"></asp:Label>
                                <asp:TextBox ID="TextAP" runat="server" CssClass="auto-style5" Width="249px" Height="21px"></asp:TextBox>
                            </div>
                            <div class="auto-style6">
                                <asp:Label ID="Label2" align="right" runat="server" Text="Apellido Materno:" Width="151px" CssClass="auto-style6" Height="24px"></asp:Label>
                                <asp:TextBox ID="TextAM" runat="server" CssClass="auto-style5" Width="249px" Height="21px"></asp:TextBox>
                            <div class="auto-style7">
                  
                                <asp:Button ID="BtnBuscarP" runat="server" Text="Buscar" Width="87px" CssClass="Boton Marginleft" Font-Size="12pt" OnClick="BtnBuscarP_Click"/>
                                <asp:Button ID="BtnNuevBu" runat="server" Text="Nueva búsqueda" Width="163px" CssClass="Boton Marginleft" Font-Size="12pt" OnClick="BtnNuevBu_Click"/>
                            </div>
                            </div>
                        </div>
                    </fieldset>
                </div>

            <div id="DivHistorico" runat="server" visible="false">
                <fieldset style="border-color: black; width: 95%; margin-left: 11px;">
                   <legend style="width: 329px; color: darkblue; font-size: 12px;">Movimientos realizados al personal</legend>
                    <div>
                        <br />
            
                        <asp:GridView ID="GridHistoricoP" CssClass="StyleGridV" runat="server" Width="100%" AutoGenerateColumns="False">
                             <Columns>
                                    <asp:BoundField DataField="ClaveEmpleado" HeaderText="Clave Empleado"/>
                                    <asp:BoundField DataField="NombreCompleto"  HeaderText="Nombre" /> 
                                    <asp:BoundField DataField="Cargo" HeaderText="Cargo" />
                                   <asp:BoundField DataField="UniAdmin" HeaderText="Unidad Administrativa" />
                                     <asp:BoundField DataField="Fecha" HeaderText="Fecha"/>
                                </Columns>
                        </asp:GridView>
                    </div>

                </fieldset>
            </div>

            
            </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>