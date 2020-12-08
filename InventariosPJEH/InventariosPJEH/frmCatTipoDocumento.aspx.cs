using System;
using System.Collections.Generic;
using System.Data;
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
    public partial class frmCatTipoDocumento : System.Web.UI.Page
    {
        CTipoDocumento obJN = new CTipoDocumento();
        BdCatTipoDocumento obJE = new BdCatTipoDocumento();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.Form["__EVENTTARGET"] == "AccionEliminarr")
            {
                GridBuscarTipoDocumento_RowDeleting(this, new GridViewDeleteEventArgs(Int32.Parse(Rowindex.Value)));

            }
            else if (Request.Form["__EVENTTARGET"] == "AccionVacio")
            {
                Rowindex.Value = string.Empty;

            }

        }

        /// <summary>
        /// Evento del botón (Buscar) permite buscar el tipo de documento
        /// </summary>
        protected void BtnBuscar_Click(object sender, EventArgs e)
        {
            HiddenId.Value = TxtTipoDocExistente.Text;
            
            DivTabla.Visible = true;
            BuscarTipoDocumento();
            if (GridBuscar.Rows.Count == 0)
            {

                MostrarMensaje("** No existen datos con la búsqueda solicitada **", "error", "Normal", "Incorrecto");
                DivTabla.Visible = false;
            }
            BtnActualizar.Visible = false;
            BtnCancelar.Visible = false;
            BtnLimpiar.Visible = true;
            BtnGuardar.Visible = false;
        }

        /// <summary>
        /// Clase que maneja, en la BD, realiza la consulta solicitada en el botón Buscar
        /// </summary>
        public void BuscarTipoDocumento()
        {
          
                DivTabla.Visible = true;
                GridBuscar.DataSource = BdCatTipoDocumento.ConsultarTipoDocumento(TxtTipoDocExistente.Text);
                GridBuscar.DataBind();

                if (GridBuscar.Rows.Count == 0)
                {                  
                    MostrarMensaje("** No existen datos con la búsqueda solicitada **", "error", "Normal", "Incorrecto");
                    DivTabla.Visible = false;
                }

                DivMostrarNuevoR.Visible = false;
         

        }

        /// <summary>
        /// Clase que realiza la consulta a la Base de datos, posteriormente lo muestra en la tabla
        /// </summary>
        public void BuscarBuevoTipo()
        {
            DivTabla.Visible = true;

            GridBuscar.DataSource = BdCatTipoDocumento.ConsultarTipoDocumento(TxtTipoDocExistente.Text);
            GridBuscar.DataBind();

            if (GridBuscar.Rows.Count == 0)
            {               
                MostrarMensaje("** No existen datos con la búsqueda solicitada **", "error", "Normal", "Incorrecto");
                DivTabla.Visible = false;
            }
        }

        /// <summary>
        /// Clase para poder mostrar los mensajes en cada evento
        /// </summary>
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

        /// <summary>
        /// Clase para limpiar los datos de la seccion buscar 
        /// </summary>
        public void LimpiarBuscar()
        {
            TxtTipoDocExistente.Text = string.Empty;

        }

        /// <summary>
        /// Clase para limpiar los datos de la sección nuevo registro
        /// </summary>
        public void LimpiarRegistro()
        {
            TxtTipoDocumento.Text = string.Empty;
            LbId.Text = string.Empty;

        }

        /// <summary>
        /// Evento del botón (Nuevo registro) muestra la sección para realizar un nuevo registro,
        /// se puede mostrar y ocultar
        /// </summary>
        protected void BtnMostraOcultar(object sender, EventArgs e)
        {
            if (DivMostrarNuevoR.Visible == false)
            {
                LimpiarRegistro();
                DivTabla.Visible = false;
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
                DivTabla.Visible = false;
                BtnActualizar.Visible = false;
                BtnCancelar.Visible = false;
            }


        }

        /// <summary>
        /// Evento del botón (Editar) se encuentra dentro del GridView,
        /// realiza la edición del registro seleccionado dentro del GridView
        /// </summary> 
        protected void GridBuscarTipoDocumento_RowCommand(object sender, GridViewCommandEventArgs e)
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
            string IdTipoDoc = Page.Server.HtmlDecode(RowSelecionada.Cells[0].Text);
            string Documento = Page.Server.HtmlDecode(RowSelecionada.Cells[1].Text);

            TxtTipoDocumento.Text = Documento;
            LbId.Text = IdTipoDoc;

        }

        /// <summary>
        /// Evento del botón (Eliminar) se encuentra dentro del GridView,
        /// realiza la eliminacion del registro seleccionado, mostrando un mensaje de confirmación
        /// </summary>
        protected void GridBuscarTipoDocumento_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            DivMostrarNuevoR.Visible = false;
            BtnActualizar.Visible = false;
            BtnGuardar.Visible = false;
            BtnCancelar.Visible = false;
            BtnLimpiar.Visible = true;

            if (String.IsNullOrEmpty(Rowindex.Value))
            {
                Rowindex.Value = e.RowIndex.ToString();
                MostrarMensaje("Prueba", "info", "NotificacionEliminar", "Inicio");
            }
            else
            {
                int index = Convert.ToInt32(e.RowIndex);
                string tipoDoc = Page.Server.HtmlDecode(GridBuscar.DataKeys[index].Values["IdTipoDoc"].ToString());

                try
                {
                    int n = e.RowIndex;
                    string xcod = GridBuscar.DataKeys[n].Value.ToString();
                    obJN.IdTipoDoc = xcod;
                    obJE.Eliminar_TipoDoc(obJN);

                    GridBuscar.EditIndex = -1;

                    BuscarBuevoTipo();
                    MostrarMensaje("** Tipo documento eliminado **", "error", "Normal", "Incorrecto");
                    Rowindex.Value = "";
                    if (GridBuscar.Rows.Count == 0)
                    {
                        DivTabla.Visible = false;
                        MostrarMensaje("** No existen datos con la búsqueda solicitada **", "error", "Normal", "Incorrecto");
                    }
                    else if (GridBuscar.Rows.Count != 0)
                    {
                        MostrarMensaje("** No se puede eliminar existen campos relacionados a otras tablas **", "error", "Normal", "Incorrecto");

                    }

                }
                catch (Exception )
                {
                    Rowindex.Value = "";
                    MostrarMensaje("** Error al eliminar **", "error", "Normal", "Incorrecto");
                }
            }
        }

        /// <summary>
        /// Evento del botón (Actualizar) actualiza los datos y muestra en la tabla la actualización
        /// </summary>
        protected void BtnActualizar_Click(object sender, EventArgs e)
        {
            if (TxtTipoDocumento.Text != "")
            {
                DivTabla.Visible = true;

                SqlConnection cnn = new SqlConnection(CConexion.Obtener());

                SqlCommand cmd = new SqlCommand("UPDATE Cat_TipoDocumento SET Documento=@documento where IdTipoDoc=@idTipoDoc", cnn);
                cmd.Parameters.AddWithValue("@documento", TxtTipoDocumento.Text.ToUpper());
                cmd.Parameters.AddWithValue("@idTipoDoc", LbId.Text);
                try
                {

                    cnn.Open();
                    cmd.ExecuteNonQuery();
                    MostrarMensaje("** Se actualizo correctmente **", "error", "Normal", "Incorrecto");
                    BuscarBuevoTipo();
                    DivMostrarNuevoR.Visible = false;
                    BtnActualizar.Visible = false;
                    BtnCancelar.Visible = false;
                    LimpiarRegistro();

                }
                catch (Exception)
                {

                    MostrarMensaje("** Error al actualizar tipo documento **", "error", "Normal", "Incorrecto");
                    cnn.Close();
                }
            }
            else
            {
                MostrarMensaje("** El campo tipo documento es requerido**", "error", "Normal", "Incorrecto");

            }

        }

        /// <summary>
        /// Evento del botón (Cancelar) realiza la cancelación de cualquier acción realizada 
        /// </summary>
        protected void BtnCancelar_Click(object sender, EventArgs e)
        {
            LimpiarRegistro();
            DivMostrarNuevoR.Visible = false;
            BtnCancelar.Visible = false;
            BtnGuardar.Visible = false;
            BtnActualizar.Visible = false;
        }

        /// <summary>
        /// Evento del botón (Limpiar) limpia la página devolviendo a su estado inicial
        /// </summary>
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
        /// Evento para realizar la paginación del GridView
        /// </summary>
        protected void GridBuscar_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridBuscar.PageIndex = e.NewPageIndex;
            BuscarBuevoTipo();
        }

        /// <summary>
        /// Evento del botón (Guardar) guarda un nuevo registro en la BD.
        /// </summary>
        protected void BtnGuardarNuevoDocumento(object sender, EventArgs e)
        {
            if (TxtTipoDocumento.Text != "")
            {
                BtnGuardar.Visible = true;
                DivTabla.Visible = true;
                SqlConnection cnn = new SqlConnection(CConexion.Obtener());

                SqlCommand insert = new SqlCommand("insert into Cat_TipoDocumento(Documento) values(@documento)", cnn);
                insert.Parameters.AddWithValue("@documento", TxtTipoDocumento.Text.ToUpper());

                try
                {
                    cnn.Open();
                    insert.ExecuteNonQuery();

                    MostrarMensaje("** Guardado correctamente **", "error", "Normal", "Incorrecto");
                    BuscarBuevoTipo();
                    DivMostrarNuevoR.Visible = false;
                    BtnGuardar.Visible = false;
                    BtnCancelar.Visible = false;
                    LimpiarRegistro();

                }
                catch (Exception )
                {

                    MostrarMensaje("** Error al registrar tipo documento **", "error", "Normal", "Incorrecto");
                    cnn.Close();
                }
            }
            else
            {
                MostrarMensaje("** El campo tipo documento es requerido **", "error", "Normal", "Incorrecto");
            }
        }
    }
}