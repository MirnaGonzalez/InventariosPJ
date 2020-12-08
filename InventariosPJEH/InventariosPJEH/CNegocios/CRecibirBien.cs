using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InventariosPJEH.CNegocios
{
    public class CRecibirBien
    {
        public string IdRegMant { get; set; }
        public string IdInventario { get; set; }
        public string NumInventario { get; set; }
        public string DescripcionBien { get; set; }
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public string Serie { get; set; }
        public string NombrePersona { get; set; }
        public string IdActividad { get; set; }
        public string IdENSU { get; set; }
        public string IdUsuario { get; set; }
    }
} 