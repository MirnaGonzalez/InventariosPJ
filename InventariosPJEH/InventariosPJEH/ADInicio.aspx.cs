using InventariosPJEH.CVista;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace InventariosPJEH
{
    public partial class Inicio : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            EnvRecParametros Recibir = Page.Session["EnvRecParametros"] as EnvRecParametros;

            if (Recibir != null && Recibir.MsjMostrar != null && Recibir.MsjMostrar != "")
            {
                MostrarMensaje(Recibir.MsjMostrar, "info", "Intervalo", "Inicio");
                Recibir.MsjMostrar = "";
                Page.Session["EnvRecParametros"] = Recibir;
            }

            if (!Page.IsPostBack)
            {
                //EnvRecParametros Enviar = new EnvRecParametros();
                //Page.Session["EnvRecParametros"] = Enviar;
            }
        }

        protected void MostrarMensaje(string Mensaje, string Tipo, string TipoFuncion, string ClaveMsj)
        {
            string Msj = "";
            if (TipoFuncion == "Normal")
                Msj = "MostrarMensaje('" + Mensaje + "', '" + Tipo + "');";
            else
                Msj = "MostrarMensajeInterval('" + Mensaje + "', '" + Tipo + "');";

            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), ClaveMsj, Msj, true);
        }
    }
}