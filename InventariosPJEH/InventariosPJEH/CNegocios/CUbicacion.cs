using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InventariosPJEH.CNegocios
{
    public class CUbicacion
    {
        public int IdInventario { get; set; }
        public Int64 NumInventario { get; set; }
        public int IdActividad { get; set; }
        public int IdENSU { get; set; }
        public int IdUsuario { get; set; }
        public DateTime Fecha { get; set; }
        public string Estatus { get; set; }
        public string OficioBien { get; set; }
        public int IdResguardo { get; set; }
        public int IdEmpleado { get; set; }
        public string Grupo { get; set; }
        public DateTime FechaResguardo { get; set; }
        public int TipoPartida { get; set; }


    }
}