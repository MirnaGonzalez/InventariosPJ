using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using InventariosPJEH.CAccesoDatos;

namespace InventariosPJEH.CNegocios
{
    public class CSubClase_Partida
    {
        public string IdPartida { get; set; }
        public string Partida { get; set; }
        public string TipoPartida { get; set; }
        public string NumPartida { get; set; }
        public string IdSubClase { get; set; }
        public string SubClase { get; set; }
        public string Folio { get; set; }

    }
}