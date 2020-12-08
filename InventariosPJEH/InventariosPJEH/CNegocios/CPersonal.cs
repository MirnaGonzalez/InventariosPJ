using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using InventariosPJEH.CAccesoDatos;
using InventariosPJEH.CNegocios;



namespace InventariosPJEH.CNegocios
{
    public class CPersonal
    {
        public int ClaveEmp { get; set; }
        public Int64 IdUsuario { get; set; }
        public string IdEmpleado { get; set; }
        public string ClaveEmpleado { get; set; }
        public string Claveempleado { get; set; }
        public string Nombre { get; set; }
        public string APaterno { get; set; }
        public string AMaterno { get; set; }
        public int IdUniAdmin { get; set; }
        public int IdCargo { get; set; }
        public string Titular { get; set; }
        public string EstatusPer { get; set; }
        public string EstatusInv { get; set; }
        public string Nomina { get; set; }
        public string Descripcion { get; set; }
        public string UniAdmin { get; set; }
        public string Cargo { get; set; }
        public int IdAbreviaturas { get; set; }
        public string Distrito { get; set; }
        //public string IdUniAdmin { get; set; }



        //BdPersonal obJD= new BdPersonal();



        public static int AutenticarPersonal(string ClaveEmpleado, string Nombre, string APaterno, string AMaterno, ref TResultado Res)
        {
            return BdCat_Personal.ConsultarPersonal(ClaveEmpleado, Nombre, APaterno, AMaterno, ref Res);
        }
        public CPersonal() {  }
        public CPersonal(string pClaveempleado, string pNombre, string pAPaterno, string pAMaterno, string pTitular, string pEstatusPer, string pEstatusInv, string pNomina)
        {
           this.Claveempleado = pClaveempleado;
            this.Nombre = pNombre;
            this.APaterno = pAPaterno;
            this.AMaterno = pAMaterno;
            this.Titular = pTitular;
            this.EstatusPer = pEstatusPer;
            this.EstatusInv = pEstatusInv;
            this.Nomina = pNomina;

        }

        


        //public static DataTable ObtenerSubFondo()
        //{
        //    return BdCat_Personal.IniciarDropDown();
        //}

        public static DataTable ObtenerClasificacion(int IdClasificacion)
        {
            return BdCat_Personal.ObtenerClasificacion(IdClasificacion);
        }

        public static DataTable ObtenerUniAdmin()
        {
            return BdCat_Personal.ObtenerDistritos();
        }

        public static DataTable ObtenerCargos()
        {
            return BdCat_Personal.ObtenerCargos();
        }
        public static DataTable ObtenerNomina(int IdDistrito)
        {
            return BdCat_Personal.ObtenerUniAdminN(IdDistrito);
        }
    }
}