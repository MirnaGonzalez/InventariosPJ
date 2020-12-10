<%@ Page Title="" Language="C#" MasterPageFile="~/Inventarios1.Master" AutoEventWireup="true" CodeBehind="frmHistoricoBien.aspx.cs" Inherits="InventariosPJEH.frmHistoricoBien" %>


<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server" EnableViewState="True">
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
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHTitulo" runat="server">
    <div style="width: auto; text-align: center; background-color: #d8d8d8;">
        <asp:Label ID="LblTitulo" runat="server" Text="HISTORIAL DE BIENES" Font-Size="Large" Font-Bold="True" Font-Italic="True" ForeColor="#0C2261"></asp:Label>
    </div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <div class="auto-style1">
        <fieldset style="border-color: black; width: 95%; margin-left: 11px;">
            <legend style="width: auto; color: darkblue; font-size: 12px;">Intruducir parametros de busqueda</legend>
            <div>
                <div class="auto-style6">
                    <asp:Label ID="LblTitTrimestre" align="right" runat="server" Text="Número de Inventario:" Width="151px" CssClass="auto-style6" Height="24px"></asp:Label>
                    <asp:TextBox ID="TxtNumInventario" runat="server" CssClass="auto-style5" Width="209px" Height="21px" onkeypress="return soloNumeros(event)" MaxLength="14"></asp:TextBox>

                    <asp:Button ID="BtnBuscar" runat="server" Text="Buscar" Width="87px" CssClass="Boton Marginleft" Font-Size="12pt" OnClick="BtnBuscar_Click"  />

                    <asp:Button ID="BtnNuevaBusqueda" runat="server" Text="Nueva Busqueda" Width="163px" CssClass="Boton Marginleft" Font-Size="12pt" OnClick="BtnNuevaBusqueda_Click"  />

                </div>

            </div>
        </fieldset>
    </div>


    <!-- mostrar datos tabla -->
    <div runat="server" id="DivMostrar" visible="false">
        <fieldset style="border-color: black; width: 95%; margin-left: 11px;">
            <legend style="width: 329px; color: darkblue; font-size: 12px;">Movimientos realizados por número de Inventario</legend>
            <div>

                <br />



                <asp:GridView ID="GridHistorial" CssClass="StyleGridV" runat="server" Width="100%"
                    AutoGenerateColumns="False">
                    <Columns>

                        <%--<asp:BoundField DataField="Grupo" HeaderText="Grupo" />
                        <asp:BoundField DataField="SubGrupo" HeaderText="SubGrupo" />
                        <asp:BoundField DataField="Clase" HeaderText="Clase" />
                        <asp:BoundField DataField="IdSubClase" HeaderText="Subclase" />--%>

                     <%--   <asp:BoundField DataField="NumInventario" HeaderText="Consecutivo" />--%>

                       

                        <asp:BoundField DataField="Descripcionbien" HeaderText="Descripción" >
                        </asp:BoundField>
                        <asp:BoundField DataField="Fecha" HeaderText="Fecha" >
                        </asp:BoundField>
                        <asp:BoundField DataField="Nombre" HeaderText="Ubicación" />
                        <asp:BoundField DataField="UsuarioMov" HeaderText="Usuario Movimiento" />
                        <asp:BoundField DataField="Actividad" HeaderText="Actividad" />
                    </Columns>
                </asp:GridView>

            </div>


        </fieldset>

    </div>
    <div>
        <asp:HiddenField ID="HFTipoPartida" runat="server" />    
    </div>

</asp:Content>
