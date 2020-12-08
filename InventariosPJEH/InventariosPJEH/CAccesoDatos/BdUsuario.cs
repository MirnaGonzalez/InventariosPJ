using InventariosPJEH.CNegocios;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace InventariosPJEH.CAccesoDatos
{

    public class BdUsuario
    {
        public SqlConnection conexion;
        public string error;


        public BdUsuario()
        {
            this.conexion = ConexionBD.getConexion();
        }



       public CUsuario ConsultarUsuario(string Username, string Password)
        {
            SqlConnection cnn = new SqlConnection(CConexion.Obtener());

            SqlCommand comando = new SqlCommand();
            comando.CommandType = CommandType.StoredProcedure;
            comando.CommandText = "SP_Autenticar_Usuarios";
            cnn.Open();
   
            comando.Connection = conexion;
         
            comando.Parameters.Add("@username", System.Data.SqlDbType.VarChar, 50).Value = Username;
            comando.Parameters.Add("@password", System.Data.SqlDbType.VarChar, 50).Value = Password;

            
            using (var registro = comando.ExecuteReader())
            {

                if (registro.Read())
                {
                  
                    CUsuario usuario = new CUsuario();
                    usuario.Username = registro.GetString(0);
                    usuario.Password = registro.GetString(1);
                 
                    registro.Close();
                    return usuario;
                }
                else
                {
                    registro.Close();
                    return null;
                }
            }
        }

        /// <summary>
        /// Realizal autenticacion de un Usuairio y devuelve el resultado en Res
        /// -1: Se generó un error
        ///  0: Combinación de Usuario y/o Clave Incorrectas o Usuario Deshabilitado
        ///  1: Autenticación Exitosa 
        /// </summary>
        /// <param name="Username"></param>
        /// <param name="Password"></param>
        /// <returns></returns>
        public static int Autenticar(string Username, string Password, ref TResultado Res)
        {
            int ResultadoAutenticacion = -1;
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = new SqlConnection(CConexion.Obtener());

            try
            {
                               
         
                cmd.Connection.Open();
                cmd.CommandText = "SP_Autenticar_Usuarios";
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.Add("@username", System.Data.SqlDbType.VarChar, 50).Value = Username;
                cmd.Parameters.Add("@password", System.Data.SqlDbType.VarChar, 50).Value = Password;
                                           
              
                object autetnicacion = cmd.ExecuteScalar();
                int.TryParse(autetnicacion.ToString(), out ResultadoAutenticacion);
                cmd.Connection.Close();

                return 1;
            }
            catch (Exception e)
            {
                if (cmd.Connection.State == ConnectionState.Open)
                {
                    cmd.Connection.Close();
                    Res.Exito = false;
                    Res.Mensaje.Add("Se generó un error al tratar de realizar la Autenticación");
                    Res.Detalles = e.Message;

                    return -1;
                }


                if (ResultadoAutenticacion < 1)
                {
                    Res.Exito = false;
                    Res.Mensaje.Add("Se generó un error al tratar de realizar la Autenticación");
                    Res.Detalles = "La lamada al STP Actuario_Agregar se realizó correctamente pero este devolvio un -1. Indica error en la ejecución ";
                }

                return ResultadoAutenticacion;
            }
        }

        public static CUsuario ObtenerDatosUsuario(string NombreUsuario, ref TResultado Resultado)
       
        {
            SqlConnection cnn = new SqlConnection(CConexion.Obtener());
            cnn.Open();
            SqlCommand cmd = new SqlCommand(string.Format("Select * from Cat_Usuarios where Username like '%{0}%' ", NombreUsuario), cnn);
            SqlDataReader rd = cmd.ExecuteReader();
            CUsuario DatosUsuario = new CUsuario();

            try
            {
                
               
                if (rd.Read())
                {

                    DatosUsuario.AMaterno = rd["AMaterno"].ToString();
                    DatosUsuario.APaterno = rd["APaterno"].ToString();
                    DatosUsuario.Estatus = rd["Estatus"].ToString();
                    DatosUsuario.IdUsuario = rd["IdUsuario"].ToString();
                    DatosUsuario.Nombre = rd["Nombre"].ToString();
                    DatosUsuario.Password = rd["Password"].ToString();
                    DatosUsuario.Perfil = rd["Perfil"].ToString();
                    DatosUsuario.Tipo = rd["Tipo"].ToString();
                    DatosUsuario.TipoPartida = rd["TipoPartida"].ToString();
                    DatosUsuario.Username = rd["Username"].ToString();
                   
                }
                cmd.Connection.Close();
            }
            

            catch (Exception e)
                    {
                        if (cmd.Connection.State == ConnectionState.Open)
                        cmd.Connection.Close();
                        Resultado.Exito = false;
                        Resultado.Mensaje.Add(string.Format("Se generó un error al tratar de obtener el Usuario {0}", NombreUsuario));
                        Resultado.Detalles = e.Message;
                    }

            return DatosUsuario;

        }

        /// <summary>
        /// Recibe un SqlDataReader y devuelve un Usuario
        /// Notas: El DataReader debe estar abierto y posicionado en la fila desde la cual se creara el objeto
        /// </summary>
        /// <returns></returns>
        private static CUsuario DataReaderToTUsuario(SqlDataReader rdr)
        {

            CUsuario DUsuario = new CUsuario();
            DUsuario.Perfil = BdConverter.FieldToString(rdr["Perfil"]);
            DUsuario.Tipo = BdConverter.FieldToString(rdr["Tipo"]);
            DUsuario.TipoPartida = BdConverter.FieldToString(rdr["TipoPartida"]);
          

            return DUsuario;
        }
    }
}