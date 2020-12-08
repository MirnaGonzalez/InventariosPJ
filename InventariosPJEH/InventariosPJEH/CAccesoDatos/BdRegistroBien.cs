using InventariosPJEH.CNegocios;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace InventariosPJEH.CAccesoDatos
{
    public class BdRegistroBien
    {
        public SqlConnection conexion;
        public string error;
        protected SqlDataAdapter adaptador;
        protected SqlDataReader reader;
        protected DataSet data;

        public BdRegistroBien()
        {
            this.conexion = ConexionBD.getConexion();
        }

        /// <summary>
        /// Consulta para obtener los tipos de documento. 
        /// </summary>
        /// <returns></returns>
        public static List<CRegistroBien> ObtenerTipoDocumento()
        {
            List<CRegistroBien> lista = new List<CRegistroBien>();

            SqlConnection cnn = new SqlConnection(CConexion.Obtener());
            SqlCommand cmd = new SqlCommand("select IdTipoDoc, Documento from Cat_TipoDocumento", cnn);
            cnn.Open();

            using (var rd = cmd.ExecuteReader())
            {
                while (rd.Read())
                {
                    CRegistroBien cRegistroDeUnBien = new CRegistroBien();
                    cRegistroDeUnBien.Documento = rd.GetString(1);
                    cRegistroDeUnBien.IdTipoDoc = rd.GetInt32(0);
                    lista.Add(cRegistroDeUnBien);
                }
            }
            return lista;
        }

        /// <summary>
        /// Consulta para obtener los tipos de adquisición. 
        /// </summary>
        /// <returns></returns>
        public static List<CRegistroBien> ObtenerTipoAdquisicion()
        {
            List<CRegistroBien> listaAdqui = new List<CRegistroBien>();

            SqlConnection cnn = new SqlConnection(CConexion.Obtener());
            SqlCommand cmd = new SqlCommand("select IdTipoAdqui, TipoAdqui from Cat_TipoAdquisicion ORDER BY TipoAdqui", cnn);
            cnn.Open();

            using (var rd = cmd.ExecuteReader())
            {
                while (rd.Read())
                {
                    CRegistroBien cRegistroDeUnBien = new CRegistroBien();
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
        public static List<CRegistroBien> ObtenerProveedor()
        {
            List<CRegistroBien> listaProveedor = new List<CRegistroBien>();

            SqlConnection cnn = new SqlConnection(CConexion.Obtener());
            SqlCommand cmd = new SqlCommand("select IdProveedor, Proveedor from Cat_Proveedores ORDER BY Proveedor ", cnn);
            cnn.Open();

            using (var rd = cmd.ExecuteReader())
            {
                while (rd.Read())
                {
                    CRegistroBien cRegistroDeUnBien = new CRegistroBien();
                    cRegistroDeUnBien.Proveedor = rd.GetString(1);
                    cRegistroDeUnBien.IdProveedor = rd.GetInt32(0);
                    listaProveedor.Add(cRegistroDeUnBien);
                }
            }
            return listaProveedor;
        }

        /// <summary>
        /// Consulta para obtener las areas de resguardo. 
        /// </summary>
        /// <returns></returns>
        public static List<CRegistroBien> ObtenerAreaDeResguardoInicial()
        {
            List<CRegistroBien> listaAreaResguardo = new List<CRegistroBien>();

            SqlConnection cnn = new SqlConnection(CConexion.Obtener());
            SqlCommand cmd = new SqlCommand("SELECT Nombre, IdSeccion FROM Cat_Secciones WHERE TipoSeccion = 'R' ORDER BY Nombre ", cnn);
            cnn.Open();

            using (var rd = cmd.ExecuteReader())
            {
                while (rd.Read())
                {
                    CRegistroBien cRegistroDeUnBien = new CRegistroBien();
                    cRegistroDeUnBien.Nombre = rd.GetString(0);
                    cRegistroDeUnBien.IdSeccion = rd.GetInt32(1);
                    listaAreaResguardo.Add(cRegistroDeUnBien);
                }
            }
            return listaAreaResguardo;
        }

        /// <summary>
        /// Consulta para obtener las propiedades. 
        /// </summary>
        /// <returns></returns>
        public static List<CRegistroBien> ObtenerPropiedad()
        {
            List<CRegistroBien> listaPropiedad = new List<CRegistroBien>();

            SqlConnection cnn = new SqlConnection(CConexion.Obtener());
            SqlCommand cmd = new SqlCommand("SELECT IdPropiedad, Propiedad FROM Cat_Propiedades ORDER BY Propiedad ", cnn);
            cnn.Open();

            using (var rd = cmd.ExecuteReader())
            {
                while (rd.Read())
                {
                    CRegistroBien cRegistroDeUnBien = new CRegistroBien();
                    cRegistroDeUnBien.Propiedad = rd.GetString(1);
                    cRegistroDeUnBien.IdPropiedad = rd.GetInt32(0);
                    listaPropiedad.Add(cRegistroDeUnBien);
                }
            }
            return listaPropiedad;
        }

        /// <summary>
        /// Consulta para obtener todas las partidas 
        /// </summary>
        /// <returns></returns>
        public static List<CRegistroBien> ObtenerPartida(string TipoPartida)
        {
   
            List<CRegistroBien> listaPartida = new List<CRegistroBien>();
           
            SqlConnection cnn = new SqlConnection(CConexion.Obtener());
            SqlCommand cmd = new SqlCommand("SELECT Partida, IdPartida FROM Cat_Partidas WHERE (TipoPartida = @TipoPartida) ORDER BY Partida", cnn);
            cmd.Parameters.Add("@TipoPartida", SqlDbType.Char).Value = TipoPartida;
            cnn.Open();

            using (var rd = cmd.ExecuteReader())
            {
                while (rd.Read())
                {
                    CRegistroBien cRegistroDeUnBien = new CRegistroBien();
                    cRegistroDeUnBien.Partida = rd.GetString(0);
                    cRegistroDeUnBien.idPartida = rd.GetInt32(1);
                    listaPartida.Add(cRegistroDeUnBien);
                }
            }
            return listaPartida;
        }

        /// <summary>
        /// Consulta para obtener las subclases. 
        /// </summary>
        /// <returns></returns>
        public static List<CRegistroBien> ObtenerSubClase(string TipoPartida)
        {
            List<CRegistroBien> listaSubClase = new List<CRegistroBien>();

            SqlConnection cnn = new SqlConnection(CConexion.Obtener());
            SqlCommand cmd = new SqlCommand("SELECT Cat_SubClase.IdSubClase, Cat_SubClase.SubClase FROM Cat_SubClase INNER JOIN Cat_Partidas ON Cat_SubClase.IdPartida = Cat_Partidas.IdPartida WHERE (Cat_Partidas.TipoPartida = @TipoPartida) ORDER BY Cat_SubClase.SubClase", cnn);
            cmd.Parameters.Add("@TipoPartida", SqlDbType.Char).Value = TipoPartida;
            cnn.Open();

            using (var rd = cmd.ExecuteReader())
            {
                while (rd.Read())
                {
                    CRegistroBien cRegistroDeUnBien = new CRegistroBien();
                    cRegistroDeUnBien.idSubClase = rd.GetInt32(0);
                    cRegistroDeUnBien.SubClase = rd.GetString(1);
                    listaSubClase.Add(cRegistroDeUnBien);
                }
            }
            return listaSubClase;
        }

        /// <summary>
        /// Consulta para obtener la lista de CONAC.
        /// </summary>
        /// <returns></returns>
        public static List<CRegistroBien> ObtenerCONAC(string TipoPartida)
        {
            List<CRegistroBien> listaCONAC = new List<CRegistroBien>();

            SqlConnection cnn = new SqlConnection(CConexion.Obtener());
            SqlCommand cmd = new SqlCommand("SELECT Descripcion, IdCONAC FROM Cat_CONAC WHERE (TipoPartida = @TipoPartida) ORDER BY Descripcion", cnn);
            cmd.Parameters.Add("@TipoPartida", SqlDbType.Char).Value = TipoPartida;
            cnn.Open();

            using (var rd = cmd.ExecuteReader())
            {
                while (rd.Read())
                {
                    CRegistroBien cRegistroDeUnBien = new CRegistroBien();
                    cRegistroDeUnBien.Descripcion = rd.GetString(0);
                    cRegistroDeUnBien.IdCONAC = rd.GetInt32(1);
                    listaCONAC.Add(cRegistroDeUnBien);
                }
            }
            return listaCONAC;
        }

        /// <summary>
        /// Consulta para obtener las marcas. 
        /// </summary>
        /// <returns></returns>
        public static List<CRegistroBien> ObtenerMarca()
        {
            List<CRegistroBien> listaMarca = new List<CRegistroBien>();

            SqlConnection cnn = new SqlConnection(CConexion.Obtener());
            SqlCommand cmd = new SqlCommand("select Descripcion, IdMarca from Cat_Marca ORDER BY Descripcion", cnn);
            cnn.Open();

            using (var rd = cmd.ExecuteReader())
            {
                while (rd.Read())
                {
                    CRegistroBien cRegistroDeUnBien = new CRegistroBien();
                    cRegistroDeUnBien.Descripcion = rd.GetString(0);
                    cRegistroDeUnBien.IdMarca = rd.GetInt32(1);
                    listaMarca.Add(cRegistroDeUnBien);
                }
            }
            return listaMarca;
        }

        /// <summary>
        /// Consulta para obtener toda la informaión requerid para llenar
        /// el grid. 
        /// </summary>
        /// <param name="TextBoxNoDocumento"></param>
        /// <returns></returns>
        public static DataTable Grid(TextBox TextBoxNoDocumento)
        {
            string num = TextBoxNoDocumento.Text;
            DataTable Tabla = new DataTable("Resultado");
            SqlConnection cnn = new SqlConnection(CConexion.Obtener());
            var query = @"SELECT '1' AS id, 'Bienes Informáticos' AS Detalle, SUM(FichaBien.CostoTotal) AS Importe, FichaBien.TipoPartida FROM FichaBien INNER JOIN FichaCompra ON FichaBien.IdCompra = FichaCompra.IdCompra WHERE(FichaCompra.NumDocumento = @num AND FichaBien.TipoPartida = 1) GROUP BY FichaBien.TipoPartida UNION SELECT '2' AS id, 'Bienes Muebles' AS Detalle, SUM(FichaBien.CostoTotal) AS Importe, FichaBien.TipoPartida FROM FichaBien INNER JOIN FichaCompra ON FichaBien.IdCompra = FichaCompra.IdCompra WHERE(FichaCompra.NumDocumento = @num AND FichaBien.TipoPartida = 2) GROUP BY FichaBien.TipoPartida";
            SqlCommand cmd = new SqlCommand(query, cnn);
            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Add("@num", SqlDbType.VarChar, 30).Value = num;
            cmd.Connection = cnn;
            adp.SelectCommand = cmd;
            cnn.Open();
            adp.Fill(Tabla);
            cnn.Close();
            return Tabla;
        }

        /// <summary>
        /// Consulta para obtener todos los tipos de partida. 
        /// </summary>
        /// <param name="DropDownListPartida"></param>
        /// <returns></returns>
        public static DataTable TipoPartida(DropDownList DropDownListPartida)
        {
            int id = Convert.ToInt32(DropDownListPartida.SelectedValue);

            DataTable TablaTipoPartida = new DataTable("Resultado");
            SqlConnection cnn = new SqlConnection(CConexion.Obtener());
            var query = @"select TipoPartida from Cat_Partidas where idPartida = @id";
            SqlCommand cmd = new SqlCommand(query, cnn);
            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;
            cmd.Connection = cnn;
            adp.SelectCommand = cmd;
            cnn.Open();
            adp.Fill(TablaTipoPartida);
            cnn.Close();
            return TablaTipoPartida;
        }

        /// <summary>
        /// Consulta para obtener la lista de las ubicaciones de los bienes. 
        /// </summary>
        /// <param name="DropDownListAreaDeResguardoInicial"></param>
        /// <returns></returns>
        public static DataTable idENSU(DropDownList DropDownListAreaDeResguardoInicial)
        {
            int id = Convert.ToInt32(DropDownListAreaDeResguardoInicial.SelectedValue);

            DataTable TablaENSU = new DataTable("Resultado");
            SqlConnection cnn = new SqlConnection(CConexion.Obtener());
            var query = @"select idENSU from Cat_ENSUbicacion where IdSeccion = @id";
            SqlCommand cmd = new SqlCommand(query, cnn);
            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;
            cmd.Connection = cnn;
            adp.SelectCommand = cmd;
            cnn.Open();
            adp.Fill(TablaENSU);
            cnn.Close();
            return TablaENSU;
        }

        /// <summary>
        /// Consulta para obtener el id de la factura. 
        /// </summary>
        /// <param name="TextBoxNoDocumento"></param>
        /// <returns></returns>
        public static DataTable idCompra(TextBox TextBoxNoDocumento)
        {
            String num = TextBoxNoDocumento.Text;

            DataTable TablaCompra = new DataTable("Resultado");
            SqlConnection cnn = new SqlConnection(CConexion.Obtener());
            var query = @"select IdCompra from FichaCompra where NumDocumento = @num";
            SqlCommand cmd = new SqlCommand(query, cnn);
            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@num", num);
            //cmd.Parameters.Add("@numero", SqlDbType.Int).Value = num;
            cmd.Connection = cnn;
            adp.SelectCommand = cmd;
            cnn.Open();
            adp.Fill(TablaCompra);
            cnn.Close();
            return TablaCompra;
        }

        /// <summary>
        /// Consulta para obtener el número de partida. 
        /// </summary>
        /// <param name="DropDownListPartida"></param>
        /// <returns></returns>
        public static List<CRegistroBien> ObtenerNumPartida(DropDownList DropDownListPartida)
        {
            List<CRegistroBien> lisaNumPC = new List<CRegistroBien>();
            int id = Convert.ToInt32(DropDownListPartida.SelectedValue);
            SqlConnection cnn = new SqlConnection(CConexion.Obtener());
            SqlCommand cmd = new SqlCommand("select NumPartida from Cat_Partidas where idPartida = @id", cnn);
            cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;
            cnn.Open();

            using (var rd = cmd.ExecuteReader())
            {
                while (rd.Read())
                {
                    CRegistroBien cRegistroDeUnBien = new CRegistroBien();
                    cRegistroDeUnBien.NumPartida = rd.GetInt32(0);
                    lisaNumPC.Add(cRegistroDeUnBien);
                }
            }
            return lisaNumPC;
        }

        /// <summary>
        /// Consulta para obtener toda la información de la factura.  
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
        public static List<CRegistroBien> ProveedorFactura(TextBox TextBoxNoDocumento)
        {
            string numDocumento = TextBoxNoDocumento.Text;
            List<CRegistroBien> ProveedorList = new List<CRegistroBien>();
            
            SqlConnection cnn = new SqlConnection(CConexion.Obtener());
            SqlCommand cmd = new SqlCommand("select Proveedor from Cat_Proveedores inner join FichaCompra on Cat_Proveedores.IdProveedor = FichaCompra.IdProveedor where NumDocumento = @numeroDo", cnn);
            cmd.Parameters.Add("@numeroDo", SqlDbType.VarChar, 30).Value = numDocumento;
            cnn.Open();

            using (var rd = cmd.ExecuteReader())
            {
                while (rd.Read())
                {
                    CRegistroBien cRegistroDeUnBien = new CRegistroBien();
                    cRegistroDeUnBien.Proveedor = rd.GetString(0);
                    ProveedorList.Add(cRegistroDeUnBien);
                }
            }
            return ProveedorList;
        }

        /// <summary>
        /// Consulta para obtener el documento de la factura seleccinada. 
        /// </summary>
        /// <param name="TextBoxNoDocumento"></param>
        /// <returns></returns>
        public static List<CRegistroBien> DocumentoFactura(TextBox TextBoxNoDocumento)
        {
            string numDocumento = TextBoxNoDocumento.Text;
            List<CRegistroBien> DocumentoList = new List<CRegistroBien>();

            SqlConnection cnn = new SqlConnection(CConexion.Obtener());
            SqlCommand cmd = new SqlCommand("select Documento from Cat_TipoDocumento inner join FichaCompra on Cat_TipoDocumento.IdTipoDoc = FichaCompra.IdTipoDoc where NumDocumento = @numeroDo", cnn);
            cmd.Parameters.Add("@numeroDo", SqlDbType.VarChar, 30).Value = numDocumento;
            cnn.Open();

            using (var rd = cmd.ExecuteReader())
            {
                while (rd.Read())
                {
                    CRegistroBien cRegistroDeUnBien = new CRegistroBien();
                    cRegistroDeUnBien.Documento = rd.GetString(0);
                    DocumentoList.Add(cRegistroDeUnBien);
                }
            }
            return DocumentoList;
        }

        /// <summary>
        /// Consulta para obtener el tipo de adquisición de la fcatura seleccionada. 
        /// </summary>
        /// <param name="TextBoxNoDocumento"></param>
        /// <returns></returns>
        public static List<CRegistroBien> AdquiFactura(TextBox TextBoxNoDocumento)
        {
            string numDocumento = TextBoxNoDocumento.Text;
            List<CRegistroBien> TipoAdquiList = new List<CRegistroBien>();

            SqlConnection cnn = new SqlConnection(CConexion.Obtener());
            SqlCommand cmd = new SqlCommand("  select TipoAdqui from Cat_TipoAdquisicion inner join FichaCompra on Cat_TipoAdquisicion.IdTipoAdqui = FichaCompra.IdTipoAdqui where NumDocumento = @numeroDo", cnn);
            cmd.Parameters.Add("@numeroDo", SqlDbType.VarChar, 30).Value = numDocumento;
            cnn.Open();

            using (var rd = cmd.ExecuteReader())
            {
                while (rd.Read())
                {
                    CRegistroBien cRegistroDeUnBien = new CRegistroBien();
                    cRegistroDeUnBien.TipoAdqui = rd.GetString(0);
                    TipoAdquiList.Add(cRegistroDeUnBien);
                }
            }
            return TipoAdquiList;
        }

        /// <summary>
        /// Consulta para obtener la propiedad de la factura seleccionada. 
        /// </summary>
        /// <param name="TextBoxNoDocumento"></param>
        /// <returns></returns>
        public static List<CRegistroBien> PropiedadFactura(TextBox TextBoxNoDocumento)
        {
            string numDocumento = TextBoxNoDocumento.Text;
            List<CRegistroBien> PropiedadList = new List<CRegistroBien>();

            SqlConnection cnn = new SqlConnection(CConexion.Obtener());
            SqlCommand cmd = new SqlCommand("select Propiedad from Cat_Propiedades inner join FichaCompra on Cat_Propiedades.IdPropiedad = FichaCompra.IdPropiedad where NumDocumento = @numeroDo", cnn);
            cmd.Parameters.Add("@numeroDo", SqlDbType.VarChar, 30).Value = numDocumento;
            cnn.Open();

            using (var rd = cmd.ExecuteReader())
            {
                while (rd.Read())
                {
                    CRegistroBien cRegistroDeUnBien = new CRegistroBien();
                    cRegistroDeUnBien.Propiedad = rd.GetString(0);
                    PropiedadList.Add(cRegistroDeUnBien);
                }
            }
            return PropiedadList;
        }
    }

}
