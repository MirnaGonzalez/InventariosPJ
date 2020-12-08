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
using InventariosPJEH.CVista;
using System.Text.RegularExpressions;
using System.Windows;

namespace InventariosPJEH
{
    public partial class frmCatProveedores : System.Web.UI.Page
    {
        CProveedores obJN = new CProveedores();
        BdCatProveedores obJE = new BdCatProveedores();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.Form["__EVENTTARGET"] == "AccionEliminarr")
            {
                GridBuscarProveedor_RowDeleting(this, new GridViewDeleteEventArgs(Int32.Parse(Rowindex.Value)));
            }
            else if (Request.Form["__EVENTTARGET"] == "AccionVacio")
            {
                Rowindex.Value = string.Empty;
            }

            if (!this.IsPostBack)
            {
                ObtenerEdtados();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void DropMunicipio_SelectedIndexChanged(object sender, EventArgs e)
        {
            int IdEstados = Convert.ToInt32(DropEstados.SelectedValue);
            DataTable Datos = new DataTable();
            Datos = BdCatProveedores.IniciarMunicipios(IdEstados);
            DropMunicipio.DataSource = Datos;
            DropMunicipio.DataTextField = "Municipio";
            DropMunicipio.DataValueField = "IdMunicipio";
            DropMunicipio.DataBind();
            DropMunicipio.Items.Insert(0, new ListItem("Seleccionar", "0"));
        }

        /// <summary>
        /// 
        /// </summary>
        private void ObtenerEdtados()
        {
            DataTable Datos = new DataTable();
            Datos = BdCatProveedores.IniciarEdtados();
            DropEstados.DataSource = Datos;
            DropEstados.DataTextField = "Estado";
            DropEstados.DataValueField = "IdEstado";
            DropEstados.DataBind();
            DropEstados.Items.Insert(0, new ListItem("Seleccionar", "0"));
            DropMunicipio.Items.Insert(0, new ListItem("Seleccionar", "0"));

        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void BtnBuscar_Click(object sender, EventArgs e)
        {
            HiddenProveedor.Value = TxtProveedor.Text;
            BtnActualizar.Visible = false;
            BtnCancelar.Visible = false;
            BtnLimpiar.Visible = false;
            BtnGuardar.Visible = false;
            BuscarProveedor();
        }

        /// <summary>
        /// 
        /// </summary>
        public void BuscarProveedor()
        {

            if (TxtProveedorExistente.Text != "")
            {
                DivTabla.Visible = true;
                
                GridBuscarProveedor.DataSource = BdCatProveedores.ConsultarGbProveedor(TxtProveedorExistente.Text);
                GridBuscarProveedor.DataBind();
             
                if (GridBuscarProveedor.Rows.Count == 0)
                {
                    MostrarMensaje("** No existen datos con la búsqueda solicitada **", "error", "Normal", "Incorrecto");
                }

                DivMostrarNuevoR.Visible = false;
            }
            else
            {
                MostrarMensaje("** Campo vacío **", "error", "Normal", "Incorrecto");
            }

        }

        /// <summary>
        /// 
        /// </summary>
        public void LimpiarBuscar()
        {
            TxtProveedorExistente.Text = string.Empty;

        }

        /// <summary>
        /// 
        /// </summary>
        public void LimpiarRegistro()
        {
            DropEstados.SelectedIndex = 0;
            DropMunicipio.SelectedIndex = 0;
            TxtCalle.Text = string.Empty;
            TxtColonia.Text = string.Empty;
            TxtCorreoElectronico.Text = string.Empty;
            TxtCodigoPostal.Text = string.Empty;
            TxtTelefono.Text = string.Empty;
            TxtProveedor.Text = string.Empty;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void BtnMostraOcultar(object sender, EventArgs e)
        {
            if (DivMostrarNuevoR.Visible == false)
            {
                LimpiarRegistro();
                DivMostrarNuevoR.Visible = true;
                BtnGuardar.Visible = true;
                BtnActualizar.Visible = false;
                lgNuevoRegistro.Visible = true;
                lgModificarRegistro.Visible = false;
                BtnCancelar.Visible = true;
                BtnLimpiar.Visible = true;
                DivTabla.Visible = false;

            }
            else if (DivMostrarNuevoR.Visible == true)
            {
                DivMostrarNuevoR.Visible = false;
                BtnGuardar.Visible = false;
                BtnLimpiar.Visible = false;
                BtnCancelar.Visible = false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        private Boolean ValidarFormatoCorreo(String email)
        {
            String expresion;
            expresion = "\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*";
            if (Regex.IsMatch(email, expresion))
            {
                if (Regex.Replace(email, expresion, String.Empty).Length == 0)
                {
                    return true;
                }
                else
                {
                    return false;

                }
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void BtnGuardarNuevoProvedor(object sender, EventArgs e)
        {
            if (TxtProveedor.Text != "")
            {
                if (DropEstados.SelectedIndex != 0)
                {
                    if (DropMunicipio.SelectedIndex != 0)
                    {
                        if (TxtCalle.Text != "")
                        {
                            if (TxtColonia.Text != "")
                            {
                                if (TxtCorreoElectronico.Text != "")
                                {
                                    if (ValidarFormatoCorreo(TxtCorreoElectronico.Text))
                                    {
                                        if (TxtCodigoPostal.Text != "")
                                        {
                                            if (TxtCodigoPostal.Text.Length == 5)
                                            {
                                                if (TxtTelefono.Text != "")
                                                {
                                                    if (TxtTelefono.Text.Length == 10)
                                                    {

                                                        BtnGuardar.Visible = true;
                                                        DivTabla.Visible = true;
                                                        int IdEstados = Convert.ToInt32(DropEstados.SelectedValue);
                                                        int IdMunicipio = Convert.ToInt32(DropMunicipio.SelectedValue);

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
                                                            SqlCommand cmd = new SqlCommand("SP_Insertar_Proveedor", Conn, lTransaccion);
                                                            //SqlCommand cmd = new SqlCommand("SP_Insertar_Personal1", Conn);

                                                            //Se indica al tipo de comando que es de tipo Procedimiento Alamcenado
                                                            cmd.CommandType = CommandType.StoredProcedure;
                                                            //Se limpian los parametros
                                                            cmd.Parameters.Clear();
                                                            //Comienza a mandar a cada uno de los parametros, deben de enviarse en el tipo
                                                            //de datos que coincida es sql server 
                                                            cmd.Parameters.AddWithValue("@proveedor", TxtProveedor.Text.ToUpper());
                                                            cmd.Parameters.AddWithValue("@calle", TxtCalle.Text.ToUpper());
                                                            cmd.Parameters.AddWithValue("@colonia", TxtColonia.Text.ToUpper());
                                                            cmd.Parameters.AddWithValue("@cp", TxtCodigoPostal.Text);
                                                            cmd.Parameters.AddWithValue("@telefono", TxtTelefono.Text);
                                                            cmd.Parameters.AddWithValue("@Email", TxtCorreoElectronico.Text.ToLower());
                                                            cmd.Parameters.AddWithValue("@idmunicipio", IdMunicipio);
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
                                                            {
                                                                success = true;
                                                            }
                                                            else
                                                            {
                                                                MostrarMensaje("** Error al insertar **", "error", "Normal", "Incorrecto");
                                                            }

                                                        }
                                                        catch (Exception)
                                                        {
                                                            MostrarMensaje("** Error al insertar **", "error", "Normal", "Incorrecto");
                                                        }
                                                        finally
                                                        {
                                                            if (success)
                                                            {
                                                                lTransaccion.Commit();
                                                                Conn.Close();
                                                                MostrarMensaje("** Proveedor guardado correctamente **", "error", "Normal", "Incorrecto");
                                                                buscarProveedor();
                                                                LimpiarRegistro();
                                                                DivMostrarNuevoR.Visible = false;
                                                                BtnCancelar.Visible = false;
                                                                BtnActualizar.Visible = false;
                                                            }
                                                            else
                                                            {
                                                                lTransaccion.Rollback();
                                                                Conn.Close();
                                                            }
                                                        }
                                                    }
                                                    else
                                                    {
                                                        MostrarMensaje("** El campo número de teléfono debe de ser de 10 dígitos **", "error", "Normal", "Incorrecto");
                                                    }

                                                }
                                                else
                                                {
                                                    MostrarMensaje("** El campo número de teléfono es requerido **", "error", "Normal", "Incorrecto");
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
                                        MostrarMensaje("** Correo inválido **", "error", "Normal", "Incorrecto");
                                    }
                                }
                                else
                                {
                                    MostrarMensaje("** El campo correo electrónico es requerido **", "error", "Normal", "Incorrecto");
                                }
                            }
                            else
                            {
                                MostrarMensaje("** El campo colonia es reuqerido **", "error", "Normal", "Incorrecto");
                            }
                        }
                        else
                        {
                            MostrarMensaje("** El campo calle es requerido **", "error", "Normal", "Incorrecto");
                        }
                    }
                    else
                    {
                        MostrarMensaje("** Selecciona un municipio **", "error", "Normal", "Incorrecto");
                    }
                }
                else
                {
                    MostrarMensaje("** Selecciona un estado **", "error", "Normal", "Incorrecto");
                }
            }
            else
            {
                MostrarMensaje("** El nombre del proveedor es requerido **", "error", "Normal", "Incorrecto");
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void BtnActualizar_Click(object sender, EventArgs e)
        {
            if (TxtProveedor.Text != "")
            {
                if (DropEstados.SelectedIndex != 0)
                {
                    if (DropMunicipio.SelectedIndex != 0)
                    {
                        if (TxtCalle.Text != "")
                        {
                            if (TxtColonia.Text != "")
                            {
                                if (TxtCorreoElectronico.Text != "")
                                {
                                    if (ValidarFormatoCorreo(TxtCorreoElectronico.Text))
                                    {
                                        if (TxtCodigoPostal.Text != "")
                                        {
                                            if (TxtCodigoPostal.Text.Length == 5)
                                            {

                                                if (TxtTelefono.Text != "")
                                                {
                                                    if (TxtTelefono.Text.Length == 10)
                                                    {
                                                        int IdEstados = Convert.ToInt32(DropEstados.SelectedValue);
                                                        int IdMunicipio = Convert.ToInt32(DropMunicipio.SelectedValue);

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
                                                            SqlCommand cmd = new SqlCommand("SP_Actualizar_Proveedor", Conn, lTransaccion);
                                                     

                                                            //Se indica al tipo de comando que es de tipo Procedimiento Alamcenado
                                                            cmd.CommandType = CommandType.StoredProcedure;
                                                            //Se limpian los parametros
                                                            cmd.Parameters.Clear();
                                                            //Comienza a mandar a cada uno de los parametros, deben de enviarse en el tipo
                                                            //de datos que coincida es sql server 
                                                            cmd.Parameters.AddWithValue("@idProveedor", LbId.Text);
                                                            cmd.Parameters.AddWithValue("@proveedor", TxtProveedor.Text.ToUpper());
                                                            cmd.Parameters.AddWithValue("@calle", TxtCalle.Text.ToUpper());
                                                            cmd.Parameters.AddWithValue("@colonia", TxtColonia.Text.ToUpper());
                                                            cmd.Parameters.AddWithValue("@cp", TxtCodigoPostal.Text);
                                                            cmd.Parameters.AddWithValue("@telefono", TxtTelefono.Text);
                                                            cmd.Parameters.AddWithValue("@Email", TxtCorreoElectronico.Text.ToLower());
                                                            cmd.Parameters.AddWithValue("@idmunicipio", IdMunicipio);
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
                                                        catch (Exception)
                                                        {

                                                            MostrarMensaje("Error al actualizar proveedor **", "error", "Normal", "Incorrecto");
                                                        }
                                                        finally
                                                        {
                                                            if (success)
                                                            {
                                                                lTransaccion.Commit();
                                                                Conn.Close();

                                                                MostrarMensaje("** Proveedor actualizado correctamente **", "error", "Normal", "Incorrecto");
                                                                buscarProveedor();
                                                                DivMostrarNuevoR.Visible = false;
                                                                BtnCancelar.Visible = false;
                                                                BtnActualizar.Visible = false;

                                                            }
                                                            else
                                                            {
                                                                lTransaccion.Rollback();
                                                                Conn.Close();
                                                            }
                                                        }
                                                        //}
                                                        //else
                                                        //{
                                                        //  MostrarMensaje("** El campo número de teléfono debe de ser do 10 digitos **", "error", "Normal", "Incorrecto");
                                                        //}
                                                    }
                                                    else
                                                    {
                                                        MostrarMensaje("** El número de teléfono debe de ser de 10 dígitos **", "error", "Normal", "Incorrecto");
                                                    }
                                                }
                                                else
                                                {
                                                    MostrarMensaje("** El campo número de teléfono es requerido **", "error", "Normal", "Incorrecto");
                                                }

                                            }
                                            else
                                            {
                                                MostrarMensaje("** El campo código postal debe de ser de 5 dígitos  **", "error", "Normal", "Incorrecto");
                                            }
                                        }
                                        else
                                        {
                                            MostrarMensaje("** El campo código postal es requerido **", "error", "Normal", "Incorrecto");
                                        }
                                    }
                                    else
                                    {
                                        MostrarMensaje("** Correo inválido **", "error", "Normal", "Incorrecto");
                                    }
                                }
                                else
                                {
                                    MostrarMensaje("** El campo correo electrónico es requerido **", "error", "Normal", "Incorrecto");
                                }
                            }
                            else
                            {
                                MostrarMensaje("** El campo colonia es reuqerido **", "error", "Normal", "Incorrecto");
                            }
                        }
                        else
                        {
                            MostrarMensaje("** El campo calle es requerido **", "error", "Normal", "Incorrecto");
                        }
                    }
                    else
                    {
                        MostrarMensaje("** Selecciona un municipio **", "error", "Normal", "Incorrecto");
                    }
                }
                else
                {
                    MostrarMensaje("** Selecciona un estado **", "error", "Normal", "Incorrecto");
                }
            }
            else
            {
                MostrarMensaje("** El nombre del proveedor es requerido **", "error", "Normal", "Incorrecto");
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="proveedor"></param>
        public void buscarProveedor()
        {
            DivTabla.Visible = true;
            GridBuscarProveedor.DataSource = BdCatProveedores.ConsultarGbProveedor(TxtProveedor.Text);
            GridBuscarProveedor.DataBind();

            if (GridBuscarProveedor.Rows.Count == 0)
            {
                MostrarMensaje("** No existen datos con la búsqueda solicitada **", "error", "Normal", "Incorrecto");
                DivTabla.Visible = false;
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
            LimpiarRegistro();
            DivMostrarNuevoR.Visible = false;
            BtnGuardar.Visible = false;
            BtnCancelar.Visible = false;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="Mensaje"></param>
        /// <param name="Tipo"></param>
        /// <param name="TipoFuncion"></param>
        /// <param name="ClaveMsj"></param>
        protected void MostrarMensaje(string Mensaje, string Tipo, string TipoFuncion, string ClaveMsj)
        {
            string Msj = "";
            if (TipoFuncion == "Normal")
                Msj = "MostrarMensaje('" + Mensaje + "', '" + Tipo + "');";

            else if (TipoFuncion == "NotificacionEliminar")
                //    Msj = "prueba2('" + Mensaje + "', '" + Tipo + "');";
                Msj = "confirm();";
            else
                Msj = "MostrarMensajeInterval('" + Mensaje + "', '" + Tipo + "');";


            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), ClaveMsj, Msj, true);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void GridBuscarProveedor_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            DivMostrarNuevoR.Visible = true;
            BtnActualizar.Visible = true;
            BtnGuardar.Visible = false;
            BtnCancelar.Visible = true;
            BtnLimpiar.Visible = true;
            lgNuevoRegistro.Visible = false;
            lgModificarRegistro.Visible = true;
            int index = Convert.ToInt32(e.CommandArgument);
            GridViewRow RowSelecionada = GridBuscarProveedor.Rows[index];

            string idProveedor = GridBuscarProveedor.DataKeys[index].Values["IdProveedor"].ToString();
            string Proveedor = Page.Server.HtmlDecode(RowSelecionada.Cells[1].Text);
            string Calle = Page.Server.HtmlDecode(RowSelecionada.Cells[2].Text);
            string Colonia = RowSelecionada.Cells[3].Text;
            string Estado = Page.Server.HtmlDecode(GridBuscarProveedor.DataKeys[index].Values["Estado"].ToString());
            string Municipio = Page.Server.HtmlDecode(RowSelecionada.Cells[5].Text);
            string Telefono = RowSelecionada.Cells[6].Text;
            string Correo = Page.Server.HtmlDecode(RowSelecionada.Cells[7].Text);
            string Cp = Page.Server.HtmlDecode(GridBuscarProveedor.DataKeys[index].Values["CP"].ToString());

            LbId.Text = idProveedor;
            TxtProveedor.Text = Proveedor;
            TxtCalle.Text = Calle;
            TxtColonia.Text = Colonia;
            TxtTelefono.Text = Telefono;
            TxtCorreoElectronico.Text = Correo;
            TxtCodigoPostal.Text = Cp;


            DataTable Datos = new DataTable();
            Datos = BdCatProveedores.IniciarEdtados();
            DropEstados.DataSource = Datos;
            DropEstados.DataTextField = "Estado";
            DropEstados.DataValueField = "IdEstado";
            DropEstados.DataBind();
            DropEstados.Items.Insert(0, new ListItem("Seleccionar", "0"));

            Boolean encontrado = true;
            int a = 0;
            while (encontrado)
            {
                if (DropEstados.Items[a].Text == Estado)
                {
                    DropEstados.SelectedIndex = a;
                    encontrado = false;
                }
                else
                    a++;
            }

            int IdEstados = Convert.ToInt32(DropEstados.SelectedValue);
            DataTable Datos1 = new DataTable();
            Datos1 = BdCatProveedores.IniciarMunicipios(IdEstados);
            DropMunicipio.DataSource = Datos1;
            DropMunicipio.DataTextField = "Municipio";
            DropMunicipio.DataValueField = "IdMunicipio";
            DropMunicipio.DataBind();
            DropMunicipio.Items.Insert(0, new ListItem("Seleccionar", "0"));
            encontrado = true;
            a = 0;
            while (encontrado)
            {
                if (DropMunicipio.Items[a].Text == Municipio)
                {
                    DropMunicipio.SelectedIndex = a;
                    encontrado = false;
                }
                else
                    a++;
            }
        }    
   
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void GridBuscarProveedor_RowDeleting(object sender, GridViewDeleteEventArgs e)
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
              
                try
                {
                    int n = e.RowIndex;
                    string xcod = GridBuscarProveedor.DataKeys[n].Value.ToString();
                    string priveedor = GridBuscarProveedor.Rows[e.RowIndex].Cells[1].Text;
                    obJN.IdProveedor = xcod;
                    obJE.Eliminar_Proveedor(obJN);
                    GridBuscarProveedor.EditIndex = -1;
                    buscarProveedor();
                    MostrarMensaje("** Proveedor eliminado **", "error", "Aviso", "Incorrecto");
                    Rowindex.Value = "";
                }
                catch (Exception )
                {
                    Rowindex.Value = "";
                    MostrarMensaje("** Error al eliminar proveedor **", "error", "Aviso", "Incorrecto");
                }

            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void BtnLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarRegistro();
            DivMostrarNuevoR.Visible = false;
            DivTabla.Visible = false;
            BtnLimpiar.Visible = false;
            BtnActualizar.Visible = false;
            BtnGuardar.Visible = false;
            BtnCancelar.Visible = false;
            LimpiarBuscar();

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void GridBuscarProveedor_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridBuscarProveedor.PageIndex = e.NewPageIndex;
            BuscarProveedor();
        }

       
    }
}