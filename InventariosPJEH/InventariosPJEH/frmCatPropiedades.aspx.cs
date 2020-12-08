using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using InventariosPJEH.CAccesoDatos;
using InventariosPJEH.CNegocios;
using InventariosPJEH.CVista;

namespace InventariosPJEH
{
    public partial class frmCatPropiedades : System.Web.UI.Page
    {
        CPropiedades obJN = new CPropiedades();
        BdCatPropiedades obJE = new BdCatPropiedades();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.Form["__EVENTTARGET"] == "AccionEliminarr")
            {
                GridBuscarPropiedad_RowDeleting(this, new GridViewDeleteEventArgs(Int32.Parse(Rowindex.Value)));

            }
            else if (Request.Form["__EVENTTARGET"] == "AccionVacio")
            {
                Rowindex.Value = string.Empty;

            }

          

        }

        /// <summary>
        /// 
        /// </summary>
        protected void BtnBuscar_Click(object sender, EventArgs e)
        {
            HiddenId.Value = TxtPropiedad.Text;
            BtnActualizar.Visible = false;
            BtnCancelar.Visible = false;
            BtnLimpiar.Visible = false;
            BtnGuardar.Visible = false;

            BuscarPropiedades();

            if (GridBuscar.Rows.Count == 0)
            {
                MostrarMensaje("** No existen datos con la búsqueda solicitada **", "error", "Normal", "Incorrecto");
                DivTabla.Visible = false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void BuscarPropiedades()
        {

            if (TxtPropiedadExistente.Text != "")
            {
                DivTabla.Visible = true;

                GridBuscar.DataSource = BdCatPropiedades.ConsultarGbPropiedades(TxtPropiedadExistente.Text);
                GridBuscar.DataBind();
                if (GridBuscar.Rows.Count == 0)
                {
                    DivTabla.Visible = false;
                    MostrarMensaje("** No existen datos con la búsqueda solicitada **", "error", "Normal", "Incorrecto");
                }

                DivMostrarNuevoR.Visible = false;
            }
            else
            {
                MostrarMensaje("** Campo vacío **", "error", "Normal", "Incorrecto");
            }

        }

        /// <summary>
        /// 
        /// </summary>
        public void BuscarNuevaPropiedad()
        {
            DivTabla.Visible = true;
            GridBuscar.DataSource = BdCatPropiedades.ConsultarGbPropiedades(TxtPropiedad.Text);
            GridBuscar.DataBind();

            if (GridBuscar.Rows.Count == 0)
            {
                MostrarMensaje("** No existen datos con la búsqueda solicitada **", "error", "Normal", "Incorrecto");
                DivTabla.Visible = false;
            }
        }

        protected void MostrarMensaje(string Mensaje, string Tipo, string TipoFuncion, string ClaveMsj)
        {
            string Msj = "";
            if (TipoFuncion == "Normal")
                Msj = "MostrarMensaje('" + Mensaje + "', '" + Tipo + "');";

            else if (TipoFuncion == "NotificacionEliminar")
                Msj = "confirm();";
            else
                Msj = "MostrarMensajeInterval('" + Mensaje + "', '" + Tipo + "');";

            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), ClaveMsj, Msj, true);
        }

        public void LimpiarBuscar()
        {
            TxtPropiedadExistente.Text = string.Empty;

        }
        public void LimpiarRegistro()
        {
            TxtPropiedad.Text = string.Empty;
            LbId.Text = string.Empty;

        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void BtnActualizar_Click(object sender, EventArgs e)
        {
            if (TxtPropiedad.Text != "")
            {
                DivTabla.Visible = true;
                SqlConnection cnn = new SqlConnection(CConexion.Obtener());
                SqlCommand cmd = new SqlCommand("UPDATE Cat_Propiedades SET Propiedad=@propiedad where IdPropiedad=@idPropiedad", cnn);
                cmd.Parameters.AddWithValue("@propiedad", TxtPropiedad.Text.ToUpper());
                cmd.Parameters.AddWithValue("@idPropiedad", LbId.Text);
                try
                {

                    cnn.Open();
                    cmd.ExecuteNonQuery();
                    MostrarMensaje("** Se actualizo correctmente **", "error", "Normal", "Incorrecto");
                    BuscarNuevaPropiedad();
                }
                catch (Exception)
                {
                    MostrarMensaje("** Error al actualizar propiedad **", "error", "Normal", "Incorrecto");
                    cnn.Close();
                }
            }
            else
            {
                MostrarMensaje("** El campo tipo propiedad es requerido**", "error", "Normal", "Incorrecto");
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void BtnCancelar_Click(object sender, EventArgs e)
        {
            LimpiarRegistro();
            DivMostrarNuevoR.Visible = false;
            BtnActualizar.Visible = false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void BtnGuardarNuevo(object sender, EventArgs e)
        {
            if (TxtPropiedad.Text != "")
            {
                BtnGuardar.Visible = true;
                DivTabla.Visible = true;
                SqlConnection cnn = new SqlConnection(CConexion.Obtener());
                SqlCommand insert = new SqlCommand("insert into Cat_Propiedades(Propiedad) values(@propiedad)", cnn);
                insert.Parameters.AddWithValue("@propiedad", TxtPropiedad.Text.ToUpper());

                try
                {
                    cnn.Open();
                    insert.ExecuteNonQuery();
                    MostrarMensaje("** Guardado correctamente **", "error", "Normal", "Incorrecto");
                    BuscarNuevaPropiedad();
                }
                catch (Exception)
                {
                    MostrarMensaje("** Error al registrar una propiedad **", "error", "Normal", "Incorrecto");
                    cnn.Close();
                }
            }
            else
            {
                MostrarMensaje("** El campo tipo propiedad es requerido **", "error", "Normal", "Incorrecto");
            }


        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void BtnMostraOcultar(object sender, EventArgs e)
        {

            if (DivMostrarNuevoR.Visible == false)
            {
                LimpiarRegistro();
                DivMostrarNuevoR.Visible = true;
                BtnGuardar.Visible = true;
                BtnActualizar.Visible = false;
                lgNuevoRegistro.Visible = true;
                lgModificarRegistro.Visible = false;
                BtnCancelar.Visible = true;
                BtnLimpiar.Visible = true;
            }
            else if (DivMostrarNuevoR.Visible == true)
            {
                DivMostrarNuevoR.Visible = false;
                BtnGuardar.Visible = false;
                BtnLimpiar.Visible = false;
                BtnCancelar.Visible = false;
            }


        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void BtnLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarRegistro();
            DivMostrarNuevoR.Visible = false;
            DivTabla.Visible = false;
            BtnLimpiar.Visible = false;
            BtnActualizar.Visible = false;
            BtnGuardar.Visible = false;
            BtnCancelar.Visible = false;
            LimpiarBuscar();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void GridBuscarPropiedad_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            DivMostrarNuevoR.Visible = true;
            BtnActualizar.Visible = true;
            BtnGuardar.Visible = false;
            lgNuevoRegistro.Visible = false;
            lgModificarRegistro.Visible = true;
            BtnCancelar.Visible = true;
            BtnLimpiar.Visible = true;

            int index = Convert.ToInt32(e.CommandArgument);
            GridViewRow RowSelecionada = GridBuscar.Rows[index];
            string IdPropiedad = Page.Server.HtmlDecode(RowSelecionada.Cells[0].Text);
            string Propiedad = Page.Server.HtmlDecode(RowSelecionada.Cells[1].Text);

            TxtPropiedad.Text = Propiedad;
            LbId.Text = IdPropiedad;

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void GridBuscarPropiedad_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            DivMostrarNuevoR.Visible = false;
            BtnActualizar.Visible = false;
            BtnGuardar.Visible = false;
            BtnCancelar.Visible = true;
            BtnLimpiar.Visible = true;
            HiddenId.Value = TxtPropiedad.Text;
            if (String.IsNullOrEmpty(Rowindex.Value))
            {
                Rowindex.Value = e.RowIndex.ToString();
                MostrarMensaje("Prueba", "info", "NotificacionEliminar", "Inicio");
            }
            else
            {
                int index = Convert.ToInt32(e.RowIndex);
                try
                {
                    int n = e.RowIndex;
                    string xcod = GridBuscar.DataKeys[n].Value.ToString();
                   Int32 id= Convert.ToInt32(GridBuscar.DataKeys[n].Value.ToString());
                    
                    id ++;
                    obJN.IdPropiedad = xcod;
                    obJE.Eliminar_Propiedad(obJN);

                    GridBuscar.EditIndex = -1;
                    BuscarNuevaPropiedad();
                    MostrarMensaje("** Propiedad eliminado **", "error", "Normal", "Incorrecto");
                    Rowindex.Value = "";
                }
                catch (Exception)
                {
                    Rowindex.Value = "";
                    MostrarMensaje("** Error al eliminar **", "error", "Normal", "Incorrecto");
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void GridBuscar_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridBuscar.PageIndex = e.NewPageIndex;
            BuscarPropiedades();

        }
    }
}