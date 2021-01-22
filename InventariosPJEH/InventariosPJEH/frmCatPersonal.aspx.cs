using InventariosPJEH.CVista;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using InventariosPJEH.CAccesoDatos;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Threading.Tasks;
using InventariosPJEH.CNegocios;
using System.Data.Odbc;


namespace InventariosPJEH
{

    public partial class frmCatPersonal : System.Web.UI.Page
    {
        CPersonal obJN = new CPersonal();
        BdCat_Personal obJE = new BdCat_Personal();
        public string error;
        
        protected void Page_Load(object sender, EventArgs e)
        {
            BdCat_Personal cPersonal = new BdCat_Personal();
            if (Request.Form["__EVENTTARGET"] == "AccionEliminarr")
            {
                GridBuscar_RowDeleting(this, new GridViewDeleteEventArgs(Int32.Parse(Rowindex.Value)));
            }
            else if (Request.Form["__EVENTTARGET"] == "AccionVacio")
            {
                Rowindex.Value = string.Empty;

            }
            if (!this.IsPostBack)
            {
                IniciarLlenadoDropDown();
                IniciarLlenadoDropDownNuevo();
                CargoSeleccionado();
            }
        }

        /// <summary>
        ///  Llenar el Tipo de Área - Sección Buscar
        /// </summary>
        private void IniciarLlenadoDropDown()
        {
            
            DataTable Datos = new DataTable();
            Datos = BdCat_Personal.ClasificaciónUA();
            ddlClasificacion.DataSource = Datos;

            ddlClasificacion.DataTextField = "Descripcion";
            ddlClasificacion.DataValueField = "IdAbreviatura";

            ddlClasificacion.DataBind();
            ddlClasificacion.Items.Insert(0, new ListItem("Seleccionar", "0"));
            ddlUniAdmin.Items.Insert(0, new ListItem("Seleccionar", "0"));
            ddlDistrito.Items.Insert(0, new ListItem("Seleccionar", "0"));
        }

        /// <summary>
        ///  Llenar el Tipo de Área - Sección Nuevo/Moficar
        /// </summary>
        private void IniciarLlenadoDropDownNuevo()
        {
            DataTable Datos = new DataTable();
            Datos = BdCat_Personal.ClasificaciónUA();
            DropClasificacionNuevo.DataSource = Datos;
            DropClasificacionNuevo.DataTextField = "Descripcion";
            DropClasificacionNuevo.DataValueField = "IdAbreviatura";
            DropClasificacionNuevo.DataBind();
            DropClasificacionNuevo.Items.Insert(0, new ListItem("Seleccionar", "0"));
            DropUniAdminNuevo.Items.Insert(0, new ListItem("Seleccionar", "0"));
            DropDistritoNuevo.Items.Insert(0, new ListItem("Seleccionar", "0"));
            DropCargoNuevo.Items.Insert(0, new ListItem("Seleccionar", "0"));
        }

        /// <summary>
        ///  Activar Distrito si es Primer Instancia
        /// </summary>
        protected void ClasificacionSeleccionado(object sender, EventArgs e)
        {

            if (ddlClasificacion.SelectedItem.Text == "PRIMERA INSTANCIA")
            {
                ddlUniAdmin.SelectedIndex = 0;
                this.ddlDistrito.Visible = true;
                this.LabelDistrito.Visible = true;
                DataTable Datoss = new DataTable();
                Datoss = BdCat_Personal.ObtenerDistritos();
                ddlDistrito.DataSource = Datoss;
                ddlDistrito.DataTextField = "Distrito";
                ddlDistrito.DataValueField = "IdDistrito";
                ddlDistrito.DataBind();
                ddlDistrito.Items.Insert(0, new ListItem("Seleccionar", "0"));
            }
            else
            {
                int IdClasificacion = Convert.ToInt32(ddlClasificacion.SelectedValue);
                DataTable Datos = new DataTable();
                Datos = BdCat_Personal.ObtenerClasificacion(IdClasificacion);
                ddlUniAdmin.DataSource = Datos;
                ddlUniAdmin.DataTextField = "UniAdmin";
                ddlUniAdmin.DataValueField = "idUniAdmin";
                ddlUniAdmin.DataBind();
                ddlUniAdmin.Items.Insert(0, new ListItem("Seleccionar", "0"));
                this.ddlDistrito.Visible = false;
                this.LabelDistrito.Visible = false;
            }

        }

        /// <summary>
        ///  Llenar Unidad Administrativa - Sección Buscar
        /// </summary>
        public void llenarCatalogoUniAdmin(string Unidad)
        {

            if (DropClasificacionNuevo.SelectedItem.Text == "PRIMERA INSTANCIA")
            {
                DropUniAdminNuevo.SelectedIndex = 0;
                this.DropDistritoNuevo.Visible = true;
                this.Distrito1.Visible = true;
                DataTable Datoss = new DataTable();
                Datoss = BdCat_Personal.ObtenerDistritos();
                DropDistritoNuevo.DataSource = Datoss;
                DropDistritoNuevo.DataTextField = "Distrito";
                DropDistritoNuevo.DataValueField = "IdDistrito";
                DropDistritoNuevo.DataBind();
                DropDistritoNuevo.Items.Insert(0, new ListItem("Seleccionar", "0"));
            }
            else
            {
                int IdClasificacion = Convert.ToInt32(DropClasificacionNuevo.SelectedValue);
                DataTable Datos = new DataTable();
                Datos = BdCat_Personal.ObtenerClasificacionPorNombre(Unidad);
                DropUniAdminNuevo.DataSource = Datos;
                DropUniAdminNuevo.DataTextField = "UniAdmin";
                DropUniAdminNuevo.DataValueField = "idUniAdmin";
                DropUniAdminNuevo.DataBind();
                DropUniAdminNuevo.Items.Insert(0, new ListItem("Seleccionar", "0"));
                this.DropDistritoNuevo.Visible = false;
                this.Distrito1.Visible = false;
            }

        }

