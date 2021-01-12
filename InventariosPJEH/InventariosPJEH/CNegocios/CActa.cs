using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InventariosPJEH.CNegocios
{
    public class CActa
    {
        public int idActa { get; set; }

        public String strNumActa { get; set; }

        public String strFechaActa { get; set; }

        public String strUniAdmin { get; set; }

        public String strNumInventario { get; set; }

        public String strNombreBien { get; set; }

        public String strMarca { get; set; }

        public String strModelo { get; set; }

        public String strSerie { get; set; }

        public String strNumResguardo { get; set; }

        public String strNombreResguardante { get; set; }

        public String strStatus { get; set; }

        public String strDescripcionActa { get; set; }

        public String strFechaCancela { get; set; }
    }
}