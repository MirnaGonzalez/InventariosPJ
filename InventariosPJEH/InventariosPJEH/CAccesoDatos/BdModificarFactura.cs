using InventariosPJEH.CNegocios;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace InventariosPJEH.CAccesoDatos
{
    public class BdModificarFactura
    {
        public SqlConnection conexion;
        public string error;
        protected SqlDataAdapter adaptador;
        protected SqlDataReader reader;
        protected DataSet data;

        public BdModificarFactura()
        {
            this.conexion = ConexionBD.getConexion();
        }

        /// <summary>
        /// Consulta para obtener los tipos de documento. 
        /// </summary>
        /// <returns></returns>
        public static List<CModificarFactura> ObtenerTipoDocumento()
        {
            List<CModificarFactura> lista = new List<CModificarFactura>();

            SqlConnection cnn = new SqlConnection(CConexion.Obtener());
            SqlCommand cmd = new SqlCommand("select IdTipoDoc, Documento from Cat_TipoDocumento", cnn);
            cnn.Open();

            using (var rd = cmd.ExecuteReader())
            {
                while (rd.Read())
                {
                    CModificarFactura cRegistroDeUnBien = new CModificarFactura();
                    cRegistroDeUnBien.Documento = rd.GetString(1);
                    cRegistroDeUnBien.IdTipoDoc = rd.GetInt32(0);
                    lista.Add(cRegistroDeUnBien);
                }
            }
            return lista;
        }

        /// <summary>
        /// Consulta para obtener los tipos de adquisiciión. 
        /// </summary>
        /// <returns></returns>
        public static List<CModificarFactura> ObtenerTipoAdquisicion()
        {
            List<CModificarFactura> listaAdqui = new List<CModificarFactura>();

            SqlConnection cnn = new SqlConnection(CConexion.Obtener());
            SqlCommand cmd = new SqlCommand("select IdTipoAdqui, TipoAdqui from Cat_TipoAdquisicion", cnn);
            cnn.Open();

            using (var rd = cmd.ExecuteReader())
            {
                while (rd.Read())
                {
                    CModificarFactura cRegistroDeUnBien = new CModificarFactura();
                    cRegistroDeUnBien.TipoAdqui = rd.GetString(1);
                    cRegistroDeUnBien.IdTipoAdqui = rd.GetInt32(0);
                    listaAdqui.Add(cRegistroDeUnBien);
                }
            }
            return listaAdqui;
        }

        /// <summary>
        /// Consulta para obtener los proveedores. 
        /// </summary>
        /// <returns></returns>
        public static List<CModificarFactura> ObtenerProveedor()
        {
            List<CModificarFactura> listaProveedor = new List<CModificarFactura>();

            SqlConnection cnn = new SqlConnection(CConexion.Obtener());
            SqlCommand cmd = new SqlCommand("select IdProveedor, Proveedor from Cat_Proveedores", cnn);
            cnn.Open();

            using (var rd = cmd.ExecuteReader())
            {
                while (rd.Read())
                {
                    CModificarFactura cRegistroDeUnBien = new CModificarFactura();
                    cRegistroDeUnBien.Proveedor = rd.GetString(1);
                    cRegistroDeUnBien.IdProveedor = rd.GetInt32(0);
                    listaProveedor.Add(cRegistroDeUnBien);
                }
            }
            return listaProveedor;
        }

        /// <summary>
        /// Consulta para obtener las propiedades. 
        /// </summary>
        /// <returns></returns>
        public static List<CModificarFactura> ObtenerPropiedad()
        {
            List<CModificarFactura> listaPropiedad = new List<CModificarFactura>();

            SqlConnection cnn = new SqlConnection(CConexion.Obtener());
            SqlCommand cmd = new SqlCommand("select IdPropiedad, Propiedad from Cat_Propiedades", cnn);
            cnn.Open();

            using (var rd = cmd.ExecuteReader())
            {
                while (rd.Read())
                {
                    CModificarFactura cRegistroDeUnBien = new CModificarFactura();
                    cRegistroDeUnBien.Propiedad = rd.GetString(1);
                    cRegistroDeUnBien.IdPropiedad = rd.GetInt32(0);
                    listaPropiedad.Add(cRegistroDeUnBien);
                }
            }
            return listaPropiedad;
        }

        /// <summary>
        /// Consulta para obtener toda l información de las facturas. 
        /// </summary>
        /// <param name="TextBoxNoDocumento"></param>
        /// <returns></returns>
        public static DataTable BuscarFactura(TextBox TextBoxNoDocumento)
        {
            string numDoc = TextBoxNoDocumento.Text;
            DataTable TablaFactura = new DataTable("Resultado");
            SqlConnection cnn = new SqlConnection(CConexion.Obtener());
            var query = @"select * from FichaCompra where NumDocumento = @numeroDoc";
            SqlCommand cmd = new SqlCommand(query, cnn);
            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Add("@numeroDoc", SqlDbType.VarChar, 30).Value = numDoc;
            cmd.Connection = cnn;
            adp.SelectCommand = cmd;
            cnn.Open();
            adp.Fill(TablaFactura);
            cnn.Close();
            return TablaFactura;
        }

        /// <summary>
        /// Consulta para obtener el proveedor de la factura seleccionada. 
        /// </summary>
        /// <param name="TextBoxNoDocumento"></param>
        /// <returns></returns>
        public static DataTable ProveedorFactura(TextBox TextBoxNoDocumento)
        {
            string numDoc = TextBoxNoDocumento.Text;
            DataTable TablaFactura = new DataTable("Resultado");
            SqlConnection cnn = new SqlConnection(CConexion.Obtener());
            var query = @"select Proveedor from Cat_Proveedores inner join FichaCompra on Cat_Proveedores.IdProveedor = FichaCompra.IdProveedor 
                            where NumDocumento = @numeroDoc";
            SqlCommand cmd = new SqlCommand(query, cnn);
            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Add("@numeroDoc", SqlDbType.VarChar, 30).Value = numDoc;
            cmd.Connection = cnn;
            adp.SelectCommand = cmd;
            cnn.Open();
            adp.Fill(TablaFactura);
            cnn.Close();
            return TablaFactura;
        }

        /// <summary>
        /// Consulta para obtener el documento de la factura seleccionada. 
        /// </summary>
        /// <param name="TextBoxNoDocumento"></param>
        /// <returns></returns>
        public static DataTable DocumentoFactura(TextBox TextBoxNoDocumento)
        {
            string numDoc = TextBoxNoDocumento.Text;
            DataTable TablaFactura = new DataTable("Resultado");
            SqlConnection cnn = new SqlConnection(CConexion.Obtener());
            var query = @"select Documento from Cat_TipoDocumento inner join FichaCompra on Cat_TipoDocumento.IdTipoDoc = FichaCompra.IdTipoDoc 
                        where NumDocumento = @numeroDoc";
            SqlCommand cmd = new SqlCommand(query, cnn);
            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Add("@numeroDoc", SqlDbType.VarChar, 30).Value = numDoc;
            cmd.Connection = cnn;
            adp.SelectCommand = cmd;
            cnn.Open();
            adp.Fill(TablaFactura);
            cnn.Close();
            return TablaFactura;
        }

        /// <summary>
        /// Consulta para obtener el tipo de adquisición de la factura seleccinada. 
        /// </summary>
        /// <param name="TextBoxNoDocumento"></param>
        /// <returns></returns>
        public static DataTable AdquiFactura(TextBox TextBoxNoDocumento)
        {
            string numDoc = TextBoxNoDocumento.Text;
            DataTable TablaFactura = new DataTable("Resultado");
            SqlConnection cnn = new SqlConnection(CConexion.Obtener());
            var query = @"select TipoAdqui from Cat_TipoAdquisicion inner join FichaCompra on Cat_TipoAdquisicion.IdTipoAdqui = FichaCompra.IdTipoAdqui
                        where NumDocumento = @numeroDoc";
            SqlCommand cmd = new SqlCommand(query, cnn);
            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Add("@numeroDoc", SqlDbType.VarChar, 30).Value = numDoc;
            cmd.Connection = cnn;
            adp.SelectCommand = cmd;
            cnn.Open();
            adp.Fill(TablaFactura);
            cnn.Close();
            return TablaFactura;
        }

        /// <summary>
        /// Consulta para obtener la propiedad de la factura seleccionada. 
        /// </summary>
        /// <param name="TextBoxNoDocumento"></param>
        /// <returns></returns>
        public static DataTable PropiedadFactura(TextBox TextBoxNoDocumento)
        {
            string numDoc = TextBoxNoDocumento.Text;
            DataTable TablaFactura = new DataTable("Resultado");
            SqlConnection cnn = new SqlConnection(CConexion.Obtener());
            var query = @"select Propiedad from Cat_Propiedades inner join FichaCompra on Cat_Propiedades.IdPropiedad = FichaCompra.IdPropiedad
                        where NumDocumento = @numeroDoc";
            SqlCommand cmd = new SqlCommand(query, cnn);
            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Add("@numeroDoc", SqlDbType.VarChar, 30).Value = numDoc;
            cmd.Connection = cnn;
            adp.SelectCommand = cmd;
            cnn.Open();
            adp.Fill(TablaFactura);
            cnn.Close();
            return TablaFactura;
        }
    }

}
