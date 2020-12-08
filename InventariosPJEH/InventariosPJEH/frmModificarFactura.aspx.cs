using System.Globalization;
using InventariosPJEH.CNegocios;
using InventariosPJEH.CAccesoDatos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Linq;

namespace InventariosPJEH
{
    public partial class frmModificarFactura : System.Web.UI.Page
    {
        public SqlConnection conexion;

        public frmModificarFactura()
        {
            this.conexion = ConexionBD.getConexion();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            BdModificarFactura cRegistroDeUnBien = new BdModificarFactura();
            if (!Page.IsPostBack)
            {
                IniciarLlenadoDropDownTipoAdquisicion();
                IniciarLlenadoDropDownTipoDocumento();
                IniciarLlenadoDropDownListProveedor();
                IniciarLlenadoDropDownListPropiedad();
            }
        }

        /// <summary>
        /// Método para mostrar una alert al usuario. 
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
        /// Método para llenar el Drop TipoAdquisición mediante una consulta a la BD.
        /// </summary>
        protected void IniciarLlenadoDropDownTipoAdquisicion()
        {
            List<CModificarFactura> listaAdqui = new List<CModificarFactura>();
            listaAdqui = BdModificarFactura.ObtenerTipoAdquisicion();
            DropDownListTipoAdquisicion.DataSource = listaAdqui;
            DropDownListTipoAdquisicion.DataTextField = "tipoAdqui";
            DropDownListTipoAdquisicion.DataValueField = "IdTipoAdqui";
            DropDownListTipoAdquisicion.DataBind();
            DropDownListTipoAdquisicion.Items.Insert(0, new ListItem("Seleccionar", "0"));
        }

        /// <summary>
        /// Método para llenar el Drop TipoDocumento mediante una consulta a la BD.
        /// </summary>
        private void IniciarLlenadoDropDownTipoDocumento()
        {
            List<CModificarFactura> lista = new List<CModificarFactura>();
            lista = BdModificarFactura.ObtenerTipoDocumento();
            DropDownListTipoDocumento.DataSource = lista;
            DropDownListTipoDocumento.DataTextField = "Documento";
            DropDownListTipoDocumento.DataValueField = "IdTipoDoc";
            DropDownListTipoDocumento.DataBind();
            DropDownListTipoDocumento.Items.Insert(0, new ListItem("Seleccionar", "0"));
        }

        /// <summary>
        /// Método para llenar el Drop Proveedores mediante una consulta a la BD.
        /// </summary>
        private void IniciarLlenadoDropDownListProveedor()
        {
            List<CModificarFactura> listaProveedor = new List<CModificarFactura>();
            listaProveedor = BdModificarFactura.ObtenerProveedor();
            DropDownListProveedor.DataSource = listaProveedor;
            DropDownListProveedor.DataTextField = "Proveedor";
            DropDownListProveedor.DataValueField = "IdProveedor";
            DropDownListProveedor.DataBind();
            DropDownListProveedor.Items.Insert(0, new ListItem("Seleccionar", "0"));
        }

        /// <summary>
        /// Método para llenar el Drop Propiedades mediante una consulta a la BD.
        /// </summary>
        private void IniciarLlenadoDropDownListPropiedad()
        {
            List<CModificarFactura> listaPropiedad = new List<CModificarFactura>();
            listaPropiedad = BdModificarFactura.ObtenerPropiedad();
            DropDownListPropiedad.DataSource = listaPropiedad;
            DropDownListPropiedad.DataTextField = "Propiedad";
            DropDownListPropiedad.DataValueField = "IdPropiedad";
            DropDownListPropiedad.DataBind();
            DropDownListPropiedad.Items.Insert(0, new ListItem("Seleccionar", "0"));
        }

