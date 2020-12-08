using InventariosPJEH.CAccesoDatos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace InventariosPJEH.CNegocios
{
    public class CProveedores
    {
       
        public string IdProveedor { get; set; }
        public string Proveedor { get; set; }
        public string Calle { get; set; }
        public string Colonia { get; set; }
        public string CP { get; set; }
        public string Telefono { get; set; }
        public int IdMunicipio { get; set; }
        public int IdEstado { get; set; }
        public string email { get; set; }
        public string Estado { get; set; }
        public string Municipio { get; set; }
     
    }
}