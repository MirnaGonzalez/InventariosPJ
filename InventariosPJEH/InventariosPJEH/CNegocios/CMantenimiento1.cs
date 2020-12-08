using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using InventariosPJEH.CAccesoDatos;

namespace InventariosPJEH.CNegocios
{
    public class CMantenimientoR
    {
        public int IdInventario { get; set; }
        public string NombreRecibe { get; set; }
        public string NumOficio { get; set; }
        public string NombreEntrega { get; set; }
        public string SistemaOperativo { get; set; }
        public string CuentaOffice { get; set; }
        public string Ip { get; set; }
        public string FallaReportada { get; set; }
        public string DiagnosticoReparacion { get; set; }

        public static int InsertarRecibirBien(CMantenimientoR Mantenimiento, ref TResultado ResAgragarMantenimiento)
        {
            return BdMantenimiento.InsertarRecibirBien(Mantenimiento, ref ResAgragarMantenimiento);
        }
    }
}