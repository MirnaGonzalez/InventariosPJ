using InventariosPJEH.CAccesoDatos;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AjaxControlToolkit;
using InventariosPJEH.CNegocios;

namespace InventariosPJEH
{
    public partial class RecibirBien : System.Web.UI.Page
    {
        public SqlConnection conexion;
        public RecibirBien()
        {
            this.conexion = ConexionBD.getConexion();
        }
        protected void Page_Load(object sender, EventArgs e)
        {
        }
        protected void BtnBuscar_Click(object sender, EventArgs e)
        {
            BuscarInventario();
            BuscarSerie();
            div1.Visible = true;
        }
        public void BuscarInventario()
        {
            if (TxtNumInventario.Text != "")
            {
                GridPrueba.DataSource = BdRecibirBien.ConsultarGbBienesReparar(TxtNumInventario.Text);
                GridPrueba.DataBind();
                if (GridPrueba.Rows.Count == 0)
                {
                    MostrarMensaje("**Este Número de Inventario ya ha sido registrado para Mantenimiento**", "error", "Normal", "Incorrecto");
                    
                }
            }
        }
        public void BuscarSerie()
        {
            if (TextBox2.Text != "")
            {

                GridPrueba.DataSource = BdRecibirBien.ConsultarGbBienesReparar1(TextBox2.Text);
                GridPrueba.DataBind();
                if (GridPrueba.Rows.Count == 0)
                {
                    MostrarMensaje("** Este Número de Inventario ya ha sido registrado para Mantenimiento **", "error", "Normal", "Incorrecto");

                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Mensaje"></param>
        /// <param name="Tipo"></param>
        /// <param name="TipoFuncion"></param>
        /// <param name="ClaveMsj"></param>
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
        protected void GridPrueba_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            GridViewRow RowSelecionada = GridPrueba.Rows[index];
            string IdRegMant = GridPrueba.DataKeys[index].Values["IdRegMant"].ToString();
            string IdInventario = GridPrueba.DataKeys[index].Values["IdInventario"].ToString();
            string NumInventario = GridPrueba.DataKeys[index].Values["NumInventario"].ToString();
            string IdActividad = GridPrueba.DataKeys[index].Values["IdActividad"].ToString();
            string IdENSU = GridPrueba.DataKeys[index].Values["IdENSU"].ToString();
            string IdUsuario = GridPrueba.DataKeys[index].Values["IdUsuario"].ToString();

            LbIdregmant.Text = IdRegMant;
            LbId.Text = IdInventario;
            LbNumInventario.Text = NumInventario;
            LbActividad.Text = IdActividad;
            LbENSU.Text = IdENSU;
            LbUsuario.Text = IdUsuario;

            bodyContenedorRecibir.Visible = true; 
        }
        protected void GridPrueba_PageIndexChanging(Object sender, GridViewPageEventArgs e)
        {
        }
        protected void BtnCancelar_Click(object sender, EventArgs e)
        {
            LimpiarRegistro2();
            div1.Visible = false;
            bodyContenedorRecibir.Visible = false;
        }
        public void LimpiarRegistro2()
        {
            TxtNumInventario.Text = string.Empty;
            TextBox2.Text = string.Empty;
            LblNoInventario.Text = string.Empty;
            TextBoxNomRecibeBien.Text = string.Empty;
            TextBoxNoOficio.Text = string.Empty;
            TextBoxNomEntregaBien.Text = string.Empty;
            TextBoxSO.Text = string.Empty;
            TextBoxCuenta.Text = string.Empty;
            TextBoxIP.Text = string.Empty;
            TextBoxFallaReportada.Text = string.Empty;
            TextBoxDiag.Text = string.Empty;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void BtnGuardar_Click(object sender, EventArgs e)
        {
            if (HiddenFecha.Value != "") {
                if (Validar())
                {
                    BtnGuardar.Visible = true;
                   
                    CMantenimientoR Mant = new CMantenimientoR();
                    Mant.IdInventario = Convert.ToInt32(LbId.Text);
                    Mant.NombreRecibe = TextBoxNomRecibeBien.Text;
                    Mant.NumOficio = TextBoxNoOficio.Text;
                    Mant.NombreEntrega = TextBoxNomEntregaBien.Text;
                    Mant.SistemaOperativo = TextBoxSO.Text;
                    Mant.CuentaOffice = TextBoxCuenta.Text;
                    Mant.Ip = TextBoxIP.Text;
                    Mant.FallaReportada = TextBoxFallaReportada.Text;
                    Mant.DiagnosticoReparacion = TextBoxDiag.Text;

                    TResultado ResInsertarMant = new TResultado();
                    int IdMantenimiento = CMantenimientoR.InsertarRecibirBien(Mant, ref ResInsertarMant);

                    if (ResInsertarMant.Exito)
                    {
                        TResultado ResActualizarFichaBien = new TResultado();
                        Int64 NumInventario = Convert.ToInt64(TxtNumInventario.Text);
                        BdMantenimiento.ModificarFichaBienRegMant(NumInventario, 3, IdMantenimiento, ref ResActualizarFichaBien);
                        if (ResActualizarFichaBien.Exito)
                        {
                            MostrarMensaje("**Registro exitoso**", "info", "Normal", "Incorrecto");
                        }
                        else
                        {
                            MostrarMensaje(ResActualizarFichaBien.Mensaje.FirstOrDefault(), "error", "Normal", "ErrorResActualizarFicha");
                        }
                    }
                    else
                    {
                        MostrarMensaje(ResInsertarMant.Mensaje.FirstOrDefault(), "error", "Normal", "ErrorResInsertarMant");
                    }

                }

                SqlConnection cnn = new SqlConnection(CConexion.Obtener());
                SqlCommand insert1 = new SqlCommand("INSERT INTO HistoricoUbicacion(IdInventario,IdENSU,IdUsuario,IdActividad,Fecha)values(@idinventario,@idENSU,@idUsuario,@idactividad,@fecha)", cnn);

                insert1.Parameters.AddWithValue("@idinventario", LbId.Text);
                insert1.Parameters.AddWithValue("@idENSU", LbENSU.Text);
                insert1.Parameters.AddWithValue("@idUsuario", LbUsuario.Text);
                insert1.Parameters.AddWithValue("@idactividad", LbActividad.Text);
                insert1.Parameters.AddWithValue("@fecha", HiddenFecha.Value);

                SqlCommand update1 = new SqlCommand("UPDATE Ubicacion set IdActividad=3, Fecha=@fecha where IdInventario = @idinventario and IdActividad = 2", cnn);
                update1.Parameters.AddWithValue("@idinventario", LbId.Text);
                update1.Parameters.AddWithValue("@fecha", HiddenFecha.Value);

                cnn.Open();
                insert1.ExecuteNonQuery();
                update1.ExecuteNonQuery();
                GridPrueba.DataSource = BdRecibirBien.ConsultarGbBienesReparar(TxtNumInventario.Text);
                GridPrueba.DataBind();

                LimpiarRegistro2();
                cnn.Close();

                div1.Visible = true;
                bodyContenedorRecibir.Visible = false;
            }
        }

        protected bool Validar()
        {
            List<string> LErrores = new List<string>();

            if (LbId.Text == "")
                LErrores.Add("Debe ingresar el Id");
            int Id = 0;
            if (!int.TryParse(LbId.Text, out Id))
                LErrores.Add("El valor proporcionado no es vàlido, modifique y vuelva a intentar.");

            if (TextBoxNomRecibeBien.Text == "")
                LErrores.Add("Debe ingresar el Nombre de quien recibe el bien");

            if (TextBoxNoOficio.Text == "")
                LErrores.Add("Debe ingresar el Numero de oficio");

            if (TextBoxNomEntregaBien.Text == "")
                LErrores.Add("Debe ingresar el Nombre entrega bien");

            if (TextBoxSO.Text == "")
                LErrores.Add("Debe ingresar el Sistema operativo");

            if (TextBoxCuenta.Text == "")
                LErrores.Add("Debe ingresar la cuenta de Officce");

            if (TextBoxIP.Text == "")
                LErrores.Add("Debe ingresar la IP");

            if (LErrores.Count == 0)
                return true;
            else
            {
                MostrarMensaje(LErrores.FirstOrDefault(), "error", "Normal", "ErrorValidacion");
                return false;
            }

        }
        protected void time(object sender, EventArgs e)
        {
            //lblHora.Text = DateTime.Now.ToString("hh:mm:ss");
            HiddenFecha.Value = DateTime.Now.ToString("yyyy-MM-ddThh:mm:ss.fff");
        }
    }
}