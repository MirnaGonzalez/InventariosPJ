using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using InventariosPJEH.CAccesoDatos;
using InventariosPJEH.CNegocios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace InventariosPJEH
{
    public partial class MostrarOrdenServicio : System.Web.UI.Page
    {
        ReportDocument Documento;

        protected void Page_Load(object sender, EventArgs e)
        {
            TParametrosMantenimiento Parametros = Page.Session["Parametros"] as TParametrosMantenimiento;
            List<CMantenimiento> LMantenimiento = new List<CMantenimiento>();
            TResultados ResAsignarMante = new TResultados();

            LMantenimiento = BdReparaBien.ObtenerReporteXIdInventario(Parametros.IdInventario);
         
            if (LMantenimiento.Count > 0)
                {
                    Documento = new ReportDocument();
                
                        Documento.Load(Server.MapPath("~/Reportes/RptReparaBien.rpt"));

                    Documento.SetDataSource(LMantenimiento);
                  

                System.IO.BinaryReader stream = new System.IO.BinaryReader(Documento.ExportToStream(ExportFormatType.PortableDocFormat));

                    this.Response.ClearContent();
                    this.Response.ClearHeaders();
                    this.Response.ContentType = "application/pdf";
                    this.Response.BinaryWrite(stream.ReadBytes(Convert.ToInt32(stream.BaseStream.Length)));
                    this.Response.End();
                }
            //}
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