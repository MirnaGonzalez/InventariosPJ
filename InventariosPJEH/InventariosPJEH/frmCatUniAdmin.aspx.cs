using InventariosPJEH.CVista;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using InventariosPJEH.CAccesoDatos;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Threading.Tasks;
using InventariosPJEH.CNegocios;
using System.Data.Odbc;
using System.Text.RegularExpressions;

namespace InventariosPJEH
{
    public partial class frmCatUniAdmin : System.Web.UI.Page
    {
        private static List<CUnidadAdmin> datosUnis;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.Form["__EVENTTARGET"] == "AccionEliminarr")
            {
                //if(!IdUniAdminTxt.Text.Equals(""))
                GridBuscar_RowDeleting(this, new GridViewDeleteEventArgs(Int32.Parse(IdUniAdminTxt.Text)));
            }
            else if (Request.Form["__EVENTTARGET"] == "AccionVacio")
            {
                Rowindex.Value = string.Empty;

            }
            if (!this.IsPostBack)
            {
                IniciarLlenadoDropTipo();
                IniciarLlenadoUniNuevo();
                IniciarLlenadoSub();
                IniciarLlenadoTipo();
                datosUnis = new List<CUnidadAdmin>();
            }

        }
        //Llenado inicial
        private void IniciarLlenadoDropTipo()
        {
            DataTable Datos = new DataTable();
            Datos = BdUnidadAdmin.ClasificaciónUA();
            ddlTipo.DataSource = Datos;
            ddlTipo.DataTextField = "Descripcion";
            ddlTipo.DataValueField = "IdAbreviatura";
            ddlTipo.DataBind();
            ddlTipo.Items.Insert(0, new ListItem("Seleccionar", "0"));
            ddlDistrito.Items.Insert(0, new ListItem("Seleccionar", "0"));
        }


        /// <summary>
        /// llenado unidad administrativa nuevo
        /// </summary>
        public void IniciarLlenadoUniNuevo()
        {
            DataTable Datos = new DataTable();
            Datos = BdUnidadAdmin.ClasificaciónUA();
            ddlTipoNuevo.DataSource = Datos;
            ddlTipoNuevo.DataTextField = "Descripcion";
            ddlTipoNuevo.DataValueField = "IdAbreviatura";
            ddlTipoNuevo.DataBind();
            ddlTipoNuevo.Items.Insert(0, new ListItem("Seleccionar", "0"));
            ddlDistritoNuevo.Items.Insert(0, new ListItem("Seleccionar", "0"));
            ddlSubFondoN.Items.Insert(0, new ListItem("Seleccionar", "0"));
        }

        public void IniciarLlenadoSub()
        {
            DataTable Datos = new DataTable();
            Datos = BdUnidadAdmin.ObtenerSubFondo();
            ddlSubFondoN.DataSource = Datos;
            ddlSubFondoN.DataTextField = "SubFondo";
            ddlSubFondoN.DataValueField = "IdSubFondo";
            ddlSubFondoN.DataBind();
            ddlDistritoNuevo.Items.Insert(0, new ListItem("Seleccionar", "0"));
            ddlSubFondoN.Items.Insert(0, new ListItem("Seleccionar", "0"));
        }

        public void IniciarLlenadoTipo()
        {
            DataTable Datos = new DataTable();
            Datos = BdUnidadAdmin.ObtenerTipo();
            ddlTipoSub.DataSource = Datos;
            ddlTipoSub.DataTextField = "Tipo";
            ddlTipoSub.DataValueField = "IdTipo";
            ddlTipoSub.DataBind();
            ddlTipoSub.Items.Insert(0, new ListItem("Seleccionar", "0"));
        }


        protected void ddlTipo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlTipo.SelectedItem.Text == "PRIMERA INSTANCIA")
            {
                this.LabelDistrito.Visible = true;
                this.ddlDistrito.Visible = true;
                DataTable Datoos = new DataTable();
                Datoos = BdUnidadAdmin.ObtenerDistritos();
                ddlDistrito.DataSource = Datoos;
                ddlDistrito.DataTextField = "Distrito";
                ddlDistrito.DataValueField = "IdDistrito";
                ddlDistrito.DataBind();
                ddlDistrito.Items.Insert(0, new ListItem("Seleccionar", "0"));

                this.ddlDistritoNuevo.Visible = true;
                this.LblDistritoN.Visible = true;
                ddlDistritoNuevo.DataSource = Datoos;
                ddlDistritoNuevo.DataTextField = "Distrito";
                ddlDistritoNuevo.DataValueField = "IdDistrito";
                ddlDistritoNuevo.DataBind();
                ddlDistritoNuevo.Items.Insert(0, new ListItem("Seleccionar", "0"));

            }
            else
            {
                this.LabelDistrito.Visible = false;
                this.ddlDistrito.Visible = false;
            }
        }


        protected void ddlDistrito_SelectedIndexChanged(object sender, EventArgs e)
        {
            int IdDistrito = Convert.ToInt32(ddlDistrito.SelectedValue);

        }

        protected void BtnBuscar_Click(object sender, EventArgs e)
        {
            BuscarTipo();
        }

        /// <summary>
        ///  Función para buscar Unidad Administrativa
        /// </summary>
        public void BuscarTipo()
        {
            try
            {
                string DescClasific = Convert.ToString(ddlTipo.SelectedItem);
                string Distrito = Convert.ToString(ddlDistrito.SelectedItem);

                DivResultados.Visible = true;

                if (ddlTipo.SelectedIndex != 0 & ddlDistrito.SelectedIndex != 0)
                {
                    datosUnis = BdUnidadAdmin.ConsultarGbUD(DescClasific, Distrito);
                    GridTabla.DataSource = datosUnis;
                    GridTabla.DataBind();
                    LimpiarRegistros();
                }
                else
                {
                    datosUnis = BdUnidadAdmin.ConsultarGbUnidad(DescClasific);
                    GridTabla.DataSource = datosUnis;
                    GridTabla.DataBind();

                    if (GridTabla.Rows.Count == 0)
                    {
                        MostrarMensaje("** No existen datos con la búsqueda solicitada **", "error", "Normal", "Incorrecto");
                        DivResultados.Visible = false;
                        LimpiarRegistros();
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

        protected void btnEditar_Click(object sender, ImageClickEventArgs e)
        {
            DivNuevoReg.Visible = true;
            DivUniAdminPres.Visible = false;
        }

        /// <summary>
        /// Mensajes de error o confirmacion
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
                Msj = "confirm();";
            else
                Msj = "MostrarMensajeInterval('" + Mensaje + "', '" + Tipo + "');";

            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), ClaveMsj, Msj, true);
        }

        public void LimpiarRegistros()
        {
            ddlTipo.SelectedIndex = 0;
            ddlDistrito.SelectedIndex = 0;
        }

        public void LimpiarNuevoR()
        {
            ddlTipoNuevo.SelectedIndex = 0;
            ddlDistritoNuevo.SelectedIndex = 0;
            ddlSubFondoN.SelectedIndex = 0;
            ddlTipoSub.SelectedIndex = 0;
            TxTCorreoN.Text = string.Empty;
            TxTNombreUniN.Text = string.Empty;
            TxtTelefonoNue.Text = string.Empty;
        }

        protected void ddlTipoNuevo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlTipoNuevo.SelectedItem.Text == "PRIMERA INSTANCIA")
            {
                this.ddlDistritoNuevo.Visible = true;
                this.LblDistritoN.Visible = true;
                DataTable Datos = new DataTable();
                Datos = BdUnidadAdmin.ObtenerDistritos();
                ddlDistritoNuevo.DataSource = Datos;
                ddlDistritoNuevo.DataTextField = "Distrito";
                ddlDistritoNuevo.DataValueField = "IdDistrito";
                ddlDistritoNuevo.DataBind();
                ddlDistritoNuevo.Items.Insert(0, new ListItem("Seleccionar", "0"));
            }
            else
            {
                this.ddlDistritoNuevo.Visible = false;
                this.LblDistritoN.Visible = false;
            }
        }

        protected void BtnNuevoR_Click(object sender, EventArgs e)
        {
            IdUniAdminTxt.Text = "-1";
            if (DivNuevoReg.Visible == false)
            {
                LimpiarRegistros();
                DivResultados.Visible = false;
                DivNuevoReg.Visible = true;
                BtnGuardar.Visible = true;
                BtnCancelar.Visible = true;
            }
            else if (DivNuevoReg.Visible == true)
            {
                DivNuevoReg.Visible = false;
                DivResultados.Visible = false;
                BtnGuardar.Visible = false;
                BtnCancelar.Visible = false;
            }
        }

        private Boolean ValidarFormatoCorreo(String email)
        {
            String expresion;
            expresion = "\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*";
            if (Regex.IsMatch(email, expresion))
            {
                if (Regex.Replace(email, expresion, String.Empty).Length == 0)
                {
                    return true;
                }
                else
                {
                    return false;

                }
            }
            else
            {
                return false;
            }
        }
        protected void BtnGuardar_Click(object sender, EventArgs e)
        {

            if (ddlTipoNuevo.SelectedIndex != 0)
            {
                if (!ddlTipoNuevo.SelectedValue.Equals("2") || (ddlTipoNuevo.SelectedValue.Equals("2") && ddlDistritoNuevo.SelectedIndex != 0))
                {
                    if (ddlSubFondoN.SelectedIndex != 0)
                    {
                        if (ddlTipoSub.SelectedIndex != 0)
                        {
                            if (TxTNombreUniN.Text != "")
                            {
                                if (TxtTelefonoNue.Text != "")
                                {
                                    if (TxTCorreoN.Text != "")
                                    {
                                        if (ValidarFormatoCorreo(TxTCorreoN.Text))
                                        {
                                            if (IdUniAdminTxt.Text.Equals("-1"))
                                            {
                                                if (BdUnidadAdmin.InsertarUniAdmin(Convert.ToInt32(ddlTipoNuevo.SelectedValue),
                                                             Convert.ToInt32(ddlDistritoNuevo.SelectedValue),
                                                             Convert.ToInt32(ddlSubFondoN.SelectedValue),
                                                            ddlTipoSub.SelectedValue,
                                                            TxTNombreUniN.Text,
                                                            TxtTelefonoNue.Text,
                                                            TxTCorreoN.Text
                                                             ))
                                                {
                                                    MostrarMensaje("Unidad Administrativa generada de forma correcta", "info", "Normal", "ModificacionCorrecta");
                                                    if (DivResultados.Visible)
                                                    {
                                                        BuscarTipo();
                                                    }
                                                    LimpiarNuevoR();
                                                    DivNuevoReg.Visible = false;
                                                    DivUniAdminPres.Visible = true;
                                                }
                                                else
                                                {
                                                    MostrarMensaje("** Error en Base de Datos **", "error", "Normal", "Incorrecto");
                                                }
                                            }
                                            else
                                            {
                                                if (BdUnidadAdmin.ActualizarUniAdmin(int.Parse(IdUniAdminTxt.Text),
                                                            Convert.ToInt32(ddlTipoNuevo.SelectedValue),
                                                             Convert.ToInt32(ddlDistritoNuevo.SelectedValue),
                                                             Convert.ToInt32(ddlSubFondoN.SelectedValue),
                                                            ddlTipoSub.SelectedValue,
                                                            TxTNombreUniN.Text.Trim(),
                                                            TxtTelefonoNue.Text,
                                                            TxTCorreoN.Text
                                                             ))
                                                {
                                                    MostrarMensaje("Unidad Administrativa actualizada de forma correcta", "info", "Normal", "ModificacionCorrecta");

                                                    BuscarTipo();
                                                    LimpiarNuevoR();
                                                    DivNuevoReg.Visible = false;
                                                    DivUniAdminPres.Visible = true;
                                                }
                                                else
                                                {
                                                    MostrarMensaje("** Error en Base de Datos **", "error", "Normal", "Incorrecto");
                                                }
                                            }
                                        }
                                        else
                                        {
                                            MostrarMensaje("** Correo inválido **", "error", "Normal", "Incorrecto");
                                        }
                                    }
                                    else
                                    {
                                        MostrarMensaje("** Introduzca correo **", "error", "Normal", "Incorrecto");
                                    }
                                }
                                else
                                {
                                    MostrarMensaje("** Introduzca Teléfono **", "error", "Normal", "Incorrecto");
                                }
                            }
                            else
                            {
                                MostrarMensaje("** Introduzca Nombre **", "error", "Normal", "Incorrecto");
                            }
                        }
                        else
                        {
                            MostrarMensaje("** Seleccione Tipo **", "error", "Normal", "Incorrecto");
                        }
                    }
                    else
                    {
                        MostrarMensaje("** Seleccione Subfondo **", "error", "Normal", "Incorrecto");
                    }
                }
                else
                {
                    MostrarMensaje("** Seleccione Distrito **", "error", "Normal", "Incorrecto");
                }
            }
            else
            {
                MostrarMensaje("** Seleccione Clasificación **", "error", "Normal", "Incorrecto");
            }
        }

        protected void GridBuscar_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            if (String.IsNullOrEmpty(Rowindex.Value))
            {
                Rowindex.Value = e.RowIndex.ToString();
                MostrarMensaje("Prueba", "info", "NotificacionEliminar", "Inicio");
            }
            else
            {
                int n = e.RowIndex;
                BdUnidadAdmin.EliminarUnidad(e.RowIndex);
                BuscarTipo();
                MostrarMensaje("** Unidad Administrativa eliminada **", "error", "Normal", "Incorrecto");
                Rowindex.Value = "";
            }

        }

        protected void btnEditar_Click(object sender, EventArgs e)
        {
            try
            {
                DivNuevoReg.Visible = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void GridModificar_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            GridViewRow RowSelecionada = GridTabla.Rows[index];
            CUnidadAdmin unidad = datosUnis.Find(x => x.UniAdmin == Page.Server.HtmlDecode(RowSelecionada.Cells[0].Text));
            if (unidad != null)
            {
                if (unidad.IdDistrito != -1)
                {
                    LblDistritoN.Visible = true;
                    ddlDistritoNuevo.Visible = true;
                    ddlDistritoNuevo.SelectedValue = unidad.IdDistrito.ToString();
                }
                else
                {
                    LblDistritoN.Visible = false;
                    ddlDistritoNuevo.Visible = false;
                }
                IdUniAdminTxt.Text = unidad.IdUniAdmin.ToString();
                ddlTipoNuevo.SelectedValue = unidad.IdClasificacion.ToString();
                ddlSubFondoN.SelectedValue = unidad.IdSubFondo.ToString();
                ddlTipoSub.SelectedValue = unidad.Tipo;
                TxTNombreUniN.Text = unidad.UniAdmin;
                TxtTelefonoNue.Text = unidad.Telefono;
                TxTCorreoN.Text = unidad.Email;

            }
        }

        protected void BtnCancelar_Click(object sender, EventArgs e)
        {
            LimpiarNuevoR();
            DivNuevoReg.Visible = false;
            DivUniAdminPres.Visible = true;
        }



    }



}