<%@ Page Title="" Language="C#" MasterPageFile="~/Inventarios1.Master" AutoEventWireup="true" CodeBehind="frmReporteCONAC.aspx.cs" Inherits="InventariosPJEH.frmReporteCONAC" %>


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

        .auto-style6 {
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHTitulo" runat="server">
    <div style="width: auto; text-align: center; background-color: #d8d8d8;">
        <asp:Label ID="LblTitulo" runat="server" Text="REPORTE VALOR EN LIBROS (CONAC)" Font-Size="Large" Font-Bold="True" Font-Italic="True" ForeColor="#0C2261"></asp:Label>
    </div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <div class="auto-style1">
        <fieldset style="border-color: black; width: 95%; margin-left: 11px;">
            <legend style="width: auto; color: darkblue; font-size: 12px;">Intruducir parametros para realizar calculo</legend>
            <div>
                <div class="auto-style6">
                    <asp:Label ID="LblTitTrimestre" align="right" runat="server" Text="Trimestre a calcular:" Width="190px" CssClass="auto-style6"></asp:Label>
                    <asp:DropDownList ID="DdlTrimestre" runat="server" CssClass="DropGeneral" AutoPostBack="True" Height="23px" Width="356px" OnSelectedIndexChanged="DdlTrimestre_SelectedIndexChanged1">
                        <asp:ListItem Value="0">---Seleccionar-----</asp:ListItem>
                        <asp:ListItem Value="1">Primer Trimestre</asp:ListItem>
                        <asp:ListItem Value="2">Segundo Trimestre</asp:ListItem>
                        <asp:ListItem Value="3">Tercer Trimestre</asp:ListItem>
                        <asp:ListItem Value="4">Cuarto Trimestre</asp:ListItem>
                    </asp:DropDownList>

                    <asp:Button ID="BtnCalcular" runat="server" Text="Generar calculo" Width="202px" CssClass="Boton Marginleft" OnClick="BtnCalcular_Click" />

                </div>
                <br />
                <div class="auto-style6">
                    <asp:Label ID="LblTitAño" runat="server" align="right" Text="Año a calcular:" Width="190px" CssClass="auto-style6"></asp:Label>
                    <asp:TextBox ID="TxtAnio" runat="server" CssClass="auto-style5" Width="125px" Height="21px" onkeypress="return soloNumeros(event)" MaxLength="4"></asp:TextBox>
                </div>
                <br />

            </div>
        </fieldset>
    </div>


    <!-- mostrar datos tabla -->
    <div runat="server" id="DivMostrar" visible="false">
        <fieldset style="border-color: black; width: 95%; margin-left: 11px;">
            <legend style="width: auto; color: darkblue; font-size: 12px;">Valor en Libros del calculo de depreciación </legend>
            <br />
            <div class="auto-style2">
                <asp:Label ID="LblTitTotal" runat="server" Text="Total de Bienes:" Width="129px" CssClass="auto-style6"></asp:Label>

                <asp:Label ID="LblTotal" runat="server" Width="166px" CssClass="auto-style6" Font-Bold="True"></asp:Label>
            </div>
            <div style="float: right; width: 35%; height: 20px;">
                <asp:Button ID="BtnExportar" runat="server" Text="Exportar" CssClass="Boton Marginleft" OnClick="BtnExportar_Click" />
            </div>
            <br />
            <div class="auto-style2">
                <asp:Label ID="LblTitCosto" runat="server" Text="Costo Total:" Width="130px" CssClass="auto-style6"></asp:Label>
                <asp:Label ID="LblCostoTotal" runat="server" Width="180px" CssClass="auto-style6" Font-Bold="True"></asp:Label>
            </div>
            <div>

                <br />



                <asp:GridView ID="GridDepreciacion" CssClass="StyleGridV" runat="server" Width="100%"
                    AutoGenerateColumns="False">
                    <Columns>

                        <asp:TemplateField HeaderText="Grupo">
                            <ItemTemplate>
                                <asp:Label runat="server" Text='<%# (Convert.ToInt32(Eval("Grupo")) == 0) ? "" : Eval("Grupo")   %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle BackColor="#525252" ForeColor="White" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="SubGrupo">
                            <ItemTemplate>
                                <asp:Label runat="server" Text='<%# (Convert.ToInt32(Eval("SubGrupo")) == 0) ? "" : Eval("SubGrupo")   %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle BackColor="#525252" ForeColor="White" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Clase">
                            <ItemTemplate >
                                <asp:Label runat="server" Text='<%# (Convert.ToInt32(Eval("Clase")) == 0) ? "" : Eval("Clase")   %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle BackColor="#525252" ForeColor="White" />
                        </asp:TemplateField>
                        <asp:TemplateField  HeaderText="SubClase">
                            <ItemTemplate>
                                <asp:Label runat="server" Text='<%# (Convert.ToInt32(Eval("IdSubClase")) == 0) ? "" : Eval("IdSubClase")   %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle BackColor="#525252" ForeColor="White" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Consecutivo">
                            <ItemTemplate>
                                <asp:Label runat="server" Text='<%# (Convert.ToInt64(Eval("NumInventario")) == 0) ? "" : Eval("NumInventario")   %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle BackColor="#525252" ForeColor="White" />
                        </asp:TemplateField>

                        <%--<asp:BoundField DataField="Grupo" HeaderText="Grupo" />
                        <asp:BoundField DataField="SubGrupo" HeaderText="SubGrupo" />
                        <asp:BoundField DataField="Clase" HeaderText="Clase" />
                        <asp:BoundField DataField="IdSubClase" HeaderText="Subclase" />--%>

                     <%--   <asp:BoundField DataField="NumInventario" HeaderText="Consecutivo" />--%>
                        <asp:BoundField DataField="Descripcionbien" HeaderText="Denominación" >
                        <HeaderStyle BackColor="#525252" ForeColor="White" />
                        </asp:BoundField>
                        <asp:BoundField DataField="SaldoXDepreciar" HeaderText="Saldo por Depreciar" DataFormatString="{0:$ ##,###.######}" >
                        <HeaderStyle BackColor="#525252" ForeColor="White" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Añocompra" HeaderText="Año" Visible="False" />
                    </Columns>
                </asp:GridView>

            </div>


        </fieldset>

    </div>
    <div>
        <asp:HiddenField ID="HFTipoPartida" runat="server" />
        <asp:HiddenField ID="HFNameFile" runat="server" />
        <asp:HiddenField ID="HFStatus" Value="0" runat="server" />
    </div>

</asp:Content>
