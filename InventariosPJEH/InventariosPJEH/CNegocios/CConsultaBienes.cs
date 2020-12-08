using System;


namespace InventariosPJEH.CNegocios
{
    public class CConsultaBienes
    {
        public Int32 NumPartida { get; set; }
        public Int32 IdPropiedad { get; set; }
        public Int32 IdTipoAdqui { get; set; }
        public Int32 IdTipoDoc { get; set; }
        public Int32 IdProveedor { get; set; }
        public string TipoAdqui { get; set; }
        public string Documento { get; set; }
        public string Proveedor { get; set; }
        public string Nombre { get; set; }
        public string Propiedad { get; set; }
        public string Partida { get; set; }
        public Int32 TipoPartida { get; set; }
        public Int32 idSubClase { get; set; }
        public string SubClase { get; set; }
        public string Descripcion { get; set; }
        public string NumDocumento { get; set; }
        public Int32 idPartida { get; set; }
        public Int32 IdCONAC { get; set; }
        public Int32 IdMarca { get; set; }
        public Int32 IdSeccion { get; set; }
        public Int32 IdENSU { get; set; }
        public Int32 IdCompra { get; set; }
        public string FechaAdquisicion { get; set; }
        public string DetAdqui { get; set; }
        public Int64 NumInventario { get; set; }
        public string DescripcionBien { get; set; }
        public string Modelo { get; set; }
        public string Serie { get; set; }
        public double CostoTotal { get; set; }
        public string NombrePersona { get; set; }
        public Int32 IdResguardo { get; set; }
        public Int32 IdActividad { get; set; }
        public string Actividad { get; set; }
    }
}