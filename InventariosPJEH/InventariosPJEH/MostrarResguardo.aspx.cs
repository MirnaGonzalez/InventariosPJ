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
    public partial class MostrarResguardo : System.Web.UI.Page
    {
        ReportDocument Documento;

        protected void Page_Load(object sender, EventArgs e)
        {
            //int IdEmpleado = Convert.ToInt32(Page.Session["IdEmpleado"]);
            TParametros Parametros = Page.Session["Parametros"] as TParametros;

            List<CResguardo> LFichaBien = new List<CResguardo>();
            TResultado ResAsignarResguardo = new TResultado();


            LFichaBien = BdAsignarResguardo.ObtenerResguardoXIdEmpleado(Parametros.IdEmpleado);

            //TResultado ResDTDiligencias = new TResultado();
            //WsAgendaEjecucion WebService = new WsAgendaEjecucion();

            //string LDilJSON = WebService.ObtenerInstructivo(Recibir.IdDiligencia, ref ResDTDiligencias).ToString();

            //JavaScriptSerializer ObjSerializer = new JavaScriptSerializer();
            //List<TDiligenciaPDF> Datos = ObjSerializer.Deserialize<List<TDiligenciaPDF>>(LDilJSON);

            //if (ResDTDiligencias.Exito)
            //{
            if (LFichaBien.Count > 0)
            {
                Documento = new ReportDocument();

                //TDiligenciaPDF Instructivo = (from Dil in Datos
                //                              select Dil).FirstOrDefault();
                //string Tipo = Instructivo.Tipo;
                //bool Automatico = Instructivo.Automatico;
                //string SubTipo = Instructivo.Subtipo;

                //if (Tipo == "EJ" && Recibir.InstructivoSN)
                //{
                //    Documento.Load(Server.MapPath("~/Reportes/CRInstructivoSDSN.rpt")); //Instructivo sin dirección y sin nombre (Solo visible para los instructivos generados de forma automática)
                //}
                //else if (Tipo == "EJ" && Automatico)
                //    Documento.Load(Server.MapPath("~/Reportes/CRInstructivoSD.rpt")); //Instructivo sin dirección (Generado de forma automática)
                //else if (SubTipo == "Edicto" || SubTipo == "EDICTO")
                //    Documento.Load(Server.MapPath("~/Reportes/CREdicto.rpt"));
                //else
                Documento.Load(Server.MapPath("~/Reportes/RptResguardo.rpt"));

                Documento.SetDataSource(LFichaBien);
                Documento.SetParameterValue("NombreTitular", Parametros.NombreTitular);

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