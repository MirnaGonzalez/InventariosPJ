//Directivas using para la importacion de librerias. 
using System.Globalization;
using InventariosPJEH.CNegocios;
using InventariosPJEH.CAccesoDatos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Linq;

namespace InventariosPJEH
{
    public partial class frmRegistroBien : System.Web.UI.Page
    {
        public SqlConnection conexion;

        public frmRegistroBien()
        {
            this.conexion = ConexionBD.getConexion();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            BdRegistroBien cRegistroDeUnBien = new BdRegistroBien();
            if (!Page.IsPostBack)
            {
                //Llenado de Listas Desoplegables 
                LLenarGrid();
                IniciarLlenadoDropDownTipoAdquisicion();
                IniciarLlenadoDropDownTipoDocumento();
                IniciarLlenadoDropDownListProveedor();
                IniciarLlenadoDropDownListAreaDeResguardoInicial();
                IniciarLlenadoDropDownListPropiedad();
                IniciarLlenadoDropDownListPartida();
                IniciarLlenadoDropDownListSubClase();
                IniciarLlenadoDropDownListCatCONAC();
                IniciarLlenadoDropDownListMarca();


            }
        }

        /// <summary>
        /// Este método es utilizado para llenar el grid de forma manual, para que al iniciar 
        /// la página aparezca en blanco y posteriormente se llene con los datos al consultar a la BD. 
        /// </summary>
        public void LLenarGrid()
        {
            DataTable dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[3] { new DataColumn("id", typeof(int)),
                        new DataColumn("Detalle", typeof(string)),
                        new DataColumn("Importe",typeof(double)) });
            dt.Rows.Add(1, "Bienes Informaticos", 0);
            dt.Rows.Add(2, "Bienes Muebles", 0);
            gridFactura.DataSource = dt;
            gridFactura.DataBind();

            LblSubTo.Text = "0";
            LblIva1.Text = "0";
            LblTotal1.Text = "0";
        }

        /// <summary>
        /// Este método es utilizado para mostrar una alerta al usuario al realizar alguna función 
        /// de forma exitosa o para informarle sobre algún error. 
        /// </summary>
        /// <param name="Mensaje"></param> 
        /// <param name="Tipo"></param>
        /// <param name="TipoFuncion"></param>
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
        /// Método utilizado para llenar el Drop de TipoAdquisicion mediante una consulta a la BD.
        /// </summary>
        protected void IniciarLlenadoDropDownTipoAdquisicion()
        {
            List<CRegistroBien> listaAdqui = new List<CRegistroBien>();
            listaAdqui = BdRegistroBien.ObtenerTipoAdquisicion();
            ddlTipoAdquisicion.DataSource = listaAdqui;
            ddlTipoAdquisicion.DataTextField = "tipoAdqui";
            ddlTipoAdquisicion.DataValueField = "IdTipoAdqui";
            ddlTipoAdquisicion.DataBind();
            ddlTipoAdquisicion.Items.Insert(0, new ListItem("Seleccionar", "0"));
        }

        /// <summary>
        /// Método utilizado para llenar el Drop de TipoDocumento mediante una consulta a la BD.
        /// </summary>
        private void IniciarLlenadoDropDownTipoDocumento()
        {
            List<CRegistroBien> lista = new List<CRegistroBien>();
            lista = BdRegistroBien.ObtenerTipoDocumento();
            ddlTipoDocumento.DataSource = lista;
            ddlTipoDocumento.DataTextField = "Documento";
            ddlTipoDocumento.DataValueField = "IdTipoDoc";
            ddlTipoDocumento.DataBind();
            ddlTipoDocumento.Items.Insert(0, new ListItem("Seleccionar", "0"));
        }

        /// <summary>
        /// Método utilizado para llenar el Drop de Proveedores mediante una consulta a la BD.
        /// </summary>
        private void IniciarLlenadoDropDownListProveedor()
        {
            List<CRegistroBien> listaProveedor = new List<CRegistroBien>();
            listaProveedor = BdRegistroBien.ObtenerProveedor();
            ddlProveedor.DataSource = listaProveedor;
            ddlProveedor.DataTextField = "Proveedor";
            ddlProveedor.DataValueField = "IdProveedor";
            ddlProveedor.DataBind();
            ddlProveedor.Items.Insert(0, new ListItem("Seleccionar", "0"));
        }

        /// <summary>
        /// Método utilizado para llenar el Drop de AreaDeResguardo mediante una consulta a la BD.
        /// </summary>
        private void IniciarLlenadoDropDownListAreaDeResguardoInicial()
        {
            List<CRegistroBien> listaAreaResguardo = new List<CRegistroBien>();
            listaAreaResguardo = BdRegistroBien.ObtenerAreaDeResguardoInicial();
            ddlResguardoInicial.DataSource = listaAreaResguardo;
            ddlResguardoInicial.DataTextField = "Nombre";
            ddlResguardoInicial.DataValueField = "IdSeccion";
            ddlResguardoInicial.DataBind();
            ddlResguardoInicial.Items.Insert(0, new ListItem("Seleccionar", "0"));
        }

        /// <summary>
        /// Método utilizado para llenar el Drop de Propiedades mediante una consulta a la BD.
        /// </summary>
        private void IniciarLlenadoDropDownListPropiedad()
        {
            List<CRegistroBien> listaPropiedad = new List<CRegistroBien>();
            listaPropiedad = BdRegistroBien.ObtenerPropiedad();
            ddlPropiedad.DataSource = listaPropiedad;
            ddlPropiedad.DataTextField = "Propiedad";
            ddlPropiedad.DataValueField = "IdPropiedad";
            ddlPropiedad.DataBind();
            ddlPropiedad.Items.Insert(0, new ListItem("Seleccionar", "0"));
        }

        /// <summary>
        /// Método utilizado para llenar el Drop de Partidas mediante una consulta a la BD.
        /// </summary>
        private void IniciarLlenadoDropDownListPartida()
        {
            CUsuario Usuario = Page.Session["Usuario"] as CUsuario;
            string TipoPartida = Usuario.TipoPartida;

            List<CRegistroBien> listaPartida = new List<CRegistroBien>();
            listaPartida = BdRegistroBien.ObtenerPartida(TipoPartida);
            ddlPartida.DataSource = listaPartida;
            ddlPartida.DataTextField = "Partida";
            ddlPartida.DataValueField = "idPartida";
            ddlPartida.DataBind();
            ddlPartida.Items.Insert(0, new ListItem("Seleccionar", "0"));
        }

        /// <summary>
        /// Método utilizado para llenar el Drop de SubClases mediante una consulta a la BD.
        /// </summary>
        private void IniciarLlenadoDropDownListSubClase()
        {
            CUsuario Usuario = Page.Session["Usuario"] as CUsuario;
            string TipoPartida = Usuario.TipoPartida;

            List<CRegistroBien> listaSubClase = new List<CRegistroBien>();
            listaSubClase = BdRegistroBien.ObtenerSubClase(TipoPartida);
            ddlSubClase.DataSource = listaSubClase;
            ddlSubClase.DataTextField = "SubClase";
            ddlSubClase.DataValueField = "idSubClase";
            ddlSubClase.DataBind();
            ddlSubClase.Items.Insert(0, new ListItem("Seleccionar", "0"));
        }

        /// <summary>
        /// Método utilizado para llenar el Drop de CONAC mediante una consulta a la BD.
        /// </summary>
        private void IniciarLlenadoDropDownListCatCONAC()
        {
            CUsuario Usuario = Page.Session["Usuario"] as CUsuario;
            string TipoPartida = Usuario.TipoPartida;

            List<CRegistroBien> listaCONAC = new List<CRegistroBien>();
            listaCONAC = BdRegistroBien.ObtenerCONAC(TipoPartida);
            ddlCatCONAC.DataSource = listaCONAC;
            ddlCatCONAC.DataTextField = "Descripcion";
            ddlCatCONAC.DataValueField = "IdCONAC";
            ddlCatCONAC.DataBind();
            ddlCatCONAC.Items.Insert(0, new ListItem("Seleccionar", "0"));
        }

        /// <summary>
        /// Método utilizado para llenar el Drop de Marcas mediante una consulta a la BD.
        /// </summary>
        private void IniciarLlenadoDropDownListMarca()
        {
         
            List<CRegistroBien> listaMarca = new List<CRegistroBien>();
            listaMarca = BdRegistroBien.ObtenerMarca();
            ddlMarca.DataSource = listaMarca;
            ddlMarca.DataTextField = "Descripcion";
            ddlMarca.DataValueField = "IdMarca";
            ddlMarca.DataBind();
            ddlMarca.Items.Insert(0, new ListItem("Seleccionar", "0"));
        }

        /// <summary>
        /// Método utilizado para limpiar los campos del formulario. 
        /// </summary>
        public void LimpiarTodo()
        {
            TextBoxNoDocumento.Text = string.Empty;
            txtAjustarCosto.Text = "";
            txtFechaAdquisicion.Text = "";
            txtPUnitario.Text = string.Empty;
            checkboxIVA.Checked = false;
            labelIVA.Text = string.Empty;
            txtDescuento.Text = string.Empty;
            LabelSubTotal1.Text = string.Empty;
            labelCostototal.Text = string.Empty;
            checkboxDescuento.Checked = false;
            ddlResguardoInicial.SelectedIndex = 0;
            TextBoxCantidadDeBienes.Text = string.Empty;
            txtNombreBien.Text = string.Empty;
            ddlPartida.SelectedIndex = 0;
            ddlSubClase.SelectedIndex = 0;
            ddlCatCONAC.SelectedIndex = 0;
            LabelGrupo.Text = string.Empty;
            LabelSubGrupo.Text = string.Empty;
            LabelClase.Text = string.Empty;
            ddlMarca.SelectedIndex = 0;
            txtModelo.Text = string.Empty;
            txtSerie.Text = string.Empty;
            TextBoxFrente.Text = string.Empty;
            TextBoxAltura.Text = string.Empty;
            TextBoxFondo.Text = string.Empty;
            TextBoxDiametro.Text = string.Empty;
            txtVidaUtil.Text = string.Empty;
            txtDetalleAdquisicion.Text = "";
            LblConcatena.Text = "";
            rbtnRegistroIndividual.Checked = false;
            rbtnRegistroLote.Checked = false;
            txtDetalle.Text = "";
            cbxAjustarCosto.Checked = false;
            IniciarLlenadoDropDownListProveedor();
            IniciarLlenadoDropDownTipoDocumento();
            IniciarLlenadoDropDownTipoAdquisicion();
            IniciarLlenadoDropDownListPropiedad();
        }

