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
using CrystalDecisions.Shared;

namespace InventariosPJEH
{
    public partial class frmReparaBien : System.Web.UI.Page
    {
        public SqlConnection conexion;
        public frmReparaBien()
        {
            this.conexion = ConexionBD.getConexion();
        }
        //tipo Array
        ParameterFields parametros = new ParameterFields();
        // variable tipo Parametro
        ParameterField miparametro = new ParameterField();
        // variable para guardar valor en el parametro
        ParameterDiscreteValue valor = new ParameterDiscreteValue();
        protected void Page_Load(object sender, EventArgs e)
        {
        }
        protected void BtnBuscar_Click(object sender, EventArgs e)
        {
            if (TextBoxSerie.Text != "" || TextBoxNoInventario.Text != "")
            {
                BuscarInventario();
                BuscarSerie();
            }
            else
            {
                Response.Redirect("frmReparaBien.aspx");
            }
        }
        public void BuscarSerie()
        {
            if (TextBoxSerie.Text != "")
            {
                datosBusqueda.Visible = true;
                GridViewBusqueda.DataSource = BdReparaBien.ConsultarGbBienesReparar2(TextBoxSerie.Text);
                LimpiarBusqueda();
                GridViewBusqueda.DataBind();
                if (GridViewBusqueda.Rows.Count == 0)
                {
                    MostrarMensaje("** El bien no ha sido envíado a reparación **", "error", "Normal", "Incorrecto");
                    BtnSinReparacion.Visible = false;
                }
                else
                {
                    BtnSinReparacion.Visible = true;
                    //Obtener el valor de IdInventario y NumInventario del Grid Mostrado
                    int indexx = 0;
                    string IDV = GridViewBusqueda.DataKeys[indexx].Values["IdInventario"].ToString();
                    string Numin = GridViewBusqueda.DataKeys[indexx].Values["NumInventario"].ToString();
                    LbNuminventario.Text = Numin;
                    LbIdinv.Text = IDV;
                }
            }
            else
            {
            }
        }
        public void BuscarInventario()
        {
            if (TextBoxNoInventario.Text != "")
            {
                datosBusqueda.Visible = true;

                GridViewBusqueda.DataSource = BdReparaBien.ConsultarGbBienesReparar(TextBoxNoInventario.Text);
                LimpiarBusqueda();
                GridViewBusqueda.DataBind();
                if (GridViewBusqueda.Rows.Count == 0)
                {
                    MostrarMensaje("** El bien no ha sido envíado a Reparación **", "error", "Normal", "Incorrecto");
                    BtnSinReparacion.Visible = false;
                    salidaMant.Visible = false;
                }
                else
                {
                    BtnSinReparacion.Visible = true;
                    //Obtener el valor de IdInventario y NumInventario del Grid Mostrado
                    int indexx = 0;
                    string IDV = GridViewBusqueda.DataKeys[indexx].Values["IdInventario"].ToString();
                    string Numin = GridViewBusqueda.DataKeys[indexx].Values["NumInventario"].ToString();
                    LbNuminventario.Text = Numin;
                    LbIdinv.Text = IDV;
                    ////Código para asignar valores a Texbox de acuerdo al grid mostrado
                    int index = 0;
                    GridViewRow RowSelecionada = GridViewBusqueda.Rows[index];
                    string IdInventario = GridViewBusqueda.DataKeys[index].Values["IdInventario"].ToString();
                    string NumInventario = Page.Server.HtmlDecode(RowSelecionada.Cells[1].Text);
                    string IdActividad = GridViewBusqueda.DataKeys[index].Values["IdActividad"].ToString();
                    string IdENSU = GridViewBusqueda.DataKeys[index].Values["IdENSU"].ToString();
                    string IdUsuario = GridViewBusqueda.DataKeys[index].Values["IdUsuario"].ToString();

                    LbId.Text = IdInventario;
                    TextBoxNumInventario.Text = NumInventario;
                    LbActividad.Text = IdActividad;
                    LbENSU.Text = IdENSU;
                    LbUsuario.Text = IdUsuario;
                }
            }
            else
            {
            }
        }
        public void CambiarIdActividad()
        {
            //Metodo para Cambiar de IdActividad la tabal FichaBien
            if (LbNuminventario.Text != "")
            {
                SqlConnection cnn = new SqlConnection(CConexion.Obtener());

                //Ejecucion de Sentencia Sql para actualizar datos de la BD//
                SqlCommand updatee = new SqlCommand("UPDATE FichaBien set IdActividad=8 where IdInventario=@idv", cnn);

                cnn.Open();


                try
                {
                    foreach (GridViewRow rowDatos in this.GridViewBusqueda.Rows)
                    {

                        if (rowDatos.RowType == DataControlRowType.DataRow)
                        {
                            //Se limpian los parametros
                            updatee.Parameters.Clear();

                            updatee.Parameters.AddWithValue("@idv", GridViewBusqueda.DataKeys[rowDatos.RowIndex].Values[0].ToString());


                            //Ejecucion de las Sentenciasa SQl//
                            updatee.ExecuteNonQuery();

                            MostrarMensaje("** Bien envíado a lista sin Reparación **", "error", "Normal", "Incorrecto");
                        }
                    }
                }
                catch (Exception)
                {
                    MostrarMensaje("** Error al envíar un Bien **", "error", "Normal", "Incorrecto");
                    cnn.Close();
                }
            }
            else
            {
                MostrarMensaje("** Ingresar Número de Inventario **", "error", "Normal", "Incorrecto");
            }
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
        protected void BtnGuardar_Click(object sender, EventArgs e)
        {
            if (HiddenFecha.Value != "")
            {
                if (LbId.Text != "")
                {
                    if (TextBoxNombreRecibeBien.Text != "")
                    {
                        if (TextBoxNoOficio.Text != "")
                        {
                            if (TextBoxNombreEntregaBien.Text != "")
                            {
                                if (TextBoxSistemaOperativo.Text != "")
                                {
                                    if (TextBoxCuentaOffice.Text != "")
                                    {
                                        if (TextBoxIP.Text != "")
                                        {
                                            if (TextBoxFallaReportada.Text != "")
                                            {
                                                if (TextBoxDiagnostico.Text != "")
                                                {
                                                    guardar.Visible = true;
                                                    salidaMant.Visible = true;

                                                    //Se llama la conexion a la Base de datos//
                                                    SqlConnection cnn = new SqlConnection(CConexion.Obtener());

                                                    //Ejecucion de Sentencia Sql para obtener los datos de la Base de Datos//
                                                    SqlCommand updatee = new SqlCommand("UPDATE Mantenimiento set IdInventario = @idinventario, NombreRecibe = @nombreRecibe, NumOficio = @numOficio, NombreEntrega = @nombreEntrega, " +
                                                       "SistemaOperativo = @sistemaOperativo, CuentaOffice = @cuentaOffice, Ip = @ip, FallaReportada = @fallaReportada, DiagnosticoReparacion = @diagnostico where IdInventario=@idinventario", cnn);

                                                    //Se asignan los parametros deseados para la consulta de la BD//
                                                    updatee.Parameters.AddWithValue("@idinventario", LbId.Text);
                                                    updatee.Parameters.AddWithValue("@nombreRecibe", TextBoxNombreRecibeBien.Text.ToUpper());
                                                    updatee.Parameters.AddWithValue("@numOficio", TextBoxNoOficio.Text.ToUpper());
                                                    updatee.Parameters.AddWithValue("@nombreEntrega", TextBoxNombreEntregaBien.Text.ToUpper());
                                                    updatee.Parameters.AddWithValue("@sistemaOperativo", TextBoxSistemaOperativo.Text.ToUpper());
                                                    updatee.Parameters.AddWithValue("@cuentaOffice", TextBoxCuentaOffice.Text.ToLower());
                                                    updatee.Parameters.AddWithValue("@ip", TextBoxIP.Text);
                                                    updatee.Parameters.AddWithValue("@fallaReportada", TextBoxFallaReportada.Text.ToUpper());
                                                    updatee.Parameters.AddWithValue("@diagnostico", TextBoxDiagnostico.Text.ToUpper());

                                                    //Ejecucion de Sentencia Sql para insertar los datos en la Base de Datos//
                                                    SqlCommand insert1 = new SqlCommand("INSERT INTO HistoricoUbicacion(IdInventario,IdENSU,IdUsuario,IdActividad,Fecha)" +
                                                        "values(@idinventario,@idENSU,@idUsuario,@idactividad,@fecha)", cnn);

                                                    insert1.Parameters.AddWithValue("@idinventario", LbId.Text);
                                                    insert1.Parameters.AddWithValue("@idENSU", LbENSU.Text);
                                                    insert1.Parameters.AddWithValue("@idUsuario", LbUsuario.Text);
                                                    insert1.Parameters.AddWithValue("@idactividad", LbActividad.Text);
                                                    insert1.Parameters.AddWithValue("@fecha", HiddenFecha.Value);

                                                    SqlCommand update = new SqlCommand("UPDATE FichaBien set IdActividad=4 where IdInventario = @idinventario", cnn);
                                                    SqlCommand update1 = new SqlCommand("UPDATE Ubicacion set IdActividad=4,Fecha=@fecha where IdInventario = @idinventario", cnn);

                                                    update.Parameters.AddWithValue("@idinventario", LbId.Text);
                                                    update1.Parameters.AddWithValue("@idinventario", LbId.Text);
                                                    update1.Parameters.AddWithValue("@fecha", HiddenFecha.Value);

                                                    try
                                                    {
                                                        cnn.Open();
                                                        //Ejecucion de las Sentenciasa SQl//
                                                        updatee.ExecuteNonQuery();
                                                        insert1.ExecuteNonQuery();
                                                        update.ExecuteNonQuery();
                                                        update1.ExecuteNonQuery();
                                                        //Eventos que suceden al guardar la información
                                                        MostrarMensaje("** Información Actualizada **", "error", "Normal", "Incorrecto");
                                                        cancelar.Visible = false;
                                                        salidaMant.Visible = false;
                                                        guardar.Visible = false;
                                                        BtnSinReparacion.Visible = true;
                                                        LimpiarRegistro();
                                                        BuscarInventario();
                                                        BuscarSerie();
                                                    }

                                                    catch (Exception)
                                                    {
                                                        MostrarMensaje("** Error al registrar una Reparación del Bien **", "error", "Normal", "Incorrecto");
                                                        cnn.Close();
                                                    }
                                                }
                                                else
                                                {
                                                    MostrarMensaje("** El Campo Diagnóstico está Vacío **", "error", "Normal", "Incorrecto");
                                                }
                                            }
                                            else
                                            {
                                                MostrarMensaje("** El Campo Falla está vacío **", "error", "Normal", "Incorrecto");
                                            }
                                        }
                                        else
                                        {
                                            MostrarMensaje("** Debe ingresar una IP valida **", "error", "Normal", "Incorrecto");
                                        }
                                    }
                                    else
                                    {
                                        MostrarMensaje("** Ingresa su Cuenta de Office **", "error", "Normal", "Incorrecto");
                                    }
                                }
                                else
                                {
                                    MostrarMensaje("** El campo Sistema Operativo está vacío **", "error", "Normal", "Incorrecto");
                                }
                            }
                            else
                            {
                                MostrarMensaje("** Ingrese Nombre quien entrega el Bien *", "error", "Normal", "Incorrecto");
                            }
                        }
                        else
                        {
                            MostrarMensaje("** El Campo Oficio está vacío *", "error", "Normal", "Incorrecto");
                        }
                    }
                    else
                    {
                        MostrarMensaje("** Ingrese Nombre quien recibe el Bien *", "error", "Normal", "Incorrecto");
                    }
                }
                else
                {
                    MostrarMensaje("** Ingrese Numero de id*", "error", "Normal", "Incorrecto");
                }
            }
            else
            {
                MostrarMensaje("** Ingrese Fecha*", "error", "Normal", "Incorrecto");
            }
        }
        protected void guardardatos_Click(object sender, EventArgs e)
        {
            if (IdRegMante.Text != "")
            {
                if (IdInventariio.Text != "")
                {
                    if (TextBoxTarjetaInf.Text != "")
                    {
                        if (TextBoxNumRefaccion.Text != "")
                        {
                            if (TextBoxDescrRefac.Text != "")
                            {
                                SqlConnection cnn = new SqlConnection(CConexion.Obtener());
                                SqlCommand insert1 = new SqlCommand("INSERT INTO PiezaReparacion(IdInventario,NumTarjetaInf,NumRefaccion,DescripcionRefaccion,IdRegMant)" +
                                                        "values(@idinventario,@numTarjetaInf,@cantidad,@descripcionRefaccion,@idRegMant)", cnn);
                                //Se asignan los parametros deseados para ingresar datos en la BD//
                                insert1.Parameters.AddWithValue("@idinventario", IdInventariio.Text);
                                insert1.Parameters.AddWithValue("@numTarjetaInf", TextBoxTarjetaInf.Text);
                                insert1.Parameters.AddWithValue("@cantidad", TextBoxNumRefaccion.Text);
                                insert1.Parameters.AddWithValue("@descripcionRefaccion", TextBoxDescrRefac.Text.ToUpper());
                                insert1.Parameters.AddWithValue("@idRegMant", IdRegMante.Text);

                                try
                                {
                                    cnn.Open();
                                    //Ejecucion Sentencio SQL
                                    insert1.ExecuteNonQuery();
                                    //Eventos que suceden al guardar la información
                                    MostrarMensaje("** Información Guardada Correctamente **", "error", "Normal", "Incorrecto");
                                    GridPiezas.Visible = true;
                                    LimpiarRegistroPiezas();
                                    piezasRepara.Visible = true;
                                    guardardatos.Enabled = false;
                                    cancelarpiezas.Visible = true;
                                }

                                catch (Exception)
                                {
                                    MostrarMensaje("** Error al registrar una pieza de Reparación **", "error", "Normal", "Incorrecto");
                                    cnn.Close();
                                }
                            }
                            else
                            {
                                MostrarMensaje("** El Campo Descripcion de Refacción esta vacío **", "error", "Normal", "Incorrecto");
                            }
                        }
                        else
                        {
                            MostrarMensaje("** Debe Ingresar un numero de Refacción**", "error", "Normal", "Incorrecto");

                        }
                    }
                    else
                    {
                        MostrarMensaje("** Debe ingresar un numero de Tarjeta**", "error", "Normal", "Incorrecto");
                    }
                }
                else
                {
                    MostrarMensaje("** Es necesario el Id Inventario **", "error", "Normal", "Incorrecto");
                }
            }
            else
            {
                    MostrarMensaje("** El necesario el Id de Registro **", "error", "Normal", "Incorrecto");
            }
        }                    
        protected void time(object sender, EventArgs e)
        {
            //Codigo para asignar fecha y hora del sistema a un Label
            HiddenFecha.Value = DateTime.Now.ToString("yyyy-MM-ddThh:mm:ss.fff");
        }
        public void BtnCancelar(object sender, EventArgs e)
        {
            LimpiarRegistro();
            detalleReparacion.Visible = true;
            guardar.Visible = false;
            cancelar.Visible = false;
            ordenServicio.Visible = false;
            salidaMant.Visible = false;
            BtnSinReparacion.Visible = true;
        }
        public void BtnCancelar_Piezas(object sender, EventArgs e)
        {
            LimpiarRegistro();
            detalleReparacion.Visible = true;
            PiezasReparacion.Visible = false;
        }
        public void LimpiarBusqueda()
        {
            TextBoxNoInventario.Text = string.Empty;
            TextBoxSerie.Text = string.Empty;
        }
        public void LimpiarRegistro()
        {
            TextBoxNombreRecibeBien.Text = string.Empty;
            TextBoxNoOficio.Text = string.Empty;
            TextBoxNombreEntregaBien.Text = string.Empty;
            TextBoxSistemaOperativo.Text = string.Empty;
            TextBoxCuentaOffice.Text = string.Empty;
            TextBoxIP.Text = string.Empty;
            TextBoxFallaReportada.Text = string.Empty;
            TextBoxDiagnostico.Text = string.Empty;
        }
        public void LimpiarRegistroPiezas()
        {
            TextBoxTarjetaInf.Text = string.Empty;
            TextBoxNumRefaccion.Text = string.Empty;
            TextBoxDescrRefac.Text = string.Empty;
        }
        public void BtnOrdenServicio(object sender, EventArgs e)
        {
            //Método para generar un reporte con Crystal
            if (LbId.Text != "")
            {
                //Asignacion de Pametros desde la clase CNegocios
                TParametrosMantenimiento Parametros = new TParametrosMantenimiento();
                Parametros.IdInventario = Convert.ToInt32(LbId.Text);
                // creacion de una sesion para envíar datos
                Page.Session["Parametros"] = Parametros;
                //Envia a la pagina donde se muestra el reporte
                string _open = "window.open('MostrarOrdenServicio.aspx', '_blank');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), _open, true);
            }
        }
        protected void EnviarBajaBien(object sender, EventArgs e)
        {
            //Conexion a otro formulario
            CambiarIdActividad();
            Response.Redirect("frmBajaBienSoporte.aspx");
        }
        protected void btn_Piezas_Command(object sender, CommandEventArgs e)
        {
            detalleReparacion.Visible = true;
            BtnSinReparacion.Visible = true;
            piezasRepara.Visible = true;
            PiezasReparacion.Visible = true;
            salidaMant.Visible = false;
            guardar.Visible = false;
            cancelar.Visible = false;
            ordenServicio.Visible = false;
            salidaMant.Visible = false;
            divConcatena.Visible = false;
            cancelarpiezas.Visible = true;
            guardardatos.Visible = true;

            //Código para asignar valores a Texbox de acuerdo al grid mostrado
            int index = Convert.ToInt32(e.CommandArgument);
            GridViewRow RowSelecionada = GridViewBusqueda.Rows[index];
            string No = Page.Server.HtmlDecode(RowSelecionada.Cells[1].Text);

            TextBoxNoInven.Text = No;

            int indexx = Convert.ToInt32(e.CommandArgument);
            IdRegMante.Text = Convert.ToString(Grid.Rows[indexx].Cells[0].Text);
            IdInventariio.Text = Convert.ToString(Grid.Rows[indexx].Cells[1].Text);
        }
        protected void btn_Mantenimiento_Command(object sender, CommandEventArgs e)
        {
            //Controles a mostror u ocultar 
            detalleReparacion.Visible = true;
            BtnSinReparacion.Visible = true;
            piezasRepara.Visible = false;
            PiezasReparacion.Visible = false;
            salidaMant.Visible = true;
            PiezasReparacion.Visible = false;
            guardar.Visible = true;
            cancelar.Visible = true;
            ordenServicio.Visible = true;
            salidaMant.Visible = true;
            divConcatena.Visible = true;

      

            int indexx = Convert.ToInt32(e.CommandArgument);

            TextBoxNombreRecibeBien.Text = Convert.ToString(Grid.Rows[indexx].Cells[2].Text);
            TextBoxNoOficio.Text = Convert.ToString(Grid.Rows[indexx].Cells[3].Text);
            TextBoxNombreEntregaBien.Text = Convert.ToString(Grid.Rows[indexx].Cells[4].Text);
            TextBoxSistemaOperativo.Text = Convert.ToString(Grid.Rows[indexx].Cells[5].Text);
            TextBoxCuentaOffice.Text = Convert.ToString(Grid.Rows[indexx].Cells[6].Text);
            TextBoxIP.Text = Convert.ToString(Grid.Rows[indexx].Cells[7].Text);
            TextBoxFallaReportada.Text = Convert.ToString(Grid.Rows[indexx].Cells[8].Text);
            TextBoxDiagnostico.Text = Convert.ToString(Grid.Rows[indexx].Cells[9].Text);
        }
        protected void btn_tarjetainf_Command(object sender, CommandEventArgs e)
        {
            //Método para generar un reporte con Crystal
            if (LbId.Text != "")
            {
                //Asignacion de Pametros desde la calse CNegocios
                TParametrosMantenimiento Parametros = new TParametrosMantenimiento();
                Parametros.IdInventarios = Convert.ToInt32(LbId.Text);
                // creacion de una sesion para envíar datos
                Page.Session["Parametros"] = Parametros;
                //Envia a la pagina donde se mostrar el reporte
                string _open = "window.open('MostrarTarjeta.aspx', '_blank');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), _open, true);
            }
        }
    }
}