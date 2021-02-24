using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InventariosPJEH.CNegocios
{
    public class CUnidadAdmin
    {
        public string Tipo { get; set; }
        public string DescTipo { get; set; }
        public int IdUniAdmin { get; set; }
        public string UniAdmin { get; set; }
        public int IdSubFondo { get; set; }
        public string SubFondo { get; set; }
        public string Email { get; set; }
        public string Telefono { get; set; }
        public int IDUniAdmin { get; set; }
        public int IdDistrito { get; set; }
        public String Distrito { get; set; }

        public string Abreviatura { get; set; }

        public string Clasificacion { get; set; }
        public string DescClasific { get; set; }
        public int IdClasificacion { get; set; }
        public int IdEmpleado { get; set; }
        
}
}