        /// <summary>
        /// Método para limpiar los campos del registro de un Bien 
        /// </summary>
        public void LimpiarRegistro()
        {
            ddlTipoAdquisicion.SelectedIndex = 0;
            ddlTipoDocumento.SelectedIndex = 0;
            TextBoxNoDocumento.Text = string.Empty;
            txtFechaAdquisicion.Text = "";
            txtPUnitario.Text = string.Empty;
            checkboxIVA.Checked = false;
            labelIVA.Text = string.Empty;
            txtDescuento.Text = string.Empty;
            LabelSubTotal1.Text = string.Empty;
            labelCostototal.Text = string.Empty;
            checkboxDescuento.Checked = false;
            ddlProveedor.SelectedIndex = 0;
            ddlResguardoInicial.SelectedIndex = 0;
            ddlPropiedad.SelectedIndex = 0;
            TextBoxCantidadDeBienes.Text = string.Empty;
            txtNombreBien.Text = string.Empty;
            ddlPartida.SelectedIndex = 0;
            ddlSubClase.SelectedIndex = 0;
            ddlCatCONAC.SelectedIndex = 0;
            LabelGrupo.Text = string.Empty;
            LblGrupo1.Visible = false;
            LblSubGrupo1.Visible = false;
            LblClase.Visible = false;
            LabelSubGrupo.Text = string.Empty;
            LabelClase.Text = string.Empty;
            ddlMarca.SelectedIndex = 0;
            txtModelo.Text = string.Empty;
            txtSerie.Text = string.Empty;
            TextBoxFrente.Text = string.Empty;
            TextBoxAltura.Text = string.Empty;
            TextBoxFondo.Text = string.Empty;
            TextBoxDiametro.Text = string.Empty;
            txtVidaUtil.Text = string.Empty;
            txtDetalle.Text = "";
            txtAjustarCosto.Text = "";
            txtAjustarCosto.Visible = false;
            txtDescuento.Visible = false;
            cbxAjustarCosto.Checked = false;
            div9.Visible = false;
            div8.Visible = false;
        }

