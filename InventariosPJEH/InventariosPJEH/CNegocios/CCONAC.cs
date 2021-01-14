using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using InventariosPJEH.CAccesoDatos;

namespace InventariosPJEH.CNegocios
{
    public class CCONAC
    {
        public string IdCONAC { get; set; }
        public string IdClaveCONAC { get; set; }
        public string Grupo { get; set; }
        public string SubGrupo { get; set; }
        public string Clase { get; set; }
        public string Descripcion { get; set; }
        public string TipoPartida { get; set; }
    }
}