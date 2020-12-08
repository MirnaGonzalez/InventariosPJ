using System;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using InventariosPJEH.CNegocios;
using InventariosPJEH.CAccesoDatos;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using Microsoft.Office.Interop.Excel;

namespace InventariosPJEH.CAccesoDatos
{
    /// <summary>
    /// Summary description for Class1
    /// </summary>
    public class BdReparaBien
    {
        public SqlConnection conexion;
        public string error;
        protected SqlDataAdapter adaptador;
        protected SqlDataReader reader;
        protected DataSet data;
        /// <summary>
        /// Lista todos los bienes a reparar relacionados con el numero de inventario
        /// </summary>
        /// <param name="nNumeroInventario"></param>
        /// <returns></returns>
        public static List<CReparaBien> ConsultarGbBienesReparar(string nNumeroInventario)
        {
            List<CReparaBien> lista = new List<CReparaBien>();
            SqlConnection cnn = new SqlConnection(CConexion.Obtener());
            try
            {
                SqlCommand cmd = new SqlCommand(string.Format("select IdInventario,NumInventario,DescripcionBien, Marca, Modelo,Serie, NombrePersona,IdActividad,IdENSU,IdUsuario " +
                    "from VistaConsultarBienes where NumInventario like '%{0}%' and IdActividad=3 ORDER BY NumInventario ASC", nNumeroInventario), cnn);

                cnn.Open();

                using (var rd = cmd.ExecuteReader())
                { 
                    while (rd.Read())
                    {
                        CReparaBien crepara = new CReparaBien();

                        crepara.IdInventario = rd["IdInventario"].ToString();
                        crepara.NumInventario = rd["NumInventario"].ToString();
                        crepara.DescripcionBien = rd["DescripcionBien"].ToString();
                        crepara.Marca = rd["Marca"].ToString();
                        crepara.Modelo = rd["Modelo"].ToString();
                        crepara.Serie = rd["Serie"].ToString();
                        crepara.NombrePersona = rd["NombrePersona"].ToString();
                        crepara.IdActividad = rd["IdActividad"].ToString();
                        crepara.IdENSU = rd["IdENSU"].ToString();
                        crepara.IdUsuario = rd["IdUsuario"].ToString();

                        lista.Add(crepara);
                    }
                }
            }
            catch (Exception)
            {
                cnn.Close();
            }
            return lista;
        }
        public static List<CReparaBien> ConsultarGbBienesReparar2(string nNumeroSerie)
        {
            List<CReparaBien> lista = new List<CReparaBien>();

            SqlConnection cnn = new SqlConnection(CConexion.Obtener());
            try
            {
                SqlCommand cmd = new SqlCommand(string.Format("select IdInventario, NumInventario, DescripcionBien, Marca, Modelo, Serie, NombrePersona, IdActividad, IdENSU, IdUsuario " +
                    "from VistaConsultarBienes where Serie like '%{0}%' and IdActividad=3 ORDER BY Serie ASC", nNumeroSerie), cnn);

                cnn.Open();

                using (var rd = cmd.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        CReparaBien crepara = new CReparaBien();
                        crepara.IdInventario = rd["IdInventario"].ToString();
                        crepara.NumInventario = rd["NumInventario"].ToString();
                        crepara.DescripcionBien = rd["DescripcionBien"].ToString();
                        crepara.Marca = rd["Marca"].ToString();
                        crepara.Modelo = rd["Modelo"].ToString();
                        crepara.Serie = rd["Serie"].ToString();
                        crepara.NombrePersona = rd["NombrePersona"].ToString();
                        crepara.IdActividad = rd["IdActividad"].ToString();
                        crepara.IdENSU = rd["IdENSU"].ToString();
                        crepara.IdUsuario = rd["IdUsuario"].ToString();

                        lista.Add(crepara);
                    }
                }
            }
            catch (Exception)
            {
                cnn.Close();
            }
            return lista;
        }
        public static List<CBienSinReparar> ConsultarGbBienesSinReparar(string nNumeroInventario2)
        {
            List<CBienSinReparar> lista = new List<CBienSinReparar>();
            SqlConnection cnn = new SqlConnection(CConexion.Obtener());
            try
            {
                SqlCommand cmd = new SqlCommand(string.Format("select IdInventario,NumInventario,DescripcionBien, Marca, Modelo,Serie," +
                    "FechaAdquisicion, IdActividad,IdUsuario,IdENSU,TipoPartida " +
                    "from VistaConsultarBienes where (NumInventario like '%{0}%')  and (IdActividad=8) and (TipoPartida=1) ORDER BY NumInventario ASC", nNumeroInventario2), cnn);

                cnn.Open();

                using (var rd = cmd.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        CBienSinReparar csinrepara = new CBienSinReparar();
                        csinrepara.IdInventario = rd["IdInventario"].ToString();
                        csinrepara.NumInventario = rd["NumInventario"].ToString();
                        csinrepara.DescripcionBien = rd["DescripcionBien"].ToString();
                        csinrepara.Marca = rd["Marca"].ToString();
                        csinrepara.Modelo = rd["Modelo"].ToString();
                        csinrepara.Serie = rd["Serie"].ToString();
                        csinrepara.IdActividad = rd["IdActividad"].ToString();
                        csinrepara.FechaAdquisicion = rd["FechaAdquisicion"].ToString();
                        csinrepara.IdUsuario = rd["IdUsuario"].ToString();
                        csinrepara.IdENSU = rd["IdENSU"].ToString();
                        csinrepara.IdENSU = rd["TipoPartida"].ToString();

                        lista.Add(csinrepara);
                    }
                }
            }
            catch (Exception)
            {
                cnn.Close();
            }
            return lista;
        }
        public static List<CMantenimiento> ConsultaMantenimiento(string nRegistro)
        {
            List<CMantenimiento> lista = new List<CMantenimiento>();
            SqlConnection cnn = new SqlConnection(CConexion.Obtener());
            try
            {
                SqlCommand cmd = new SqlCommand(string.Format("select * FROM Mantenimiento", nRegistro), cnn);           
                cnn.Open();

                using (var rd = cmd.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        CMantenimiento cman = new CMantenimiento();
                        cman.IdRegMant = rd["IdRegMant"].ToString();
                        cman.IdInventario = rd["IdInventario"].ToString();
                        cman.NombreEntrega = rd["NombreEntrega"].ToString();
                        cman.NumOficio = rd["NumOficio"].ToString();
                        cman.NombreRecibe = rd["NombreRecibe"].ToString();
                        cman.SistemaOperativo = rd["SistemaOperativo"].ToString();
                        cman.CuentaOffice = rd["CuentaOffice"].ToString();
                        cman.Ip = rd["Ip"].ToString();
                        cman.FallaReportada = rd["FallaReportada"].ToString();
                        cman.DiagnosticoReparacion = rd["DiagnosticoReparacion"].ToString();

                        lista.Add(cman);
                    }
                }
            }
            catch (Exception)
            {
                cnn.Close();
            }
            return lista;
        }
        public static List<CMantenimiento> ObtenerReporteXIdInventario(int IdInventario)
        {
            List<CMantenimiento> LMantenimiento = new List<CMantenimiento>();

            SqlConnection cnn = new SqlConnection(CConexion.Obtener());
            SqlCommand cmd = new SqlCommand();
            SqlDataReader rd;
            cmd.Connection = cnn;
            cmd.CommandText = " SELECT * ";
            cmd.CommandText += " FROM dbo.VistaReparaBien ";
            cmd.CommandText += " WHERE (IdInventario = @ida)";
            cmd.Parameters.Add("@ida", SqlDbType.BigInt).Value = IdInventario;
            try
            {
                cnn.Open();
                rd = cmd.ExecuteReader();

                while (rd.Read())
                {
                    CMantenimiento  mantenimiento = new CMantenimiento();
                    mantenimiento.IdInventario = BdConverter.FieldToString(rd["IdInventario"]);
                    mantenimiento.NumInventario = BdConverter.FieldToString(rd["NumInventario"]);
                    mantenimiento.DescripcionBien = BdConverter.FieldToString(rd["DescripcionBien"]);
                    mantenimiento.Modelo = BdConverter.FieldToString(rd["Modelo"]);
                    mantenimiento.Serie = BdConverter.FieldToString(rd["Serie"]);               
               
                    mantenimiento.NombreEntrega = BdConverter.FieldToString(rd["NombreEntrega"]);
                    mantenimiento.NumOficio = BdConverter.FieldToString(rd["NumOficio"]);
                    mantenimiento.NombreRecibe = BdConverter.FieldToString(rd["NombreRecibe"]);
                    mantenimiento.SistemaOperativo = BdConverter.FieldToString(rd["SistemaOperativo"]);
                    mantenimiento.CuentaOffice = BdConverter.FieldToString(rd["CuentaOffice"]);
                    mantenimiento.Ip = BdConverter.FieldToString(rd["Ip"]);
                    mantenimiento.FallaReportada = BdConverter.FieldToString(rd["FallaReportada"]);
                    mantenimiento.DiagnosticoReparacion = BdConverter.FieldToString(rd["DiagnosticoReparacion"]);
                    
                    mantenimiento.NumTarjetaInf = BdConverter.FieldToString(rd["NumTarjetaInf"]);
                    mantenimiento.NumRefaccion = BdConverter.FieldToString(rd["NumRefaccion"]);
                    mantenimiento.DescripcionRefaccion = BdConverter.FieldToString(rd["DescripcionRefaccion"]);
                    mantenimiento.Marca = BdConverter.FieldToString(rd["Marca"]);
                    mantenimiento.Seccion = BdConverter.FieldToString(rd["Seccion"]);

                    LMantenimiento.Add(mantenimiento);
                }
                cmd.Connection.Close();
            }
            catch (Exception)
            {
                if (cmd.Connection.State != ConnectionState.Closed)
                    cmd.Connection.Close();
            }

            return LMantenimiento;
        }
        public static List<CTarjeta> ObtenerTarjetaXIdInventario(int IdInventarios)
        {
            List<CTarjeta> LTarjeta = new List<CTarjeta>();

            SqlConnection cnn = new SqlConnection(CConexion.Obtener());
            SqlCommand cmd = new SqlCommand();
            SqlDataReader rd;
            cmd.Connection = cnn;
            cmd.CommandText = " SELECT * ";
            cmd.CommandText += " FROM dbo.VistaReporteTarjeta ";
            cmd.CommandText += " WHERE (IdInventario = @idv)";
            cmd.Parameters.Add("@idv", SqlDbType.BigInt).Value = IdInventarios;
            try
            {
                cnn.Open();
                rd = cmd.ExecuteReader();

                while (rd.Read())
                {
                    CTarjeta tarjetainfo = new CTarjeta();
                    tarjetainfo.IdInventario = BdConverter.FieldToString(rd["IdInventario"]);
                    tarjetainfo.NumInventario = BdConverter.FieldToString(rd["NumInventario"]);
                    tarjetainfo.DescripcionBien = BdConverter.FieldToString(rd["DescripcionBien"]);
                    tarjetainfo.Modelo = BdConverter.FieldToString(rd["Modelo"]);
                    tarjetainfo.Serie = BdConverter.FieldToString(rd["Serie"]);                    
                    tarjetainfo.NombreEntrega = BdConverter.FieldToString(rd["NombreEntrega"]);
                    tarjetainfo.NumOficio = BdConverter.FieldToString(rd["NumOficio"]);
                    tarjetainfo.NombreRecibe = BdConverter.FieldToString(rd["NombreRecibe"]);
                    tarjetainfo.SistemaOperativo = BdConverter.FieldToString(rd["SistemaOperativo"]);
                    tarjetainfo.CuentaOffice = BdConverter.FieldToString(rd["CuentaOffice"]);
                    tarjetainfo.Ip = BdConverter.FieldToString(rd["Ip"]);
                    tarjetainfo.FallaReportada = BdConverter.FieldToString(rd["FallaReportada"]);
                    tarjetainfo.DiagnosticoReparacion = BdConverter.FieldToString(rd["DiagnosticoReparacion"]);                    
                    tarjetainfo.NumTarjetaInf = BdConverter.FieldToString(rd["NumTarjetaInf"]);
                    tarjetainfo.NumRefaccion = BdConverter.FieldToString(rd["NumRefaccion"]);
                    tarjetainfo.DescripcionRefaccion = BdConverter.FieldToString(rd["DescripcionRefaccion"]);
                    tarjetainfo.Marca = BdConverter.FieldToString(rd["Marca"]);
                    tarjetainfo.Seccion = BdConverter.FieldToString(rd["Seccion"]);
                    tarjetainfo.Area = BdConverter.FieldToString(rd["Area"]);
                    tarjetainfo.IdActividad = BdConverter.FieldToString(rd["IdActividad"]);


                    LTarjeta.Add(tarjetainfo);
                }
                cmd.Connection.Close();
            }
            catch (Exception)
            {
                if (cmd.Connection.State != ConnectionState.Closed)
                    cmd.Connection.Close();
            }

            return LTarjeta;
        }
        public static List<CPiezas> ConsultaPiezas(string nNumeroo)
        {
            List<CPiezas> lista = new List<CPiezas>();

            SqlConnection cnn = new SqlConnection(CConexion.Obtener());
            try
            {
                SqlCommand cmd = new SqlCommand(string.Format("select IdInventario,NumTarjetaInf,NumRefaccion,DescripcionRefaccion,IdRegMant FROM PiezaReparacion",nNumeroo), cnn);
          
                cnn.Open();

                using (var rd = cmd.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        CPiezas cpieza = new CPiezas();

                        cpieza.IdInventario = rd["IdInventario"].ToString();
                        cpieza.NumTarjetaInf = rd["NumTarjetaInf"].ToString();
                        cpieza.NumRefaccion = rd["NumRefaccion"].ToString();
                        cpieza.DescripcionRefaccion= rd["DescripcionRefaccion"].ToString();
                        cpieza.IdRegMant = rd["IdRegMant"].ToString();

                        lista.Add(cpieza);
                    }
                }
            }
            catch (Exception)
            {
                cnn.Close();
            }
            return lista;
        }
        public void Quitar_Bien(CReparaBien obJE)
        {

            SqlConnection Conn = new SqlConnection(CConexion.Obtener());
            bool success = false;
            SqlTransaction lTransaccion = null;
            int Valor_Retornado = 0;

            try
            {
                Conn.Open();
                lTransaccion = Conn.BeginTransaction(System.Data.IsolationLevel.Serializable);
                SqlCommand cmd = new SqlCommand("SP_Eliminar_BienMantenimiento", Conn, lTransaccion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@idInventario", obJE.IdInventario);
                SqlParameter ValorRetorno = new SqlParameter("@Comprobacion", SqlDbType.Int);
                ValorRetorno.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(ValorRetorno);
                cmd.ExecuteNonQuery();
                Valor_Retornado = Convert.ToInt32(ValorRetorno.Value);
                if (Valor_Retornado == 1)
                    success = true;
            }
            catch (Exception)
            {
                Conn.Close();
            }
            finally
            {
                if (success)
                {
                    lTransaccion.Commit();
                    Conn.Close();
                }
                else
                {
                    lTransaccion.Rollback();
                    Conn.Close();
                }
            }
        }
    }
}
