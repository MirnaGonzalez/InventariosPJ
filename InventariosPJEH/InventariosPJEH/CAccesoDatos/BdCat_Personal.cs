using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using InventariosPJEH.CNegocios;
using System.Configuration;
using System.Windows;
using System.Web.UI;
using InventariosPJEH.CVista;
using InventariosPJEH.CAccesoDatos;
using System.Web.UI.WebControls;

using System.Data.Odbc;

namespace InventariosPJEH.CAccesoDatos

{
    public class BdCat_Personal
    {


        public SqlConnection conexion;
        public string error;
        protected SqlDataAdapter adaptador;
        protected SqlDataReader reader;
        protected DataSet data;
        public BdCat_Personal()
        {
            this.conexion = ConexionBD.getConexion();
        }

        /// <summary>
        /// CONSULTA DE PERSONAL
        /// </summary>
        /// <param name="clasificacion"></param>
        /// <param name="distrito"></param>
        /// <param name="uniadmin"></param>
        /// <param name="pClaveEmpleado"></param>
        /// <param name="pNombre"></param>
        /// <param name="APaterno"></param>
        /// <param name="AMaterno"></param>
        /// <returns></returns>


        public static DataTable ConsultarGbPersonal(string clasificacion, string distrito, string uniadmin, string pClaveEmpleado, string pNombre, string APaterno, string AMaterno)
        {
                       
            CUsuario DatosUsuario = new CUsuario();
            DataTable Tabla = new DataTable("Resultado");
            SqlConnection cnn = new SqlConnection(CConexion.Obtener());
            var query = @"SELECT Descripcion,Distrito,UniAdmin,ClaveEmpleado,Nombre,APaterno,AMaterno, Titular, EstatusPer,EstatusInv, Nomina, IdEmpleado, Cargo FROM VistaPersonalBuscar where Descripcion = @Descripcion ";




            if (distrito != "")
            {
                query += "   and distrito = @distrito ";
            }

            if (uniadmin != "")
            {
                query += " and uniadmin = @uniadmin ";
            }

            if (pClaveEmpleado != "")
            {
                query += " and ClaveEmpleado = @ClaveEmpleado ";
            }

            if (pNombre != "")
            {
                query += " and Nombre LIKE '%' + @Nombre + '%' ";
            }

            if (APaterno != "")
            {
                query += " and APaterno LIKE '%' + @APaterno + '%' ";
            }

            if (AMaterno != "")
            {
                query += " and AMaterno LIKE '%' + @AMaterno + '%' ";
            }


            query += " ORDER BY ClaveEmpleado ASC ";


            SqlCommand cmd = new SqlCommand(query, cnn);
            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Add("@Descripcion", System.Data.SqlDbType.VarChar, 25).Value = clasificacion;
            cmd.Parameters.Add("@Distrito", System.Data.SqlDbType.VarChar, 100).Value = distrito;
            cmd.Parameters.Add("@UniAdmin", System.Data.SqlDbType.VarChar, 250).Value = uniadmin;
            cmd.Parameters.Add("@claveEmpleado", System.Data.SqlDbType.VarChar, 50).Value = pClaveEmpleado;
            cmd.Parameters.Add("@nombre", System.Data.SqlDbType.VarChar, 50).Value = pNombre;
            cmd.Parameters.Add("@aPaterno", System.Data.SqlDbType.VarChar, 50).Value = APaterno;
            cmd.Parameters.Add("@aMaterno", System.Data.SqlDbType.VarChar, 50).Value = AMaterno;


            cmd.Connection = cnn;
            adp.SelectCommand = cmd;
            cnn.Open();
            adp.Fill(Tabla);
            cnn.Close();

            return Tabla;
        }

                      
        /// <summary>
        /// 
        /// 
        /// </summary>
        /// <param name="ClaveEmpleado"></param>
        /// <param name="Nombre"></param>
        /// <param name="APaterno"></param>
        /// <param name="AMaterno"></param>
        /// <param name="Res"></param>
        /// <returns></returns>
        public static int ConsultarPersonal(string ClaveEmpleado, string Nombre, string APaterno, string AMaterno, ref TResultado Res)
        {
            int ResultadoAutenticacion = -1;
            SqlCommand cmd = new SqlCommand();

            cmd.Connection = new SqlConnection(CConexion.Obtener());
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "SP_Consultar_Personal";
            cmd.Parameters.Add("@claveEmpleado", System.Data.SqlDbType.VarChar, 50).Value = ClaveEmpleado;
            cmd.Parameters.Add("@nombre", System.Data.SqlDbType.VarChar, 50).Value = Nombre;
            cmd.Parameters.Add("@aPaterno", System.Data.SqlDbType.VarChar, 50).Value = APaterno;
            cmd.Parameters.Add("@aMaterno", System.Data.SqlDbType.VarChar, 50).Value = AMaterno;

            return ResultadoAutenticacion;

        }


/// <summary>
/// Selecciona los Tipos de Unidades Administrativas
/// </summary>
/// <returns></returns>
        public static DataTable ClasificaciónUA()
        {
            DataTable Tabla = new DataTable("Resultado");
            SqlConnection cnn = new SqlConnection(CConexion.Obtener());
            SqlCommand cmd = new SqlCommand();
            SqlDataAdapter adp = new SqlDataAdapter();
            cmd.CommandText = "select DISTINCT idAbreviatura, Descripcion from Abreviaturas where tipo = 'ClasificaciónUA' ORDER BY Descripcion ASC";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = cnn;
            adp.SelectCommand = cmd;
            cnn.Open();
            adp.Fill(Tabla);
            cnn.Close();
            return Tabla;
        }

