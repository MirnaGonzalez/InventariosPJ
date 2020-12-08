using InventariosPJEH.CNegocios;
using InventariosPJEH.CVista;
using InventariosPJEH.CAccesoDatos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Configuration;

namespace InventariosPJEH
{

    public partial class ADLogin : System.Web.UI.Page
    {
        BdUsuario usuariosBD = new BdUsuario();

        protected void Page_Load(object sender, EventArgs e)
        {
           
        }

        protected void BtnEntrar_Click(object sender, EventArgs e)
        {
            if (TxtNomUsuario.Text != "")
            {
                if (TxtContrasena.Text != "")
                {
                    ///
                    int Autenticar = 0;
                    TResultado ResAutenticacion = new TResultado();
                    Autenticar = CUsuario.Autenticar(TxtNomUsuario.Text, TxtContrasena.Text, ref ResAutenticacion);

                    if (Autenticar != -1)
                    {

                        if (Autenticar != 0)
                        {
                            string usuario = TxtNomUsuario.Text;
                            string contraseña = TxtContrasena.Text;

                            CUsuario unUsuario = usuariosBD.ConsultarUsuario(usuario, contraseña);

                            if (unUsuario != null)
                            {

                                EnvRecParametros Enviar = new EnvRecParametros();
                                Enviar.MsjMostrar = "** Bienvenido **";

                                Page.Session["EnvRecParametros"] = Enviar;

                                //Obtener datos de Usuario
                                TResultado ResUsuario = new TResultado();
                                CUsuario DatosUsuario = new CUsuario();

                                DatosUsuario = CUsuario.ObtenerDatosUsuario(TxtNomUsuario.Text, ref ResUsuario);

                                Page.Session["Usuario"] = DatosUsuario;

                                Response.Redirect("ADInicio.aspx");

                                                              

                            }
                            else
                            {
                                MostrarMensaje("** Fallo la conexion.", "info", "Normal");
                            }
                        }

                        else
                        {
                            MostrarMensaje("** El usuario y/o contraseña incorrectas ó usuario deshabilitado.", "error", "Normal");
                        }
                    }
                    else
                    {
                        MostrarMensaje("** Error al tratar de autenticar al usuario.", "error", "Normal");
                    }

                }
                else
                {
                    MostrarMensaje("** La contraseña es requerida.", "info", "Normal");
                }
            }
            else
            {
                MostrarMensaje("** El nombre de usuario es requerido.", "info", "Normal");
            }
        }

        protected void MostrarMensaje(string Mensaje, string Tipo, string TipoFuncion)
        {
            string Msj = "";
            if (TipoFuncion == "Normal")
                Msj = "MostrarMensaje('" + Mensaje + "', '" + Tipo + "');";
            else
                Msj = "MostrarMensajeInterval('" + Mensaje + "', '" + Tipo + "');";

            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Ntn", Msj, true);
        }
    }
}