        /// <summary>
        /// Método para validar si el número de documento ingresado existe. 
        /// </summary>
        protected void TextBoxNoDocumento_TextChanged(object sender, EventArgs e)
        {
            if (!existe1(this.TextBoxNoDocumento.Text))
            {
                IniciarLlenadoDropDownListProveedor();
                IniciarLlenadoDropDownTipoDocumento();
                IniciarLlenadoDropDownTipoAdquisicion();
                IniciarLlenadoDropDownListPropiedad();
                TxtDetalleAdquisicion.Text = "";
                TxtFechaAdquisicion.Text = "";
            }
            else
            {
                /////// Llenar Campos ////////
                DataTable BuscarFactura = new DataTable();
                BuscarFactura = BdModificarFactura.BuscarFactura(TextBoxNoDocumento);
                List<CModificarFactura> FacturaList = new List<CModificarFactura>();
                for (int i = 0; i < BuscarFactura.Rows.Count; i++)
                {
                    CModificarFactura cFactura = new CModificarFactura();
                    cFactura.NumDocumento = Convert.ToString(BuscarFactura.Rows[i]["NumDocumento"]);
                    cFactura.FechaAdquisicion = Convert.ToString(BuscarFactura.Rows[i]["FechaAdquisicion"]);
                    cFactura.DetAdqui = Convert.ToString(BuscarFactura.Rows[i]["DetAdqui"]);
                    FacturaList.Add(cFactura);
                }

                DataTable BuscarProveedor = new DataTable();
                BuscarProveedor = BdModificarFactura.ProveedorFactura(TextBoxNoDocumento);
                List<CModificarFactura> proveedorList = new List<CModificarFactura>();
                for (int i = 0; i < BuscarProveedor.Rows.Count; i++)
                {
                    CModificarFactura cFactura = new CModificarFactura();
                    cFactura.Proveedor = Convert.ToString(BuscarProveedor.Rows[i]["Proveedor"]);
                    proveedorList.Add(cFactura);
                }

                DataTable BuscarTipoAdqui = new DataTable();
                BuscarTipoAdqui = BdModificarFactura.AdquiFactura(TextBoxNoDocumento);
                List<CModificarFactura> TipoAdquiList = new List<CModificarFactura>();
                for (int i = 0; i < BuscarTipoAdqui.Rows.Count; i++)
                {
                    CModificarFactura cFactura = new CModificarFactura();
                    cFactura.TipoAdqui = Convert.ToString(BuscarTipoAdqui.Rows[i]["TipoAdqui"]);
                    TipoAdquiList.Add(cFactura);
                }

                DataTable BuscarTipoDoc = new DataTable();
                BuscarTipoDoc = BdModificarFactura.DocumentoFactura(TextBoxNoDocumento);
                List<CModificarFactura> DocList = new List<CModificarFactura>();
                for (int i = 0; i < BuscarTipoDoc.Rows.Count; i++)
                {
                    CModificarFactura cFactura = new CModificarFactura();
                    cFactura.Documento = Convert.ToString(BuscarTipoDoc.Rows[i]["Documento"]);
                    DocList.Add(cFactura);
                }

                DataTable BuscarPropiedad = new DataTable();
                BuscarPropiedad = BdModificarFactura.PropiedadFactura(TextBoxNoDocumento);
                List<CModificarFactura> PropiedadList = new List<CModificarFactura>();
                for (int i = 0; i < BuscarPropiedad.Rows.Count; i++)
                {
                    CModificarFactura cFactura = new CModificarFactura();
                    cFactura.Propiedad = Convert.ToString(BuscarPropiedad.Rows[i]["Propiedad"]);
                    PropiedadList.Add(cFactura);
                }

                TextBoxNoDocumento.Text = FacturaList.FirstOrDefault().NumDocumento.ToString();
                TxtFechaAdquisicion.Text = FacturaList.FirstOrDefault().FechaAdquisicion.ToString();
                TxtDetalleAdquisicion.Text = FacturaList.FirstOrDefault().DetAdqui.ToString();
                string proveedor = proveedorList.FirstOrDefault().Proveedor.ToString();
                DropDownListProveedor.SelectedIndex = DropDownListProveedor.Items.IndexOf(DropDownListProveedor.Items.FindByText(proveedor));
                string tipoAdqui = TipoAdquiList.FirstOrDefault().TipoAdqui.ToString();
                DropDownListTipoAdquisicion.SelectedIndex = DropDownListTipoAdquisicion.Items.IndexOf(DropDownListTipoAdquisicion.Items.FindByText(tipoAdqui));
                string documento = DocList.FirstOrDefault().Documento.ToString();
                DropDownListTipoDocumento.SelectedIndex = DropDownListTipoDocumento.Items.IndexOf(DropDownListTipoDocumento.Items.FindByText(documento));
                string propiedad = PropiedadList.FirstOrDefault().Propiedad.ToString();
                DropDownListPropiedad.SelectedIndex = DropDownListPropiedad.Items.IndexOf(DropDownListPropiedad.Items.FindByText(propiedad));

                /////// LLenar Grid ////////
                MostrarMensaje("** Número de documento existente **", "info", "Normal");
            }
        }
        
        /// <summary>
        /// Método para cancelar la modificación de la factura. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void BtnCancelarFactura_Click(object sender, EventArgs e)
        {
            TxtDetalleAdquisicion.Text = "";
            TextBoxNoDocumento.Text = "";
            TxtFechaAdquisicion.Text = "";
            IniciarLlenadoDropDownListProveedor();
            IniciarLlenadoDropDownTipoDocumento();
            IniciarLlenadoDropDownTipoAdquisicion();
            IniciarLlenadoDropDownListPropiedad();
        }

