using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InventariosPJEH.CNegocios
{     
    public class CHistoricoBien
    {
        public long NumInventario { get; set; }
        public string DescripcionBien { get; set; }
        public decimal MOI { get; set; }
        public DateTime Fecha { get; set; }
        public string Nombre { get; set; }
        public string UsuarioMov { get; set; }
        public string Actividad { get; set; }
        
    }
}