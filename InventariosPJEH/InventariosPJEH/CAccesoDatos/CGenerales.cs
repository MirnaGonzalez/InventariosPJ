using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;


namespace InventariosPJEH.CAccesoDatos
{
    public class ConexionBD
    {
        private static SqlConnection objConexion;
        private static string error;
        public static  SqlConnection getConexion()
        {
            if (objConexion != null)
                return objConexion;
            objConexion = new SqlConnection();
            // objConexion.ConnectionString = "Data Source=KITTY\\SQLEXPRESS; Initial Catalog=Inventarios; Integrated Security=true";
            // objConexion.ConnectionString = "Data Source=MIRNAGONZLEF221\\SQLEXPRESS;Initial Catalog=Inventarios;Persist Security Info=True;User ID=MYGL;Password=MYGL31";

            objConexion.ConnectionString = "Data Source=200.79.183.181;Initial Catalog=Inventarios;Persist Security Info=True;User ID=Inventa2020;Password=Resguardo2020";

            try
            {
                objConexion.Open();
                return objConexion;
            }
            catch (Exception e)
            {
                error = e.Message;
                return null;
            }
        }
        public static void cerrarConexion()
        {
            if (objConexion != null)
                objConexion.Close();
        }
    }

    /// <summary>
    /// Devuelve la cadena de conexión 
    /// </summary>
    internal class CConexion
    {

        
        internal static string Obtener()
        {

            // return "Data Source=KITTY\\SQLEXPRESS; Initial Catalog=Inventarios; Uid=sa; Password=Kittymala123;";

            // return "Server=MIRNAGONZLEF221\\SQLEXPRESS;Database=Inventarios;Uid=MYGL;Pwd=MYGL31";

            return "Server=200.79.183.181;Database=Inventarios;Uid=Inventa2020;Pwd=Resguardo2020";




        }
    }
    

  
    /// ///////////////////////////////////////////////////////////////////////
   

    public class BdConverter
    {
        /// <summary>
        /// Convierte un campo (Object) a un cadena
        /// Si el valor es nulo devuelve una cadena Vacia
        /// devuelve 0;
        /// </summary>
        /// <param name="O"></param>
        /// <returns></returns>
        public static string FieldToString(object Campo)
        {
            if (Campo == DBNull.Value)
                return string.Empty;
            else
                return Campo.ToString();
        }

        /// <summary>
        /// Convierte un campo (Object) a un Entero
        /// Si el valor es nulo devuelve una cadena Vacia
        /// devuelve 0;
        /// </summary>
        /// <param name="O"></param>
        /// <returns></returns>
        public static int FieldToInt(object Campo)
        {
            if (Campo == DBNull.Value)
                return 0;
            else
                return (int)Campo;
        }

        /// <summary>
        /// Convierte un campo (Object) a un Entero
        /// Si el valor es nulo devuelve 0
        /// </summary>
        /// <param name="O"></param>
        /// <returns></returns>
        public static Int64 FieldToInt64(object Campo)
        {
            if (Campo == DBNull.Value)
                return 0;
            else
                return (Int64)Campo;
        }

        /// <summary>
        /// Convierte un campo (Object) a un Flotante
        /// Si el valor es nulo devuelve 0
        /// </summary>
        /// <param name="O"></param>
        /// <returns></returns>
        public static float FieldToFloat(object Campo)
        {
            if (Campo == DBNull.Value)
                return 0;
            else
                return (float)(Double)Campo;
        }

        /// <summary>
        /// Convierte un campo (Object) a un Double
        /// Si el valor es nulo devuelve 0
        /// </summary>
        /// <param name="O"></param>
        /// <returns></returns>
        public static Double FieldToDouble(object Campo)
        {
            if (Campo == DBNull.Value)
                return 0;
            else
                return (Double)Campo;
        }

        public static DateTime FieldToDate(Object Campo)
        {
            if (Campo == DBNull.Value)
                return new DateTime();
            else
                return Convert.ToDateTime(Campo);
        }

        public static Boolean FieldToBoolean(Object Campo)
        {
            if (Campo == DBNull.Value)
                return false;
            else
                return Convert.ToBoolean(Campo);
        }
    }
}