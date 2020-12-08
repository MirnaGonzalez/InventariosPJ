using InventariosPJEH.CAccesoDatos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InventariosPJEH.CNegocios
{
    public class CUsuario
    {
        public string IdUsuario { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Nombre { get; set; }
        public string APaterno { get; set; }
        public string AMaterno { get; set; }
        public string Perfil { get; set; }
        public string Tipo { get; set; }
        public string Estatus { get; set; }
        public string TipoPartida { get; set; }
        

        /// <summary>
        /// Realizal autenticacion de un Usuairio
        /// -1: Se generó un error
        ///  0: Usuario y/o Clave Incorrectas o Usuario Deshabilitado
        ///  1: Autenticación Exitosa 
        /// </summary>
        /// <param name="Username"></param>
        /// <param name="Password"></param>
        /// <returns></returns>
        public static int Autenticar(string Username, string Password, ref TResultado Res)
        {
            return BdUsuario.Autenticar(Username, Password, ref Res);
        }

        /// <summary>
        ///  Debuelve un Objeto del Tipo TUsuario a partir del Nombre de usuario y la contraseña pasadas como argumentos.
        /// </summary>
        /// <param name="Usuario"></param>
        /// <param name="Resultado"></param>
        /// <returns></returns>
        public static CUsuario ObtenerDatosUsuario(string Username, ref TResultado Resultado)
        {
            return BdUsuario.ObtenerDatosUsuario(Username, ref Resultado);
        }

    }
}