using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using InventariosPJEH.CAccesoDatos;
using InventariosPJEH.CNegocios;

namespace InventariosPJEH
{
    public partial class frmCatSubClases_Partidas : System.Web.UI.Page
    {


        CSubClase_Partida obJN = new CSubClase_Partida();
        BdCatSubClase_Partidas obJE = new BdCatSubClase_Partidas();
        CConexion bd = new CConexion();

        protected void Page_Load(object sender, EventArgs e)
        {

            if (Request.Form["__EVENTTARGET"] == "AccionEliminarr")
            {
                GridBuscarPartidas_RowDeleting(this, new GridViewDeleteEventArgs(Int32.Parse(Rowindex.Value)));
            }
            else if (Request.Form["__EVENTTARGET"] == "AccionVacio")
            {
                Rowindex.Value = string.Empty;

            }

            if (Request.Form["__EVENTTARGET"] == "AccionEliminarr1")
            {

                GridBuscarSubClases_RowDeleting(this, new GridViewDeleteEventArgs(Int32.Parse(Rowindex2.Value)));

            }
            else if (Request.Form["__EVENTTARGET"] == "AccionVacio")
            {
                Rowindex2.Value = string.Empty;

            }

            if (!this.IsPostBack)
            {

                IniciarLlenadoDropDown();

            }
        }

        /// <summary>
        /// Método para inciar el llenado de partidas
        /// </summary>
        public void IniciarLlenadoDropDown()
        {
            DataTable Datos = new DataTable();
            Datos = BdCatSubClase_Partidas.InicializarCarga();
            DropPartidaNuevo.DataSource = Datos;
            DropPartidaNuevo.DataTextField = "Partida";
            DropPartidaNuevo.DataValueField = "IdPartida";
            DropPartidaNuevo.DataBind();
            DropPartidaNuevo.Items.Insert(0, new ListItem("Seleccionar", "0"));
        }