        /// <summary>
        /// Método del Botón Limpiar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            ddlTipoAdquisicion.SelectedIndex = 0;
            ddlTipoDocumento.SelectedIndex = 0;
            TextBoxNoDocumento.Text = string.Empty;
            txtAjustarCosto.Text = "";
            txtFechaAdquisicion.Text = "";
            txtPUnitario.Text = string.Empty;
            checkboxIVA.Checked = false;
            labelIVA.Text = string.Empty;
            txtDescuento.Text = string.Empty;
            LabelSubTotal1.Text = string.Empty;
            labelCostototal.Text = string.Empty;
            checkboxDescuento.Checked = false;
            ddlProveedor.SelectedIndex = 0;
            ddlResguardoInicial.SelectedIndex = 0;
            ddlPropiedad.SelectedIndex = 0;
            TextBoxCantidadDeBienes.Text = string.Empty;
            txtNombreBien.Text = string.Empty;
            ddlPartida.SelectedIndex = 0;
            ddlSubClase.SelectedIndex = 0;
            ddlCatCONAC.SelectedIndex = 0;
            LabelGrupo.Text = string.Empty;
            LblGrupo1.Visible = false;
            LblSubGrupo1.Visible = false;
            LblClase.Visible = false;
            LabelSubGrupo.Text = string.Empty;
            LabelClase.Text = string.Empty;
            ddlMarca.SelectedIndex = 0;
            txtModelo.Text = string.Empty;
            txtSerie.Text = string.Empty;
            TextBoxFrente.Text = string.Empty;
            TextBoxAltura.Text = string.Empty;
            TextBoxFondo.Text = string.Empty;
            TextBoxDiametro.Text = string.Empty;
            txtVidaUtil.Text = string.Empty;
            txtDetalle.Text = "";
            txtAjustarCosto.Visible = false;
            txtDescuento.Visible = false;
            cbxAjustarCosto.Checked = false;
        }

        /// <summary>
        /// Método del Botón Cancelar, utilizado para cancelar el registro del bien. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Cancel_Click(object sender, EventArgs e)
        {
            btnGuardarFactura.Visible = true;
            LimpiarTodo();
            Bien.Visible = false;
            divConcatena.Visible = false;
            InfoGeneral.Visible = false;
            MontoOriginalDeLaInversion.Visible = false;
            Factura.Visible = true;
            Botones.Visible = false;
            LLenarGrid();
        }

        /// <summary>
        /// Método para validar si el check de registro por lote esta seleccionado o no. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void RdRegistroLote_CheckedChanged(object sender, EventArgs e)
        {
            divConcatena.Visible = true;
            div2.Visible = true;
            div4.Visible = true;
            div5.Visible = true;
            div8.Visible = false;
            txtDescuento.Visible = false;
            Botones.Visible = true;
            LablCantidadDeBienes.Visible = true;
            TextBoxCantidadDeBienes.Visible = true;
            InfoGeneral.Visible = true;
            MontoOriginalDeLaInversion.Visible = true;
            LblGrupo1.Visible = false;
            LblSubGrupo1.Visible = false;
            LblClase.Visible = false;
            txtAjustarCosto.Visible = false;

            String proveedor = Convert.ToString(ddlProveedor.SelectedItem);
            String tipoDoc = Convert.ToString(ddlTipoDocumento.SelectedItem);
            String NoDoc = TextBoxNoDocumento.Text;
            String tipoA = Convert.ToString(ddlTipoAdquisicion.SelectedItem);
            String Detalle = txtDetalleAdquisicion.Text;
            String Propiedad = Convert.ToString(ddlPropiedad.SelectedItem);
            string fecha = txtFechaAdquisicion.Text;
            //DateTime Fecha = Convert.ToDateTime(TxtFechaAdquisicion.Text);
            LblConcatena.Text = proveedor + "&nbsp;|&nbsp;" + tipoDoc + "&nbsp;" + NoDoc + " &nbsp;|&nbsp; " + tipoA + "," + Propiedad + "&nbsp;|&nbsp;" + fecha;
        }

        /// <summary>
        /// Método para obtener la clave CONAC
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void DropDownListCatCONAC_SelectedIndexChanged(object sender, EventArgs e)
        {
            LblGrupo1.Visible = true;
            LblSubGrupo1.Visible = true;
            LblClase.Visible = true;

            DropDownList DropCONAC = sender as DropDownList;
            string a = DropCONAC.SelectedItem.ToString();
            SqlConnection con = new SqlConnection(CConexion.Obtener());
            con.Open();
            string cadSql = "select * from Cat_CONAC where Descripcion = @dato";
            SqlCommand cmd = new SqlCommand(cadSql, con);
            cmd.Parameters.Add("@Dato", SqlDbType.VarChar, 100).Value = a;
            SqlDataReader leer = cmd.ExecuteReader();
            if (leer.Read() == true)
            {
                LabelGrupo.Text = leer["Grupo"].ToString();
                LabelSubGrupo.Text = leer["SubGrupo"].ToString();
                LabelClase.Text = leer["Clase"].ToString();
            }
            else
            {
                LabelGrupo.Text = "";
                LabelSubGrupo.Text = "";
                LabelClase.Text = "";
            }
        }

        /// <summary>
        /// Método para saber si el check de IVA esta seleccionado 
        /// y así poder realizar las operacines correspondientes. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void checkboxIVA_CheckedChanged(object sender, EventArgs e)
        {
            if(checkboxIVA.Checked == true)
            {
                SqlConnection con = new SqlConnection(CConexion.Obtener());
                con.Open();
                DateTime today = DateTime.Today;
                string anio = Convert.ToString(today.Year);
                string cadSql = " select IVA from Cat_INPC where Anio = @anio";
                SqlCommand cmd = new SqlCommand(cadSql, con);
                cmd.Parameters.Add("@anio", SqlDbType.Int, 4).Value = anio;
                SqlDataReader leer = cmd.ExecuteReader();

                if (leer.Read() == true)
                {
                    labelIVA.Text = leer["IVA"].ToString();
                }
                else
                {
                    labelIVA.Text = "0";
                }
                labelIVA.Visible = true;
                LabelSubTotal1.Text = "";

                String pu = txtPUnitario.Text;
                String iva = labelIVA.Text;
                Decimal ValorConIVA = Convert.ToDecimal(pu, CultureInfo.CreateSpecificCulture("en-US")) + (Convert.ToDecimal(pu, CultureInfo.CreateSpecificCulture("en-US")) * Convert.ToDecimal(iva));
                Decimal numResult = Math.Truncate(Convert.ToDecimal(ValorConIVA) * 100) / 100;
                var formatInfo = new NumberFormatInfo
                {
                    NumberDecimalSeparator = ".",
                    NumberDecimalDigits = 2
                };
                Decimal result = Convert.ToDecimal(numResult, CultureInfo.CreateSpecificCulture("en-US"));
                LabelSubTotal1.Text = result.ToString("N2", new CultureInfo("en-US"));

                String Propiedad = Convert.ToString(ddlPropiedad.SelectedItem);

                String descuento = txtDescuento.Text;
                Decimal ValorCostoT = (Convert.ToDecimal(result, CultureInfo.CreateSpecificCulture("en-US")) - Convert.ToDecimal(descuento, CultureInfo.CreateSpecificCulture("en-US")));
                Decimal numResult1 = Math.Truncate(Convert.ToDecimal(ValorCostoT) * 100) / 100;
                var formatInfo1 = new NumberFormatInfo
                {
                    NumberDecimalSeparator = ".",
                    NumberDecimalDigits = 2
                };
                Decimal result1 = Convert.ToDecimal(numResult1, CultureInfo.CreateSpecificCulture("en-US"));
                labelCostototal.Text = result1.ToString("N2", new CultureInfo("en-US"));
            } else
            {
                labelIVA.Text = "0";
                labelIVA.Visible = false;
                TextBoxPrecioUnitario_TextChanged(sender, e);
            }
            
        }

        /// <summary>
        /// Método para validar si el check de Ajustar costo esta seleccionado,
        /// para así poder reemplazar el costo. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void cbxAjustarCosto_CheckedChanged(object sender, EventArgs e)
        {
            if(cbxAjustarCosto.Checked == true)
            {
                txtAjustarCosto.Visible = true;
            } else
            {
                txtAjustarCosto.Text = "";
                txtAjustarCosto.Visible = false;
            }
            
        }

        /// <summary>
        /// Método para reaizar las operaciones correspondientes al ingresar una cantidad
        /// para el precio unitario. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void TextBoxPrecioUnitario_TextChanged(object sender, EventArgs e)
        {
            NumberFormatInfo nfi = new CultureInfo("es-ES", false).NumberFormat;

            if (checkboxIVA.Checked == false)
            {
                labelIVA.Text = "0";
            }
            if (checkboxDescuento.Checked == false)
            {
                txtDescuento.Text = "0";
            }
                String pu = txtPUnitario.Text;
                String iva = labelIVA.Text;
                Decimal ValorConIVA = Convert.ToDecimal(pu, CultureInfo.CreateSpecificCulture("en-US")) + 
                (Convert.ToDecimal(pu, CultureInfo.CreateSpecificCulture("en-US")) * Convert.ToDecimal(iva));
                Decimal numResult = Math.Truncate(Convert.ToDecimal(ValorConIVA) * 100) /100;
                
                Decimal result = Convert.ToDecimal(numResult, CultureInfo.CreateSpecificCulture("en-US"));
                LabelSubTotal1.Text = result.ToString("N2", new CultureInfo("en-US"));

            String Propiedad = Convert.ToString(ddlPropiedad.SelectedItem);

                String descuento = txtDescuento.Text;
                Decimal ValorCostoT = (Convert.ToDecimal(result, CultureInfo.CreateSpecificCulture("en-US")) - Convert.ToDecimal(descuento, CultureInfo.CreateSpecificCulture("en-US")));
                Decimal numResult1 = Math.Truncate(Convert.ToDecimal(ValorCostoT) * 100) / 100;
                var formatInfo1 = new NumberFormatInfo
                {
                    NumberDecimalSeparator = ".",
                    NumberDecimalDigits = 2
                };
            Decimal result1 = Convert.ToDecimal(numResult1, CultureInfo.CreateSpecificCulture("en-US"));
            labelCostototal.Text = result1.ToString("N2", new CultureInfo("en-US"));

            if (Convert.ToDouble(result1) >= 5914.3 && Propiedad == "PODER JUDICIAL DEL ESTADO DE HIDALGO")
                {
                    div9.Visible = true;
                }
                else
                {
                    div9.Visible = false;
                }
        }

        /// <summary>
        /// Método para validar si el check de descuento esta seleccionado y poder 
        /// aplicarlo al precio unitario. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void checkboxDescuento_CheckedChanged(object sender, EventArgs e)
        {
            if(checkboxDescuento.Checked == true)
            {
                txtDescuento.Visible = true;
            } else
            {
                labelCostototal.Text = "0";
                txtDescuento.Visible = false;
                TextBoxPrecioUnitario_TextChanged(sender, e);
            }
        }

        /// <summary>
        /// Método para aplicar el descuento al precio unitario, una vez que se 
        /// ingrese una cantidad al text box
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void TextBoxDescuento_TextChanged(object sender, EventArgs e)
        {
            String pu = txtPUnitario.Text;
            String iva = labelIVA.Text;
            Decimal ValorConIVA = Convert.ToDecimal(pu, CultureInfo.CreateSpecificCulture("en-US")) + (Convert.ToDecimal(pu, CultureInfo.CreateSpecificCulture("en-US")) * Convert.ToDecimal(iva));
            Decimal numResult = Math.Truncate(Convert.ToDecimal(ValorConIVA) * 100) / 100;
            var formatInfo = new NumberFormatInfo
            {
                NumberDecimalSeparator = ".",
                NumberDecimalDigits = 2
            };
            Decimal result = Convert.ToDecimal(numResult, CultureInfo.CreateSpecificCulture("en-US"));
            LabelSubTotal1.Text = result.ToString("N2", new CultureInfo("en-US"));

            String Propiedad = Convert.ToString(ddlPropiedad.SelectedItem);

            String descuento = txtDescuento.Text;
            Decimal ValorCostoT = (Convert.ToDecimal(result, CultureInfo.CreateSpecificCulture("en-US")) - Convert.ToDecimal(descuento, CultureInfo.CreateSpecificCulture("en-US")));
            Decimal numResult1 = Math.Truncate(Convert.ToDecimal(ValorCostoT) * 100) / 100;
            var formatInfo1 = new NumberFormatInfo
            {
                NumberDecimalSeparator = ".",
                NumberDecimalDigits = 2
            };
            Decimal result1 = Convert.ToDecimal(numResult1, CultureInfo.CreateSpecificCulture("en-US"));
            labelCostototal.Text = result1.ToString("N2", new CultureInfo("en-US"));
        }

        /// <summary>
        /// Método para validar si la partida seleccinada en el drop es de tipo
        /// BIENES MUEBLES, si es así el div8 se mostrará y se pedirá al usuario 
        /// que ingrese los datos de la dimención del Bien. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void DropDownListPartida_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList DropPartida = sender as DropDownList;
            string p = DropPartida.SelectedItem.ToString();
            if(p == "BIENES MUEBLES                                                                                                                                                                                                                                                 ")
            {
                div8.Visible = true;
            } else
            {
                div8.Visible = false;
            }
        }

        /// <summary>
        /// Método para validar si el Número de Documento ya existe en la tabla Fichaompra
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public bool existe1(string num)
        {
            string num1 = TextBoxNoDocumento.Text;
            SqlConnection cnn = new SqlConnection(CConexion.Obtener());
            string sql = "select count(*)from FichaCompra where NumDocumento = @num";
            cnn.Open();

            try
            {
                SqlCommand query = new SqlCommand(sql, cnn);
                query.Parameters.AddWithValue("@num", num1);

                int count = Convert.ToInt32(query.ExecuteScalar());
                if (count == 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch (SqlException) { throw; }
            finally
            {
                if (cnn.State == ConnectionState.Open) cnn.Close();
                if (cnn.State == ConnectionState.Broken) cnn.Close();
            }
        }

        /// <summary>
        /// Método para validar cuando se ingrese un número de documento y poder
        /// realizar una consulta a la BD y llenar el GRID con los datos correspondientes. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void TextBoxNoDocumento_TextChanged(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(CConexion.Obtener());
            con.Open();
            DateTime today = DateTime.Today;
            string anio = Convert.ToString(today.Year);
            string cadSql = " select IVA from Cat_INPC where Anio = @anio";
            SqlCommand cmd = new SqlCommand(cadSql, con);
            cmd.Parameters.Add("@anio", SqlDbType.Int, 4).Value = anio;
            SqlDataReader leer = cmd.ExecuteReader();

            if (leer.Read() == true)
            {
                LblIva1.Text = leer["IVA"].ToString();
            }
            else
            {
                LblIva1.Text = "0";
            }

            if (!existe1(this.TextBoxNoDocumento.Text))
            {
                btnGuardarFactura.Visible = true;
                IniciarLlenadoDropDownListProveedor();
                IniciarLlenadoDropDownTipoDocumento();
                IniciarLlenadoDropDownTipoAdquisicion();
                IniciarLlenadoDropDownListPropiedad();
                txtDetalleAdquisicion.Text = "";
                txtFechaAdquisicion.Text = "";
                LLenarGrid();
            }
            else
            {
                btnGuardarFactura.Visible = false;
                /////// Llenar Campos ////////
                DataTable BuscarFactura = new DataTable();
                BuscarFactura = BdRegistroBien.BuscarFactura(TextBoxNoDocumento);

                List<CRegistroBien> FacturaList = new List<CRegistroBien>();
                for (int i = 0; i < BuscarFactura.Rows.Count; i++)
                {
                    CRegistroBien cFactura = new CRegistroBien();
                    cFactura.NumDocumento = Convert.ToString(BuscarFactura.Rows[i]["NumDocumento"]);
                    cFactura.FechaAdquisicion = Convert.ToString(BuscarFactura.Rows[i]["FechaAdquisicion"]);
                    cFactura.DetAdqui = Convert.ToString(BuscarFactura.Rows[i]["DetAdqui"]);
                    FacturaList.Add(cFactura);
                }

                List<CRegistroBien> ProveedorFactura = new List<CRegistroBien>();
                ProveedorFactura = BdRegistroBien.ProveedorFactura(TextBoxNoDocumento);
                ddlProveedor.DataSource = ProveedorFactura;
                ddlProveedor.DataTextField = "Proveedor";
                ddlProveedor.DataBind();
                //////
                List<CRegistroBien> DocumentoFactura = new List<CRegistroBien>();
                DocumentoFactura = BdRegistroBien.DocumentoFactura(TextBoxNoDocumento);
                ddlTipoDocumento.DataSource = DocumentoFactura;
                ddlTipoDocumento.DataTextField = "Documento";
                ddlTipoDocumento.DataBind();
                //////
                TextBoxNoDocumento.Text = FacturaList.FirstOrDefault().NumDocumento.ToString();
                //////
                List<CRegistroBien> AdquiFactura = new List<CRegistroBien>();
                AdquiFactura = BdRegistroBien.AdquiFactura(TextBoxNoDocumento);
                ddlTipoAdquisicion.DataSource = AdquiFactura;
                ddlTipoAdquisicion.DataTextField = "TipoAdqui";
                ddlTipoAdquisicion.DataBind();
                //////
                txtFechaAdquisicion.Text = FacturaList.FirstOrDefault().FechaAdquisicion.ToString();
                //////
                List<CRegistroBien> PropiedadFactura = new List<CRegistroBien>();
                PropiedadFactura = BdRegistroBien.PropiedadFactura(TextBoxNoDocumento);
                ddlPropiedad.DataSource = PropiedadFactura;
                ddlPropiedad.DataTextField = "Propiedad";
                ddlPropiedad.DataBind();
                ///////
                txtDetalleAdquisicion.Text = FacturaList.FirstOrDefault().DetAdqui.ToString();

                /////// LLenar Grid ////////
                MostrarMensaje("** Número de documento existente **", "info", "Normal");
                    DataTable Datos = new DataTable();
                    Datos = BdRegistroBien.Grid(TextBoxNoDocumento);
                    gridFactura.DataSource = Datos;
                    gridFactura.DataBind();
                String texto1 = "";
                String texto2 = "";
                if (gridFactura.Rows.Count == 2)
                {
                    texto1 = gridFactura.Rows[0].Cells[2].Text;
                    texto2 = gridFactura.Rows[1].Cells[2].Text; 
                }
                else if (gridFactura.Rows.Count == 1)
                {
                    texto1 = gridFactura.Rows[0].Cells[2].Text;
                    texto2 = "0";
                }
                else if (gridFactura.Rows.Count == 0)
                {
                    texto1 = "0";
                    texto2 = "0";
                    LLenarGrid();
                }

                    double suma = Convert.ToDouble(texto1) + Convert.ToDouble(texto2);
                    LblSubTo.Text = suma.ToString("N2", new CultureInfo("en-US"));
               


                    Double sub = Convert.ToDouble(suma);
                    Double iv = Convert.ToDouble(LblIva1.Text);
                    double iva = (sub * iv);
                    LblIva1.Text = iva.ToString("N2", new CultureInfo("en-US"));

                    double resIva = iva + sub;
                    LblTotal1.Text = resIva.ToString("N2", new CultureInfo("en-US"));

          

            }
        }

        /// <summary>
        /// Método para validar si es check de registro individual esta seleccionado y así 
        /// mostrar los campos correspondientes a dicho registro. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void RdRegistroIndividual_CheckedChanged(object sender, EventArgs e)
        {
            div2.Visible = true;
            div4.Visible = true;
            div5.Visible = true;
            divConcatena.Visible = true;
            Botones.Visible = true;
            InfoGeneral.Visible = true;
            MontoOriginalDeLaInversion.Visible = true;
            LablCantidadDeBienes.Visible = false;
            TextBoxCantidadDeBienes.Visible = false;

            String proveedor = Convert.ToString(ddlProveedor.SelectedItem);
            String tipoDoc = Convert.ToString(ddlTipoDocumento.SelectedItem);
            String NoDoc = TextBoxNoDocumento.Text;
            String tipoA = Convert.ToString(ddlTipoAdquisicion.SelectedItem);
            String Detalle = txtDetalleAdquisicion.Text;
            String Propiedad = Convert.ToString(ddlPropiedad.SelectedItem);
            string fecha = txtFechaAdquisicion.Text;
            LblConcatena.Text = proveedor + "&nbsp;|&nbsp;" + tipoDoc + "&nbsp;" + NoDoc + " &nbsp;|&nbsp; " + tipoA + "," + Propiedad + "&nbsp;|&nbsp;" + fecha;
        }

        /// <summary>
        /// Método para validar si es número de documento existe y asi poder mostrar 
        /// los campos correspondientes al registro. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void BtnNuevoRegistro_Click(object sender, EventArgs e)
        {
            if (!existe1(this.TextBoxNoDocumento.Text))
            {
                MostrarMensaje("** No existe el nùmero de documento **", "info", "Normal");
            }
            else
            {
                Factura.Visible = false;
                Bien.Visible = true;
            }
        }

        /// <summary>
        /// Métoodo para cancelar el registro de una nueva factura. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void BtnCancelarFactura_Click(object sender, EventArgs e)
        {
            txtDetalleAdquisicion.Text = "";
            TextBoxNoDocumento.Text = "";
            txtFechaAdquisicion.Text = "";
            LLenarGrid();
            IniciarLlenadoDropDownListProveedor();
            IniciarLlenadoDropDownTipoDocumento();
            IniciarLlenadoDropDownTipoAdquisicion();
            IniciarLlenadoDropDownListPropiedad();
        }

        /// <summary>
        /// Método para guardar el registro de la nueva factura. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void BtnGuardarFactura_Click(object sender, EventArgs e)
        {
            if (ddlProveedor.SelectedIndex != 0)
            {
                if (ddlTipoDocumento.SelectedIndex != 0)
                {
                    if (ddlTipoAdquisicion.SelectedIndex != 0)
                    {
                        if (ddlPropiedad.SelectedIndex != 0)
                        {
                            if (TextBoxNoDocumento.Text != "")
                            {
                                if (txtFechaAdquisicion.Text != "")
                                {
                                    if (!existe1(this.TextBoxNoDocumento.Text))
                                    {
                                        int idProveedor = Convert.ToInt32(ddlProveedor.SelectedValue);
                                        int idTipoDoc = Convert.ToInt32(ddlTipoDocumento.SelectedValue);
                                        int idTipoAdqui = Convert.ToInt32(ddlTipoAdquisicion.SelectedValue);
                                        string DetalleAdqui = txtDetalleAdquisicion.Text;
                                        int idPropiedad = Convert.ToInt32(ddlPropiedad.SelectedValue);
                                        string NumDoc = TextBoxNoDocumento.Text;
                                        string fecha = txtFechaAdquisicion.Text;
                                        SqlConnection Conn = new SqlConnection(CConexion.Obtener());
                                        bool success = false;
                                        SqlTransaction lTransaccion = null;
                                        int Valor_Retornado = 0;
                                        try
                                        {
                                            Conn.Open();
                                            lTransaccion = Conn.BeginTransaction(System.Data.IsolationLevel.Serializable);
                                            SqlCommand cmd = new SqlCommand("SP_Insertar_Factura", Conn, lTransaccion);
                                            cmd.CommandType = CommandType.StoredProcedure;
                                            cmd.Parameters.Clear();
                                            cmd.Parameters.Add(new SqlParameter("@IdProveedor", idProveedor));
                                            cmd.Parameters.Add(new SqlParameter("@IdTipoDoc", idTipoDoc));
                                            cmd.Parameters.Add(new SqlParameter("@NumDocumento", NumDoc));
                                            cmd.Parameters.Add(new SqlParameter("@IdTipoAdqui", idTipoAdqui));
                                            cmd.Parameters.Add(new SqlParameter("@DetAdqui", DetalleAdqui.ToUpper()));
                                            cmd.Parameters.Add(new SqlParameter("@FechaAdquisicion", fecha));
                                            cmd.Parameters.Add(new SqlParameter("@IdPropiedad", idPropiedad));
                                            SqlParameter ValorRetorno = new SqlParameter("@Comprobacion", SqlDbType.Int);
                                            ValorRetorno.Direction = ParameterDirection.Output;
                                            cmd.Parameters.Add(ValorRetorno);
                                            cmd.ExecuteNonQuery();
                                            Valor_Retornado = Convert.ToInt32(ValorRetorno.Value);
                                            if (Valor_Retornado == 1)
                                                success = true;
                                        }
                                        catch (Exception)
                                        {
                                            MostrarMensaje("** Error al insertar Factura **", "info", "Normal");
                                        }
                                        finally
                                        {
                                            if (success)
                                            {
                                                lTransaccion.Commit();
                                                Conn.Close();
                                                MostrarMensaje("** Factura guardada satisfactoriamente **", "info", "Normal");
                                            }
                                            else
                                            {
                                                lTransaccion.Rollback();
                                                Conn.Close();
                                            }
                                            LimpiarFactura();
                                            LLenarGrid();
                                        }
                                    }
                                    else
                                    {
                                        MostrarMensaje("** Número de documento existente, ingrese uno diferente **", "info", "Normal");
                                    }
                                }
                                else
                                {
                                    MostrarMensaje("** La fecha es requerida **", "info", "Normal");
                                }
                            }
                            else
                            {
                                MostrarMensaje("** El Número de documento es requerido **", "info", "Normal");
                            }
                        }
                        else
                        {
                            MostrarMensaje("** La propiedad es requerida **", "info", "Normal");
                        }
                    }
                    else
                    {
                        MostrarMensaje("** El tipo de adquisición es requerido **", "info", "Normal");
                    }
                }
                else
                {
                    MostrarMensaje("** El tipo de documento es requerido **", "info", "Normal");
                }
            }
            else
            {
                MostrarMensaje("** El proveedor es requerido **", "info", "Normal");
            }
        }

        /// <summary>
        /// Método para limpiar los campos del registro de una nueva factura. 
        /// </summary>
        public void LimpiarFactura()
        {
            ddlProveedor.SelectedIndex = 0;
            ddlTipoDocumento.SelectedIndex = 0;
            ddlTipoAdquisicion.SelectedIndex = 0;
            txtDetalleAdquisicion.Text = string.Empty;
            ddlPropiedad.SelectedIndex = 0;
            TextBoxNoDocumento.Text = string.Empty;
            txtFechaAdquisicion.Text = string.Empty;
        }

        /// <summary>
        /// Método para guardar el registro del Bien. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void BtnGuardar_Click(object sender, EventArgs e)
        {
            if (rbtnRegistroIndividual.Checked == true)
            {
                if (txtNombreBien.Text != "")
                {
                    if (ddlPartida.SelectedIndex != 0)
                    {
                        if (ddlSubClase.SelectedIndex != 0)
                        {
                            if (ddlCatCONAC.SelectedIndex != 0)
                            {
                                if (ddlMarca.SelectedIndex != 0)
                                {
                                    if (ddlResguardoInicial.SelectedIndex != 0)
                                    {
                                        if (txtPUnitario.Text != "")
                                        {
                                            InsertarBienIndividual();
                                            String pu = txtPUnitario.Text;
                                            String iva = labelIVA.Text;
                                            Decimal ValorConIVA = Convert.ToDecimal(pu, CultureInfo.CreateSpecificCulture("en-US")) + (Convert.ToDecimal(pu, CultureInfo.CreateSpecificCulture("en-US")) * Convert.ToDecimal(iva));
                                            Decimal numResult = Math.Truncate(Convert.ToDecimal(ValorConIVA) * 100) / 100;
                                            var formatInfo = new NumberFormatInfo
                                            {
                                                NumberDecimalSeparator = ".",
                                                NumberDecimalDigits = 2
                                            };
                                            Decimal result = Convert.ToDecimal(numResult, CultureInfo.CreateSpecificCulture("en-US"));
                                            LabelSubTotal1.Text = result.ToString("N2", new CultureInfo("en-US"));

                                            String Propiedad = Convert.ToString(ddlPropiedad.SelectedItem);

                                            String descuento = txtDescuento.Text;
                                            Decimal ValorCostoT = (Convert.ToDecimal(result, CultureInfo.CreateSpecificCulture("en-US")) - Convert.ToDecimal(descuento, CultureInfo.CreateSpecificCulture("en-US")));
                                            Decimal numResult1 = Math.Truncate(Convert.ToDecimal(ValorCostoT) * 100) / 100;
                                            var formatInfo1 = new NumberFormatInfo
                                            {
                                                NumberDecimalSeparator = ".",
                                                NumberDecimalDigits = 2
                                            };
                                            Decimal result1 = Convert.ToDecimal(numResult1, CultureInfo.CreateSpecificCulture("en-US"));
                                            labelCostototal.Text = result1.ToString("N2", new CultureInfo("en-US"));
                                            

                                            if (cbxAjustarCosto.Checked == true)
                                            {
                                                if (txtAjustarCosto.Text != "")
                                                {
                                                    Decimal ajustar = Convert.ToDecimal(txtAjustarCosto.Text, CultureInfo.CreateSpecificCulture("en-US"));
                                                    labelCostototal.Text = ajustar.ToString("N", new CultureInfo("en-US"));
                                                }
                                            }

                                            if (div8.Visible == true)

                                            {
                                                if (TextBoxFrente.Text != "")
                                                {
                                                    if (TextBoxFondo.Text != "")
                                                    {
                                                        if (TextBoxAltura.Text != "")
                                                        {
                                                            if (TextBoxDiametro.Text != "")
                                                            {
                                                                InsertarDimencionBien();
                                                            }
                                                            else
                                                            {
                                                                MostrarMensaje("** Diametro requerido **", "info", "Normal");
                                                            }
                                                        }
                                                        else
                                                        {
                                                            MostrarMensaje("** Altura requerido **", "info", "Normal");
                                                        }
                                                    }
                                                    else
                                                    {
                                                        MostrarMensaje("** Fondo requerido **", "info", "Normal");
                                                    }
                                                }
                                                else
                                                {
                                                    MostrarMensaje("** Frente requerido **", "info", "Normal");
                                                }
                                            }

                                            int id = Convert.ToInt32(ddlPartida.SelectedValue);
                                            SqlConnection con = new SqlConnection(CConexion.Obtener());
                                            con.Open();
                                            string cadSql = "select NumPartida from Cat_Partidas where idPartida = @id";
                                            SqlCommand cmd1 = new SqlCommand(cadSql, con);
                                            cmd1.Parameters.Add("@id", SqlDbType.Int).Value = id;
                                            SqlDataReader leer = cmd1.ExecuteReader();
                                            string tipo;
                                            if (leer.Read() == true)
                                            {
                                                tipo = leer[0].ToString();
                                            }
                                            else
                                            {
                                                tipo = "";
                                            }
                                            con.Close();

                                            if (Convert.ToDouble(result1) >= 5914.3 && Propiedad == "PODER JUDICIAL DEL ESTADO DE HIDALGO" && Convert.ToInt32(tipo) >= 5000)
                                            {
                                                if (div9.Visible == true)
                                                {
                                                    if (txtVidaUtil.Text != "")
                                                    {
                                                        insertarEnDepreciar();
                                                    }
                                                    else
                                                    {
                                                        MostrarMensaje("** Vida útil requerida **", "info", "Normal");
                                                    }
                                                }
                                            }
                                            MostrarNumInventarioIndividual(sender, e);
                                           /// LimpiarRegistro();

                                        }
                                        else
                                        {
                                            MostrarMensaje("** Precio Unitario requerido **", "info", "Normal");
                                        }
                                    }
                                    else
                                    {
                                        MostrarMensaje("** Área de resguardo requerida **", "info", "Normal");
                                    }
                                }
                                else
                                {
                                    MostrarMensaje("** Marca requerida **", "info", "Normal");
                                }
                            }
                            else
                            {
                                MostrarMensaje("** Catálago CONAC requerido **", "info", "Normal");
                            }
                        }
                        else
                        {
                            MostrarMensaje("** Subclase requerida **", "info", "Normal");
                        }
                    }
                    else
                    {
                        MostrarMensaje("** Partida requerida **", "info", "Normal");
                    }
                }
                else
                {
                    MostrarMensaje("** Nombre del Bien requerido **", "info", "Normal");
                }
            }
            else if (rbtnRegistroLote.Checked == true)
            {
                if (TextBoxCantidadDeBienes.Text != "")
                {
                    if (txtNombreBien.Text != "")
                    {
                        if (ddlPartida.SelectedIndex != 0)
                        {
                            if (ddlSubClase.SelectedIndex != 0)
                            {
                                if (ddlCatCONAC.SelectedIndex != 0)
                                {
                                    if (ddlMarca.SelectedIndex != 0)
                                    {
                                        if (ddlResguardoInicial.SelectedIndex != 0)
                                        {
                                            if (txtPUnitario.Text != "")
                                            {
                                                InsertarBienPorLote();

                                               
                                                MostrarNumInventario(sender, e);
                                                LimpiarRegistro();
                                            }
                                            else
                                            {
                                                MostrarMensaje("** Precio Unitario requerido **", "info", "Normal");
                                            }
                                        }
                                        else
                                        {
                                            MostrarMensaje("** Área de resguardo requerida **", "info", "Normal");
                                        }
                                    }
                                    else
                                    {
                                        MostrarMensaje("** Marca requerida **", "info", "Normal");
                                    }
                                }
                                else
                                {
                                    MostrarMensaje("** Catálago CONAC requerido **", "info", "Normal");
                                }
                            }
                            else
                            {
                                MostrarMensaje("** Subclase requerida **", "info", "Normal");
                            }
                        }
                        else
                        {
                            MostrarMensaje("** Partida requerida **", "info", "Normal");
                        }
                    }
                    else
                    {
                        MostrarMensaje("** Nombre del Bien requerido **", "info", "Normal");
                    }
                }
                else
                {
                    MostrarMensaje("** Cantidad de Bienes requerida **", "info", "Normal");
                }
            }
        }

        /// <summary>
        /// Método para mostrar el número de inventario que se registro. 
        /// </summary>
        /// <param name="n"></param>
        private void MostrarNumero(string n)
        {
                string Msj = "";
            Msj = "MostrarMensaje('" + n + "');";

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Ntn", Msj, true);
        }

        /// <summary>
        /// Método para insertar el registro en las tablas FichaBien, HistoricoUbicacion y
        /// Ubicacion haciendo uso de procedimientos almacenados. 
        /// </summary>
        public void InsertarBienIndividual()
        {
            DataTable TablaTipoPartida = new DataTable();
            TablaTipoPartida = BdRegistroBien.TipoPartida(ddlPartida);
            string TipoPartida;


            string nombre = txtNombreBien.Text;
            int idCONAC = Convert.ToInt32(ddlCatCONAC.SelectedValue);
            int idSubClase = Convert.ToInt32(ddlSubClase.SelectedValue);
            string modelo = txtModelo.Text;
            string serie = txtSerie.Text;
            int marca = Convert.ToInt32(ddlMarca.SelectedValue);
            string detalle = txtDetalle.Text;
            string fechaAd = txtFechaAdquisicion.Text;
            string precio = txtPUnitario.Text;

            //Obtiene TipoPartida
            List<CRegistroBien> PartidaList = new List<CRegistroBien>();
            for (int i = 0; i < TablaTipoPartida.Rows.Count; i++)
            {
                CRegistroBien cTipoPartida = new CRegistroBien();
                cTipoPartida.TipoPartida = Convert.ToInt32(TablaTipoPartida.Rows[i]["TipoPartida"]);

                PartidaList.Add(cTipoPartida);
            }
            TipoPartida = PartidaList.FirstOrDefault().TipoPartida.ToString();


            ///////
            DataTable TablaENSU = new DataTable();
            TablaENSU = BdRegistroBien.idENSU(ddlResguardoInicial);
            string ENSU;
            

            List<CRegistroBien> ENSUList = new List<CRegistroBien>();
            for (int i = 0; i < TablaENSU.Rows.Count; i++)
            {
                CRegistroBien cRegistroDeUnBien = new CRegistroBien();
                cRegistroDeUnBien.IdENSU = Convert.ToInt32(TablaENSU.Rows[i]["idENSU"]);

                ENSUList.Add(cRegistroDeUnBien);
            }
            ENSU = ENSUList.FirstOrDefault().IdENSU.ToString();


            ///////
            DataTable TablaCompra = new DataTable();
            TablaCompra = BdRegistroBien.idCompra(TextBoxNoDocumento);
            string Compra;
            ///////
            List<CRegistroBien> CompraList = new List<CRegistroBien>();
            for (int i = 0; i < TablaCompra.Rows.Count; i++)
            {
                CRegistroBien cRegistroDeUnBien = new CRegistroBien();
                cRegistroDeUnBien.IdCompra = Convert.ToInt32(TablaCompra.Rows[i]["IdCompra"]);

                CompraList.Add(cRegistroDeUnBien);
            }
            Compra = CompraList.FirstOrDefault().IdCompra.ToString();


            ///////
            SqlConnection con = new SqlConnection(CConexion.Obtener());
            con.Open();
            //string sqlCad1 = " SELECT MAX(RIGHT(NumInventario, 4)) FROM FichaBien";
            string sqlCad1 = " SELECT FOLIO FROM CAT_SUBCLASE WHERE IDSUBCLASE = @IdSubclase";
            SqlCommand cmdFolio = new SqlCommand(sqlCad1, con);
            cmdFolio.Parameters.AddWithValue("@IdSubclase", idSubClase);
            SqlDataReader leerMax1 = cmdFolio.ExecuteReader();
            string Folio;
            if (leerMax1.Read() == true)
            {
                Folio = leerMax1[0].ToString();
                if (Folio == "")
                {
                    Folio = "0001";
                }
                else
                {
                    Folio = leerMax1[0].ToString();
                }
            }
            else
            {
                Folio = "";
            }
            con.Close();

            

            int c;
            string NumInventario="";
            string consecutivo="0000";
        
            for(c= Convert.ToInt32(Folio); c<= Convert.ToInt32(Folio) + 1; c++)
            {
                if (c < 10)
                { // Se agregan 3 ceros
                    consecutivo = "000" + c;
                }
                else if (c > 9 && c < 100)
                { // Se agregan 2 ceros
                    consecutivo = "00" + c;
                }
                else if (c > 99 && c < 1000)
                {// Se agrega un cero
                    consecutivo = "0" + c;
                }
                else {
                    // No se agrega nada
                    consecutivo = c.ToString();
                }

                String subClase = Convert.ToString(ddlSubClase.SelectedValue); /// 
                String Partida = Convert.ToString(ddlPartida.SelectedValue);
                NumInventario = LabelGrupo.Text + LabelSubGrupo.Text + LabelClase.Text + subClase + "81" + TipoPartida + consecutivo;
               

            }



            String pu = txtPUnitario.Text;
            String iva = labelIVA.Text;
            Decimal ValorConIVA = Convert.ToDecimal(pu, CultureInfo.CreateSpecificCulture("en-US")) + (Convert.ToDecimal(pu, CultureInfo.CreateSpecificCulture("en-US")) * Convert.ToDecimal(iva));
            Decimal numResult = Math.Truncate(Convert.ToDecimal(ValorConIVA) * 100) / 100;
            Decimal result = Convert.ToDecimal(numResult, CultureInfo.CreateSpecificCulture("en-US"));

            String descuento = txtDescuento.Text;
            Decimal ValorCostoT = (Convert.ToDecimal(result, CultureInfo.CreateSpecificCulture("en-US")) - Convert.ToDecimal(descuento, CultureInfo.CreateSpecificCulture("en-US")));
            Decimal numResult1 = Math.Truncate(Convert.ToDecimal(ValorCostoT) * 100) / 100;
            Decimal result1 = Convert.ToDecimal(numResult1, CultureInfo.CreateSpecificCulture("en-US"));
            int IdUsuario = 5;
            int IdAct = 1;

            string desc = txtDescuento.Text;
            string costoT = Convert.ToString(result1);

            if (cbxAjustarCosto.Checked == true)
            {
                if (txtAjustarCosto.Text != "")
                {
                    costoT = txtAjustarCosto.Text;
                }
            }

            SqlConnection Conn = new SqlConnection(CConexion.Obtener());
            bool success = false;
            SqlTransaction lTransaccion = null;
            int Valor_Retornado = 0;

            int NuevoFolio = (int.Parse(consecutivo));

            try
            {
                Conn.Open();
                lTransaccion = Conn.BeginTransaction(System.Data.IsolationLevel.Serializable);
                SqlCommand cmd = new SqlCommand("SP_Registro_Individual", Conn, lTransaccion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                cmd.Parameters.Add(new SqlParameter("@NumInventario", Convert.ToInt64(NumInventario)));
                cmd.Parameters.Add(new SqlParameter("@DescripcionBien", nombre.ToUpper()));
                cmd.Parameters.Add(new SqlParameter("@IdCONAC", idCONAC));
                cmd.Parameters.Add(new SqlParameter("@IdSubClase", idSubClase));
                cmd.Parameters.Add(new SqlParameter("@Modelo", modelo.ToUpper()));
                cmd.Parameters.Add(new SqlParameter("@Serie", serie.ToUpper()));
                cmd.Parameters.Add(new SqlParameter("@IdMarca", marca));
                cmd.Parameters.Add(new SqlParameter("@Detalle", detalle.ToUpper()));
                cmd.Parameters.Add(new SqlParameter("@TipoPartida", TipoPartida));
                cmd.Parameters.Add(new SqlParameter("@IdENSU", ENSU));
                cmd.Parameters.Add(new SqlParameter("@FechaCaptura", Convert.ToDateTime(fechaAd)));
                cmd.Parameters.Add(new SqlParameter("@PrecioUnitario", Convert.ToDouble(precio)));
                cmd.Parameters.Add(new SqlParameter("@SubTotal", Convert.ToDouble(result)));
                cmd.Parameters.Add(new SqlParameter("@Descuento", Convert.ToDouble(desc)));
                cmd.Parameters.Add(new SqlParameter("@CostoTotal", Convert.ToDouble(costoT)));
                cmd.Parameters.Add(new SqlParameter("@IdCompra", Compra));
                cmd.Parameters.Add(new SqlParameter("@folio", NuevoFolio));
                SqlParameter ValorRetorno = new SqlParameter("@Comprobacion", SqlDbType.Int);
                ValorRetorno.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(ValorRetorno);
                cmd.ExecuteNonQuery();
                Valor_Retornado = Convert.ToInt32(ValorRetorno.Value);
                if (Valor_Retornado == 1)
                    success = true;
            }
            catch (Exception ex)
            {
                string script = ex.ToString();

                MostrarMensaje("** Error al registrar el Bien **", "info", "Normal");
            }
            finally
            {
                if (success)
                {
                    lTransaccion.Commit();
                    Conn.Close();
                }
                else
                {
                    lTransaccion.Rollback();
                    Conn.Close();
                }
            }


            ////////////////////////////////////////////
            con.Open();
            string sqlCadid = "SELECT MAX (IdInventario) FROM FichaBien";
            SqlCommand cmdMaxid = new SqlCommand(sqlCadid, con);
            SqlDataReader leerMaxid = cmdMaxid.ExecuteReader();
            string maxid;
            if (leerMaxid.Read() == true)
            {
                maxid = leerMaxid[0].ToString();
            }
            else
            {
                maxid = "";
            }
            con.Close();
            try
            {
                Conn.Open();
                lTransaccion = Conn.BeginTransaction(System.Data.IsolationLevel.Serializable);
                SqlCommand cmd = new SqlCommand("SP_Insertar_HistoricoUbicacion", Conn, lTransaccion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                cmd.Parameters.Add(new SqlParameter("@IdInventario", Convert.ToInt32(maxid)));
                cmd.Parameters.Add(new SqlParameter("@IdENSU", ENSU));
                cmd.Parameters.Add(new SqlParameter("@IdUsuario", IdUsuario));
                cmd.Parameters.Add(new SqlParameter("@Fecha", Convert.ToDateTime(fechaAd)));
                cmd.Parameters.Add(new SqlParameter("@IdActividad", IdAct));
                SqlParameter ValorRetorno = new SqlParameter("@Comprobacion", SqlDbType.Int);
                ValorRetorno.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(ValorRetorno);
                cmd.ExecuteNonQuery();
                Valor_Retornado = Convert.ToInt32(ValorRetorno.Value);
                if (Valor_Retornado == 1)
                    success = true;
            }
            catch (Exception ex)
            {
                string script = ex.ToString();

                MostrarMensaje("** Error al insertar Serie **", "info", "Normal");
            }
            finally
            {
                if (success)
                {
                    lTransaccion.Commit();
                    Conn.Close();
                }
                else
                {
                    lTransaccion.Rollback();
                    Conn.Close();
                }
            }


            ///////////////////////////////////////////
            //Insertar en Tabla Ubicación

            try
            {
                Conn.Open();
                lTransaccion = Conn.BeginTransaction(System.Data.IsolationLevel.Serializable);
                SqlCommand cmd = new SqlCommand("SP_Insertar_Ubicacion", Conn, lTransaccion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                cmd.Parameters.Add(new SqlParameter("@IdInventario", Convert.ToInt32(maxid)));
                cmd.Parameters.Add(new SqlParameter("@IdENSU", ENSU));
                cmd.Parameters.Add(new SqlParameter("@IdUsuario", IdUsuario));
                cmd.Parameters.Add(new SqlParameter("@Fecha", Convert.ToDateTime(fechaAd)));
                cmd.Parameters.Add(new SqlParameter("@IdActividad", IdAct));
                SqlParameter ValorRetorno = new SqlParameter("@Comprobacion", SqlDbType.Int);
                ValorRetorno.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(ValorRetorno);
                cmd.ExecuteNonQuery();
                Valor_Retornado = Convert.ToInt32(ValorRetorno.Value);
                if (Valor_Retornado == 1)
                    success = true;
            }
            catch (Exception ex)
            {
                string script = ex.ToString();

                MostrarMensaje("** Error al insertar la ubicación del bien **", "info", "Normal");
            }
            finally
            {
                if (success)
                {
                    lTransaccion.Commit();
                    Conn.Close();
                }
                else
                {
                    lTransaccion.Rollback();
                    Conn.Close();
                }
            }


        }

        /// <summary>
        /// Método para insertar en la tabla dimención bien, haciendo uso de un
        /// procedimiento almacenado. 
        /// </summary>
        public void InsertarDimencionBien()
        {
            SqlConnection con = new SqlConnection(CConexion.Obtener());
            ///////////////////////////////////////////////////////////
            con.Open();
            string cadSql1 = "select MAX(idInventario) from FichaBien";
            SqlCommand cmd11 = new SqlCommand(cadSql1, con);
            SqlDataReader leer1 = cmd11.ExecuteReader();
            string idInventario1;

            if (leer1.Read() == true)
            {
                idInventario1 = leer1[0].ToString();
            }
            else
            {
                idInventario1 = "";
            }
            con.Close();
            ///////////////////////////////////////////////////////////////
            con.Open();
            string cadSql = "select IdInventario from FichaBien where IdInventario = @idInventario";
            SqlCommand cmd1 = new SqlCommand(cadSql, con);
            cmd1.Parameters.AddWithValue("@idInventario", idInventario1);
            SqlDataReader leer = cmd1.ExecuteReader();
            string idInven;
            if (leer.Read() == true)
            {
                idInven = leer[0].ToString();
            }
            else
            {
                idInven = "";
            }
            con.Close();


            string Frente = TextBoxFrente.Text;
            string Fondo = TextBoxFondo.Text;
            string Altura = TextBoxAltura.Text;
            string Diametro = TextBoxDiametro.Text;

            String subClase = Convert.ToString(ddlSubClase.SelectedValue);
            String Partida = Convert.ToString(ddlPartida.SelectedValue);
            string num = LabelGrupo.Text + LabelSubGrupo.Text + LabelClase.Text + subClase + "81" + Partida;

            SqlConnection Conn = new SqlConnection(CConexion.Obtener());
            bool success = false;
            SqlTransaction lTransaccion = null;
            int Valor_Retornado = 0;
            try
            {
                Conn.Open();
                lTransaccion = Conn.BeginTransaction(System.Data.IsolationLevel.Serializable);
                SqlCommand cmd = new SqlCommand("SP_Insertar_DimencionBien", Conn, lTransaccion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                cmd.Parameters.Add(new SqlParameter("@IdInventario", idInven));
                cmd.Parameters.Add(new SqlParameter("@Frente", Frente));
                cmd.Parameters.Add(new SqlParameter("@Fondo", Fondo));
                cmd.Parameters.Add(new SqlParameter("@Altura", Altura));
                cmd.Parameters.Add(new SqlParameter("@Diametro", Diametro));
                SqlParameter ValorRetorno = new SqlParameter("@Comprobacion", SqlDbType.Int);
                ValorRetorno.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(ValorRetorno);
                cmd.ExecuteNonQuery();
                Valor_Retornado = Convert.ToInt32(ValorRetorno.Value);
                if (Valor_Retornado == 1)
                    success = true;
            }
            catch (Exception ex)
            {
                string script = ex.ToString();
                MostrarMensaje("** Error al Registrar Dimención Bien **", "info", "Normal");
            }
            finally
            {
                if (success)
                {
                    lTransaccion.Commit();
                    Conn.Close();
                }
                else
                {
                    lTransaccion.Rollback();
                    Conn.Close();
                }
            }
        }

        /// <summary>
        /// Método para insertar un bien por lotes.
        /// </summary>
        public void InsertarBienPorLote()
        {
            DataTable TablaTipoPartida = new DataTable();
            TablaTipoPartida = BdRegistroBien.TipoPartida(ddlPartida);
            string tipo;
            ///////
            List<CRegistroBien> PartidaList = new List<CRegistroBien>();
            for (int i = 0; i < TablaTipoPartida.Rows.Count; i++)
            {
                CRegistroBien cTipoPartida = new CRegistroBien();
                cTipoPartida.TipoPartida = Convert.ToInt32(TablaTipoPartida.Rows[i]["TipoPartida"]);

                PartidaList.Add(cTipoPartida);
            }
            tipo = PartidaList.FirstOrDefault().TipoPartida.ToString();
            ///////
            DataTable TablaENSU = new DataTable();
            TablaENSU = BdRegistroBien.idENSU(ddlResguardoInicial);
            string ENSU;
            ///////
            List<CRegistroBien> ENSUList = new List<CRegistroBien>();
            for (int i = 0; i < TablaENSU.Rows.Count; i++)
            {
                CRegistroBien cRegistroDeUnBien = new CRegistroBien();
                cRegistroDeUnBien.IdENSU = Convert.ToInt32(TablaENSU.Rows[i]["idENSU"]);

                ENSUList.Add(cRegistroDeUnBien);
            }
            ENSU = ENSUList.FirstOrDefault().IdENSU.ToString();
            ///////
            DataTable TablaCompra = new DataTable();
            TablaCompra = BdRegistroBien.idCompra(TextBoxNoDocumento);
            string Compra;
            ///////
            List<CRegistroBien> CompraList = new List<CRegistroBien>();
            for (int i = 0; i < TablaCompra.Rows.Count; i++)
            {
                CRegistroBien cRegistroDeUnBien = new CRegistroBien();
                cRegistroDeUnBien.IdCompra = Convert.ToInt32(TablaCompra.Rows[i]["IdCompra"]);

                CompraList.Add(cRegistroDeUnBien);
            }
            Compra = CompraList.FirstOrDefault().IdCompra.ToString();
            ////////

            SqlConnection con = new SqlConnection(CConexion.Obtener());
            con.Open();
            string sqlCad = "select MAX(NumLote + 1) from FichaBien";
            SqlCommand cmdMax = new SqlCommand(sqlCad, con);
            SqlDataReader leerMax = cmdMax.ExecuteReader();
            string max;
            if (leerMax.Read() == true)
            {
                max = leerMax[0].ToString();
                if (max == "")
                {
                    max = "1";
                }
                else
                {
                    max = leerMax[0].ToString();
                }
            }
            else
            {
                max = "";
            }
            con.Close();
            ////////////////////////////////////////
            string nombreBien = txtNombreBien.Text;
            SqlConnection con1 = new SqlConnection(CConexion.Obtener());
            con1.Open();
            string sqlCad2 = "select MIN(NumInventario) AS menor, MAX(NumInventario) AS mayor from FichaBien where DescripcionBien = @desc";
            SqlCommand cmdMax2 = new SqlCommand(sqlCad2, con1);
            cmdMax2.Parameters.Add("@desc", SqlDbType.VarChar, 150).Value = nombreBien;
            SqlDataReader leerMax2 = cmdMax2.ExecuteReader();
            string menor;
            string mayor;
            if (leerMax2.Read() == true)
            {
                menor = leerMax2[0].ToString();

                if (menor == "")
                {
                    menor = "0000";
                }
                else
                {
                    menor = leerMax2[0].ToString();
                }
                mayor = leerMax2[1].ToString();
                if (mayor == "")
                {
                    mayor = "0000";
                }
                else
                {
                    mayor = leerMax2[1].ToString();
                }
            }
            else
            {
                menor = "";
                mayor = "";
            }
            con1.Close();
            /////////////////////////////////////7

            string nombre = txtNombreBien.Text;
            int idCONAC = Convert.ToInt32(ddlCatCONAC.SelectedValue);
            int idSubClase = Convert.ToInt32(ddlSubClase.SelectedValue);
            string modelo = txtModelo.Text;
            string serie = txtSerie.Text;
            int marca = Convert.ToInt32(ddlMarca.SelectedValue);
            string detalle = txtDetalle.Text;
            string fechaAd = txtFechaAdquisicion.Text;
            string precio = txtPUnitario.Text;
            String pu = txtPUnitario.Text;
            String iva = labelIVA.Text;
            Decimal ValorConIVA = Convert.ToDecimal(pu, CultureInfo.CreateSpecificCulture("en-US")) + (Convert.ToDecimal(pu, CultureInfo.CreateSpecificCulture("en-US")) * Convert.ToDecimal(iva));
            Decimal numResult = Math.Truncate(Convert.ToDecimal(ValorConIVA) * 100) / 100;
            var formatInfo = new NumberFormatInfo
            {
                NumberDecimalSeparator = ".",
                NumberDecimalDigits = 2
            };
            Decimal result = Convert.ToDecimal(numResult, CultureInfo.CreateSpecificCulture("en-US"));
            LabelSubTotal1.Text = result.ToString("N2", new CultureInfo("en-US"));

            String descuento = txtDescuento.Text;
            Decimal ValorCostoT = (Convert.ToDecimal(result, CultureInfo.CreateSpecificCulture("en-US")) - Convert.ToDecimal(descuento, CultureInfo.CreateSpecificCulture("en-US")));
            Decimal numResult1 = Math.Truncate(Convert.ToDecimal(ValorCostoT) * 100) / 100;
            var formatInfo1 = new NumberFormatInfo
            {
                NumberDecimalSeparator = ".",
                NumberDecimalDigits = 2
            };
            Decimal result1 = Convert.ToDecimal(numResult1, CultureInfo.CreateSpecificCulture("en-US"));
            labelCostototal.Text = result1.ToString("N2", new CultureInfo("en-US"));

            string desc = txtDescuento.Text;
            string costoT = Convert.ToString(result1);
            string canti = TextBoxCantidadDeBienes.Text;
            if (cbxAjustarCosto.Checked == true)
            {
                if (txtAjustarCosto.Text != "")
                {
                    costoT = txtAjustarCosto.Text;
                }
            }

            for (int i = 1; i <= Convert.ToInt32(canti); i++)
            {
                con.Open();
                string sqlCad1 = " SELECT MAX(RIGHT(NumInventario, 4)) FROM FichaBien";
                SqlCommand cmdMax1 = new SqlCommand(sqlCad1, con);
                SqlDataReader leerMax1 = cmdMax1.ExecuteReader();
                string max1;
                if (leerMax1.Read() == true)
                {
                    max1 = leerMax1[0].ToString();
                    if (max1 == "")
                    {
                        max1 = "0001";
                    }
                    else
                    {
                        max1 = leerMax1[0].ToString();
                    }
                }
                else
                {
                    max1 = "";
                }
                con.Close();
                
                int c;
                string num = "";
                string consecutivo = "0000";
                for (c = Convert.ToInt32(max1); c <= Convert.ToInt32(max1) + 1; c++)
                {
                    if (c < 10)
                    { // Se agregan 3 ceros
                        consecutivo = "000" + c;
                    }
                    else if (c > 9 && c < 100)
                    { // Se agregan 2 ceros
                        consecutivo = "00" + c;
                    }
                    else if (c > 99 && c < 1000)
                    {// Se agrega un cero
                        consecutivo = "0" + c;
                    }
                    else
                    {
                        // No se agrega nada
                        consecutivo = c.ToString();
                    }

                    String SubClase = Convert.ToString(ddlSubClase.SelectedValue); /// 
                    String partida = Convert.ToString(ddlPartida.SelectedValue);
                    num = LabelGrupo.Text + LabelSubGrupo.Text + LabelClase.Text + SubClase + "81" + partida + consecutivo;
                }
                SqlConnection Conn = new SqlConnection(CConexion.Obtener());
                bool success = false;
                SqlTransaction lTransaccion = null;
                int Valor_Retornado = 0;
                try
                {
                    Conn.Open();
                    lTransaccion = Conn.BeginTransaction(System.Data.IsolationLevel.Serializable);
                    SqlCommand cmd = new SqlCommand("SP_Registro_PorLote", Conn, lTransaccion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Clear();
                    cmd.Parameters.Add(new SqlParameter("@NumLote", Convert.ToInt32(max)));
                    cmd.Parameters.Add(new SqlParameter("@NumInventario", Convert.ToInt64(num)));
                    cmd.Parameters.Add(new SqlParameter("@DescripcionBien", nombre));
                    cmd.Parameters.Add(new SqlParameter("@IdCONAC", idCONAC));
                    cmd.Parameters.Add(new SqlParameter("@IdSubClase", idSubClase));
                    cmd.Parameters.Add(new SqlParameter("@Modelo", modelo));
                    cmd.Parameters.Add(new SqlParameter("@Serie", serie));
                    cmd.Parameters.Add(new SqlParameter("@IdMarca", marca));
                    cmd.Parameters.Add(new SqlParameter("@Detalle", detalle));
                    cmd.Parameters.Add(new SqlParameter("@TipoPartida", tipo));
                    cmd.Parameters.Add(new SqlParameter("@IdENSU", ENSU));
                    cmd.Parameters.Add(new SqlParameter("@FechaCaptura", Convert.ToDateTime(fechaAd)));
                    cmd.Parameters.Add(new SqlParameter("@PrecioUnitario", Convert.ToDouble(precio)));
                    cmd.Parameters.Add(new SqlParameter("@SubTotal", Convert.ToDouble(result)));
                    cmd.Parameters.Add(new SqlParameter("@Descuento", Convert.ToDouble(desc)));
                    cmd.Parameters.Add(new SqlParameter("@CostoTotal", Convert.ToDouble(costoT)));
                    cmd.Parameters.Add(new SqlParameter("@IdCompra", Compra));
                    SqlParameter ValorRetorno = new SqlParameter("@Comprobacion", SqlDbType.Int);
                    ValorRetorno.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(ValorRetorno);
                    cmd.ExecuteNonQuery();
                    Valor_Retornado = Convert.ToInt32(ValorRetorno.Value);
                    if (Valor_Retornado == 1)
                        success = true;
                }
                catch (Exception ex)
                {
                    string script = ex.ToString();
                    MostrarMensaje("** Error al Registrar Bien por Lote**", "info", "Normal");
                }
                finally
                {
                    if (success)
                    {
                        lTransaccion.Commit();
                        Conn.Close();
                    }
                    else
                    {
                        lTransaccion.Rollback();
                        Conn.Close();
                    }
                }

                ////////////////////////////////////////////

                con.Open();
                string sqlCadid = "SELECT MAX (IdInventario) FROM FichaBien";
                SqlCommand cmdMaxid = new SqlCommand(sqlCadid, con);
                SqlDataReader leerMaxid = cmdMaxid.ExecuteReader();
                string maxid;
                if (leerMaxid.Read() == true)
                {
                    maxid = leerMaxid[0].ToString();
                }
                else
                {
                    maxid = "";
                }
                con.Close();

                int IdUsuario = 5;
                int IdAct = 1;


                try
                {
                    Conn.Open();
                    lTransaccion = Conn.BeginTransaction(System.Data.IsolationLevel.Serializable);
                    SqlCommand cmd = new SqlCommand("SP_Insertar_HistoricoUbicacion", Conn, lTransaccion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Clear();
                    cmd.Parameters.Add(new SqlParameter("@IdInventario", Convert.ToInt32(maxid)));
                    cmd.Parameters.Add(new SqlParameter("@IdENSU", ENSU));
                    cmd.Parameters.Add(new SqlParameter("@IdUsuario", IdUsuario));
                    cmd.Parameters.Add(new SqlParameter("@Fecha", Convert.ToDateTime(fechaAd)));
                    cmd.Parameters.Add(new SqlParameter("@IdActividad", IdAct));
                    SqlParameter ValorRetorno = new SqlParameter("@Comprobacion", SqlDbType.Int);
                    ValorRetorno.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(ValorRetorno);
                    cmd.ExecuteNonQuery();
                    Valor_Retornado = Convert.ToInt32(ValorRetorno.Value);
                    if (Valor_Retornado == 1)
                        success = true;
                }
                catch (Exception ex)
                {
                    string script = ex.ToString();

                    MostrarMensaje("** Error al insertar Serie **", "info", "Normal");
                }
                finally
                {
                    if (success)
                    {
                        lTransaccion.Commit();
                        Conn.Close();
                    }
                    else
                    {
                        lTransaccion.Rollback();
                        Conn.Close();
                    }
                }



                if (div8.Visible == true)
                {
                    if (TextBoxFrente.Text != "")
                    {
                        if (TextBoxFondo.Text != "")
                        {
                            if (TextBoxAltura.Text != "")
                            {
                                if (TextBoxDiametro.Text != "")
                                {
                                    InsertarDimencionBien();
                                }
                                else
                                {
                                    MostrarMensaje("** Diametro requerida **", "info", "Normal");
                                }
                            }
                            else
                            {
                                MostrarMensaje("** Altura requerido **", "info", "Normal");
                            }
                        }
                        else
                        {
                            MostrarMensaje("** Fondo requerida **", "info", "Normal");
                        }
                    }
                    else
                    {
                        MostrarMensaje("** Frente requerido **", "info", "Normal");
                    }
                }

                String Propiedad = Convert.ToString(ddlPropiedad.SelectedItem);

                String descuento1 = txtDescuento.Text;
                Decimal ValorCostoT1 = (Convert.ToDecimal(result, CultureInfo.CreateSpecificCulture("en-US")) - 
                    Convert.ToDecimal(descuento1, CultureInfo.CreateSpecificCulture("en-US")));
                Decimal numResult11 = Math.Truncate(Convert.ToDecimal(ValorCostoT1) * 100) / 100;
                
                Decimal result11 = Convert.ToDecimal(numResult11, CultureInfo.CreateSpecificCulture("en-US"));
                labelCostototal.Text = result1.ToString("N2", new CultureInfo("en-US"));


                if (cbxAjustarCosto.Checked == true)
                {
                    if (txtAjustarCosto.Text != "")
                    {
                        Decimal ajustar = Convert.ToDecimal(txtAjustarCosto.Text, CultureInfo.CreateSpecificCulture("en-US"));
                        labelCostototal.Text = ajustar.ToString("N", new CultureInfo("en-US"));
                    }
                }

                int id = Convert.ToInt32(ddlPartida.SelectedValue);
                con.Open();
                string cadSql = "select NumPartida from Cat_Partidas where idPartida = @id";
                SqlCommand cmd1 = new SqlCommand(cadSql, con);
                cmd1.Parameters.Add("@id", SqlDbType.Int).Value = id;
                SqlDataReader leer = cmd1.ExecuteReader();
                string tipoP;
                if (leer.Read() == true)
                {
                    tipoP = leer[0].ToString();
                }
                else
                {
                    tipoP = "";
                }
                con.Close();

                if (Convert.ToDouble(result11) >= 5914.3 && Propiedad == "PODER JUDICIAL DEL ESTADO DE HIDALGO" && Convert.ToInt32(tipoP) >= 5000)
                {
                    if (div9.Visible == true)
                    {
                        if (txtVidaUtil.Text != "")
                        {
                            insertarEnDepreciar();
                        }
                        else
                        {
                            MostrarMensaje("** Vida Util requerida **", "info", "Normal");
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Método para insertar en la tabla Depreciar 
        /// </summary>
        public void insertarEnDepreciar()
        {
            
            SqlConnection con = new SqlConnection(CConexion.Obtener());
            ///////////////////////////////////////////////////////////
            con.Open();
            string cadSql1 = "select MAX(idInventario) from FichaBien";
            SqlCommand cmd11 = new SqlCommand(cadSql1, con);
            SqlDataReader leer1 = cmd11.ExecuteReader();
            string idInventario1;

            if (leer1.Read() == true)
            {
                idInventario1 = leer1[0].ToString();
            }
            else
            {
                idInventario1 = "";
            }
            con.Close();
            ///////////////////////////////////////////////////////////////
            con.Open();
            string cadSql = "select idInventario, NumInventario, IdCONAC from FichaBien where IdInventario = @idInventario";
            SqlCommand cmd1 = new SqlCommand(cadSql, con);
            cmd1.Parameters.AddWithValue("@idInventario", idInventario1);
            SqlDataReader leer = cmd1.ExecuteReader();
            string idInven;
            string NumInventario;
            string idConac;

            if (leer.Read() == true)
            {
                idInven = leer[0].ToString();
                NumInventario = leer[1].ToString();
                idConac = leer[2].ToString();
            }
            else
            {
                idInven = "";
                NumInventario = "";
                idConac = "";
            }
            con.Close();
            
            String subClase = Convert.ToString(ddlSubClase.SelectedValue);
            String Partida = Convert.ToString(ddlPartida.SelectedValue);
            string num = LabelGrupo.Text + LabelSubGrupo.Text + LabelClase.Text + subClase + "81" + Partida;

            string vida = txtVidaUtil.Text;

            String pu = txtPUnitario.Text;
            String iva = labelIVA.Text;
            Decimal ValorConIVA = Convert.ToDecimal(pu, CultureInfo.CreateSpecificCulture("en-US")) + (Convert.ToDecimal(pu, CultureInfo.CreateSpecificCulture("en-US")) * Convert.ToDecimal(iva));
            Decimal numResult = Math.Truncate(Convert.ToDecimal(ValorConIVA) * 100) / 100;
            Decimal result = Convert.ToDecimal(numResult, CultureInfo.CreateSpecificCulture("en-US"));

            String descuento = txtDescuento.Text;
            Decimal ValorCostoT = (Convert.ToDecimal(result, CultureInfo.CreateSpecificCulture("en-US")) - Convert.ToDecimal(descuento, CultureInfo.CreateSpecificCulture("en-US")));
            Decimal numResult1 = Math.Truncate(Convert.ToDecimal(ValorCostoT) * 100) / 100;
            Decimal result1 = Convert.ToDecimal(numResult1, CultureInfo.CreateSpecificCulture("en-US"));


            string montoR = result1.ToString();
            string idPartida = ddlPartida.SelectedIndex.ToString();
            int estatus = 0;
            DateTime Fecha = Convert.ToDateTime(txtFechaAdquisicion.Text);
            Fecha = Fecha.AddDays(-1);
            DateTime FechaFinal = Fecha.AddYears(Convert.ToInt32(vida));

            if (cbxAjustarCosto.Checked == true)
            {
                if (txtAjustarCosto.Text != "")
                {
                    montoR = txtAjustarCosto.Text;
                }
            }
            Decimal resMontoR = (Convert.ToDecimal(montoR, CultureInfo.CreateSpecificCulture("en-US")) - 1);
            Decimal resMonto = (Convert.ToDecimal(montoR, CultureInfo.CreateSpecificCulture("en-US")));
            SqlConnection Conn = new SqlConnection(CConexion.Obtener());
                bool success = false;
                SqlTransaction lTransaccion = null;
                int Valor_Retornado = 0;
                try
                {
                    Conn.Open();
                    lTransaccion = Conn.BeginTransaction(System.Data.IsolationLevel.Serializable);
                    SqlCommand cmd = new SqlCommand("SP_Depreciar", Conn, lTransaccion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Clear();
                    cmd.Parameters.Add(new SqlParameter("@IdInventario", Convert.ToInt32(idInven)));
                    cmd.Parameters.Add(new SqlParameter("@NumInventario", Convert.ToInt64(NumInventario)));
                    cmd.Parameters.Add(new SqlParameter("@VidaUtil", vida));
                    cmd.Parameters.Add(new SqlParameter("@MontoDepreciar", Convert.ToInt32(resMontoR)));
                    cmd.Parameters.Add(new SqlParameter("@MontoReal", Convert.ToInt32(resMonto)));
                    cmd.Parameters.Add(new SqlParameter("@IdCONAC", Convert.ToInt32(idConac)));
                    cmd.Parameters.Add(new SqlParameter("@IdPartida", idPartida));
                    cmd.Parameters.Add(new SqlParameter("@FechaInicio", Fecha));
                    cmd.Parameters.Add(new SqlParameter("@FechaTermino", FechaFinal));
                    cmd.Parameters.Add(new SqlParameter("@Estatus", estatus));
                    SqlParameter ValorRetorno = new SqlParameter("@Comprobacion", SqlDbType.Int);
                    ValorRetorno.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(ValorRetorno);
                    cmd.ExecuteNonQuery();
                    Valor_Retornado = Convert.ToInt32(ValorRetorno.Value);
                    if (Valor_Retornado == 1)
                        success = true;
                }
                catch (Exception ex)
                {
                    string script = ex.ToString();
                    MostrarMensaje("** Error al Registrar en Depreciar**", "info", "Normal");
                }
                finally
                {
                    if (success)
                    {
                        lTransaccion.Commit();
                        Conn.Close();
                    }
                    else
                    {
                        lTransaccion.Rollback();
                        Conn.Close();
                    }
                }
        }

        /// <summary>
        /// Método para mostrar los números de inventario que se 
        /// registrarron por lotes. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void MostrarNumInventario(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(CConexion.Obtener());
            con.Open();
            string sqlCad = "select MAX(NumLote) from FichaBien";
            SqlCommand cmdMax = new SqlCommand(sqlCad, con);
            SqlDataReader leerMax = cmdMax.ExecuteReader();
            string max;
            if (leerMax.Read() == true)
            {
                max = leerMax[0].ToString();
                if (max == "")
                {
                    max = "1";
                }
                else
                {
                    max = leerMax[0].ToString();
                }
            }
            else
            {
                max = "";
            }
            con.Close();
            ////////////////////////////////////////
            string nombreBien = txtNombreBien.Text;
            SqlConnection con1 = new SqlConnection(CConexion.Obtener());
            con1.Open();
            string sqlCad2 = "select MIN(NumInventario) AS menor, MAX(NumInventario) AS mayor from FichaBien where DescripcionBien = @desc";
            SqlCommand cmdMax2 = new SqlCommand(sqlCad2, con1);
            cmdMax2.Parameters.Add("@desc", SqlDbType.VarChar, 150).Value = nombreBien;
            SqlDataReader leerMax2 = cmdMax2.ExecuteReader();
            string menor;
            string mayor;
            if (leerMax2.Read() == true)
            {
                menor = leerMax2[0].ToString();

                if (menor == "")
                {
                    menor = "0000";
                }
                else
                {
                    menor = leerMax2[0].ToString();
                }
                mayor = leerMax2[1].ToString();
                if (mayor == "")
                {
                    mayor = "0000";
                }
                else
                {
                    mayor = leerMax2[1].ToString();
                }
            }
            else
            {
                menor = "";
                mayor = "";
            }
            con1.Close();
            /////////////////////////////////////
            Bien.Visible = false;
            Factura.Visible = true;
            Cancel_Click(sender, e);
            MostrarNumero("Registro exitoso, Lote: &nbsp;" + max + "&nbsp; Número de Inventario:&nbsp; " + menor + "&nbsp; -&nbsp; " + mayor);
        }

        /// <summary>
        /// Método para mostrar el número de inventario que se registro 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void MostrarNumInventarioIndividual(object sender, EventArgs e)
        {
            ////////////////////////////////////////
            string nombreBien = txtNombreBien.Text;
            SqlConnection con1 = new SqlConnection(CConexion.Obtener());
            con1.Open();
            string sqlCad2 = " SELECT TOP(1) NumInventario FROM FichaBien ORDER BY IdInventario DESC";
            SqlCommand cmdMax2 = new SqlCommand(sqlCad2, con1);
            cmdMax2.Parameters.Add("@desc", SqlDbType.VarChar, 150).Value = nombreBien;
            SqlDataReader leerMax2 = cmdMax2.ExecuteReader();
            string numero;
            if (leerMax2.Read() == true)
            {
                numero = leerMax2[0].ToString();

                if (numero == "")
                {
                    numero = "0000";
                }
                else
                {
                    numero = leerMax2[0].ToString();
                }
            }
            else
            {
                numero = "";
            }
            con1.Close();
            /////////////////////////////////////7
            Bien.Visible = false;
            Factura.Visible = true;
            Cancel_Click(sender, e);
            MostrarNumero("Registro individual exitoso, &nbsp;" + " &nbsp; Número de Inventario: &nbsp;" + numero);
        }

    }
}