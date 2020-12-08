using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using InventariosPJEH.CNegocios;

namespace InventariosPJEH.CAccesoDatos
{
    public class BdCatEdificios_Niveles_Secciones
    {
        //Métod para incializar la carga de partidas
        public static DataTable InicializarCarga()
        {
            DataTable Tabla = new DataTable("Resultado");
            SqlConnection cnn = new SqlConnection(CConexion.Obtener());
            SqlCommand cmd = new SqlCommand();
            SqlDataAdapter adp = new SqlDataAdapter();
            cmd.CommandText = "select * from Cat_Municipios where idEstado=13 ORDER BY Municipio ASC";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = cnn;
            adp.SelectCommand = cmd;
            cnn.Open();
            adp.Fill(Tabla);
            cnn.Close();
            return Tabla;
        }

        public static DataTable InicializarCargaEdificioNuevo()
        {
            DataTable Tabla = new DataTable("Resultado");
            SqlConnection cnn = new SqlConnection(CConexion.Obtener());
            SqlCommand cmd = new SqlCommand();
            SqlDataAdapter adp = new SqlDataAdapter();
            cmd.CommandText = "select * from Cat_Edificio ORDER BY Edificio ASC";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = cnn;
            adp.SelectCommand = cmd;
            cnn.Open();
            adp.Fill(Tabla);
            cnn.Close();
            return Tabla;
        }

        public static DataTable ObtenerEdificios(int edificios)
        {

            DataTable Tabla = new DataTable("Resultado");
            SqlConnection cnn = new SqlConnection(CConexion.Obtener());
            SqlCommand cmd = new SqlCommand();
            SqlDataAdapter adp = new SqlDataAdapter();
            cmd.CommandText = "select DISTINCT IdEdificio, Edificio from VistaMunicipiosEdificios where IdMunicipio = " + edificios + "ORDER BY Edificio ASC";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = cnn;
            adp.SelectCommand = cmd;
            cnn.Open();
            adp.Fill(Tabla);
            cnn.Close();
            return Tabla;
        }

        public static DataTable ObtenerNiveles(int niveles)
        {

            DataTable Tabla = new DataTable("Resultado");
            SqlConnection cnn = new SqlConnection(CConexion.Obtener());
            SqlCommand cmd = new SqlCommand();
            SqlDataAdapter adp = new SqlDataAdapter();
            cmd.CommandText = "select DISTINCT IdNivel, Nivel from VistaEdificiosNivelesSecciones where IdEdificio= " + niveles + "ORDER BY Nivel ASC";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = cnn;
            adp.SelectCommand = cmd;
            cnn.Open();
            adp.Fill(Tabla);
            cnn.Close();
            return Tabla;
        }

        public static DataTable ObtenerSecciones(int edificios)
        {
            DataTable Tabla = new DataTable("Resultado");
            SqlConnection cnn = new SqlConnection(CConexion.Obtener());
            SqlCommand cmd = new SqlCommand();
            SqlDataAdapter adp = new SqlDataAdapter();
            cmd.CommandText = "select DISTINCT IdSeccion, Nombre from VistaEdificiosNivelesSecciones where IdNivel= " + edificios + "ORDER BY Nombre ASC";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = cnn;
            adp.SelectCommand = cmd;
            cnn.Open();
            adp.Fill(Tabla);
            cnn.Close();
            return Tabla;
        }

        


        public static DataTable ConsultarGbEdNivSec(string pMunicipio, string pEdificio ) //, string pNivel, string pSeccion)
        {

            CUsuario DatosUsuario = new CUsuario();
            DataTable Tabla = new DataTable("Resultado");
            SqlConnection cnn = new SqlConnection(CConexion.Obtener());
            //var query = @"SELECT DISTINCT IdMunicipio, Municipio, IdEdificio, Edificio, Calle, Colonia, CP FROM VistaEdificiosNivelesSecciones WHERE Municipio = @Municipio ";
            var query = @"SELECT DISTINCT IdMunicipio, Municipio, IdEdificio, Edificio, Calle, Colonia, CP FROM VistaMunicipiosEdificios WHERE Municipio = @Municipio  ";


            if (pEdificio != "")
            {
                query += " and Edificio = @Edificio ";
            }

            //if (pNivel != "")
            //{
            //    query += " and Nivel = @Nivel ";
            //}

            //if (pSeccion != "")
            //{
            //    query += " and Seccion = @Seccion ";
            //}

        
            query += " ORDER BY IdEdificio ASC ";


            SqlCommand cmd = new SqlCommand(query, cnn);
            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Add("@Municipio", System.Data.SqlDbType.VarChar, 250).Value = pMunicipio;
            cmd.Parameters.Add("@Edificio", System.Data.SqlDbType.VarChar, 250).Value = pEdificio;
            //cmd.Parameters.Add("@Nivel", System.Data.SqlDbType.VarChar, 250).Value = pNivel;
            //cmd.Parameters.Add("@Seccion", System.Data.SqlDbType.VarChar, 250).Value = pSeccion;


            cmd.Connection = cnn;
            adp.SelectCommand = cmd;
            cnn.Open();
            adp.Fill(Tabla);
            cnn.Close();

            return Tabla;
        }




