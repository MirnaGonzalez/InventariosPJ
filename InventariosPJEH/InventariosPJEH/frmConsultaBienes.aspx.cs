using InventariosPJEH.CNegocios;
using InventariosPJEH.CAccesoDatos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;
using Excel = Microsoft.Office.Interop.Excel;
using System.IO;
using System.Web;
using Microsoft.Office.Interop.Excel;
using DataTable = System.Data.DataTable;
using System.Drawing;
using System.Web.UI.HtmlControls;
using System.Text;
using Page = System.Web.UI.Page;

namespace InventariosPJEH
{
    public partial class frmConsultaBienes : System.Web.UI.Page
    {
        public SqlConnection conexion;

        public frmConsultaBienes()
        {
            this.conexion = ConexionBD.getConexion();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                IniciarLlenadoDropDownListPartida();
                IniciarLlenadoDropDownListSubClase();
                IniciarLlenadoDropDownListMarca();
                IniciarLlenadoDropDownListPropiedad();
                IniciarLlenadoDropDownListProveedor();
                IniciarLlenadoDropDown();
                IniciarLlenadoDropEstatus();
                IniciarLlenadoDropAreaResguardo();
            }
        }

        private void IniciarLlenadoDropDown()
        {
            DataTable Datos = new DataTable();
            Datos = BdConsultaBienes.Ini1();
            DropClasificacion.DataSource = Datos;
            DropClasificacion.DataTextField = "Descripcion";
            DropClasificacion.DataValueField = "IdAbreviatura";
            DropClasificacion.DataBind();
            DropClasificacion.Items.Insert(0, new ListItem("Seleccionar", "0"));
            DropUniAdmin.Items.Insert(0, new ListItem("Seleccionar", "0"));
            DropDistrito.Items.Insert(0, new ListItem("Seleccionar", "0"));
            DropPersona.Items.Insert(0, new ListItem("Seleccionar", "0"));
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

        /// <summary>
        /// Método para buscar un Bien dependiendo los filtro de búsqueda. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void BtnBuscarBien_Click(object sender, EventArgs e)
        {
            //Capturamos los errores con try catch
            try
            {
                DivFiltrosDeBusqueda.Visible = false;
                divGrid.Visible = true;
                DataTable Datos = new DataTable();
                Datos = BdConsultaBienes.BuscarGrid(DropDownListPropiedad, DropDownListPartida,
                DropDownListSubClase, TextBoxNoDocumento, DropDownListProveedor, TxtFechaAdquisicion,
                TxtNoInventario, DropDownListMarca, TextBoxModelo, TextBoxSerie, DropPersona, TxtNoResguardo, DropClasificacion,
                DropUniAdmin, DropEstatus, DropAreaMantenimiento);
                GridModificar.DataSource = Datos;
                GridModificar.DataBind();
                Botones.Visible = true;

                if (GridModificar.Rows.Count == 0)
                {
                    MostrarMensaje("** No existen Bienes con la búsqueda solicitada **", "info", "Normal");
                    DivFiltrosDeBusqueda.Visible = true;
                    divGrid.Visible = false;
                    Botones.Visible = false;
                }
                ////////////////////////////////////////////
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        /// <summary>
        /// Método para llenar el drop de área de resguardo
        /// </summary>
        private void IniciarLlenadoDropAreaResguardo()
        {
            DropAreaMantenimiento.Items.Insert(0, new ListItem("Seleccionar", "0"));
            DropAreaMantenimiento.Items.Insert(1, new ListItem("Soporte Técnico", "1"));
            DropAreaMantenimiento.Items.Insert(2, new ListItem("Servicios Generales", "2"));
            DropAreaMantenimiento.Items.Insert(3, new ListItem("Servicio de Autos", "3"));
        }

        /// <summary>
        /// Método para llenar el drop de proveedores 
        /// </summary>
        private void IniciarLlenadoDropDownListProveedor()
        {
            List<CConsultaBienes> listaProveedor = new List<CConsultaBienes>();
            listaProveedor = BdConsultaBienes.ObtenerProveedor();
            DropDownListProveedor.DataSource = listaProveedor;
            DropDownListProveedor.DataTextField = "Proveedor";
            DropDownListProveedor.DataValueField = "IdProveedor";
            DropDownListProveedor.DataBind();
            DropDownListProveedor.Items.Insert(0, new ListItem("Seleccionar", "0"));
        }

        /// <summary>
        /// Método para llenar el drop de estatus 
        /// </summary>
        private void IniciarLlenadoDropEstatus()
        {
            List<CConsultaBienes> listaEstatus = new List<CConsultaBienes>();
            listaEstatus = BdConsultaBienes.Estatus();
            DropEstatus.DataSource = listaEstatus;
            DropEstatus.DataTextField = "Actividad";
            DropEstatus.DataValueField = "IdActividad";
            DropEstatus.DataBind();
            DropEstatus.Items.Insert(0, new ListItem("Seleccionar", "0"));
        }

        /// <summary>
        /// Método para llenar el drop de propiedades 
        /// </summary>
        private void IniciarLlenadoDropDownListPropiedad()
        {
            List<CConsultaBienes> listaPropiedad = new List<CConsultaBienes>();
            listaPropiedad = BdConsultaBienes.ObtenerPropiedad();
            DropDownListPropiedad.DataSource = listaPropiedad;
            DropDownListPropiedad.DataTextField = "Propiedad";
            DropDownListPropiedad.DataValueField = "IdPropiedad";
            DropDownListPropiedad.DataBind();
            DropDownListPropiedad.Items.Insert(0, new ListItem("Seleccionar", "0"));
        }

        /// <summary>
        /// Método para llenar el drop de partidas 
        /// </summary>
        private void IniciarLlenadoDropDownListPartida()
        {
            List<CConsultaBienes> listaPartida = new List<CConsultaBienes>();
            listaPartida = BdConsultaBienes.ObtenerPartida();
            DropDownListPartida.DataSource = listaPartida;
            DropDownListPartida.DataTextField = "Partida";
            DropDownListPartida.DataValueField = "idPartida";
            DropDownListPartida.DataBind();
            DropDownListPartida.Items.Insert(0, new ListItem("Seleccionar", "0"));
        }

        /// <summary>
        /// Método para llenar el drop de subclases  
        /// </summary>
        private void IniciarLlenadoDropDownListSubClase()
        {
            List<CConsultaBienes> listaSubClase = new List<CConsultaBienes>();
            listaSubClase = BdConsultaBienes.ObtenerSubClase();
            DropDownListSubClase.DataSource = listaSubClase;
            DropDownListSubClase.DataTextField = "SubClase";
            DropDownListSubClase.DataValueField = "idSubClase";
            DropDownListSubClase.DataBind();
            DropDownListSubClase.Items.Insert(0, new ListItem("Seleccionar", "0"));
        }

        /// <summary>
        /// Método para llenar el drop de marcas 
        /// </summary>
        private void IniciarLlenadoDropDownListMarca()
        {
            List<CConsultaBienes> listaMarca = new List<CConsultaBienes>();
            listaMarca = BdConsultaBienes.ObtenerMarca();
            DropDownListMarca.DataSource = listaMarca;
            DropDownListMarca.DataTextField = "Descripcion";
            DropDownListMarca.DataValueField = "IdMarca";
            DropDownListMarca.DataBind();
            DropDownListMarca.Items.Insert(0, new ListItem("Seleccionar", "0"));
        }

        /// <summary>
        /// Método para limpiar todos los campos de la búsqueda.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void BtnLimpiarBusqueda_Click(object sender, EventArgs e)
        {
            DivFiltrosDeBusqueda.Visible = true;
            divGrid.Visible = false;
            Botones.Visible = false;
            TextBoxNoDocumento.Text = "";
            TxtFechaAdquisicion.Text = "";
            TxtNoInventario.Text = "";
            TextBoxModelo.Text = "";
            TextBoxSerie.Text = "";
            IniciarLlenadoDropDownListPartida();
            IniciarLlenadoDropDownListSubClase();
            IniciarLlenadoDropDownListMarca();
            IniciarLlenadoDropDownListPropiedad();
            IniciarLlenadoDropDownListProveedor();
            DropDownListPropiedad.SelectedIndex = 0;
            DropDownListPartida.SelectedIndex = 0;
            DropDownListSubClase.SelectedIndex = 0;
            DropDownListProveedor.SelectedIndex = 0;
            DropDownListMarca.SelectedIndex = 0;
            DropClasificacion.SelectedIndex = 0;
            DropDistrito.SelectedIndex = 0;
            DropDistrito.Visible = false;
            LabelDistrito.Visible = false;
            LblPersona.Visible = false;
            DropUniAdmin.SelectedIndex = 0;
            DropPersona.SelectedIndex = 0;
            DropPersona.Visible = false;
            TxtNoResguardo.Text = "";
            DropAreaMantenimiento.SelectedIndex = 0;
            DropEstatus.SelectedIndex = 0;
        }

        /// <summary>
        /// Método para cancelar la búsqueda.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Btncancelar_Click(object sender, EventArgs e)
        {
            DivFiltrosDeBusqueda.Visible = true;
            divGrid.Visible = false;
            Botones.Visible = false;
        }

        /// <summary>
        /// Método que manada a llamar el método para exportar a excel. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void BtnExportar_Click(object sender, EventArgs e)
        {
            try
            {
                ExpExcelDataTable();
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Método para la paginación del grid. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void GridModificar_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridModificar.PageIndex = e.NewPageIndex;
            DataTable Datos = new DataTable();
            Datos = BdConsultaBienes.BuscarGrid(DropDownListPropiedad, DropDownListPartida,
                DropDownListSubClase, TextBoxNoDocumento, DropDownListProveedor, TxtFechaAdquisicion,
                TxtNoInventario, DropDownListMarca, TextBoxModelo, TextBoxSerie, DropPersona, TxtNoResguardo, DropClasificacion,
                DropUniAdmin, DropEstatus, DropAreaMantenimiento);
            GridModificar.DataSource = Datos;
            GridModificar.DataBind();
        }

        /// <summary>
        /// Método para exportar la tabla de resultados a excel. 
        /// </summary>
        protected void ExpExcelDataTable()
        {
            DataTable Datos = new DataTable();
            Datos = BdConsultaBienes.BuscarGrid(DropDownListPropiedad, DropDownListPartida,
                DropDownListSubClase, TextBoxNoDocumento, DropDownListProveedor, TxtFechaAdquisicion,
                TxtNoInventario, DropDownListMarca, TextBoxModelo, TextBoxSerie, DropPersona, TxtNoResguardo, DropClasificacion,
                DropUniAdmin, DropEstatus, DropAreaMantenimiento);
            GridPrueba.DataSource = Datos;
            GridPrueba.DataBind();

            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition",
             "attachment;filename=ConsultaBienes.xls");
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);

            for (int i = 0; i < GridPrueba.Rows.Count; i++)
            {
                //Aplicar el estilo de texto 
                GridPrueba.Rows[i].Attributes.Add("class", "textmode");
            }

            GridPrueba.RenderControl(hw);

            //Estilo con el formato de número
            string style = @"<style> .textmode { mso-number-format:\@; } </style>";
            Response.Write(style);
            Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.End();
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Confirms that an HtmlForm control is rendered for the specified ASP.NET
               server control at run time. */
        }

        /// <summary>
        /// Método para llenar los drop anidados. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ClasificacionSeleccionado(object sender, EventArgs e)
        {
            if (DropClasificacion.SelectedItem.Text == "PRIMERA INSTANCIA")
            {
                DropUniAdmin.SelectedIndex = 0;
                DropPersona.SelectedIndex = 0;
                this.DropDistrito.Visible = true;
                this.LabelDistrito.Visible = true;
                this.DropPersona.Visible = false;
                this.LblPersona.Visible = false;
                DataTable Datoss = new DataTable();
                Datoss = BdConsultaBienes.ObtenerDistritos();
                DropDistrito.DataSource = Datoss;
                DropDistrito.DataTextField = "Distrito";
                DropDistrito.DataValueField = "IdDistrito";
                DropDistrito.DataBind();
                DropDistrito.Items.Insert(0, new ListItem("Seleccionar", "0"));
            }
            else
            {
                int IdClasificacion = Convert.ToInt32(DropClasificacion.SelectedValue);
                DataTable Datos = new DataTable();
                Datos = BdConsultaBienes.ObtenerClasificacion(IdClasificacion);
                DropUniAdmin.DataSource = Datos;
                DropUniAdmin.DataTextField = "UniAdmin";
                DropUniAdmin.DataValueField = "idUniAdmin";
                DropUniAdmin.DataBind();
                DropUniAdmin.Items.Insert(0, new ListItem("Seleccionar", "0"));
                this.DropDistrito.Visible = false;
                this.LabelDistrito.Visible = false;
            }
        }

        /// <summary>
        /// Método para el evento change del drop Distrito. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void DropDistrito_SelectedIndexChanged(object sender, EventArgs e)
        {
            int IdDistrito = Convert.ToInt32(DropDistrito.SelectedValue);
            DataTable Datos = new DataTable();
            Datos = BdConsultaBienes.ObtenerUniAdminN(IdDistrito);
            DropUniAdmin.DataSource = Datos;
            DropUniAdmin.DataTextField = "UniAdmin";
            DropUniAdmin.DataValueField = "IdUniAdmin";
            DropUniAdmin.DataBind();
            DropUniAdmin.Items.Insert(0, new ListItem("Seleccionar", "0"));
        }

        /// <summary>
        /// Método para el evento change del drop UniAdmin. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void DropUniAdmin_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.DropPersona.Visible = true;
            this.LblPersona.Visible = true;
            int idAreaResguardo = Convert.ToInt32(DropUniAdmin.SelectedValue);
            DataTable Datos = new DataTable();
            Datos = BdConsultaBienes.ObtenerPersona(idAreaResguardo);
            DropPersona.DataSource = Datos;
            DropPersona.DataTextField = "Nombre";
            DropPersona.DataValueField = "IdEmpleado";
            DropPersona.DataBind();
            DropPersona.Items.Insert(0, new ListItem("Seleccionar", "0"));
        }

        /// <summary>
        /// Método para limpiar los campos.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void BtnLimpiar_Click(object sender, EventArgs e)
        {
            DropDownListPropiedad.SelectedIndex = 0;
            DropDownListPartida.SelectedIndex = 0;
            DropDownListSubClase.SelectedIndex = 0;
            TextBoxNoDocumento.Text = "";
            DropDownListProveedor.SelectedIndex = 0;
            TxtFechaAdquisicion.Text = "";
            TxtNoInventario.Text = "";
            DropDownListMarca.SelectedIndex = 0;
            TextBoxModelo.Text = "";
            TextBoxSerie.Text = "";
            DropClasificacion.SelectedIndex = 0;
            DropDistrito.SelectedIndex = 0;
            DropDistrito.Visible = false;
            DropUniAdmin.SelectedIndex = 0;
            DropPersona.SelectedIndex = 0;
            DropPersona.Visible = false;
            TxtNoResguardo.Text = "";
            DropAreaMantenimiento.SelectedIndex = 0;
            DropEstatus.SelectedIndex = 0;
            LabelDistrito.Visible = false;
            LblPersona.Visible = false;
        }
    }
}

