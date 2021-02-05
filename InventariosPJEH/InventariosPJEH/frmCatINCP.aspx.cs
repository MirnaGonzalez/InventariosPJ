using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using InventariosPJEH.CAccesoDatos;
using InventariosPJEH.CNegocios;


namespace InventariosPJEH
{
    public partial class frmCatINCP : System.Web.UI.Page
    {
        CINCP obJN = new CINCP();
        BdCatINCP obJE = new BdCatINCP();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.Form["__EVENTTARGET"] == "AccionEliminarr")
            {
                GridBuscarINCP_RowDeleting(this, new GridViewDeleteEventArgs(Int32.Parse(Rowindex.Value)));

            }
            else if (Request.Form["__EVENTTARGET"] == "AccionVacio")
            {
                Rowindex.Value = string.Empty;
            }

            if (!this.IsPostBack)
            {

                IniciarLlenadoDropDown();

            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void IniciarLlenadoDropDown()
        {
            DataTable Datos = new DataTable();
            Datos = BdCatINCP.Iniciar();
            DropINCP.DataSource = Datos;
            DropINCP.DataTextField = "Anio";
            DropINCP.DataBind();
            DropINCP.Items.Insert(0, new ListItem("Seleccionar", "0"));
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void BtnBuscar_Click(object sender, EventArgs e)
        {
            BuscarINCP();
        }

       
        /// <summary>
        /// 
        /// </summary>
        public void BuscarINCP()
        {
           // if (DropINCP.SelectedIndex != 0)
          //  {
                string incp = Convert.ToString(DropINCP.SelectedItem);
                DivTabla.Visible = true;
                GridBuscar.DataSource = BdCatINCP.ConsultarGbINCP(incp);              
                GridBuscar.DataBind();
                if (GridBuscar.Rows.Count == 0)
                {
                    MostrarMensaje("** No existen datos con la búsqueda solicitada **", "error", "Normal", "Incorrecto");
                }
        //    }
       //     else
         //   {
           //     MostrarMensaje("** Selecciona un año **", "error", "Normal", "Incorrecto");
            //}
        }

        public void BuscarINCPNuevo()
        {
            DivTabla.Visible = true;
            GridBuscar.DataSource = BdCatINCP.ConsultarGbINCP(TxtAnio.Text);
            GridBuscar.DataBind();
            if (GridBuscar.Rows.Count == 0)
            {
                MostrarMensaje("** No existen datos con la búsqueda solicitada **", "error", "Normal", "Incorrecto");
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
        public void LimpiarBuscar()
        {
            DropINCP.SelectedIndex = 0;

        }

        /// <summary>
        /// 
        /// </summary>
        public void LimpiarRegistro()
        {
            DropINCP.SelectedIndex = 0;
            TxtAnio.Text = string.Empty;
            TxtIVA.Text = string.Empty;
            TxtNumUmas.Text = string.Empty;
            TxtValorUma.Text = string.Empty;
            LbINCP.Text = string.Empty;
        }
 
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void BtnGuardarNuevoINCP(object sender, EventArgs e)
        {

            if (TxtAnio.Text != "")
            {
                if (TxtAnio.Text.Length == 4)
                {
                    if (TxtIVA.Text != "")
                    {
                        if (TxtNumUmas.Text != "")
                        {
                            if (TxtValorUma.Text != "")
                            {

                                BtnGuardar.Visible = true;
                                DivTabla.Visible = true;
                                SqlConnection cnn = new SqlConnection(CConexion.Obtener());

                                SqlCommand insert = new SqlCommand("INSERT INTO Cat_INPC(Anio,IVA,NumUMAS,ValorUMA,INCP) values(@anio, @iva,@numumas,@valorumas,@incp)", cnn);
                                insert.Parameters.AddWithValue("@anio", TxtAnio.Text);
                                insert.Parameters.AddWithValue("@iva", Convert.ToDecimal(TxtIVA.Text, CultureInfo.CreateSpecificCulture("en-US")));
                                insert.Parameters.AddWithValue("@numumas", Convert.ToDecimal(TxtNumUmas.Text, CultureInfo.CreateSpecificCulture("en-US")));
                                insert.Parameters.AddWithValue("@valorumas", Convert.ToDecimal(TxtValorUma.Text, CultureInfo.CreateSpecificCulture("en-US")));
                                insert.Parameters.AddWithValue("@incp", Convert.ToDecimal(LbINCP.Text, CultureInfo.CreateSpecificCulture("en-US")));

                                try
                                {
                                    cnn.Open();
                                    insert.ExecuteNonQuery();
                                    MostrarMensaje("** Registro exitoso **", "error", "Normal", "Incorrecto");
                                    if (DropINCP.SelectedIndex != 0)
                                    {
                                       
                                        BtnGuardar.Visible = false;
                                        BtnCancelar.Visible = false;
                                        DivMostrarNuevoR.Visible = false;
                                    }
                                    else
                                    {
                                     
                                        //BuscarINCPNuevo();
                                        BtnGuardar.Visible = false;
                                        BtnCancelar.Visible = false;
                                        DivMostrarNuevoR.Visible = false;
                                    }
                                    BuscarINCP();
                                    IniciarLlenadoDropDown();

                                }
                                catch (Exception )
                                {
                                    MostrarMensaje("** Error al registrar INCP **", "error", "Normal", "Incorrecto");
                                    cnn.Close();
                                }

                            }
                            else
                            {
                                MostrarMensaje("** El campo valor de umas es requerido **", "error", "Normal", "Incorrecto");
                            }
                        }
                        else
                        {
                            MostrarMensaje("** El campo número de umas es requerido **", "error", "Normal", "Incorrecto");
                        }
                    }
                    else
                    {
                        MostrarMensaje("** El campo IVA es requerido **", "error", "Normal", "Incorrecto");
                    }
                }
                else
                {
                    MostrarMensaje("** El campo año debe ser de 4 digítos **", "error", "Normal", "Incorrecto");
                }
            }
            else
            {
                MostrarMensaje("** El campo año es requerido *", "error", "Normal", "Incorrecto");
            }
        }

        protected void BtnActualizar_Click(object sender, EventArgs e)
        {
            if (TxtAnio.Text != "")
            {
                if (TxtAnio.Text.Length == 4)
                {
                    if (TxtIVA.Text != "")
                    {
                        if (TxtNumUmas.Text != "")
                        {
                            if (TxtValorUma.Text != "")
                            {
                                DivTabla.Visible = true;
                                SqlConnection cnn = new SqlConnection(CConexion.Obtener());
                                SqlCommand update = new SqlCommand("UPDATE Cat_INPC SET Anio=@anio,IVA=@iva,NumUMAS=@numumas,ValorUMA=@valorumas,INCP=@incp where IdINCP=@idINCP", cnn);
                                update.Parameters.AddWithValue("@anio", TxtAnio.Text);
                                update.Parameters.AddWithValue("@iva", Convert.ToDecimal(TxtIVA.Text, CultureInfo.CreateSpecificCulture("en-US")));
                                update.Parameters.AddWithValue("@numumas", Convert.ToDecimal(TxtNumUmas.Text, CultureInfo.CreateSpecificCulture("en-US")));
                                update.Parameters.AddWithValue("@valorumas", Convert.ToDecimal(TxtValorUma.Text, CultureInfo.CreateSpecificCulture("en-US")));
                                
                                update.Parameters.AddWithValue("@incp", Convert.ToDecimal(LbINCP.Text, CultureInfo.CreateSpecificCulture("en-US")));
                                //update.Parameters.AddWithValue ("@incp", Convert.ToDecimal(LbINCP.Text).ToString("###,###.00"));
                      
                                update.Parameters.AddWithValue("@idINCP", LbId.Text);

                                try
                                {
                                    cnn.Open();
                                    update.ExecuteNonQuery();
                                    MostrarMensaje("** Se actualizó correctamente **", "error", "Normal", "Incorrecto");
                                    BuscarINCP();
                                    IniciarLlenadoDropDown();
                                    BtnActualizar.Visible = false;
                                    BtnCancelar.Visible = false;
                                    DivMostrarNuevoR.Visible = false;
                                }
                                catch (Exception )
                                {
                                    MostrarMensaje("** Error al actualizar **", "error", "Normal", "Incorrecto");
                                    cnn.Close();
                                }

                            }
                            else
                            {
                                MostrarMensaje("** El campo valor de umas es requerido **", "error", "Normal", "Incorrecto");
                            }
                        }
                        else
                        {
                            MostrarMensaje("** El campo número de umas es requerido **", "error", "Normal", "Incorrecto");
                        }
                    }
                    else
                    {
                        MostrarMensaje("** El campo IVA es requerido **", "error", "Normal", "Incorrecto");
                    }
                }
                else
                {
                    MostrarMensaje("** El campo año debe ser de 4 digítos *", "error", "Normal", "Incorrecto");
                }
            }
            else
            {
                MostrarMensaje("** El campo año es requerido *", "error", "Normal", "Incorrecto");
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void GridBuscarINCP_RowCommand(object sender, GridViewCommandEventArgs e)
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

            string idINCP = GridBuscar.DataKeys[index].Values["IdINCP"].ToString();
            string anio = RowSelecionada.Cells[1].Text.Replace(",", ".");
            string iva = RowSelecionada.Cells[2].Text.Replace(",", ".");
            string numumas = RowSelecionada.Cells[3].Text.Replace(",", ".");
            string valorumas = RowSelecionada.Cells[4].Text.Replace(",", ".");
            string incp = RowSelecionada.Cells[5].Text;

            LbId.Text = idINCP;
            TxtAnio.Text = anio;
            TxtIVA.Text= iva;
            TxtNumUmas.Text = numumas; 
            TxtValorUma.Text = valorumas;
            LbINCP.Text = incp;
        }

        protected void GridBuscarINCP_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            DivMostrarNuevoR.Visible = false;
            BtnActualizar.Visible = false;
            BtnGuardar.Visible = false;
            BtnCancelar.Visible=false;
            if (String.IsNullOrEmpty(Rowindex.Value))
            {
                Rowindex.Value = e.RowIndex.ToString();
                MostrarMensaje("Prueba", "info", "NotificacionEliminar", "Inicio");
            }
            else
            {
                try
                {
                    int n = e.RowIndex;
                    string xcod = GridBuscar.DataKeys[n].Value.ToString();
                    string priveedor = GridBuscar.Rows[e.RowIndex].Cells[1].Text;
                    obJN.IdINCP = xcod;
                    obJE.Eliminar_INCP(obJN);
                    GridBuscar.EditIndex = -1;
                    BuscarINCP();
                    IniciarLlenadoDropDown();
                   // BuscarINCPNuevo();
                    MostrarMensaje("** INCP eliminado **", "error", "Aviso", "Incorrecto");
                    Rowindex.Value = "";
                    LimpiarBuscar();
                    if (GridBuscar.Rows.Count == 0)
                    {
                        DivTabla.Visible = false;
                        MostrarMensaje("** No existen datos con la búsqueda solicitada **", "error", "Normal", "Incorrecto");
                    }
                    else if (GridBuscar.Rows.Count != 0)
                    {
                        MostrarMensaje("** No se puede eliminar, existen campos relacionados a otras tablas **", "error", "Normal", "Incorrecto");

                    }

                }
                catch (Exception)
            {
                Rowindex.Value = "";
                MostrarMensaje("** Error al eliminar INCP **", "error", "Aviso", "Incorrecto");
            }
        }
        }

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
            //LimpiarRegistro();
            DivMostrarNuevoR.Visible = false;
            BtnGuardar.Visible = false;
            BtnCancelar.Visible = false;
        }

        protected void TxtValorUma_TextChanged(object sender, EventArgs e)
        {
            if (TxtNumUmas.Text !="")
            {
                String numUmas = TxtNumUmas.Text;
                String valorUmas = TxtValorUma.Text;
                Decimal incp = Convert.ToDecimal(numUmas, CultureInfo.CreateSpecificCulture("en-US")) * (Convert.ToDecimal(valorUmas, CultureInfo.CreateSpecificCulture("en-US")));
                Decimal resultado = Math.Truncate(Convert.ToDecimal(incp) * 100) / 100;
                LbINCP.Text = resultado.ToString("N2", new CultureInfo("en-US"));
            }
            else
            {

            }
           
        }

        protected void TxtNumUmas_TextChanged(object sender, EventArgs e)
        {
            if (TxtValorUma.Text !="")
            {
                String numUmas = TxtNumUmas.Text;
                String valorUmas = TxtValorUma.Text;
                Decimal incp = Convert.ToDecimal(numUmas, CultureInfo.CreateSpecificCulture("en-US")) * (Convert.ToDecimal(valorUmas, CultureInfo.CreateSpecificCulture("en-US")));
                Decimal resultado = Math.Truncate(Convert.ToDecimal(incp)*100)/100;
                LbINCP.Text = resultado.ToString("N2", new CultureInfo("en-US"));
            }
            else
            {

            }

           
        }

        protected void TxtIVA_TextChanged(object sender, EventArgs e)
        {

        }
    }
        }