        /// <summary>
        ///  Llenar el Tipo de Área - Sección Nuevo/Modificar
        /// </summary>

        protected void ClasificacionSeleccionadoNuevo(object sender, EventArgs e)
        {
            if (DropClasificacionNuevo.SelectedItem.Text == "PRIMERA INSTANCIA")
            {
                DropUniAdminNuevo.SelectedIndex = 0;
                this.DropDistritoNuevo.Visible = true;
                this.Distrito1.Visible = true;
                DataTable Datoss = new DataTable();
                Datoss = BdCat_Personal.ObtenerDistritos();
                DropDistritoNuevo.DataSource = Datoss;
                DropDistritoNuevo.DataTextField = "Distrito";
                DropDistritoNuevo.DataValueField = "IdDistrito";
                DropDistritoNuevo.DataBind();
                DropDistritoNuevo.Items.Insert(0, new ListItem("Seleccionar", "0"));
            }
            else
            {
                int IdClasificacion = Convert.ToInt32(DropClasificacionNuevo.SelectedValue);
                DataTable Datos = new DataTable();
                Datos = BdCat_Personal.ObtenerClasificacion(IdClasificacion);
                DropUniAdminNuevo.DataSource = Datos;
                DropUniAdminNuevo.DataTextField = "UniAdmin";
                DropUniAdminNuevo.DataValueField = "idUniAdmin";
                DropUniAdminNuevo.DataBind();
                DropUniAdminNuevo.Items.Insert(0, new ListItem("Seleccionar", "0"));
                this.DropDistritoNuevo.Visible = false;
                this.Distrito1.Visible = false;
            }

        }

        /// <summary>
        ///  Llenar Cargo
        /// </summary>
        private void CargoSeleccionado()
        {
     
            DataTable Datos = new DataTable();
            Datos = BdCat_Personal.ObtenerCargos();
            DropCargoNuevo.DataSource = Datos;
            DropCargoNuevo.DataTextField = "Cargo";
            DropCargoNuevo.DataValueField = "IdCargo";
            DropCargoNuevo.DataBind();
            DropCargoNuevo.Items.Insert(0, new ListItem("Seleccionar", "0"));

        }

        /// <summary>
        ///  Clic Botón Buscar
        /// </summary>
        protected void BtnBuscar_Click(object sender, EventArgs e)
        {
            HiddenClaveEmpleado.Value = TxtClaveEmpleadoNuevo.Text;
            string nomina = Convert.ToString(DropNomina.SelectedItem);
       
                BuscarPersona();
           }

        




        /// <summary>
        ///  Función para buscar persona
        /// </summary>
        public void BuscarPersona()
        {

            if (ddlClasificacion.SelectedIndex != 0 | ddlUniAdmin.SelectedIndex != 0 | txtClaveEmpleado.Text != "" | txtNombre.Text != "" | txtAPaterno.Text != "" | txtAMaterno.Text != "")
            {
                //Parámetros de Búsqueda
                string clasificacion = Convert.ToString(ddlClasificacion.SelectedItem);
                string distrito = Convert.ToString(ddlDistrito.SelectedItem);
                string uniadmin = Convert.ToString(ddlUniAdmin.SelectedItem);

                if (clasificacion == "Seleccionar")
                {
                    clasificacion = "";
                    MostrarMensaje("** El tipo de área es requerido **", "error", "Normal", "Incorrecto");
                }

                if (distrito == "Seleccionar")
                {
                    distrito = "";
                }

                if (uniadmin == "Seleccionar")
                {
                    uniadmin = "";
                }


                DivTabla.Visible = true;

                gridBuscar.DataSource = BdCat_Personal.ConsultarGbPersonal(clasificacion, distrito, uniadmin, txtClaveEmpleado.Text, txtNombre.Text, txtAPaterno.Text, txtAMaterno.Text);
                gridBuscar.DataBind();


                if (gridBuscar.Rows.Count==0)
                    {

                   
                    MostrarMensaje("** No existen datos con la búsqueda solicitada **", "error", "Normal", "Incorrecto");
                    }

             

            }
            else
            {
                MostrarMensaje("** El tipo de área es requerida  **", "error", "Normal", "Incorrecto");
            }

        }


        public void mostrarEliminado(string claveBuscarEmpleado)
        {
            DivTabla.Visible = true;
            HiddenClaveEmpleado.Value = LbId.Text;

            //Parámetros de Búsqueda
            string clasificacion = Convert.ToString(ddlClasificacion.SelectedItem);
            string distrito = Convert.ToString(ddlDistrito.SelectedItem);
            string uniadmin = Convert.ToString(ddlUniAdmin.SelectedItem);

            if (clasificacion == "Seleccionar")
            {
                clasificacion = "";
                MostrarMensaje("** El tipo de área es requerido **", "error", "Normal", "Incorrecto");
            }

            if (distrito == "Seleccionar")
            {
                distrito = "";
            }

            if (uniadmin == "Seleccionar")
            {
                uniadmin = "";
            }

            gridBuscar.DataSource = BdCat_Personal.ConsultarGbPersonal(clasificacion, distrito, uniadmin, claveBuscarEmpleado, txtNombre.Text, txtAPaterno.Text, txtAMaterno.Text);

            gridBuscar.DataBind();
            LimpiarBuscar();
        }

