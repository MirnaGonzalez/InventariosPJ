<%@ Page Title="" Language="C#" MasterPageFile="~/Inventarios1.Master" AutoEventWireup="true" CodeBehind="frmAsignarResguardo.aspx.cs" Inherits="InventariosPJEH.frmAsignarResguardo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

    <asp:UpdatePanel ID="UpdatePanel" runat="server">
        <ContentTemplate>
            <div class="GralDivContenedor">
                <div style="width: 100%;">
                    <fieldset style="border-color: black; width: 95%; margin-left: 11px;">
                        <legend style="width: auto; color: darkblue; font-size: 12px;">Buscar Nombre del personal</legend>
                        <div>
                            <div style="height:30px; text-align:right;">
                                <asp:Button ID="BtnNuevaBusqueda" CssClass="Boton" runat="server" Text="Buscar nuevo" OnClick="BtnNuevaBusqueda_Click" />
                            </div>
                            <div style="height:30px;">
                                <div style="width: 15%; float: right; height: auto;">
                                    <asp:Label ID="LblFechRes" runat="server" Text=""></asp:Label>
                                </div>
                                <div style="width: 40%; float: right; height: auto; text-align: right;">
                                    <asp:Label ID="LblTitFech" runat="server" Text="Fecha de Resguardo:"></asp:Label>
                                </div>
                            </div>
                            <div  class="auto-style6">
                                <asp:Label ID="LblTipUnAdm" runat="server" Text="Tipo Unidad Administrativa: " Width="190px" CssClass="auto-style6"></asp:Label>
                                <asp:DropDownList ID="DdlTipUnAdm" runat="server" CssClass="Drop" OnSelectedIndexChanged="DdlTipUnAdm_SelectedIndexChanged" AutoPostBack="true">
                                    <asp:ListItem Value="0">---Seleccionar-----</asp:ListItem>
                                    <asp:ListItem Value="UA">Unidad Administrativa</asp:ListItem>
                                    <asp:ListItem Value="1I">Primera Instancia</asp:ListItem>
                                    <asp:ListItem Value="2I">Segunda Instancia</asp:ListItem>
                                    <asp:ListItem Value="TE">Tribunal Electoral</asp:ListItem>
                                    <asp:ListItem Value="TF">Tribunal Fiscal</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <br />
                            <div class="auto-style6">
                                <asp:Label ID="LblDist" runat="server" Text="Distrito: " Width="190px" CssClass="auto-style6"></asp:Label>
                                <asp:DropDownList ID="DdlDist" runat="server" CssClass="Drop" OnSelectedIndexChanged="DdlDist_SelectedIndexChanged" AutoPostBack="true">
                                </asp:DropDownList>
                            </div>
                            <br />
                            <div class="auto-style6">
                                <asp:Label ID="LblUniAdm" runat="server" Text="Unidad Administrativa: " Width="190px" CssClass="auto-style6"></asp:Label>
                                <asp:DropDownList ID="DdlUniAdmin" runat="server" CssClass="Drop" OnSelectedIndexChanged="DdlUniAdmin_SelectedIndexChanged" AutoPostBack="true">
                                </asp:DropDownList>
                            </div>
                            <br />
                            <div class="auto-style6">
                                <asp:Label ID="LblNomPersonal" runat="server" Text="Nombre del personal: " Width="190px" CssClass="auto-style6"></asp:Label>
                                <asp:DropDownList ID="DdlNomPers" runat="server" CssClass="Drop" AutoPostBack="true" OnSelectedIndexChanged="DdlNomPers_SelectedIndexChanged">
                                </asp:DropDownList>
                            </div>

                        </div>
                    </fieldset>
                </div>

                <div style="width:100%;">
                    <div style="float:left; width:45%;">
                        <fieldset style="border-color: black; width: 90%; margin-left: 11px;">
                            <legend style="width: auto; color: darkblue; font-size: 12px;">Buscar Inventario</legend>

                            <div id="DivOptionBuscar" style="text-align: center">
                                <asp:RadioButton ID="RdPartida" runat="server" Text="Partida/Subclase" GroupName="BusquedaInventario" OnCheckedChanged="RdBusquedInventario_CheckedChanged" AutoPostBack="true" />
                                &nbsp
                                <%--<asp:RadioButton ID="RdArea" runat="server" Text="Area de Resguardo" GroupName="BusquedaInventario" OnCheckedChanged="RdBusquedInventario_CheckedChanged" AutoPostBack="true" />--%>
                                &nbsp
                                <asp:RadioButton ID="RdNumInventario" Text="Numero de inventario" runat="server" GroupName="BusquedaInventario" OnCheckedChanged="RdBusquedInventario_CheckedChanged" AutoPostBack="true" />
                            </div>
                            <br/>
                            <div id="DivBuscarPartida" runat="server" visible="false">
                                <div class="auto-style6" style="text-align: left;">
                                    <asp:Label ID="LblPartida" runat="server" CssClass="auto-style6" Text="Partida: " Width="70px"></asp:Label>
                                    <asp:DropDownList ID="DdlPartida" runat="server" AutoPostBack="true" CssClass="Drop" Width="300px" OnSelectedIndexChanged="DdlPartida_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </div>
                                <br />
                                <div class="auto-style6" style="text-align: left;">
                                    <asp:Label ID="LblSubClase" runat="server" CssClass="auto-style6" Text="Subclase: " Width="70px"></asp:Label>
                                    <asp:DropDownList ID="DdlSubclase" runat="server" AutoPostBack="true" CssClass="Drop" Width="300px">
                                    </asp:DropDownList>
                                </div>
                                <br />
                                <div class="auto-style4" style="text-align: center">
                                    <asp:Button ID="BtnBusquedaPartida" runat="server" CssClass="Boton" OnClick="BtnBusquedaPartida_Click" Text="Buscar" />
                                </div>
                            </div>
                            <%--<div id="DivBuscarAreaResguardo" runat="server" visible="false">
                                <div class="auto-style6" style="text-align: left;">
                                    <asp:Label ID="LblAreaResInic" runat="server" CssClass="auto-style6" Text="Area de Resguardo Inicial: " Width="190px"></asp:Label>
                                    <asp:DropDownList ID="DdlAreaResInic" runat="server" CssClass="Drop">
                                    </asp:DropDownList>
                                </div>
                                <br />
                                <div class="auto-style4" style="text-align: center">
                                    <asp:Button ID="BtnAreaResguardo" runat="server" CssClass="Boton" OnClick="BtnAreaResguardo_Click" Text="Buscar" />
                                </div>
                            </div>--%>
                            <div id="DivBuscarNumInventario" runat="server" visible="false">
                                <div class="auto-style6" style="text-align: left;">
                                    <asp:Label ID="LblNumInv" runat="server" CssClass="auto-style6" Text="Número de Inventario: " Width="190px"></asp:Label>
                                    <asp:TextBox ID="TxtNumInv" runat="server"></asp:TextBox>
                                </div>
                                <br />
                                <div class="auto-style4" style="text-align: center">
                                    <asp:Button ID="BtnBuscarInv" runat="server" CssClass="Boton" OnClick="BtnBuscarInv_Click" Text="Buscar" />
                                </div>
                            </div>
                        
                        </fieldset>
                    </div>
                    <div style="float:left; width: 53%">
                        <div style="width:100%; height:240px;">
                            <div style="float: left; width: 100%">
                                <fieldset style="border-color: black; width: 97%;">
                                    <legend>Agregar grupo</legend>
                                    <div runat="server" style="width: 99%; float: left; height: auto;">
                                        <div style="text-align: center">
                                            <asp:Label ID="LblApartado" runat="server" Text=""></asp:Label>
                                            <asp:Label ID="LblUbicFis" runat="server" Text=""></asp:Label>
                                        </div>
                                        <br />
                                        <div style="margin-left: 10px;">
                                            <div>
                                                <asp:Label ID="LblEdif" runat="server" Text="Edificio:" Width="80px"></asp:Label>
                                                <asp:DropDownList ID="DdlEdificio" runat="server" Height="20px" Width="330px" OnSelectedIndexChanged="DdlEdificio_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                            </div>
                                            <br />
                                            <div>
                                                <asp:Label ID="LblNivel" runat="server" Text="Nivel:" Width="80px"></asp:Label>
                                                <asp:DropDownList ID="DdlNivel" runat="server" Height="20px" Width="330px" OnSelectedIndexChanged="DdlNivel_SelectedIndexChanged" AutoPostBack="true" ></asp:DropDownList>
                                            </div>
                                            <br />
                                            <div>
                                                <asp:Label ID="LblSeccion" runat="server" Text="Sección:" Width="80px"></asp:Label>
                                                <asp:DropDownList ID="DdlSeccion" runat="server" Height="20px" Width="330px"></asp:DropDownList>
                                            </div>
                                            <br />
                                            <div style="text-align: center">
                                                <asp:Button ID="BtnNuevoGrupo" runat="server" Text="Nuevo Grupo" CssClass="Boton" OnClick="BtnNuevoGrupo_Click" />
                                            </div>
                                        </div>
                                    </div>
                                </fieldset>
                            </div>
                            <%--<div style="float: left; width: 44%; margin-left:10px;">
                                <fieldset style="border-color: black; width: 95%;">
                                    <legend style="width: auto; color: darkblue; font-size: 12px; text-align: center;">Agregar</legend>
                                
                                    <div style="height:123px;">
                                        <div style="height:35px;">
                                            <asp:Label ID="LblGEdificio" runat="server" Text="Edificio:" Width="60px"></asp:Label>
                                            &nbsp
                                            <asp:Label ID="LblGNombreEdificio" runat="server" Text=""></asp:Label>
                                        </div>
                                        <div style="height:35px;">
                                            <asp:Label ID="LblGNivel" runat="server" Text="Nivel:" Width="60px"></asp:Label>
                                            &nbsp;
                                            <asp:Label ID="LblGNombreNivel" runat="server" Text=""></asp:Label>
                                        </div>
                                        <div style="height:35px;">
                                            <asp:Label ID="LblGSeccion" runat="server" Text="Sección:" Width="60px"></asp:Label>
                                            &nbsp;
                                            <asp:Label ID="LblGNombreSeccion" runat="server" Text=""></asp:Label>
                                        </div>
                                        </br>
                                    </div>
                                </fieldset>
                            </div>--%>
                        </div>
                    </div>
                </div>

                <div style="width: 100%; text-align: center;">
                    <div style="width: 80%; margin-left: 10%; height:18px;">
                        <div style="background-color: #00CC00; color: Black; float: left; width: 23%; margin-right: 10px;">
                            <%--<asp:Label ID="LblVerLocali" runat="server" BackColor="Green"></asp:Label>--%>
                            <asp:Label ID="LblLocali" runat="server" Text="Asignado a Resguardo"></asp:Label>
                        </div>

                        <div style="background-color: Red; color: Black; float: left; width: 23%; margin-right: 10px;">
                            <%--<asp:Label ID="LblRojNoLocali" runat="server" BackColor="Red"></asp:Label>--%>
                            <asp:Label ID="LblNoLocali" runat="server" Text="No localizado" BackColor="Red"></asp:Label>
                        </div>
                        <div style="background-color: Yellow; color: Black; float: left; width: 23%; margin-right: 10px;">
                            <%--<asp:Label ID="LblAmaRepa" runat="server" BackColor="Yellow"></asp:Label>--%>
                            <asp:Label ID="LblRepa" runat="server" Text="En reparacion" BackColor="Yellow"></asp:Label>
                        </div>
                        <div style="background-color: #0033CC; color: Black; float: left; width: 23%;">
                            <%--<asp:Label ID="LblAzulDev" runat="server" BackColor="Blue"></asp:Label>--%>
                            <asp:Label ID="LblDev" runat="server" Text="Devuelto de reparacion" BackColor="Blue"></asp:Label>
                        </div>
                    </div>
                </div>

                <div style="width: 100%;">
                    <div style="width: 100%; float:left">
                        <div style="float: left; width: 49%;">
                            <fieldset style="border-color: black; width: 90%;">

                                <legend style="width: auto; color: darkblue; font-size: 12px; text-align: center;">Disponibles</legend>

                                <div id="DivDisponibles" runat="server" style="width: 100%;">
                                    <div style="float: left; text-align:right; width:100%; height:40px;">
                                        <asp:Button ID="BtnAgregar" runat="server" Text="Agregar" CssClass="Boton" OnClick="BtnAgregar_Click" />
                                    </div>
                                    <div>
                                        <asp:GridView ID="GridDisponibles" runat="server" AutoGenerateColumns="false" CssClass="StyleGridV">
                                            <Columns>

                                                <asp:TemplateField ItemStyle-Height="10px" ItemStyle-Width="150px" ItemStyle-ForeColor="Black" HeaderText="Inventario">
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="ChkSeleccionar" runat="server" Text='<%# Eval("NumInventario") %>' OnCheckedChanged="ChkSelNumInventario_CheckedChanged" Checked='<%# Eval("Seleccionado") %>' AutoPostBack="true" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <%--<asp:BoundField DataField="NumInventario" HeaderText="Inventario" ItemStyle-Width="30" />--%>
                                                <asp:BoundField DataField="DescripcionBien" HeaderText="Descripcion" ItemStyle-Width="150" />



                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                            </fieldset>
                        </div>
                    

                        <div style="float: left; width: 49%">
                            <fieldset style="border-color: black; width: 95%; margin-left: 11px;">
                                <legend style="width: auto; color: darkblue; font-size: 12px; text-align: center;">Asignados</legend>
                                
                                <div id="Div1" runat="server" style="width: 100%;">
                                    <div style="height:30px;">
                                        <div style="float:left; width:100%; height:30px;">
                                            <asp:Label ID="LblGrupo" runat="server" Text="Grupo:" Width="80px"></asp:Label>
                                            <asp:DropDownList ID="DdlGrupo" runat="server" Width="40%" OnSelectedIndexChanged="DdlGrupo_SelectedIndexChanged" AutoPostBack="true">
                                            </asp:DropDownList>
                                        </div>
                                        
                                        
                                    </div>
                                    <div style="float:left; width:75%; height:60px;">
                                        <div style="float:left; width:100%; height:40px;">
                                            <asp:Label ID="LblENSU" runat="server" Text=""></asp:Label>
                                        </div>
                                        <div style="float:left; width:100%; height:20px;">
                                            <asp:Label ID="LblIdResguardo" runat="server" Text="Num de Resguardo"></asp:Label>
                                            <asp:Label ID="LblResguardo" runat="server"></asp:Label>
                                        </div>
                                        
                                    </div>
                                    <div style="float: left; text-align:right; width:23%; height:40px;">
                                        <asp:Button ID="BtnQuitar" runat="server" Text="Quitar" CssClass="Boton" OnClick="BtnQuitar_Click" />
                                    </div>
                                    <div>
                                        <asp:GridView ID="GridAsignados" runat="server" AutoGenerateColumns="false" CssClass="StyleGridV" OnRowDataBound="GridAsignados_RowDataBound" OnRowCommand="GridAsignados_RowCommand" OnSelectedIndexChanged="GridAsignados_SelectedIndexChanged">
                                            <Columns>
                                        
                                                <%--<asp:BoundField DataField="NumInventario" HeaderText="Inventario" ItemStyle-Width="150" ItemStyle-ForeColor="Black" ItemStyle-Font-Bold="true"/>--%>
                                                <asp:TemplateField ItemStyle-Height="10px" ItemStyle-Width="250px" HeaderText="Inventario">
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="ChkSeleccionar" runat="server" Text='<%# Eval("NumInventario") %>' OnCheckedChanged="ChkSelNumInventarioAsignado_CheckedChanged" Checked='<%# Eval("Seleccionado") %>' AutoPostBack="true" ForeColor="Black" Font-Bold="True" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:BoundField DataField="DescripcionBien" HeaderText="Descripción" ItemStyle-Width="150" ItemStyle-ForeColor="Black" ItemStyle-Font-Bold="true"/>

                                                <asp:TemplateField ItemStyle-Height="10px" ItemStyle-Width="50px" HeaderText="Estatus">
                                                    <ItemTemplate>
                                                        <asp:DropDownList ID="DdlTipoActividad" runat="server" CssClass="Drop" Width="150px" SelectedValue='<%# Bind("IdActividad") %>' >
                                                            <asp:ListItem Value="2">Asignado a Resguardo</asp:ListItem>
                                                            <asp:ListItem Value="3">Enviado a Reparación</asp:ListItem>
                                                            <asp:ListItem Value="4">Devuelto de Reparación</asp:ListItem>
                                                            <asp:ListItem Value="5">Perdido - No Localizado</asp:ListItem>
                                                            <asp:ListItem Value="6">Baja Temporal</asp:ListItem>
                                                            <asp:ListItem Value="8">Bienes sin Reparación</asp:ListItem>
                                                            <asp:ListItem Value="9">Enviados para Baja</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Guardar" HeaderStyle-Font-Size="7pt">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="ImgGuardar" runat="server" ImageUrl="~/Imagenes/Generales/guardar.png" Width="32px" Height="32px" CommandName="CNGuardar" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                        
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                            </fieldset>


                        </div>
                    </div>

                    <div style="width: 100%;">
                        <fieldset>
                            <div runat="server" style="width: 100%; float: left; height: auto; text-align: center;">
                                <asp:Button ID="BtnImprimir" runat="server" Text="Imprimir" CssClass="Boton" OnClick="BtnImprimir_Click" />
                               <%-- <div runat="server" style="width: 33%; float: left; height: auto; text-align: center;">
                                    <%--<asp:Button ID="BtnGuardar" runat="server" Text="Guardar" CssClass="Boton" />--%>
                                <%--</div>
                                <div runat="server" style="width: 33%; float: left; height: auto; text-align: center;">--%>
                                    <%--<asp:Button ID="BtnCancelar" runat="server" Text="Cancelar" CssClass="Boton" />--%>
                                    
                              <%--  </div>--%>
                              <%--  <div runat="server" style="width: 33%; float: left; height: auto; text-align: center;">
                                    
                                </div>--%>
                            </div>
                        </fieldset>
                    </div>

                    <asp:HiddenField ID="HFIdENSU" runat="server" />
                    <asp:HiddenField ID="HFIdResguardo" runat="server" />
                    <asp:HiddenField ID="HFIdUsuario" runat="server" />
                    <asp:HiddenField ID="HFPerfil" runat="server" />
                    <asp:HiddenField ID="HFIdUniAdmin" runat="server" />

                </div>
            </div>
        </ContentTemplate>

    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPHTitulo" runat="server">
    <div style="width: auto; text-align: center; background-color: #d8d8d8;">
        <asp:Label ID="LblLema" runat="server" Text="REGISTRO DE RESGUARDO" Font-Size="Large" Font-Bold="True" Font-Italic="True" ForeColor="#6D252B"></asp:Label>
    </div>
</asp:Content>
