using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using InventariosPJEH.CAccesoDatos;
using InventariosPJEH.CNegocios;

namespace InventariosPJEH
{
    public partial class frmHistoricoBien : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CUsuario DatosUsuario = Page.Session["Usuario"] as CUsuario;
                HFTipoPartida.Value = DatosUsuario.TipoPartida;

            }
        }

        protected void BtnBuscar_Click(object sender, EventArgs e)
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
                MostrarHistorial();
            }

        }

        protected void BtnNuevaBusqueda_Click(object sender, EventArgs e)
        {
            // Limpiar Parametros  

            TxtNumInventario.Text = "";
            DivMostrar.Visible = false;
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

            if (string.IsNullOrEmpty(TxtNumInventario.Text))
            {
                LErrores.Add("Numero de Inventario");
             }
            return LErrores;
        }


        public void MostrarHistorial()
        {
            List<CHistoricoBien> HistorialBienes = BdHistoricoBien.MostrarHistorialBien(Convert.ToInt64(TxtNumInventario.Text));

            
            //Double CostoTotal = listaDepreciacion.Sum(item => item.SaldoXDepreciar);
            //int TotalBienes = listaDepreciacion.Count;

            //LblTotal.Text = TotalBienes.ToString();
            //LblCostoTotal.Text = CostoTotal.ToString("$#,##0.00");

                      


            GridHistorial.DataSource = HistorialBienes;
            GridHistorial.DataBind();

            if (HistorialBienes.Count == 0 || HistorialBienes == null)

                MostrarMensaje("** No existen datos  **", "error", "Normal", "Incorrecto");
            else
                DivMostrar.Visible = true;
        }
    }
}