        public void BuscarPersonalNuevo()
        {
            //Parámetros de Búsqueda
            string clasificacion = Convert.ToString(ddlClasificacion.SelectedItem);
            string distrito = Convert.ToString(ddlDistrito.SelectedItem);
            string uniadmin = Convert.ToString(ddlUniAdmin.SelectedItem);

            if (clasificacion == "Seleccionar")
            {
                clasificacion = "";
                MostrarMensaje("** El tipo de área es requerido **", "error", "Normal", "Incorrecto");
            }

            if (distrito == "Seleccionar")
            {
                distrito = "";
            }

            if (uniadmin == "Seleccionar")
            {
                uniadmin = "";
            }

            gridBuscar.DataSource = BdCat_Personal.ConsultarGbPersonal(clasificacion, distrito, uniadmin, TxtClaveEmpleadoNuevo.Text, TxtNombreNuevo.Text, TxtAPaternoNuevo.Text, TxtAMaternoNuevo.Text);
            gridBuscar.DataBind();
        }


        public void BuscarPersona(string claveBuscarEmpleado)
        {

            DivTabla.Visible = true;
            int Autenticar = 0;
            TResultado ResAutenticacion = new TResultado();

            //Parámetros de Búsqueda
            string clasificacion = Convert.ToString(ddlClasificacion.SelectedItem);
            string distrito = Convert.ToString(ddlDistrito.SelectedItem);
            string uniadmin = Convert.ToString(ddlUniAdmin.SelectedItem);

            if (clasificacion == "Seleccionar")
            {
                clasificacion = "";
                 MostrarMensaje("** El tipo de área es requerido **", "error", "Normal", "Incorrecto");
            }

            if (distrito == "Seleccionar")
            {
                distrito = "";
            }

            if (uniadmin == "Seleccionar")
            {
                uniadmin = "";
            }



            gridBuscar.DataSource = BdCat_Personal.ConsultarGbPersonal(clasificacion, distrito, uniadmin, TxtClaveEmpleadoNuevo.Text, TxtNombreNuevo.Text, TxtAPaternoNuevo.Text, TxtAMaternoNuevo.Text);
            gridBuscar.DataBind();

            if (Autenticar != -1)
            {
                if (Autenticar != -0)
                {
                    if (Autenticar != -1)
                    {
                        if (Autenticar != 0)
                        {

                            EnvRecParametros Enviar = new EnvRecParametros();
                            Enviar.MsjMostrar = "** Bienvenido **";

                            Page.Session["EnvRecParametros"] = Enviar;
                            string claveEmpleado = txtClaveEmpleado.Text;
                            string nombre = txtNombre.Text;
                            string aPaterno = txtAPaterno.Text;
                            string aMaterno = txtAMaterno.Text;
                            TResultado Resultado = new TResultado();

                        }
                    }
                }
            }
            LimpiarBuscar();
        }


        public void LimpiarBuscar()
        {
            ddlUniAdmin.SelectedIndex = 0;
            ddlClasificacion.SelectedIndex = 0;
            ddlDistrito.SelectedIndex = 0;
            txtClaveEmpleado.Text = string.Empty;
            txtNombre.Text = string.Empty;
            txtAMaterno.Text = string.Empty;
            txtAPaterno.Text = string.Empty;
        }

        public void LimpiarRegistro()
        {
            DropUniAdminNuevo.SelectedIndex = 0;
            DropClasificacionNuevo.SelectedIndex = 0;
            DropDistritoNuevo.SelectedIndex = 0;
            DropCargoNuevo.SelectedIndex = 0;
            DropNomina.SelectedIndex = 0;
            TxtClaveEmpleadoNuevo.Text = string.Empty;
            TxtNombreNuevo.Text = string.Empty;
            TxtAMaternoNuevo.Text = string.Empty;
            TxtAPaternoNuevo.Text = string.Empty;
           
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
        protected void MostrarMensajeConfirm(string Mensaje, string Tipo, string confirmSi, string confirmNo)
        {
            string Msj = "";
            if (confirmSi == "Normal")
                Msj = "MostrarMensaje('" + Mensaje + "', '" + Tipo + "', '" + confirmSi + "', '" + confirmNo + "');";
            else
                Msj = "MostrarMensajeInterval('" + Mensaje + "', '" + Tipo + "');";

            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), confirmNo, Msj, true);
        }

