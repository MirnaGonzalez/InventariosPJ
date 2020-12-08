using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using InventariosPJEH.CAccesoDatos;

namespace InventariosPJEH.CNegocios
{
    public class CEdificios_Niveles_Secciones
    {
        public string IdMunicipio { get; set; }
        public string Municipio { get; set; }
        public string IdEdificio { get; set; }
        public string Edificio { get; set; }
        public string Calle { get; set; }
        public string Colonia { get; set; }
        public string CP { get; set; }
        public string IdNivel { get; set; }
        public string Nivel { get; set; }
        public string IdSeccion { get; set; }
        public string Nombre { get; set; }
        public string Tipo { get; set; }
        public string IdENSU { get; set; }

    }
}