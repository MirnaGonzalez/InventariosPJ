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
    public partial class frmCatEdNiSec : System.Web.UI.Page
    {
        CEdificios_Niveles_Secciones obJN = new CEdificios_Niveles_Secciones();
        CEdificios_Niveles_Secciones obJN1 = new CEdificios_Niveles_Secciones();
        BdCatEdificios_Niveles_Secciones obJE = new BdCatEdificios_Niveles_Secciones();
        protected void Page_Load(object sender, EventArgs e)
        {

            if (Request.Form["__EVENTTARGET"] == "AccionEliminarr")
            {

              //  GridBuscarEdificios_RowDeleting(this, new GridViewDeleteEventArgs(Int32.Parse(Rowindex.Value)));
            }

            else if (Request.Form["__EVENTTARGET"] == "AccionVacio")
            {
                Rowindex.Value = string.Empty;
            }

            if (Request.Form["__EVENTTARGET"] == "AccionEliminarr1")
            {
                GridBuscarNiveles_RowDeleting(this, new GridViewDeleteEventArgs(Int32.Parse(Rowindex2.Value)));
            }
            else if (Request.Form["__EVENTTARGET"] == "AccionVacio")
            {
                Rowindex2.Value = string.Empty;
            }

            if (Request.Form["__EVENTTARGET"] == "AccionEliminarr2")
            {
                GridBuscarSecciones_RowDeleting(this, new GridViewDeleteEventArgs(Int32.Parse(Rowindex3.Value)));
            }
            else if (Request.Form["__EVENTTARGET"] == "AccionVacio")
            {
                Rowindex3.Value = string.Empty;
            }


            if (!this.IsPostBack)
            {

                IniciarLlenadoDropDown();
                IniciarLlenadoDropDownNuevo();
                IniciarLlenadoDropEdificioNuevo();
            }
        }

        //Método para inciar el llenado de municipios
        public void IniciarLlenadoDropDown()
        {
            DataTable Datos = new DataTable();
            Datos = BdCatEdificios_Niveles_Secciones.InicializarCarga();
            ddlMunicipio.DataSource = Datos;
            ddlMunicipio.DataTextField = "Municipio";
            ddlMunicipio.DataValueField = "IdMunicipio";
            ddlMunicipio.DataBind();
            ddlMunicipio.Items.Insert(0, new ListItem("Seleccionar", "0"));
            ddlEdificio.Items.Insert(0, new ListItem("Seleccionar", "0"));
            //ddlNivel.Items.Insert(0, new ListItem("Seleccionar", "0"));
            //ddlSeccion.Items.Insert(0, new ListItem("Seleccionar", "0"));

        }

        //Evento para llenar la lista niveles
        protected void BuscarEdificio(object sender, EventArgs e)
        {

            int IdMunicipio = Convert.ToInt32(ddlMunicipio.SelectedValue);
            DataTable Datos = new DataTable();
            Datos = BdCatEdificios_Niveles_Secciones.ObtenerEdificios(IdMunicipio);
            ddlEdificio.DataSource = Datos;
            ddlEdificio.DataTextField = "Edificio";
            ddlEdificio.DataValueField = "IdEdificio";
            ddlEdificio.DataBind();
            ddlEdificio.Items.Insert(0, new ListItem("Seleccionar", "0"));

            DivNiveles.Visible  = false;
            DivNuevoNivel.Visible = false;
            DivSecciones.Visible = false;
            DivNuevaSeccion.Visible = false;

        }

        //Evento para llenar ddl Niveles del Encabezado
        //protected void BuscarNiveles(object sender, EventArgs e)
        //{
        //    int IdEdificios = Convert.ToInt32(ddlEdificio.SelectedValue);
        //    DataTable Datos = new DataTable();
        //    Datos = BdCatEdificios_Niveles_Secciones.ObtenerNiveles(IdEdificios);
        //    ddlNivel.DataSource = Datos;
        //    ddlNivel.DataTextField = "Nivel";
        //    ddlNivel.DataValueField = "IdNivel";
        //    ddlNivel.DataBind();
        //    ddlNivel.Items.Insert(0, new ListItem("Seleccionar", "0"));

        //    DivNiveles.Visible = false;
        //    DivNuevoNivel.Visible = false;
        //    DivSecciones.Visible = false;
        //    DivNuevaSeccion.Visible = false;


        //}

        //protected void BuscarSecciones(object sender, EventArgs e)
        //{
        //    int IdNivel = Convert.ToInt32(ddlNivel.SelectedValue);
        //    DataTable Datos = new DataTable();
        //    Datos = BdCatEdificios_Niveles_Secciones.ObtenerSecciones(IdNivel);
        //    ddlSeccion.DataSource = Datos;
        //    ddlSeccion.DataTextField = "Nombre";
        //    ddlSeccion.DataValueField = "IdSeccion";
        //    ddlSeccion.DataBind();
        //    ddlSeccion.Items.Insert(0, new ListItem("Seleccionar", "0"));

        //    DivSecciones.Visible = false;
        //    DivNuevaSeccion.Visible = false;

        //}


        //Método para inciar el llenado de municipios en la sección de nuevo registro
        public void IniciarLlenadoDropDownNuevo()
        {
            DataTable Datos = new DataTable();
            Datos = BdCatEdificios_Niveles_Secciones.InicializarCarga();
            DropMunicipioNuevo.DataSource = Datos;
            DropMunicipioNuevo.DataTextField = "Municipio";
            DropMunicipioNuevo.DataValueField = "IdMunicipio";
            DropMunicipioNuevo.DataBind();
            DropMunicipioNuevo.Items.Insert(0, new ListItem("Seleccionar", "0"));
        }

        public void IniciarLlenadoDropEdificioNuevo()
        {
            DataTable Datos = new DataTable();
            Datos = BdCatEdificios_Niveles_Secciones.InicializarCargaEdificioNuevo();
            DropEdificiosNuevo.DataSource = Datos;
            DropEdificiosNuevo.DataTextField = "Edificio";
            DropEdificiosNuevo.DataValueField = "IdEdificio";
            DropEdificiosNuevo.DataBind();
            DropEdificiosNuevo.Items.Insert(0, new ListItem("Seleccionar", "0"));
        }

       

        //Muestra los mensajes de notificación
        protected void MostrarMensaje(string Mensaje, string Tipo, string TipoFuncion, string ClaveMsj)
        {
            string Msj = "";
            if (TipoFuncion == "Normal")
                Msj = "MostrarMensaje('" + Mensaje + "', '" + Tipo + "');";
            else if (TipoFuncion == "NotificacionEliminar")
                Msj = "confirm();";
            else if (TipoFuncion == "NotificacionEliminar1")
                Msj = "confirm1();";
            else if (TipoFuncion == "NotificacionEliminar2")
                Msj = "confirm2();";

            else
                Msj = "MostrarMensajeInterval('" + Mensaje + "', '" + Tipo + "');";

            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), ClaveMsj, Msj, true);
        }

        protected void BtnBuscar_Click(object sender, EventArgs e)
        {
            BuscarPorFiltros();
            DivEdificios.Visible = true;
        }

        public void BuscarPorFiltros()
        {

            if (ddlMunicipio.SelectedIndex != 0 | ddlEdificio.SelectedIndex != 0 ) // | ddlNivel.SelectedIndex != 0 | ddlSeccion.SelectedIndex != 0)
            {
                string municipio = Convert.ToString(ddlMunicipio.SelectedItem);
                string edificios = Convert.ToString(ddlEdificio.SelectedItem);
                //string nivel = Convert.ToString(ddlNivel.SelectedItem);
                //string seccion = Convert.ToString(ddlSeccion.SelectedItem);

                if (edificios == "Seleccionar")
                {
                    edificios = "";
                }

                //if (nivel == "Seleccionar")
                //{
                //    nivel = "";
                //}

                //if (seccion == "Seleccionar")
                //{
                //    seccion = "";
                //}


                GridEdificios.DataSource = BdCatEdificios_Niveles_Secciones.ConsultarGbEdNivSec(municipio, edificios); //, nivel, seccion);
                GridEdificios.DataBind();
                DivEdificios.Visible = true;

                if (GridEdificios.Rows.Count == 0)
                {
                    DivEdificios.Visible = false;
                    DivNuevoEdificio.Visible = false;
                    BtnActualizar.Visible = false;
                    //BtnLimpiar.Visible = false;
                    BtnCancelar.Visible = false;
                    MostrarMensaje("No existen datos con la búsqueda solicitada", "error", "Normal", "Incorrecto");
                }


            }

            else
            {
                MostrarMensaje("Debe seleccionar al menos el Municipio", "error", "Normal", "Incorrecto");
            }

        }

        protected void GridBuscar_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            DivNuevoEdificio.Visible = false;
            BtnGuardar.Visible = false;
            BtnActualizar.Visible = false;
            BtnCancelar.Visible = false;
         //  BtnLimpiar.Visible = false;
            LgEdiNivel.Visible = true;
          
           
            int index = Convert.ToInt32(e.NewSelectedIndex);
            GridViewRow RowSeleccionada = GridEdificios.Rows[index];
            string edificios = GridEdificios.DataKeys[index].Values["IdEdificio"].ToString();
            HiddenIdEdificio.Value = edificios;
            BuscarNiveles(edificios);
           



        }

        //Métod para buscar una subclase
        /// <summary>
        /// 
        /// </summary>
        /// <param name="idEdificios"></param>
        public void BuscarNiveles(string idEdificios)
        {
            DivNiveles.Visible = true;
            LgEdiNivel.Visible = true;
            GridNiveles.Visible = true;
            List<CEdificios_Niveles_Secciones> lista = new List<CEdificios_Niveles_Secciones>();
            lista = BdCatEdificios_Niveles_Secciones.ConsultarGbNiveles(idEdificios);
            Page.Session["lstEdificio"] = lista;


            GridNiveles.DataSource = lista;
            GridNiveles.DataBind();
            if (GridNiveles.Rows.Count == 0)
            {
                DivNiveles.Visible = false;
               
                MostrarMensaje("** No existen niveles en el edificio seleccionado **", "error", "Normal", "Incorrecto");
                DivNuevaSeccion.Visible = false;
            }
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="municipio"></param>
        public void BuscarNuevoRegsitroEdificios(string municipio, string edificio, string nivel, string seccion)
        {
            DivEdificios.Visible = true;
            GridEdificios.DataSource = BdCatEdificios_Niveles_Secciones.ConsultarGbEdNivSec(municipio, edificio); //, nivel, seccion);
            GridEdificios.DataBind();
            if (GridEdificios.Rows.Count == 0)
            {
                DivEdificios.Visible = false;
                MostrarMensaje("** No existen datos con la búsqueda solicitada **", "error", "Normal", "Incorrecto");
            }
        }





        protected void GridBuscarEdificios_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            int index = Convert.ToInt32(e.CommandArgument);
            GridViewRow RowSelecionada = GridEdificios.Rows[index];

            string idEdificio = GridEdificios.DataKeys[index].Values["IdEdificio"].ToString();
            Page.Session["idEdificio"] = idEdificio;

           
            switch (e.CommandName)


            {
                case "Editar":

                    ////////////////////////////////////7
                    DivNuevoEdificio.Visible = true;
                    BtnActualizar.Visible = true;
                    BtnGuardar.Visible = false;
                    BtnCancelar.Visible = true;
                    //BtnLimpiar.Visible = true;
                    lgNuevoRegistro.Visible = false;
                    lgModificarRegistro.Visible = true;


                    string edificio = Page.Server.HtmlDecode(RowSelecionada.Cells[1].Text);

                    string calle = Page.Server.HtmlDecode(RowSelecionada.Cells[2].Text);
                    string colonia = Page.Server.HtmlDecode(RowSelecionada.Cells[3].Text);
                    string cp = RowSelecionada.Cells[4].Text;
                    string idMunicipio = RowSelecionada.Cells[5].Text;
                    string municipio = Page.Server.HtmlDecode(RowSelecionada.Cells[6].Text);

                    lbIdEdificios.Text = idEdificio;
                    TxtEdificioNuevo.Text = edificio;
                    TxtCalleNuevo.Text = calle;
                    TxtColoniaNuevo.Text = colonia;
                    TxtCodigoPostalNuevo.Text = cp;
                    HiddenIdEdificio.Value = idEdificio;
                    HiddenEdificioNuevo.Value = TxtEdificioNuevo.Text;
                    Boolean encontrado = true;
                    int a = 0;

                    while (encontrado)
                    {
                        if (DropMunicipioNuevo.Items[a].Text == municipio)
                        {
                            DropMunicipioNuevo.SelectedIndex = a;
                            encontrado = false;
                        }
                        else if (DropMunicipioNuevo.Items[a].Text != municipio)
                        {
                            a++;
                        }
                    }
                    ///////////////////////////////////////7
                    break;

                case "Eliminar":

                    if (Request.Form["__EVENTTARGET"] == "AccionEliminarr")
                    {

                        BorrarEdificio(idEdificio);
                    }
                    else if (Request.Form["__EVENTTARGET"] == "AccionVacio")
                    {
                        Rowindex.Value = string.Empty;
                    }


                     break;



                case "Agregar":
                    DivNuevoNivel.Visible = true;

                    //////// BtnMostraOcultar2
                    if (lgModificarRegistro2.Visible == true)
                    {
                        if (DivNuevoNivel.Visible == true)
                        {
                            DivNuevoNivel.Visible = true;
                            btnGuardarNivel.Visible = true;
                            lgNuevoRegistro2.Visible = true;
                            BtnCancelar.Visible = true;
                            //BtnLimpiar.Visible = true;

                            DivNuevoEdificio.Visible = false;
                            BtnGuardar.Visible = false;
                            BtnActualizar.Visible = false;
                            BtnActualizar2.Visible = false;
                            lgModificarRegistro2.Visible = false;

                            LimpiarRegistro2();

                        }
                    }
                    else if (lgModificarRegistro2.Visible == false)
                    {
                        DivNuevoNivel.Visible = false;
                        DivNuevoEdificio.Visible = false;
                        btnGuardarNivel.Visible = false;
                        //BtnLimpiar.Visible = false;
                        BtnCancelar.Visible = false;
                        BtnGuardar.Visible = false;
                        BtnActualizar.Visible = false;
                    }
                    if (DivNuevoNivel.Visible == false)
                    {
                        DivNuevoNivel.Visible = true;
                        btnGuardarNivel.Visible = true;
                        lgNuevoRegistro2.Visible = true;
                        BtnCancelar.Visible = true;
                      //  BtnLimpiar.Visible = true;

                        DivNuevoEdificio.Visible = false;
                        BtnActualizar2.Visible = false;
                        lgModificarRegistro2.Visible = false;
                        BtnGuardar.Visible = false;
                        BtnActualizar.Visible = false;

                        LimpiarRegistro2();

                    }
                    //////////


                    break;

                case "Consultar":
                    BuscarNiveles(idEdificio);
                    break;

                default:
                    break;
            }
        }

        //protected void GridBuscarEdificios_RowDeleting(object sender, GridViewDeleteEventArgs e)
        public void BorrarEdificio(string idEdificio)
        {
                        DivNuevoEdificio.Visible = false;
                        BtnActualizar.Visible = false;
                        BtnGuardar.Visible = false;
                        BtnCancelar.Visible = false;

                        bool success = false;
                        SqlConnection Conn = new SqlConnection(CConexion.Obtener());
                        SqlTransaction lTransaccion = null;
                        int Valor_Retornado = 0;
                        SqlConnection cnn = new SqlConnection(CConexion.Obtener());

                                try
                                {

                                    SqlCommand cmd = new SqlCommand("SP_Eliminar_Edificios", cnn);
                                    cmd.CommandType = CommandType.StoredProcedure;
                                    cmd.Parameters.AddWithValue("@idEdificio", Page.Session["idEdificio"]);
                                    SqlParameter ValorRetorno = new SqlParameter("@NumError", SqlDbType.Int);
                                     cnn.Open();

                                    int retorno = cmd.ExecuteNonQuery();



                                     ValorRetorno.Direction = ParameterDirection.Output;
                                    cmd.Parameters.Add(ValorRetorno);
                                    cmd.ExecuteNonQuery();
                                    Valor_Retornado = Convert.ToInt32(ValorRetorno.Value);


                                    Valor_Retornado = Convert.ToInt32(ValorRetorno.Value);
                                    if (Valor_Retornado == 1)
                                        success = true;

                                    cnn.Close();

                                }
                                catch (Exception ex)
                                {
                                    string script = ex.ToString();
                                     MostrarMensaje("** No se puede eliminar el edificio por que tiene niveles asociados**", "error", "Normal", "Incorrecto");
        }
                                finally
        {
            if (success)
            {
                lTransaccion.Commit();
                cnn.Close();
            }
            else
            {
                lTransaccion.Rollback();
                cnn.Close();
            }
        }

   }   

      


        protected void BtnMostraOcultar(object sender, EventArgs e)
        {
            if (lgModificarRegistro.Visible == true)
            {
                if (DivNuevoEdificio.Visible == true)
                {
                    DivNuevoNivel.Visible = false;
                    DivNuevaSeccion.Visible = false;
                    DivNuevoEdificio.Visible = true;
                    DivNiveles.Visible = false;
                    DivSecciones.Visible = false;
                    BtnGuardar.Visible = true;
                    btnGuardarNivel.Visible = false;
                    btnGuadarSeccion.Visible = false;
                    BtnActualizar.Visible = false;
                    lgNuevoRegistro.Visible = true;
                    lgModificarRegistro.Visible = false;
                    BtnCancelar.Visible = true;
                    //BtnLimpiar.Visible = true;
                    LimpiarRegistro();

                }


            }
            else if (lgModificarRegistro.Visible == false)
            {
                DivNuevoEdificio.Visible = false;
                DivNuevoNivel.Visible = false;
                DivNuevaSeccion.Visible = false;
                BtnGuardar.Visible = false;
              //  BtnLimpiar.Visible = false;
                BtnCancelar.Visible = false;
                btnGuardarNivel.Visible = false;
                btnGuadarSeccion.Visible = false;
                DivNiveles.Visible = false;
                DivSecciones.Visible = false;
            }
            if (DivNuevoEdificio.Visible == false)
            {
                DivNuevoEdificio.Visible = true;
                DivNuevoNivel.Visible = false;
                DivNuevaSeccion.Visible = false;
                BtnGuardar.Visible = true;
                BtnActualizar.Visible = false;
                lgNuevoRegistro.Visible = true;
                lgModificarRegistro.Visible = false;
                BtnCancelar.Visible = true;
             //   BtnLimpiar.Visible = true;
                btnGuardarNivel.Visible = false;
                btnGuadarSeccion.Visible = false;
                DivNiveles.Visible = false;
                DivSecciones.Visible = false;
                LimpiarRegistro();

            }
        }

        //Método para limpiar un nuevo registro de partidas
        public void LimpiarRegistro()
        {
            DropMunicipioNuevo.SelectedIndex = 0;
            TxtEdificioNuevo.Text = string.Empty;
            TxtCalleNuevo.Text = string.Empty;
            TxtColoniaNuevo.Text = string.Empty;
            TxtCodigoPostalNuevo.Text = string.Empty;

        }

        protected void Guardar_Click(object sender, EventArgs e)
        {
            if (TxtEdificioNuevo.Text != "")
            {
                if (TxtCalleNuevo.Text != "")
                {
                    if (TxtColoniaNuevo.Text != "")
                    {
                        if (TxtCodigoPostalNuevo.Text != "")
                        {
                            if (TxtCodigoPostalNuevo.Text.Length == 5)
                            {
                                if (DropMunicipioNuevo.SelectedIndex != 0)
                            {

                                BtnGuardar.Visible = true;
                                DivEdificios.Visible = true;
                                int idMunicipio = Convert.ToInt32(DropMunicipioNuevo.SelectedValue);
                                    string municipio = Convert.ToString(DropMunicipioNuevo.SelectedItem);
                                SqlConnection cnn = new SqlConnection(CConexion.Obtener());
                                SqlCommand insert = new SqlCommand("INSERT INTO Cat_Edificio(Edificio,Calle,Colonia,CP,IdMunicipio) values(@edificio,@calle,@colonia,@cp,@idMunicipio)", cnn);
                                insert.Parameters.AddWithValue("@edificio", TxtEdificioNuevo.Text.ToUpper() );
                                insert.Parameters.AddWithValue("@calle", TxtCalleNuevo.Text.ToUpper());
                                insert.Parameters.AddWithValue("@colonia", TxtColoniaNuevo.Text.ToUpper());
                                insert.Parameters.AddWithValue("@cp", TxtCodigoPostalNuevo.Text.ToUpper());
                                insert.Parameters.AddWithValue("@idMunicipio",idMunicipio);
                                    HiddenMunicipio.Value = municipio;
                                        
                                    try
                                    {
                                    cnn.Open();
                                    insert.ExecuteNonQuery();
                                    MostrarMensaje("** Registro exitoso **", "error", "Normal", "Incorrecto");
                                            
                                        BuscarNuevoRegsitroEdificios(municipio, TxtEdificioNuevo.Text, TxtNivelNuevo.Text, TxtSeccionesNuevo.Text);
                                        BtnCancelar.Visible = false;
                                        DivNuevoEdificio.Visible = false;
                                        BtnGuardar.Visible = false;
                                        LimpiarRegistro();
                                }
                                catch (Exception )
                                {
                                    MostrarMensaje("** Error al registrar un edificio **", "error", "Normal", "Incorrecto");
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
                                 MostrarMensaje("** El campo código postal debe de ser de 5 dígitos **", "error", "Normal", "Incorrecto");
                            }
                        }
                    else
                    {
                            MostrarMensaje("** El campo código postal es requerido **", "error", "Normal", "Incorrecto");
                        }

                    }
                else
                {
                        MostrarMensaje("** El campo colonia es requerido **", "error", "Normal", "Incorrecto");
                    }

                }
                else
                {
                    MostrarMensaje("** El campo calle es requerido **", "error", "Normal", "Incorrecto");
                }
            }
            else
            {
                MostrarMensaje("** El campo edificio es requerido *", "error", "Normal", "Incorrecto");
            }

        }

        protected void BtnActualizar_Click(object sender, EventArgs e)
        {
            if (TxtEdificioNuevo.Text != "")
            {
                if (TxtCalleNuevo.Text != "")
                {
                    if (TxtColoniaNuevo.Text != "")
                    {
                        if (TxtCodigoPostalNuevo.Text != "")
                        {
                            if (TxtCodigoPostalNuevo.Text.Length == 5)
                            {
                                if (DropMunicipioNuevo.SelectedIndex != 0)
                                {

                                    BtnGuardar.Visible =false;
                                    DivEdificios.Visible = true;
                                    int idMunicipio = Convert.ToInt32(DropMunicipioNuevo.SelectedValue);
                                    string municipio = Convert.ToString(DropMunicipioNuevo.SelectedItem);

                                    SqlConnection cnn = new SqlConnection(CConexion.Obtener());
                                    SqlCommand update = new SqlCommand("UPDATE Cat_Edificio SET Edificio=@edificio,Calle=@calle,Colonia=@colonia,CP=@cp,IdMunicipio=@idMunicipio WHERE IdEdificio=@idEdificio", cnn);            
                                    update.Parameters.AddWithValue("@idEdificio", lbIdEdificios.Text);
                                    update.Parameters.AddWithValue("@edificio", TxtEdificioNuevo.Text.ToUpper());
                                    update.Parameters.AddWithValue("@calle", TxtCalleNuevo.Text.ToUpper());
                                    update.Parameters.AddWithValue("@colonia", TxtColoniaNuevo.Text.ToUpper());
                                    update.Parameters.AddWithValue("@cp", TxtCodigoPostalNuevo.Text);
                                    update.Parameters.AddWithValue("@idMunicipio", idMunicipio);

                                    try
                                    {
                                        cnn.Open();
                                        update.ExecuteNonQuery();
                                        MostrarMensaje("** Se actualizó correctamente **", "error", "Normal", "Incorrecto");

                                        if (ddlMunicipio.SelectedIndex == 0 | ddlEdificio.SelectedIndex == 0 ) // | ddlNivel.SelectedIndex == 0 | ddlSeccion.SelectedIndex == 0)
                                        {
                                           string  Municipio = Convert.ToString(DropMunicipioNuevo.SelectedItem);
                                            BuscarNuevoRegsitroEdificios(municipio, TxtEdificioNuevo.Text, TxtNivelNuevo.Text, TxtSeccionesNuevo.Text);
                                            BtnCancelar.Visible = false;
                                            DivNuevoEdificio.Visible = false;
                                            BtnActualizar.Visible = false;

                                        }
                                        else
                                        {
                                            BuscarPorFiltros();
                                            BtnCancelar.Visible = false;
                                            DivNuevoEdificio.Visible = false;
                                            BtnActualizar.Visible = false;
                                        }

                                        


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
                                MostrarMensaje("** El campo código postal debe de ser de 5 dígitos **", "error", "Normal", "Incorrecto");
                            }
                        }
                        else
                        {
                            MostrarMensaje("** El campo código postal es requerido **", "error", "Normal", "Incorrecto");
                        }

                    }
                    else
                    {
                        MostrarMensaje("** El campo colonia es requerido **", "error", "Normal", "Incorrecto");
                    }

                }
                else
                {
                    MostrarMensaje("** El campo calle es requerido **", "error", "Normal", "Incorrecto");
                }
            }
            else
            {
                MostrarMensaje("** El campo edificio es requerido *", "error", "Normal", "Incorrecto");
            }
        }

        protected void BtnLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarRegistro();
            DivNuevoEdificio.Visible = false;
            DivEdificios.Visible = false;
            DivNiveles.Visible = false;
            DivSecciones.Visible = false;
            DivNuevaSeccion.Visible = false;
            DivNuevoNivel.Visible = false;
            btnGuardarNivel.Visible = false;
            btnGuadarSeccion.Visible = false;
            //BtnLimpiar.Visible = false;
            BtnActualizar.Visible = false;
            BtnActualizar2.Visible = false;
            BtnActualizar3.Visible = false;
            BtnGuardar.Visible = false;
            BtnCancelar.Visible = false;
            ddlMunicipio.SelectedIndex = 0;
            ddlEdificio.SelectedIndex = 0;
          //  ddlNivel.SelectedIndex = 0;
        //    ddlSeccion.SelectedIndex = 0;
        
            LimpiarRegistro2();
            LimpiarRegistro3();
            
        }

        protected void BtnCancelar_Click(object sender, EventArgs e)
        {
            if (lgNuevoRegistro.Visible == true)
            {
                //BtnLimpiar.Visible = true;
            }
            else if (lgModificarRegistro.Visible == true)
            {
               // BtnLimpiar.Visible = true;
                BtnActualizar.Visible = false;
                BtnMostrarNuevoR.Visible = true;
            }
            DivNuevoEdificio.Visible = false;
            DivNuevoNivel.Visible = false;
            BtnGuardar.Visible = false;
            btnGuardarNivel.Visible = false;
            BtnCancelar.Visible = false;
        }

        /*********************************NIVELES********************************************/

        protected void GridBuscar2_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            //DivNuevoNivel.Visible = false;
            //btnGuardarNivel.Visible = false;
            //BtnActualizar2.Visible = false;
            //BtnCancelar.Visible = false;
            //BtnLimpiar.Visible = false;
            //DivNuevoNivel.Visible = false;
            //DivSecciones.Visible = true;
            //GridSecciones.Visible = true;
            //LgEdiNivelSeccion.Visible = true;
           
           
            //int index = Convert.ToInt32(e.NewSelectedIndex);
            //GridViewRow RowSeleccionada = GridNiveles.Rows[index];
            //string idNivel = GridNiveles.DataKeys[index].Values["IdNivel"].ToString();
            ////string idEdificio = GridNiveles.DataKeys[index].Values["idEdificio"].ToString();
            //HiddenIdNivel.Value = idNivel;
     
            //BuscarSeccionNuevo(idNivel, HiddenIdEdificio.Value);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="idNivel"></param>
        public void BuscarSeccionNuevo(string idNivel, string idEdificio)
        {
            DivSecciones.Visible = true;

            GridSecciones.DataSource = BdCatEdificios_Niveles_Secciones.ConsultarGbSecciones(idNivel, idEdificio);
            GridSecciones.DataBind();
            if (GridSecciones.Rows.Count == 0)
            {
                DivSecciones.Visible = false;
                MostrarMensaje("** No existen datos con la búsqueda solicitada **", "error", "Normal", "Incorrecto");
            }
        }



        public void BuscarSecciones(string idNivel, string idEdificio)
        {
            DivSecciones.Visible = true;


            List<CEdificios_Niveles_Secciones> lista = new List<CEdificios_Niveles_Secciones>();

            lista = BdCatEdificios_Niveles_Secciones.ConsultarGbSecciones(idNivel, idEdificio);
            Page.Session["lstSecciones"] = lista;


            GridSecciones.DataSource = lista;
            GridSecciones.DataBind();

            if (GridSecciones.Rows.Count == 0)
            {
                DivSecciones.Visible = false;
                MostrarMensaje("** No existen datos con la búsqueda solicitada **", "error", "Normal", "Incorrecto");
            }
        }

        protected void GridBuscarNiveles_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            DivNuevoNivel.Visible = true;
            BtnActualizar2.Visible = true;
            btnGuardarNivel.Visible = false;
            BtnCancelar.Visible = true;
            //BtnLimpiar.Visible = true;
            lgNuevoRegistro2.Visible = false;
            lgModificarRegistro2.Visible = true;
           
            int index = Convert.ToInt32(e.CommandArgument);
            GridViewRow RowSelecionada = GridNiveles.Rows[index];

            string idNivel = GridNiveles.DataKeys[index].Values["IdNivel"].ToString();
            string idEdificio = GridNiveles.DataKeys[index].Values["IdEdificio"].ToString();

            switch (e.CommandName)


            {
                case "Editar":

                        List<CEdificios_Niveles_Secciones> lstEdificio = Page.Session["lstEdificio"] as List<CEdificios_Niveles_Secciones>;

                        var EdificioSeleccionado = (from datos in lstEdificio
                                                    where datos.IdEdificio == idEdificio
                                                    select datos).FirstOrDefault();

                        string edificio = EdificioSeleccionado.Edificio;

                        string nivel = RowSelecionada.Cells[3].Text;

                        LbIdNiveles.Text = idNivel;
                        TxtNivelNuevo.Text = nivel;
                        HiddenIdNivel.Value = idNivel;
                        HiddenEdificio.Value = edificio;
                        Boolean encontrado = true;
                        int a = 1;

                        while (encontrado)
                        {
                            if (DropEdificiosNuevo.Items[a].Text == edificio)
                            {
                                DropEdificiosNuevo.SelectedIndex = a;
                                encontrado = false;
                            }
                            else if (DropEdificiosNuevo.Items[a].Text != edificio)
                            {
                                a++;
                            }
                                           
                         }

                    break;

                case "Agregar":

                    if (lgModificarRegistro3.Visible == true)
                    {
                        if (DivNuevaSeccion.Visible == true)
                        {
                            DivNuevaSeccion.Visible = true;
                            btnGuadarSeccion.Visible = true;
                            BtnActualizar3.Visible = false;
                            lgNuevaSeccion.Visible = true;
                            lgModificarRegistro3.Visible = false;
                            BtnCancelar.Visible = true;
                            //BtnLimpiar.Visible = true;
                            LimpiarRegistro3();

                        }
                    }
                    else if (lgModificarRegistro3.Visible == false)
                    {
                        DivNuevaSeccion.Visible = false;
                        btnGuadarSeccion.Visible = false;
                     //   BtnLimpiar.Visible = false;
                        BtnCancelar.Visible = false;
                    }
                    if (DivNuevaSeccion.Visible == false)
                    {
                        DivNuevaSeccion.Visible = true;
                        btnGuadarSeccion.Visible = true;
                        BtnActualizar3.Visible = false;
                        lgNuevaSeccion.Visible = true;
                        lgModificarRegistro3.Visible = false;
                        BtnCancelar.Visible = true;
                      //  BtnLimpiar.Visible = true;
                        LimpiarRegistro3();

                    }

                    break;

                case "Consultar":

                    DivNuevoNivel.Visible = false;
                    btnGuardarNivel.Visible = false;
                    BtnActualizar2.Visible = false;
                    BtnCancelar.Visible = false;
                    //BtnLimpiar.Visible = false;
                    DivNuevoNivel.Visible = false;
                    DivSecciones.Visible = true;
                    GridSecciones.Visible = true;
                    LgEdiNivelSeccion.Visible = true;


            
                    HiddenIdNivel.Value = idNivel;
                    BuscarSeccionNuevo(idNivel, HiddenIdEdificio.Value);
                    break;

                default:
                    break;
            }


        }
        

        protected void GridBuscarNiveles_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            DivNuevoNivel.Visible = false;
            BtnActualizar2.Visible = false;
            btnGuardarNivel.Visible = false;
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
                        GridViewRow RowSelecionada = GridNiveles.Rows[index];

                        string xcod = GridNiveles.DataKeys[n].Value.ToString();
                        obJN.IdNivel = xcod;
                        obJE.Eliminar_Niveles(obJN);
                        GridNiveles.EditIndex = -1;
                        BuscarNiveles(HiddenIdEdificio.Value);
                        MostrarMensaje("** Registro eliminado **", "error", "Normal", "Incorrecto");
                        Rowindex2.Value = "";
                    }

                }
                catch (Exception )
                {
                    Rowindex2.Value = "";
                    MostrarMensaje("** Error al eliminar registro **", "error", "Normal", "Incorrecto");
                }
            }
        }

        protected void BtnMostraOcultar2(object sender, EventArgs e)
        {
           
        }

        //Método para limpiar un nuevo registro de partidas
        public void LimpiarRegistro2()
        {
            TxtNivelNuevo.Text = string.Empty;
            DropEdificiosNuevo.SelectedIndex = 0;

        }

        protected void GuardarNivel_Click(object sender, EventArgs e)
        {
            if (TxtNivelNuevo.Text != "")
            {
                if (DropEdificiosNuevo.SelectedIndex != 0)
                {
                    string edificio = Convert.ToString(HiddenEdificio.Value);
                    string edi = Convert.ToString(DropEdificiosNuevo.SelectedItem);
                  

                  
               
                    btnGuardarNivel.Visible = false;
                    DivNiveles.Visible = true;

                    int seccion = Convert.ToInt32(DropEdificiosNuevo.SelectedValue);

                    SqlConnection cnn = new SqlConnection(CConexion.Obtener());
                    SqlCommand insert = new SqlCommand("INSERT INTO Cat_Niveles(Nivel, IdEdificio) VALUES(@nivel, @idEdificio)", cnn);
                    insert.Parameters.AddWithValue("@nivel", TxtNivelNuevo.Text);
                    insert.Parameters.AddWithValue("@idEdificio", seccion);

                    try
                    {
                        cnn.Open();
                        insert.ExecuteNonQuery();
                        MostrarMensaje("** Guardado correctamente **", "error", "Normal", "Incorrecto");
                        BuscarNiveles(HiddenIdEdificio.Value);
                        BtnCancelar.Visible = false;
                        DivNuevoNivel.Visible = true;
                        BtnActualizar2.Visible = false;
                        DivNiveles.Visible = true;
                        LimpiarRegistro2();
                    }
                    catch (Exception )
                    {
                        MostrarMensaje("** Error al registrar una nivel **", "error", "Normal", "Incorrecto");
                        cnn.Close();
                    }
                
                }
                else
                {
                    MostrarMensaje("** Selecciona un edificio **", "error", "Normal", "Incorrecto");

                }
            }
            else
            {
                MostrarMensaje("** El campo nombre del nivel es requerido **", "error", "Normal", "Incorrecto");
            }


        }

        protected void BtnActualizarNivel_Click(object sender, EventArgs e)
        {
            if (TxtNivelNuevo.Text != "")
            {
                if (DropEdificiosNuevo.SelectedIndex != 0)
                {
                    btnGuardarNivel.Visible = false;
                DivNiveles.Visible = true;

                SqlConnection cnn = new SqlConnection(CConexion.Obtener());
                SqlCommand update = new SqlCommand("UPDATE Cat_Niveles SET Nivel=@nivel WHERE IdNivel=@idNivel", cnn);
                update.Parameters.AddWithValue("@idNivel", HiddenIdNivel.Value);
                update.Parameters.AddWithValue("@nivel", TxtNivelNuevo.Text);

                try
                {
                    cnn.Open();
                    update.ExecuteNonQuery();
                    MostrarMensaje("** Se actualizó correctamente **", "error", "Normal", "Incorrecto");
                    BuscarNiveles(HiddenIdEdificio.Value);
                        BtnCancelar.Visible = false;
                        DivNuevoNivel.Visible = false;
                        BtnActualizar2.Visible = false;
                    }
                catch (Exception)
                {
                    MostrarMensaje("** Error al actualizar una nivel **", "error", "Normal", "Incorrecto");
                    cnn.Close();
                }
                }
                else
                {
                    MostrarMensaje("** Selecciona un edificio **", "error", "Normal", "Incorrecto");

                }
            }
            else
            {
                MostrarMensaje("** El campo nombre del nivel es requerido **", "error", "Normal", "Incorrecto");
            }

        }

        protected void BtnLimpiarNivel_Click(object sender, EventArgs e)
        {
            LimpiarRegistro2();
            DivNuevoNivel.Visible = false;
            DivNiveles.Visible = false;
        
            BtnActualizar2.Visible = false;
            btnGuardarNivel.Visible = false;
            BtnCancelar.Visible = false;
            DivEdificios.Visible = false;
            LimpiarRegistro();
          
            DivNuevoEdificio.Visible = false;
            DivEdificios.Visible = false;
            //BtnLimpiar.Visible = false;
            BtnActualizar.Visible = false;
            BtnGuardar.Visible = false;
            BtnCancelar.Visible = false;
            ddlMunicipio.SelectedIndex = 0;
            ddlEdificio.SelectedIndex = 0;
          //  ddlNivel.SelectedIndex = 0;
         //   ddlSeccion.SelectedIndex = 0;
      

        }

        protected void BtnCancelarNivel_Click(object sender, EventArgs e)
        {
            if (lgNuevoRegistro2.Visible == true)
            {
              //  BtnLimpiar.Visible = true;
            }
            else if (lgModificarRegistro2.Visible == true)
            {
             //   BtnLimpiar.Visible = true;
                BtnActualizar2.Visible = false;
               
            }
            DivNuevoNivel.Visible = false;
            btnGuardarNivel.Visible = false;
            BtnCancelar.Visible = false;
        }


        /***************SECCIONES********************/

        protected void GridBuscarSecciones_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            DivNuevaSeccion.Visible = true;
            BtnActualizar3.Visible = true;
            btnGuadarSeccion.Visible = false;
            BtnCancelar.Visible = true;
         //   BtnLimpiar.Visible = true;
            lgNuevaSeccion.Visible = false;
            lgModificarRegistro3.Visible = true;

            int index = Convert.ToInt32(e.CommandArgument);
            GridViewRow RowSelecionada = GridSecciones.Rows[index];


            string idSeccion = GridSecciones.DataKeys[index].Values["IdSeccion"].ToString();
            string tipo = (RowSelecionada.Cells[6].Controls[1] as Label).Text;
            string idENSU = GridSecciones.DataKeys[index].Values["IdENSU"].ToString();


            
            SqlConnection Conn = new SqlConnection(Conexion.Obtener());
            SqlCommand cmdsql = new SqlCommand("Select Nombre from VistaEdificiosNivelesSecciones where IdSeccion = @idNivel ", Conn);
            cmdsql.Parameters.AddWithValue("@idNivel", HiddenIdNivel.Value);
            Conn.Open();
            SqlDataReader leer = cmdsql.ExecuteReader();


            string nombre;
            if (leer.Read() == true)
            {
                nombre = leer[0].ToString();
            }
            else
            {
                nombre = "";
            }

            Conn.Close();



            LbIdSecciones.Text = idSeccion;
            TxtSeccionesNuevo.Text = nombre;
            LbIdENSU.Text = idENSU;
            HiddenIdENSU.Value = idENSU;

            Boolean encontrado = true;
            int a = 0;

            while (encontrado)
            {
                if (DropTipoNuevo.Items[a].Text == tipo)
                {
                    DropTipoNuevo.SelectedIndex = a;
                   

                    encontrado = false;
                }
                else if (DropTipoNuevo.Items[a].Text != tipo)
                {
                    a++;
                }
            }
        }

        protected void GridBuscarSecciones_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            DivNuevaSeccion.Visible = false;
            BtnActualizar3.Visible = false;
            btnGuadarSeccion.Visible = false;
            BtnCancelar.Visible = false;
            if (String.IsNullOrEmpty(Rowindex3.Value))
            {
                Rowindex3.Value = e.RowIndex.ToString();
                MostrarMensaje("Prueba", "info", "NotificacionEliminar2", "Inicio");
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
                        GridViewRow RowSelecionada = GridSecciones.Rows[index];
                        string idENSU1 = GridSecciones.DataKeys[index].Values["IdENSU"].ToString();

                        string xcod = GridSecciones.DataKeys[n].Value.ToString();
                        obJN.IdSeccion = xcod;
                        obJN1.IdENSU = HiddenIdENSU.Value;
                        obJE.Eliminar_Secciones(obJN, obJN1);
                        GridSecciones.EditIndex = -1;
                        //BuscarSeccionNuevo(HiddenId2.Value);
                       

                        BuscarSecciones(HiddenIdNivel.Value, HiddenIdEdificio.Value);
                        MostrarMensaje("** Registro eliminado **", "error", "Normal", "Incorrecto");
                        Rowindex3.Value = "";
                    }

                }
                catch (Exception )
                {
                    Rowindex3.Value = "";
                    MostrarMensaje("** Error al eliminar registro **", "error", "Normal", "Incorrecto");
                }
            }
        }

        protected void BtnMostraOcultar3(object sender, EventArgs e)
        {
            if (lgModificarRegistro3.Visible == true)
            {
                if (DivNuevaSeccion.Visible == true)
                {
                    DivNuevaSeccion.Visible = true;
                    btnGuadarSeccion.Visible = true;
                    BtnActualizar3.Visible = false;
                    lgNuevaSeccion.Visible = true;
                    lgModificarRegistro3.Visible = false;
                    BtnCancelar.Visible = true;
                //    BtnLimpiar.Visible = true;
                    LimpiarRegistro3();

                }
            }
            else if (lgModificarRegistro3.Visible == false)
            {
                DivNuevaSeccion.Visible = false;
                btnGuadarSeccion.Visible = false;
             //   BtnLimpiar.Visible = false;
                BtnCancelar.Visible = false;
            }
            if (DivNuevaSeccion.Visible == false)
            {
                DivNuevaSeccion.Visible = true;
                btnGuadarSeccion.Visible = true;
                BtnActualizar3.Visible = false;
                lgNuevaSeccion.Visible = true;
                lgModificarRegistro3.Visible = false;
                BtnCancelar.Visible = true;
             //   BtnLimpiar.Visible = true;
                LimpiarRegistro3();

            }
        }

        public void GuardarSeccionNuevo()
        {
            if (TxtSeccionesNuevo.Text != "")
            {
                if (DropTipoNuevo.SelectedIndex != 0)
                {

                    btnGuadarSeccion.Visible = true;
                    DivSecciones.Visible = true;
                    string seccion = Convert.ToString(DropTipoNuevo.SelectedValue);
                    SqlConnection cnn = new SqlConnection(CConexion.Obtener());
                    SqlCommand insert = new SqlCommand("INSERT INTO Cat_Secciones(Nombre, TipoSeccion) VALUES(@nombre,@tipo)", cnn); 
                    insert.Parameters.AddWithValue("@nombre", TxtSeccionesNuevo.Text);
                    insert.Parameters.AddWithValue("@tipo", seccion);

                    try
                    {
                        cnn.Open();
                        insert.ExecuteNonQuery();

                    }
                    catch (Exception )
                    {
                        MostrarMensaje("** Error al registrar una sección **", "error", "Normal", "Incorrecto");
                        cnn.Close();
                    }
                }
                else
                {
                    MostrarMensaje("** Selecciona un tipo **", "error", "Normal", "Incorrecto");
                }
            }
            else
            {
                MostrarMensaje("** El campo nombre de sección es requerido **", "error", "Normal", "Incorrecto");
            }
        }
   
        protected void GuardarSecciones_Click(object sender, EventArgs e)
        {
            GuardarSeccionNuevo();

            SqlConnection Conn = new SqlConnection(CConexion.Obtener());
            Conn.Open();
            string conSQLEdificio = "SELECT MAX(IdEdificio) from Cat_Edificio";
            SqlCommand cmd1 = new SqlCommand(conSQLEdificio, Conn);
            SqlDataReader leer = cmd1.ExecuteReader();
            string IdEdificio;
            if (leer.Read() == true)
            {
                IdEdificio = leer[0].ToString();
            }
            else
            {
                IdEdificio = "";
            }
            Conn.Close();

         
                SqlConnection Conn1 = new SqlConnection(CConexion.Obtener());
            Conn1.Open();
            string conSQLNivel = "SELECT MAX(IdNivel) from Cat_Niveles";
                SqlCommand cmd2 = new SqlCommand(conSQLNivel, Conn1);
                SqlDataReader leer1 = cmd2.ExecuteReader();
                string IdNivel;
                if (leer1.Read() == true)
                {
                    IdNivel = leer1[0].ToString();
                }
                else
                {
                    IdNivel = "";
                }
                Conn1.Close();

            


            //// SELECCIONAR SECCIÓN

            SqlConnection Conn2 = new SqlConnection(CConexion.Obtener());
            Conn2.Open();
            string conSQLSeccion = "SELECT MAX(IdSeccion) from Cat_Secciones";
            SqlCommand cmd3 = new SqlCommand(conSQLSeccion, Conn2);
            SqlDataReader leer2 = cmd3.ExecuteReader();
            string IdSeccion;
            if (leer2.Read() == true)
            {
                IdSeccion = leer2[0].ToString();
            }
            else
            {
                IdSeccion = "";
            }
            Conn2.Close();


            btnGuadarSeccion.Visible = true;
            DivSecciones.Visible = true;


            string seccion = Convert.ToString(DropTipoNuevo.SelectedValue);


            SqlConnection cnn = new SqlConnection(CConexion.Obtener());
            SqlCommand insert = new SqlCommand("INSERT INTO Cat_ENSUbicacion(IdEdificio, IdNivel,IdSeccion) VALUES(@idEdificio, @idNivel, @idSeccion)", cnn);
            insert.Parameters.AddWithValue("@idEdificio", IdEdificio);
            insert.Parameters.AddWithValue("@idNivel", IdNivel);
            insert.Parameters.AddWithValue("@idSeccion", IdSeccion);

            try
            {
                cnn.Open();
                insert.ExecuteNonQuery();
                MostrarMensaje("** Guardado correctamente **", "error", "Normal", "Incorrecto");

                //BuscarSeccionNuevo(HiddenId2.Value);
                BuscarSecciones(HiddenIdNivel.Value, IdEdificio);

                BtnCancelar.Visible = false;
                DivNuevaSeccion.Visible = false;
                btnGuadarSeccion.Visible = false;
                DivSecciones.Visible = true;
                btnGuardarNivel.Visible = false;
                BtnCancelar.Visible = false;
            }
            catch (Exception )
            {
                MostrarMensaje("** Error al registrar una sección **", "error", "Normal", "Incorrecto");
                cnn.Close();
            }
            
        }

        protected void BtnActualizarSecciones_Click(object sender, EventArgs e)
        {
            if (TxtNivelNuevo.Text != "")
            {
                if (DropTipoNuevo.SelectedIndex != 0)
                {

                    SqlConnection cnn = new SqlConnection(CConexion.Obtener());
                    cnn.Open();

                    try
                    {
                        btnGuardarNivel.Visible = false;
                        DivNiveles.Visible = true;
                        
                        
                        string seccion = Convert.ToString(DropTipoNuevo.SelectedValue);

                        SqlCommand update = new SqlCommand("UPDATE Cat_Secciones SET Nombre=@nombre, TipoSeccion=@tipo WHERE IdSeccion=@idSeccion", cnn);
                        update.Parameters.AddWithValue("@idSeccion", LbIdSecciones.Text);
                        update.Parameters.AddWithValue("@nombre", TxtSeccionesNuevo.Text);
                        update.Parameters.AddWithValue("@tipo", seccion);

               
                    
                        update.ExecuteNonQuery();
                        MostrarMensaje("** Se actualizó correctamente **", "error", "Normal", "Incorrecto");
                        BuscarSecciones(HiddenIdNivel.Value, HiddenIdEdificio.Value);
                        BtnCancelar.Visible = false;
                        DivNuevaSeccion.Visible = false;
                        BtnActualizar3.Visible = false;

                        cnn.Close();

                    }
                    catch (Exception)
                    {
                        MostrarMensaje("** Error al registrar una sección **", "error", "Normal", "Incorrecto");
                        cnn.Close();
                    }
                }
                else
                {
                    MostrarMensaje("** Selecciona un tipo **", "error", "Normal", "Incorrecto");
                }
            }
            else
            {
                MostrarMensaje("** El campo nombre de sección es requerido **", "error", "Normal", "Incorrecto");
            }
        }

        protected void BtnLimpiarSecciones_Click(object sender, EventArgs e)
        {
            LimpiarRegistro3();
            DivNuevaSeccion.Visible = false;
            DivSecciones.Visible = false;
            BtnActualizar3.Visible = false;
            btnGuadarSeccion.Visible = false;
            BtnCancelar.Visible = false;
          
            LimpiarRegistro();
            DivNuevoEdificio.Visible = false;
            DivEdificios.Visible = false;
        //    BtnLimpiar.Visible = false;
            DivNiveles.Visible = false;
            BtnActualizar.Visible = false;
            BtnGuardar.Visible = false;
            ddlMunicipio.SelectedIndex = 0;
            ddlEdificio.SelectedIndex = 0;
         //   ddlNivel.SelectedIndex = 0;
        //    ddlSeccion.SelectedIndex = 0;
  
        }

        //Método para limpiar un nuevo registro de partidas
        public void LimpiarRegistro3()
        {
            TxtSeccionesNuevo.Text = string.Empty;
            DropTipoNuevo.SelectedIndex = 0;
        }

        protected void BtnCancelarSecciones_Click(object sender, EventArgs e)
        {
            if (DivNuevaSeccion.Visible == true)
            {
              //  BtnLimpiar.Visible = true;
            }
            else if (lgModificarRegistro3.Visible == true)
            {
             //   BtnLimpiar.Visible = true;
                BtnActualizar3.Visible = false;

            }
            DivNuevaSeccion.Visible = false;
            btnGuadarSeccion.Visible = false;
            BtnCancelar.Visible = false;
        }

          protected void GridEdificios_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridEdificios.PageIndex = e.NewPageIndex;
            BuscarPorFiltros();
        }

        protected void GridBuscarEdificioNiveles_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridNiveles.PageIndex = e.NewPageIndex;
            BuscarNiveles(HiddenIdEdificio.Value);
        }

        protected void GridBuscarEdNivSec_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridSecciones.PageIndex = e.NewPageIndex;
            BuscarSeccionNuevo(HiddenIdNivel.Value, HiddenIdEdificio.Value );
        }

        protected void OcultarSecciones_Click(object sender, EventArgs e)
        {
            DivSecciones.Visible = false;
        }

        protected void OcultarNiveles_Click(object sender, EventArgs e)
        {
            DivNiveles.Visible = false;
        }

        protected void OcultarEdificios_Click(object sender, EventArgs e)
        {
            DivEdificios.Visible = false;
        }
    }
}