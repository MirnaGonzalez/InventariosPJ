using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InventariosPJEH.CNegocios
{
    public class CDatosResguardante
    {
        public int intOIdEmpleado { get; set; }

        public int idResguardo { get; set; }

        public int idAreaAdscrip { get; set; }
        
        public String txtONombreResguardante { get; set; }

        public String txtOCargoResguardo { get; set; }

        public String txtOAreaAdscripcionResguardo { get; set; }
    }
}