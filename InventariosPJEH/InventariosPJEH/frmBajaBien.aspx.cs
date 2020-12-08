using InventariosPJEH.CNegocios;
using InventariosPJEH.CAccesoDatos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;
using Excel = Microsoft.Office.Interop.Excel;
using System.IO;
using System.Web;
using Microsoft.Office.Interop.Excel;
using DataTable = System.Data.DataTable;
using System.Drawing;
using System.Web.UI.HtmlControls;
using System.Text;
using Page = System.Web.UI.Page;
using System.Linq;

namespace InventariosPJEH
{
    public partial class BajaTemporaloDefinitivaDeUnBien : System.Web.UI.Page
    {
        public SqlConnection conexion;

        /// <summary>
        ///Conexión a BD
        /// </summary>
        public BajaTemporaloDefinitivaDeUnBien()
        {
            this.conexion = ConexionBD.getConexion();
        }

        //protected void Page_Load(object sender, EventArgs e)
        //{
        //    if (!Page.IsPostBack)
        //    {
                
        //    }
        //}


                 
         /// <summary>
         /// Método para mostrar mensajes de alerta
         /// </summary>
         /// <param name="Mensaje"></param>
         /// <param name="Tipo"></param>
         /// <param name="TipoFuncion"></param>         
        protected void MostrarMensaje(string Mensaje, string Tipo, string TipoFuncion)
        {
            string Msj = "";
            if (TipoFuncion == "Normal")
                Msj = "MostrarMensaje('" + Mensaje + "', '" + Tipo + "');";
            else
                Msj = "MostrarMensajeInterval('" + Mensaje + "', '" + Tipo + "');";

            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Ntn", Msj, true);
        }

        /// <summary>
        /// Método RowComand del Grid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void GridBienes_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            //Se obtiene el Número de Inventario de la fila seleccionada
            int index = Convert.ToInt32(e.CommandArgument);
            GridViewRow RowSelecionada = GridBienes.Rows[index];
            string Bien = Page.Server.HtmlDecode(RowSelecionada.Cells[0].Text);
            Lblbien.Text = Bien;
        }

        /// <summary>
        /// Método para buscar los bienes haciendo una consulta dependiendo
        /// los filtros ingresados. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void BtnBuscarBien_Click1(object sender, EventArgs e)
        {
            if(TxtNoInventario.Text != "")
            {
                DataTable BuscarBien = new DataTable();
                BuscarBien = BdBajaBien.BuscarGrid(TxtNoInventario);
                List<CBajaBien> BienList = new List<CBajaBien>();
                for (int i = 0; i < BuscarBien.Rows.Count; i++)
                {
                    CBajaBien cBaja = new CBajaBien();
                    cBaja.NumInventario = Convert.ToInt64(BuscarBien.Rows[i]["NumInventario"]);
                    cBaja.DescripcionBien = Convert.ToString(BuscarBien.Rows[i]["DescripcionBien"]);
                    cBaja.Modelo = Convert.ToString(BuscarBien.Rows[i]["Modelo"]);
                    cBaja.Serie = Convert.ToString(BuscarBien.Rows[i]["Serie"]);
                    cBaja.Descripcion = Convert.ToString(BuscarBien.Rows[i]["Descripcion"]);
                    cBaja.FechaAdquisicion = Convert.ToString(BuscarBien.Rows[i]["FechaAdquisicion"]);
                    BienList.Add(cBaja);
                }
                string NumInven = BienList.FirstOrDefault().NumInventario.ToString();
                string DescripcionBien = BienList.FirstOrDefault().DescripcionBien.ToString();
                string Modelo = BienList.FirstOrDefault().Modelo.ToString();
                string Serie = BienList.FirstOrDefault().Serie.ToString();
                string Descripcion = BienList.FirstOrDefault().Descripcion.ToString();
                string FechaAdquisicion = BienList.FirstOrDefault().FechaAdquisicion.ToString();

                //Se crea una nueva tabla para asignarla al GridView y se agreguen las filas al precionar en el boton agregar
                DataTable dt;
                if (Session["Datos"] != null)
                {

                    dt = Session["Datos"] as DataTable;
                }
                else
                {
                    dt = new DataTable();
                    dt.Columns.Add("NumInventario");
                    dt.Columns.Add("DescripcionBien");
                    dt.Columns.Add("Modelo");
                    dt.Columns.Add("Serie");
                    dt.Columns.Add("Descripcion");
                    dt.Columns.Add("FechaAdquisicion");
                }

                DataRow fila = dt.NewRow();
                fila["NumInventario"] = NumInven;
                fila["DescripcionBien"] = DescripcionBien;
                fila["Modelo"] = Modelo;
                fila["Serie"] = Serie;
                fila["Descripcion"] = Descripcion;
                fila["FechaAdquisicion"] = FechaAdquisicion;

                dt.Rows.Add(fila);

                divGrid.Visible = true;
                Botones.Visible = true;
                GridBienes.DataSource = dt;
                GridBienes.DataBind();
                Session["Datos"] = dt;
                TxtNoInventario.Text = "";
            }
            else
            {
                MostrarMensaje("** Número de inventario requerido **", "info", "Normal");
            }
        }