        protected void BtnGuardarNuevoPersonal(object sender, EventArgs e)
        {
            string titular = string.Empty;
            string estatusper = string.Empty;
            string estatusinv = string.Empty;

            if (DropClasificacionNuevo.SelectedIndex != 0)
            {
                if (DropUniAdminNuevo.SelectedIndex != 0)
                {
                    if (DropCargoNuevo.SelectedIndex != 0)
                    {
                        if (TxtClaveEmpleadoNuevo.Text != "")
                        {
                           // if (TxtClaveEmpleadoNuevo.Text.Length == 4)
                           // {
                                if (TxtNombreNuevo.Text != "")
                                {
                                    if (TxtAPaternoNuevo.Text != "")
                                    {
                                        if (TxtAMaternoNuevo.Text != "")
                                        {
                                            if (DropNomina.SelectedIndex != 0)
                                            {
                                                btnGuardar.Visible = true;
                                                int uniAdmin = Convert.ToInt32(DropUniAdminNuevo.SelectedValue);
                                                int cargo = Convert.ToInt32(DropCargoNuevo.SelectedValue);
                                                string nomina = Convert.ToString(DropNomina.SelectedItem);

                                                if (rbTitularNo.Checked)
                                                {
                                                    titular = "N";
                                                }
                                                else if (rbTitularSi.Checked)
                                                {
                                                    titular = "S";
                                                }
                                                if (rbSPendiente.Checked)
                                                {
                                                    estatusinv = "SP";
                                                }
                                                else if (rbPendiente.Checked)
                                                {
                                                    estatusinv = "P";
                                                }
                                                if (rbEstatusPerII.Checked)
                                                {
                                                    estatusper = "I";
                                                }
                                                else if (rbEstatusPerA.Checked)
                                                {
                                                    estatusper = "A";
                                                }
                                                SqlConnection Conn = new SqlConnection(CConexion.Obtener());

                                                Conn.Open();
                                                string consultar = "SELECT * from Cat_Personal WHERE ClaveEmpleado= '" + TxtClaveEmpleadoNuevo.Text + "'";
                                                SqlCommand cmd1 = new SqlCommand(consultar, Conn);
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
                                                Conn.Close();

                                                if (string.IsNullOrWhiteSpace(TxtClaveEmpleadoNuevo.Text))
                                                {
                                                    MostrarMensaje("** Error campo vacío **", "error", "Normal", "Incorrecto");

                                                }
                                                else
                                                {
                                                    if (validar == TxtClaveEmpleadoNuevo.Text)
                                                    {
                                                        MostrarMensaje("** Esta clave ya existe **", "error", "Normal", "Incorrecto");

                                                    }
                                                    else
                                                    {
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

                                                        lTransaccion = Conn.BeginTransaction(System.Data.IsolationLevel.Serializable);
                                                        //Especificamos el comando, en este caso el nombre del Procedimiento Almacenado, lTransaccion
                                                        SqlCommand cmd = new SqlCommand("SP_Insertar_Personal", Conn, lTransaccion);

                                                        cmd.CommandType = CommandType.StoredProcedure;

                                                        cmd.Parameters.Clear();

                                                        cmd.Parameters.AddWithValue("@claveEmpleado", TxtClaveEmpleadoNuevo.Text);
                                                        cmd.Parameters.AddWithValue("@nombre", TxtNombreNuevo.Text.ToUpper().Trim());
                                                        cmd.Parameters.AddWithValue("@aPaterno", TxtAPaternoNuevo.Text.ToUpper().Trim());
                                                        cmd.Parameters.AddWithValue("@aMaterno", TxtAMaternoNuevo.Text.ToUpper().Trim());
                                                        cmd.Parameters.AddWithValue("@idUniAdmin", uniAdmin);
                                                        cmd.Parameters.AddWithValue("@idCargo", cargo);
                                                        cmd.Parameters.AddWithValue("@titular", titular.ToUpper());
                                                        cmd.Parameters.AddWithValue("@estatusPer", estatusper.ToUpper());
                                                        cmd.Parameters.AddWithValue("@estatusInv", estatusinv.ToUpper());
                                                        cmd.Parameters.AddWithValue("@nomina", nomina.ToUpper());
                                                        //declaramos el valor de retorno
                                                        SqlParameter ValorRetorno = new SqlParameter("@Comprobacion", SqlDbType.Int);

                                                        ValorRetorno.Direction = ParameterDirection.Output;
                                                        cmd.Parameters.Add(ValorRetorno);

                                                        cmd.ExecuteNonQuery();
                                                        Valor_Retornado = Convert.ToInt32(ValorRetorno.Value);
                                                        //Dependiendo del valor de retorno la variable success si el procedimiento retorna un 1 la operación se realizó
                                                        //con exito de no ser así s emantiene en false y por lo tanto falló la operación
                                                        if (Valor_Retornado == 1)
                                                            success = true;
                                                      
                                                        ///////////////////////////////////////////////////


                                                       
                                                    }
                                                        catch (Exception ex)
                                                        {
                                                      
                                                            MostrarMensaje(ex.Message, "error", "Normal", "Incorrecto");
                                                        }
                                                        finally
                                                        {
                                                            if (success)
                                                            {
                                                                lTransaccion.Commit();
                                                                Conn.Close();
                                                                string IdEmpleado;

                                                            if (rbTitularSi.Checked.ToString() == "True")
                                                            {
                                                                Conn.Open();
                                                                string conSQLEmpleado = "SELECT MAX(IdEmpleado) from Cat_Personal where IdUniAdmin = '" + uniAdmin + "'";
                                                                SqlCommand cmd2 = new SqlCommand(conSQLEmpleado, Conn);
                                                                 IdEmpleado = Convert.ToString(cmd2.ExecuteScalar());
                                                                Conn.Close();
                                                            }
                                                            else
                                                            {
                                                                IdEmpleado = "";
                                                            }
                                                            //ACTUALIZAR EL TITULAR DE UNA ÁREA ADMINISTRATIVA
                                                            ActualizarTitular(uniAdmin, IdEmpleado);

                                                          

                                                            MostrarMensaje("** Personal guardado correctamente **", "error", "Normal", "Incorrecto");
                                                                BuscarPersonalNuevo();
                                                                DivMostrarNuevo.Visible = false;
                                                                btnGuardar.Visible = false;
                                                                btnCancelar.Visible = false;
                                                                LimpiarRegistro();

                                                            }
                                                            else
                                                            {
                                                                lTransaccion.Rollback();
                                                                Conn.Close();
                                                            }
                                                        }

                                                    }///

                                                }
                                            }
                                            else
                                            {
                                                MostrarMensaje("** Seleccione el campo nómina **", "error", "Normal", "Incorrecto");
                                            }
                                        }
                                        else
                                        {
                                            MostrarMensaje("** El apellido materno es requerido **", "error", "Normal", "Incorrecto");
                                        }
                                    }
                                    else
                                    {
                                        MostrarMensaje("** El apellido  paterno es requerido **", "error", "Normal", "Incorrecto");
                                    }
                                }
                                else
                                {
                                    MostrarMensaje("** El nombre es requerido **", "error", "Normal", "Incorrecto");
                                }

                            }
                         // else
                         //    {
                         //       MostrarMensaje("** La clave del empleado debe de ser de 4 dígitos **", "error", "Normal", "Incorrecto");
                         // }
                      //  }
                        else
                        {
                            MostrarMensaje("** La clave del empleado es requerido **", "error", "Normal", "Incorrecto");
                        }
                    }
                    else
                    {
                        MostrarMensaje("** Seleccionar un cargo **", "error", "Normal", "Incorrecto");
                    }
                }
                else
                {
                    MostrarMensaje("** Seleccionar unidad administrativa **", "error", "Normal", "Incorrecto");
                }
            }
            else
            {
                MostrarMensaje("** Seleccionar el campo clasificación**", "error", "Normal", "Incorrecto");
            }

        }


        public void Insertar(int id, string sub, string apP)
        {
            SqlConnection cnn = new SqlConnection(ConfigurationManager.ConnectionStrings["Ejem"].ToString());
            SqlCommand cmd = new SqlCommand("SP_Insertar_Registros", cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@IdSubFondo", id));
            cmd.Parameters.Add(new SqlParameter("@SubFondo", sub));
            cmd.Parameters.Add(new SqlParameter("@Tipo", apP));
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            cnn.Open();
            da.Fill(dt);
            cnn.Close();
        }
       
        protected void RbTitularSi_CheckedChanged(object sender, EventArgs e)
        {
            HiddenTitular.Value = "S"; 
        }

        protected void RbTitularNo_CheckedChanged(object sender, EventArgs e)
        {     
            HiddenTitular.Value = "N";
        }

        protected void RbEstatusPerA_CheckedChanged(object sender, EventArgs e)
        {
            HiddenStatusPersona.Value = "A";
        }

        protected void RbEstatusPerI_CheckedChanged(object sender, EventArgs e)
        {    
            HiddenStatusPersona.Value = "I";
        }

        protected void RadioButton1_CheckedChanged(object sender, EventArgs e)
        {
            HiddenStatusInventario.Value = "SP";
        }

        protected void RadioButton2_CheckedChanged(object sender, EventArgs e)
        {
            HiddenStatusInventario.Value = "P";
        }

        protected void GridBuscar_RowCommand(object sender, GridViewCommandEventArgs e)
        {
                DivMostrarNuevo.Visible = true;
                lgNuevoRegistro.Visible = false;
                btnActualizar.Visible = true;
                btnCancelar.Visible = true;
                btnGuardar.Visible = false;
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow RowSelecionada = gridBuscar.Rows[index];

                string idEmpleado = Page.Server.HtmlDecode(gridBuscar.DataKeys[index].Values["IdEmpleado"].ToString());
                string clave = Page.Server.HtmlDecode(RowSelecionada.Cells[1].Text);
                string Nombres = Page.Server.HtmlDecode(RowSelecionada.Cells[2].Text);
                string apellidoP = Page.Server.HtmlDecode(RowSelecionada.Cells[3].Text);
                string apellidoM = Page.Server.HtmlDecode(RowSelecionada.Cells[4].Text);
                string titular = (RowSelecionada.Cells[5].Controls[1] as Label).Text;
                string estatusPers = (RowSelecionada.Cells[6].Controls[1] as Label).Text;
                string estatusInve = (RowSelecionada.Cells[7].Controls[1] as Label).Text;
                string nomina = Page.Server.HtmlDecode((RowSelecionada.Cells[8].Controls[1] as Label).Text);
                string descripcion = Page.Server.HtmlDecode(gridBuscar.DataKeys[index].Values["Descripcion"].ToString());

                string uniAdmin = Page.Server.HtmlDecode(gridBuscar.DataKeys[index].Values["UniAdmin"].ToString());

                string cargo = Page.Server.HtmlDecode(gridBuscar.DataKeys[index].Values["Cargo"].ToString());
                string distrito = Page.Server.HtmlDecode(gridBuscar.DataKeys[index].Values["Distrito"].ToString());

                LbId.Text = idEmpleado;
                HiddenClaveEmpleado.Value = clave;
                TxtClaveEmpleadoNuevo.Text = clave;
                TxtNombreNuevo.Text = Nombres;
                TxtAPaternoNuevo.Text = apellidoP;
                TxtAMaternoNuevo.Text = apellidoM;
                Boolean encontrado = true;
                int a = 0;

                if (descripcion != "PRIMERA INSTANCIA")
                {

                    encontrado = true;
                    a = 0;
                    while (encontrado)
                    {
                        if (DropClasificacionNuevo.Items[a].Text == descripcion)
                        {
                            DropClasificacionNuevo.SelectedIndex = a;
                            encontrado = false;
                        }
                        else
                            a++;
                    }

                    int IdClasificacion = Convert.ToInt32(DropClasificacionNuevo.SelectedValue);
                    DataTable Datos = new DataTable();
                    Datos = BdCat_Personal.ObtenerClasificacion(IdClasificacion);
                    DropUniAdminNuevo.DataSource = Datos;
                    DropUniAdminNuevo.DataTextField = "UniAdmin";
                    DropUniAdminNuevo.DataValueField = "idUniAdmin";
                    DropUniAdminNuevo.DataBind();
                    DropUniAdminNuevo.Items.Insert(0, new ListItem("Seleccionar", "0"));
                    this.DropDistritoNuevo.Visible = false;
                    this.Distrito1.Visible = false;

                    encontrado = true;
                    a = 0;
                    while (encontrado)
                    {
                        if (DropUniAdminNuevo.Items[a].Text == uniAdmin)
                        {
                            DropUniAdminNuevo.SelectedIndex = a;
                            encontrado = false;
                        }
                        else
                            a++;
                    }

                }
                else
                {



                    encontrado = true;
                    a = 0;
                    while (encontrado)
                    {
                        if (DropClasificacionNuevo.Items[a].Text == descripcion)
                        {
                            DropClasificacionNuevo.SelectedIndex = a;
                            encontrado = false;
                        }
                        else
                            a++;
                    }


                    DropUniAdminNuevo.SelectedIndex = 0;
                    this.DropDistritoNuevo.Visible = true;
                    this.Distrito1.Visible = true;
                    DataTable Datoss = new DataTable();
                    Datoss = BdCat_Personal.ObtenerDistritos();
                    DropDistritoNuevo.DataSource = Datoss;
                    DropDistritoNuevo.DataTextField = "Distrito";
                    DropDistritoNuevo.DataValueField = "IdDistrito";
                    DropDistritoNuevo.DataBind();
                    DropDistritoNuevo.Items.Insert(0, new ListItem("Seleccionar", "0"));

                    encontrado = true;
                    a = 0;
                    while (encontrado)
                    {
                        if (DropDistritoNuevo.Items[a].Text == distrito)
                        {
                            DropDistritoNuevo.SelectedIndex = a;
                            encontrado = false;
                        }
                        else
                            a++;
                    }


                    int IdClasificacion = Convert.ToInt32(DropClasificacionNuevo.SelectedValue);
                    DataTable Datos = new DataTable();
                    Datos = BdCat_Personal.ObtenerClasificacion(IdClasificacion);
                    DropUniAdminNuevo.DataSource = Datos;
                    DropUniAdminNuevo.DataTextField = "UniAdmin";
                    DropUniAdminNuevo.DataValueField = "idUniAdmin";
                    DropUniAdminNuevo.DataBind();
                    DropUniAdminNuevo.Items.Insert(0, new ListItem("Seleccionar", "0"));

                    encontrado = true;
                    a = 0;
                    while (encontrado)
                    {
                        if (DropUniAdminNuevo.Items[a].Text == uniAdmin)
                        {
                            DropUniAdminNuevo.SelectedIndex = a;
                            encontrado = false;
                        }
                        else
                            a++;
                    }

                }

                ///distrito
                ///
                encontrado = true;
                a = 0;
                while (encontrado)
                {
                    if (DropCargoNuevo.Items[a].Text == cargo)
                    {
                        DropCargoNuevo.SelectedIndex = a;
                        encontrado = false;
                    }
                    else

                        a++;
                }

                encontrado = true;
                a = 0;
                while (encontrado)
                {
                    if (DropNomina.Items[a].Text == nomina)
                    {
                        DropNomina.SelectedIndex = a;
                        encontrado = false;
                    }
                    else

                        a++;
                }

                if (titular.Equals(rbTitularSi.Text))
                {
                    HiddenTitular.Value = "S";
                    rbTitularSi.Checked = true;
                    rbTitularNo.Checked = false;
                }
                else if (titular.Equals(rbTitularNo.Text))
                {

                    HiddenTitular.Value = "N";
                    rbTitularNo.Checked = true;
                    rbTitularSi.Checked = false;
                }


                if (estatusPers.Equals(rbEstatusPerA.Text))
                {
                    HiddenStatusPersona.Value = "A";
                    rbEstatusPerA.Checked = true;
                    rbEstatusPerII.Checked = false;
                }
                else if (estatusPers.Equals(rbEstatusPerII.Text))
                {
                    HiddenStatusPersona.Value = "I";
                    rbEstatusPerA.Checked = false;
                    rbEstatusPerII.Checked = true;
                }


                if (estatusInve.Equals(rbSPendiente.Text))
                {
                    HiddenStatusInventario.Value = "SP";
                    rbSPendiente.Checked = true;
                    rbPendiente.Checked = false;
                }
                else if (estatusInve.Equals(rbPendiente.Text))
                {
                    HiddenStatusInventario.Value = "P";
                    rbSPendiente.Checked = false;
                    rbPendiente.Checked = true;
                }
        }

        protected void BtnActualizar_Click(object sender, EventArgs e)
        {
            if (DropClasificacionNuevo.SelectedIndex != 0)
            {
                if (DropUniAdminNuevo.SelectedIndex != 0)
                {
                    if (DropCargoNuevo.SelectedIndex != 0)
                    {
                        if (TxtClaveEmpleadoNuevo.Text != "")
                        {
                          //  if (TxtClaveEmpleadoNuevo.Text.Length == 4)
                            {
                                if (TxtNombreNuevo.Text != "")
                            {
                                if (TxtAPaternoNuevo.Text != "")
                                {
                                    if (TxtAMaternoNuevo.Text != "")
                                    {
                                        if (DropNomina.SelectedIndex != 0)
                                        {
                                            HiddenClaveEmpleado.Value = TxtClaveEmpleadoNuevo.Text;
                                            int uniAdmin = Convert.ToInt32(DropUniAdminNuevo.SelectedValue);
                                            int cargo = Convert.ToInt32(DropCargoNuevo.SelectedValue);
                                            string nomina = Convert.ToString(DropNomina.SelectedItem);

                                            SqlConnection Conn = new SqlConnection(CConexion.Obtener());
                                                bool success = false;
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
                                                    SqlCommand cmd = new SqlCommand("SP_Actualizar_Personal", Conn, lTransaccion);

                                                    cmd.CommandType = CommandType.StoredProcedure;
                                                    cmd.Parameters.Clear();

                                                    cmd.Parameters.AddWithValue("@IdEmpleado", LbId.Text);
                                                    cmd.Parameters.AddWithValue("@ClaveEmpleado", TxtClaveEmpleadoNuevo.Text);
                                                    cmd.Parameters.AddWithValue("@Nombre", TxtNombreNuevo.Text.ToUpper().Trim());
                                                    cmd.Parameters.AddWithValue("@APaterno", TxtAPaternoNuevo.Text.ToUpper().Trim());
                                                    cmd.Parameters.AddWithValue("@AMaterno", TxtAMaternoNuevo.Text.ToUpper().Trim());
                                                    cmd.Parameters.AddWithValue("@IdUniAdmin", uniAdmin);
                                                    cmd.Parameters.AddWithValue("@IdCargo", cargo);
                                                    cmd.Parameters.AddWithValue("@Titular", HiddenTitular.Value.ToUpper());
                                                    cmd.Parameters.AddWithValue("@EstatusPer", HiddenStatusPersona.Value.ToUpper());
                                                    cmd.Parameters.AddWithValue("@EstatusInv", HiddenStatusInventario.Value.ToUpper());
                                                    cmd.Parameters.AddWithValue("@Nomina", nomina.ToUpper());


                                                    SqlParameter ValorRetorno = new SqlParameter("@Comprobacion", SqlDbType.Int);
                                                    ValorRetorno.Direction = ParameterDirection.Output;
                                                    cmd.Parameters.Add(ValorRetorno);

                                                    cmd.ExecuteNonQuery();

                                                    Valor_Retornado = Convert.ToInt32(ValorRetorno.Value);

                                                    if (Valor_Retornado == 1)
                                                        success = true;

                                                    string IdEmpleado;
                                                    ///////////////////////////////////////////////////
                                                    if ( rbTitularSi.Checked.ToString() == "True")
                                                    {
                                                        IdEmpleado = LbId.Text;
                                                    }
                                                    else
                                                    {
                                                        IdEmpleado = "";
                                                    }


                                                    //ACTUALIZAR EL TITULAR DE UNA ÁREA ADMINISTRATIVA
                                                    ActualizarTitular(uniAdmin, IdEmpleado);

                                                }

                                                catch (Exception ex)
                                                {



                                                    MostrarMensaje(ex.Message, "error", "Normal", "Incorrecto");
                                                }
                                                finally
                                                {
                                                    if (success)
                                                    {
                                                        lTransaccion.Commit();
                                                        Conn.Close();

                                                        MostrarMensaje("** Personal actualizado correctamente **", "error", "Normal", "Incorrecto");

                                                        BuscarPersonalNuevo();
                                                        DivMostrarNuevo.Visible = false;
                                                        btnActualizar.Visible = false;
                                                        btnCancelar.Visible = false;
                                                        LimpiarRegistro();
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
                                            MostrarMensaje("** Seleccione el campo nómina **", "error", "Normal", "Incorrecto");
                                        }
                                    }
                                    else
                                    {
                                        MostrarMensaje("** El apellido materno es requerido **", "error", "Normal", "Incorrecto");
                                    }
                                }
                                else
                                {
                                    MostrarMensaje("** El apellido  paterno es requerido **", "error", "Normal", "Incorrecto");
                                }
                            }
                            else
                            {
                                MostrarMensaje("** El nombre es requerido **", "error", "Normal", "Incorrecto");
                            }
                        }
                     //    else
                     //   {
                     //           MostrarMensaje("** La clave del empleado debe de ser de 4 dígitos **", "error", "Normal", "Incorrecto");
                     //       }
                        }
                        else
                        {
                            MostrarMensaje("** La clave de empleado es requerido **", "error", "Normal", "Incorrecto");
                        }
                    }
                    else
                    {
                        MostrarMensaje("** Seleccionar cargo **", "error", "Normal", "Incorrecto");
                    }
                }
                else
                {
                    MostrarMensaje("** Seleccionar unidad administrativa **", "error", "Normal", "Incorrecto");
                }
            }
            else
            {
                MostrarMensaje("** Seleccionar el campo clasificación**", "error", "Normal", "Incorrecto");
            }
        }

        protected void BtnCancelar_Click(object sender, EventArgs e)
        {
            LimpiarRegistro();
            DivMostrarNuevo.Visible = false;
            btnGuardar.Visible = false;
            btnCancelar.Visible = false;

        }

        protected void GridBuscar_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            DivMostrarNuevo.Visible = false;
            btnActualizar.Visible = false;
            btnGuardar.Visible = false;
            btnCancelar.Visible = false;
            if (String.IsNullOrEmpty(Rowindex.Value))
            {
                Rowindex.Value = e.RowIndex.ToString();
                MostrarMensaje("Prueba", "info", "NotificacionEliminar", "Inicio");
            }
            else
            {
                HiddenClaveEmpleado.Value = TxtClaveEmpleadoNuevo.Text;
                int n = e.RowIndex;
                string IdEmpleado = gridBuscar.DataKeys[n].Value.ToString();
                obJN.IdEmpleado = IdEmpleado;

                Boolean encontrado = true;
            try
            {
                    if (encontrado == true)
                    {
                        SqlConnection Conn = new SqlConnection(CConexion.Obtener());
                        Conn.Open();
                        string consultar = "SELECT IdEmpleado FROM Cat_Personal WHERE IdEmpleado= '" + IdEmpleado + "' AND EstatusInv = 'P'";

                        SqlCommand cmd1 = new SqlCommand(consultar, Conn);
                        string clave = Convert.ToString(cmd1.ExecuteScalar());

                        SqlDataReader leer = cmd1.ExecuteReader();



                        if (clave == IdEmpleado)
                        {
                            MostrarMensaje("** El empleado no se puede eliminar porque tiene inventario pendiente **", "error", "Normal", "Incorrecto");

                        }

                        else
                        { 
                                                   
                            obJE.Eliminar_Personal(obJN);
                            gridBuscar.EditIndex = -1;
                            
                            MostrarMensaje("** Personal eliminado de forma correcta **", "error", "Normal", "Incorrecto");
                            Rowindex.Value = "";
                            BuscarPersona();
                        }
                    }

            }
            catch (Exception ex)
            {
                    Rowindex.Value = "";
                    MostrarMensaje("** Error al eliminar personal **", "error", "Normal", "Incorrecto");
            }
        }

        }

        protected void BtnMostraOcultar(object sender, EventArgs e)
        {
            if (DivMostrarNuevo.Visible == false)
            {
                LimpiarRegistro();
                DivTabla.Visible = false;
                DivMostrarNuevo.Visible = true;
                rbEstatusPerA.Checked = true;
                rbSPendiente.Checked = true;
                rbTitularNo.Checked = true;
                btnGuardar.Visible = true;
                btnActualizar.Visible = false;
                btnCancelar.Visible = true;
                lgNuevoRegistro.Visible = true;
               

            }
            else if (DivMostrarNuevo.Visible == true)
            {
                DivMostrarNuevo.Visible = false;
                DivTabla.Visible = false;
                btnGuardar.Visible = false;
                btnCancelar.Visible = false;
            }
        }

        protected void DropDistrito_SelectedIndexChanged(object sender, EventArgs e)
        {
            int IdDistrito = Convert.ToInt32(ddlDistrito.SelectedValue);
            DataTable Datos = new DataTable();
            Datos = BdCat_Personal.ObtenerUniAdminN(IdDistrito);
            ddlUniAdmin.DataSource = Datos;
            ddlUniAdmin.DataTextField = "UniAdmin";
            ddlUniAdmin.DataValueField = "IdUniAdmin";
            ddlUniAdmin.DataBind();
            ddlUniAdmin.Items.Insert(0, new ListItem("Seleccionar", "0"));
        }

        protected void DropDistritoNuevo_SelectedIndexChanged(object sender, EventArgs e)
        {
            int IdDistrito = Convert.ToInt32(DropDistritoNuevo.SelectedValue);
            DataTable Datos = new DataTable();
            Datos = BdCat_Personal.ObtenerUniAdminN(IdDistrito);
            DropUniAdminNuevo.DataSource = Datos;
            DropUniAdminNuevo.DataTextField = "UniAdmin";
            DropUniAdminNuevo.DataValueField = "IdUniAdmin";
            DropUniAdminNuevo.DataBind();
            DropUniAdminNuevo.Items.Insert(0, new ListItem("Seleccionar", "0"));
        }



        protected void GridBuscar_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gridBuscar.PageIndex = e.NewPageIndex;
            BuscarPersona();
        }


        public void ActualizarTitular (int uniAdmin, string IdEmpleado)
        {
            ///////////////////////////////////////////////////
            //ACTUALIZAR EL TITULAR DE UNA ÁREA ADMINISTRATIVA

            try
            {

                SqlConnection Conn1 = new SqlConnection(CConexion.Obtener());





                Conn1.Open();
                SqlCommand update = new SqlCommand("UPDATE Cat_UniAdmin SET IdEmpleado=@IdEmpleado WHERE IdUniAdmin=@IdUniAdmin", Conn1);
                update.Parameters.AddWithValue("@IdEmpleado", IdEmpleado);
                update.Parameters.AddWithValue("@IdUniAdmin", uniAdmin);
                update.ExecuteNonQuery();

                Conn1.Close();
            }

            catch (Exception ex )
            {
                 MostrarMensaje(ex.Message , "error", "Normal", "Incorrecto");
            }

        }
    }

}