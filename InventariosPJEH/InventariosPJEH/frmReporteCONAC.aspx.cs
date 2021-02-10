using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using InventariosPJEH.CAccesoDatos;
using InventariosPJEH.CNegocios;
using System.IO;
using System.Web.UI.HtmlControls;
using System.Text;
using System.Drawing;

namespace InventariosPJEH
{
    public partial class frmReporteCONAC : System.Web.UI.Page
    {

        DateTime FechaInicial;
        DateTime FechaFinal;


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CUsuario DatosUsuario = Page.Session["Usuario"] as CUsuario;
                HFTipoPartida.Value = DatosUsuario.TipoPartida;

            }

        }

        protected void DdlTrimestre_SelectedIndexChanged1(object sender, EventArgs e)
        {
            Limpiar();
            TxtAnio.Focus();
        }

        protected void BtnCalcular_Click(object sender, EventArgs e)
        {
            List<string> listaErrores = ValidarDatosCalculo();

            if (listaErrores.Count > 0)
            {
                string error = "";
                foreach (var itemError in listaErrores)
                {
                    error += " - " + itemError;
                }
                // MostrarMensaje($"Verifica los siguientes datos:{error}. ", "error", "Normal", "Incorrecto");
                MostrarMensaje($"DATOS FALTANTES:  {error}", "info", "Intervalo", "Inicio");
                DivMostrar.Visible = false;

            }
            else
            {
                MostrarDepreciacion();
            }


        }


        public void MostrarDepreciacion()
        {

            int Trimestre = Convert.ToInt32(DdlTrimestre.SelectedValue);
            string FechaIT = "";
            string FechaFT = "";
          

            switch (Trimestre)
            {
                // Primer Trimestre
                case 1:

                    FechaIT = TxtAnio.Text + "-01-01 00:00:00";
                    FechaInicial = Convert.ToDateTime(FechaIT);

                    FechaFT = TxtAnio.Text + "-03-31 23:59:59";
                    FechaFinal = Convert.ToDateTime(FechaFT);
                
                    HFNameFile.Value = "ReporteCONAC_1erTrimestre_" + Convert.ToString(TxtAnio.Text) + ".xls";
                    break;

                // Segundo Trimestre
                case 2:
                    FechaIT = TxtAnio.Text + "-04-01 00:00:00";
                    FechaInicial = Convert.ToDateTime(FechaIT);

                    FechaFT = TxtAnio.Text + "-06-30 23:59:59";
                    FechaFinal = Convert.ToDateTime(FechaFT);

                    HFNameFile.Value = "ReporteCONAC_2doTrimestre_" + Convert.ToString(TxtAnio.Text) + ".xls";
                    break;


                // Trimestre Trimestre
                case 3:
                    FechaIT = TxtAnio.Text + "-07-01 00:00:00";
                    FechaInicial = Convert.ToDateTime(FechaIT);

                    FechaFT = TxtAnio.Text + "-09-30 23:59:59";
                    FechaFinal = Convert.ToDateTime(FechaFT);
                    HFNameFile.Value = "ReporteCONAC_3erTrimestre_" + Convert.ToString(TxtAnio.Text) + ".xls";
                    break;


                // Cuarto Trimestre
                case 4:
                    FechaIT = TxtAnio.Text + "-10-01 00:00:00";
                    FechaInicial = Convert.ToDateTime(FechaIT);

                    FechaFT = TxtAnio.Text + "-12-31 23:59:59";
                    FechaFinal = Convert.ToDateTime(FechaFT);
                    HFNameFile.Value = "ReporteCONAC_4toTrimestre_" + Convert.ToString(TxtAnio.Text) + ".xls";
                    break;

            }

            List<CCalculoDepreciacion> listaDepreciacion = BdCalcularDepreciacion.MostraDepreciacion(FechaInicial, FechaFinal, Convert.ToInt32(HFStatus.Value), HFTipoPartida.Value, "");

            Double CostoTotal = listaDepreciacion.Sum(item => item.SaldoXDepreciar);
            int TotalBienes = listaDepreciacion.Count;

            LblTotal.Text = TotalBienes.ToString();
            LblCostoTotal.Text = CostoTotal.ToString("$#,##0.00");


            listaDepreciacion.Add(new CCalculoDepreciacion { DescripcionBien="Total:", SaldoXDepreciar= CostoTotal  });;


            GridDepreciacion.DataSource = listaDepreciacion;
            GridDepreciacion.DataBind();
     
            if (listaDepreciacion.Count == 0 || listaDepreciacion == null)

                MostrarMensaje("** No existen datos  **", "error", "Normal", "Incorrecto");
            else
                DivMostrar.Visible = true;
        }

        //Mostrar Mensajes 
        protected void MostrarMensaje(string Mensaje, string Tipo, string TipoFuncion, string ClaveMsj)
        {
            string Msj = "";
            if (TipoFuncion == "Normal")
                Msj = "MostrarMensaje('" + Mensaje + "', '" + Tipo + "');";

            else if (TipoFuncion == "NotificacionEliminar")
                //    Msj = "prueba2('" + Mensaje + "', '" + Tipo + "');";
                Msj = "confirm();";
            else
                Msj = "MostrarMensajeInterval('" + Mensaje + "', '" + Tipo + "');";


            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), ClaveMsj, Msj, true);
        }

        // Validar Datos esten llenos para calcular
        private List<string> ValidarDatosCalculo()
        {
            List<string> LErrores = new List<string>();

            if (DdlTrimestre.SelectedIndex == 0 || DdlTrimestre.SelectedValue == "0" || DdlTrimestre.SelectedValue == "")
                LErrores.Add("Trimestre a calcular");

            if (string.IsNullOrEmpty(TxtAnio.Text))
            {
                LErrores.Add("Año a realizar el calculo");
                if ((TxtAnio.Text.Length != 4))
                    LErrores.Add("Año a 4 digitos");
            }
            return LErrores;
        }

        // Limpiar Parametros  
        protected void Limpiar()
        {
            TxtAnio.Text = "";
            LblCostoTotal.Text = "";
            LblTotal.Text = "";
            DivMostrar.Visible = false;
   
        }

        protected void BtnExportar_Click(object sender, EventArgs e)
        {
            GridDepreciacion.Width = Unit.Percentage(0);

            HttpResponse response = Response;
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            Page pageToRender = new Page();
            HtmlForm form = new HtmlForm();


            form.Controls.Add(GridDepreciacion);
           // GridDepreciacion.HeaderRow.BackColor = Color.Aqua;
            pageToRender.Controls.Add(form);
            response.Clear();
            response.Buffer = true;
            response.ContentType = "application/vnd.ms-excel";
            response.AddHeader("Content-Disposition", "attachment;filename=" + HFNameFile.Value);
            response.Charset = "UTF-8";
            response.ContentEncoding = Encoding.Default;
            pageToRender.RenderControl(htw);
            response.Write(sw.ToString());
            response.End();

            GridDepreciacion.Width = Unit.Percentage(100);
                    
        }
        //public override void VerifyRenderingInServerForm(Control control)
        //{
        //    /* Confirms that an HtmlForm control is rendered for the specified ASP.NET
        //       server control at run time. */
        //}

        protected  void ExportarExcel()
        {

            List<CCalculoDepreciacion> listaDepreciacion = BdCalcularDepreciacion.MostraDepreciacion(FechaInicial, FechaFinal, Convert.ToInt32(HFStatus.Value), HFTipoPartida.Value, "");
                    

            GridDepreciacion.DataSource = listaDepreciacion;
            GridDepreciacion.DataBind();

            //DataTable Datos = new DataTable();
            //Datos = BdConsultaBienes.BuscarGrid(DropDownListPropiedad, DropDownListPartida,
            //    DropDownListSubClase, TextBoxNoDocumento, DropDownListProveedor, TxtFechaAdquisicion,
            //    TxtNoInventario, DropDownListMarca, TextBoxModelo, TextBoxSerie, DropPersona, TxtNoResguardo, DropClasificacion,
            //    DropUniAdmin, DropEstatus, DropAreaMantenimiento);
            //GridPrueba.DataSource = Datos;
            //GridPrueba.DataBind();

            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition",
             "attachment;filename=Conac.xls");
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);

            for (int i = 0; i < GridDepreciacion.Rows.Count; i++)
            {
                //Aplicar el estilo de texto 
                GridDepreciacion.Rows[i].Attributes.Add("class", "textmode");
            }
            GridDepreciacion.RenderControl(hw);

            //Estilo con el formato de número
            string style = @"<style> .textmode { mso-number-format:\@; } </style>";
            Response.Write(style);
            Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.End();
        }
    }
}