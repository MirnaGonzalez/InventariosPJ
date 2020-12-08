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
    public partial class frmCatMarca : System.Web.UI.Page
    {
        CMarca obJN = new CMarca();
        BdCatMarca obJE = new BdCatMarca();
        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (Request.Form["__EVENTTARGET"] == "AccionEliminarr")
            {
                GridBuscarMarcas_RowDeleting(this, new GridViewDeleteEventArgs(Int32.Parse(Rowindex.Value)));

            }
            else if (Request.Form["__EVENTTARGET"] == "AccionVacio")
            {
                Rowindex.Value = string.Empty;

            }
            if (!this.IsPostBack)
            {
                IniciarLlenadoDropDown();
                IniciarLlenadoDropDownNuevo();

            }
        }


        /// <summary>
        /// Clase que inicia el llenado de la lista Subclase a buscar
        /// </summary>
        private void IniciarLlenadoDropDown()
        {
            DataTable Datos = new DataTable();
            Datos = BdCatMarca.IniciarLlenado();
            DropSubClaseBuscar.DataSource = Datos;
            DropSubClaseBuscar.DataTextField = "SubClase";
            DropSubClaseBuscar.DataValueField = "IdSubClase";
            DropSubClaseBuscar.DataBind();
            DropSubClaseBuscar.Items.Insert(0, new ListItem("Seleccionar", "0"));
        }


        /// <summary>
        /// Inicia el llenado de la lista SubClase para registrar una nueva marca
        /// </summary>
        private void IniciarLlenadoDropDownNuevo()
        {
            DataTable Datos = new DataTable();
            Datos = BdCatMarca.IniciarLlenado();
            DropSubClase.DataSource = Datos;
            DropSubClase.DataTextField = "SubClase";
            DropSubClase.DataValueField = "IdSubClase";
            DropSubClase.DataBind();
            DropSubClase.Items.Insert(0, new ListItem("Seleccionar", "0"));
        }

        protected void MostrarMensaje(string Mensaje, string Tipo, string TipoFuncion, string ClaveMsj)
        {
            string Msj = "";
            if (TipoFuncion == "Normal")
                Msj = "MostrarMensaje('" + Mensaje + "', '" + Tipo + "');";
            else if (TipoFuncion == "NotificacionEliminar")
                Msj = "confirm();";
            else
                Msj = "MostrarMensajeInterval('" + Mensaje + "', '" + Tipo + "');";

            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), ClaveMsj, Msj, true);
        }

        /// <summary>
        /// Evento del botón (buscar) permite buscar una marca  
        /// </summary>
        protected void BtnBuscar_Click(object sender, EventArgs e)
        {
            BuscarMarca();
        }


        /// <summary>
        /// Clase que pemrite buscar marcas relacionado a los bienes  
        /// </summary>
        public void BuscarMarca()
        {
            string subGrupo = Convert.ToString(DropSubClaseBuscar.SelectedItem);
            DivTabla.Visible = true;

            if (DropSubClaseBuscar.SelectedIndex != 0 & TxtMarcaBuscar.Text == "")
            {

                GridBuscar.DataSource = BdCatMarca.ConsultarGbSubclase(subGrupo);
                GridBuscar.DataBind();
                if (GridBuscar.Rows.Count == 0)
                {
                    MostrarMensaje("** No existen subclases relacionados a una marca solicitada **", "error", "Normal", "Incorrecto");
                    DivTabla.Visible = false;
                }


            }
            else
            {
                GridBuscar.DataSource = BdCatMarca.ConsultarGbMarca(subGrupo, TxtMarcaBuscar.Text);
                GridBuscar.DataBind();
                if (GridBuscar.Rows.Count == 0)
                {
                    MostrarMensaje("** No existen datos con la búsqueda solicitada **", "error", "Normal", "Incorrecto");
                    DivTabla.Visible = false;
                }
            }

        }

            public void BuscarNuevoRegistro()
        {
            string subGrupo = Convert.ToString(DropSubClase.SelectedItem);
            DivTabla.Visible = true;
            if (DropSubClase.SelectedIndex != 0 | TxtDescripcion.Text != "")
            {
                if (DropSubClase.SelectedIndex > 0 & TxtDescripcion.Text != "")
                {
                    GridBuscar.DataSource = BdCatMarca.ConsultarGbMarca(subGrupo, TxtDescripcion.Text);
                    GridBuscar.DataBind();
                    if (GridBuscar.Rows.Count == 0)
                    {
                        DivTabla.Visible = false;
                        MostrarMensaje("** No existen datos con la búsqueda solicitada **", "error", "Normal", "Incorrecto");
                    }
                }

            }
            else
            {
                MostrarMensaje("** Campos Vacíos **", "error", "Normal", "Incorrecto");
            }
        }

        /// <summary>
        /// Clase que sirve para limpiar la sección del registro
        /// </summary>
        public void LimpiarRegistro()
        {
            DropSubClase.SelectedIndex = 0;
            TxtDescripcion.Text = string.Empty;
        }

        //Evento del botón actualizar
        protected void BtnActualizar_Click(object sender, EventArgs e)
        {
            ActualizarMarca();
            ActualizarMarcaSubclase();
            BuscarNuevoRegistro();
            DivMostrarNuevoR.Visible = false;
            BtnActualizar.Visible = false;
            BtnCancelar.Visible = false;
        }

        
        //Método para actualizar la relación de la subclase y la marca
        private void ActualizarMarcaSubclase()
        {
            SqlConnection Conn = new SqlConnection(CConexion.Obtener());
           
            //Una variable boleana para determinar si se realizó la operacion que se realiza en el Procedimiento Almacenado
            bool success = false;
            /*Se declara una transacción para que todo lo que se realiza en ella, desde una simple inserción hasta multiples 
            o que involucren más operaciones sobre la base de datos puedan deshacerse si se presenta un error */

            SqlTransaction lTransaccion = null;
            //Variable para el valor de retorno
            int Valor_Retornado = 0;
            //inicializa un try catch
            try
            {

                Conn.Open();
                int IdSubclase = Convert.ToInt32(DropSubClase.SelectedValue);
                //se inicia la transacción
                lTransaccion = Conn.BeginTransaction(System.Data.IsolationLevel.Serializable);
                //Especificamos el comando, en este caso el nombre del Procedimiento Almacenado, lTransaccion
                SqlCommand cmd = new SqlCommand("SP_Actualizar_MarcaSubClase", Conn, lTransaccion);
                //SqlCommand cmd = new SqlCommand("SP_Insertar_Personal1", Conn);

                //Se indica al tipo de comando que es de tipo Procedimiento Alamcenado
                cmd.CommandType = CommandType.StoredProcedure;
                //Se limpian los parametros
                cmd.Parameters.Clear();
                //Comienza a mandar a cada uno de los parametros, deben de enviarse en el tipo
                //de datos que coincida es sql server 
                cmd.Parameters.AddWithValue("@idMarSub", LbMarSub.Text);
                cmd.Parameters.AddWithValue("@idSubClase", IdSubclase);
                cmd.Parameters.AddWithValue("@idMarca", lbMarca.Text);
                //declaramos el valor de retorno
                SqlParameter ValorRetorno = new SqlParameter("@Comprobacion", SqlDbType.Int);
                //asigamos el valor de retorno
                ValorRetorno.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(ValorRetorno);
                //Se executa la consulta
                cmd.ExecuteNonQuery();
                Valor_Retornado = Convert.ToInt32(ValorRetorno.Value);
                //Dependiendo del valor de retorno la variable success si el procedimiento retorna un 1 la operación se realizó
                //con exito de no ser así s emantiene en false y por lo tanto falló la operación
                if (Valor_Retornado == 1)
                    success = true;
                //MostrarMensaje("** Persona guardada", "info", "Normal");
            }
            catch (Exception )
            {
                //MostrarMensaje("** Error al insertar Personal", "info", "Normal");
                MostrarMensaje("Error al actualizar **", "error", "Normal", "Incorrecto");
            }
            finally
            {
                if (success)
                {
                    lTransaccion.Commit();
                    Conn.Close();
                    //MostrarMensaje("** Perdsona Guardada Satisfactoriamente", "info", "Normal");
                    MostrarMensaje("** Se actualizó correctamente **", "error", "Normal", "Incorrecto");
                }
                else
                {
                    lTransaccion.Rollback();
                    Conn.Close();
                }
            }

        }

        //Método para guardar la marca
        public int GuardarMarca(){

            SqlConnection Conn = new SqlConnection(CConexion.Obtener());
            //Una variable boleana para determinar si se realizó la operacion que se realiza en el Procedimiento Almacenado
            bool success = false;
            /*Se declara una transacción para que todo lo que se realiza en ella, desde una simple inserción hasta multiples 
            o que involucren más operaciones sobre la base de datos puedan deshacerse si se presenta un error */

            SqlTransaction lTransaccion = null;
            //Variable para el valor de retorno
            int Valor_Retornado = 0;
            //inicializa un try catch
            try
            {
                Conn.Open();
                //se inicia la transacción
                lTransaccion = Conn.BeginTransaction(System.Data.IsolationLevel.Serializable);
                //Especificamos el comando, en este caso el nombre del Procedimiento Almacenado, lTransaccion
                SqlCommand cmd = new SqlCommand("SP_Insertar_Marca", Conn, lTransaccion);
                //SqlCommand cmd = new SqlCommand("SP_Insertar_Personal1", Conn);

                //Se indica al tipo de comando que es de tipo Procedimiento Alamcenado
                cmd.CommandType = CommandType.StoredProcedure;
                //Se limpian los parametros
                cmd.Parameters.Clear();
                //Comienza a mandar a cada uno de los parametros, deben de enviarse en el tipo
                //de datos que coincida es sql server 
                cmd.Parameters.AddWithValue("@descripcion", TxtDescripcion.Text.ToUpper());
                //declaramos el valor de retorno
                SqlParameter ValorRetorno = new SqlParameter("@Comprobacion", SqlDbType.Int);
                //asigamos el valor de retorno
                ValorRetorno.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(ValorRetorno);
                //Se executa la consulta
                cmd.ExecuteNonQuery();
                Valor_Retornado = Convert.ToInt32(ValorRetorno.Value);
                //Dependiendo del valor de retorno la variable success si el procedimiento retorna un 1 la operación se realizó
                //con exito de no ser así s emantiene en false y por lo tanto falló la operación
                if (Valor_Retornado == 1)
                    success = true;
            }
            catch (Exception )
            {
                MostrarMensaje("** Error al insertar **", "error", "Normal", "Incorrecto");
            }
            finally
            {
                if (success)
                {
                    lTransaccion.Commit();
                    Conn.Close();
                    MostrarMensaje("** Se guardo correctamente **", "error", "Normal", "Incorrecto");
                }
                else
                {
                    lTransaccion.Rollback();
                    Conn.Close();
                }
            }
            return -1;
        }

        //Método para actualizar la marca
        public void ActualizarMarca()
        {
            SqlConnection Conn = new SqlConnection(CConexion.Obtener());
            //Una variable boleana para determinar si se realizó la operacion que se realiza en el Procedimiento Almacenado
            bool success = false;
            /*Se declara una transacción para que todo lo que se realiza en ella, desde una simple inserción hasta multiples 
            o que involucren más operaciones sobre la base de datos puedan deshacerse si se presenta un error */

            SqlTransaction lTransaccion = null;
            //Variable para el valor de retorno
            int Valor_Retornado = 0;
            //inicializa un try catch
            try
            {
                Conn.Open();
                //se inicia la transacción
                lTransaccion = Conn.BeginTransaction(System.Data.IsolationLevel.Serializable);
                //Especificamos el comando, en este caso el nombre del Procedimiento Almacenado, lTransaccion
                SqlCommand cmd = new SqlCommand("SP_Actualizar_Marca", Conn, lTransaccion);
                //SqlCommand cmd = new SqlCommand("SP_Insertar_Personal1", Conn);

                //Se indica al tipo de comando que es de tipo Procedimiento Alamcenado
                cmd.CommandType = CommandType.StoredProcedure;
                //Se limpian los parametros
                cmd.Parameters.Clear();
                //Comienza a mandar a cada uno de los parametros, deben de enviarse en el tipo
                //de datos que coincida es sql server 
                cmd.Parameters.AddWithValue("@idMarca", lbMarca.Text);
                cmd.Parameters.AddWithValue("@descripcion", TxtDescripcion.Text.ToUpper());
                //declaramos el valor de retorno
                SqlParameter ValorRetorno = new SqlParameter("@Comprobacion", SqlDbType.Int);
                //asigamos el valor de retorno
                ValorRetorno.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(ValorRetorno);
                //Se executa la consulta
                cmd.ExecuteNonQuery();
                Valor_Retornado = Convert.ToInt32(ValorRetorno.Value);
                //Dependiendo del valor de retorno la variable success si el procedimiento retorna un 1 la operación se realizó
                //con exito de no ser así s emantiene en false y por lo tanto falló la operación
                if (Valor_Retornado == 1)
                    success = true;
                //MostrarMensaje("** Persona guardada", "info", "Normal");
            }
            catch (Exception )
            {
                //MostrarMensaje("** Error al insertar Personal", "info", "Normal");
                MostrarMensaje("** Error al actualizar **", "error", "Normal", "Incorrecto");
            }
            finally
            {
                if (success)
                {
                    lTransaccion.Commit();
                    Conn.Close();
                    MostrarMensaje("** Se actualizó correctamente **", "error", "Normal", "Incorrecto");
                }
                else
                {
                    lTransaccion.Rollback();
                    Conn.Close();
                }
            }
        }

        //Evento para guardar la marca 
        protected void Guardar_Click(object sender, EventArgs e)
        {
            if(DropSubClase.SelectedIndex != 0)
            {
                if (TxtDescripcion.Text != "")
                {

                    BtnGuardar.Visible = true;
                    DivTabla.Visible = true;
                    GuardarMarca();
                    SqlConnection Conn = new SqlConnection(CConexion.Obtener());
                    Conn.Open();
                    string conSQL = "SELECT MAX(IdMarca) from Cat_Marca";
                    SqlCommand cmd1 = new SqlCommand(conSQL, Conn);
                    SqlDataReader leer = cmd1.ExecuteReader();
                    string IdMarca;
                    if (leer.Read() == true)
                    {
                        IdMarca = leer[0].ToString();
                    }
                    else
                    {
                        IdMarca = "";
                    }
                    Conn.Close();
                    //Una variable boleana para determinar si se realizó la operacion que se realiza en el Procedimiento Almacenado
                    bool success = false;
                    /*Se declara una transacción para que todo lo que se realiza en ella, desde una simple inserción hasta multiples 
                    o que involucren más operaciones sobre la base de datos puedan deshacerse si se presenta un error */

                    SqlTransaction lTransaccion = null;
                    //Variable para el valor de retorno
                    int Valor_Retornado = 0;
                    //inicializa un try catch
                    try
                    {

                        Conn.Open();
                        int IdSubclase = Convert.ToInt32(DropSubClase.SelectedValue);
                        //se inicia la transacción
                        lTransaccion = Conn.BeginTransaction(System.Data.IsolationLevel.Serializable);
                        //Especificamos el comando, en este caso el nombre del Procedimiento Almacenado, lTransaccion
                        SqlCommand cmd = new SqlCommand("SP_Insertar_MarcaSubClase", Conn, lTransaccion);
                        //SqlCommand cmd = new SqlCommand("SP_Insertar_Personal1", Conn);

                        //Se indica al tipo de comando que es de tipo Procedimiento Alamcenado
                        cmd.CommandType = CommandType.StoredProcedure;
                        //Se limpian los parametros
                        cmd.Parameters.Clear();
                        //Comienza a mandar a cada uno de los parametros, deben de enviarse en el tipo
                        //de datos que coincida es sql server 
                        cmd.Parameters.AddWithValue("@idSubClase", IdSubclase);
                        cmd.Parameters.AddWithValue("@idMarca", IdMarca);
                        //declaramos el valor de retorno
                        SqlParameter ValorRetorno = new SqlParameter("@Comprobacion", SqlDbType.Int);
                        //asigamos el valor de retorno
                        ValorRetorno.Direction = ParameterDirection.Output;
                        cmd.Parameters.Add(ValorRetorno);
                        //Se executa la consulta
                        cmd.ExecuteNonQuery();
                        Valor_Retornado = Convert.ToInt32(ValorRetorno.Value);
                        //Dependiendo del valor de retorno la variable success si el procedimiento retorna un 1 la operación se realizó
                        //con exito de no ser así s emantiene en false y por lo tanto falló la operación
                        if (Valor_Retornado == 1)
                            success = true;
                        //MostrarMensaje("** Persona guardada", "info", "Normal");
                    }
                    catch (Exception )
                    {
                        //MostrarMensaje("** Error al insertar Personal", "info", "Normal");
                        MostrarMensaje("Error al insertar **", "error", "Normal", "Incorrecto");
                    }
                    finally
                    {
                        if (success)
                        {
                            lTransaccion.Commit();
                            Conn.Close();
                            //MostrarMensaje("** Perdsona Guardada Satisfactoriamente", "info", "Normal");
                            MostrarMensaje("** Se guardo correctamente **", "error", "Normal", "Incorrecto");
                        }
                        else
                        {
                            lTransaccion.Rollback();
                            Conn.Close();
                        }
                    }
                    BuscarNuevoRegistro();
                    DivMostrarNuevoR.Visible = false;
                    BtnGuardar.Visible = false;
                    BtnCancelar.Visible = false;
                }
                else
                {
                    MostrarMensaje("** El campo nombre de la marca está vacío **", "error", "Normal", "Incorrecto");
                }
            }
            else
            {
                MostrarMensaje("** Seleccione una subclase **", "error", "Normal", "Incorrecto");
            }

        }  

        //Evento para mostrar y ocultar el nuevo registro
        protected void BtnMostraOcultar(object sender, EventArgs e)
        {
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
            else if (DivMostrarNuevoR.Visible == true)
            {
                DivMostrarNuevoR.Visible = false;
                BtnGuardar.Visible = false;
                BtnLimpiar.Visible = false;
                BtnCancelar.Visible = false;
            }
        }

        //Evento para el botón cancelar
        protected void BtnCancelar_Click(object sender, EventArgs e)
        {
            if (lgNuevoRegistro.Visible == true)
            {
                BtnLimpiar.Visible = true;
            }
            else if (lgModificarRegistro.Visible == true)
            {
                BtnLimpiar.Visible = true;
                BtnActualizar.Visible = false;
                BtnMostrarNuevoR.Visible = true;
            }
            DivMostrarNuevoR.Visible = false;
            BtnGuardar.Visible = false;
            BtnCancelar.Visible = false;
        }

        //Evento para el botón cancelar
        protected void BtnLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarRegistro();
            DivMostrarNuevoR.Visible = false;
            DivTabla.Visible = false;
            BtnLimpiar.Visible = false;
            BtnActualizar.Visible = false;
            BtnGuardar.Visible = false;
            BtnCancelar.Visible = false;
            TxtMarcaBuscar.Text = string.Empty;
            DropSubClaseBuscar.SelectedIndex = 0;
        }

        //Evento para el botón editar en la tabla
        protected void GridBuscarMarcas_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            DivMostrarNuevoR.Visible = true;
            BtnActualizar.Visible = true;
            BtnGuardar.Visible = false;
            BtnCancelar.Visible = true;
            BtnLimpiar.Visible = true;
            lgNuevoRegistro.Visible = false;
            lgModificarRegistro.Visible = true;
            int index = Convert.ToInt32(e.CommandArgument);
            GridViewRow RowSelecionada = GridBuscar.Rows[index];

            string idMarca = GridBuscar.DataKeys[index].Values["IdMarca"].ToString();
            string marca = Page.Server.HtmlDecode(RowSelecionada.Cells[1].Text);
            string idsubClase = GridBuscar.DataKeys[index].Values["IdSubclase"].ToString();
            string subClase = Page.Server.HtmlDecode(RowSelecionada.Cells[3].Text);
            string idPartida = GridBuscar.DataKeys[index].Values["IdPartida"].ToString();
            string partida = Page.Server.HtmlDecode(RowSelecionada.Cells[5].Text);
            string idMarSub = Page.Server.HtmlDecode(GridBuscar.DataKeys[index].Values["IdMarSub"].ToString());

            lbMarca.Text = idMarca;
            TxtDescripcion.Text = marca;
            LbMarSub.Text = idMarSub;
            Boolean encontrado = true;
            int a = 0;

            while (encontrado)
            {
                if (DropSubClase.Items[a].Text == subClase)
                {
                    DropSubClase.SelectedIndex = a;
                    encontrado = false;
                }
                else if (DropSubClase.Items[a].Text != subClase)
                {
                    a++;
                }
            }
        }

        //Evento para el botón eliminar en la tabla 
        protected void GridBuscarMarcas_RowDeleting(object sender, GridViewDeleteEventArgs e)
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
                        obJN.IdMarca = xcod;
                        obJE.Eliminar_Marca(obJN);
                        GridBuscar.EditIndex = -1;
                        BuscarNuevoRegistro();
                        MostrarMensaje("** Marca eliminada **", "error", "Normal", "Incorrecto");
                        Rowindex.Value = "";
                        if (GridBuscar.Rows.Count == 0)
                        {
                            DivTabla.Visible = false;
                            MostrarMensaje("** No existen datos con la búsqueda solicitada **", "error", "Normal", "Incorrecto");
                        }
                        else if(GridBuscar.Rows.Count != 0)
                        {
                            MostrarMensaje("** No se puede eliminar existen campos relacionados a otras tablas **", "error", "Normal", "Incorrecto");

                        }
                        
                    }

                }
                catch (Exception )
                {
                    Rowindex.Value = "";
                    MostrarMensaje("** Error al eliminar marca **", "error", "Normal", "Incorrecto");
                }
            }

        }

        protected void GridBuscar_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridBuscar.PageIndex = e.NewPageIndex;
            BuscarMarca();
        }
    }
    }

        