using System;
using System.Collections.Generic;
using InventariosPJEH.CAccesoDatos;
using InventariosPJEH.CNegocios;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace InventariosPJEH
{
    public partial class frmActaAdministrativa : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            BtnFiltroB_Click();
        }

        protected void BtnActasRadio_CheckedChanged(object sender, EventArgs e)
        {

            if (tabGeneracionRadio.Checked == true)
            {
                divGeneracionActas.Visible = true;
                divConsultaActas.Visible = false;
                cancelarEdicionConsultaActa();
            }
            if (tabConsultaRadio.Checked == true)
            {
                divConsultaActas.Visible = true;
                divGeneracionActas.Visible = false;
                limpiarCamposGenerarActa();
            }

        }

        protected void BtnFiltroB_Click()
        {
            List<string> listaErrores = ValidarDatos();

            if (listaErrores.Count > 0)
            {
                string error = "";
                foreach (var itemError in listaErrores)
                {
                    error += " - " + itemError;
                }
               // MostrarMensaje($"DATOS FALTANTES:  {error}", "info", "Intervalo", "Inicio");
                // DivMostrar.Visible = false;

            }
            else
            {
                Dictionary<CDatosResguardante, List<CDatosBienesActa>> resBusqueda = MostrarFiltro();
                DivTablaResultadosResguardo.Visible = true;
                if(resBusqueda.Count > 0)
                {
                    sectionBienes.Visible = true;
                    foreach (KeyValuePair<CDatosResguardante, List<CDatosBienesActa>> entry in resBusqueda)
                    {
                        // do something with entry.Value or entry.Key
                        lblNombreResul.Text = entry.Key.txtONombreResguardante;
                        lblCargoResul.Text = entry.Key.txtOCargoResguardo;
                        lblAreaResul.Text = entry.Key.txtOAreaAdscripcionResguardo;
                        lblIdResguardo.Text = entry.Key.idResguardo.ToString();
                        lblIdUniAdmin.Text = entry.Key.idAreaAdscrip.ToString();
                        gridResultados.DataSource = entry.Value;
                        gridResultados.DataBind();
                        sectionGuardarActa.Visible = true;
                        if (gridResultados.Rows.Count == 0)
                        {
                            MostrarMensaje("** No existen datos con la búsqueda solicitada **", "error", "Normal", "Incorrecto");
                        }                        
                    }
                }
                else
                {
                    sectionBienes.Visible = false;
                    MostrarMensaje("** No existen datos con la búsqueda solicitada **", "error", "Normal", "Incorrecto");
                }
                
              
            }

        }

        private List<string> ValidarDatos()
        {
            List<string> LErrores = new List<string>();

            if (string.IsNullOrEmpty(TxtFiltroB.Text))
            {
                LErrores.Add("Numero de Inventario");
            }
            return LErrores;
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

        public Dictionary<CDatosResguardante, List<CDatosBienesActa>> MostrarFiltro()
        {
            Dictionary<CDatosResguardante, List<CDatosBienesActa>> lstActaAdmin = BdActaAdmin.MostrarBusqueda(Convert.ToInt64(TxtFiltroB.Text));
            return lstActaAdmin;
        }

        public void limpiarCamposGenerarActa()
        {
            lblNombreResul.Text = "";
            lblCargoResul.Text = "";
            lblAreaResul.Text = "";
            lblIdResguardo.Text = "";
            lblIdUniAdmin.Text = "";
            txtNumActa.Text = "";
            txtFechaAdquisicion.Text = "";
            TxtFiltroB.Text = "";
            gridResultados.DataSource = null;
            gridResultados.DataBind();
            sectionBienes.Visible = false;
        }

        protected void BtnInsertarActa_Click(object sender, EventArgs e)
        {
            //Capturamos los errores con try catch
            try
            {
                if(!BdActaAdmin.InsertarActa(int.Parse(lblIdUniAdmin.Text), int.Parse(lblIdResguardo.Text), txtNumActa.Text, txtFechaAdquisicion.Text))
                {
                    MostrarMensaje("** Error en Base de Datos **", "error", "Normal", "Incorrecto");
                }
                else
                {
                    limpiarCamposGenerarActa();
                    MostrarMensaje("La Insersión se realizó correctamente", "info", "Normal", "ModificacionCorrecta");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void BtnGuardarEditActa_Click(object sender, EventArgs e)
        {
            //Capturamos los errores con try catch
            try
            {
                if (!BdActaAdmin.ActualizarActa(txtEditNumActa.Text, txtEditConActaFechaSol.Text, lstEstatus.SelectedValue, txtEditConActaDescripcion.Text))
                {
                    MostrarMensaje("** Error en Base de Datos **", "error", "Normal", "Incorrecto");
                }
                else
                {
                    cancelarEdicionConsultaActa();
                    divConsultaActasEditar.Visible = false;
                    MostrarMensaje("La modificación se realizó correctamente", "info", "Normal", "ModificacionCorrecta");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void BtnCancelar_Click(object sender, EventArgs e)
        {
            //Capturamos los errores con try catch
            try
            {
                limpiarCamposGenerarActa();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        protected void BtnCancelarEdit_Click(object sender, EventArgs e)
        {
            //Capturamos los errores con try catch
            try
            {
                cancelarEdicionConsultaActa();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        protected void BtnActivarPeriodo(object sender, EventArgs e)
        {            
            if (checkConPeriodo.Checked == true)
            {
                celdaConFechaIni.Visible = true;
                TceldaConFechaFin.Visible = true;
                txtConFechaIni.Text = "";
                txtConFechaFin.Text = "";
                lblConNumActa.Visible = false;
                txtConNumActa.Visible = false;
                lblConNumInventario.Visible = false;
                txtConNumInventario.Visible = false;
                txtConNumActa.Text = "";
                txtConNumInventario.Text = "";
            }
            else
            {
                txtConFechaIni.Text = "";
                txtConFechaFin.Text = "";
                celdaConFechaIni.Visible = false;
                TceldaConFechaFin.Visible = false;
                lblConNumActa.Visible = true;
                txtConNumActa.Visible = true;
                lblConNumInventario.Visible = true;
                txtConNumInventario.Visible = true;
                txtConNumActa.Text = "";
                txtConNumInventario.Text = "";
            }            
        }

        protected void BtnConsultarActa(object sender, EventArgs e)
        {
            try
            {
                if (!checkConPeriodo.Checked)
                {
                    if (String.IsNullOrWhiteSpace(txtConNumActa.Text) && String.IsNullOrWhiteSpace(txtConNumInventario.Text))
                    {
                        MostrarMensaje("** Datos Faltantes **", "error", "Normal", "Incorrecto");
                    }
                    else
                    {
                        gridConsultaActas.DataSource = BdActaAdmin.ConsultarActas(txtConNumActa.Text, txtConNumInventario.Text, txtConFechaIni.Text, txtConFechaFin.Text);
                        gridConsultaActas.DataBind();
                        tablaConsultaActas.Visible = true;
                        if (gridConsultaActas.Rows.Count == 0)
                        {
                            MostrarMensaje("** No existen datos con la búsqueda solicitada **", "error", "Normal", "Incorrecto");
                        }
                    }
                }
                else
                {
                    if ((String.IsNullOrWhiteSpace(txtConFechaIni.Text) && String.IsNullOrWhiteSpace(txtConFechaFin.Text)))
                    {
                        MostrarMensaje("** Datos Faltantes **", "error", "Normal", "Incorrecto");
                    }
                    else
                    {
                        gridConsultaActas.DataSource = BdActaAdmin.ConsultarActas(txtConNumActa.Text, txtConNumInventario.Text, txtConFechaIni.Text, txtConFechaFin.Text);
                        gridConsultaActas.DataBind();
                        tablaConsultaActas.Visible = true;
                        if (gridConsultaActas.Rows.Count == 0)
                        {
                            MostrarMensaje("** No existen datos con la búsqueda solicitada **", "error", "Normal", "Incorrecto");
                        }
                    }
                }               
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void btnEditar_Click(object sender, EventArgs e)
        {            
            try
            {
                divConsultaActasEditar.Visible = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void GridModificar_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            GridViewRow RowSelecionada = gridConsultaActas.Rows[index];  
            txtEditNumActa.Text = Page.Server.HtmlDecode(RowSelecionada.Cells[0].Text);
            txtEditActaFechaActa.Text = Page.Server.HtmlDecode(RowSelecionada.Cells[3].Text);            
        }

        protected void BtnConActasFechaModificar(object sender, EventArgs e)
        {
            if (checkConActasFechaMod.Checked)
            {
                txtEditConActaDescripcion.Visible = true;
                celdaConActasFechaSolucion.Visible = true;
                lblConActaEstatus.Visible = true;
                lstEstatus.Visible = true;
                lblEditConActasDescripcion.Visible = true;
            }
            else
            {
                txtEditConActaDescripcion.Visible = false;
                celdaConActasFechaSolucion.Visible = false;
                lblConActaEstatus.Visible = false;
                lstEstatus.Visible = false;
                lblEditConActasDescripcion.Visible = false;
                txtEditConActaDescripcion.Text = "";
                txtEditConActaFechaSol.Text = "";
                lstEstatus.SelectedValue = "-1";
            }
            

        }

        protected void BtnLimpiarActa(object sender, EventArgs e)
        {
            cancelarEdicionConsultaActa();
            divConsultaActasEditar.Visible = false;
            tablaConsultaActas.Visible = false;
            gridConsultaActas.DataSource = null;
            gridConsultaActas.DataBind();
            txtConNumActa.Text = "";
            txtConNumInventario.Text = "";
        }
        

        public void cancelarEdicionConsultaActa()
        {            
            txtEditConActaDescripcion.Visible = false;
            celdaConActasFechaSolucion.Visible = false;
            lblConActaEstatus.Visible = false;
            lstEstatus.Visible = false;
            lblEditConActasDescripcion.Visible = false;
            txtEditConActaDescripcion.Text = "";
            txtEditConActaFechaSol.Text = "";
            lstEstatus.SelectedValue = "-1";
            txtEditNumActa.Text = "";
            txtEditActaFechaActa.Text = "";
            checkConActasFechaMod.Checked = false;
        }

        //protected void BtnSetFechaFinConsulta(object sender, EventArgs e)
        //{
        //    calConFechaFin.SelectedDate = calConFechaIni.SelectedDate.Value.AddDays(1);            
        //}
    }
}