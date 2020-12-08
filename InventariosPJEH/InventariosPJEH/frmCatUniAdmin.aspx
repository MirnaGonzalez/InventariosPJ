<%@ Page Title="" Language="C#" MasterPageFile="~/Inventarios1.Master" AutoEventWireup="true" CodeBehind="frmCatUniAdmin.aspx.cs" Inherits="InventariosPJEH.frmCatUniAdmin" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        .auto-style1 {
            height: 20px;
            width: 643px;
        }
        .auto-style2 {
            height: 20px;
            width: 261px;
        }
        .auto-style3 {
            width: 261px;
        }
        .auto-style4 {
            width: 643px;
        }
        .auto-style5 {
            width: 82%;
            height: 43px;
        }
        .auto-style6 {
            height: 20px;
            width: 302px;
        }
        .auto-style7 {
            width: 302px;
        }
        .auto-style8 {
            margin-top: 10px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <asp:ScriptManager ID="ScriptManager1" AsyncPostBackTimeout="3600" runat="server">
    </asp:ScriptManager>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
     
     <ContentTemplate>
          <div class="TempGralDivContenedor">
               <div class="TempGralDivGenerales">
                   <fieldset style="height:auto;">
                       
                        <asp:UpdatePanel ID="ConsultaPanel" runat="server">
                            <ContentTemplate>

                                 <div class="TempGralDivContenedor">
            <div class="TempGralDivGenerales">
                <fieldset style="height:100px;">
                    <legend>Consultar por: </legend>
                    <div class="TempGralDivContGenerales">
              
                    <div class="TempGralDivContGenerales">
                        &nbsp
                        <br />
                        <table class="auto-style5">
                            <tr>
                                <td class="auto-style2">
                                    <asp:Label ID="LblSección" runat="server" CssClass="TempGralLblGenerales4" Text="Tipo de Área : "></asp:Label>
                                </td>
                                <td class="auto-style1">
                                    <asp:DropDownList ID="ddlSeccion" runat="server" Height="16px" Width="398px">
                                        <asp:ListItem Value="0">---------------------------</asp:ListItem>
                                        <asp:ListItem Value="1I">Primera Instancia</asp:ListItem>
                                        <asp:ListItem Value="2I"></asp:ListItem>
                                        <asp:ListItem Value="UA">Segunda Instancia</asp:ListItem>
                                        <asp:ListItem Value="TE">Unidad Administrativa</asp:ListItem>
                                        <asp:ListItem Value="TF">Tribunal ElectoralTribunal Fiscal</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td class="auto-style2">
                                    <asp:Label ID="LblDistrito" runat="server" CssClass="TempGralLblGenerales4" Text="Distrito : "></asp:Label>
                                </td>
                                <td class="auto-style1">
                                    <asp:DropDownList ID="DropDistrito" runat="server" AutoPostBack="True" CssClass="auto-style8" Height="17px" OnSelectedIndexChanged="DropDistrito_SelectedIndexChanged" Visible="False" Width="398px">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td class="auto-style3">
                                    <asp:Label ID="LblUniAdmin" runat="server" CssClass="TempGralLblGenerales4" Text="Unidad Administrativa : "></asp:Label>
                                </td>
                                <td class="auto-style4">
                                    <asp:DropDownList ID="ddlUniAdmin" runat="server" Height="16px" Width="398px">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                        </table>
                        <br />
                        <asp:Button ID="btnConsulta" runat="server" CssClass="Boton"  Text="Consultar" />
                    </div>
                              </div>

                </fieldset>
            </div>

        </div>
                                       
                                <div class="DivResultados">   
                                      <legend>Resultados: </legend>
                                     <asp:GridView ID="GridUniAdmin" runat="server" AllowPaging="True" AlternatingRowStyle-CssClass="AlterS" AutoGenerateColumns="False" CssClass="StyleGridV" PagerStyle-CssClass="PagerS" PageSize="10" Width="100%">
                                         <Columns>
                                             <asp:BoundField DataField="Num" HeaderStyle-Width="30px" HeaderText="No." ItemStyle-Width="30px" />
                                            <asp:BoundField DataField="Seccion" HeaderText="Sección" />
                                             <asp:BoundField DataField="UniAdmin" HeaderText="Unidad Administrativa" />
                                             <asp:BoundField DataField="Tipo" HeaderText="Tipo" />
                                            
                                             <asp:TemplateField HeaderStyle-Font-Size="7pt" HeaderText="Modificar" ItemStyle-Height="10px" ItemStyle-HorizontalAlign="Center">
                                                 <ItemTemplate>
                                                     <asp:CheckBox ID="ChkSeleccionar" runat="server" AutoPostBack="true"  Visible='<%# ((string)Eval("Estado") != "CERRADO") ? bool.Parse("True") : bool.Parse("False") %>' />
                                                 </ItemTemplate>
                                             </asp:TemplateField>
                                         </Columns>
                                     </asp:GridView>
                                </div>


               
                    <fieldset style="height:auto;">
                        

                        <legend>Modificar: </legend>
                        <div style="width:98%; float:left;">
                            <table class="auto-style5">
                                <tr>
                                    <td class="auto-style6">
                                        <asp:Label ID="lblSeccionMod" runat="server" CssClass="TempGralLblGenerales4" Text="Sección : "></asp:Label>
                                    </td>
                                    <td class="auto-style1">
                                        <asp:DropDownList ID="ddlSeccionMod" runat="server" Height="16px" Width="499px">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="auto-style6">
                                        <asp:Label ID="lblTipoMod" runat="server" CssClass="TempGralLblGenerales4" Text="Tipo: "></asp:Label>
                                    </td>
                                    <td class="auto-style1">
                                        <asp:DropDownList ID="ddlTipoMod" runat="server">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="auto-style7">
                                        <asp:Label ID="lblUniAdminMod" runat="server" CssClass="TempGralLblGenerales4" Text="Unidad Administrativa : "></asp:Label>
                                    </td>
                                    <td class="auto-style4">
                                        <asp:TextBox ID="txtUniAdmin" runat="server" ReadOnly="true" Width="91%"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                            <br />
                            <asp:Button ID="btnGuardar" runat="server" CssClass="Boton" Text="Guardar" />
                            <br />
                        </div>
                        </div>

                        <asp:HiddenField ID="HFIdUniAdmin" runat="server" />
                    </fieldset>
                    
                </div>



                            </ContentTemplate>
                        </asp:UpdatePanel>

                    </fieldset>
               </div>



          </div>

     </ContentTemplate>

    </asp:UpdatePanel>
   


</asp:Content>


<asp:Content ID="Content3" ContentPlaceHolderID="CPHTitulo" runat="server">
    <div>
        <div class="DivTituloCA">
            <asp:Label ID="LblTitulo" runat="server" Text="Unidades Administrativas" CssClass="LblTituloCA"></asp:Label>
        </div>
    </div>
</asp:Content>
     
           