        /// <summary>
        /// 
        /// </summary>
        /// <param name="pEdificio"></param>
        /// <returns></returns>
        public static List<CEdificios_Niveles_Secciones> ConsultarGbNiveles(string pEdificio)
        {
            List<CEdificios_Niveles_Secciones> lista = new List<CEdificios_Niveles_Secciones>();
            SqlConnection cnn = new SqlConnection(CConexion.Obtener());
            SqlCommand cmd = new SqlCommand(string.Format("Select DISTINCT IdEdificio,Edificio,IdNivel,Nivel from VistaEdificioNiveles where IdEdificio = {0} ORDER BY Edificio ASC", pEdificio), cnn);
            cnn.Open();
            using (var rd = cmd.ExecuteReader())
            {
                while (rd.Read())
                {
                    CEdificios_Niveles_Secciones cENS = new CEdificios_Niveles_Secciones();
                    cENS.IdEdificio = rd["IdEdificio"].ToString();
                    cENS.Edificio = rd["Edificio"].ToString();
                    cENS.IdNivel = rd["IdNivel"].ToString();
                    cENS.Nivel = rd["Nivel"].ToString();
                    lista.Add(cENS);
                }
            }
            return lista;
        }
 
        public static List<CEdificios_Niveles_Secciones> ConsultarGbSecciones(string pNivel, string pEdificio)
        {
            List<CEdificios_Niveles_Secciones> lista = new List<CEdificios_Niveles_Secciones>();
            SqlConnection cnn = new SqlConnection(CConexion.Obtener());
            SqlCommand cmd = new SqlCommand(string.Format("Select DISTINCT IdEdificio,Edificio,IdNivel,Nivel,IdSeccion,Nombre,TipoSeccion, IdENSU from VistaEdificiosNivelesSecciones where IdNivel like '%{0}%' and IdEdificio like '%{1}%' ORDER BY Edificio ASC", pNivel, pEdificio), cnn);
            cnn.Open();
            using (var rd = cmd.ExecuteReader())
            {
                while (rd.Read())
                {
                    CEdificios_Niveles_Secciones cENS = new CEdificios_Niveles_Secciones();
                    cENS.IdEdificio = rd["IdEdificio"].ToString();
                    cENS.Edificio = rd["Edificio"].ToString();
                    cENS.IdNivel = rd["IdNivel"].ToString();
                    cENS.Nivel = rd["Nivel"].ToString();
                    cENS.IdSeccion = rd["IdSeccion"].ToString();
                    cENS.Nombre = rd["Nombre"].ToString();
                    cENS.Tipo = rd["TipoSeccion"].ToString();
                    cENS.IdENSU = rd["IdENSU"].ToString();
                    lista.Add(cENS);
                }
            }
            return lista;
        }

    //    //Método para eliminar una subclase
    //    public void Eliminar_Edificios(CEdificios_Niveles_Secciones obJE)
    //    {
    //        bool success = false;
    //        SqlConnection Conn = new SqlConnection(CConexion.Obtener());
    //        SqlTransaction lTransaccion = null;
    //        int Valor_Retornado = 0;
    //        try
    //        {
                          

    //            SqlConnection cnn = new SqlConnection(CConexion.Obtener());
    //            SqlCommand cmd = new SqlCommand("SP_Eliminar_Edificios", cnn);
    //            cmd.CommandType = CommandType.StoredProcedure;
    //            cmd.Parameters.AddWithValue("@idEdificio", obJE.IdEdificio);
    //            SqlParameter ValorRetorno = new SqlParameter("@Comprobacion", SqlDbType.Int);
    //            cnn.Open();

    //            int retorno = cmd.ExecuteNonQuery();
    //            cnn.Close();


    //            ValorRetorno.Direction = ParameterDirection.Output;
    //            cmd.Parameters.Add(ValorRetorno);
    //            cmd.ExecuteNonQuery();
    //            Valor_Retornado = Convert.ToInt32(ValorRetorno.Value);


    //            Valor_Retornado = Convert.ToInt32(ValorRetorno.Value);
    //            if (Valor_Retornado == 1)
    //                success = true;

    //        }
    //        catch (Exception ex)
    //        {
    //            string script = ex.ToString();
    //            MostrarMensaje("** Error al Eliminar Oficio **", "info", "Normal");
    //        }
    //        finally
    //        {
    //            if (success)
    //            {
    //                lTransaccion.Commit();
    //                cnn.Close();
    //            }
    //            else
    //            {
    //                lTransaccion.Rollback();
    //                cnn.Close();
    //            }
    //        }
    //    }

    //}

        //Método para eliminar una subclase
        public void Eliminar_Niveles(CEdificios_Niveles_Secciones obJE)
        {
            SqlConnection cnn = new SqlConnection(CConexion.Obtener());
            SqlCommand cmd = new SqlCommand("SP_Eliminar_Niveles", cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@idNivel", obJE.IdNivel);
            cnn.Open();
            cmd.ExecuteNonQuery();
            cnn.Close();

        }

        //Método para eliminar una sección
        public void Eliminar_Secciones(CEdificios_Niveles_Secciones obJE, CEdificios_Niveles_Secciones obJE1)
        {
            SqlConnection cnn = new SqlConnection(CConexion.Obtener());
            SqlCommand cmd = new SqlCommand("SP_Eliminar_Secciones", cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@idSeccion", obJE.IdSeccion);
            cmd.Parameters.AddWithValue("@idEnsu ", obJE1.IdENSU);
            cnn.Open();
            cmd.ExecuteNonQuery();
            cnn.Close();

        }
    }
}