        public static DataTable ObtenerClasificacion(int Clasificacion)
        {

            DataTable Tabla = new DataTable("Resultado");
            SqlConnection cnn = new SqlConnection(CConexion.Obtener());
            SqlCommand cmd = new SqlCommand();
            SqlDataAdapter adp = new SqlDataAdapter();
            cmd.CommandText = "select DISTINCT UniAdmin, idUniAdmin from Cat_UniAdmin u, Abreviaturas where u.IdAbreviatura = " + Clasificacion + "ORDER BY UniAdmin ASC";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = cnn;
            adp.SelectCommand = cmd;
            cnn.Open();
            adp.Fill(Tabla);
            cnn.Close();
            return Tabla;
        }
        public static DataTable ObtenerClasificacionPorNombre (string Unidad)
        {

            DataTable Tabla = new DataTable("Resultado");
            SqlConnection cnn = new SqlConnection(CConexion.Obtener());
            SqlCommand cmd = new SqlCommand();
            SqlDataAdapter adp = new SqlDataAdapter();
         
            cmd.CommandText = "select DISTINCT UniAdmin, Descripcion, idUniAdmin from Cat_UniAdmin u, Abreviaturas  where Descripcion = " + Unidad + "ORDER BY UniAdmin ASC";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = cnn;
            adp.SelectCommand = cmd;
            cnn.Open();
            adp.Fill(Tabla);
            cnn.Close();
            return Tabla;
        }

        public static DataTable ObtenerDistritos()
        {
            SqlCommand cmd = new SqlCommand();
            DataTable Tabla = new DataTable("Resultado");
            SqlConnection cnn = new SqlConnection(CConexion.Obtener());
            SqlDataAdapter adp = new SqlDataAdapter();
            
            cmd.CommandText = "select IdDistrito, Distrito from Cat_Distritos ORDER BY Distrito ASC";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = cnn;
            adp.SelectCommand = cmd;
            cnn.Open();
            adp.Fill(Tabla);
            cnn.Close();
            return Tabla;
        }

        public static DataTable ObtenerUniAdminN(int IdDistrito)
        {
            SqlCommand cmd = new SqlCommand();
            DataTable Tabla = new DataTable("Resultado");
            SqlConnection cnn = new SqlConnection(CConexion.Obtener());
            SqlDataAdapter adp = new SqlDataAdapter();
            cmd.CommandText = "select IdUniAdmin, UniAdmin from VistaDistritos where  IdDistrito =" + IdDistrito + "ORDER BY UniAdmin ASC";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = cnn;
            adp.SelectCommand = cmd;
            cnn.Open();
            adp.Fill(Tabla);
            cnn.Close();
            return Tabla;
        }
        public static DataTable ObtenerCargos()
        {
            SqlCommand cmd = new SqlCommand();
            DataTable Tabla = new DataTable("Resultado");
            SqlConnection cnn = new SqlConnection(CConexion.Obtener());
            SqlDataAdapter adp = new SqlDataAdapter();
            cmd.CommandText = "SELECT Cargo, IdCargo from Cat_Cargo ORDER BY Cargo ASC";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = cnn;
            adp.SelectCommand = cmd;
            cnn.Open();
            adp.Fill(Tabla);
            cnn.Close();
            return Tabla;
        }

        public static DataTable BuscarPersonal(string pClaveEmpleado, string pNombre, string APaterno, string AMaterno )
        {
         
            using (SqlConnection cnn = new SqlConnection(CConexion.Obtener()))
                {
               
                SqlCommand cmd = new SqlCommand(string.Format(
                "Select * from VistaPersonalBuscar where ClaveEmpleado like '%{0}%' OR Nombre like '%{1}%' OR APaterno like '%{2}%' OR AMaterno like '%{3}%' ORDER BY APaterno ASC", pClaveEmpleado, pNombre, APaterno, AMaterno));

                DataTable Tabla = new DataTable("Resultado");
                SqlDataAdapter adp = new SqlDataAdapter();
                cmd.CommandType = CommandType.Text;
                cmd.Connection = cnn;
                adp.SelectCommand = cmd;
                cnn.Open();
                adp.Fill(Tabla);
                cnn.Close();
                return Tabla;
            }
        }


        public void Eliminar_Personal(CPersonal obJE)
        {
            SqlConnection cnn = new SqlConnection(CConexion.Obtener());
            SqlCommand cmd = new SqlCommand("SP_Eliminar_Personal", cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@idEmpleado", obJE.IdEmpleado);
            cnn.Open();
            cmd.ExecuteNonQuery();
            cnn.Close();
        }







    }
}


    
    


