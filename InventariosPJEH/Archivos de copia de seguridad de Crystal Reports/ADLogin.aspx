<%@ Page Title="" Language="C#" MasterPageFile="~/Inventarios0.Master" AutoEventWireup="true" CodeBehind="ADLogin.aspx.cs" Inherits="InventariosPJEH.ADLogin" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>

    <asp:HiddenField ID="HFPerfil" runat="server" />

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            
            <div style="width:auto; text-align:center;">
                <asp:Label ID="LblLema" runat="server" Text="UN PODER JUDICIAL HUMANO, ACCESIBLE, TRANSPARENTE Y EFICIENTE" Font-Size="Large" Font-Bold="True" Font-Italic="True" ForeColor="#0C2261"></asp:Label>
            </div>
            <div style="width:auto; text-align:center; margin-bottom:25px;">
                <img src="Imagenes/Generales/BarraLema2.png" height="10px" width="350px"/>
                <asp:Label ID="Label" runat="server" Text="" Width="50px" ></asp:Label>
                <img src="Imagenes/Generales/BarraLema2.png" height="10px" width="350px"/>
            </div>

            <div style="height:220px">
                <div style="width:100%; float:left; height:auto; ">
                    
                    <div style="float:left; height:200px; width:25%;">
                    </div>

                    <div style="text-align:center; float:left; width:48%;">
                        <fieldset style="width:150px; margin:auto; border-color:#0C2261;">
                            <legend></legend>
                                
                            <div style="width:315px; text-align:center;">
                                <asp:Label ID="LblBienvenido" runat="server" Text="BIENVENIDO" Font-Bold="True" Font-Size="Medium" ForeColor="#0C2261"></asp:Label>
                            </div>

                            <div style="float:left; width: 320px;">
                                <div style="float:left; width: 160px;">
                                    <div style="width: 160px; text-align:left;">
                                        <asp:Label ID="LblUsuario" runat="server" AssociatedControlID="TxtNomUsuario" Text="Usuario: "></asp:Label>
                                        <asp:TextBox ID="TxtNomUsuario" runat="server" CssClass="textEntry"></asp:TextBox>
                                    </div>
                                
                                    &nbsp

                                    <div style="width: 160px; text-align:left;">
                                        <asp:Label ID="LblContrasena" runat="server" AssociatedControlID="TxtContrasena" Text="Contraseña: "></asp:Label>
                                        <asp:TextBox ID="TxtContrasena" runat="server" CssClass="passwordEntry" TextMode="Password"></asp:TextBox>
                                    </div>
                                </div>

                                <div style="float:left; width: 160px;">
                                    <img src="Imagenes/Generales/Login.png" width="110"/>
                                </div>
                            </div>

                            <div style="width: 320px; margin-top:15px;">
                                <asp:Button ID="BtnEntrar" runat="server" Text="Entrar" CssClass="Boton" OnClick="BtnEntrar_Click" />
                            </div>
                        </fieldset>
                    </div>

                    <div style="float:left; height:200px; width:25%;">
                    </div>

                </div>

            </div>
              <!-- <div>
            <asp:GridView ID="gvdEjem" runat="server" AutoGenerateColumns="true"> </asp:GridView>

            </div>   -->
            <div style="float:left; width:30%;">
                <asp:Label ID="Label3" runat="server" Text="Versión 1.0.1 <br/> <br/>" Font-Bold="true" Font-Size="Medium" Width="90%"></asp:Label>
                <asp:Label ID="Label4" runat="server" Text="Derechos Reservados <br/> Dirección de Modernización y Sistemas (Subdirección de sistemas) <br/> (771) 71 7 90 00 Ext. 9510 <br/>  modsis@pjhidalgo.gob.mx"></asp:Label>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>
<asp:Content ID="Content3" runat="server" contentplaceholderid="CPHTitulo">
    <div>
        <div class="DivTituloCA">
            <asp:Label ID="LblTitulo" runat="server" Text="" CssClass="LblTituloCA"></asp:Label>
        </div>
    </div>
</asp:Content>

