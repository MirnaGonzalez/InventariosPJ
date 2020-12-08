using System;


namespace InventariosPJEH.CNegocios
{
    public class CBajaBien
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
        public Int32 NumLote { get; set; }    
        public Int32 Grupo { get; set; }
        public Int32 SubGrupo { get; set; }
        public Int32 Clase { get; set; }
        public string Detalle { get; set; }
        public double PrecioUnitario { get; set; }
        public double SubTotal { get; set; }
        public Int32 VidaUtil { get; set; }
        public double Frente { get; set; }
        public double Fondo { get; set; }
        public double Altura { get; set; }
        public double Diametro { get; set; }
        public DateTime FechaCaptura { get; set; }
        public double Descuento { get; set; }
        public Int32 IdInventario { get; set; }
        public Int32 IdUsuario { get; set; }
        public DateTime Fecha { get; set; }
    }
}