using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using InventariosPJEH.CAccesoDatos;
using InventariosPJEH.CNegocios;
using Microsoft.SqlServer.Server;
using System.Linq;

namespace InventariosPJEH
{
    public partial class frmCalcularDepreciacion : System.Web.UI.Page
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
        protected void DdlTrimestre_SelectedIndexChanged(object sender, EventArgs e)
        {
            Limpiar();
            TxtAnio.Focus();
        }

        protected void CB_Inactivo_CheckedChanged(object sender, EventArgs e)
        {
            if (CB_Inactivo.Checked == true)
            {
                DivInactivo.Visible = true;
                HFStatus.Value = "1";
            }
            else
            {
                DivInactivo.Visible = false;
                HFStatus.Value = "0";
            }
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

                    break;

                // Segundo Trimestre
                case 2:
                    FechaIT = TxtAnio.Text + "-04-01 00:00:00";
                    FechaInicial = Convert.ToDateTime(FechaIT);

                    FechaFT = TxtAnio.Text + "-06-30 23:59:59";
                    FechaFinal = Convert.ToDateTime(FechaFT);
                    break;


                // Trimestre Trimestre
                case 3:
                    FechaIT = TxtAnio.Text + "-07-01 00:00:00";
                    FechaInicial = Convert.ToDateTime(FechaIT);

                    FechaFT = TxtAnio.Text + "-09-30 23:59:59";
                    FechaFinal = Convert.ToDateTime(FechaFT);
                    break;


                // Cuarto Trimestre
                case 4:
                    FechaIT = TxtAnio.Text + "-10-01 00:00:00";
                    FechaInicial = Convert.ToDateTime(FechaIT);

                    FechaFT = TxtAnio.Text + "-12-31 23:59:59";
                    FechaFinal = Convert.ToDateTime(FechaFT);
                    break;

            }
            List<CCalculoDepreciacion> listaDepreciacion = BdCalcularDepreciacion.MostraDepreciacion(FechaInicial, FechaFinal, Convert.ToInt32(HFStatus.Value), HFTipoPartida.Value);

            decimal CostoTotal = listaDepreciacion.Sum(item => item.MOI);
            int TotalBienes = listaDepreciacion.Count;

            LblTotal.Text = TotalBienes.ToString();
            LblCostoTotal.Text = CostoTotal.ToString("$#,##0.00");


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
            if (CB_Inactivo.Checked == true)
            {
                if (DdlBaja.SelectedIndex == 0 || DdlBaja.SelectedValue == "0" || DdlBaja.SelectedValue == "")
                    LErrores.Add("Baja");
            }

            return LErrores;

        }

        // Limpiar Parametros  
        protected  void Limpiar()
        {
            TxtAnio.Text = "";
            //GridDepreciacion.DataSource = null;
            LblCostoTotal.Text = "";
            LblTotal.Text = "";
            CB_Inactivo.Checked = false;
            DdlBaja.SelectedValue = null;
        }

    }
}