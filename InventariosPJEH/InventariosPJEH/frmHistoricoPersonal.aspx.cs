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

        protected void BtnNuevBu_Click(object sender, EventArgs e)
        {
            // Limpiar Parametros  
            TxtNomP.Text = "";
            TextAP.Text = "";
            TextAM.Text = "";
            GridHistoricoP.DataSource = null;
            GridHistoricoP.DataBind();
            DivHistorico.Visible = false;
        }

        public void MostrarHistorialPersonal()
        {

            List<CHistoricoPersonal> cHistorico = BdHistoricoPersonal.MostrarHistorialPersonal(TxtNomP.Text, TextAP.Text, TextAM.Text);

            GridHistoricoP.DataSource = cHistorico;
            GridHistoricoP.DataBind();
            if (GridHistoricoP.Rows.Count == 0)
            {
                GridHistoricoP.DataSource = null;
                GridHistoricoP.DataBind();
                DivHistorico.Visible = false;
                MostrarMensaje("** No existen resultados con los filtros solicitados  **", "error", "Normal", "Incorrecto");
            }
            else
            {
                DivHistorico.Visible = true;
            }

        }

        public void BtnBuscarP_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(TxtNomP.Text) && string.IsNullOrEmpty(TextAP.Text) && string.IsNullOrEmpty(TextAM.Text))
            {

                // MostrarMensaje($"Verifica los siguientes datos:{error}. ", "error", "Normal", "Incorrecto");
                MostrarMensaje("** Datos Faltantes **", "error", "Normal", "Incorrecto");
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

    }
}