        /// <summary>
        /// Método para limpiar los filtros de busqueda. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void BtnLimpiarBusqueda_Click(object sender, EventArgs e)
        {
            //Se crea una tabla en blanco para asignarla al GridView y se eliminen los datos que ya tenia cargados 
            DataTable dt;
            if (Session["Datos"] != null)
            {
                dt = new DataTable();
                dt.Columns.Add("NumInventario");
                dt.Columns.Add("DescripcionBien");
                dt.Columns.Add("Modelo");
                dt.Columns.Add("Serie");
                dt.Columns.Add("Descripcion");
                dt.Columns.Add("FechaAdquisicion");
            }
            else
            {
                dt = Session["Datos"] as DataTable;
            }

            divGrid.Visible = false;
            Botones.Visible = false;
            TxtNoInventario.Text = "";
            GridBienes.DataSource = dt;
            GridBienes.DataBind();
            Session["Datos"] = dt;
        }

        /// <summary>
        /// Método para mandar  llamar los metodos ActualizarEstatusFichaBienTemporal,
        /// ActualizarEstatusUbicacionTemporal e InsertarEstatusHistoricoUbicacionTemporal
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnBajaTemporal_Click(object sender, ImageClickEventArgs e)
        {
            ActualizarEstatusFichaBienTemporal(sender, e);
            ActualizarEstatusUbicacionTemporal(sender, e);
            InsertarEstatusHistoricoUbicacionTemporal(sender, e);
        }

