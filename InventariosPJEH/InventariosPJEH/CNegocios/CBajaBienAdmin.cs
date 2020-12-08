using InventariosPJEH.CAccesoDatos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InventariosPJEH.CNegocios
{
    public class CBajaBienAdmin
    {
        public string IdInventario { get; set; }
        public string NumInventario { get; set; }
        public string DescripcionBien { get; set; }
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public string Serie { get; set; }
        public string FechaAdquisicion { get; set; }
        public string IdActividad { get; set; }
        public string IdENSU { get; set; }
        public string IdUsuario { get; set; }

        public static int InsertarHistoricoUbicacion(CUbicacion HistoricoUbi, ref TResultado ResAgragarHistoricoUbi)
        {
            return BdBajaBienAdmin.InsertarHistoricoUbicacion(HistoricoUbi, ref ResAgragarHistoricoUbi);
        }
    }
}

public class CFichaBienBaja
{
    public int IdInventario { get; set; }
    public Int64 NumInventario { get; set; }
    public int IdActividad { get; set; }
    public int IdENSU { get; set; }
    public int IdUsuario { get; set; }
    public string DescripcionBien { get; set; }
    public string Marca { get; set; }
    public string Modelo { get; set; }
    public string Serie { get; set; }
    public DateTime FechaAdquisicion { get; set; }
    public int IdResguardo { get; set; }
    public string Clasificacion { get; set; }
    public int TipoPartida { get; set; }

}

public class CGrupoResguardo
{ 
    public int IdResguardo { get; set; }
    public int IdEmpleado { get; set; }
    public string NombreCompleto { get; set; }
    public string Grupo { get; set; }
    public int IdENSU { get; set; }
    public string Edificio { get; set; }
    public string Nivel { get; set; }
    public string Seccion { get; set; }


}