using InventariosPJEH.CNegocios;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace InventariosPJEH.CAccesoDatos
{
    public class BdAsignarResguardo
    {
        public static List<CConsultaBienes> ObtenerPartida(int TipoPartida)
        {
            List<CConsultaBienes> listaPartida = new List<CConsultaBienes>();

            SqlConnection cnn = new SqlConnection(CConexion.Obtener());
            SqlCommand cmd = new SqlCommand("select Partida,idPartida from Cat_Partidas where TipoPartida = @TipoPartida", cnn);
            cmd.Parameters.Add("@TipoPartida", SqlDbType.Int).Value = TipoPartida;
            cnn.Open();



            using (var rd = cmd.ExecuteReader())
            {


                while (rd.Read())
                {
                    CConsultaBienes cRegistroDeUnBien = new CConsultaBienes();
                    cRegistroDeUnBien.Partida = rd.GetString(0);
                    cRegistroDeUnBien.idPartida = rd.GetInt32(1);
                    listaPartida.Add(cRegistroDeUnBien);
                }
            }
            return listaPartida;
        }

        public static DataTable ObtenerEdificio()
        {
            SqlCommand cmd = new SqlCommand();
            DataTable Tabla = new DataTable("Resultado");
            SqlConnection cnn = new SqlConnection(CConexion.Obtener());
            SqlDataAdapter adp = new SqlDataAdapter();
            cmd.CommandText = "SELECT IdEdificio, Edificio ";
            cmd.CommandText += "FROM Cat_Edificio ";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = cnn;
            adp.SelectCommand = cmd;
            cnn.Open();
            adp.Fill(Tabla);
            cnn.Close();
            return Tabla;
        }

        public static DataTable ObtenerDistritosXTipoUnidad(string Tipo)
        {
            SqlCommand cmd = new SqlCommand();
            DataTable Tabla = new DataTable("Resultado");
            SqlConnection cnn = new SqlConnection(CConexion.Obtener());
            SqlDataAdapter adp = new SqlDataAdapter();
            cmd.CommandText = "SELECT DISTINCT(D.IdDistrito), D.Distrito ";
            cmd.CommandText += "FROM dbo.Cat_UniAdmin	UA ";
            cmd.CommandText += "INNER JOIN  dbo.Cat_UADistrito	UAD ON  UA.IdUniAdmin = UAD.IdUniAdmin ";
            cmd.CommandText += "INNER JOIN dbo.Cat_Distritos D ON UAD.IdDistrito = D.IdDistrito ";
            cmd.CommandText += "WHERE UA.Clasificacion = @Tipo";
            cmd.Parameters.Add("@Tipo", SqlDbType.VarChar, 2).Value = Tipo;
            cmd.CommandType = CommandType.Text;
            cmd.Connection = cnn;
            adp.SelectCommand = cmd;
            cnn.Open();
            adp.Fill(Tabla);
            cnn.Close();
            return Tabla;
        }

        public static DataTable ObtenerUnidAdminXDistritoXClasificacion(int IdDistrito, string Clasificacion)
        {
            SqlCommand cmd = new SqlCommand();
            DataTable Tabla = new DataTable("Resultado");
            SqlConnection cnn = new SqlConnection(CConexion.Obtener());
            SqlDataAdapter adp = new SqlDataAdapter();
            cmd.CommandText = "SELECT UA.IdUniAdmin, UA.UniAdmin ";
            cmd.CommandText += "FROM dbo.Cat_UniAdmin	UA ";
            cmd.CommandText += "INNER JOIN  dbo.Cat_UADistrito	UAD ON  UA.IdUniAdmin = UAD.IdUniAdmin ";
            cmd.CommandText += "WHERE UAD.IdDistrito = @IdDistrito and UA.Clasificacion = @Clasificacion ";
            cmd.Parameters.Add("@IdDistrito", SqlDbType.Int).Value = IdDistrito;
            cmd.Parameters.Add("@Clasificacion", SqlDbType.VarChar, 2).Value = Clasificacion;
            cmd.CommandType = CommandType.Text;
            cmd.Connection = cnn;
            adp.SelectCommand = cmd;
            cnn.Open();
            adp.Fill(Tabla);
            cnn.Close();
            return Tabla;
        }

        public static DataTable ObtenerUnidAdminXClasificacion(string Clasificacion)
        {
            SqlCommand cmd = new SqlCommand();
            DataTable Tabla = new DataTable("Resultado");
            SqlConnection cnn = new SqlConnection(CConexion.Obtener());
            SqlDataAdapter adp = new SqlDataAdapter();
            cmd.CommandText = "SELECT IdUniAdmin, UniAdmin ";
            cmd.CommandText += "FROM dbo.Cat_UniAdmin ";
            cmd.CommandText += "WHERE Clasificacion = @Clasificacion ";
            cmd.Parameters.Add("@Clasificacion", SqlDbType.VarChar, 2).Value = Clasificacion;
            cmd.CommandType = CommandType.Text;
            cmd.Connection = cnn;
            adp.SelectCommand = cmd;
            cnn.Open();
            adp.Fill(Tabla);
            cnn.Close();
            return Tabla;
        }

        public static DataTable ObtenerPersonalXUnidAdmin(int IdUniAdmin)
        {
            SqlCommand cmd = new SqlCommand();
            DataTable Tabla = new DataTable("Resultado");
            SqlConnection cnn = new SqlConnection(CConexion.Obtener());
            SqlDataAdapter adp = new SqlDataAdapter();
            cmd.CommandText = "SELECT IdEmpleado, Nombre + APaterno + AMaterno AS NombrePersonal ";
            cmd.CommandText += "FROM dbo.Cat_Personal ";
            cmd.CommandText += "WHERE IdUniAdmin = @IdUniAdmin ";
            cmd.Parameters.Add("@IdUniAdmin", SqlDbType.VarChar, 2).Value = IdUniAdmin;
            cmd.CommandType = CommandType.Text;
            cmd.Connection = cnn;
            adp.SelectCommand = cmd;
            cnn.Open();
            adp.Fill(Tabla);
            cnn.Close();
            return Tabla;
        }

        public static DataTable ObtenerSubClasePartida(int IdPartida)
        {
            SqlCommand cmd = new SqlCommand();
            DataTable Tabla = new DataTable("Resultado");
            SqlConnection cnn = new SqlConnection(CConexion.Obtener());
            SqlDataAdapter adp = new SqlDataAdapter();
            cmd.CommandText = "select IdSubClase, SubClase, IdPartida from Cat_SubClase where IdPartida =" + IdPartida;
            cmd.CommandType = CommandType.Text;
            cmd.Connection = cnn;
            adp.SelectCommand = cmd;
            cnn.Open();
            adp.Fill(Tabla);
            cnn.Close();
            return Tabla;
        }

        public static List<CFichaBien> ObtenerFichaBienXNumInventario(Int64 NumInv, ref TResultado Resultado)
        {
            List<CFichaBien> LFichaBien = new List<CFichaBien>();

            SqlConnection cnn = new SqlConnection(CConexion.Obtener());
            SqlCommand cmd = new SqlCommand();
            SqlDataReader rd;
            cmd.Connection = cnn;
            cmd.CommandText = "select IdInventario, NumInventario, DescripcionBien from FichaBien where NumInventario = @NumInv AND IdActividad in (1)";
            cmd.Parameters.Add("@NumInv", SqlDbType.BigInt).Value = NumInv;
            try
            {
                cnn.Open();
                rd = cmd.ExecuteReader();

                while (rd.Read())
                {
                    CFichaBien FichaBien = new CFichaBien();

                    FichaBien.IdInventario = BdConverter.FieldToInt(rd["IdInventario"]);
                    FichaBien.NumInventario = BdConverter.FieldToInt64(rd["NumInventario"]);
                    FichaBien.DescripcionBien = BdConverter.FieldToString(rd["DescripcionBien"]);
                    LFichaBien.Add(FichaBien);
                }

                cmd.Connection.Close();

            }
            catch (Exception e)
            {
                if (cmd.Connection.State != ConnectionState.Closed)
                    cmd.Connection.Close();
                Resultado.Exito = false;
                Resultado.Mensaje.Add("Se generó un error al tratar de obtener fichabien por Numero de Inventario.");
                Resultado.Detalles = e.Message;
                //return -1;
            }

            return LFichaBien;
        }

        public static List<CFichaBien> ObtenerFichaBienXSubClase(int IdSubClase)
        {
            List<CFichaBien> LFichaBien = new List<CFichaBien>();

            SqlConnection cnn = new SqlConnection(CConexion.Obtener());
            SqlCommand cmd = new SqlCommand();
            SqlDataReader rd;
            cmd.Connection = cnn;
            cmd.CommandText = "select IdInventario, NumInventario, DescripcionBien from FichaBien where IdSubClase = @IdSubClase AND IdActividad in (1)";
            cmd.Parameters.Add("@IdSubClase", SqlDbType.BigInt).Value = IdSubClase;

            try
            {
                cnn.Open();
                rd = cmd.ExecuteReader();


                while (rd.Read())
                {
                    CFichaBien FichaBien = new CFichaBien();

                    FichaBien.IdInventario = BdConverter.FieldToInt(rd["IdInventario"]);
                    FichaBien.NumInventario = BdConverter.FieldToInt64(rd["NumInventario"]);
                    FichaBien.DescripcionBien = BdConverter.FieldToString(rd["DescripcionBien"]);
                    LFichaBien.Add(FichaBien);
                }
                cmd.Connection.Close();

            }
            catch (Exception)
            {
                if (cmd.Connection.State != ConnectionState.Closed)
                    cmd.Connection.Close();
                //return -1;
            }

            return LFichaBien;
        }

        public static List<CFichaBien> ObtenerFichaBienXAreaResguardo(int IdSeccion, ref TResultado Resultado)
        {
            List<CFichaBien> LFichaBien = new List<CFichaBien>();

            SqlConnection cnn = new SqlConnection(CConexion.Obtener());
            SqlCommand cmd = new SqlCommand();
            SqlDataReader rd;
            cmd.Connection = cnn;
            cmd.CommandText = "select NumInventario, DescripcionBien from FichaBien where IdSubClase = @IdSubClase AND IdActividad in (1)";
            cmd.Parameters.Add("@IdSubClase", SqlDbType.BigInt).Value = IdSeccion;
            try
            {
                cnn.Open();
                rd = cmd.ExecuteReader();

                while (rd.Read())
                {
                    CFichaBien FichaBien = new CFichaBien();

                    FichaBien.NumInventario = BdConverter.FieldToInt64(rd["NumInventario"]);
                    FichaBien.DescripcionBien = BdConverter.FieldToString(rd["DescripcionBien"]);
                    LFichaBien.Add(FichaBien);
                }

                cmd.Connection.Close();

            }
            catch (Exception e)
            {
                if (cmd.Connection.State != ConnectionState.Closed)
                    cmd.Connection.Close();
                Resultado.Exito = false;
                Resultado.Mensaje.Add("Se generó un error al tratar de obtener fichabien por area resguardo");
                Resultado.Detalles = e.Message;
                //return -1;
            }

            return LFichaBien;
        }

        public static List<CFichaBien> ObtenerFichaBienXIdEmpleado(int IdEmpleado, ref TResultado Resultado)
        {
            List<CFichaBien> LFichaBien = new List<CFichaBien>();

            SqlConnection cnn = new SqlConnection(CConexion.Obtener());
            SqlCommand cmd = new SqlCommand();
            SqlDataReader rd;
            cmd.Connection = cnn;
            cmd.CommandText = " SELECT     dbo.Cat_Personal.IdEmpleado, dbo.GrupoBienResguardo.IdGrupo, dbo.Resguardo.IdResguardo, dbo.Cat_ENSUbicacion.IdENSU, dbo.FichaBien.IdInventario, dbo.FichaBien.NumInventario, dbo.FichaBien.DescripcionBien, dbo.FichaBien.Modelo, dbo.FichaBien.Serie, dbo.FichaBien.IdActividad  ";
            cmd.CommandText += " FROM dbo.Cat_ENSUbicacion ";
            cmd.CommandText += " INNER JOIN dbo.GrupoBienResguardo ON dbo.Cat_ENSUbicacion.IdENSU = dbo.GrupoBienResguardo.IdENSU ";
            cmd.CommandText += " INNER JOIN dbo.FichaBien ON dbo.Cat_ENSUbicacion.IdENSU = dbo.FichaBien.IdENSU ";
            cmd.CommandText += " INNER JOIN dbo.Cat_Personal ON dbo.Cat_Personal.IdEmpleado = dbo.GrupoBienResguardo.IdEmpleado ";
            cmd.CommandText += " INNER JOIN dbo.Resguardo ON dbo.Cat_Personal.IdEmpleado = dbo.Resguardo.IdEmpleado ";
            cmd.CommandText += " WHERE (dbo.Cat_Personal.IdEmpleado = @IdEmpleado) AND dbo.FichaBien.IdActividad not in (1,6) ";

            //cmd.CommandText = "select NumInventario, DescripcionBien from FichaBien where IdEmpleado = @IdEmpleado AND IdActividad not in (1,6)";
            cmd.Parameters.Add("@IdEmpleado", SqlDbType.BigInt).Value = IdEmpleado;
            try
            {
                cnn.Open();
                rd = cmd.ExecuteReader();

                while (rd.Read())
                {
                    CFichaBien FichaBien = new CFichaBien();

                    FichaBien.NumInventario = BdConverter.FieldToInt64(rd["NumInventario"]);
                    FichaBien.DescripcionBien = BdConverter.FieldToString(rd["DescripcionBien"]);
                    FichaBien.IdActividad = BdConverter.FieldToInt(rd["IdActividad"]);
                    FichaBien.IdGrupo = BdConverter.FieldToInt(rd["IdGrupo"]);
                    FichaBien.IdInventario = BdConverter.FieldToInt(rd["IdInventario"]);
                    LFichaBien.Add(FichaBien);
                }

                cmd.Connection.Close();

            }
            catch (Exception e)
            {
                if (cmd.Connection.State != ConnectionState.Closed)
                    cmd.Connection.Close();
                Resultado.Exito = false;
                Resultado.Mensaje.Add("Se generó un error al tratar de Agregar UbicacionENSU.");
                Resultado.Detalles = e.Message;
                //return -1;
            }

            return LFichaBien;
        }

        public static List<CResguardo> ObtenerResguardoXIdEmpleado(int IdEmpleado)
        {
            List<CResguardo> LResguardo = new List<CResguardo>();

            SqlConnection cnn = new SqlConnection(CConexion.Obtener());
            SqlCommand cmd = new SqlCommand();
            SqlDataReader rd;
            cmd.Connection = cnn;
            cmd.CommandText = " SELECT * ";
            cmd.CommandText += " FROM dbo.VtaReporteResguardo ";
            cmd.CommandText += " WHERE (IdEmpleado = @IdEmpleado) AND IdActividad not in (1,6) ";

            //cmd.CommandText = "select NumInventario, DescripcionBien from FichaBien where IdEmpleado = @IdEmpleado AND IdActividad not in (1,6)";
            cmd.Parameters.Add("@IdEmpleado", SqlDbType.BigInt).Value = IdEmpleado;
            try
            {

                cnn.Open();
                rd = cmd.ExecuteReader();

                while (rd.Read())
                {
                    CResguardo Resguardo = new CResguardo();

                    Resguardo.IdResguardo = BdConverter.FieldToInt(rd["IdResguardo"]);
                    Resguardo.IdEmpleado = BdConverter.FieldToInt(rd["IdEmpleado"]);
                    Resguardo.NombreEmpleado = BdConverter.FieldToString(rd["NombreEmpleado"]);
                    Resguardo.Adscripcion = BdConverter.FieldToString(rd["Adscripcion"]);
                    Resguardo.Domicilio = BdConverter.FieldToString(rd["Domicilio"]);
                    Resguardo.FechaResguardo = BdConverter.FieldToDate(rd["FechaResguardo"]);
                    Resguardo.NombreGrupo = BdConverter.FieldToString(rd["NombreGrupo"]);
                    Resguardo.IdInventario = BdConverter.FieldToInt(rd["IdInventario"]);
                    Resguardo.NumInventario = BdConverter.FieldToInt64(rd["NumInventario"]);
                    Resguardo.DescripcionBien = BdConverter.FieldToString(rd["DescripcionBien"]);
                    Resguardo.Marca = BdConverter.FieldToString(rd["Marca"]);
                    Resguardo.Modelo = BdConverter.FieldToString(rd["Modelo"]);
                    Resguardo.Serie = BdConverter.FieldToString(rd["Serie"]);
                    Resguardo.Propiedad = BdConverter.FieldToString(rd["Propiedad"]);
                    Resguardo.CostoTotal = BdConverter.FieldToFloat(rd["CostoTotal"]);
                    Resguardo.Factura = BdConverter.FieldToString(rd["Factura"]);
                    Resguardo.IdGrupo = BdConverter.FieldToInt(rd["IdGrupo"]);
                    Resguardo.IdENSU = BdConverter.FieldToInt(rd["IdENSU"]);
                    Resguardo.IdActividad = BdConverter.FieldToInt(rd["IdActividad"]);
                    LResguardo.Add(Resguardo);
                }
                cmd.Connection.Close();

            }
            catch (Exception )
            {
                if (cmd.Connection.State != ConnectionState.Closed)
                    cmd.Connection.Close();
                //Resultado.Exito = false;
                //Resultado.Mensaje.Add("Se generó un error al tratar de obterner el numero de resguardo .");
                //Resultado.Detalles = e.Message;
                //return -1;
            }

            return LResguardo;
        }

        public static CGrupoBienResguardo AgregarUbicacionENSU(int IdEdificio, int IdNivel, int IdSeccion, int IdEmpleado, int TipoPartida, ref TResultado Resultado)
        {
            //int IdAsignado = -1;
            CGrupoBienResguardo GrupoBienR = new CGrupoBienResguardo();
            //object ResProcedimiento = new object();
            SqlDataReader rd;
            SqlCommand cmd = new SqlCommand("[dbo].[stp_ENSUbicacion_Agregar]", new SqlConnection(CConexion.Obtener()));
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@IdEdificio", SqlDbType.Int).Value = IdEdificio;
            cmd.Parameters.Add("@IdNivel", SqlDbType.Int).Value = IdNivel;
            cmd.Parameters.Add("@IdSeccion", SqlDbType.Int).Value = IdSeccion;
            cmd.Parameters.Add("@IdEmpleado", SqlDbType.Int).Value = IdEmpleado;
            cmd.Parameters.Add("@TipoPartida", SqlDbType.Int).Value = TipoPartida;
            try
            {
                cmd.Connection.Open();
                rd = cmd.ExecuteReader();

                while (rd.Read())
                {
                    GrupoBienR.IdENSU = BdConverter.FieldToInt(rd["IdENSU"]);
                    GrupoBienR.IdResguardo = BdConverter.FieldToInt(rd["IdResguardo"]);
                }

                cmd.Connection.Open();

                //cmd.Connection.Open();
                //ResProcedimiento = cmd.ExecuteScalar();
                //cmd.Connection.Close();
                //int.TryParse(ResProcedimiento.ToString(), out IdAsignado);
                //return IdAsignado;


            }
            catch (Exception e)
            {
                if (cmd.Connection.State != ConnectionState.Closed)
                    cmd.Connection.Close();
                Resultado.Exito = false;
                Resultado.Mensaje.Add("Se generó un error al tratar de Agregar UbicacionENSU.");
                Resultado.Detalles = e.Message;
                //return -1;
            }
            return GrupoBienR;
        }

        public static int ModificarFichaBien(Int64 NumInventario, int IdActividad, ref TResultado Resultado)
        {
            int RegModificados = -1;
            //CGrupoBienResguardo GrupoBienR = new CGrupoBienResguardo();
            //object ResProcedimiento = new object();
            //SqlDataReader rd;
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

        public static int ModificarEstatusInvPersonal(int IdEmpleado, string EstatusInv, ref TResultado Resultado)
        {
            int RegModificados = -1;
            //CGrupoBienResguardo GrupoBienR = new CGrupoBienResguardo();
            //object ResProcedimiento = new object();
            //SqlDataReader rd;
            SqlCommand cmd = new SqlCommand("[dbo].[stp_Cat_Personal_Modificar]", new SqlConnection(CConexion.Obtener()));
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@EstatusInv", SqlDbType.NVarChar, 2).Value = EstatusInv;
            cmd.Parameters.Add("@IdEmpleado", SqlDbType.Int).Value = IdEmpleado;

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

        public static int AsignarFichaBien(int IdInventario, int IdENSU, int IdUsuario, int IdActividad, ref TResultado Resultado)
        {
            int RegModificados = -1;
            SqlCommand cmd = new SqlCommand("[dbo].[stp_FichaBien_Asignar]", new SqlConnection(CConexion.Obtener()));
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@IdInventario", SqlDbType.Int).Value = IdInventario;
            cmd.Parameters.Add("@IdENSU", SqlDbType.Int).Value = IdENSU;
            cmd.Parameters.Add("@IdUsuario", SqlDbType.Int).Value = IdUsuario;
            cmd.Parameters.Add("@IdActividad", SqlDbType.Int).Value = IdActividad;
            //cmd.Parameters.Add("@Fecha", SqlDbType.DateTime).Value = Fecha;

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

        public static List<CGrupoBienResguardo> ObtenerGrupoBienRXIdEmpleado(int IdEmpleado, ref TResultado Resultado)
        {
            List<CGrupoBienResguardo> LGrupoBienR = new List<CGrupoBienResguardo>();

            SqlConnection cnn = new SqlConnection(CConexion.Obtener());
            SqlCommand cmd = new SqlCommand();
            SqlDataReader rd;
            cmd.Connection = cnn;
            cmd.CommandText = " SELECT     dbo.GrupoBienResguardo.IdGrupo, 'Grupo ' + dbo.GrupoBienResguardo.Grupo AS Grupo, dbo.Cat_Edificio.Edificio, dbo.Cat_Niveles.Nivel, dbo.Cat_Secciones.Nombre AS Seccion, dbo.GrupoBienResguardo.IdENSU, dbo.Resguardo.IdResguardo";
            cmd.CommandText += " FROM         dbo.Cat_ENSUbicacion INNER JOIN ";
            cmd.CommandText += " dbo.GrupoBienResguardo ON dbo.Cat_ENSUbicacion.IdENSU = dbo.GrupoBienResguardo.IdENSU INNER JOIN ";
            cmd.CommandText += " dbo.Cat_Personal INNER JOIN ";
            cmd.CommandText += " dbo.Resguardo ON dbo.Cat_Personal.IdEmpleado = dbo.Resguardo.IdEmpleado ON ";
            cmd.CommandText += " dbo.GrupoBienResguardo.IdResguardo = dbo.Resguardo.IdResguardo INNER JOIN ";
            cmd.CommandText += " dbo.Cat_Edificio ON dbo.Cat_ENSUbicacion.IdEdificio = dbo.Cat_Edificio.IdEdificio INNER JOIN ";
            cmd.CommandText += " dbo.Cat_Niveles ON dbo.Cat_ENSUbicacion.IdNivel = dbo.Cat_Niveles.IdNivel INNER JOIN ";
            cmd.CommandText += " dbo.Cat_Secciones ON dbo.Cat_ENSUbicacion.IdSeccion = dbo.Cat_Secciones.IdSeccion ";
            cmd.CommandText += " WHERE     (dbo.Cat_Personal.IdEmpleado = @IdEmpleado) ";

            //cmd.CommandText = "select NumInventario, DescripcionBien from FichaBien where IdEmpleado = @IdEmpleado AND IdActividad not in (1,6)";
            cmd.Parameters.Add("@IdEmpleado", SqlDbType.BigInt).Value = IdEmpleado;
            try
            {
                cnn.Open();
                rd = cmd.ExecuteReader();

                while (rd.Read())
                {
                    CGrupoBienResguardo GrupoBienR = new CGrupoBienResguardo();

                    GrupoBienR.IdGrupo = BdConverter.FieldToInt(rd["IdGrupo"]);
                    GrupoBienR.Grupo = BdConverter.FieldToString(rd["Grupo"]);
                    GrupoBienR.Edificio = BdConverter.FieldToString(rd["Edificio"]);
                    GrupoBienR.Nivel = BdConverter.FieldToString(rd["Nivel"]);
                    GrupoBienR.Seccion = BdConverter.FieldToString(rd["Seccion"]);
                    GrupoBienR.IdENSU = BdConverter.FieldToInt(rd["IdENSU"]);
                    GrupoBienR.IdResguardo = BdConverter.FieldToInt(rd["IdResguardo"]);
                    LGrupoBienR.Add(GrupoBienR);
                }
                cmd.Connection.Close();
            }
            catch (Exception e)
            {
                if (cmd.Connection.State != ConnectionState.Closed)
                    cmd.Connection.Close();
                Resultado.Exito = false;
                Resultado.Mensaje.Add("Se generó un error al tratar de obtener el idEmpleado.");
                Resultado.Detalles = e.Message;
                //return -1;
            }
            return LGrupoBienR;
        }

        public static DataTable ObtenerNivelesXIdEdificio(int IdEdificio, ref TResultado Resultado)
        {
            SqlCommand cmd = new SqlCommand();
            DataTable Tabla = new DataTable("Resultado");
            SqlConnection cnn = new SqlConnection(CConexion.Obtener());
            SqlDataAdapter adp = new SqlDataAdapter();
            cmd.CommandText = " SELECT IdNivel, Nivel ";
            cmd.CommandText += " FROM dbo.Cat_Niveles ";
            cmd.CommandText += " WHERE IdEdificio = @IdEdificio ";
            cmd.Parameters.Add("@IdEdificio", SqlDbType.Int).Value = IdEdificio;
            cmd.CommandType = CommandType.Text;
            cmd.Connection = cnn;
            adp.SelectCommand = cmd;
            try
            {
                cnn.Open();
                adp.Fill(Tabla);
                cnn.Close();
            }
            catch (Exception e)
            {
                if (cmd.Connection.State != ConnectionState.Closed)
                    cmd.Connection.Close();
                Resultado.Exito = false;
                Resultado.Mensaje.Add("Se generó un error al tratar de obtener los niveles");
                Resultado.Detalles = e.Message;
            }
            return Tabla;
        }

        public static DataTable ObtenerSeccionesXENSUUbicacion(int IdEdificio, int IdNivel, ref TResultado Resultado)
        {
            SqlCommand cmd = new SqlCommand();
            DataTable Tabla = new DataTable("Resultado");
            SqlConnection cnn = new SqlConnection(CConexion.Obtener());
            SqlDataAdapter adp = new SqlDataAdapter();
            cmd.CommandText = " SELECT Cat_ENSUbicacion.IdSeccion, Cat_Secciones.Nombre AS Seccion ";
            cmd.CommandText += " FROM Cat_ENSUbicacion ";
            cmd.CommandText += " INNER JOIN Cat_Edificio ON Cat_ENSUbicacion.IdEdificio = Cat_Edificio.IdEdificio  ";
            cmd.CommandText += " INNER JOIN Cat_Niveles ON Cat_ENSUbicacion.IdNivel = Cat_Niveles.IdNivel ";
            cmd.CommandText += " INNER JOIN Cat_Secciones ON Cat_ENSUbicacion.IdSeccion = Cat_Secciones.IdSeccion ";
            cmd.CommandText += " WHERE (Cat_ENSUbicacion.IdEdificio = @IdEdificio) AND (Cat_ENSUbicacion.IdNivel = @IdNivel) ";

            //cmd.CommandText += " WHERE IdEdificio = @IdEdificio ";
            cmd.Parameters.Add("@IdEdificio", SqlDbType.Int).Value = IdEdificio;
            cmd.Parameters.Add("@IdNivel", SqlDbType.Int).Value = IdNivel;

            cmd.CommandType = CommandType.Text;
            cmd.Connection = cnn;
            adp.SelectCommand = cmd;

            try
            {
                cnn.Open();
                adp.Fill(Tabla);
                cnn.Close();
            }
            catch (Exception e)
            {
                if (cmd.Connection.State != ConnectionState.Closed)
                    cmd.Connection.Close();
                Resultado.Exito = false;
                Resultado.Mensaje.Add("Se generó un error al tratar de obtener la Seccion ");
                Resultado.Detalles = e.Message;
                throw;
            }

            return Tabla;
        }

        public static string NombreTitularArea(int IdUniAdmin)
        {
            SqlCommand cmd = new SqlCommand();
            string NombreTitular = "";
            //DataTable Tabla = new DataTable("Resultado");
            SqlConnection cnn = new SqlConnection(CConexion.Obtener());
            SqlDataAdapter adp = new SqlDataAdapter();
            cmd.CommandText = "select (Cat_Personal.Nombre + ' ' + Cat_Personal.APaterno + ' ' + Cat_Personal.AMaterno) AS NombreTitular from  Cat_Personal ";
            cmd.CommandText += " INNER JOIN Cat_UniAdmin ON Cat_Personal.IdUniAdmin = Cat_UniAdmin.IdUniAdmin ";
            cmd.CommandText += " WHERE Cat_Personal.Titular = 'S' AND Cat_Personal.IdUniAdmin = @IdUniAdmin ";
            cmd.Parameters.Add("@IdUniAdmin", SqlDbType.Int).Value = IdUniAdmin;
            cmd.CommandType = CommandType.Text;
            cmd.Connection = cnn;
            adp.SelectCommand = cmd;

            try
            {
                cnn.Open();
                object res = cmd.ExecuteScalar();
                cmd.Connection.Close();

                NombreTitular = res.ToString();
            }
            catch (Exception)
            {
                if (cmd.Connection.State != ConnectionState.Closed)
                    cmd.Connection.Close();
                //Resultado.Exito = false;
                //Resultado.Mensaje.Add("Se generó un error al tratar de obtener el Nombre del Titular ");
                //Resultado.Detalles = e.Message;
            }
            return NombreTitular;
        }
    }
}