using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using InventariosPJEH.CAccesoDatos;
using InventariosPJEH.CNegocios;
using InventariosPJEH.CVista;
using System.Linq;
using System.Web;

namespace InventariosPJEH.CAccesoDatos
{
    public class BdCatProveedores
    {

        /// <summary>
        /// Clase que muestra la lista de todos los estados 
        /// </summary>
        /// <returns></returns>
        public static DataTable IniciarEdtados()
        {
            DataTable Tabla = new DataTable("Resultado");
            SqlConnection cnn = new SqlConnection(CConexion.Obtener());
            SqlCommand cmd = new SqlCommand();
            SqlDataAdapter adp = new SqlDataAdapter();
            cmd.CommandText = "select * from Cat_Estado ORDER BY Estado ASC";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = cnn;
            adp.SelectCommand = cmd;
            cnn.Open();
            adp.Fill(Tabla);
            cnn.Close();
            return Tabla;
        }

        /// <summary>
        /// Clase que muestra todos los municipios 
        /// </summary>
        /// <param name="IdEstado"></param>
        /// <returns></returns>
        public static DataTable IniciarMunicipios(int IdEstado)
        {
            DataTable Tabla = new DataTable("Resultado");
            SqlConnection cnn = new SqlConnection(CConexion.Obtener());
            SqlCommand cmd = new SqlCommand();
            SqlDataAdapter adp = new SqlDataAdapter();
            cmd.CommandText = "select Distinct IdMunicipio,  Municipio from Cat_Municipios m, Cat_Estado e where m.IdEstado=" + IdEstado + "ORDER BY Municipio ASC";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = cnn;
            adp.SelectCommand = cmd;
            cnn.Open();
            adp.Fill(Tabla);
            cnn.Close();
            return Tabla;
        }

        /// <summary>
        /// Lista todos los proveedores relacionados con el nombre del proveedor
        /// </summary>
        /// <param name="nNombreProveedor"></param>
        /// <returns></returns>
        public static List<CProveedores> ConsultarGbProveedor(string nNombreProveedor)
        {
            List<CProveedores> lista = new List<CProveedores>();
            SqlConnection cnn = new SqlConnection(CConexion.Obtener());
            try
            {
          
                 SqlCommand cmd = new SqlCommand(string.Format("select * from VistaProveedores where Proveedor like '%{0}%' ORDER BY Proveedor ASC", nNombreProveedor), cnn);

                cnn.Open();

                using (var rd = cmd.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        CProveedores cproveedor = new CProveedores();
                        cproveedor.IdProveedor = rd["IdProveedor"].ToString();
                        cproveedor.Proveedor = rd["Proveedor"].ToString();
                        cproveedor.Calle = rd["Calle"].ToString();
                        cproveedor.Colonia = rd["Colonia"].ToString();
                        cproveedor.Estado = rd["Estado"].ToString();
                        cproveedor.Municipio = rd["Municipio"].ToString();
                        cproveedor.Telefono = rd["Telefono"].ToString();
                        cproveedor.email = rd["email"].ToString();
                        cproveedor.CP = rd["CP"].ToString();

                        lista.Add(cproveedor);
                    }
                }
            }
            catch (Exception )
            {
                cnn.Close();
            }
            return lista;
        }


        /// <summary>
        /// Clase que elimina a los proveedores
        /// </summary>
        /// <param name="obJE"></param>
        /// 
        public void Eliminar_Proveedor(CProveedores obJE)
        {
            SqlConnection cnn = new SqlConnection(CConexion.Obtener());
            cnn.Open();
            SqlCommand cmd = new SqlCommand("SP_Eliminar_Proveedor", cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            /// cmd.Parameters.AddWithValue("@idProveedor", obJE.IdProveedor);
            cmd.Parameters.Add(new SqlParameter("@idProveedor", SqlDbType.Int)).Value = obJE.IdProveedor;
            cmd.Parameters.Add(new SqlParameter("@Comprobacion", SqlDbType.Int)).Value = 2;
            cmd.ExecuteNonQuery();
            cnn.Close();
        }


    }
}