using InventariosPJEH.CNegocios;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace InventariosPJEH.CAccesoDatos
{
    public class BdBajaBien
    {
        public SqlConnection conexion;
        public string error;
        protected SqlDataAdapter adaptador;
        protected SqlDataReader reader;
        protected DataSet data;

        public BdBajaBien()
        {
            this.conexion = ConexionBD.getConexion();
        }

        /// <summary>
        /// Consulta para llenar el GridView con los datos del Bien
        /// </summary>
        /// <param name="TxtNoInventario"></param>
        /// <returns></returns>
        public static DataTable BuscarGrid(TextBox TxtNoInventario)
        {
            string numInven = TxtNoInventario.Text;
            DataTable Tabla = new DataTable("Resultado");
            SqlConnection cnn = new SqlConnection(CConexion.Obtener());
            var query = @"select * from VistaConsultarBienes where NumInventario like @numInven";
            SqlCommand cmd = new SqlCommand(query, cnn);
            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Add("@numInven", SqlDbType.VarChar).Value = numInven;
            cmd.Connection = cnn;
            adp.SelectCommand = cmd;
            cnn.Open();
            adp.Fill(Tabla);
            cnn.Close();
            return Tabla;
        }

        /// <summary>
        /// Consulta para obtener los datos de la tabla Fichabien y poder actualizar el Estatus del Bien 
        /// </summary>
        /// <param name="Lblbien"></param>
        /// <param name="sender"></param>
        /// <param name="GridBienes"></param>
        /// <returns></returns>
        public static DataTable FichaBienInfo(Label Lblbien, object sender, GridView GridBienes)
        {
            int rowIndex = ((sender as ImageButton).NamingContainer as GridViewRow).RowIndex;
            Int64 id = Convert.ToInt64(GridBienes.DataKeys[rowIndex].Values[0]);
            string Bien = Convert.ToString(id);

            string NumBien = Lblbien.Text;
            DataTable TablaBien = new DataTable("Resultado");
            SqlConnection cnn = new SqlConnection(CConexion.Obtener());
            var query = @"select * from FichaBien where NumInventario = @numBien";
            SqlCommand cmd = new SqlCommand(query, cnn);
            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Add("@numBien", SqlDbType.VarChar, 30).Value = Bien;
            cmd.Connection = cnn;
            adp.SelectCommand = cmd;
            cnn.Open();
            adp.Fill(TablaBien);
            cnn.Close();
            return TablaBien;
        }

        /// <summary>
        /// Consulta para obtener los datos de la tabla Ubicacion y poder actualizar el Estatus del Bien 
        /// </summary>
        /// <param name="Lblbien"></param>
        /// <param name="sender"></param>
        /// <param name="GridBienes"></param>
        /// <returns></returns>
        public static DataTable Ubicacion(Label Lblbien, object sender, GridView GridBienes)
        {
            int rowIndex = ((sender as ImageButton).NamingContainer as GridViewRow).RowIndex;
            Int64 id = Convert.ToInt64(GridBienes.DataKeys[rowIndex].Values[0]);
            string Bien = Convert.ToString(id);

            SqlConnection con = new SqlConnection(CConexion.Obtener());
            con.Open();
            string sqlCad1 = @"select idInventario from FichaBien where NumInventario = @numBien";
            SqlCommand cmdMax1 = new SqlCommand(sqlCad1, con);
            cmdMax1.Parameters.Add("@numBien", SqlDbType.VarChar, 30).Value = Bien;
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
            var query = @"select * from Ubicacion where IdInventario = @num";
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
        /// Consulta para obtener los datos de la tabla HistoricoUbicacion y poder insertar el Bien
        /// </summary>
        /// <param name="Lblbien"></param>
        /// <param name="sender"></param>
        /// <param name="GridBienes"></param>
        /// <returns></returns>
        public static DataTable HistoricoUbicacion(Label Lblbien, object sender, GridView GridBienes)
        {
            int rowIndex = ((sender as ImageButton).NamingContainer as GridViewRow).RowIndex;
            Int64 id = Convert.ToInt64(GridBienes.DataKeys[rowIndex].Values[0]);
            string Bien = Convert.ToString(id);

            SqlConnection con = new SqlConnection(CConexion.Obtener());
            con.Open();
            string sqlCad1 = @"select idInventario from FichaBien where NumInventario = @numBien";
            SqlCommand cmdMax1 = new SqlCommand(sqlCad1, con);
            cmdMax1.Parameters.Add("@numBien", SqlDbType.VarChar, 30).Value = Bien;
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
            var query = @"select * from HistoricoUbicacion where IdInventario = @num";
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

    }

}
