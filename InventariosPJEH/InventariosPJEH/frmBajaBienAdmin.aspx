<%@ Page Title="BajaBienAdmin" Language="C#" MasterPageFile="~/Inventarios1.Master" AutoEventWireup="true" CodeBehind="frmBajaBienAdmin.aspx.cs" Inherits="InventariosPJEH.frmBajaBienAdmin" EnableEventValidation="false" %>

<asp:Content ID="ContenedorBBA" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManagerBBA" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanelBBA" runat="server">
        <Triggers>
            <asp:PostBackTrigger ControlID="BtnImprimir" />
            <asp:PostBackTrigger ControlID="BtnImrpimirLista" />
            <asp:PostBackTrigger ControlID="BtnImpLista" />
        </Triggers>
        <ContentTemplate>
            <%-- Filtro de Busqueda --%>
            <div style="width: 98%; margin-left: 11px;">
                <div id="divAgregar1" style="width: 98%; margin-left: 5px;">
                    <fieldset style="border-color: black; width: 98%; margin-left: 5px;">
                        <legend style="width: auto; color: darkblue; font-size: 12px;">Filtro de Busqueda</legend>

                        <div id="divlblfiltro" style="width: 100%; margin-left: 5px;">

                            <asp:Label ID="LablNoInventario" runat="server" Text="Numero de Inventario:" Width="150px"></asp:Label>

                            <asp:TextBox ID="TextBoxNoInventario" runat="server" AutoPostBack="true"></asp:TextBox>
                            &nbsp
                            <asp:Button ID="BtnAgregar" CssClass="Boton" runat="server" Text="Agregar" OnClick="BtnAgregar_Click" Height="29px" Width="129px" />

                        </div>
                    </fieldset>
                </div>
                <%--Calculo de depreciación--%>
                <div id="divCalculoDepre" style="width: 98%; margin-left: 5px;">
                    <fieldset style="border-color: black; width: 98%; margin-left: 5px;">
                        <legend style="width: auto; color: darkblue; font-size: 12px;">Consulta de Bienes</legend>
                        <div id="Div2" style="width: 100%; margin-left: 5px;">

                            <div style="width:32%; float:left;">
                                <asp:RadioButton ID="RbRevisarBienes" runat="server" GroupName="ActividadBien" AutoPostBack="true" Text="Registro Individual" OnCheckedChanged="RbActividadBien_CheckedChanged" />
                            </div>
                            <div style="width:32%; float:left;">
                                <asp:RadioButton ID="RbConsultaBajaT" runat="server" GroupName="ActividadBien" AutoPostBack="true" Text="Consultar Baja Temporal" OnCheckedChanged="RbActividadBien_CheckedChanged" />
                            </div>
                            <div style="width:32%; float:left;">
                                <asp:RadioButton ID="RbConsultaBD" runat="server" GroupName="ActividadBien" AutoPostBack="true" Text="Consultar Baja Definitiva" OnCheckedChanged="RbActividadBien_CheckedChanged" />
                            </div>
                        </div>
                    </fieldset>
                    <%--Filtro--%>
                    <div id="Filtro" style="width: 50%; margin-left: 300px;">

                        <asp:TextBox ID="TxtNumInventario" runat="server" AutoPostBack="true" CssClass="TextBox" Height="22px" Width="183px"></asp:TextBox>
                        &nbsp;
                            <asp:Button ID="BtnFiltro" runat="server" CssClass="Boton" Height="28px" OnClick="BtnFiltro_Click" Text="Filtro" Width="100px" />

                    </div>

                    <%-- Registro Individual--%>
                    <div id="divGeneral" runat="server" visible="false" style="width:100%;">
                        <fieldset style="border-color: black; width: 98%; margin-left: 5px;">
                            <legend style="width: auto; color: darkblue; font-size: 12px;">Revisar Bienes</legend>
                            <%--Grid para mostrar datos --%>
                            <div id="divGrid" style="width: 95%; margin-left: 0px;">
                                <asp:GridView ID="GridRevisarBien" runat="server" AutoGenerateColumns="false" CssClass="StyleGridV table table-striped table-hover table-condensed small-top-margin" HorizontalAlign="Center" Width="100%" OnRowCommand="GridViewBBA_RowCommand">

                                    <Columns>
                                        <asp:BoundField DataField="NumInventario" HeaderText="Inventario" />
                                        <asp:BoundField DataField="DescripcionBien" HeaderText="Nombre del Bien" />
                                        <asp:BoundField DataField="Marca" HeaderText="Marca" />
                                        <asp:BoundField DataField="Modelo" HeaderText="Modelo" />
                                        <asp:BoundField DataField="Serie" HeaderText="Serie" />
                                        <asp:BoundField DataField="FechaAdquisicion" HeaderText="Fecha de Adquisición" />
                                        <asp:BoundField DataField="IdResguardo" HeaderText="Numero de Resguardo" />

                                        <asp:TemplateField HeaderText="Clasificación" ItemStyle-Width="80">
                                            <ItemTemplate>
                                                <asp:DropDownList ID="DdlClasificacion" runat="server" CommandArgument='<%# Container.DataItemIndex.ToString() %>'>
                                                    <asp:ListItem Text="--Selecionar--" Value="0"></asp:ListItem>
                                                    <asp:ListItem Text="Servible" Value="Servible"></asp:ListItem>
                                                    <asp:ListItem Text="Inservible" Value="Inservible"></asp:ListItem>
                                                </asp:DropDownList>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Baja Temporal" ItemStyle-Width="80">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="btnBajaTemporal" runat="server" CommandName="BajaTemporal" Height="40" ImageUrl="~/Imagenes/Generales/Temporal.png" Width="40" CommandArgument='<%# Container.DataItemIndex.ToString() %>' Visible='<%# ((int)Eval("IdResguardo") == 0) ? bool.Parse("True") : bool.Parse("False") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Baja Definitiva" ItemStyle-Width="80">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="btnBajaDefinitiva" runat="server" CommandName="BajaDefinitiva" Height="40" ImageUrl="~/Imagenes/Generales/definitiva.png" Width="40" CommandArgument='<%# Container.DataItemIndex.ToString() %>' Visible='<%# ((int)Eval("IdResguardo") == 0) ? bool.Parse("True") : bool.Parse("False") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Liberar Res guardo" ItemStyle-Width="80">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="btnLiberar" runat="server" CommandName="Liberar" Height="40" ImageUrl="~/Imagenes/Generales/Liberar.png" Width="40" CommandArgument='<%# Container.DataItemIndex.ToString() %>' Visible='<%# ((int)Eval("IdResguardo") == 0) ? bool.Parse("False") : bool.Parse("True") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Quitar" ItemStyle-Width="50">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="btnQuitar" runat="server" CommandName="Quitar" Height="40" ImageUrl="~/Imagenes/Generales/Quitar.png" Width="40" CommandArgument='<%# Container.DataItemIndex.ToString() %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:BoundField DataField="IdInventario" HeaderText="IdInventario">
                                            <ItemStyle CssClass="hidden" />
                                            <HeaderStyle CssClass="hidden" />
                                        </asp:BoundField>

                                        <asp:BoundField DataField="IdENSU" HeaderText="IdENSU">
                                            <ItemStyle CssClass="hidden" />
                                            <HeaderStyle CssClass="hidden" />

                                        </asp:BoundField>
                                    </Columns>
                                </asp:GridView>
                            </div>

                            <div id="divtotalBienes" runat="server" visible="true">

                                <asp:Label ID="LblTotal" runat="server" Text="Total de Bienes:" Width="110px"></asp:Label>

                                <asp:Label ID="LblNumBienes" runat="server" Width="50px"></asp:Label>

                                <div id="div1" style="width: 40%; margin-left: 400px;">
                                <asp:Button ID="BtnImprimir" CssClass="Boton" runat="server" Text="Imprimir Lista" OnClick="BtnImrpimirLista_Click" />
                                </div>
                            </div>
                        </fieldset>
                    </div>
                    <%--Consultar Baja Temporal--%>
                    <div id="divConsultaBT" runat="server" visible="false" style="width:100%;">
                        <fieldset style="border-color: black; width: 98%; margin-left: 5px;">
                            <legend style="width: auto; color: darkblue; font-size: 12px;">Consultar Baja Temporal</legend>
                            <%--Grid para mostrar datos --%>
                            <div id="div4" style="width: 100%; margin-left: 11px;">
                                <asp:GridView ID="GridBajaTemporal" runat="server" AutoGenerateColumns="false" CssClass="StyleGridV" HorizontalAlign="Center" Width="95%"
                                    DataKeyNames="IdInventario,NumInventario,DescripcionBien,Marca,Modelo,Serie,FechaAdquisicion,Clasificacion" OnRowCommand="GridBajaTemporal_RowCommand">

                                    <Columns>
                                        <asp:BoundField DataField="NumInventario" HeaderText="Inventario" />
                                        <asp:BoundField DataField="DescripcionBien" HeaderText="Nombre del Bien" />
                                        <asp:BoundField DataField="Marca" HeaderText="Marca" />
                                        <asp:BoundField DataField="Modelo" HeaderText="Modelo" />
                                        <asp:BoundField DataField="Serie" HeaderText="Serie" />
                                        <asp:BoundField DataField="FechaAdquisicion" HeaderText="Fecha de Adquisición" />
                                        <asp:BoundField DataField="Clasificacion" HeaderText="Clasificacion" />

                                        <asp:TemplateField HeaderText="Ubicacion" ItemStyle-Width="80">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="CheckBoxUbicacion" runat="server" OnCheckedChanged="CheckBoxUbicacion_CheckedChanged" AutoPostBack="true" />
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Eliminar de Lista" ItemStyle-Width="100">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="btnEliminar" runat="server" CommandName="Eliminar" Height="40" ImageUrl="~/Imagenes/Generales/Eliminar.png" Width="40" CommandArgument='<%# Container.DataItemIndex.ToString() %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:BoundField DataField="IdInventario" HeaderText="IdInventario">
                                            <ItemStyle CssClass="hidden" />
                                            <HeaderStyle CssClass="hidden" />
                                        </asp:BoundField>

                                        <asp:BoundField DataField="TipoPartida" HeaderText="TipoPartida">
                                            <ItemStyle CssClass="hidden" />
                                            <HeaderStyle CssClass="hidden" />
                                        </asp:BoundField>

                                    </Columns>
                                </asp:GridView>
                            </div>
                            <%--total de Bienes de la lista ya generada--%>
                            <div id="divlbl" runat="server" visible="true">
                                <div style="width:13%; float:left;">
                                    <asp:Label ID="LbTotalBienes" runat="server" Text="Total de Bienes:" Width="165px"></asp:Label>
                                </div>
                                <div style="width:5%; float:left;">
                                    <asp:Label ID="LblNumBiene" runat="server" Width="110px"></asp:Label>
                                </div>
                            </div>
                            &nbsp;
                            <%--UBICACION DE BIENES--%>

                            <div id="divUbicacionB" runat="server" visible="false">
                                <fieldset class="auto-style19" style="border-color: black; margin-left: 11px;">
                                    <legend style="width: auto; color: darkblue; font-size: 12px;">Ubicación de Bienes</legend>

                                    <div id="DivUbicacion" style="width: 100%; margin-left: 5px;">
                                        <div id="divlbl1" style="width:20%; float:left;">
                                            <asp:Label ID="NoResguardo" runat="server" Text="Numero de Resguardo:" Width="188px"></asp:Label>
                                        </div>
                                        <div id="divlbl2" style="width:75%; float:left;">
                                            <asp:TextBox ID="TxtNumResguardo" runat="server" AutoPostBack="true" CssClass="TextBox" OnTextChanged="TxtNumResguardo_TextChanged" Height="16px" Width="168px"></asp:TextBox>
                                        </div>
                                        &nbsp
                                    </div>
                                     
                                    <div id="di" style="width: 100%; margin-left: 5px;">
                                        &nbsp
                                        &nbsp
                                        <div id="divlbl0" style="width:25%; float:left;">
                                            <asp:Label ID="NombreResguardante" runat="server" Text="Nombre del Resguardante:" Width="229px"></asp:Label>
                                        </div>
                                        <div id="divlbl3"  style="width:65%; float:left;">
                                            
                                            <asp:Label ID="lblNomResguardante" runat="server" Width="586px"></asp:Label>
                                        </div>
                                        &nbsp
                                   </div>

                                   <div id="divl" style="width: 100%; margin-left: 5px;">
                                       &nbsp
                                       &nbsp
                                        <div id="divlbl4"  style="width:15%; float:left;">
                                            <asp:Label ID="LblGrupoAsignado" runat="server" Text="Grupo Asignado:" Width="199px"></asp:Label>
                                        </div>
                                        <div id="divlbl5"  style="width:85%; float:left;">
                                            <asp:DropDownList ID="DropGrupoAsignado" runat="server" Height="20px" Width="200px" AutoPostBack="true" OnSelectedIndexChanged="DropGrupoAsignado_SelectedIndexChanged"></asp:DropDownList>
                                        </div>
                                    </div>
                                    &nbsp
                                     &nbsp
                                    <div id="divlb" style="width: 100%; margin-left: 5px;">
                                        &nbsp
                                        <div id="divlbl6"  style="width:15%; float:left;">
                                            <asp:Label ID="Ubicacion" runat="server" Text="Ubicación:" Width="136px"></asp:Label>
                                        </div>
                                        <div id="divlbl7"  style="width:85%; float:left;">
                                            <asp:Label ID="lblENSU" runat="server" Text="Edificio-Nivel-Sección" Width="665px"></asp:Label>
                                        </div>
                                        &nbsp
                                     &nbsp
                                    </div>
                                    <div id="div2" style="width: 40%; margin-left: 350px;">
                                        <asp:Button ID="BntBT" CssClass="Boton" runat="server" Text="Baja Temporal" Width="164px" OnClick="BntBT_Click1" />
                                    </div>
                                </fieldset>
                            </div>

                            <%--BOTONES--%>
                                <div id="divBtn" runat="server" visible="true">
                                    <asp:Button ID="BtnImrpimirLista" CssClass="Boton" runat="server" Text="Imprimir Lista" OnClick="BtnImrpimirLista_Click" />
                                    <asp:HiddenField ID="HFNumResguardo" runat="server" />
                                    <asp:HiddenField ID="HFIdENSU" runat="server" />
                                    <asp:HiddenField ID="HFGrupo" runat="server" />
                                    <asp:HiddenField ID="HFIdEmpleado" runat="server" />
                                </div>

                        </fieldset>
                    </div>
                    <%--Consulta Baja Definitiva--%>
                    <div id="div6" runat="server" visible="false">
                        <fieldset style="border-color: black; margin-left: 11px;">
                            <legend style="width: auto; color: darkblue; font-size: 12px;">Consultar Baja Defnitiva</legend>
                            <%--Grid para mostrar bienes en baja temporal--%>
                            <div id="div8" runat="server" visible="true">
                                <asp:GridView ID="GridBajaDefinitiva" runat="server" AutoGenerateColumns="false" CssClass="StyleGridV" HorizontalAlign="Center" Width="100%"
                                    DataKeyNames="IdInventario,NumInventario,DescripcionBien,Marca,Modelo,Serie,FechaAdquisicion,Clasificacion" OnRowCommand="GridBajaDefinitiva_RowCommand">

                                    <Columns>
                                        <asp:BoundField DataField="NumInventario" HeaderText="Inventario" />
                                        <asp:BoundField DataField="DescripcionBien" HeaderText="Nombre del Bien" />
                                        <asp:BoundField DataField="Marca" HeaderText="Marca" />
                                        <asp:BoundField DataField="Modelo" HeaderText="Modelo" />
                                        <asp:BoundField DataField="Serie" HeaderText="Serie" />
                                        <asp:BoundField DataField="FechaAdquisicion" HeaderText="Fecha de Adquisición" />
                                        <asp:BoundField DataField="Clasificacion" HeaderText="Clasificacion" />

                                        <asp:TemplateField HeaderText="Eliminar1" ItemStyle-Width="80">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="btnEliminar" runat="server" CommandName="Eliminar1" Height="40" ImageUrl="~/Imagenes/Generales/Eliminar.png" Width="40" CommandArgument='<%# Container.DataItemIndex.ToString() %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:BoundField DataField="IdInventario" HeaderText="IdInventario">
                                            <ItemStyle CssClass="hidden" />
                                            <HeaderStyle CssClass="hidden" />
                                        </asp:BoundField>

                                    </Columns>
                                </asp:GridView>

                                <div id="divBtnImprimir" style="width: 100%; margin-left: 5px; ">

                                    <div id="divBtnImprimir1" style="width: 25%; margin-left: 5px; float:left;">
                                        <asp:Label ID="TotalBienes" runat="server" Text="Total de Bienes:" Width="175px"></asp:Label>
                                     </div>
                                    <div id="divBtnImprimir2" style="width: 30%; margin-left: 5px;">
                                        <asp:Label ID="TotaldeBienes" runat="server" Width="110px"></asp:Label>
                                    </div>
                                    <div id="divBtnImprimir3" style="width: 25%; margin-left: 5px; float:left;">
                                         &nbsp
                                        <asp:Label ID="LblNumOficio" runat="server" Text="Numero de Oficio:" Width="175px"></asp:Label>
                                    </div>
                                    <div id="divBtnImprimir4" style="width: 30%; margin-left: 5px;">
                                        <asp:TextBox ID="TxtNumOficio" runat="server" AutoPostBack="true" CssClass="TextBox"></asp:TextBox>
                                    </div>
                                    <div id="divBtnImprimir5" style="width: 70%; margin-left: 5px; float:left;">
                                        <asp:Button ID="BtnBD1" runat="server" CssClass="Boton" Text="Baja Definitiva" OnClick="BtnBD1_Click" />
                                    </div>
                                    <div id="divBtnImprimir6" style="width: 30%; margin-left: 5px;">
                                        <asp:Button ID="BtnImpLista" runat="server" CssClass="Boton" Text="Imprimir Lista" OnClick="BtnImrpimirLista_Click" />
                                    </div>
                                </div>

                            </div>

                        </fieldset>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="ContentTituloBBA" runat="server" ContentPlaceHolderID="CPHTitulo">
    <div style="width: auto; text-align: center; background-color: #d8d8d8;">
        <asp:Label ID="LblTituloBBA" runat="server" Text="BAJA DEFINITIVA / TEMPORAL DE BIENES" Font-Size="Large" Font-Bold="True" Font-Italic="True" ForeColor="#0C2261"></asp:Label>
    </div>
</asp:Content>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="HeadContent">
    <style type="text/css">
        .auto-style19 {
            width: 826px;
        }

        .auto-style20 {
            text-transform: uppercase;
        }
    </style>
</asp:Content>

