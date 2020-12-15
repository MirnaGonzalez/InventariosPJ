using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using InventariosPJEH.CAccesoDatos;
using InventariosPJEH.CNegocios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace InventariosPJEH
{
    public partial class MostrarEtiqueta : System.Web.UI.Page
    {
        ReportDocument Documento;

        protected void Page_Load(object sender, EventArgs e)
        {
            List<EtiquetaInventario> Lista = Page.Session["etiqueta"] as List<EtiquetaInventario>;

            if (Lista.Count > 0)
            {
                Documento = new ReportDocument();
                Documento.Load(Server.MapPath("~/Reportes/RptEtiquetas.rpt"));
                Documento.SetDataSource(Lista);


                System.IO.BinaryReader stream = new System.IO.BinaryReader(Documento.ExportToStream(ExportFormatType.PortableDocFormat));

                this.Response.ClearContent();
                this.Response.ClearHeaders();
                this.Response.ContentType = "application/pdf";
                this.Response.BinaryWrite(stream.ReadBytes(Convert.ToInt32(stream.BaseStream.Length)));
                this.Response.End();
            }

        }

        protected void Page_Unload(object sender, EventArgs e)
        {
            if (Documento != null)
            {
                if (Documento.IsLoaded)
                {
                    Documento.Close();
                    Documento.Dispose();
                }
            }
          

        }





        

    }
}