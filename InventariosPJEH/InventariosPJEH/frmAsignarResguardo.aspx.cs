using InventariosPJEH.CAccesoDatos;
using InventariosPJEH.CNegocios;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace InventariosPJEH
{
    public partial class frmAsignarResguardo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                //LlenarPartidas();
                LlenarEdificio();
                //LlenarSeccion();
                HFIdUsuario.Value = "1"; // Page.Session["IdUsuario"].ToString();
                HFPerfil.Value = "Admin";

                LblFechRes.Text = DateTime.Now.ToShortTimeString();
            }

        }

        /// <summary>
        /// Función para mostra las notificaciones necesarias al usuario
        /// </summary>
        /// <param name="Mensaje">El mensaje que se requiere mostrar al usuario.</param>
        /// <param name="Tipo">Tipo de mensaje a mostrar (info, error, etc)</param>
        /// <param name="TipoFuncion">Tipo de función a ejecutar (Normal, Intervalo) el de intervalo se muestra solo para los mensajes a mostrar en un postback</param>
        /// <param name="ClaveMsj">Clave con la que se va a diferenciar cada notificación a mostrar.</param>
        protected void MostrarMensaje(string Mensaje, string Tipo, string TipoFuncion, string ClaveMsj)
        {
            string Msj = "";
            if (TipoFuncion == "Normal")
                Msj = "MostrarMensaje('" + Mensaje + "', '" + Tipo + "');";
            else
                Msj = "MostrarMensajeInterval('" + Mensaje + "', '" + Tipo + "');";

            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), ClaveMsj, Msj, true);
        }

        protected void LlenarPartidas()
        {

            CUsuario Usuario = Page.Session["Usuario"] as CUsuario;
            int TipoPartida = Convert.ToInt32(Usuario.TipoPartida);

            List<CConsultaBienes> LPartidas = new List<CConsultaBienes>();
            LPartidas = BdAsignarResguardo.ObtenerPartida(TipoPartida);
            DdlPartida.DataSource = LPartidas;
            DdlPartida.DataTextField = "Partida";
            DdlPartida.DataValueField = "idPartida";
            DdlPartida.DataBind();
            DdlPartida.Items.Insert(0, new ListItem("-- Seleccionar partida --", "0"));

        }

        protected void LlenarEdificio()
        {
            DataTable TEdificios = new DataTable();
            TEdificios = BdAsignarResguardo.ObtenerEdificio();
            DdlEdificio.DataSource = TEdificios;
            DdlEdificio.DataTextField = "Edificio";
            DdlEdificio.DataValueField = "IdEdificio";
            DdlEdificio.DataBind();
            DdlEdificio.Items.Insert(0, new ListItem("-- Seleccionar edificio --", "0"));
        }

        //protected void LlenarSeccion()
        //{
        //    DataTable TEdificios = new DataTable();
        //    TEdificios = BdConsultaBienes.ObtenerSecciones();
        //    DdlSeccion.DataSource = TEdificios;
        //    DdlSeccion.DataTextField = "Seccion";
        //    DdlSeccion.DataValueField = "IdSeccion";
        //    DdlSeccion.DataBind();
        //    DdlSeccion.Items.Insert(0, new ListItem("-- Seleccionar sección --", "0"));
        //}
        
        //protected void LlenarAreaResguardo()
        //{
        //    List<CConsultaBienes> LAreaResguardo = new List<CConsultaBienes>();
        //    LAreaResguardo = BdConsultaBienes.ObtenerAreaResguardo();
        //    DdlAreaResInic.DataSource = LAreaResguardo;
        //    DdlAreaResInic.DataTextField = "Nombre";
        //    DdlAreaResInic.DataValueField = "IdSeccion";
        //    DdlAreaResInic.DataBind();
        //    DdlAreaResInic.Items.Insert(0, new ListItem("-- Seleccionar Area de Resguardo --", "0"));

        //}

        protected void DdlTipUnAdm_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            string IdTipo = (sender as DropDownList).SelectedValue;
            if (IdTipo == "1I")
            {
                DataTable Distritos = new DataTable();
                DdlDist.Visible = true;
                LblDist.Visible = true;
                Distritos = BdAsignarResguardo.ObtenerDistritosXTipoUnidad(IdTipo);
                DdlDist.DataSource = Distritos;
                DdlDist.DataTextField = "Distrito";
                DdlDist.DataValueField = "IdDistrito";
                DdlDist.DataBind();
                DdlDist.Items.Insert(0, new ListItem("Seleccionar", "0"));
            }
            else
            {
                //int IdDistrito = Convert.ToInt32(DdlDist.SelectedValue);
                //string Clasificacion = DdlTipUnAdm.SelectedValue;
                string Clasificacion = IdTipo;
                DdlDist.Visible = false;
                LblDist.Visible = false;
                DataTable UnidadesAdmin = new DataTable();
                //UnidadesAdmin = BdAsignarResguardo.ObtenerUnidAdminXDistritoXClasificacion(IdDistrito, Clasificacion);
                UnidadesAdmin = BdAsignarResguardo.ObtenerUnidAdminXClasificacion(Clasificacion);
                DdlUniAdmin.DataSource = UnidadesAdmin;
                DdlUniAdmin.DataTextField = "UniAdmin";
                DdlUniAdmin.DataValueField = "IdUniAdmin";
                DdlUniAdmin.DataBind();
                DdlUniAdmin.Items.Insert(0, new ListItem("Seleccionar", "0"));
            }
        }

        protected void DdlDist_SelectedIndexChanged(object sender, EventArgs e)
        {
            int IdDistrito = Convert.ToInt32(DdlDist.SelectedValue);
            string Clasificacion = DdlTipUnAdm.SelectedValue;

            DataTable UnidadesAdmin = new DataTable();
            UnidadesAdmin = BdAsignarResguardo.ObtenerUnidAdminXDistritoXClasificacion(IdDistrito, Clasificacion);
            DdlUniAdmin.DataSource = UnidadesAdmin;
            DdlUniAdmin.DataTextField = "UniAdmin";
            DdlUniAdmin.DataValueField = "IdUniAdmin";
            DdlUniAdmin.DataBind();
            DdlUniAdmin.Items.Insert(0, new ListItem("Seleccionar", "0"));
        }

        protected void DdlUniAdmin_SelectedIndexChanged(object sender, EventArgs e)
        {
            int IdUnidadAdmin = Convert.ToInt32((sender as DropDownList).SelectedValue);
            

            if (IdUnidadAdmin != 0)
            {
                HFIdUniAdmin.Value = IdUnidadAdmin.ToString();
                DataTable Personal = new DataTable();
                Personal = BdAsignarResguardo.ObtenerPersonalXUnidAdmin(IdUnidadAdmin);
                DdlNomPers.DataSource = Personal;
                DdlNomPers.DataTextField = "NombrePersonal";
                DdlNomPers.DataValueField = "IdEmpleado";
                DdlNomPers.DataBind();
                DdlNomPers.Items.Insert(0, new ListItem("Seleccionar", "0"));
            }
            else
            {
                HFIdUniAdmin.Value = "";
                MostrarMensaje("Debe seleccionar una unidad", "error", "Normal", "ErrorUnidadAdmin");
            }
        }

        protected void DdlPartida_SelectedIndexChanged(object sender, EventArgs e)
        {
            int IdPartida = Convert.ToInt32((sender as DropDownList).SelectedValue);

            DataTable SubClase = new DataTable();
            SubClase = BdAsignarResguardo.ObtenerSubClasePartida(IdPartida);
            DdlSubclase.DataSource = SubClase;
            DdlSubclase.DataTextField = "SubClase";
            DdlSubclase.DataValueField = "IdSubClase";
            DdlSubclase.DataBind();
            DdlSubclase.Items.Insert(0, new ListItem("Seleccionar", "0"));

        }
        
        protected void BtnBuscarInv_Click(object sender, EventArgs e)
        {
            if (TxtNumInv.Text != "")
                BusquedaXInventario();
            else
                MostrarMensaje("Debe introducir el número de inventario a buscar.", "info", "Normal", "NumInventarioNoSeleccionado");
        }

        protected void BusquedaXInventario()
        {
            Int64 NumInventario = -1;

            if (Int64.TryParse(TxtNumInv.Text, out NumInventario))
            {
                
                DataTable TEdificios = new DataTable();

                TResultado Resultado = new TResultado();
                List<CFichaBien> LFichaBien = new List<CFichaBien>();
                LFichaBien = BdAsignarResguardo.ObtenerFichaBienXNumInventario(NumInventario, ref Resultado);
                if (Resultado.Exito)
                {
                    if(LFichaBien.Count == 0)
                        MostrarMensaje("No se encontraron resultados para el número de inventario proporcionado. .", "error", "Normal", "NotExitoso");
                    else
                        MostrarMensaje("La informacion se obtuvo correctamente .", "info", "Normal", "NotExitoso");
                }
                else
                    MostrarMensaje(Resultado.Mensaje.FirstOrDefault(), "error", "Normal", "NotExitoso");

                GridDisponibles.DataSource = LFichaBien;
                GridDisponibles.DataBind();

                Page.Session["LFichaBienDisponibles"] = LFichaBien;
            }
        }

        protected void ChkSelNumInventario_CheckedChanged(object sender, EventArgs e)
        {
            List<CFichaBien> LFichaBien = new List<CNegocios.CFichaBien>();
            LFichaBien = Page.Session["LFichaBienDisponibles"] as List<CFichaBien>;
            CheckBox ChkSeleccionar = sender as CheckBox;

            if (ChkSeleccionar.Checked)
                LFichaBien.FirstOrDefault(x => x.NumInventario == Convert.ToInt64(ChkSeleccionar.Text)).Seleccionado = true;
            else
                LFichaBien.FirstOrDefault(x => x.NumInventario == Convert.ToInt64(ChkSeleccionar.Text)).Seleccionado = false;

            Page.Session["LFichaBienDisponibles"] = LFichaBien;
        }

        protected void ChkSelNumInventarioAsignado_CheckedChanged(object sender, EventArgs e)
        {
            List<CFichaBien> LFichaBienAsignados = new List<CNegocios.CFichaBien>();
            LFichaBienAsignados = Page.Session["LFichaBienAsignados"] as List<CFichaBien>;
            CheckBox ChkSeleccionar = sender as CheckBox;

            if (ChkSeleccionar.Checked)
                LFichaBienAsignados.FirstOrDefault(x => x.NumInventario == Convert.ToInt64(ChkSeleccionar.Text)).Seleccionado = true;
            else
                LFichaBienAsignados.FirstOrDefault(x => x.NumInventario == Convert.ToInt64(ChkSeleccionar.Text)).Seleccionado = false;

            Page.Session["LFichaBienDisponibles"] = LFichaBienAsignados;
        }

        protected void RdBusquedInventario_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton RdBusqueda = sender as RadioButton;

            if (RdBusqueda.ID == "RdPartida")
            {
                DdlPartida.Items.Clear();
                DdlSubclase.Items.Clear();
                DivBuscarPartida.Visible = true;
                //DivBuscarAreaResguardo.Visible = false;
                DivBuscarNumInventario.Visible = false;
                LlenarPartidas();
            }
            else if (RdBusqueda.ID == "RdArea")
            {
                DivBuscarPartida.Visible = false;
                //DivBuscarAreaResguardo.Visible = true;
                DivBuscarNumInventario.Visible = false;
                //LlenarAreaResguardo();
            }
            else if (RdBusqueda.ID == "RdNumInventario")
            {
                DivBuscarPartida.Visible = false;
                //DivBuscarAreaResguardo.Visible = false;
                DivBuscarNumInventario.Visible = true;
                TxtNumInv.Text = "";
            }
            GridDisponibles.DataSource = null;
            GridDisponibles.DataBind();
        }

        protected void BtnBusquedaPartida_Click(object sender, EventArgs e)
        {
            //if (DdlSubclase.SelectedValue != "0")
            //{
            //    List<CFichaBien> LFichaBien = new List<CFichaBien>();
            //    LFichaBien = BdConsultaBienes.ObtenerFichaBienXSubClase(Convert.ToInt32(DdlSubclase.SelectedValue));

            //    GridDisponibles.DataSource = LFichaBien;
            //    GridDisponibles.DataBind();

            //    Page.Session["LFichaBienDisponibles"] = LFichaBien;
            //}

            BusquedaXPartida();
        }

        protected void BusquedaXPartida()
        {
            if (DdlSubclase.SelectedValue != "0")
            {

              
                List<CFichaBien> LFichaBien = new List<CFichaBien>();
                LFichaBien = BdAsignarResguardo.ObtenerFichaBienXSubClase(Convert.ToInt32(DdlSubclase.SelectedValue));

                if (LFichaBien.Count > 0)
                {
                    GridDisponibles.DataSource = LFichaBien;
                    GridDisponibles.DataBind();

                    Page.Session["LFichaBienDisponibles"] = LFichaBien;
                }
                else
                {
                    MostrarMensaje("No se encontró inventario disponible", "info", "Normal", "ErrorInventarioNoDisponible");
                }
            }
        }

        //protected void BtnAreaResguardo_Click(object sender, EventArgs e)
        //{

        //}

        protected void DdlNomPers_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DdlNomPers.SelectedValue != "0")
            {
                //LlenarGridAsignados(Convert.ToInt32(DdlNomPers.SelectedValue));
                LlenarDdlGrupo(Convert.ToInt32(DdlNomPers.SelectedValue));
            }

        }

        protected void LlenarGridAsignados(int IdEmpleado)
        {

            DataTable TEdificios = new DataTable();

            TResultado Resultado = new TResultado();
            List<CFichaBien> LFichaBien = new List<CFichaBien>();
            List<CFichaBien> LFichaBienSelect = new List<CFichaBien>();
            LFichaBien = BdAsignarResguardo.ObtenerFichaBienXIdEmpleado(IdEmpleado,ref Resultado);
            if (!Resultado.Exito)
                MostrarMensaje(Resultado.Mensaje.FirstOrDefault(), "error", "Normal", "NotExitoso");

            if (DdlGrupo.SelectedValue != "0" && DdlGrupo.SelectedValue != "")
            {
                LFichaBienSelect = (from FB in LFichaBien
                                    where FB.IdGrupo == Convert.ToInt32(DdlGrupo.SelectedValue)
                                    select FB).ToList();
            }
            else
                LFichaBienSelect = LFichaBien;
            if (LFichaBienSelect.Count > 0)
            {
                GridAsignados.DataSource = LFichaBienSelect;

                GridAsignados.DataBind();
                Page.Session["LFichaBienAsignados"] = LFichaBien;
            }
            else
            {
                MostrarMensaje("El personal seleccionado no cuenta con resguardo asignado.", "info", "Normal", "NotExitoso");
                
                GridAsignados.DataSource = null;

                GridAsignados.DataBind();
            }
        }

        protected void LlenarDdlGrupo(int IdEmpleado)
        {

            DataTable TEdificios = new DataTable();

            TResultado Resultado = new TResultado();

            List<CGrupoBienResguardo> LGrupoBienR = new List<CGrupoBienResguardo>();
            LGrupoBienR = BdAsignarResguardo.ObtenerGrupoBienRXIdEmpleado(IdEmpleado, ref Resultado);
            if (!Resultado.Exito)
                MostrarMensaje(Resultado.Mensaje.FirstOrDefault(), "error", "Normal", "NotExitoso");

            if (LGrupoBienR.Count > 0)
            {
                DdlGrupo.DataSource = LGrupoBienR;

                DdlGrupo.DataTextField = "Grupo";
                DdlGrupo.DataValueField = "IdGrupo";
                DdlGrupo.DataBind();
                DdlGrupo.Items.Insert(0, new ListItem(" -- Seleccionar grupo -- ", "0"));
                DdlGrupo.SelectedIndex = 1;

                Page.Session["LGrupoBienR"] = LGrupoBienR;

                //List<CGrupoBienResguardo> LGrupoBienR = Page.Session["LGrupoBienR"] as List<CGrupoBienResguardo>;

                CGrupoBienResguardo GrupoBienR = (from GBR in LGrupoBienR
                                                  where GBR.IdGrupo == Convert.ToInt32(DdlGrupo.SelectedValue)
                                                  select GBR).FirstOrDefault();

                LblENSU.Text = GrupoBienR.Edificio + " - " + GrupoBienR.Nivel + " - " + GrupoBienR.Seccion;
                LblResguardo.Text = GrupoBienR.IdResguardo.ToString();

                LlenarGridAsignados(Convert.ToInt32(DdlNomPers.SelectedValue));
            }
            else
            {
                MostrarMensaje("El personal seleccionado no cuenta con resguardo asignado.", "info", "Normal", "NotExitoso");

                DdlGrupo.Items.Clear();
                LblResguardo.Text = "";
                LblENSU.Text = "";
                GridAsignados.DataSource = null;
                GridAsignados.DataBind();
            }
        }

        protected void GridAsignados_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //System.Data.DataRow row = ((System.Data.DataRowView)e.Row.DataItem).Row;
                CFichaBien FilaFichaBien = e.Row.DataItem as CFichaBien;
                if (FilaFichaBien.IdActividad == 2)
                    e.Row.BackColor = System.Drawing.Color.FromArgb(255, 0, 204, 0); //System.Drawing.Color.Green;
                else if (FilaFichaBien.IdActividad == 3 || FilaFichaBien.IdActividad == 8 || FilaFichaBien.IdActividad == 9)
                    e.Row.BackColor = System.Drawing.Color.Yellow;
                else if (FilaFichaBien.IdActividad == 4)
                    e.Row.BackColor = System.Drawing.Color.FromArgb(255, 0, 51, 204); //System.Drawing.Color.Blue;
                else if (FilaFichaBien.IdActividad == 5)
                    e.Row.BackColor = System.Drawing.Color.Red;

           

            }
        }

        protected void BtnNuevoGrupo_Click(object sender, EventArgs e)
        {
            if (DdlEdificio.SelectedValue != "0" && DdlNivel.SelectedValue != "0" && DdlSeccion.SelectedValue != "0")
            {
                if (DdlNomPers.SelectedValue != "" && DdlNomPers.SelectedValue != "0")
                {
                    int IdEdificio = Convert.ToInt32(DdlEdificio.SelectedValue);
                    int IdNivel = Convert.ToInt32(DdlNivel.SelectedValue);
                    int IdSeccion = Convert.ToInt32(DdlSeccion.SelectedValue);
                    int IdEmpleado = Convert.ToInt32(DdlNomPers.SelectedValue);
                    int TipoPartida = 1;

                    TResultado ResUbicacionENSU = new TResultado();
                    CGrupoBienResguardo GrupoBien = BdAsignarResguardo.AgregarUbicacionENSU(IdEdificio, IdNivel, IdSeccion, IdEmpleado, TipoPartida, ref ResUbicacionENSU);

                    LlenarDdlGrupo(IdEmpleado);

                    HFIdENSU.Value = GrupoBien.IdENSU.ToString();
                }
                else
                    MostrarMensaje("Debe seleccionar una persona antes de crear un grupo.", "error", "Normal", "PersonaNoSeleccionada");
            }
            else
                MostrarMensaje("Debe seleccionar Edificio-Nivel-Sección antes de crear un grupo.", "error", "Normal", "PersonaNoSeleccionada");
        }

        protected void DdlEdificio_SelectedIndexChanged(object sender, EventArgs e)
        {
            int IdEdificio = Convert.ToInt32((sender as DropDownList).SelectedValue);

            if (IdEdificio != 0)
            {

                DataTable TEdificios = new DataTable();

                TResultado Resultado = new TResultado();
                DataTable Personal = new DataTable();
                Personal = BdAsignarResguardo.ObtenerNivelesXIdEdificio(IdEdificio, ref Resultado);
                
                if (!Resultado.Exito)
                    MostrarMensaje(Resultado.Mensaje.FirstOrDefault(), "error", "Normal", "NotExitoso");
                    

                DdlNivel.DataSource = Personal;
                DdlNivel.DataTextField = "Nivel";
                DdlNivel.DataValueField = "IdNivel";
                DdlNivel.DataBind();
                DdlNivel.Items.Insert(0, new ListItem(" -- Selecciona un nivel -- ", "0"));
            }

        }

        protected void DdlNivel_SelectedIndexChanged(object sender, EventArgs e)
        {
            int IdEdificio = Convert.ToInt32(DdlEdificio.SelectedValue);
            int IdNivel = Convert.ToInt32(DdlNivel.SelectedValue);

            if (IdNivel != 0)
            {
                DataTable TEdificios = new DataTable();

                TResultado Resultado = new TResultado();

                TEdificios = BdAsignarResguardo.ObtenerSeccionesXENSUUbicacion(IdEdificio, IdNivel, ref Resultado);
                if (!Resultado.Exito)
                    MostrarMensaje(Resultado.Mensaje.FirstOrDefault(), "error", "Normal", "NotExitoso");

                DdlSeccion.DataSource = TEdificios;
                DdlSeccion.DataTextField = "Seccion";
                DdlSeccion.DataValueField = "IdSeccion";
                DdlSeccion.DataBind();
                DdlSeccion.Items.Insert(0, new ListItem("-- Seleccionar sección --", "0"));
            }
        }

        protected void DdlGrupo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DdlGrupo.SelectedValue != "0")
            {
                List<CGrupoBienResguardo> LGrupoBienR = Page.Session["LGrupoBienR"] as List<CGrupoBienResguardo>;

                CGrupoBienResguardo GrupoBienR = (from GBR in LGrupoBienR
                                                  where GBR.IdGrupo == Convert.ToInt32(DdlGrupo.SelectedValue)
                                                  select GBR).FirstOrDefault();

                LblENSU.Text = GrupoBienR.Edificio + " - " + GrupoBienR.Nivel + " - " + GrupoBienR.Seccion;
                LblResguardo.Text = GrupoBienR.IdResguardo.ToString();

                LlenarGridAsignados(Convert.ToInt32(DdlNomPers.SelectedValue));
                
                //HOLA!!!
                //LblGNombreEdificio.Text = GrupoBienR.Edificio;
                //LblGNombreNivel.Text = GrupoBienR.Nivel;
                //LblGNombreSeccion.Text = GrupoBienR.Seccion;

            }
        }

        protected void GridAsignados_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "CNGuardar")
            {
                GridViewRow Row;
                Row = (GridViewRow)((ImageButton)e.CommandSource).NamingContainer;
                Int64 NumInventario = Convert.ToInt64((Row.Cells[0].FindControl("ChkSeleccionar") as CheckBox).Text);

                int IdActividad = Convert.ToInt32((Row.Cells[2].FindControl("DdlTipoActividad") as DropDownList).SelectedValue);

                TResultado ResModificarFichaBien = new TResultado();
                int RegModificados = BdAsignarResguardo.ModificarFichaBien(NumInventario, IdActividad, ref ResModificarFichaBien);

                if (ResModificarFichaBien.Exito)
                {
                    LlenarGridAsignados(Convert.ToInt32(DdlNomPers.SelectedValue));

                    TResultado ResModificarEstatusInv = new TResultado();
                    int RegMod = 0;
                    if (IdActividad == 5)
                    {
                        RegMod = BdAsignarResguardo.ModificarEstatusInvPersonal(Convert.ToInt32(DdlNomPers.SelectedValue), "P", ref ResModificarEstatusInv);
                    }
                    else
                    {
                        List<CFichaBien> LFichaBien = Page.Session["LFichaBienAsignados"] as List<CFichaBien>;
                        List<CFichaBien> LFichaBienNoLocalizado = new List<CFichaBien>();
                        LFichaBienNoLocalizado = (from FBNoLoc in LFichaBien
                                               where FBNoLoc.IdActividad == 5
                                               select FBNoLoc).ToList();

                        if (LFichaBienNoLocalizado.Count() == 0)
                        {
                            RegMod = BdAsignarResguardo.ModificarEstatusInvPersonal(Convert.ToInt32(DdlNomPers.SelectedValue), "SP", ref ResModificarEstatusInv);
                        }
                    }
                }
            }
        }

        protected void BtnAgregar_Click(object sender, EventArgs e)
        {
            List<CFichaBien> LFichaBienDisponibles = new List<CNegocios.CFichaBien>();
            LFichaBienDisponibles = Page.Session["LFichaBienDisponibles"] as List<CFichaBien>;

            List<CFichaBien> LFBSeleccionados = new List<CNegocios.CFichaBien>();

            LFBSeleccionados = LFichaBienDisponibles.Where(x => x.Seleccionado).ToList();

            if (DdlGrupo.SelectedValue != "0")
            {
                List<CGrupoBienResguardo> LGrupoBienR = Page.Session["LGrupoBienR"] as List<CGrupoBienResguardo>;

                CGrupoBienResguardo GrupoBien = (from Gb in LGrupoBienR
                                                 where Gb.IdGrupo == Convert.ToInt32(DdlGrupo.SelectedValue)
                                                 select Gb).FirstOrDefault();

                TResultado ResAsignarFichaBien = new TResultado();
                foreach (CFichaBien FBItem in LFBSeleccionados)
                {
                    int IdUsuario = Convert.ToInt32(HFIdUsuario.Value);
                    int RegModificados = BdAsignarResguardo.AsignarFichaBien(FBItem.IdInventario, GrupoBien.IdENSU, IdUsuario, 2, ref ResAsignarFichaBien);

                    if (!ResAsignarFichaBien.Exito)
                    {
                        return;
                    }
                }

                if (ResAsignarFichaBien.Exito)
                {
                    LlenarGridAsignados(Convert.ToInt32(DdlNomPers.SelectedValue));
                    if (RdPartida.Checked)
                        BusquedaXPartida();
                    else if (RdPartida.Checked)
                        BusquedaXInventario();
                }
                
            }
        }

        protected void BtnQuitar_Click(object sender, EventArgs e)
        {
            List<CFichaBien> LFichaBienAsignados = new List<CNegocios.CFichaBien>();
            LFichaBienAsignados = Page.Session["LFichaBienAsignados"] as List<CFichaBien>;

            List<CFichaBien> LFBSeleccionados = new List<CNegocios.CFichaBien>();

            LFBSeleccionados = LFichaBienAsignados.Where(x => x.Seleccionado).ToList();

            TResultado ResAsignarFichaBien = new TResultado();
            foreach (CFichaBien FBItem in LFBSeleccionados)
            {
                int IdUsuario = Convert.ToInt32(HFIdUsuario.Value);
                int RegModificados = BdAsignarResguardo.AsignarFichaBien(FBItem.IdInventario, 1, IdUsuario, 1, ref ResAsignarFichaBien);

                if (!ResAsignarFichaBien.Exito)
                {
                    return;
                }
            }

            if (ResAsignarFichaBien.Exito)
            {
                LlenarGridAsignados(Convert.ToInt32(DdlNomPers.SelectedValue));
                if (RdPartida.Checked)
                    BusquedaXPartida();
                else if (RdPartida.Checked)
                    BusquedaXInventario();
            }
        }

        protected void BtnImprimir_Click(object sender, EventArgs e)
        {
            if (DdlNomPers.SelectedValue != "0")
            {
                TParametros Parametros = new TParametros();
                Parametros.IdEmpleado = Convert.ToInt32(DdlNomPers.SelectedValue);
                Parametros.NombreTitular = BdAsignarResguardo.NombreTitularArea(Convert.ToInt32(HFIdUniAdmin.Value));

                Page.Session["Parametros"] = Parametros;
                
                string _open = "window.open('MostrarResguardo.aspx', '_blank');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), _open, true);
            }
        }

        protected void GridAsignados_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void BtnNuevaBusqueda_Click(object sender, EventArgs e)
        {
            Response.Redirect("frmAsignarResguardo.aspx");
        }
    }
}