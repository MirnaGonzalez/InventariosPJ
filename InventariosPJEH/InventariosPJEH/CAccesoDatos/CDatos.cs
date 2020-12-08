using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using InventariosPJEH.CNegocios;

namespace InventariosPJEH.CAccesoDatos
{

    /// <summary>
    /// Devuelve la cadena de conexión 
    /// </summary>
    internal class Conexion
    {
        internal static string Obtener()
        {
            // return "Server=SERVERK\\SQLEXPRESS;Database=Inventarios;Uid=sa;Pwd=Kmala123;";

            return "Server=200.79.183.181;Database=Inventarios;Uid=Inventa2020;Pwd=Resguardo2020";


        }


    }


    /// <summary>
    /// Clase que maneja, en la BD, 
    /// las Altas Bajas y consultas Relacionasdas con las Unidades Administrativas
    /// </summary>
    public class BdUniAdmin
    {
        /// <summary>
        /// Obtiene la lista de Unidades Administrativas  desde la Base de Datos y 
        /// devuelve una lista de objetos UniAdmin
        /// </summary>
        /// <param name="r"></param>
        /// <returns></returns>
        public static List<TUniAdmin> ObtenerLista(ref TResultado r)
        {
            SqlConnection cnn = new SqlConnection(Conexion.Obtener());
            SqlCommand cmd = new SqlCommand();
            SqlDataReader rdr = null;
            string qry;

            List<TUniAdmin> ListaUniAdmin = new List<TUniAdmin>();

            qry =  "SELECT IdUniAdmin, UniAdmin ";
            qry += "FROM Cat_UniAdmin ";
            qry += "WHERE (Tipo = 'UA') ";
            qry += "ORDER BY UniAdmin ";

            cmd.Connection = cnn;
            cmd.CommandText = qry;

            try
            {
                cnn.Open();
                rdr = cmd.ExecuteReader();
            }
            catch (Exception e)
            {
                r.Exito = false;
                r.Mensaje.Add("Se generó un error al tratar de obtener la lista de Unidades Administrativas");
                r.Mensaje.Add(e.Message);

                if (cnn.State == ConnectionState.Open)
                    cnn.Close();
            }

            try
            {
                while (rdr.Read())
                {

                    TUniAdmin UniAdm = new TUniAdmin();
                    UniAdm.IdUniAdmin = BdConverter.FieldToInt(rdr.GetInt32(0));
                    UniAdm.UniAdmin = BdConverter.FieldToString(rdr.GetString(1));

                    ListaUniAdmin.Add(UniAdm);
                }

                cnn.Close();
            }
            catch (Exception e)
            {
                r.Exito = false;
                r.Mensaje.Add("Se generó un error al tratar de leer la lista de Unidades Administrativas");
                r.Detalles = e.Message;
                if (cnn.State == ConnectionState.Open)
                    cnn.Close();
            }

            return ListaUniAdmin;
        }

        
    }


}