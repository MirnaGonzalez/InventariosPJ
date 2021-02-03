using InventariosPJEH.CNegocios;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace InventariosPJEH.CAccesoDatos
{
    public class BdModificacionBien
    {
        public SqlConnection conexion;
        public string error;
        protected SqlDataAdapter adaptador;
        protected SqlDataReader reader;
        protected DataSet data;

        public BdModificacionBien()
        {
            this.conexion = ConexionBD.getConexion();
        }

        /// <summary>
        /// Consulta para obtener el número de inventario y la descripción del Bien
        /// dependiendo de los filtro ingresados por el usuario. 
        /// </summary>
        /// <param name="TxtNoDoc1"></param>
        /// <param name="TxtNoLote"></param>
        /// <param name="TxtNoInventario"></param>
        /// <returns></returns>
        public static DataTable BuscarGrid(TextBox TxtNoDoc1,TextBox TxtNoLote, TextBox TxtNoInventario)
        {
            string numDoc = TxtNoDoc1.Text;
            string numLote = TxtNoLote.Text;
            string numInven = TxtNoInventario.Text;

            DataTable Tabla = new DataTable("Resultado");
            SqlConnection cnn = new SqlConnection(CConexion.Obtener());
            var query = @"  select NumInventario, DescripcionBien, Serie 
                              from FichaBien 
                              Inner join FichaCompra
                              on FichaBien.IdCompra = FichaCompra.IdCompra
                              where (NumLote = @numLote OR NumInventario = @numInven OR NumDocumento = @numDoc)";
            SqlCommand cmd = new SqlCommand(query, cnn);
            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Add("@numDoc", SqlDbType.VarChar).Value = numDoc;
            cmd.Parameters.Add("@numLote", SqlDbType.VarChar).Value = numLote;
            cmd.Parameters.Add("@numInven", SqlDbType.VarChar).Value = numInven;
            cmd.Connection = cnn;
            adp.SelectCommand = cmd;
            cnn.Open();
            adp.Fill(Tabla);
            cnn.Close();
            return Tabla;
        }

        /// <summary>
        /// Consulta para obtener el número de lote al que pertenece el Bien. 
        /// </summary>
        /// <param name="TxtNoDoc1"></param>
        /// <param name="TxtNoLote"></param>
        /// <param name="TxtNoInventario"></param>
        /// <returns></returns>
        public static DataTable BuscarNumLote(TextBox TxtNoDoc1, TextBox TxtNoLote, TextBox TxtNoInventario)
        {
            string numDoc = TxtNoDoc1.Text;
            string numLote = TxtNoLote.Text;
            string numInven = TxtNoInventario.Text;

            DataTable TablaLote = new DataTable("Resultado");
            SqlConnection cnn = new SqlConnection(CConexion.Obtener());
            var query = @"  select NumLote 
                              from FichaBien 
                              Inner join FichaCompra
                              on FichaBien.IdCompra = FichaCompra.IdCompra
                              where (NumLote = @numLote OR NumInventario = @numInven OR NumDocumento = @numDoc)";
            SqlCommand cmd = new SqlCommand(query, cnn);
            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Add("@numDoc", SqlDbType.VarChar).Value = numDoc;
            cmd.Parameters.Add("@numLote", SqlDbType.VarChar).Value = numLote;
            cmd.Parameters.Add("@numInven", SqlDbType.VarChar).Value = numInven;
            cmd.Connection = cnn;
            adp.SelectCommand = cmd;
            cnn.Open();
            adp.Fill(TablaLote);
            cnn.Close();
            return TablaLote;
        }

        /// <summary>
        /// Consulta para obtener el número de documento. 
        /// </summary>
        /// <param name="TxtNoDoc1"></param>
        /// <param name="TxtNoLote"></param>
        /// <param name="TxtNoInventario"></param>
        /// <returns></returns>
        public static DataTable BuscarNumDoc(TextBox TxtNoDoc1, TextBox TxtNoLote, TextBox TxtNoInventario)
        {
            string numDoc = TxtNoDoc1.Text;
            string numLote = TxtNoLote.Text;
            string numInven = TxtNoInventario.Text;

            DataTable TablaDoc = new DataTable("Resultado");
            SqlConnection cnn = new SqlConnection(CConexion.Obtener());
            var query = @" select NumDocumento
                            from FichaCompra 
                            Inner join FichaBien
                            on FichaBien.IdCompra = FichaCompra.IdCompra
                            where (NumLote = @numLote OR NumInventario = @numInven OR NumDocumento = @numDoc)";
            SqlCommand cmd = new SqlCommand(query, cnn);
            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Add("@numDoc", SqlDbType.VarChar).Value = numDoc;
            cmd.Parameters.Add("@numLote", SqlDbType.VarChar).Value = numLote;
            cmd.Parameters.Add("@numInven", SqlDbType.VarChar).Value = numInven;
            cmd.Connection = cnn;
            adp.SelectCommand = cmd;
            cnn.Open();
            adp.Fill(TablaDoc);
            cnn.Close();
            return TablaDoc;
        }

        /// <summary>
        /// Consulta para obtener la información de l afactura a la que pertenece el Bien. 
        /// </summary>
        /// <param name="Lblbien"></param>
        /// <returns></returns>
        public static DataTable BuscarFactura(Label Lblbien)
        {
            string NumBien = Lblbien.Text;
            DataTable TablaFactura = new DataTable("Resultado");
            SqlConnection cnn = new SqlConnection(CConexion.Obtener());
            var query = @"select * from FichaCompra
                            inner join FichaBien
                            on FichaCompra.idCompra = FichaBien.idCompra
                            where NumInventario = @numBien";
            SqlCommand cmd = new SqlCommand(query, cnn);
            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Add("@numBien", SqlDbType.VarChar, 30).Value = NumBien;
            cmd.Connection = cnn;
            adp.SelectCommand = cmd;
            cnn.Open();
            adp.Fill(TablaFactura);
            cnn.Close();
            return TablaFactura;
        }

        /// <summary>
        /// Consulta para obtener todos los proveedores. 
        /// </summary>
        /// <param name="Lblbien"></param>
        /// <returns></returns>
        public static DataTable BuscarProveedor(Label Lblbien)
        {
            string NumBien = Lblbien.Text;
            DataTable TablaFactura = new DataTable("Resultado");
            SqlConnection cnn = new SqlConnection(CConexion.Obtener());
            var query = @" select Proveedor from Cat_Proveedores 
                              inner join FichaCompra 
                              on Cat_Proveedores.IdProveedor = FichaCompra.IdProveedor 
                              inner join FichaBien on FichaBien.idCompra = FichaCompra.idCompra
                              where NumInventario = @numBien";
            SqlCommand cmd = new SqlCommand(query, cnn);
            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Add("@numBien", SqlDbType.VarChar, 30).Value = NumBien;
            cmd.Connection = cnn;
            adp.SelectCommand = cmd;
            cnn.Open();
            adp.Fill(TablaFactura);
            cnn.Close();
            return TablaFactura;
        }

        /// <summary>
        /// Consulta para obtener todos los tipos de documento. 
        /// </summary>
        /// <param name="Lblbien"></param>
        /// <returns></returns>
        public static DataTable BuscarDocumento(Label Lblbien)
        {
            string NumBien = Lblbien.Text;
            DataTable TablaFactura = new DataTable("Resultado");
            SqlConnection cnn = new SqlConnection(CConexion.Obtener());
            var query = @" select Documento from Cat_TipoDocumento 
                            inner join FichaCompra 
                            on Cat_TipoDocumento.IdTipoDoc = FichaCompra.IdTipoDoc 
                            inner join FichaBien on FichaBien.idCompra = FichaCompra.idCompra
                            where NumInventario = @numBien";
            SqlCommand cmd = new SqlCommand(query, cnn);
            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Add("@numBien", SqlDbType.VarChar, 30).Value = NumBien;
            cmd.Connection = cnn;
            adp.SelectCommand = cmd;
            cnn.Open();
            adp.Fill(TablaFactura);
            cnn.Close();
            return TablaFactura;
        }

        /// <summary>
        /// Consulta para obtener los tipos de adquisición. 
        /// </summary>
        /// <param name="Lblbien"></param>
        /// <returns></returns>
        public static DataTable BuscarTipoAdqui(Label Lblbien)
        {
            string NumBien = Lblbien.Text;
            DataTable TablaFactura = new DataTable("Resultado");
            SqlConnection cnn = new SqlConnection(CConexion.Obtener());
            var query = @"select TipoAdqui from Cat_TipoAdquisicion 
                        inner join FichaCompra 
                        on Cat_TipoAdquisicion.IdTipoAdqui = FichaCompra.IdTipoAdqui 
                        inner join FichaBien
                        on FichaCompra.idCompra = FichaBien.idCompra
                        where NumInventario = @numBien";
            SqlCommand cmd = new SqlCommand(query, cnn);
            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Add("@numBien", SqlDbType.VarChar, 30).Value = NumBien;
            cmd.Connection = cnn;
            adp.SelectCommand = cmd;
            cnn.Open();
            adp.Fill(TablaFactura);
            cnn.Close();
            return TablaFactura;
        }

        /// <summary>
        /// Consulta para obtener las propiedades. 
        /// </summary>
        /// <param name="Lblbien"></param>
        /// <returns></returns>
        public static DataTable BuscarPropiedad(Label Lblbien)
        {
            string NumBien = Lblbien.Text;
            DataTable TablaFactura = new DataTable("Resultado");
            SqlConnection cnn = new SqlConnection(CConexion.Obtener());
            var query = @"select Propiedad from Cat_Propiedades 
                          inner join FichaCompra 
                          on Cat_Propiedades.IdPropiedad = FichaCompra.IdPropiedad 
                          inner join FichaBien
                          on FichaCompra.idCompra = FichaBien.idCompra
                          where NumInventario = @numBien";
            SqlCommand cmd = new SqlCommand(query, cnn);
            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Add("@numBien", SqlDbType.VarChar, 30).Value = NumBien;
            cmd.Connection = cnn;
            adp.SelectCommand = cmd;
            cnn.Open();
            adp.Fill(TablaFactura);
            cnn.Close();
            return TablaFactura;
        }

        /// <summary>
        /// Consulta para obtener los tipos de partida. 
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
        /// Consulta para obtener las ubicaciones de los Bienes. 
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
        /// <param name="Lblbien"></param>
        /// <returns></returns>
        public static DataTable idCompra(Label Lblbien)
        {
            string NumBien = Lblbien.Text;
            SqlConnection con = new SqlConnection(CConexion.Obtener());
            con.Open();
            string sqlCad1 = @"select NumDocumento from FichaCompra
                                inner join FichaBien
                                on FichaCompra.IdCompra = FichaBien.IdCompra
                                where NumInventario = @numBien";
            SqlCommand cmdMax1 = new SqlCommand(sqlCad1, con);
            cmdMax1.Parameters.Add("@numBien", SqlDbType.VarChar, 30).Value = NumBien;
            SqlDataReader leerMax1 = cmdMax1.ExecuteReader();
            string num;
            if (leerMax1.Read() == true)
            {
                num = leerMax1[0].ToString();
            }
            else
            {
                num = "";
            }
            con.Close();

            DataTable TablaCompra = new DataTable("Resultado");
            SqlConnection cnn = new SqlConnection(CConexion.Obtener());
            var query = @"select IdCompra from FichaCompra where NumDocumento = @num";
            SqlCommand cmd = new SqlCommand(query, cnn);
            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@num", num);
            cmd.Connection = cnn;
            adp.SelectCommand = cmd;
            cnn.Open();
            adp.Fill(TablaCompra);
            cnn.Close();
            return TablaCompra;
        }

        /// <summary>
        /// Consulta para obtener las areas de resguardo. 
        /// </summary>
        /// <returns></returns>
        public static List<CModificacionBien> ObtenerAreaDeResguardoInicial()
        {
            List<CModificacionBien> listaAreaResguardo = new List<CModificacionBien>();

            SqlConnection cnn = new SqlConnection(CConexion.Obtener());
            SqlCommand cmd = new SqlCommand("SELECT Nombre, IdSeccion FROM Cat_Secciones WHERE TipoSeccion = 'R' ORDER BY Nombre ", cnn);
            cnn.Open();

            using (var rd = cmd.ExecuteReader())
            {
                while (rd.Read())
                {
                    CModificacionBien cModificacionBien = new CModificacionBien();
                    cModificacionBien.Nombre = rd.GetString(0);
                    cModificacionBien.IdSeccion = rd.GetInt32(1);
                    listaAreaResguardo.Add(cModificacionBien);
                }
            }
            return listaAreaResguardo;
        }

        /// <summary>
        /// Consulta para obtener las partidas. 
        /// </summary>
        /// <returns></returns>
        public static List<CModificacionBien> ObtenerPartida(string TipoPartida)
        {
            List<CModificacionBien> listaPartida = new List<CModificacionBien>();

            SqlConnection cnn = new SqlConnection(CConexion.Obtener());
            SqlCommand cmd = new SqlCommand("SELECT Partida, IdPartida FROM Cat_Partidas WHERE (TipoPartida = @TipoPartida) ORDER BY Partida", cnn);
            cmd.Parameters.Add("@TipoPartida", SqlDbType.Char).Value = TipoPartida;
            cnn.Open();

            using (var rd = cmd.ExecuteReader())
            {
                while (rd.Read())
                {
                    CModificacionBien cModificacionBien = new CModificacionBien();
                    cModificacionBien.Partida = rd.GetString(0);
                    cModificacionBien.idPartida = rd.GetInt32(1);
                    listaPartida.Add(cModificacionBien);
                }
            }
            return listaPartida;
        }

        /// <summary>
        /// Consulta para obtener las subclases. 
        /// </summary>
        /// <returns></returns>
        public static List<CModificacionBien> ObtenerSubClase(string TipoPartida)
        {
            List<CModificacionBien> listaSubClase = new List<CModificacionBien>();

            SqlConnection cnn = new SqlConnection(CConexion.Obtener());
            SqlCommand cmd = new SqlCommand("SELECT Cat_SubClase.IdSubClase, Cat_SubClase.SubClase FROM Cat_SubClase INNER JOIN Cat_Partidas ON Cat_SubClase.IdPartida = Cat_Partidas.IdPartida WHERE (Cat_Partidas.TipoPartida = @TipoPartida) ORDER BY Cat_SubClase.SubClase", cnn);
            cmd.Parameters.Add("@TipoPartida", SqlDbType.Char).Value = TipoPartida;
            cnn.Open();

            using (var rd = cmd.ExecuteReader())
            {
                while (rd.Read())
                {
                    CModificacionBien cModificacionBien = new CModificacionBien();
                    cModificacionBien.idSubClase = rd.GetInt32(0);
                    cModificacionBien.SubClase = rd.GetString(1);
                    listaSubClase.Add(cModificacionBien);
                }
            }
            return listaSubClase;
        }

        /// <summary>
        /// Consulta para obtener la lista de CONAC. 
        /// </summary>
        /// <returns></returns>
        public static List<CModificacionBien> ObtenerCONAC(string TipoPartida)
        {
            List<CModificacionBien> listaCONAC = new List<CModificacionBien>();

            SqlConnection cnn = new SqlConnection(CConexion.Obtener());
            SqlCommand cmd = new SqlCommand("SELECT Descripcion, IdCONAC FROM Cat_CONAC WHERE (TipoPartida = @TipoPartida) ORDER BY Descripcion", cnn);
            cmd.Parameters.Add("@TipoPartida", SqlDbType.Char).Value = TipoPartida;
            cnn.Open();

            using (var rd = cmd.ExecuteReader())
            {
                while (rd.Read())
                {
                    CModificacionBien cModificacionBien = new CModificacionBien();
                    cModificacionBien.Descripcion = rd.GetString(0);
                    cModificacionBien.IdCONAC = rd.GetInt32(1);
                    listaCONAC.Add(cModificacionBien);
                }
            }
            return listaCONAC;
        }

        /// <summary>
        /// Consulta para obtener las marcas. 
        /// </summary>
        /// <returns></returns>
        public static List<CModificacionBien> ObtenerMarca()
        {
            List<CModificacionBien> listaMarca = new List<CModificacionBien>();

            SqlConnection cnn = new SqlConnection(CConexion.Obtener());
            SqlCommand cmd = new SqlCommand("select Descripcion, IdMarca from Cat_Marca ORDER BY Descripcion", cnn);
            cnn.Open();

            using (var rd = cmd.ExecuteReader())
            {
                while (rd.Read())
                {
                    CModificacionBien cModificacionBien = new CModificacionBien();
                    cModificacionBien.Descripcion = rd.GetString(0);
                    cModificacionBien.IdMarca = rd.GetInt32(1);
                    listaMarca.Add(cModificacionBien
);
                }
            }
            return listaMarca;
        }

        /// <summary>
        /// Consulta para obtener la inormación del Bien. 
        /// </summary>
        /// <param name="Lblbien"></param>
        /// <returns></returns>
        public static DataTable BuscarBien(Label Lblbien)
        {
            string NumBien = Lblbien.Text;
            DataTable TablaFactura = new DataTable("Resultado");
            SqlConnection cnn = new SqlConnection(CConexion.Obtener());
            var query = @"select * from FichaBien where NumInventario = @numBien";
            SqlCommand cmd = new SqlCommand(query, cnn);
            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Add("@numBien", SqlDbType.VarChar, 30).Value = NumBien;
            cmd.Connection = cnn;
            adp.SelectCommand = cmd;
            cnn.Open();
            adp.Fill(TablaFactura);
            cnn.Close();
            return TablaFactura;
        }

        /// <summary>
        /// Consulta para obtener las subclases. 
        /// </summary>
        /// <param name="Lblbien"></param>
        /// <returns></returns>
        public static DataTable BuscarSubClase(Label Lblbien)
        {
            string NumBien = Lblbien.Text;
            DataTable TablaFactura = new DataTable("Resultado");
            SqlConnection cnn = new SqlConnection(CConexion.Obtener());
            var query = @"select SubClase from Cat_SubClase
                          inner join FichaBien
                          on FichaBien.IdSubClase = Cat_SubClase.IdSubClase
                          where NumInventario = @numBien";
            SqlCommand cmd = new SqlCommand(query, cnn);
            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Add("@numBien", SqlDbType.VarChar, 30).Value = NumBien;
            cmd.Connection = cnn;
            adp.SelectCommand = cmd;
            cnn.Open();
            adp.Fill(TablaFactura);
            cnn.Close();
            return TablaFactura;
        }

        /// <summary>
        /// Consulta para obtener la lista de CONAC. 
        /// </summary>
        /// <param name="Lblbien"></param>
        /// <returns></returns>
        public static DataTable BuscarCONAC(Label Lblbien)
        {
            string NumBien = Lblbien.Text;
            DataTable TablaFactura = new DataTable("Resultado");
            SqlConnection cnn = new SqlConnection(CConexion.Obtener());
            var query = @"select Descripcion from Cat_CONAC
                          inner join FichaBien
                          on FichaBien.idCONAC = Cat_CONAC.IdCONAC
                          where NumInventario = @numBien";
            SqlCommand cmd = new SqlCommand(query, cnn);
            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Add("@numBien", SqlDbType.VarChar, 30).Value = NumBien;
            cmd.Connection = cnn;
            adp.SelectCommand = cmd;
            cnn.Open();
            adp.Fill(TablaFactura);
            cnn.Close();
            return TablaFactura;
        }

        /// <summary>
        /// Consulta para obtener los Id de CONAC. 
        /// </summary>
        /// <param name="Lblbien"></param>
        /// <returns></returns>
        public static DataTable BuscarIdCONAC(Label Lblbien)
        {
            string NumBien = Lblbien.Text;
            DataTable TablaFactura = new DataTable("Resultado");
            SqlConnection cnn = new SqlConnection(CConexion.Obtener());
            var query = @"select idCONAC from FichaBien where NumInventario = @numBien";
            SqlCommand cmd = new SqlCommand(query, cnn);
            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Add("@numBien", SqlDbType.VarChar, 30).Value = NumBien;
            cmd.Connection = cnn;
            adp.SelectCommand = cmd;
            cnn.Open();
            adp.Fill(TablaFactura);
            cnn.Close();
            return TablaFactura;
        }

        /// <summary>
        /// Consulta para obtener las Marcas. 
        /// </summary>
        /// <param name="Lblbien"></param>
        /// <returns></returns>
        public static DataTable BuscarMarca(Label Lblbien)
        {
            string NumBien = Lblbien.Text;
            DataTable TablaFactura = new DataTable("Resultado");
            SqlConnection cnn = new SqlConnection(CConexion.Obtener());
            var query = @" select Descripcion from Cat_Marca
                              inner join FichaBien
                              on FichaBien.IdMarca = Cat_Marca.idMarca
                              where NumInventario = @numBien";
            SqlCommand cmd = new SqlCommand(query, cnn);
            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Add("@numBien", SqlDbType.VarChar, 30).Value = NumBien;
            cmd.Connection = cnn;
            adp.SelectCommand = cmd;
            cnn.Open();
            adp.Fill(TablaFactura);
            cnn.Close();
            return TablaFactura;
        }

        /// <summary>
        /// Consult para obtener las areas de resguardo. 
        /// </summary>
        /// <param name="Lblbien"></param>
        /// <returns></returns>
        public static DataTable BuscarAreaResguardo(Label Lblbien)
        {
            string NumBien = Lblbien.Text;
            DataTable TablaFactura = new DataTable("Resultado");
            //SqlConnection cnn = new SqlConnection(CConexion.Obtener());
            //var query = @"select Nombre from Cat_Secciones
            //              inner join FichaBien
            //              on FichaBien.IdENSU = Cat_Secciones.IdSeccion
            //              where NumInventario = @numBien";

            SqlConnection cnn = new SqlConnection(CConexion.Obtener());
            var query = @"SELECT Cat_Secciones.Nombre FROM Cat_Secciones INNER JOIN 
                         Cat_ENSUbicacion ON Cat_Secciones.IdSeccion = Cat_ENSUbicacion.IdSeccion INNER JOIN
                         FichaBien ON Cat_ENSUbicacion.IdENSU = FichaBien.IdENSU
                         WHERE FichaBien.NumInventario = @numBien";
            SqlCommand cmd = new SqlCommand(query, cnn);
            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Add("@numBien", SqlDbType.VarChar, 30).Value = NumBien;
            cmd.Connection = cnn;
            adp.SelectCommand = cmd;
            cnn.Open();
            adp.Fill(TablaFactura);
            cnn.Close();
            return TablaFactura;
        }

        /// <summary>
        /// Consulta para obtener la descripción de la CONAC seleccionada. 
        /// </summary>
        /// <param name="Lblbien"></param>
        /// <returns></returns>
        public static DataTable BuscarGSC(Label Lblbien)
        {
            string NumBien = Lblbien.Text;
            SqlConnection con = new SqlConnection(CConexion.Obtener());
            con.Open();
            string cadSql = @"select Descripcion from Cat_CONAC
                          inner join FichaBien
                          on FichaBien.IdCONAC = Cat_CONAC.IdCONAC
                          where NumInventario = @numBien";
            SqlCommand cmd1 = new SqlCommand(cadSql, con);
            cmd1.Parameters.Add("@numBien", SqlDbType.VarChar, 30).Value = NumBien;
            SqlDataReader leer = cmd1.ExecuteReader();
            string Desc;
            if (leer.Read() == true)
            {
                Desc = leer[0].ToString();
            }
            else
            {
                Desc = "";
            }
            con.Close();

            DataTable TablaFactura = new DataTable("Resultado");
            SqlConnection cnn = new SqlConnection(CConexion.Obtener());
            var query = @"select * from Cat_CONAC where Descripcion = @dato";
            SqlCommand cmd = new SqlCommand(query, cnn);
            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@dato", Desc);
            cmd.Connection = cnn;
            adp.SelectCommand = cmd;
            cnn.Open();
            adp.Fill(TablaFactura);
            cnn.Close();
            return TablaFactura;
        }

        /// <summary>
        /// Consulta para obtener las partidas. 
        /// </summary>
        /// <param name="Lblbien"></param>
        /// <returns></returns>
        public static DataTable BuscarPartida(Label Lblbien)
        {
            string NumBien = Lblbien.Text;
            DataTable TablaFactura = new DataTable("Resultado");
            SqlConnection cnn = new SqlConnection(CConexion.Obtener());
            var query = @"select Partida from Cat_Partidas
                          inner join Cat_SubClase
                          on Cat_Partidas.IdPartida = Cat_SubClase.idPartida
                          inner join FichaBien
                          on FichaBien.IdSubClase = Cat_SubClase.IdSubClase
                          where NumInventario = @numBien";
            SqlCommand cmd = new SqlCommand(query, cnn);
            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Add("@numBien", SqlDbType.VarChar, 30).Value = NumBien;
            cmd.Connection = cnn;
            adp.SelectCommand = cmd;
            cnn.Open();
            adp.Fill(TablaFactura);
            cnn.Close();
            return TablaFactura;
        }

        /// <summary>
        /// Consulta para obtener la información de la tabla depreciar. 
        /// </summary>
        /// <param name="Lblbien"></param>
        /// <returns></returns>
        public static DataTable InfoDepreciar(Label Lblbien)
        {
            string NumBien = Lblbien.Text;
            DataTable TablaFactura = new DataTable("Resultado");
            SqlConnection cnn = new SqlConnection(CConexion.Obtener());
            var query = @"  select VidaUtil from Depreciar 
                            where NumInventario = @numBien";
            SqlCommand cmd = new SqlCommand(query, cnn);
            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Add("@numBien", SqlDbType.VarChar, 30).Value = NumBien;
            cmd.Connection = cnn;
            adp.SelectCommand = cmd;
            cnn.Open();
            adp.Fill(TablaFactura);
            cnn.Close();
            return TablaFactura;
        }

        /// <summary>
        /// Consulta para obtener información de l atabla dimención de los Bienes. 
        /// </summary>
        /// <param name="Lblbien"></param>
        /// <returns></returns>
        public static DataTable InfoDimencion(Label Lblbien)
        {
            string NumBien = Lblbien.Text;
            SqlConnection con = new SqlConnection(CConexion.Obtener());
            con.Open();
            string sqlCad1 = @"select IdInventario from FichaBien 
                                where NumInventario = @numBien";
            SqlCommand cmdMax1 = new SqlCommand(sqlCad1, con);
            cmdMax1.Parameters.Add("@numBien", SqlDbType.VarChar, 30).Value = NumBien;
            SqlDataReader leerMax1 = cmdMax1.ExecuteReader();
            string numInven;
            if (leerMax1.Read() == true)
            {
                numInven = leerMax1[0].ToString();
            }
            else
            {
                numInven = "";
            }
            con.Close();

            DataTable TablaFactura = new DataTable("Resultado");
            SqlConnection cnn = new SqlConnection(CConexion.Obtener());
            var query = @"  select * from DimencionBien 
                            where IdInventario = @numBien";
            SqlCommand cmd = new SqlCommand(query, cnn);
            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Add("@numBien", SqlDbType.VarChar, 30).Value = numInven;
            cmd.Connection = cnn;
            adp.SelectCommand = cmd;
            cnn.Open();
            adp.Fill(TablaFactura);
            cnn.Close();
            return TablaFactura;
        }

        /// <summary>
        /// Consulta para obtener información de los Bienes de la tabla FichaBien. 
        /// </summary>
        /// <param name="Lblbien"></param>
        /// <param name="sender"></param>
        /// <param name="GridModificar"></param>
        /// <returns></returns>
        public static DataTable FichaBienInfo(Label Lblbien, object sender, GridView GridModificar)
        {
            int rowIndex = ((sender as ImageButton).NamingContainer as GridViewRow).RowIndex;
            Int64 id = Convert.ToInt64(GridModificar.DataKeys[rowIndex].Values[0]);

            string Bien = Convert.ToString(id);
            DataTable TablaFactura = new DataTable("Resultado");
            SqlConnection cnn = new SqlConnection(CConexion.Obtener());
            var query = @"select * from FichaBien where NumInventario = @numBien";
            SqlCommand cmd = new SqlCommand(query, cnn);
            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Add("@numBien", SqlDbType.VarChar, 30).Value = Bien;
            cmd.Connection = cnn;
            adp.SelectCommand = cmd;
            cnn.Open();
            adp.Fill(TablaFactura);
            cnn.Close();
            return TablaFactura;
        }
    }

}