        /// <summary>
        /// Método para guardar la factura modificada. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void BtnGuardarFactura_Click(object sender, EventArgs e)
        {
            if (DropDownListProveedor.SelectedIndex != 0)
            {
                if (DropDownListTipoDocumento.SelectedIndex != 0)
                {
                    if (DropDownListTipoAdquisicion.SelectedIndex != 0)
                    {
                        if (DropDownListPropiedad.SelectedIndex != 0)
                        {
                            if (TextBoxNoDocumento.Text != "")
                            {
                                if (TxtFechaAdquisicion.Text != "")
                                {
                                    int idProveedor = Convert.ToInt32(DropDownListProveedor.SelectedValue);
                                    int idTipoDoc = Convert.ToInt32(DropDownListTipoDocumento.SelectedValue);
                                    int idTipoAdqui = Convert.ToInt32(DropDownListTipoAdquisicion.SelectedValue);
                                    string DetalleAdqui = TxtDetalleAdquisicion.Text;
                                    int idPropiedad = Convert.ToInt32(DropDownListPropiedad.SelectedValue);
                                    string NumDoc = TextBoxNoDocumento.Text;
                                    string fecha = TxtFechaAdquisicion.Text;
                                    SqlConnection Conn = new SqlConnection(CConexion.Obtener());
                                    bool success = false;
                                    SqlTransaction lTransaccion = null;
                                    int Valor_Retornado = 0;
                                    try
                                    {
                                        
                                        Conn.Open();
                                        lTransaccion = Conn.BeginTransaction(System.Data.IsolationLevel.Serializable);
                                        SqlCommand cmd = new SqlCommand("SP_Modificar_Factura", Conn, lTransaccion);
                                        cmd.CommandType = CommandType.StoredProcedure;
                                        cmd.Parameters.Clear();
                                        cmd.Parameters.Add(new SqlParameter("@IdProveedor", idProveedor));
                                        cmd.Parameters.Add(new SqlParameter("@IdTipoDoc", idTipoDoc));
                                        cmd.Parameters.Add(new SqlParameter("@NumDocumento", NumDoc));
                                        cmd.Parameters.Add(new SqlParameter("@IdTipoAdqui", idTipoAdqui));
                                        cmd.Parameters.Add(new SqlParameter("@DetAdqui", DetalleAdqui));
                                        cmd.Parameters.Add(new SqlParameter("@FechaAdquisicion", Convert.ToDateTime(fecha)));
                                        cmd.Parameters.Add(new SqlParameter("@IdPropiedad", idPropiedad));
                                        SqlParameter ValorRetorno = new SqlParameter("@Comprobacion", SqlDbType.Int);
                                        ValorRetorno.Direction = ParameterDirection.Output;
                                        cmd.Parameters.Add(ValorRetorno);
                                        cmd.ExecuteNonQuery();
                                        Valor_Retornado = Convert.ToInt32(ValorRetorno.Value);
                                        if (Valor_Retornado == 1)
                                            success = true;
                                    }
                                    catch (Exception)
                                    {
                                        MostrarMensaje("** Error al modificar Factura **", "info", "Normal");
                                    }
                                    finally
                                    {
                                        if (success)
                                        {
                                            lTransaccion.Commit();
                                            Conn.Close();
                                            MostrarMensaje("** Factura modificada satisfactoriamente **", "info", "Normal");
                                        }
                                        else
                                        {
                                            lTransaccion.Rollback();
                                            Conn.Close();
                                        }
                                        LimpiarFactura();
                                    }
                                }
                                else
                                {
                                    MostrarMensaje("** La fecha es requerida **", "info", "Normal");
                                }
                            }
                            else
                            {
                                MostrarMensaje("** El Número de documento es requerido **", "info", "Normal");
                            }
                        }
                        else
                        {
                            MostrarMensaje("** La propiedad es requerida **", "info", "Normal");
                        }
                    }
                    else
                    {
                        MostrarMensaje("** El tipo de adquisición es requerido **", "info", "Normal");
                    }
                }
                else
                {
                    MostrarMensaje("** El tipo de documento es requerido **", "info", "Normal");
                }
            }
            else
            {
                MostrarMensaje("** El proveedor es requerido **", "info", "Normal");
            }
        }

        /// <summary>
        /// Método para limpiar los campos del registro de la factura. 
        /// </summary>
        public void LimpiarFactura()
        {
            DropDownListProveedor.SelectedIndex = 0;
            DropDownListTipoDocumento.SelectedIndex = 0;
            DropDownListTipoAdquisicion.SelectedIndex = 0;
            TxtDetalleAdquisicion.Text = string.Empty;
            DropDownListPropiedad.SelectedIndex = 0;
            TextBoxNoDocumento.Text = string.Empty;
            TxtFechaAdquisicion.Text = string.Empty;
        }

        /// <summary>
        /// Método para validar si existe la factura a buscar. 
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public bool existe1(string num)
        {
            string num1 = TextBoxNoDocumento.Text;
            SqlConnection cnn = new SqlConnection(CConexion.Obtener());
            string sql = "select count(*)from FichaCompra where NumDocumento = @num";
            cnn.Open();

            try
            {
                SqlCommand query = new SqlCommand(sql, cnn);
                query.Parameters.AddWithValue("@num", num1);

                int count = Convert.ToInt32(query.ExecuteScalar());
                if (count == 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch (SqlException) { throw; }
            finally
            {
                if (cnn.State == ConnectionState.Open) cnn.Close();
                if (cnn.State == ConnectionState.Broken) cnn.Close();
            }
        }
    }

}