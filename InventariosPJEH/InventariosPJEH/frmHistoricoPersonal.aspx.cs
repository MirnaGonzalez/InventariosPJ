using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using InventariosPJEH.CNegocios;
using InventariosPJEH.CAccesoDatos;

namespace InventariosPJEH
{
    public partial class frmHistoricoPersonal : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void BtnNuevBu_Click(object sender, EventArgs e)
        {
            // Limpiar Parametros  
            TxtNomP.Text = "";
            TextAP.Text = "";
            TextAM.Text = "";
            DivHistorico.Visible = false;


        }

        public void MostrarHistorialPersonal()
        {
             
            List<CHistoricoPersonal> cHistorico = BdHistoricoPersonal.MostrarHistorialPersonal(TxtNomP.Text,TextAP.Text, TextAM.Text);
            
           
            GridHistoricoP.DataSource = cHistorico;
            GridHistoricoP.DataBind();

            if (cHistorico.Count == 0 || cHistorico == null)

                MostrarMensaje("** No existen datos  **", "error", "Normal", "Incorrecto");
            else
                DivHistorico.Visible = true;
            GridHistoricoP.Visible = true;
        }

        public void BtnBuscarP_Click (object sender, EventArgs e)
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
                DivHistorico.Visible = false;
               

            }
            else
            {
                MostrarHistorialPersonal();
            }
        }

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

        private List<string> ValidarDatosCalculo()
        {
            List<string> LErrores = new List<string>();

            if (string.IsNullOrEmpty(TxtNomP.Text) && string.IsNullOrEmpty(TextAP.Text) && string.IsNullOrEmpty(TextAM.Text))
            {
                LErrores.Add("La búsqueda no puede estar vacía");                
            }
            return LErrores;
        }

    }
}