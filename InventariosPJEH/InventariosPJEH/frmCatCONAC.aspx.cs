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

namespace InventariosPJEH
{
    public partial class frmCatCONAC : System.Web.UI.Page
    {
        CCONAC obJN = new CCONAC();
        BdCatCONAC obJE = new BdCatCONAC();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.Form["__EVENTTARGET"] == "AccionEliminarr")
            {
                GridBuscarCONAC_RowDeleting(this, new GridViewDeleteEventArgs(Int32.Parse(Rowindex.Value)));

            }
            else if (Request.Form["__EVENTTARGET"] == "AccionVacio")
            {
                Rowindex.Value = string.Empty;

            }
            DateTime today = DateTime.Today;
            Console.WriteLine(today);
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

        //Evento para buscar un registro
        protected void BtnBuscar_Click(object sender, EventArgs e)
        {
            HiddenId.Value = IdCONAC.Text;

            DivTabla.Visible = true;
            GridBuscar.DataSource = BdCatCONAC.ConsultarGbCONAC(TxtDescripcionBuscar.Text);
            GridBuscar.DataBind();
            if (GridBuscar.Rows.Count == 0)
            {
                DivTabla.Visible = false;
                MostrarMensaje("** No existen datos con la búsqueda solicitada **", "error", "Normal", "Incorrecto");
            }
        }

               

        //Método para buscar el nuevo registro
        public void BuscarNuevoRegistro()
        {
            string partida = Convert.ToString(DropTpoPartida.SelectedValue);
            DivTabla.Visible = true;
            DivMostrarNuevoR.Visible = false;
            GridBuscar.DataSource = BdCatCONAC.ConsultarGbCONACNuevoRegistro(IdCONAC.Text, TxtGrupo.Text, TxtSubGrupo.Text, TxtClase.Text);
            GridBuscar.DataBind();
            if (GridBuscar.Rows.Count == 0)
            {
                DivTabla.Visible = false;

                MostrarMensaje("** No existen datos con la búsqueda solicitada **", "error", "Normal", "Incorrecto");
            }
        }

        //Evento para mostrar y ocultar un nuevo registro
        protected void BtnMostraOcultar(object sender, EventArgs e)
        {
            if (DivMostrarNuevoR.Visible == false)
            {
              
                DivMostrarNuevoR.Visible = true;
                BtnGuardar.Visible = true;
                BtnActualizar.Visible = false;
                lgNuevoRegistro.Visible = true;
                lgModificarRegistro.Visible = false;
                BtnCancelar.Visible = true;
                BtnLimpiar.Visible = true;
                LimpiarRegistro();
            }
            else if (DivMostrarNuevoR.Visible == true)
            {
                DivMostrarNuevoR.Visible = false;
                BtnGuardar.Visible = false;
                BtnLimpiar.Visible = false;
                BtnCancelar.Visible = false;

            }
        }

