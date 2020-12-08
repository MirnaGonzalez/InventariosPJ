using InventariosPJEH.CVista;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using InventariosPJEH.CAccesoDatos;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Threading.Tasks;
using InventariosPJEH.CNegocios;
using System.Data.Odbc;

namespace InventariosPJEH
{
    public partial class frmCatUniAdmin : System.Web.UI.Page
    {

        /*Utilizar el llenado de Unidades Administrativas de frmCatPersonal */
        CPersonal obJN = new CPersonal();
        BdCat_Personal obJE = new BdCat_Personal();
        public string error;

        protected void Page_Load(object sender, EventArgs e)
        {

            BdCat_Personal cPersonal = new BdCat_Personal();

            //if (!this.IsPostBack)
            //{
            //    BdCat_Personal cPersonal = new BdCat_Personal();

            //    if (!this.IsPostBack)
            //    {

            //        IniciarLlenadoDropDown();
            //        IniciarLlenadoDropDownNuevo();
            //        CargoSeleccionado();
            //    }

            //}
        }

    }




}