using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InventariosPJEH.CNegocios
{
    public class TParametros
    {
        public Int32 IdEmpleado { get; set; }
        public string NombreTitular { get; set; }
        public string NombreEncargado { get; set; }
    }

    public class CFichaBien
    {
        public Int32 IdInventario { get; set; }
        public Int64 NumInventario { get; set; }
        public string DescripcionBien { get; set; }
        public Int32 IdCONAC { get; set; }
        public Int32 IdSubClase { get; set; }
        public string Modelo { get; set; }
        public string Serie { get; set; }
        public Int32 IdMarca { get; set; }
        public String Detalle { get; set; }
        public string TipoPartida { get; set; }
        public Int32 IdENSU { get; set; }
        public DateTime FechaCaptura { get; set; }
        public float PrecioUnitario { get; set; }
        public float SubTotal { get; set; }
        public float Descuento { get; set; }
        public float CostoTotal { get; set; }
        public Int32 IdCompra { get; set; }
        public Int32 IdActividad { get; set; }
        public Int32 NumLote { get; set; }
        public bool Seleccionado { get; set; }
        public Int32 IdGrupo { get; set; }
    }

    public class CResguardo
    {
        public Int32 IdResguardo { get; set; }
        public Int32 IdEmpleado { get; set; }
        public string NombreEmpleado { get; set; }
        public string Adscripcion { get; set; }
        public string Domicilio { get; set; }
        public DateTime FechaResguardo { get; set; }
        public string NombreGrupo { get; set; }
        public Int32 IdInventario { get; set; }
        public Int64 NumInventario { get; set; }
        public string DescripcionBien { get; set; }
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public string Serie { get; set; }
        public string Propiedad { get; set; }
        public float CostoTotal { get; set; }
        public string Factura { get; set; }
        public Int32 IdGrupo { get; set; }
        public Int32 IdENSU { get; set; }
        public Int32 IdActividad { get; set; }
        public string Grupo { get; set; }
        public string NombreDirAdmon { get; set; }
        public string CargoD { get; set; }
        public string CargoE { get; set; }
        public string Cargo { get; set; }

    }

    public class CGrupoBienResguardo
    {
        public Int32 IdGrupo { get; set; }
        public string Grupo { get; set; }
        public string Edificio { get; set; }
        public string Nivel { get; set; }
        public string Seccion { get; set; }

        public Int32 IdENSU { get; set; }
        public Int32 IdResguardo { get; set; }
    }

    public class BdConverter
    {
        /// <summary>
        /// Convierte un campo (Object) a un cadena
        /// Si el valor es nulo devuelve una cadena Vacia
        /// devuelve 0;
        /// </summary>
        /// <param name="O"></param>
        /// <returns></returns>
        public static string FieldToString(object Campo)
        {
            if (Campo == DBNull.Value)
                return string.Empty;
            else
                return Campo.ToString();
        }

        /// <summary>
        /// Convierte un campo (Object) a un Entero
        /// Si el valor es nulo devuelve una cadena Vacia
        /// devuelve 0;
        /// </summary>
        /// <param name="O"></param>
        /// <returns></returns>
        public static int FieldToInt(object Campo)
        {
            if (Campo == DBNull.Value)
                return 0;
            else
                return (int)Campo;
        }

        /// <summary>
        /// Convierte un campo (Object) a un Entero
        /// Si el valor es nulo devuelve 0
        /// </summary>
        /// <param name="O"></param>
        /// <returns></returns>
        public static Int64 FieldToInt64(object Campo)
        {
            if (Campo == DBNull.Value)
                return 0;
            else
                return (Int64)Campo;
        }

        /// <summary>
        /// Convierte un campo (Object) a un Flotante
        /// Si el valor es nulo devuelve 0
        /// </summary>
        /// <param name="O"></param>
        /// <returns></returns>
        public static float FieldToFloat(object Campo)
        {
            if (Campo == DBNull.Value)
                return 0;
            else
                return (float)(Double)Campo;
        }

        /// <summary>
        /// Convierte un campo (Object) a un Double
        /// Si el valor es nulo devuelve 0
        /// </summary>
        /// <param name="O"></param>
        /// <returns></returns>
        public static Double FieldToDouble(object Campo)
        {
            if (Campo == DBNull.Value)
                return 0;
            else
                return (Double)Campo;
        }

        public static DateTime FieldToDate(Object Campo)
        {
            if (Campo == DBNull.Value)
                return new DateTime();
            else
                return Convert.ToDateTime(Campo);
        }

        public static Boolean FieldToBoolean(Object Campo)
        {
            if (Campo == DBNull.Value)
                return false;
            else
                return Convert.ToBoolean(Campo);
        }
    }
}