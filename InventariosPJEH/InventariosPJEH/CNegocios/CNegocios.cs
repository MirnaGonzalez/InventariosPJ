using System;
using System.Runtime.CompilerServices;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using InventariosPJEH.CAccesoDatos;
using System.Runtime;
using System.IO;
using System.Data;

namespace InventariosPJEH.CNegocios
{
    /// <summary>
    /// Estructura que representa el resultado la la ejecución de un método
    /// Se usa como argumento para llamar algunos métodos
    /// </summary>
    public class TResultado
    {
        public Boolean Exito { get; set; }
        public int CodigoError { get; set; }
        public List<String> Mensaje { get; set; }
        public String Detalles { get; set; }

        public TResultado()
        {
            Exito = true;
            Mensaje = new List<string>();
        }

    }

    /// <summary>
    /// Clase que representa una UnidadAdministrativa con sus principales propiedades (IdUniAdmin, UniAdmin)
    /// </summary>
    public class TUniAdmin
    {
        public int IdUniAdmin { get; set; }
        public string UniAdmin { get; set; }

        /// <summary>
        /// Devuelve la lista de Todas las Unidades Administrativas almacenadas en la base de datos
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static List<TUniAdmin> ObtenerLista(ref TResultado r)
        {
            return BdUniAdmin.ObtenerLista(ref r);
        }

     }

}