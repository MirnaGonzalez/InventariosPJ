using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InventariosPJEH.CNegocios
{
    public class CCalculoDepreciacion
    {

        public long NumInventario { get; set; }
        public string DescripcionBien { get; set; }
        public decimal MOI { get; set; }
        public DateTime FechaAdquisicion { get; set; }
        public int Añocompra { get; set; }
        public float VidaUtil { get; set; }
        public decimal MontoDepreciar { get; set; }
        public DateTime FID { get; set; }
        public DateTime FFD { get; set; }
        public float Formula { get; set; }
        public float Porcentaje { get; set; }
        public int TDias { get; set; }
        public int DiferenciaDias { get; set; }
        public int FVI { get; set; }
        public int FVT { get; set; }
        public int SAC { get; set; }
        public int Trimestre { get; set; }
        public double DepreciacionTrimestre  { get; set; }
        public double SaldoAcumulado { get; set; }
        public double SaldoXDepreciar { get; set; }
        public int IdPartida { get; set; }
        public int Grupo { get; set; }
        public int SubGrupo { get; set; }
        public int Clase { get; set; }
        public int IdSubClase { get; set; }
        public string Descripcion { get; set; }

    }
}