        //Evento para limpiar toda la página
        protected void BtnLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarRegistro();
            DivMostrarNuevoR.Visible = false;
            DivTabla.Visible = false;
            BtnLimpiar.Visible = false;
            BtnActualizar.Visible = false;
            BtnGuardar.Visible = false;
            BtnCancelar.Visible = false;
            TxtDescripcionBuscar.Text = string.Empty;

        }

        //Método para limpiar el nuevo registro 
        public void LimpiarRegistro()
        {
            DropTpoPartida.SelectedIndex = 0;
            TxtGrupo.Text = string.Empty;
            TxtSubGrupo.Text = string.Empty;
            TxtClase.Text = string.Empty;
            TxtDescripcion.Text = string.Empty;
        }

        //Evento para limpiar el botón cancelar 
        protected void BtnCancelar_Click(object sender, EventArgs e)
        {
            if (lgNuevoRegistro.Visible == true)
            {
                BtnLimpiar.Visible = true;
            }
            else if (lgModificarRegistro.Visible == true)
            {
                BtnLimpiar.Visible = true;
                BtnActualizar.Visible = false;
                BtnMostrarNuevoR.Visible = true;
            }
            DivMostrarNuevoR.Visible = false;
            BtnGuardar.Visible = false;
            BtnCancelar.Visible = false;
            BtnActualizar.Visible = false;

        }

        //Evento para actualizar un registro
        protected void BtnActualizar_Click(object sender, EventArgs e)
        {
            HiddenId.Value = IdCONAC.Text;
            if (TxtGrupo.Text != "")
            {
                if (TxtSubGrupo.Text != "")
                {
                    if (TxtClase.Text != "")
                    {
                        if (TxtDescripcion.Text != "")
                        {
                            if (DropTpoPartida.SelectedIndex != 0)
                            {
                                BtnGuardar.Visible = false;
                                DivTabla.Visible = true;
                                string tipoPartida = Convert.ToString(DropTpoPartida.SelectedValue);
                                SqlConnection cnn = new SqlConnection(CConexion.Obtener());
                                IdCONAC.Text = TxtGrupo.Text + TxtSubGrupo.Text + TxtClase.Text;
                                SqlCommand update = new SqlCommand("UPDATE Cat_CONAC SET IdCONAC=@idCONAC, Grupo=@grupo,SubGrupo=@subGrupo,Clase=@clase,Descripcion=@descripcion,TipoPartida=@tipoPartida WHERE IdClaveCONAC=@IdClaveCONAC", cnn);
                                update.Parameters.AddWithValue("@IdClaveCONAC", IdClaveCONAC.Text);
                                update.Parameters.AddWithValue("@idCONAC", IdCONAC.Text);
                                update.Parameters.AddWithValue("@grupo", TxtGrupo.Text);
                                update.Parameters.AddWithValue("@subGrupo", TxtSubGrupo.Text);
                                update.Parameters.AddWithValue("@clase", TxtClase.Text);
                                update.Parameters.AddWithValue("@descripcion", TxtDescripcion.Text.ToUpper());
                                update.Parameters.AddWithValue("@tipoPartida", tipoPartida);

                                try
                                {
                                    cnn.Open();
                                    update.ExecuteNonQuery();
                                    MostrarMensaje("** Se actualizó correctamente **", "error", "Normal", "Incorrecto");
                                    BuscarNuevoRegistro();
                                    DivMostrarNuevoR.Visible = false;
                                    BtnCancelar.Visible = false;
                                    BtnActualizar.Visible = false;
                                }
                                catch (Exception )
                                {
                                    MostrarMensaje("** Error al registrar CONAC **", "error", "Normal", "Incorrecto");
                                    cnn.Close();
                                }
                            }
                            else
                            {
                                MostrarMensaje("** Selecciona un tipo de partida **", "error", "Normal", "Incorrecto");
                            }

                        }
                        else
                        {
                            MostrarMensaje("** El campo descrpción es requerido **", "error", "Normal", "Incorrecto");
                        }
                    }
                    else
                    {
                        MostrarMensaje("** El campo clase es requerido **", "error", "Normal", "Incorrecto");
                    }
                }
                else
                {
                    MostrarMensaje("** El campo subgrupo es requerido **", "error", "Normal", "Incorrecto");
                }
            }
            else
            {
                MostrarMensaje("** El campo grupo es requerido *", "error", "Normal", "Incorrecto");
            }
        }

        //Evento para editar un nuevo registro
        protected void GridBuscarCONAC_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            DivMostrarNuevoR.Visible = true;
            BtnActualizar.Visible = true;
            BtnGuardar.Visible = false;
            BtnCancelar.Visible = true;
            BtnLimpiar.Visible = true;
            lgNuevoRegistro.Visible = false;
            lgModificarRegistro.Visible = true;
            int index = Convert.ToInt32(e.CommandArgument);
            GridViewRow RowSelecionada = GridBuscar.Rows[index];
  
            string IdClaveCONAC = RowSelecionada.Cells[0].Text;
            string idCONAC = RowSelecionada.Cells[1].Text;
            string grupo = RowSelecionada.Cells[2].Text;
            string subGrupo = RowSelecionada.Cells[3].Text;
            string clase = RowSelecionada.Cells[4].Text;
            string descripcion = Page.Server.HtmlDecode(Page.Server.HtmlDecode(RowSelecionada.Cells[5].Text));
            string tipoPartida = Page.Server.HtmlDecode((RowSelecionada.Cells[6].Controls[1] as Label).Text);
            TxtGrupo.Text = grupo;
            TxtSubGrupo.Text = subGrupo;
            TxtClase.Text = clase;
            TxtDescripcion.Text = descripcion;
            IdCONAC.Text = idCONAC;
            this.IdClaveCONAC.Text = IdClaveCONAC;

            Boolean encontrado = true;
            int a = 0;
            
            while (encontrado)
            {
                if (DropTpoPartida.Items[a].Text == tipoPartida)
                {
                    DropTpoPartida.SelectedIndex = a;
                    encontrado = false;
                }
                else if (DropTpoPartida.Items[a].Text != tipoPartida)
                {
                    a++;
                }     
            }
        }

        //Evento para eliminar un registro
        protected void GridBuscarCONAC_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            DivMostrarNuevoR.Visible = false;
            BtnActualizar.Visible = false;
            BtnGuardar.Visible = false;
            DivMostrarNuevoR.Visible = false;
            BtnActualizar.Visible = false;
            BtnCancelar.Visible = false;
            if (String.IsNullOrEmpty(Rowindex.Value))
            {
                Rowindex.Value = e.RowIndex.ToString();
                MostrarMensaje("Prueba", "info", "NotificacionEliminar", "Inicio");
            }
            else
            {
                Boolean encontrado = true;
                try
                {
                    if (encontrado == true)
                    {

                        int n = e.RowIndex;
                        int index = Convert.ToInt32(e.RowIndex);
                        GridViewRow RowSelecionada = GridBuscar.Rows[index];
                        string xcod = GridBuscar.DataKeys[n].Value.ToString();

                        obJN.IdClaveCONAC = xcod;
                        obJE.Eliminar_CONAC(obJN);
                        GridBuscar.EditIndex = -1;
                        BuscarNuevoRegistro();
                        MostrarMensaje("** Registro eliminado de forma correcta**", "error", "Aviso", "Incorrecto");
                        Rowindex.Value = "";


                        //// REVISAR

                        if (GridBuscar.Rows.Count == 0)
                        {
                            DivTabla.Visible = false;
                            MostrarMensaje("** No existen datos con la búsqueda solicitada **", "error", "Normal", "Incorrecto");
                        }
                        else if (GridBuscar.Rows.Count != 0)
                        {
                            MostrarMensaje("** No se puede eliminar, existen datos relacionados **", "error", "Normal", "Incorrecto");

                        }


                    }
                    }
                    catch (Exception)
                    {
                        Rowindex.Value = "";
                        MostrarMensaje("** Error al eliminar el registro **", "error", "Aviso", "Incorrecto");
                    
                }
            }
           
        }

        //Evento para guardar un nuevo registro
        protected void Guardar_Click(object sender, EventArgs e)
        {
            if (TxtGrupo.Text != "")
            {
                if (TxtSubGrupo.Text != "")
                {
                    if (TxtClase.Text != "")
                    {
                        if (TxtDescripcion.Text != "")
                        {
                            if (DropTpoPartida.SelectedIndex != 0)
                            {


                                BtnGuardar.Visible = true;
                                DivTabla.Visible = true;
                                string tipoPartida = Convert.ToString(DropTpoPartida.SelectedValue);
                                SqlConnection cnn = new SqlConnection(CConexion.Obtener());
                                IdCONAC.Text = TxtGrupo.Text + TxtSubGrupo.Text + TxtClase.Text;
                                SqlCommand insert = new SqlCommand("INSERT INTO Cat_CONAC(IdCONAC,Grupo,SubGrupo,Clase,Descripcion,TipoPartida) values(@idCONAC, @grupo,@subGrupo,@clase,@descripcion,@tipoPartida)", cnn);
                                insert.Parameters.AddWithValue("@idCONAC", IdCONAC.Text);
                                insert.Parameters.AddWithValue("@grupo", TxtGrupo.Text);
                                insert.Parameters.AddWithValue("@subGrupo", TxtSubGrupo.Text);
                                insert.Parameters.AddWithValue("@clase", TxtClase.Text);
                                insert.Parameters.AddWithValue("@descripcion", TxtDescripcion.Text.ToUpper());
                                insert.Parameters.AddWithValue("@tipoPartida", tipoPartida);

                                try
                                {
                                    cnn.Open();
                                    insert.ExecuteNonQuery();
                                    MostrarMensaje("** Registro exitoso **", "error", "Normal", "Incorrecto");
                                    BuscarNuevoRegistro();
                                    DivMostrarNuevoR.Visible = false;
                                    BtnGuardar.Visible = false;
                                    BtnCancelar.Visible = false;
                                    LimpiarRegistro();

                                }
                                catch (Exception )
                                {
                                    MostrarMensaje("** Error al registrar CONAC **", "error", "Normal", "Incorrecto");
                                    cnn.Close();
                                }
                            }
                            else
                            {
                                MostrarMensaje("** Selecciona un tipo de partida **", "error", "Normal", "Incorrecto");
                            }

                        }
                        else
                        {
                            MostrarMensaje("** El campo descrpción es reuqerido **", "error", "Normal", "Incorrecto");
                        }
                    }
                    else
                    {
                        MostrarMensaje("** El campo clase es requerido **", "error", "Normal", "Incorrecto");
                    }
                }
                else
                {
                    MostrarMensaje("** El campo subgrupo es requerido **", "error", "Normal", "Incorrecto");
                }
            }
            else
            {
                MostrarMensaje("** El campo grupo es requerido *", "error", "Normal", "Incorrecto");
            }
        }

        protected void GridBuscar_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridBuscar.PageIndex = e.NewPageIndex;

            HiddenId.Value = IdCONAC.Text;
            BuscarNuevoRegistro();
        }
    }
}