using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InventariosPJEH.CNegocios
{
    public class CReporteDepreciacion
    {
        public string Partida { get; set; }
        public int NumPartida { get; set; }
        public float DepreciacionTrimestre { get; set; }
        public float SaldoAcumulado { get; set; }
        public float SaldoXDepreciar { get; set; }
        public float VidaUtil { get; set; }
        public string AgrupaAnio { get; set; }

    }
}