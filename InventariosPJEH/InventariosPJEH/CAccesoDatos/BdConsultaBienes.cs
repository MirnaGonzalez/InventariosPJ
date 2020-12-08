using InventariosPJEH.CNegocios;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace InventariosPJEH.CAccesoDatos
{
    public class BdConsultaBienes
    {
        public SqlConnection conexion;
        public string error;
        protected SqlDataAdapter adaptador;
        protected SqlDataReader reader;
        protected DataSet data;

        public BdConsultaBienes()
        {
            this.conexion = ConexionBD.getConexion();
        }

        /// <summary>
        /// Consulta para obtener los Proveedores
        /// </summary>
        /// <returns></returns>
        public static List<CConsultaBienes> ObtenerProveedor()
        {
            List<CConsultaBienes> listaProveedor = new List<CConsultaBienes>();

            SqlConnection cnn = new SqlConnection(CConexion.Obtener());
            SqlCommand cmd = new SqlCommand("select IdProveedor, Proveedor from Cat_Proveedores", cnn);
            cnn.Open();

            using (var rd = cmd.ExecuteReader())
            {
                while (rd.Read())
                {
                    CConsultaBienes cRegistroDeUnBien = new CConsultaBienes();
                    cRegistroDeUnBien.Proveedor = rd.GetString(1);
                    cRegistroDeUnBien.IdProveedor = rd.GetInt32(0);
                    listaProveedor.Add(cRegistroDeUnBien);
                }
            }
            return listaProveedor;
        }

        /// <summary>
        /// Consulta para obtener las Propiedades 
        /// </summary>
        /// <returns></returns>
        public static List<CConsultaBienes> ObtenerPropiedad()
        {
            List<CConsultaBienes> listaPropiedad = new List<CConsultaBienes>();

            SqlConnection cnn = new SqlConnection(CConexion.Obtener());
            SqlCommand cmd = new SqlCommand("select IdPropiedad, Propiedad from Cat_Propiedades", cnn);
            cnn.Open();

            using (var rd = cmd.ExecuteReader())
            {
                while (rd.Read())
                {
                    CConsultaBienes cRegistroDeUnBien = new CConsultaBienes();
                    cRegistroDeUnBien.Propiedad = rd.GetString(1);
                    cRegistroDeUnBien.IdPropiedad = rd.GetInt32(0);
                    listaPropiedad.Add(cRegistroDeUnBien);
                }
            }
            return listaPropiedad;
        }

        /// <summary>
        /// Consulta para obtener las Partidas 
        /// </summary>
        /// <returns></returns>
        public static List<CConsultaBienes> ObtenerPartida()
        {
            List<CConsultaBienes> listaPartida = new List<CConsultaBienes>();

            SqlConnection cnn = new SqlConnection(CConexion.Obtener());
            SqlCommand cmd = new SqlCommand("select Partida,idPartida from Cat_Partidas", cnn);
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

        /// <summary>
        /// Consulta para obtener las SubClases 
        /// </summary>
        /// <returns></returns>
        public static List<CConsultaBienes> ObtenerSubClase()
        {
            List<CConsultaBienes> listaSubClase = new List<CConsultaBienes>();

            SqlConnection cnn = new SqlConnection(CConexion.Obtener());
            SqlCommand cmd = new SqlCommand("select idSubClase,SubClase from Cat_SubClase", cnn);
            cnn.Open();

            using (var rd = cmd.ExecuteReader())
            {
                while (rd.Read())
                {
                    CConsultaBienes cRegistroDeUnBien = new CConsultaBienes();
                    cRegistroDeUnBien.idSubClase = rd.GetInt32(0);
                    cRegistroDeUnBien.SubClase = rd.GetString(1);
                    listaSubClase.Add(cRegistroDeUnBien);
                }
            }
            return listaSubClase;
        }

        /// <summary>
        /// Consulta para obtener las Marcas 
        /// </summary>
        /// <returns></returns>
        public static List<CConsultaBienes> ObtenerMarca()
        {
            List<CConsultaBienes> listaMarca = new List<CConsultaBienes>();

            SqlConnection cnn = new SqlConnection(CConexion.Obtener());
            SqlCommand cmd = new SqlCommand("select Descripcion, IdMarca from Cat_Marca", cnn);
            cnn.Open();

            using (var rd = cmd.ExecuteReader())
            {
                while (rd.Read())
                {
                    CConsultaBienes cRegistroDeUnBien = new CConsultaBienes();
                    cRegistroDeUnBien.Descripcion = rd.GetString(0);
                    cRegistroDeUnBien.IdMarca = rd.GetInt32(1);
                    listaMarca.Add(cRegistroDeUnBien);
                }
            }
            return listaMarca;
        }

        /// <summary>
        /// Consulta para obtener todos los Bienes relacinados con los 
        /// filtros que fueron ingresados por el usuario.
        /// </summary>
        /// <param name="DropDownListPropiedad"></param>
        /// <param name="DropDownListPartida"></param>
        /// <param name="DropDownListSubClase"></param>
        /// <param name="TextBoxNoDocumento"></param>
        /// <param name="DropDownListProveedor"></param>
        /// <param name="TxtFechaAdquisicion"></param>
        /// <param name="TxtNoInventario"></param>
        /// <param name="DropDownListMarca"></param>
        /// <param name="TextBoxModelo"></param>
        /// <param name="TextBoxSerie"></param>
        /// <param name="DropPersona"></param>
        /// <param name="TxtNoResguardo"></param>
        /// <param name="DropClasificacion"></param>
        /// <param name="DropUniAdmin"></param>
        /// <param name="DropEstatus"></param>
        /// <param name="DropAreaMantenimiento"></param>
        /// <returns></returns>
        public static DataTable BuscarGrid(DropDownList DropDownListPropiedad, DropDownList DropDownListPartida, 
            DropDownList DropDownListSubClase, TextBox TextBoxNoDocumento, DropDownList DropDownListProveedor, TextBox TxtFechaAdquisicion,
            TextBox TxtNoInventario, DropDownList DropDownListMarca, TextBox TextBoxModelo, TextBox TextBoxSerie, DropDownList DropPersona,
            TextBox TxtNoResguardo, DropDownList DropClasificacion, DropDownList DropUniAdmin, DropDownList DropEstatus,
            DropDownList DropAreaMantenimiento)
        {
            int idPartida = Convert.ToInt32(DropDownListPartida.SelectedValue);
            SqlConnection con = new SqlConnection(CConexion.Obtener());
            con.Open();
            string sqlCad1 = @"select TipoPartida from Cat_Partidas
                                where IdPartida = @idP";
            SqlCommand cmdMax1 = new SqlCommand(sqlCad1, con);
            cmdMax1.Parameters.Add("@idP", SqlDbType.Int, 30).Value = idPartida;
            SqlDataReader leerMax1 = cmdMax1.ExecuteReader();
            string Tipo;
            if (leerMax1.Read() == true)
            {
                Tipo = leerMax1[0].ToString();
            }
            else
            {
                Tipo = "";
            }
            con.Close();

            string NombrePer = DropPersona.SelectedItem.Text;
            string noResguardo = TxtNoResguardo.Text;
            int idPropiedd = Convert.ToInt32(DropDownListPropiedad.SelectedValue);
            string numInven = TxtNoInventario.Text;
            int SubClase = Convert.ToInt32(DropDownListSubClase.SelectedValue);
            string numDoc = TextBoxNoDocumento.Text;
            int idProveedor = Convert.ToInt32(DropDownListProveedor.SelectedValue);
            string fecha = TxtFechaAdquisicion.Text;
            int marca = Convert.ToInt32(DropDownListMarca.SelectedValue);
            string modelo = TextBoxModelo.Text;
            string serie = TextBoxSerie.Text;
            int idAbrevi = Convert.ToInt32(DropClasificacion.SelectedValue);
            int idUniAdmin = Convert.ToInt32(DropUniAdmin.SelectedValue);
            int Estatus = Convert.ToInt32(DropEstatus.SelectedValue);
            string areaMante = DropAreaMantenimiento.SelectedItem.Text;

            if (modelo == "")
            {
                modelo = " ";
            }else
            {
                modelo = TextBoxModelo.Text;
            }

            if (serie == "")
            {
                serie = " ";
            }
            else
            {
                serie = TextBoxSerie.Text;
            }


            DataTable Tabla = new DataTable("Resultado");
            SqlConnection cnn = new SqlConnection(CConexion.Obtener());
            var query = @"select * from VistaConsultarBienes where NumInventario like @numInven or IdPropiedad like @idPropiedad 
                        or IdPartida like @idP or IdSubClase like @SubC or NumDocumento like @numDoc or IdProveedor like @idProveedor
                        or FechaAdquisicion like @fecha or IdMarca like @idMarca or Modelo like @modelo or Serie like @serie
                        or NombrePersona like @NombrePer or IdResguardo like @noRes or idAbreviatura like @IdAbre 
                        or IdUniAdmin like @idUniAdmin or IdActividad like @Estatus or Perfil like @areaMante";
            SqlCommand cmd = new SqlCommand(query, cnn);
            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Add("@numInven", SqlDbType.VarChar).Value = numInven;
            cmd.Parameters.Add("@idPropiedad", SqlDbType.Int).Value = idPropiedd;
            cmd.Parameters.Add("@TipoP", SqlDbType.VarChar).Value = Tipo;
            cmd.Parameters.Add("@SubC", SqlDbType.Int).Value = SubClase;
            cmd.Parameters.Add("@numDoc", SqlDbType.VarChar).Value = numDoc;
            cmd.Parameters.Add("@idProveedor", SqlDbType.Int).Value = idProveedor;
            cmd.Parameters.Add("@NombrePer", SqlDbType.VarChar).Value = NombrePer;
            cmd.Parameters.Add("@noRes", SqlDbType.VarChar).Value = noResguardo;
            cmd.Parameters.Add("@IdAbre", SqlDbType.Int).Value = idAbrevi;
            cmd.Parameters.Add("@idUniAdmin", SqlDbType.Int).Value = idUniAdmin;
            cmd.Parameters.Add("@Estatus", SqlDbType.Int).Value = Estatus;
            cmd.Parameters.Add("@areaMante", SqlDbType.VarChar).Value = areaMante;
            cmd.Parameters.Add("@idP", SqlDbType.Int, 30).Value = idPartida;

            if (fecha == "")
            {
                cmd.Parameters.Add("@fecha", SqlDbType.VarChar).Value = fecha;
            }
            else
            {
                cmd.Parameters.Add("@fecha", SqlDbType.Date).Value = Convert.ToDateTime(fecha);
            }
            
            cmd.Parameters.Add("@idMarca", SqlDbType.Int).Value = marca;
            cmd.Parameters.Add("@modelo", SqlDbType.VarChar).Value = modelo;
            cmd.Parameters.Add("@serie", SqlDbType.VarChar).Value = serie;
            cmd.Connection = cnn;
            adp.SelectCommand = cmd;
            cnn.Open();
            adp.Fill(Tabla);
            cnn.Close();
            return Tabla;
        }

        /// <summary>
        /// Consulta para obtener la Marca dependiendo el id de la 
        /// opción que eligio el usuario.
        /// </summary>
        /// <param name="DropDownListMarca"></param>
        /// <returns></returns>
        public static DataTable Marca (DropDownList DropDownListMarca)
        {
            int marca = Convert.ToInt32(DropDownListMarca.SelectedValue);
            DataTable Tabla = new DataTable("Resultado");
            SqlConnection cnn = new SqlConnection(CConexion.Obtener());
            var query = @"select Descripcion from Cat_Marca where IdMarca = @idMarca";
            SqlCommand cmd = new SqlCommand(query, cnn);
            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Add("@idMarca", SqlDbType.Int).Value = marca;
            cmd.Connection = cnn;
            adp.SelectCommand = cmd;
            cnn.Open();
            adp.Fill(Tabla);
            cnn.Close();
            return Tabla;
        }

        /// <summary>
        /// Consulta para obtener la clasificacion de las abreviaturas. 
        /// </summary>
        /// <returns></returns>
        public static DataTable Ini1()
        {
            DataTable Tabla = new DataTable("Resultado");
            SqlConnection cnn = new SqlConnection(CConexion.Obtener());
            SqlCommand cmd = new SqlCommand();
            SqlDataAdapter adp = new SqlDataAdapter();
            cmd.CommandText = "select DISTINCT idAbreviatura, Descripcion from Abreviaturas";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = cnn;
            adp.SelectCommand = cmd;

            cnn.Open();
            adp.Fill(Tabla);
            cnn.Close();
            return Tabla;
        }

        /// <summary>
        /// Consulta para obtener los distritos. 
        /// </summary>
        /// <returns></returns>
        public static DataTable ObtenerDistritos()
        {
            SqlCommand cmd = new SqlCommand();
            DataTable Tabla = new DataTable("Resultado");
            SqlConnection cnn = new SqlConnection(CConexion.Obtener());
            SqlDataAdapter adp = new SqlDataAdapter();
            cmd.CommandText = "select IdDistrito, Distrito from Cat_Distritos ";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = cnn;
            adp.SelectCommand = cmd;
            cnn.Open();
            adp.Fill(Tabla);
            cnn.Close();
            return Tabla;
        }

        /// <summary>
        /// Consulta para obtener la clasificación de las unidades administrativas. 
        /// </summary>
        /// <param name="Clasificacion"></param>
        /// <returns></returns>
        public static DataTable ObtenerClasificacion(int Clasificacion)
        {
            DataTable Tabla = new DataTable("Resultado");
            SqlConnection cnn = new SqlConnection(CConexion.Obtener());
            SqlCommand cmd = new SqlCommand();
            SqlDataAdapter adp = new SqlDataAdapter();
            cmd.CommandText = "select DISTINCT UniAdmin, idUniAdmin from Cat_UniAdmin u, Abreviaturas  where u.IdAbreviatura = " + Clasificacion;
            cmd.CommandType = CommandType.Text;
            cmd.Connection = cnn;
            adp.SelectCommand = cmd;
            cnn.Open();
            adp.Fill(Tabla);
            cnn.Close();
            return Tabla;
        }

        /// <summary>
        /// Consulta para obtener las unidades administrativas. 
        /// </summary>
        /// <param name="IdDistrito"></param>
        /// <returns></returns>
        public static DataTable ObtenerUniAdminN(int IdDistrito)
        {
            SqlCommand cmd = new SqlCommand();
            DataTable Tabla = new DataTable("Resultado");
            SqlConnection cnn = new SqlConnection(CConexion.Obtener());
            SqlDataAdapter adp = new SqlDataAdapter();
            cmd.CommandText = "select IdUniAdmin, UniAdmin from VistaDistritos where  IdDistrito =" + IdDistrito;
            cmd.CommandType = CommandType.Text;
            cmd.Connection = cnn;
            adp.SelectCommand = cmd;
            cnn.Open();
            adp.Fill(Tabla);
            cnn.Close();
            return Tabla;
        }

        /// <summary>
        /// Consulta para obtener el personal. 
        /// </summary>
        /// <param name="idAreaResguardo"></param>
        /// <returns></returns>
        public static DataTable ObtenerPersona(int idAreaResguardo)
        {
            SqlCommand cmd = new SqlCommand();
            DataTable Tabla = new DataTable("Resultado");
            SqlConnection cnn = new SqlConnection(CConexion.Obtener());
            SqlDataAdapter adp = new SqlDataAdapter();
            cmd.CommandText = "select Nombre, IdEmpleado from Cat_Personal where IdUniAdmin =" + idAreaResguardo;
            cmd.CommandType = CommandType.Text;
            cmd.Connection = cnn;
            adp.SelectCommand = cmd;
            cnn.Open();
            adp.Fill(Tabla);
            cnn.Close();
            return Tabla;
        }

        /// <summary>
        /// Consulta para obtener el estatus de los Bienes. 
        /// </summary>
        /// <returns></returns>
        public static List<CConsultaBienes> Estatus()
        {
            List<CConsultaBienes> listaEstaus = new List<CConsultaBienes>();

            SqlConnection cnn = new SqlConnection(CConexion.Obtener());
            SqlCommand cmd = new SqlCommand("select IdActividad, Actividad from Cat_Actividades", cnn);
            cnn.Open();

            using (var rd = cmd.ExecuteReader())
            {
                while (rd.Read())
                {
                    CConsultaBienes cRegistroDeUnBien = new CConsultaBienes();
                    cRegistroDeUnBien.IdActividad = rd.GetInt32(0);
                    cRegistroDeUnBien.Actividad = rd.GetString(1);
                    listaEstaus.Add(cRegistroDeUnBien);
                }
            }
            return listaEstaus;
        }
    }

}
