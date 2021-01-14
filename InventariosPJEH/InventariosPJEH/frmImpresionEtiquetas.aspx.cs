using InventariosPJEH.CAccesoDatos;
using InventariosPJEH.CNegocios;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace InventariosPJEH
{
    public partial class frmImpresionEtiquetas : System.Web.UI.Page { 
         protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                IniciarLlenadoDropDown();
            }
        }

        protected void RbInventario_CheckedChanged(object sender, EventArgs e)
        {
            GrupoInventario.Visible = true;
            GrupoDocumento.Visible = false;
            GrupoResguardo.Visible = false;
            GrupoArea.Visible = false;
            GrupoDistrito.Visible = false;
            GrupoUnidad.Visible = false;
            GrupoBotones.Visible = true;
        }

        protected void RbDocumento_CheckedChanged(object sender, EventArgs e)
        {
            GrupoInventario.Visible = false;
            GrupoDocumento.Visible = true;
            GrupoResguardo.Visible = false;
            GrupoArea.Visible = false;
            GrupoDistrito.Visible = false;
            GrupoUnidad.Visible = false;
            GrupoBotones.Visible = true;
        }

        protected void RbResguardo_CheckedChanged(object sender, EventArgs e)
        {
            GrupoInventario.Visible = false;
            GrupoDocumento.Visible = false;
            GrupoResguardo.Visible = true;
            GrupoArea.Visible = false;
            GrupoDistrito.Visible = false;
            GrupoUnidad.Visible = false;
            GrupoBotones.Visible = true;
        }

        protected void RbArea_CheckedChanged(object sender, EventArgs e)
        {
            GrupoInventario.Visible = false;
            GrupoDocumento.Visible = false;
            GrupoResguardo.Visible = false;
            GrupoArea.Visible = true;
            GrupoDistrito.Visible = true;
            GrupoUnidad.Visible = true;
            GrupoBotones.Visible = true;
        }

        protected void BtnGenerar_Click(object sender, EventArgs e)
        {
            string esFiltro = validarFiltro();
            string mensaje = validardatos(esFiltro);

            if (mensaje == "")
            {//ejecutar la busqueda 
                EtiquetaInventario etiqueta = new EtiquetaInventario();
                etiqueta.filtroBusqueda = esFiltro;

                if (RbDocumento.Checked == true)
                {
                    etiqueta.NumDocumento = TxtDocumento.Text.Trim();
                }
                else if (RbInventario.Checked == true)
                {
                    long numer = Convert.ToInt64(TxtNoInventario.Text.Trim());
                    etiqueta.NumInventario = numer;
                }
                else if (RbResguardo.Checked == true)
                {
                    int numer = Convert.ToInt32(TxtNoResguardo.Text.Trim());
                    etiqueta.NoResguardo = numer;
                }
                else if (RbArea.Checked == true)
                {
                    int numer = Convert.ToInt32(DdlUnidad.SelectedValue);
                    etiqueta.IdUnidad = numer;
                }

                List<EtiquetaInventario> listaResultado = new List<EtiquetaInventario>();
                listaResultado = EtiquetaInventario.etiquetaInventarios(etiqueta);

                Page.Session["etiqueta"] = listaResultado;

                string _open = "window.open('MostrarEtiqueta.aspx', '_blank');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), _open, true);
            }
            else
            {//mandar mensaje de rellenar campo 

            }

        }



        protected void ClasificacionSeleccionado(object sender, EventArgs e)
        {

            if (DdlTipoArea.SelectedItem.Text == "PRIMERA INSTANCIA")
            {
                DdlUnidad.SelectedIndex = 0;
                DdlDistrito.Visible = true;
                LblDistrito.Visible = true;
                DataTable Datoss = new DataTable();
                Datoss = BdCat_Personal.ObtenerDistritos();
                DdlDistrito.DataSource = Datoss;
                DdlDistrito.DataTextField = "Distrito";
                DdlDistrito.DataValueField = "IdDistrito";
                DdlDistrito.DataBind();
                DdlDistrito.Items.Insert(0, new ListItem("Seleccionar", "0"));
            }
            else
            {
                int IdClasificacion = Convert.ToInt32(DdlTipoArea.SelectedValue);
                DataTable Datos = new DataTable();
                Datos = BdCat_Personal.ObtenerClasificacion(IdClasificacion);
                DdlUnidad.DataSource = Datos;
                DdlUnidad.DataTextField = "UniAdmin";
                DdlUnidad.DataValueField = "idUniAdmin";
                DdlUnidad.DataBind();
                DdlUnidad.Items.Insert(0, new ListItem("Seleccionar", "0"));
                DdlDistrito.Visible = false;
                LblDistrito.Visible = false;
            }

        }
        private void IniciarLlenadoDropDown()
        {

            DataTable Datos = new DataTable();
            Datos = BdCat_Personal.ClasificaciónUA();
            DdlTipoArea.DataSource = Datos;

            DdlTipoArea.DataTextField = "Descripcion";
            DdlTipoArea.DataValueField = "IdAbreviatura";

            DdlTipoArea.DataBind();
            DdlTipoArea.Items.Insert(0, new ListItem("Seleccionar", "0"));
            DdlUnidad.Items.Insert(0, new ListItem("Seleccionar", "0"));
            DdlDistrito.Items.Insert(0, new ListItem("Seleccionar", "0"));
        }

        /// <summary>
        ///  Llenar el Tipo de Área - Sección Nuevo/Moficar
        /// </summary>
        string validarFiltro()
        {
            string esSelecccionado = "Ninguno";
            if (RbDocumento.Checked == true)
            {
                esSelecccionado = "documento";
            }
            else if (RbInventario.Checked == true)
            {
                esSelecccionado = "inventario";
            }
            else if (RbResguardo.Checked == true)
            {
                esSelecccionado = "resguardo";
            }
            else if (RbArea.Checked == true)
            {
                esSelecccionado = "area";
            }

            return esSelecccionado;

        }

        string validardatos(string filtro)
        {
            string mensaje = "";
            if (filtro == "documento")
            {
                if (string.IsNullOrWhiteSpace(TxtDocumento.Text))
                {
                    mensaje = "Ingrese No.Documento";
                }
            }
            else if (filtro == "inventario")
            {
                if (string.IsNullOrEmpty(TxtNoInventario.Text))
                {
                    mensaje = "Ingrese No.inventario";
                }
            }
            else if (filtro == "resguardo")
            {
                if (string.IsNullOrWhiteSpace(TxtNoResguardo.Text))
                {
                    mensaje = "Ingrese No.resguardo";
                }
            }
            else if (filtro == "area")
            {
                if (DdlUnidad.SelectedValue == "0")
                {
                    mensaje = "Selecciona la unidad";
                }
            }
            else if (filtro == "Ninguno")
            {
                mensaje = "Seleccione un filtro de búsqueda";

            }

            return mensaje;
        }

        protected void DdlDistrito_SelectedIndexChanged(object sender, EventArgs e)
        {
            int IdDistrito = Convert.ToInt32(DdlDistrito.SelectedValue);
            DataTable Datos = new DataTable();
            Datos = BdCat_Personal.ObtenerUniAdminN(IdDistrito);
            DdlUnidad.DataSource = Datos;
            DdlUnidad.DataTextField = "UniAdmin";
            DdlUnidad.DataValueField = "IdUniAdmin";
            DdlUnidad.DataBind();
            DdlUnidad.Items.Insert(0, new ListItem("Seleccionar", "0"));
        }

        protected void BtnLimpiar_Click(object sender, EventArgs e)
        {
            Response.Redirect("frmImpresionEtiquetas.aspx");
        }
    }

}