using System;
using System.Runtime.CompilerServices;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using InventariosPJEH.CAccesoDatos;
using System.Runtime;
using System.IO;
using System.Data;

namespace InventariosPJEH.CNegocios
{

    public class TResultados
    {
        public Boolean Exito { get; set; }
        public int CodigoError { get; set; }
        public List<String> Mensaje { get; set; }
        public String Detalles { get; set; }

        public TResultados()
        {
            Exito = true;
            Mensaje = new List<string>();
        }
    }
    public class TParametrosMantenimiento
    {
        public Int32 IdInventario { get; set; }
        public Int32 IdInventarios { get; set; }

        public string NumInventario { get; set; }
    }
    public class CReparaBien
    {
 
        public String IdInventario { get; set; }
        public String NumInventario { get; set; }
        public string DescripcionBien { get; set; }
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public string Serie { get; set; }
        public string NombrePersona { get; set; }
        public string IdActividad { get; set; }
        public string IdENSU { get; set; }
        public string IdUsuario { get; set; }
    }
   public class CBienSinReparar
    {
        public String IdInventario { get; set; }
        public String NumInventario { get; set; }
        public string DescripcionBien { get; set; }
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public string Serie { get; set; }        
        public string FechaAdquisicion { get; set; }
        public string IdActividad { get; set; }
        public string IdUsuario { get; set; }
        public string IdENSU { get; set; }
        public string TipoPartida { get; set; }


    }

    public class CBajaBienSoporte
    {
        public string IdInventario { get; set; }
        public string NumInventario { get; set; }
    }
   
    
    public class CMantenimiento
    {
        public String IdInventario { get; set; }
        public String NumInventario { get; set; }
        public String DescripcionBien { get; set; }
        public String Modelo { get; set; }
        public String Serie { get; set; }
        public string IdRegMant { get; set; }
        public string NombreRecibe { get; set; }
        public string NumOficio { get; set; }
        public string NombreEntrega { get; set; }
        public string SistemaOperativo { get; set; }
        public string CuentaOffice { get; set; }
        public string Ip { get; set; }
        public string FallaReportada { get; set; }
        public string DiagnosticoReparacion { get; set; }
        public string IdPiezaRep { get; set; }
        public string NumTarjetaInf { get; set; }
        public string NumRefaccion { get; set; }
        public string DescripcionRefaccion { get; set; }
        public string Marca { get; set; }
        public string Seccion { get; set; }
   
    }

    public class CTarjeta
    { 

    public String IdInventario { get; set; }
    public String NumInventario { get; set; }
    public String DescripcionBien { get; set; }
    public String Modelo { get; set; }
    public String Serie { get; set; }
    public string IdRegMant { get; set; }
    public string NombreRecibe { get; set; }
    public string NumOficio { get; set; }
    public string NombreEntrega { get; set; }
    public string SistemaOperativo { get; set; }
    public string CuentaOffice { get; set; }
    public string Ip { get; set; }
    public string FallaReportada { get; set; }
    public string DiagnosticoReparacion { get; set; }
    public string IdPiezaRep { get; set; }
    public string NumTarjetaInf { get; set; }
    public string NumRefaccion { get; set; }
    public string DescripcionRefaccion { get; set; }
    public string Marca { get; set; }
    public string Seccion { get; set; }
        public string Area { get; set; }
        public string IdActividad { get; set; }

    }
    public class CPiezas    
    {
        public string IdPiezaRep { get; set; }
        public string IdInventario { get; set; }
        public string NumTarjetaInf { get; set; }
        public string NumRefaccion { get; set; }
        public string DescripcionRefaccion { get; set; }
        public string IdRegMant { get; set; }
    }
}