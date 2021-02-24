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

        private static Dictionary<CDatosResguardante, List<CDatosBienesActa>> datosBienesResguardo;
        private static List<CDatosBienesActa> datosBienes;
        private static List<CActa> datosActas;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Dictionary<CDatosResguardante, List<CDatosBienesActa>> datosBienesResguardo = new Dictionary<CDatosResguardante, List<CDatosBienesActa>>();
                List<CDatosBienesActa> datosBienes = new List<CDatosBienesActa>();
                List<CActa> datosActas = new List<CActa>();
            }
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

        protected void BtnFiltroB_Click(object sender, EventArgs e)
        {
            List<string> listaErrores = ValidarDatos();

            if (listaErrores.Count > 0)
            {
                string error = "";
                foreach (var itemError in listaErrores)
                {
                    error += " - " + itemError;
                }
                MostrarMensaje($"DATOS FALTANTES:  {error}", "error", "Normal", "Incorrecto");
                // DivMostrar.Visible = false;

            }
            else
            {
                datosBienesResguardo = buscarBienes();
                DivTablaResultadosResguardo.Visible = true;
                if (datosBienesResguardo.Count > 0)
                {
                    sectionBienes.Visible = true;
                    foreach (KeyValuePair<CDatosResguardante, List<CDatosBienesActa>> entry in datosBienesResguardo)
                    {
                        // do something with entry.Value or entry.Key
                        lblNombreResul.Text = entry.Key.txtONombreResguardante;
                        lblCargoResul.Text = entry.Key.txtOCargoResguardo;
                        lblAreaResul.Text = entry.Key.txtOAreaAdscripcionResguardo;
                        lblIdResguardo.Text = entry.Key.idResguardo.ToString();
                        lblIdUniAdmin.Text = entry.Key.idAreaAdscrip.ToString();
                        datosBienes = entry.Value;
                        gridResultadosBienes.DataSource = entry.Value;
                        gridResultadosBienes.DataBind();
                        sectionGuardarActa.Visible = true;
                        if (gridResultadosBienes.Rows.Count == 0)
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

        public Dictionary<CDatosResguardante, List<CDatosBienesActa>> buscarBienes()
        {
            Dictionary<CDatosResguardante, List<CDatosBienesActa>> lstActaAdmin = new Dictionary<CDatosResguardante, List<CDatosBienesActa>>();
            try
            {
                datosBienesResguardo = BdActaAdmin.buscarBienes(Convert.ToInt64(TxtFiltroB.Text));
                lstActaAdmin = datosBienesResguardo;
            }
            catch (Exception e)
            {
                MostrarMensaje("** Error en Base de Datos **", "error", "Normal", "Incorrecto");
            }
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
            gridResultadosBienes.DataSource = null;
            gridResultadosBienes.DataBind();
            sectionBienes.Visible = false;
        }

        protected void BtnInsertarActa_Click(object sender, EventArgs e)
        {
            //Capturamos los errores con try catch           
            try
            {
                if (!String.IsNullOrEmpty(txtNumActa.Text) && !String.IsNullOrEmpty(txtFechaAdquisicion.Text))
                {
                    int resultado = BdActaAdmin.InsertarActaInventario(int.Parse(lblIdUniAdmin.Text), int.Parse(lblIdResguardo.Text), txtNumActa.Text, txtFechaAdquisicion.Text, datosBienes);
                    if (resultado != 1)
                    {
                        if (resultado == 2)
                        {
                            MostrarMensaje("** El número de acta ya existe, favor de ingresar uno diferente **", "error", "Normal", "Incorrecto");
                        }
                        else
                        {
                            MostrarMensaje("** Error en Base de Datos **", "error", "Normal", "Incorrecto");
                        }
                    }
                    else
                    {
                        limpiarCamposGenerarActa();
                        MostrarMensaje("Acta administrativa generada de forma correcta", "info", "Normal", "ModificacionCorrecta");
                    }
                }
                else
                {
                    MostrarMensaje("** Datos Faltantes **", "error", "Normal", "Incorrecto");
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
                if (!String.IsNullOrEmpty(txtEditConActaFechaSol.Text) && !lstEstatus.SelectedValue.Equals("-1"))
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
                        consultarActa();
                    }
                }
                else
                {
                    MostrarMensaje("** Datos Faltantes **", "error", "Normal", "Incorrecto");
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

        public void consultarActa()
        {
            datosActas = BdActaAdmin.ConsultarActas(txtConNumActa.Text, txtConNumInventario.Text, txtConFechaIni.Text, txtConFechaFin.Text);
            gridConsultaActas.DataSource = datosActas;
            gridConsultaActas.DataBind();
            tablaConsultaActas.Visible = true;
            if (gridConsultaActas.Rows.Count == 0)
            {
                MostrarMensaje("** No existen datos con la búsqueda solicitada **", "error", "Normal", "Incorrecto");
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
                        consultarActa();
                    }
                }
                else
                {
                    if ((String.IsNullOrWhiteSpace(txtConFechaIni.Text) || String.IsNullOrWhiteSpace(txtConFechaFin.Text)))
                    {
                        MostrarMensaje("** Datos Faltantes **", "error", "Normal", "Incorrecto");
                    }
                    else
                    {
                        consultarActa();
                    }
                }

            }
            catch (Exception ex)
            {
                MostrarMensaje("** Error en Base de Datos **", "error", "Normal", "Incorrecto");
                Console.WriteLine(ex.ToString());
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
            CActa acta = datosActas.Find(x => x.strNumActa == txtEditNumActa.Text);
            if (acta != null)
            {
                if (!acta.strStatus.Equals("A"))
                {
                    checkConActasFechaMod.Checked = true;
                    txtEditConActaDescripcion.Visible = true;
                    celdaConActasFechaSolucion.Visible = true;
                    lblConActaEstatus.Visible = true;
                    lstEstatus.Visible = true;
                    lblEditConActasDescripcion.Visible = true;
                    txtEditConActaFechaSol.Text = acta.strFechaCancela;
                    txtEditConActaDescripcion.Text = acta.strDescripcionActa;
                    lstEstatus.SelectedValue = acta.strStatus;
                }
            }
            Console.WriteLine();
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
            divConsultaActasEditar.Visible = false;
        }

        //protected void BtnSetFechaFinConsulta(object sender, EventArgs e)
        //{
        //    calConFechaFin.SelectedDate = calConFechaIni.SelectedDate.Value.AddDays(1);            
        //}
    }
}