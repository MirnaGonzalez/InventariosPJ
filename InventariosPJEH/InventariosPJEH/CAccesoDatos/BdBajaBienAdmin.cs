using InventariosPJEH.CNegocios;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace InventariosPJEH.CAccesoDatos
{
    public class BdBajaBienAdmin
    {
        public SqlConnection conexion;
        public string error;
        protected SqlDataAdapter adaptador;
        protected SqlDataReader reader;
        protected DataSet data;
        public static List<CBajaBienAdmin> ConsultarGbBajaBienAdmin(string nNumeroInventario)
        {
            List<CBajaBienAdmin> lista = new List<CBajaBienAdmin>();

            SqlConnection cnn = new SqlConnection(CConexion.Obtener());
            try
            {

                SqlCommand cmd = new SqlCommand(string.Format("select IdInventario, NumInventario, DescripcionBien, Marca, Modelo, Serie, FechaAdquisicion, IdActividad, IdENSU, IdUsuario " +
                    "from VistaConsultarBienes where NumInventario like '%{0}%' and IdActividad=1 ORDER BY NumInventario ASC", nNumeroInventario), cnn);


                cnn.Open();

                using (var rd = cmd.ExecuteReader())
                    
                {
                    while (rd.Read())
                    {
                        CBajaBienAdmin cBajaBien = new CBajaBienAdmin();
                        cBajaBien.IdInventario = rd["IdInventario"].ToString();
                        cBajaBien.NumInventario = rd["NumInventario"].ToString();
                        cBajaBien.DescripcionBien = rd["DescripcionBien"].ToString();
                        cBajaBien.Marca = rd["Marca"].ToString();
                        cBajaBien.Modelo = rd["Modelo"].ToString();
                        cBajaBien.Serie = rd["Serie"].ToString();
                        cBajaBien.FechaAdquisicion = rd["FechaAdquisicion"].ToString();
                        cBajaBien.IdActividad = rd["IdActividad"].ToString();
                        cBajaBien.IdENSU = rd["IdENSU"].ToString();
                        cBajaBien.IdUsuario = rd["IdUsuario"].ToString();


                        lista.Add(cBajaBien);
                    }
                }
            }
            catch (Exception )
            {
                cnn.Close();
            }
            return lista;
        }


        public static List<CFichaBienBaja> ConsultarFichaBien(Int64 NumeroInventario, ref TResultado ResFichaBien)
        {
            List<CFichaBienBaja> LFichaBien = new List<CFichaBienBaja>();
            SqlCommand cmd = new SqlCommand();
            SqlDataReader rdr;
            SqlConnection cnn = new SqlConnection(CConexion.Obtener());
            cmd.Connection = cnn;

            cmd.CommandText = " SELECT * ";
            cmd.CommandText += " FROM FichaBien ";
            cmd.CommandText += " WHERE NumInventario = @NumeroInventario ";

            cmd.Parameters.Add("@NumeroInventario", SqlDbType.BigInt).Value = NumeroInventario;

            try
            {
                
                cmd.Connection.Open();
                rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    CFichaBienBaja FichaB = new CFichaBienBaja();
                    FichaB.IdInventario = Convert.ToInt32(rdr["IdInventario"].ToString());
                    FichaB.NumInventario = Convert.ToInt64(rdr["NumInventario"].ToString());
                    FichaB.IdActividad = Convert.ToInt32(rdr["IdActividad"].ToString());
                    FichaB.IdENSU = Convert.ToInt32(rdr["IdENSU"].ToString());

                    LFichaBien.Add(FichaB);
                }
            }
            catch (Exception ex)
            {
                if(cmd.Connection.State == ConnectionState.Open)
                    cmd.Connection.Close();

                ResFichaBien.Exito = false;
                ResFichaBien.Mensaje.Add(ex.Message);
            }
            return LFichaBien;
        }

        public static List<CFichaBienBaja> ConsultarFichaBienPorActividad(int IdActividad, ref TResultado ResFichaBien)
        {
            List<CFichaBienBaja> LFichaBien = new List<CFichaBienBaja>();
            SqlCommand cmd = new SqlCommand();
            SqlDataReader rdr;
            SqlConnection cnn = new SqlConnection(CConexion.Obtener());
            cmd.Connection = cnn;

            cmd.CommandText = " SELECT IdInventario,NumInventario,DescripcionBien,CM.Descripcion AS Marca,Modelo,Serie,FC.FechaAdquisicion ";
            cmd.CommandText += " FROM FichaBien FB ";
            cmd.CommandText += " INNER JOIN Cat_Marca CM ON CM.IdMarca = FB.IdMarca ";
            cmd.CommandText += " INNER JOIN FichaCompra FC ON FC.IdCompra = FB.IdCompra ";
            cmd.CommandText += " WHERE FB.IdActividad = @IdActividad ";

            cmd.Parameters.Add("@IdActividad", SqlDbType.Int).Value = IdActividad;

            try
            {

                cmd.Connection.Open();
                rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    CFichaBienBaja Ficha = new CFichaBienBaja();
                    Ficha.IdInventario = Convert.ToInt32(rdr["IdInventario"].ToString());
                    Ficha.NumInventario = Convert.ToInt64(rdr["NumInventario"].ToString());
                    Ficha.DescripcionBien =rdr["DescripcionBien"].ToString();
                    Ficha.Marca = rdr["Marca"].ToString();
                    Ficha.Modelo = rdr["Modelo"].ToString();
                    Ficha.Serie = rdr["Serie"].ToString();
                    Ficha.FechaAdquisicion = Convert.ToDateTime(rdr["FechaAdquisicion"].ToString());

                    LFichaBien.Add(Ficha);
                }
            }
            catch (Exception ex)
            {
                if (cmd.Connection.State == ConnectionState.Open)
                    cmd.Connection.Close();

                ResFichaBien.Exito = false;
                ResFichaBien.Mensaje.Add(ex.Message);
            }
            return LFichaBien;
        }

        public static List<CFichaBienBaja> ConsultarFichaBienPorTipoBaja(string TipoBaja, int IdActividad, ref TResultado ResFichaBien)
        {
            List<CFichaBienBaja> LFichaBien = new List<CFichaBienBaja>();
            SqlCommand cmd = new SqlCommand();
            SqlDataReader rdr;
            SqlConnection cnn = new SqlConnection(CConexion.Obtener());
            cmd.Connection = cnn;

            cmd.CommandText = " SELECT FB.IdInventario,NumInventario,DescripcionBien,IdActividad, CM.Descripcion AS Marca,Modelo,Serie,FC.FechaAdquisicion, BB.Clasificacion, FB.IdENSU, CASE WHEN GR.IdResguardo IS NULL THEN 0 ELSE GR.IdResguardo END AS IdResguardo, FB.TipoPartida  ";
            cmd.CommandText += " FROM FichaBien FB  ";
            cmd.CommandText += " INNER JOIN Cat_Marca CM ON CM.IdMarca = FB.IdMarca ";
            cmd.CommandText += " INNER JOIN FichaCompra FC ON FC.IdCompra = FB.IdCompra ";
            cmd.CommandText += " INNER JOIN BajaBien BB ON BB.IdInventario = FB.IdInventario";
            cmd.CommandText += " LEFT JOIN GrupoBienResguardo GR ON GR.IdENSU = FB.IdENSU";
            cmd.CommandText += " WHERE BB.TipoBaja=@TipoBaja and FB.IdActividad=@IdActividad ";


            cmd.Parameters.Add("@TipoBaja", SqlDbType.NChar).Value = TipoBaja;
            cmd.Parameters.Add("@IdActividad", SqlDbType.Int).Value = IdActividad;
            try
            {

                cmd.Connection.Open();
                rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    CFichaBienBaja Ficha = new CFichaBienBaja();
                    Ficha.IdInventario = Convert.ToInt32(rdr["IdInventario"].ToString());
                    Ficha.NumInventario = Convert.ToInt64(rdr["NumInventario"].ToString());
                    Ficha.DescripcionBien = rdr["DescripcionBien"].ToString();
                    //Ficha.DescripcionBien = rdr["IdActividad"].ToString();
                    Ficha.Marca = rdr["Marca"].ToString();
                    Ficha.Modelo = rdr["Modelo"].ToString();
                    Ficha.Serie = rdr["Serie"].ToString();
                    Ficha.FechaAdquisicion = Convert.ToDateTime(rdr["FechaAdquisicion"].ToString());
                    Ficha.Clasificacion = rdr["Clasificacion"].ToString();
                    Ficha.IdENSU = Convert.ToInt32(rdr["IdENSU"].ToString());
                    Ficha.IdResguardo = Convert.ToInt32(rdr["IdResguardo"].ToString());
                    Ficha.TipoPartida = Convert.ToInt32(rdr["TipoPartida"].ToString());

                    LFichaBien.Add(Ficha);
                }
            }
            catch (Exception ex)
            {
                if (cmd.Connection.State == ConnectionState.Open)
                    cmd.Connection.Close();

                ResFichaBien.Exito = false;
                ResFichaBien.Mensaje.Add(ex.Message);
            }
            return LFichaBien;
        }

        public static DataTable ConsultarFichaBienPorTipoBajaDT(string TipoBaja, ref TResultado ResFichaBien)
        {
            //List<CFichaBienBaja> LFichaBien = new List<CFichaBienBaja>();
            DataTable DatosFichaB = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter();

            SqlCommand cmd = new SqlCommand();
          //  SqlDataReader rdr;
            SqlConnection cnn = new SqlConnection(CConexion.Obtener());
            cmd.Connection = cnn;

            cmd.CommandText = " SELECT FB.IdInventario,NumInventario,DescripcionBien,CM.Descripcion AS Marca,Modelo,Serie,FC.FechaAdquisicion, BB.Clasificacion, FB.IdENSU, CASE WHEN GR.IdResguardo IS NULL THEN 0 ELSE GR.IdResguardo END AS IdResguardo  ";
            cmd.CommandText += " FROM FichaBien FB  ";
            cmd.CommandText += " INNER JOIN Cat_Marca CM ON CM.IdMarca = FB.IdMarca ";
            cmd.CommandText += " INNER JOIN FichaCompra FC ON FC.IdCompra = FB.IdCompra ";
            cmd.CommandText += " INNER JOIN BajaBien BB ON BB.IdInventario = FB.IdInventario";
            cmd.CommandText += " LEFT JOIN GrupoBienResguardo GR ON GR.IdENSU = FB.IdENSU";
            cmd.CommandText += " WHERE BB.TipoBaja=@TipoBaja ";

            cmd.Parameters.Add("@TipoBaja", SqlDbType.NChar).Value = TipoBaja;

            sda.SelectCommand = cmd;
            try
            {

                cmd.Connection.Open();
                sda.Fill(DatosFichaB);
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                if (cmd.Connection.State == ConnectionState.Open)
                    cmd.Connection.Close();

                ResFichaBien.Exito = false;
                ResFichaBien.Mensaje.Add(ex.Message);
            }
            return DatosFichaB;
        }
        public static List<CGrupoResguardo> ConsultarGrupoResguardo(int IdResguardo, ref TResultado ResGrupoResguardo)
        {
            List<CGrupoResguardo> LGrupoResguardo = new List<CGrupoResguardo>();
            SqlCommand cmd = new SqlCommand();
            SqlDataReader rdr;
            SqlConnection cnn = new SqlConnection(CConexion.Obtener());
            cmd.Connection = cnn;

            cmd.CommandText = " SELECT R.IdResguardo, R.IdEmpleado, CP.Nombre + CP.APaterno + CP.AMaterno AS NombreCompleto, GR.Grupo, GR.IdENSU, CE.Edificio, CN.Nivel, CS.Nombre AS Seccion ";
            cmd.CommandText += " FROM Resguardo R ";
            cmd.CommandText += " INNER JOIN Cat_Personal CP ON CP.IdEmpleado = R.IdEmpleado ";
            cmd.CommandText += " LEFT JOIN GrupoBienResguardo GR ON GR.IdResguardo = R.IdResguardo ";
            cmd.CommandText += " INNER JOIN Cat_ENSUbicacion CU ON GR.IdENSU = CU.IdENSU ";
            cmd.CommandText += " INNER JOIN Cat_Edificio CE ON CE.IdEdificio = CU.IdEdificio ";
            cmd.CommandText += " INNER JOIN Cat_Niveles CN ON CN.IdNivel = CU.IdNivel ";
            cmd.CommandText += " INNER JOIN Cat_Secciones CS ON CS.IdSeccion = CU.IdSeccion ";
            cmd.CommandText += " WHERE R.IdResguardo = @IdResguardo ";


            cmd.Parameters.Add("@IdResguardo", SqlDbType.Int).Value = IdResguardo;
            try
            {

                cmd.Connection.Open();
                rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    CGrupoResguardo GrupoResguardo = new CGrupoResguardo();
                    GrupoResguardo.IdResguardo = Convert.ToInt32(rdr["IdResguardo"].ToString());
                    GrupoResguardo.IdEmpleado = Convert.ToInt32(rdr["IdEmpleado"].ToString());
                    GrupoResguardo.NombreCompleto = Convert.ToString(rdr["NombreCompleto"].ToString());
                    GrupoResguardo.Grupo = Convert.ToString(rdr["Grupo"].ToString());
                    GrupoResguardo.IdENSU = Convert.ToInt32(rdr["IdENSU"].ToString());
                    GrupoResguardo.Edificio = rdr["Edificio"].ToString();
                    GrupoResguardo.Nivel = Convert.ToString(rdr["Nivel"].ToString());
                    GrupoResguardo.Seccion = Convert.ToString(rdr["Seccion"].ToString());

                    LGrupoResguardo.Add(GrupoResguardo);
                }
            }
            catch (Exception ex)
            {
                if (cmd.Connection.State == ConnectionState.Open)
                    cmd.Connection.Close();

                ResGrupoResguardo.Exito = false;
                ResGrupoResguardo.Mensaje.Add(ex.Message);
            }
            return LGrupoResguardo;
        }

        public static int InsertarHistoricoUbicacion(CUbicacion HistoricoUbi, ref TResultado ResAgragarMantenimiento)
        {
            int IdHistoricoUbicacion = -1;
            SqlCommand insert = new SqlCommand("[dbo].[SP_BajaBienes]", new SqlConnection(CConexion.Obtener()));
            insert.CommandType = CommandType.StoredProcedure;

            insert.Parameters.AddWithValue("@IdInventario", HistoricoUbi.IdInventario);
            insert.Parameters.AddWithValue("@IdUsuario", HistoricoUbi.IdUsuario);
            insert.Parameters.AddWithValue("@IdActividad", HistoricoUbi.IdActividad);
            insert.Parameters.AddWithValue("@Estatus", HistoricoUbi.Estatus);

            try
            {
                insert.Connection.Open();
                object Resultado = insert.ExecuteScalar();
                int.TryParse(Resultado.ToString(), out IdHistoricoUbicacion);
                insert.Connection.Close();

                return IdHistoricoUbicacion;
            }
            catch (Exception ex)
            {
                if (insert.Connection.State == ConnectionState.Open)
                    insert.Connection.Close();
                ResAgragarMantenimiento.Exito = false;
                ResAgragarMantenimiento.Mensaje.Add(ex.Message);

                return IdHistoricoUbicacion;
            }
        }

        public static int ModificarUbicacion(int IdInventario, int IdActividad, DateTime Fecha, ref TResultado Resultado)
        {
            int RegModificados = -1;

            SqlCommand cmd = new SqlCommand("[dbo].[stp_Ubicacion_Modificar]", new SqlConnection(CConexion.Obtener()));
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@IdInventario", SqlDbType.Int).Value = IdInventario;
            cmd.Parameters.Add("@IdActividad", SqlDbType.Int).Value = IdActividad;
            cmd.Parameters.Add("@Fecha", SqlDbType.DateTime).Value = Fecha;

            try
            {
                cmd.Connection.Open();
                object res = cmd.ExecuteScalar();
                cmd.Connection.Close();
                int.TryParse(res.ToString(), out RegModificados);

                return RegModificados;
            }
            catch (Exception e)
            {
                if (cmd.Connection.State != ConnectionState.Closed)
                    cmd.Connection.Close();
                Resultado.Exito = false;
                Resultado.Mensaje.Add("Se generó un error al tratar de Modificar la ubicaciòn.");
                Resultado.Detalles = e.Message;
                return -1;
            }
        }

        public static int ModificarFichaBien(Int64 NumInventario, int IdActividad, ref TResultado Resultado)
        {
            int RegModificados = -1;
            
            SqlCommand cmd = new SqlCommand("[dbo].[stp_FichaBien_Modificar]", new SqlConnection(CConexion.Obtener()));
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@NumInventario", SqlDbType.BigInt).Value = NumInventario;
            cmd.Parameters.Add("@IdActividad", SqlDbType.Int).Value = IdActividad;

            try
            {
                cmd.Connection.Open();
                object res = cmd.ExecuteScalar();
                cmd.Connection.Close();

                int.TryParse(res.ToString(), out RegModificados);

                return RegModificados;
            }
            catch (Exception e)
            {
                if (cmd.Connection.State != ConnectionState.Closed)
                    cmd.Connection.Close();
                Resultado.Exito = false;
                Resultado.Mensaje.Add("Se generó un error al tratar de Modificar la actividad de FichaBien.");
                Resultado.Detalles = e.Message;
                return -1;
            }
        }

        public static int ModificarBajaBien(int IdInventario, string  TipoBaja, string Clasificacion, ref TResultado Resultado)
        {
            int RegModificados = -1;

            SqlCommand cmd = new SqlCommand("[dbo].[stp_BajaBien_Modificar]", new SqlConnection(CConexion.Obtener()));
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@IdInventario", SqlDbType.Int).Value = IdInventario;
            cmd.Parameters.Add("@TipoBaja", SqlDbType.NChar,2).Value = TipoBaja;
            
            if (!string.IsNullOrEmpty(Clasificacion))
                cmd.Parameters.Add("@Clasificacion", SqlDbType.VarChar, 10).Value = Clasificacion;

            try
            {
                cmd.Connection.Open();
                object res = cmd.ExecuteScalar();
                cmd.Connection.Close();

                int.TryParse(res.ToString(), out RegModificados);

                return RegModificados;
            }
            catch (Exception e)
            {
                if (cmd.Connection.State != ConnectionState.Closed)
                    cmd.Connection.Close();
                Resultado.Exito = false;
                Resultado.Mensaje.Add("Se generó un error al tratar de Modificar Baja Bien");
                Resultado.Detalles = e.Message;
                return -1;
            }
        }

        public static int EliminarGrupoResguardo(int IdENSU, ref TResultado Resultado)
        {
            int RegModificados = -1;

            SqlCommand cmd = new SqlCommand("[dbo].[SP_GrupoBienResguardo_Eliminar]", new SqlConnection(CConexion.Obtener()));
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@IdENSU", SqlDbType.Int).Value = IdENSU;

            try
            {
                cmd.Connection.Open();
                object res = cmd.ExecuteScalar();
                cmd.Connection.Close();

                int.TryParse(res.ToString(), out RegModificados);

                return RegModificados;
            }
            catch (Exception e)
            {
                if (cmd.Connection.State != ConnectionState.Closed)
                    cmd.Connection.Close();
                Resultado.Exito = false;
                Resultado.Mensaje.Add("Se generó un error al tratar de Eliminar Resguardo.");
                Resultado.Detalles = e.Message;
                return -1;
            }
        }

        public static int BajaTemporalBien(CUbicacion BTBien, ref TResultado ResAgragarMantenimiento)
        {
            int IdHistoricoUbicacion = -1;
            SqlCommand insert = new SqlCommand("[dbo].[SP_BajaTemporalBien]", new SqlConnection(CConexion.Obtener()));
            insert.CommandType = CommandType.StoredProcedure;

            insert.Parameters.AddWithValue("@IdInventario", BTBien.IdInventario);
            insert.Parameters.AddWithValue("@IdUsuario", BTBien.IdUsuario);
            insert.Parameters.AddWithValue("@IdActividad", BTBien.IdActividad);
            insert.Parameters.AddWithValue("@IdENSU", BTBien.IdENSU);
            insert.Parameters.AddWithValue("@TipoPartida", BTBien.TipoPartida);
            insert.Parameters.AddWithValue("@IdEmpleado", BTBien.IdEmpleado);
            insert.Parameters.AddWithValue("@Grupo", BTBien.Grupo);
            insert.Parameters.AddWithValue("@FechaResguardo", BTBien.FechaResguardo);


            try
            {
                insert.Connection.Open();
                object Resultado = insert.ExecuteScalar();
                int.TryParse(Resultado.ToString(), out IdHistoricoUbicacion);
                insert.Connection.Close();

                return IdHistoricoUbicacion;
            }
            catch (Exception ex)
            {
                if (insert.Connection.State == ConnectionState.Open)
                    insert.Connection.Close();
                ResAgragarMantenimiento.Exito = false;
                ResAgragarMantenimiento.Mensaje.Add(ex.Message);

                return IdHistoricoUbicacion;
            }
        }

        public static int BajaDefinitivaBien(CUbicacion BTBien, ref TResultado ResAgragarMantenimiento)
        {
            int IdHistoricoUbicacion = -1;
            SqlCommand insert = new SqlCommand("[dbo].[SP_BajaDefinitivaBien]", new SqlConnection(CConexion.Obtener()));
            insert.CommandType = CommandType.StoredProcedure;

            insert.Parameters.AddWithValue("@IdInventario", BTBien.IdInventario);
            insert.Parameters.AddWithValue("@IdUsuario", BTBien.IdUsuario);
            insert.Parameters.AddWithValue("@IdActividad", BTBien.IdActividad);
            insert.Parameters.AddWithValue("@OficioBien", BTBien.OficioBien);

            try
            {
                insert.Connection.Open();
                object Resultado = insert.ExecuteScalar();
                int.TryParse(Resultado.ToString(), out IdHistoricoUbicacion);
                insert.Connection.Close();

                return IdHistoricoUbicacion;
            }
            catch (Exception ex)
            {
                if (insert.Connection.State == ConnectionState.Open)
                    insert.Connection.Close();
                ResAgragarMantenimiento.Exito = false;
                ResAgragarMantenimiento.Mensaje.Add(ex.Message);

                return IdHistoricoUbicacion;
            }
        }
    }
}