        /// <summary>
        /// Muestra los mensajes de notificación
        /// </summary>
        protected void MostrarMensaje(string Mensaje, string Tipo, string TipoFuncion, string ClaveMsj)
        {
            string Msj = "";
            if (TipoFuncion == "Normal")
                Msj = "MostrarMensaje('" + Mensaje + "', '" + Tipo + "');";
            else if (TipoFuncion == "NotificacionEliminar")
                Msj = "confirm();";
            else if (TipoFuncion == "NotificacionEliminar1")
                Msj = "confirm1();";
            else
                Msj = "MostrarMensajeInterval('" + Mensaje + "', '" + Tipo + "');";

            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), ClaveMsj, Msj, true);
        }

        /// <summary>
        /// Evento del botón (Buscar) permite buscar una partida
        /// </summary>
        protected void BtnBuscar_Click(object sender, EventArgs e)
        {

            BuscarPartida();
        }

        /// <summary>
        /// Clase que maneja, en la BD, realiza la consulta solicitada en el botón Buscar
        /// </summary>
        public void BuscarPartida()
        {

           // if (TxtPartidaBuscar.Text != "")
            //{
                DivTabla.Visible = true;
                DivMostrarNuevoR.Visible = false;
                BtnGuardar.Visible = false;
                BtnCancelar.Visible = false;

                BtnActualizar.Visible = false;
                BtnActualizar2.Visible = false;
                BtnLimpiar.Visible = false;
                BtnGuardar2.Visible = false;
                GridBuscar.DataSource = BdCatSubClase_Partidas.ConsultarGbPartidas(TxtPartidaBuscar.Text);
                GridBuscar.DataBind();
                if (GridBuscar.Rows.Count == 0)
                {
                    DivTabla.Visible = false;
                    MostrarMensaje("** No existen datos con la búsqueda solicitada **", "error", "Normal", "Incorrecto");
                }
          //  }
           // else
            //{
               // MostrarMensaje("** Campo vacío **", "error", "Normal", "Incorrecto");
            //}

        }

        /// <summary>
        /// Clase que realiza la consulta a la Base de datos, posteriormente lo muestra en la tabla
        /// busca una nueva partida
        /// </summary>
        //public void BuscarPartidaNuevo()
        //{

        //    if (TxtPartidaBuscar.Text != "")
        //    {
        //        DivTabla.Visible = true;
        //        string partida = Convert.ToString(DropTipoPartida.SelectedValue);
        //        GridBuscar.DataSource = BdCatSubClase_Partidas.ConsultarGbPartidasVuevo(TxtPartidaNuevo.Text, partida);
        //        GridBuscar.DataBind();
        //        if (GridBuscar.Rows.Count == 0)
        //        {
        //            DivTabla.Visible = false;
        //            MostrarMensaje("** No existen datos con la búsqueda solicitada **", "error", "Normal", "Incorrecto");
        //        }
        //    }
        //    else
        //    {
        //        MostrarMensaje("** Campo vacío **", "error", "Normal", "Incorrecto");
        //    }

        //}


        /// <summary>
        /// Método para buscar una subclase
        /// </summary>
        /// <param name="nIdSubClase"></param>
        public void BuscarSubClase(string nIdSubClase)
        {
            DivTabla2.Visible = true;

            GridBuscar2.DataSource = BdCatSubClase_Partidas.ConsultarGbSubClases(nIdSubClase);
            GridBuscar2.DataBind();
            if (GridBuscar2.Rows.Count == 0)
            {
                DivTabla2.Visible = false;
                MostrarMensaje("** No existen datos con la búsqueda solicitada **", "error", "Normal", "Incorrecto");
            }
        }

        /// <summary>
        /// Clase que realiza la consulta a la Base de datos, posteriormente lo muestra en la tabla
        /// busca una nueva subclase
        /// </summary>
        public void BuscarSubClaseNuevo()
        {
            DivTabla2.Visible = true;
            string Partida = Convert.ToString(DropPartidaNuevo.SelectedValue);

            GridBuscar2.DataSource = BdCatSubClase_Partidas.ConsultarGbSubClasesNuevo(Partida);
            GridBuscar2.DataBind();
            if (GridBuscar2.Rows.Count == 0)
            {
                DivTabla2.Visible = false;

                MostrarMensaje("** No existen datos con la búsqueda solicitada **", "error", "Normal", "Incorrecto");
            }
        }

        /// <summary>
        /// Evento del botón (Nuevo registro) muestra la sección para realizar un nuevo registro de partidas,
        /// se puede mostrar y ocultar 
        /// </summary>
        protected void BtnMostraOcultar(object sender, EventArgs e)
        {
            if (lgModificarRegistro.Visible == true)
            {
                if (DivMostrarNuevoR.Visible == true)
                {
                    DivMostrarNuevoR.Visible = true;
                    BtnGuardar.Visible = true;
                    BtnActualizar.Visible = false;
                    lgNuevoRegistro.Visible = true;
                    lgModificarRegistro.Visible = false;
                    BtnCancelar.Visible = true;
                    BtnLimpiar.Visible = true;
                    LimpiarRegistro();

                }


            }
            else if (lgModificarRegistro.Visible == false)
            {
                DivMostrarNuevoR.Visible = false;
                BtnGuardar.Visible = false;
                BtnLimpiar.Visible = false;
                BtnCancelar.Visible = false;
            }
            if (DivMostrarNuevoR.Visible == false)
            {
                DivMostrarNuevoR.Visible = true;
                BtnGuardar.Visible = true;
                BtnActualizar.Visible = false;
                lgNuevoRegistro.Visible = true;
                lgModificarRegistro.Visible = false;
                BtnCancelar.Visible = true;
                BtnLimpiar.Visible = true;
                LimpiarRegistro();

            }

        }


        /// <summary>
        /// Clase para limpiar los datos de la seccion nuevo registro
        /// </summary>
        public void LimpiarRegistro()
        {
            DropTipoPartida.SelectedIndex = 0;
            TxtPartidaNuevo.Text = string.Empty;
            TxtNumPartidaNuevo.Text = string.Empty;
        }

        //Evento para eliminar una partida
        /// <summary>
        ///  Evento del botón (Eliminar) se encuentra dentro del GridView,
        ///  realiza la edición del registro seleccionado dentro del GridView
        /// </summary>
        protected void GridBuscarPartidas_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            DivMostrarNuevoR.Visible = false;
            BtnActualizar.Visible = false;
            BtnGuardar.Visible = false;
            BtnCancelar.Visible = false;
            if (String.IsNullOrEmpty(Rowindex.Value))
            {
                Rowindex.Value = e.RowIndex.ToString();
                MostrarMensaje("Prueba", "info", "NotificacionEliminar", "Inicio");
            }
            else
            {

                Boolean encontrado = true;
                try
                {
                    if (encontrado == true)
                    {

                        int n = e.RowIndex;
                        int index = Convert.ToInt32(e.RowIndex);
                        GridViewRow RowSelecionada = GridBuscar.Rows[index];

                        string xcod = GridBuscar.DataKeys[n].Value.ToString();
                        obJN.IdPartida = xcod;
                        obJE.Eliminar_Partidas(obJN);
                        GridBuscar.EditIndex = -1;
                        // BuscarPartidaNuevo();
                        BuscarPartida();
                        MostrarMensaje("** Partida eliminada **", "error", "Normal", "Incorrecto");
                        Rowindex.Value = "";

                        if (GridBuscar.Rows.Count == 0)
                        {
                            DivTabla.Visible = false;
                            MostrarMensaje("** No existen datos con la búsqueda solicitada **", "error", "Normal", "Incorrecto");
                        }
                        else if (GridBuscar.Rows.Count != 0)
                        {
                            MostrarMensaje("** No se puede eliminar existen campos relacionados a otras tablas **", "error", "Normal", "Incorrecto");

                        }
                    }

                }
                catch (Exception ex)
                {
                    Rowindex.Value = "";
                    MostrarMensaje("** Error al eliminar registro **", "error", "Normal", "Incorrecto");
                }
            }
        }


        /// <summary>
        /// Clase para limpiar el nuevo registro de subclases
        /// </summary>
        public void LimpiarRegistro2()
        {
            DropPartidaNuevo.SelectedIndex = 0;
            TxtClaveSubClase.Text = string.Empty;
            TxtSubClaseNuevo.Text = string.Empty;
            TxtFolioNuevo.Text = string.Empty;
        }

        /// <summary>
        /// Evento del botón (Nuevo registro) muestra la sección para realizar un nuevo registro de subclases,
        /// se puede mostrar y ocultar
        /// </summary>
        protected void BtnMostraOcultar2(object sender, EventArgs e)
        {
            if (lgModificarRegistro2.Visible == true)
            {
                if (DivMostrarNuevoR2.Visible == true)
                {
                    TxtClaveSubClase.Enabled = true;
                    DivMostrarNuevoR2.Visible = true;
                    BtnGuardar2.Visible = true;
                    BtnActualizar2.Visible = false;
                    lgNuevoRegistro2.Visible = true;
                    lgModificarRegistro2.Visible = false;
                    //BtnCancelar2.Visible = true;
                    //BtnLimpiar2.Visible = true;
                    LimpiarRegistro2();

                }
            }
            else if (lgModificarRegistro2.Visible == false)
            {
                DivMostrarNuevoR2.Visible = false;
                BtnGuardar2.Visible = false;
                BtnLimpiar.Visible = false;
                BtnCancelar.Visible = false;
            }
            if (DivMostrarNuevoR2.Visible == false)
            {
                TxtClaveSubClase.Enabled = true;
                DivMostrarNuevoR2.Visible = true;
                BtnGuardar2.Visible = true;
                BtnActualizar2.Visible = false;
                lgNuevoRegistro2.Visible = true;
                lgModificarRegistro2.Visible = false;
                BtnCancelar.Visible = true;
                BtnLimpiar.Visible = true;
                LimpiarRegistro2();

            }


        }

        /// <summary>
        ///  Evento del botón (Guardar) guarda un nuevo registro de partidas en la BD.
        /// </summary>
        protected void Guardar_Click(object sender, EventArgs e)
        {
            if (TxtPartidaNuevo.Text != "")
            {
                if (TxtNumPartidaNuevo.Text != "")
                {
                    if (DropTipoPartida.SelectedIndex != 0)
                    {

                        BtnGuardar.Visible = true;
                        DivTabla.Visible = true;
                        string tipoPartida = Convert.ToString(DropTipoPartida.SelectedValue);
                        SqlConnection cnn = new SqlConnection(CConexion.Obtener());

                        cnn.Open();
                        string consultar = "SELECT * from Cat_Partidas WHERE NumPartida= '" + TxtNumPartidaNuevo.Text + "'";
                        SqlCommand cmd1 = new SqlCommand(consultar, cnn);
                        int clave = Convert.ToInt32(cmd1.ExecuteScalar());

                        SqlDataReader leer = cmd1.ExecuteReader();
                        string validar;

                        if (leer.Read() == true)
                        {
                            validar = leer[1].ToString();
                        }
                        else
                        {
                            validar = "";
                        }
                        cnn.Close();


                        if (string.IsNullOrWhiteSpace(TxtNumPartidaNuevo.Text))
                        {
                            MostrarMensaje("** Error campo vacío **", "error", "Normal", "Incorrecto");

                        }
                        else
                        {
                            if (validar == TxtNumPartidaNuevo.Text)
                            {
                                MostrarMensaje("** Esta clave ya existe **", "error", "Normal", "Incorrecto");

                            }
                            else
                            {
                                SqlCommand insert = new SqlCommand("INSERT INTO Cat_Partidas(Partida,TipoPartida,NumPartida) values(@partida,@tipoPartida,@numPartida)", cnn);
                                insert.Parameters.AddWithValue("@partida", TxtPartidaNuevo.Text.ToUpper().Trim());
                                insert.Parameters.AddWithValue("@tipoPartida", tipoPartida);
                                insert.Parameters.AddWithValue("@numPartida", TxtNumPartidaNuevo.Text.Trim());

                                try
                                {
                                    cnn.Open();
                                    insert.ExecuteNonQuery();
                                    MostrarMensaje("** Registro exitoso **", "error", "Normal", "Incorrecto");
                                    //  BuscarPartidaNuevo();
                                    BuscarPartida();
                                    LimpiarRegistro();
                                    DivMostrarNuevoR.Visible = false;
                                    BtnGuardar.Visible = false;
                                    BtnCancelar.Visible = false;
                                    BtnActualizar2.Visible = false;

                                }
                                catch (Exception )
                                {
                                    MostrarMensaje("** Error al registrar **", "error", "Normal", "Incorrecto");
                                    cnn.Close();
                                }
                            }
                        }



                    }
                    else
                    {
                        MostrarMensaje("** Selecciona un tipo de partida **", "error", "Normal", "Incorrecto");
                    }


                }
                else
                {
                    MostrarMensaje("** El campo número partida es requerido **", "error", "Normal", "Incorrecto");
                }
            }
            else
            {
                MostrarMensaje("** El campo nombre partida es requerido *", "error", "Normal", "Incorrecto");
            }


        }

        /// <summary>
        /// Evento del botón (Actualizar) actualiza los datos de las partidas y muestra en la
        /// tabla la actualización
        /// </summary>
        protected void BtnActualizar_Click(object sender, EventArgs e)
        {
            if (TxtPartidaNuevo.Text != "")
            {
                if (TxtNumPartidaNuevo.Text != "")
                {
                    if (DropTipoPartida.SelectedIndex != 0)
                    {

                        BtnGuardar.Visible = false;
                        DivTabla.Visible = true;
                        string tipoPartida = Convert.ToString(DropTipoPartida.SelectedValue);
                        SqlConnection cnn = new SqlConnection(CConexion.Obtener());
                        SqlCommand update = new SqlCommand("UPDATE Cat_Partidas SET Partida=@partida, TipoPartida=@tipoPartida, NumPartida=@numPartida WHERE IdPartida=@idPartida", cnn);
                        update.Parameters.AddWithValue("@idPartida", lbPartida.Text);
                        update.Parameters.AddWithValue("@partida", TxtPartidaNuevo.Text.ToUpper().Trim());
                        update.Parameters.AddWithValue("@tipoPartida", tipoPartida);
                        update.Parameters.AddWithValue("@numPartida", TxtNumPartidaNuevo.Text.Trim());

                        try
                        {
                            cnn.Open();
                            update.ExecuteNonQuery();
                            MostrarMensaje("** Se actualizó correctamente **", "error", "Normal", "Incorrecto");
                            BuscarPartida();
                            DivMostrarNuevoR.Visible = false;
                            BtnActualizar.Visible = false;
                            DivTabla.Visible = true;
                            BtnGuardar2.Visible = false;
                            LimpiarRegistro();
                        }
                        catch (Exception )
                        {
                            MostrarMensaje("** Error al actualizar **", "error", "Normal", "Incorrecto");
                            cnn.Close();
                        }
                    }
                    else
                    {
                        MostrarMensaje("** Selecciona un tipo de partida **", "error", "Normal", "Incorrecto");
                    }


                }
                else
                {
                    MostrarMensaje("** El campo número partida es requerido **", "error", "Normal", "Incorrecto");
                }
            }
            else
            {
                MostrarMensaje("** El campo nombre partida es requerido *", "error", "Normal", "Incorrecto");
            }

        }


        /// <summary>
        /// Evento para buscar una subclase de la partida, se muestra en caso de que si tenga una partida 
        /// </summary>
        protected void GridBuscar_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            DivMostrarNuevoR.Visible = false;

            BtnGuardar.Visible = false;

            BtnActualizar.Visible = false;
            BtnActualizar2.Visible = false;
            BtnLimpiar.Visible = false;
            BtnCancelar.Visible = false;
            BtnGuardar2.Visible = false;

            int index = Convert.ToInt32(e.NewSelectedIndex);
            GridViewRow RowSeleccionada = GridBuscar.Rows[index];
            string idpartida = RowSeleccionada.Cells[0].Text;
            HiddenId.Value = idpartida;
            BuscarSubClase(idpartida);
            DivMostrarNuevoR2.Visible = false;
        }

        /// <summary>
        /// Evento del botón (Editar) se encuentra dentro del GridView,
        /// realiza la edición del registro seleccionado dentro del GridView
        /// </summary>
        protected void GridBuscarPartidas_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            DivMostrarNuevoR.Visible = true;
            BtnActualizar.Visible = true;
            BtnGuardar.Visible = false;
            BtnGuardar2.Visible = false;
            BtnCancelar.Visible = true;
            BtnLimpiar.Visible = true;
            lgNuevoRegistro.Visible = false;
            lgModificarRegistro.Visible = true;
            DivTabla2.Visible = false;
            DivMostrarNuevoR2.Visible = false;
            BtnActualizar2.Visible = false;
            int index = Convert.ToInt32(e.CommandArgument);
            GridViewRow RowSelecionada = GridBuscar.Rows[index];

            string idPartida = RowSelecionada.Cells[0].Text;
            string partida = Page.Server.HtmlDecode(RowSelecionada.Cells[1].Text);
            string tipoPartida = Page.Server.HtmlDecode((RowSelecionada.Cells[2].Controls[1] as Label).Text);
            string numPartida = RowSelecionada.Cells[3].Text;

            lbPartida.Text = idPartida;
            TxtPartidaNuevo.Text = partida;
            TxtNumPartidaNuevo.Text = numPartida;

            Boolean encontrado = true;
            int a = 0;

            while (encontrado)
            {
                if (DropTipoPartida.Items[a].Text == tipoPartida)
                {
                    DropTipoPartida.SelectedIndex = a;
                    encontrado = false;
                }
                else if (DropTipoPartida.Items[a].Text != tipoPartida)
                {
                    a++;
                }
            }

        }




        /// <summary>
        /// Evento del botón (Guardar) guarda un nuevo registro de subclase en la BD.
        /// </summary>
        protected void GuardarSubClase_Click(object sender, EventArgs e)
        {
            if (TxtClaveSubClase.Text != "")
            {
                if (TxtClaveSubClase.Text.Length == 4)
                {
                    if (TxtSubClaseNuevo.Text != "")
                    {
                        if (TxtFolioNuevo.Text != "")
                        {

                            if (DropPartidaNuevo.SelectedIndex != 0)
                            {

                                BtnGuardar.Visible = false;
                                DivTabla.Visible = true;
                                string Partida = Convert.ToString(DropPartidaNuevo.SelectedValue);
                                SqlConnection cnn = new SqlConnection(CConexion.Obtener());
                                SqlCommand insert = new SqlCommand("INSERT INTO Cat_SubClase(IdSubClase,SubClase,IdPartida, Folio) values(@idSubClase,@subClase,@idPartida,@folio)", cnn);
                                insert.Parameters.AddWithValue("@idSubClase", TxtClaveSubClase.Text.Trim());
                                insert.Parameters.AddWithValue("@subClase", TxtSubClaseNuevo.Text.Trim());
                                insert.Parameters.AddWithValue("@idPartida", Partida.Trim());
                                insert.Parameters.AddWithValue("@folio", TxtFolioNuevo.Text);

                                try
                                {
                                    cnn.Open();
                                    insert.ExecuteNonQuery();
                                    MostrarMensaje("** Registro exitoso **", "error", "Normal", "Incorrecto");
                                    BuscarSubClaseNuevo();
                                    DivMostrarNuevoR.Visible = false;
                                    BtnGuardar.Visible = false;
                                    BtnCancelar.Visible = false;
                                    BtnGuardar2.Visible = false;
                                    DivMostrarNuevoR2.Visible = false;
                                    LimpiarRegistro();
                                    LimpiarRegistro2();
                                }
                                catch (Exception )
                                {
                                    MostrarMensaje("** Error al registrar **", "error", "Normal", "Incorrecto");
                                    cnn.Close();
                                }
                            }
                            else
                            {
                                MostrarMensaje("** Selecciona una partida **", "error", "Normal", "Incorrecto");
                            }
                        }
                        else
                        {
                            MostrarMensaje("** El campo número de folio es requerido **", "error", "Normal", "Incorrecto");

                        }
                    }
                    else
                    {
                        MostrarMensaje("** El campo subclase es requerido **", "error", "Normal", "Incorrecto");
                    }
                }
                else
                {
                    MostrarMensaje("** La clave debe de ser de 4 dígitos *", "error", "Normal", "Incorrecto");

                }
            }

            else
            {
                MostrarMensaje("** El campo clave de subclase es requerido *", "error", "Normal", "Incorrecto");
            }
        }


        /// <summary>
        /// Evento del botón (Actualizar) actualiza un registro de subclases en la BD.
        /// </summary>
        protected void BtnActualizarSubClase_Click(object sender, EventArgs e)
        {
            if (TxtClaveSubClase.Text != "")
            {
                if (TxtSubClaseNuevo.Text != "")
                {
                    if (TxtFolioNuevo.Text != "")
                    {

                        if (DropPartidaNuevo.SelectedIndex != 0)
                        {


                            DivTabla.Visible = true;
                            string Partida = Convert.ToString(DropPartidaNuevo.SelectedValue);
                            SqlConnection cnn = new SqlConnection(CConexion.Obtener());
                            SqlCommand insert = new SqlCommand("UPDATE Cat_SubClase SET SubClase=@subClase,IdPartida=@idPartida, Folio=@folio WHERE IdSubClase=@idSubClase", cnn);
                            insert.Parameters.AddWithValue("@idSubClase", TxtClaveSubClase.Text.Trim());
                            insert.Parameters.AddWithValue("@subClase", TxtSubClaseNuevo.Text.Trim());
                            insert.Parameters.AddWithValue("@idPartida", Partida.Trim());
                            insert.Parameters.AddWithValue("@folio", TxtFolioNuevo.Text.Trim());

                            try
                            {
                                cnn.Open();
                                insert.ExecuteNonQuery();
                                MostrarMensaje("** Se actualizó correctamente **", "error", "Normal", "Incorrecto");
                                BuscarSubClaseNuevo();
                                DivMostrarNuevoR.Visible = false;
                                BtnActualizar.Visible = false;
                                BtnCancelar.Visible = false;
                                BtnGuardar.Visible = false;
                                BtnActualizar2.Visible = false;
                                DivMostrarNuevoR2.Visible = false;
                                LimpiarRegistro();
                            }
                            catch (Exception )
                            {
                                MostrarMensaje("** Error al actualizar **", "error", "Normal", "Incorrecto");
                                cnn.Close();
                            }
                        }
                        else
                        {
                            MostrarMensaje("** Selecciona una partida **", "error", "Normal", "Incorrecto");
                        }
                    }
                    else
                    {
                        MostrarMensaje("** El campo número de folio es requerido **", "error", "Normal", "Incorrecto");

                    }
                }
                else
                {
                    MostrarMensaje("** El campo subclase es requerido **", "error", "Normal", "Incorrecto");
                }
            }
            else
            {
                MostrarMensaje("** El campo clave de subclase es requerido *", "error", "Normal", "Incorrecto");
            }
        }

        /// <summary>
        /// Evento del botón (Limpiar) limpia la página devolviendo a su estado inicial
        /// </summary>
        protected void BtnLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarRegistro2();
            DivMostrarNuevoR2.Visible = false;
            DivTabla2.Visible = false;
            //BtnLimpiar2.Visible = false;
            BtnActualizar2.Visible = false;
            BtnGuardar2.Visible = false;
            //BtnCancelar2.Visible = false;
            LimpiarRegistro();
            DivMostrarNuevoR.Visible = false;
            DivTabla.Visible = false;
            BtnLimpiar.Visible = false;
            BtnActualizar.Visible = false;
            BtnGuardar.Visible = false;
            BtnCancelar.Visible = false;
            TxtPartidaBuscar.Text = string.Empty;

        }

        /// <summary>
        /// Evento del botón (Cancelar) realiza la cancelación de cualquier acción realizada 
        /// </summary>
        protected void BtnCancelar_Click(object sender, EventArgs e)
        {
            if (lgNuevoRegistro2.Visible == true)
            {
                //BtnLimpiar2.Visible = true;
            }
            else if (lgModificarRegistro2.Visible == true)
            {
                //BtnLimpiar2.Visible = true;
                BtnActualizar2.Visible = false;
                BtnMostrarNuevoR2.Visible = true;
            }
            DivMostrarNuevoR2.Visible = false;
            BtnGuardar2.Visible = false;
            //BtnCancelar2.Visible = false;
            BtnGuardar.Visible = false;
            BtnCancelar.Visible = false;
            BtnActualizar.Visible = false;
            DivMostrarNuevoR.Visible = false;
        }

        /// <summary>
        /// Evento del botón (Eliminar) se encuentra dentro del GridView,
        /// realiza la eliminacion del registro seleccionado de una subclase, mostrando un mensaje de confirmación
        /// </summary>
        protected void GridBuscarSubClases_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            DivMostrarNuevoR2.Visible = false;
            BtnActualizar2.Visible = false;
            BtnGuardar2.Visible = false;
            BtnCancelar.Visible = false;
            if (String.IsNullOrEmpty(Rowindex2.Value))
            {
                Rowindex2.Value = e.RowIndex.ToString();
                MostrarMensaje("Prueba", "info", "NotificacionEliminar1", "Inicio");
            }
            else
            {
                Boolean encontrado = true;
                try
                {
                    if (encontrado == true)
                    {

                        int n = e.RowIndex;
                        int index = Convert.ToInt32(e.RowIndex);
                        GridViewRow RowSelecionada = GridBuscar2.Rows[index];

                        string xcod = GridBuscar2.DataKeys[n].Value.ToString();
                        obJN.IdSubClase = xcod;
                        obJE.Eliminar_SubClases(obJN);
                        GridBuscar.EditIndex = -1;
                        BuscarSubClase(HiddenId.Value);
                        MostrarMensaje("** Subclase eliminado **", "error", "Normal", "Incorrecto");
                        Rowindex2.Value = "";
                    }

                }
                catch (Exception )
                {
                    Rowindex2.Value = "";
                    MostrarMensaje("** Error al subclase personal **", "error", "Normal", "Incorrecto");
                }
            }
        }

        /// Evento del botón (Editar) se encuentra dentro del GridView,
        /// realiza la edición del registro seleccionado de una subclase dentro del GridView
        protected void GridBuscarSubClases_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            TxtClaveSubClase.Enabled = false;
            BtnGuardar.Visible = false;
            DivMostrarNuevoR2.Visible = true;
            BtnActualizar2.Visible = true;
            BtnGuardar2.Visible = false;
            BtnCancelar.Visible = true;
            BtnLimpiar.Visible = true;
            lgNuevoRegistro2.Visible = false;
            lgModificarRegistro2.Visible = true;

            int index = Convert.ToInt32(e.CommandArgument);
            GridViewRow RowSelecionada = GridBuscar2.Rows[index];

            string idSubClase = RowSelecionada.Cells[0].Text;
            string subClase = Page.Server.HtmlDecode(RowSelecionada.Cells[1].Text);
            string idPartida = RowSelecionada.Cells[2].Text;
            string partida = Page.Server.HtmlDecode(RowSelecionada.Cells[3].Text);
            string folio = Page.Server.HtmlDecode(GridBuscar2.DataKeys[index].Values["Folio"].ToString());


            TxtClaveSubClase.Text = idSubClase;
            TxtSubClaseNuevo.Text = subClase;
            TxtFolioNuevo.Text = folio;

            Boolean encontrado = true;
            int a = 0;

            while (encontrado)
            {
                if (DropPartidaNuevo.Items[a].Text == partida)
                {
                    DropPartidaNuevo.SelectedIndex = a;
                    encontrado = false;
                }
                else if (DropPartidaNuevo.Items[a].Text != partida)
                {
                    a++;
                }
            }
        }



    }
}