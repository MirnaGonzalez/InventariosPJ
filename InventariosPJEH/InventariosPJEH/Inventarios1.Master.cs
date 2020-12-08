using InventariosPJEH.CNegocios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace InventariosPJEH
{
    public partial class Inventarios1 : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            CUsuario Usuario = Page.Session["Usuario"] as CUsuario;
            string JSONMenu = Page.Session["JSONMenu"] as string;

            if (Usuario != null)
            {
               
                HFPerfil.Value = Usuario.Perfil;

                
                LblPerfil.Text = Usuario.Perfil;
               
                LblNombre.Text = Usuario.Nombre.ToUpper() + " " + Usuario.APaterno.ToUpper() + " " + Usuario.AMaterno.ToUpper();

             
                if (Usuario.TipoPartida == "1") 
                {
                    LblTipoPartida.Text = "Bienes informáticos";
                }
                if (Usuario.TipoPartida == "2")
                {
                    LblTipoPartida.Text = "Bienes muebles";
                }


                HFJSONMenu.Value = JSONMenu;

                Page.Session.Timeout = 60;
            }
            else
            {
                Response.Redirect("ADLogin.aspx");
            }
        }

        protected void BtnCerrarSesion_Click(object sender, EventArgs e)
        {
            Page.Session.Abandon();
            Response.Redirect("ADLogin.aspx");
        }
    }
}