        /// <summary>
        /// Método para actualizar el estatus del bien en la tabla FichaBien de forma temporal
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ActualizarEstatusFichaBienTemporal(object sender, EventArgs e)
        {
            //Método para actualizar el estatus del bien en la tabla FicgaBien 

            DataTable FichaBienInfo = new DataTable();
            FichaBienInfo = BdBajaBien.FichaBienInfo(Lblbien, sender, GridBienes);

            List<CBajaBien> FichaList = new List<CBajaBien>();
            for (int i = 0; i < FichaBienInfo.Rows.Count; i++)
            {
                CBajaBien cFactura = new CBajaBien();
                cFactura.NumInventario = Convert.ToInt64(FichaBienInfo.Rows[i]["NumInventario"]);
                cFactura.DescripcionBien = Convert.ToString(FichaBienInfo.Rows[i]["DescripcionBien"]);
                cFactura.IdCONAC = Convert.ToInt32(FichaBienInfo.Rows[i]["IdCONAC"]);
                cFactura.idSubClase = Convert.ToInt32(FichaBienInfo.Rows[i]["idSubClase"]);
                cFactura.Modelo = Convert.ToString(FichaBienInfo.Rows[i]["Modelo"]);
                cFactura.Serie = Convert.ToString(FichaBienInfo.Rows[i]["Serie"]);
                cFactura.IdMarca = Convert.ToInt32(FichaBienInfo.Rows[i]["IdMarca"]);
                cFactura.Detalle = Convert.ToString(FichaBienInfo.Rows[i]["Detalle"]);
                cFactura.TipoPartida = Convert.ToInt32(FichaBienInfo.Rows[i]["TipoPartida"]);
                cFactura.IdENSU = Convert.ToInt32(FichaBienInfo.Rows[i]["IdENSU"]);
                cFactura.FechaCaptura = Convert.ToDateTime(FichaBienInfo.Rows[i]["FechaCaptura"]);
                cFactura.PrecioUnitario = Convert.ToDouble(FichaBienInfo.Rows[i]["PrecioUnitario"]);
                cFactura.SubTotal = Convert.ToDouble(FichaBienInfo.Rows[i]["SubTotal"]);
                cFactura.Descuento = Convert.ToDouble(FichaBienInfo.Rows[i]["Descuento"]);
                cFactura.CostoTotal = Convert.ToDouble(FichaBienInfo.Rows[i]["CostoTotal"]);
                cFactura.IdCompra = Convert.ToInt32(FichaBienInfo.Rows[i]["IdCompra"]);

                FichaList.Add(cFactura);
            }
            string NumInventario = FichaList.FirstOrDefault().NumInventario.ToString();
            string DescripcionBien = FichaList.FirstOrDefault().DescripcionBien.ToString();
            string IdCONAC = FichaList.FirstOrDefault().IdCONAC.ToString();
            string idSubClase = FichaList.FirstOrDefault().idSubClase.ToString();
            string Modelo = FichaList.FirstOrDefault().Modelo.ToString();
            string IdMarca = FichaList.FirstOrDefault().IdMarca.ToString();
            string Detalle = FichaList.FirstOrDefault().Detalle.ToString();
            string TipoPartida = FichaList.FirstOrDefault().TipoPartida.ToString();
            string IdENSU = FichaList.FirstOrDefault().IdENSU.ToString();
            string FechaCaptura = FichaList.FirstOrDefault().FechaCaptura.ToString();
            string PrecioUnitario = FichaList.FirstOrDefault().PrecioUnitario.ToString();
            string SubTotal = FichaList.FirstOrDefault().SubTotal.ToString();
            string Descuento = FichaList.FirstOrDefault().Descuento.ToString();
            string CostoTotal = FichaList.FirstOrDefault().CostoTotal.ToString();
            string IdCompra = FichaList.FirstOrDefault().IdCompra.ToString();
            string serie = FichaList.FirstOrDefault().Serie.ToString();
            int IdAct = 6;

            SqlConnection Conn = new SqlConnection(CConexion.Obtener());
            bool success = false;
            SqlTransaction lTransaccion = null;
            int Valor_Retornado = 0;
            try
            {
                Conn.Open();
                lTransaccion = Conn.BeginTransaction(System.Data.IsolationLevel.Serializable);
                SqlCommand cmd = new SqlCommand("SP_Actualizar_EstatusTemporalFichaBien", Conn, lTransaccion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                cmd.Parameters.Add(new SqlParameter("@NumInventario", Convert.ToInt64(NumInventario)));
                cmd.Parameters.Add(new SqlParameter("@DescripcionBien", DescripcionBien));
                cmd.Parameters.Add(new SqlParameter("@IdCONAC", IdCONAC));
                cmd.Parameters.Add(new SqlParameter("@IdSubClase", idSubClase));
                cmd.Parameters.Add(new SqlParameter("@Modelo", Modelo));
                cmd.Parameters.Add(new SqlParameter("@Serie", serie));
                cmd.Parameters.Add(new SqlParameter("@IdMarca", IdMarca));
                cmd.Parameters.Add(new SqlParameter("@Detalle", Detalle));
                cmd.Parameters.Add(new SqlParameter("@TipoPartida", TipoPartida));
                cmd.Parameters.Add(new SqlParameter("@IdENSU", IdENSU));
                cmd.Parameters.Add(new SqlParameter("@FechaCaptura", Convert.ToDateTime(FechaCaptura)));
                cmd.Parameters.Add(new SqlParameter("@PrecioUnitario", Convert.ToDouble(PrecioUnitario)));
                cmd.Parameters.Add(new SqlParameter("@SubTotal", Convert.ToDouble(SubTotal)));
                cmd.Parameters.Add(new SqlParameter("@Descuento", Convert.ToDouble(Descuento)));
                cmd.Parameters.Add(new SqlParameter("@CostoTotal", Convert.ToDouble(CostoTotal)));
                cmd.Parameters.Add(new SqlParameter("@IdCompra", IdCompra));
                cmd.Parameters.Add(new SqlParameter("@IdActividad", IdAct));
                SqlParameter ValorRetorno = new SqlParameter("@Comprobacion", SqlDbType.Int);
                ValorRetorno.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(ValorRetorno);
                cmd.ExecuteNonQuery();
                Valor_Retornado = Convert.ToInt32(ValorRetorno.Value);
                if (Valor_Retornado == 1)
                    success = true;
            }
            catch (Exception ex)
            {
                string script = ex.ToString();

                MostrarMensaje("** Error al realizar la Baja Temporal del Bien **", "info", "Normal");
            }
            finally
            {
                if (success)
                {
                    lTransaccion.Commit();
                    Conn.Close();
                    MostrarMensaje("** Estatus actualizado correctamente a Baja Temporal **", "info", "Normal");
                }
                else
                {
                    lTransaccion.Rollback();
                    Conn.Close();
                }
            }
        }

        /// <summary>
        /// Método para actualizar el estatus del Bien en la tabla Ubicacion de forma temporal
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ActualizarEstatusUbicacionTemporal(object sender, EventArgs e)
        {
            //Método para actualizar el estatus del bien en la tabla Ubicación 

            DataTable UbicacionInfo = new DataTable();
            UbicacionInfo = BdBajaBien.Ubicacion(Lblbien, sender, GridBienes);

            List<CBajaBien> UbicacionList = new List<CBajaBien>();
            for (int i = 0; i < UbicacionInfo.Rows.Count; i++)
            {
                CBajaBien cFactura = new CBajaBien();
                cFactura.IdInventario = Convert.ToInt32(UbicacionInfo.Rows[i]["IdInventario"]);
                cFactura.IdENSU = Convert.ToInt32(UbicacionInfo.Rows[i]["IdENSU"]);
                cFactura.IdUsuario = Convert.ToInt32(UbicacionInfo.Rows[i]["IdUsuario"]);
                cFactura.Fecha = Convert.ToDateTime(UbicacionInfo.Rows[i]["Fecha"]);
                
                UbicacionList.Add(cFactura);
            }
            string IdInventario = UbicacionList.FirstOrDefault().IdInventario.ToString();
            string IdENSU = UbicacionList.FirstOrDefault().IdENSU.ToString();
            string IdUsuario = UbicacionList.FirstOrDefault().IdUsuario.ToString();
            string Fecha = UbicacionList.FirstOrDefault().Fecha.ToString();
            int IdAct = 6;

            SqlConnection Conn = new SqlConnection(CConexion.Obtener());
            bool success = false;
            SqlTransaction lTransaccion = null;
            int Valor_Retornado = 0;
            try
            {
                Conn.Open();
                lTransaccion = Conn.BeginTransaction(System.Data.IsolationLevel.Serializable);
                SqlCommand cmd = new SqlCommand("SP_Actualizar_EstatusTemporalUbicacion", Conn, lTransaccion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                cmd.Parameters.Add(new SqlParameter("@IdInventario", IdInventario));
                cmd.Parameters.Add(new SqlParameter("@IdENSU", IdENSU));
                cmd.Parameters.Add(new SqlParameter("@IdUsuario", IdUsuario));
                cmd.Parameters.Add(new SqlParameter("@Fecha", Convert.ToDateTime(Fecha)));
                cmd.Parameters.Add(new SqlParameter("@IdActividad", IdAct));
                SqlParameter ValorRetorno = new SqlParameter("@Comprobacion", SqlDbType.Int);
                ValorRetorno.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(ValorRetorno);
                cmd.ExecuteNonQuery();
                Valor_Retornado = Convert.ToInt32(ValorRetorno.Value);
                if (Valor_Retornado == 1)
                    success = true;
            }
            catch (Exception ex)
            {
                string script = ex.ToString();

                MostrarMensaje("** Error al Actualizar la Ubicación del Bien **", "info", "Normal");
            }
            finally
            {
                if (success)
                {
                    lTransaccion.Commit();
                    Conn.Close();
                    MostrarMensaje("** Estatus actualizado correctamente a Baja Definitiva **", "info", "Normal");
                }
                else
                {
                    lTransaccion.Rollback();
                    Conn.Close();
                }
            }
        }

        /// <summary>
        /// Método para insertar el Bien en la tabla Historico Ubicación con estatus baja temporal 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void InsertarEstatusHistoricoUbicacionTemporal(object sender, EventArgs e)
        {
            //Método para insertar el Bien en la tabla HistoricoUbicacion 

            DataTable UbicacionInfo = new DataTable();
            UbicacionInfo = BdBajaBien.HistoricoUbicacion(Lblbien, sender, GridBienes);

            List<CBajaBien> UbicacionList = new List<CBajaBien>();
            for (int i = 0; i < UbicacionInfo.Rows.Count; i++)
            {
                CBajaBien cFactura = new CBajaBien();
                cFactura.IdInventario = Convert.ToInt32(UbicacionInfo.Rows[i]["IdInventario"]);
                cFactura.IdENSU = Convert.ToInt32(UbicacionInfo.Rows[i]["IdENSU"]);
                cFactura.IdUsuario = Convert.ToInt32(UbicacionInfo.Rows[i]["IdUsuario"]);
                cFactura.Fecha = Convert.ToDateTime(UbicacionInfo.Rows[i]["Fecha"]);

                UbicacionList.Add(cFactura);
            }
            string IdInventario = UbicacionList.FirstOrDefault().IdInventario.ToString();
            string IdENSU = UbicacionList.FirstOrDefault().IdENSU.ToString();
            string IdUsuario = UbicacionList.FirstOrDefault().IdUsuario.ToString();
            int IdAct = 6;


            
            DateTime thisDay = DateTime.Now;
            DateTime Fecha = Convert.ToDateTime(thisDay.ToString());


            SqlConnection Conn = new SqlConnection(CConexion.Obtener());
            bool success = false;
            SqlTransaction lTransaccion = null;
            int Valor_Retornado = 0;
            try
            {
                Conn.Open();
                lTransaccion = Conn.BeginTransaction(System.Data.IsolationLevel.Serializable);
                SqlCommand cmd = new SqlCommand("SP_Insertar_HistoricoUbicacion", Conn, lTransaccion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                cmd.Parameters.Add(new SqlParameter("@IdInventario", IdInventario));
                cmd.Parameters.Add(new SqlParameter("@IdENSU", IdENSU));
                cmd.Parameters.Add(new SqlParameter("@IdUsuario", IdUsuario));
                cmd.Parameters.Add(new SqlParameter("@Fecha", Convert.ToDateTime(Fecha)));
                cmd.Parameters.Add(new SqlParameter("@IdActividad", IdAct));
                SqlParameter ValorRetorno = new SqlParameter("@Comprobacion", SqlDbType.Int);
                ValorRetorno.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(ValorRetorno);
                cmd.ExecuteNonQuery();
                Valor_Retornado = Convert.ToInt32(ValorRetorno.Value);
                if (Valor_Retornado == 1)
                    success = true;
            }
            catch (Exception ex)
            {
                string script = ex.ToString();

                MostrarMensaje("** Error al insertar el histórico de ubicación del Bien **", "info", "Normal");
            }
            finally
            {
                if (success)
                {
                    lTransaccion.Commit();
                    Conn.Close();
                    MostrarMensaje("** Estatus actualizado correctamente **", "info", "Normal");
                }
                else
                {
                    lTransaccion.Rollback();
                    Conn.Close();
                }
            }
        }

        /// <summary>
        /// Método para actualizar el estatus del Bien en la tabla FichaBien de forma definitiva
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ActualizarEstatusFichaBienBajaDefinitiva(object sender, EventArgs e)
        {
            DataTable FichaBienInfo = new DataTable();
            FichaBienInfo = BdBajaBien.FichaBienInfo(Lblbien, sender, GridBienes);

            List<CBajaBien> FichaList = new List<CBajaBien>();
            for (int i = 0; i < FichaBienInfo.Rows.Count; i++)
            {
                CBajaBien cFactura = new CBajaBien();
                cFactura.NumInventario = Convert.ToInt64(FichaBienInfo.Rows[i]["NumInventario"]);
                cFactura.DescripcionBien = Convert.ToString(FichaBienInfo.Rows[i]["DescripcionBien"]);
                cFactura.IdCONAC = Convert.ToInt32(FichaBienInfo.Rows[i]["IdCONAC"]);
                cFactura.idSubClase = Convert.ToInt32(FichaBienInfo.Rows[i]["idSubClase"]);
                cFactura.Modelo = Convert.ToString(FichaBienInfo.Rows[i]["Modelo"]);
                cFactura.Serie = Convert.ToString(FichaBienInfo.Rows[i]["Serie"]);
                cFactura.IdMarca = Convert.ToInt32(FichaBienInfo.Rows[i]["IdMarca"]);
                cFactura.Detalle = Convert.ToString(FichaBienInfo.Rows[i]["Detalle"]);
                cFactura.TipoPartida = Convert.ToInt32(FichaBienInfo.Rows[i]["TipoPartida"]);
                cFactura.IdENSU = Convert.ToInt32(FichaBienInfo.Rows[i]["IdENSU"]);
                cFactura.FechaCaptura = Convert.ToDateTime(FichaBienInfo.Rows[i]["FechaCaptura"]);
                cFactura.PrecioUnitario = Convert.ToDouble(FichaBienInfo.Rows[i]["PrecioUnitario"]);
                cFactura.SubTotal = Convert.ToDouble(FichaBienInfo.Rows[i]["SubTotal"]);
                cFactura.Descuento = Convert.ToDouble(FichaBienInfo.Rows[i]["Descuento"]);
                cFactura.CostoTotal = Convert.ToDouble(FichaBienInfo.Rows[i]["CostoTotal"]);
                cFactura.IdCompra = Convert.ToInt32(FichaBienInfo.Rows[i]["IdCompra"]);

                FichaList.Add(cFactura);
            }
            string NumInventario = FichaList.FirstOrDefault().NumInventario.ToString();
            string DescripcionBien = FichaList.FirstOrDefault().DescripcionBien.ToString();
            string IdCONAC = FichaList.FirstOrDefault().IdCONAC.ToString();
            string idSubClase = FichaList.FirstOrDefault().idSubClase.ToString();
            string Modelo = FichaList.FirstOrDefault().Modelo.ToString();
            string IdMarca = FichaList.FirstOrDefault().IdMarca.ToString();
            string Detalle = FichaList.FirstOrDefault().Detalle.ToString();
            string TipoPartida = FichaList.FirstOrDefault().TipoPartida.ToString();
            string IdENSU = FichaList.FirstOrDefault().IdENSU.ToString();
            string FechaCaptura = FichaList.FirstOrDefault().FechaCaptura.ToString();
            string PrecioUnitario = FichaList.FirstOrDefault().PrecioUnitario.ToString();
            string SubTotal = FichaList.FirstOrDefault().SubTotal.ToString();
            string Descuento = FichaList.FirstOrDefault().Descuento.ToString();
            string CostoTotal = FichaList.FirstOrDefault().CostoTotal.ToString();
            string IdCompra = FichaList.FirstOrDefault().IdCompra.ToString();
            string serie = FichaList.FirstOrDefault().Serie.ToString();
            int IdAct = 7;

            SqlConnection Conn = new SqlConnection(CConexion.Obtener());
            bool success = false;
            SqlTransaction lTransaccion = null;
            int Valor_Retornado = 0;
            try
            {
                Conn.Open();
                lTransaccion = Conn.BeginTransaction(System.Data.IsolationLevel.Serializable);
                SqlCommand cmd = new SqlCommand("SP_Actualizar_EstatusBajaDefinitivaFichaBien", Conn, lTransaccion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                cmd.Parameters.Add(new SqlParameter("@NumInventario", Convert.ToInt64(NumInventario)));
                cmd.Parameters.Add(new SqlParameter("@DescripcionBien", DescripcionBien));
                cmd.Parameters.Add(new SqlParameter("@IdCONAC", IdCONAC));
                cmd.Parameters.Add(new SqlParameter("@IdSubClase", idSubClase));
                cmd.Parameters.Add(new SqlParameter("@Modelo", Modelo));
                cmd.Parameters.Add(new SqlParameter("@Serie", serie));
                cmd.Parameters.Add(new SqlParameter("@IdMarca", IdMarca));
                cmd.Parameters.Add(new SqlParameter("@Detalle", Detalle));
                cmd.Parameters.Add(new SqlParameter("@TipoPartida", TipoPartida));
                cmd.Parameters.Add(new SqlParameter("@IdENSU", IdENSU));
                cmd.Parameters.Add(new SqlParameter("@FechaCaptura", Convert.ToDateTime(FechaCaptura)));
                cmd.Parameters.Add(new SqlParameter("@PrecioUnitario", Convert.ToDouble(PrecioUnitario)));
                cmd.Parameters.Add(new SqlParameter("@SubTotal", Convert.ToDouble(SubTotal)));
                cmd.Parameters.Add(new SqlParameter("@Descuento", Convert.ToDouble(Descuento)));
                cmd.Parameters.Add(new SqlParameter("@CostoTotal", Convert.ToDouble(CostoTotal)));
                cmd.Parameters.Add(new SqlParameter("@IdCompra", IdCompra));
                cmd.Parameters.Add(new SqlParameter("@IdActividad", IdAct));
                SqlParameter ValorRetorno = new SqlParameter("@Comprobacion", SqlDbType.Int);
                ValorRetorno.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(ValorRetorno);
                cmd.ExecuteNonQuery();
                Valor_Retornado = Convert.ToInt32(ValorRetorno.Value);
                if (Valor_Retornado == 1)
                    success = true;
            }
            catch (Exception ex)
            {
                string script = ex.ToString();

                MostrarMensaje("** Error al realizar la Baja Definitiva **", "info", "Normal");
            }
            finally
            {
                if (success)
                {
                    lTransaccion.Commit();
                    Conn.Close();
                    MostrarMensaje("** Estatus actualizado correctamente **", "info", "Normal");
                }
                else
                {
                    lTransaccion.Rollback();
                    Conn.Close();
                }
            }
        }

        /// <summary>
        /// Método para actualizar el estatus del Bien en la tabla Ubicacion de forma definitiva
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ActualizarEstatusUbicacionBajaDefinitiva(object sender, EventArgs e)
        {
            DataTable UbicacionInfo = new DataTable();
            UbicacionInfo = BdBajaBien.Ubicacion(Lblbien, sender, GridBienes);

            List<CBajaBien> UbicacionList = new List<CBajaBien>();
            for (int i = 0; i < UbicacionInfo.Rows.Count; i++)
            {
                CBajaBien cFactura = new CBajaBien();
                cFactura.IdInventario = Convert.ToInt32(UbicacionInfo.Rows[i]["IdInventario"]);
                cFactura.IdENSU = Convert.ToInt32(UbicacionInfo.Rows[i]["IdENSU"]);
                cFactura.IdUsuario = Convert.ToInt32(UbicacionInfo.Rows[i]["IdUsuario"]);
                cFactura.Fecha = Convert.ToDateTime(UbicacionInfo.Rows[i]["Fecha"]);

                UbicacionList.Add(cFactura);
            }
            string IdInventario = UbicacionList.FirstOrDefault().IdInventario.ToString();
            string IdENSU = UbicacionList.FirstOrDefault().IdENSU.ToString();
            string IdUsuario = UbicacionList.FirstOrDefault().IdUsuario.ToString();
            string Fecha = UbicacionList.FirstOrDefault().Fecha.ToString();
            int IdAct = 7;

            SqlConnection Conn = new SqlConnection(CConexion.Obtener());
            bool success = false;
            SqlTransaction lTransaccion = null;
            int Valor_Retornado = 0;
            try
            {
                Conn.Open();
                lTransaccion = Conn.BeginTransaction(System.Data.IsolationLevel.Serializable);
                SqlCommand cmd = new SqlCommand("SP_Actualizar_EstatusBajaDefinitivaUbicacion", Conn, lTransaccion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                cmd.Parameters.Add(new SqlParameter("@IdInventario", IdInventario));
                cmd.Parameters.Add(new SqlParameter("@IdENSU", IdENSU));
                cmd.Parameters.Add(new SqlParameter("@IdUsuario", IdUsuario));
                cmd.Parameters.Add(new SqlParameter("@Fecha", Convert.ToDateTime(Fecha)));
                cmd.Parameters.Add(new SqlParameter("@IdActividad", IdAct));
                SqlParameter ValorRetorno = new SqlParameter("@Comprobacion", SqlDbType.Int);
                ValorRetorno.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(ValorRetorno);
                cmd.ExecuteNonQuery();
                Valor_Retornado = Convert.ToInt32(ValorRetorno.Value);
                if (Valor_Retornado == 1)
                    success = true;
            }
            catch (Exception ex)
            {
                string script = ex.ToString();

                MostrarMensaje("** Error al insertar Serie **", "info", "Normal");
            }
            finally
            {
                if (success)
                {
                    lTransaccion.Commit();
                    Conn.Close();
                    MostrarMensaje("** Estatus actualizado correctamente **", "info", "Normal");
                }
                else
                {
                    lTransaccion.Rollback();
                    Conn.Close();
                }
            }
        }

        /// <summary>
        /// Método para insertar el Bien en la tabla Historico Ubicación con estatus baja definitiva 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void InsertarEstatusHistoricoUbicacionBajaDefinitiva(object sender, EventArgs e)
        {
            DataTable UbicacionInfo = new DataTable();
            UbicacionInfo = BdBajaBien.HistoricoUbicacion(Lblbien, sender, GridBienes);

            List<CBajaBien> UbicacionList = new List<CBajaBien>();
            for (int i = 0; i < UbicacionInfo.Rows.Count; i++)
            {
                CBajaBien cFactura = new CBajaBien();
                cFactura.IdInventario = Convert.ToInt32(UbicacionInfo.Rows[i]["IdInventario"]);
                cFactura.IdENSU = Convert.ToInt32(UbicacionInfo.Rows[i]["IdENSU"]);
                cFactura.IdUsuario = Convert.ToInt32(UbicacionInfo.Rows[i]["IdUsuario"]);
                cFactura.Fecha = Convert.ToDateTime(UbicacionInfo.Rows[i]["Fecha"]);

                UbicacionList.Add(cFactura);
            }
            string IdInventario = UbicacionList.FirstOrDefault().IdInventario.ToString();
            string IdENSU = UbicacionList.FirstOrDefault().IdENSU.ToString();
            string IdUsuario = UbicacionList.FirstOrDefault().IdUsuario.ToString();
            int IdAct = 7;

            DateTime thisDay = DateTime.Now;
            DateTime Fecha = Convert.ToDateTime(thisDay.ToString());

            SqlConnection Conn = new SqlConnection(CConexion.Obtener());
            bool success = false;
            SqlTransaction lTransaccion = null;
            int Valor_Retornado = 0;
            try
            {
                Conn.Open();
                lTransaccion = Conn.BeginTransaction(System.Data.IsolationLevel.Serializable);
                SqlCommand cmd = new SqlCommand("SP_Insertar_HistoricoUbicacion", Conn, lTransaccion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                cmd.Parameters.Add(new SqlParameter("@IdInventario", IdInventario));
                cmd.Parameters.Add(new SqlParameter("@IdENSU", IdENSU));
                cmd.Parameters.Add(new SqlParameter("@IdUsuario", IdUsuario));
                cmd.Parameters.Add(new SqlParameter("@Fecha", Convert.ToDateTime(Fecha)));
                cmd.Parameters.Add(new SqlParameter("@IdActividad", IdAct));
                SqlParameter ValorRetorno = new SqlParameter("@Comprobacion", SqlDbType.Int);
                ValorRetorno.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(ValorRetorno);
                cmd.ExecuteNonQuery();
                Valor_Retornado = Convert.ToInt32(ValorRetorno.Value);
                if (Valor_Retornado == 1)
                    success = true;
            }
            catch (Exception ex)
            {
                string script = ex.ToString();

                MostrarMensaje("** Error al insertar Serie **", "info", "Normal");
            }
            finally
            {
                if (success)
                {
                    lTransaccion.Commit();
                    Conn.Close();
                    MostrarMensaje("** Estatus actualizado correctamente **", "info", "Normal");
                }
                else
                {
                    lTransaccion.Rollback();
                    Conn.Close();
                }
            }
        }

        /// <summary>
        /// Método para llamar los metodos de actualizar el Bien de forma definitiva e insertar
        /// en historico ubicacion de forma definitiva. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnBajaDefinitiva_Click(object sender, ImageClickEventArgs e)
        {
            ActualizarEstatusFichaBienBajaDefinitiva(sender, e);
            ActualizarEstatusUbicacionBajaDefinitiva(sender, e);
            InsertarEstatusHistoricoUbicacionBajaDefinitiva(sender, e);
        }
    }
}

