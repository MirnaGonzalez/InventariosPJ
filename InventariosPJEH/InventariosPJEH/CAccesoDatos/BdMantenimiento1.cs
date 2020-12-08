using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using InventariosPJEH.CNegocios;

namespace InventariosPJEH.CAccesoDatos
{
    public class BdMantenimiento
    {
        public static int InsertarRecibirBien(CMantenimientoR Mantenimiento, ref TResultado ResAgragarMantenimiento)
        {
            int IdRegistroM = -1;
            SqlCommand insert = new SqlCommand("[dbo].[stp_Mantenimiento_Agregar]", new SqlConnection(CConexion.Obtener()));
            insert.CommandType = CommandType.StoredProcedure;

            insert.Parameters.AddWithValue("@idinventario", Mantenimiento.IdInventario);
            insert.Parameters.AddWithValue("@nombreRecibe", Mantenimiento.NombreRecibe);
            insert.Parameters.AddWithValue("@numOficio", Mantenimiento.NumOficio);
            insert.Parameters.AddWithValue("@nombreEntrega", Mantenimiento.NombreEntrega);
            insert.Parameters.AddWithValue("@sistemaOperativo", Mantenimiento.SistemaOperativo);
            insert.Parameters.AddWithValue("@cuentaOffice", Mantenimiento.CuentaOffice);
            insert.Parameters.AddWithValue("@ip", Mantenimiento.Ip);
            insert.Parameters.AddWithValue("@fallaReportada", Mantenimiento.FallaReportada);
            insert.Parameters.AddWithValue("@diagnostico", Mantenimiento.DiagnosticoReparacion);


            try
            {
                insert.Connection.Open();
                object Resultado = insert.ExecuteScalar();
                int.TryParse(Resultado.ToString(), out IdRegistroM);
                insert.Connection.Close();

                return IdRegistroM;
            }
            catch (Exception ex)
            {
                if(insert.Connection.State == ConnectionState.Open)
                    insert.Connection.Close();
                ResAgragarMantenimiento.Exito = false;
                ResAgragarMantenimiento.Mensaje.Add(ex.Message);

                return IdRegistroM;
            }
        }

        public static int ModificarFichaBienRegMant(Int64 NumInventario, int IdActividad, int IdRegistroMantenimiento, ref TResultado Resultado)
        {
            int RegModificados = -1;
            
            SqlCommand cmd = new SqlCommand("[dbo].[stp_FichaBien_Modificar]", new SqlConnection(CConexion.Obtener()));
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@NumInventario", SqlDbType.BigInt).Value = NumInventario;
            cmd.Parameters.Add("@IdActividad", SqlDbType.Int).Value = IdActividad;
            cmd.Parameters.Add("@IdRegMant", SqlDbType.Int).Value = IdRegistroMantenimiento;

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
    }
}