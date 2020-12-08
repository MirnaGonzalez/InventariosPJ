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
using System.Globalization;

namespace InventariosPJEH
{
    public partial class frmModificacionDeUnBien : System.Web.UI.Page
    {
        public SqlConnection conexion;

        public frmModificacionDeUnBien()
        {
            this.conexion = ConexionBD.getConexion();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                IniciarLlenadoDropDownListAreaDeResguardoInicial();
                IniciarLlenadoDropDownListPartida();
                IniciarLlenadoDropDownListSubClase();
                IniciarLlenadoddlCatCONAC();
                IniciarLlenadoDropDownListMarca();
            }
        }

        /// <summary>
        /// Método para mostrar una alerta al usuario 
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
        /// Método para buscar los bienes y llenar el grid con los datos requeridos. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void BtnBuscarBien_Click(object sender, EventArgs e)
        {
            //Capturamos los errores con try catch
            try
            {
                div1.Visible = true;
                divGrid.Visible = true;
                DataTable Datos = new DataTable();
                Datos = BdModificacionBien.BuscarGrid(txtNumDoc, txtNumLote, txtNumInv);
                GridModificar.DataSource = Datos;
                GridModificar.DataBind();
                BotonLimpiar.Visible = true;
                //Buscar por número de Lote//
                DataTable DatosLote = new DataTable();
                DatosLote = BdModificacionBien.BuscarNumLote(txtNumDoc, txtNumLote, txtNumInv);

                String cadena = DatosLote.Rows[0]["NumLote"].ToString();
                if ( cadena == "")
                {
                    LblNumLote.Visible = false;
                    LblNumLote1.Visible = false;
                }
                else
                {
                    string lote;
                    List<CModificacionBien> LoteList = new List<CModificacionBien>();
                    for (int i = 0; i < DatosLote.Rows.Count; i++)
                    {
                        CModificacionBien cModificacionBien = new CModificacionBien();
                        cModificacionBien.NumLote = Convert.ToInt32(DatosLote.Rows[i]["NumLote"]);

                        LoteList.Add(cModificacionBien);
                    }
                    lote = LoteList.FirstOrDefault().NumLote.ToString();
                    LblNumLote.Visible = true;
                    LblNumLote1.Visible = true;
                    LblNumLote1.Text = lote;
                }

                /////////////////////////////////////////////////////

                DataTable DatosDoc = new DataTable();
                DatosDoc = BdModificacionBien.BuscarNumDoc(txtNumDoc, txtNumLote, txtNumInv);
                if (DatosDoc.Rows.Count == 1)
                {
                    string Ndoc;
                    List<CModificacionBien> DocList = new List<CModificacionBien>();
                    for (int i = 0; i < DatosDoc.Rows.Count; i++)
                    {
                        CModificacionBien cModificacionBien = new CModificacionBien();
                        cModificacionBien.NumDocumento = Convert.ToString(DatosDoc.Rows[i]["NumDocumento"]);

                        DocList.Add(cModificacionBien);
                    }
                    Ndoc = DocList.FirstOrDefault().NumDocumento.ToString();
                    LblNumDoc.Visible = true;
                    LblNumDoc1.Visible = true;
                    LblNumDoc1.Text = Ndoc;
                }
                else
                if (DatosDoc.Rows.Count == 0)
                {
                    LblNumDoc.Visible = false;
                    LblNumDoc1.Visible = false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Método para el botón editar, el cual muestra los campos a editar. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnEditar_Click(object sender, ImageClickEventArgs e)
        {
            DivFiltrosModificacion.Visible = false;
            Modificacion.Visible = true;
            if (LblNumLote1.Visible == true)
            {
                divConcatena.Visible = true;
                div2.Visible = true;
                div4.Visible = true;
                div5.Visible = true;
                div8.Visible = false;
                Botones.Visible = true;
                InfoGeneral.Visible = true;
                MontoOriginalDeLaInversion.Visible = true;
            } else
            {
                div2.Visible = true;
                div4.Visible = true;
                div5.Visible = true;
                divConcatena.Visible = true;
                Botones.Visible = true;
                InfoGeneral.Visible = true;
                MontoOriginalDeLaInversion.Visible = true;
            }
        }

        /// <summary>
        /// Método RowComand del Grid, en el cual se mandan a llamar diversas 
        /// consultas para llenar algunos campos (Editar). 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void GridModificar_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            GridViewRow RowSelecionada = GridModificar.Rows[index];
            string Bien = Page.Server.HtmlDecode(RowSelecionada.Cells[0].Text);
            Lblbien.Text = Bien;
            ///////////////////////////////////////////////////////////////////
            DataTable BuscarFactura = new DataTable();
            BuscarFactura = BdModificacionBien.BuscarFactura(Lblbien);

            List<CModificacionBien> FacturaList = new List<CModificacionBien>();
            for (int i = 0; i < BuscarFactura.Rows.Count; i++)
            {
                CModificacionBien cFactura = new CModificacionBien();
                cFactura.NumDocumento = Convert.ToString(BuscarFactura.Rows[i]["NumDocumento"]);
                cFactura.FechaAdquisicion = Convert.ToString(BuscarFactura.Rows[i]["FechaAdquisicion"]);
                FacturaList.Add(cFactura);
            }
            /////////////////////////////////////////////////////////////////////
            DataTable BuscarProveedor = new DataTable();
            BuscarProveedor = BdModificacionBien.BuscarProveedor(Lblbien);

            List<CModificacionBien> ProveedorList = new List<CModificacionBien>();
            for (int i = 0; i < BuscarProveedor.Rows.Count; i++)
            {
                CModificacionBien cFactura = new CModificacionBien();
                cFactura.Proveedor = Convert.ToString(BuscarProveedor.Rows[i]["Proveedor"]);
                ProveedorList.Add(cFactura);
            }
            /////////////////////////////////////////////////////////////////////
            DataTable BuscarTipoDoc = new DataTable();
            BuscarTipoDoc = BdModificacionBien.BuscarDocumento(Lblbien);

            List<CModificacionBien> TipoDocList = new List<CModificacionBien>();
            for (int i = 0; i < BuscarTipoDoc.Rows.Count; i++)
            {
                CModificacionBien cFactura = new CModificacionBien();
                cFactura.Documento = Convert.ToString(BuscarTipoDoc.Rows[i]["Documento"]);
                TipoDocList.Add(cFactura);
            }
            /////////////////////////////////////////////////////////////////////
            DataTable BuscarTipoAdqui = new DataTable();
            BuscarTipoAdqui = BdModificacionBien.BuscarTipoAdqui(Lblbien);

            List<CModificacionBien> TipoAdquiList = new List<CModificacionBien>();
            for (int i = 0; i < BuscarTipoAdqui.Rows.Count; i++)
            {
                CModificacionBien cFactura = new CModificacionBien();
                cFactura.TipoAdqui = Convert.ToString(BuscarTipoAdqui.Rows[i]["TipoAdqui"]);
                TipoAdquiList.Add(cFactura);
            }
            /////////////////////////////////////////////////////////////////////
            DataTable BuscarPropiedad = new DataTable();
            BuscarPropiedad = BdModificacionBien.BuscarPropiedad(Lblbien);

            List<CModificacionBien> PropiedadList = new List<CModificacionBien>();
            for (int i = 0; i < BuscarPropiedad.Rows.Count; i++)
            {
                CModificacionBien cFactura = new CModificacionBien();
                cFactura.Propiedad = Convert.ToString(BuscarPropiedad.Rows[i]["Propiedad"]);
                PropiedadList.Add(cFactura);
            }
            /////////////////////////////////////////////////////////////////////
            string proveedor = ProveedorList.FirstOrDefault().Proveedor.ToString();
            string tipoDoc = TipoDocList.FirstOrDefault().Documento.ToString();
            string NoDoc = FacturaList.FirstOrDefault().NumDocumento.ToString();
            string tipoA = TipoAdquiList.FirstOrDefault().TipoAdqui.ToString();
            string Propiedad = PropiedadList.FirstOrDefault().Propiedad.ToString();
            string fecha = FacturaList.FirstOrDefault().FechaAdquisicion.ToString();
            LblConcatena.Text = proveedor + "&nbsp;|&nbsp;" + tipoDoc + "&nbsp;" + NoDoc + " &nbsp;|&nbsp; " + tipoA + "," + Propiedad + "&nbsp;|&nbsp;" + fecha;
            //////////////////////////////////////////////////////////////////////

            DataTable BuscarSubClase = new DataTable();
            BuscarSubClase = BdModificacionBien.BuscarSubClase(Lblbien);

            List<CModificacionBien> SubClaseList = new List<CModificacionBien>();
            for (int i = 0; i < BuscarSubClase.Rows.Count; i++)
            {
                CModificacionBien cFactura = new CModificacionBien();
                cFactura.SubClase = Convert.ToString(BuscarSubClase.Rows[i]["SubClase"]);
                SubClaseList.Add(cFactura);
            }
            string subClase = SubClaseList.FirstOrDefault().SubClase.ToString();
            ddlSubClase.SelectedIndex = ddlSubClase.Items.IndexOf(ddlSubClase.Items.FindByText(subClase));

            /////////////////////////////////////////////////////////////////////////////////
            // ********BUSCAR DESCRIPCIÓN  CONAC **************
            DataTable BuscarCONAC = new DataTable();
            BuscarCONAC = BdModificacionBien.BuscarCONAC(Lblbien);

            List<CModificacionBien> CONACList = new List<CModificacionBien>();

            for (int i = 0; i < BuscarCONAC.Rows.Count; i++)
            {
                CModificacionBien cFactura = new CModificacionBien();
                cFactura.Descripcion = Convert.ToString(BuscarCONAC.Rows[i]["Descripcion"]);
                CONACList.Add(cFactura);
            }
            string CONAC = CONACList.FirstOrDefault().Descripcion.ToString();
            
            ddlCatCONAC.SelectedIndex = ddlCatCONAC.Items.IndexOf(ddlCatCONAC.Items.FindByText(CONAC));


            //BUSCAR ID CONAC //
            DataTable BuscarIdCONAC = new DataTable();
            BuscarIdCONAC = BdModificacionBien.BuscarIdCONAC(Lblbien);
            List<CModificacionBien> IdCONACList = new List<CModificacionBien>();

            for (int i = 0; i < BuscarIdCONAC.Rows.Count; i++)
            {
                CModificacionBien cFactura = new CModificacionBien();
                cFactura.IdCONAC = Convert.ToInt32(BuscarIdCONAC.Rows[i]["idCONAC"]);
                IdCONACList.Add(cFactura);
            }

            string IdCONAC = IdCONACList.FirstOrDefault().IdCONAC.ToString();
            LlenarClaveCONAC(IdCONAC);

            LblGrupo1.Visible = true;
            LblSubGrupo1.Visible = true;
            LblClase.Visible = true;


            //AREA DE RESGUARDO INICIAL//
            DataTable BuscarENSU = new DataTable();
            BuscarENSU = BdModificacionBien.BuscarAreaResguardo(Lblbien);
            List<CModificacionBien> ENSUList = new List<CModificacionBien>();

            for (int i = 0; i < BuscarENSU.Rows.Count; i++)
            {
                CModificacionBien cFactura = new CModificacionBien();
                cFactura.Nombre = Convert.ToString(BuscarENSU.Rows[i]["Nombre"]);
                ENSUList.Add(cFactura);
            }
            string Area = ENSUList.FirstOrDefault().Nombre.ToString();
            ddlResguardoInicial.SelectedIndex = ddlResguardoInicial.Items.IndexOf(ddlResguardoInicial.Items.FindByText(Area));


            

            /////////////////////////////////////////////////////////////////////////////////
            DataTable BuscarMarca = new DataTable();
            BuscarMarca = BdModificacionBien.BuscarMarca(Lblbien);

            List<CModificacionBien> MarcaList = new List<CModificacionBien>();
            for (int i = 0; i < BuscarMarca.Rows.Count; i++)
            {
                CModificacionBien cFactura = new CModificacionBien();
                cFactura.Descripcion = Convert.ToString(BuscarMarca.Rows[i]["Descripcion"]);
                MarcaList.Add(cFactura);
            }
            string Marca = MarcaList.FirstOrDefault().Descripcion.ToString();
            ddlMarca.SelectedIndex = ddlMarca.Items.IndexOf(ddlMarca.Items.FindByText(Marca));







            ////////////////////////////////////////////////////////////////////////////////
            DataTable BuscarNombreBien = new DataTable();
            BuscarNombreBien = BdModificacionBien.BuscarBien(Lblbien);

            List<CModificacionBien> NombreList = new List<CModificacionBien>();
            for (int i = 0; i < BuscarNombreBien.Rows.Count; i++)
            {
                CModificacionBien cFactura = new CModificacionBien();
                cFactura.DescripcionBien = Convert.ToString(BuscarNombreBien.Rows[i]["DescripcionBien"]);
                cFactura.Modelo = Convert.ToString(BuscarNombreBien.Rows[i]["Modelo"]);
                cFactura.Serie = Convert.ToString(BuscarNombreBien.Rows[i]["Serie"]);
                cFactura.Detalle = Convert.ToString(BuscarNombreBien.Rows[i]["Detalle"]);
                cFactura.PrecioUnitario = Convert.ToDouble(BuscarNombreBien.Rows[i]["PrecioUnitario"]);
                cFactura.SubTotal = Convert.ToDouble(BuscarNombreBien.Rows[i]["SubTotal"]);
                cFactura.Descuento = Convert.ToDouble(BuscarNombreBien.Rows[i]["Descuento"]);
                cFactura.CostoTotal = Convert.ToDouble(BuscarNombreBien.Rows[i]["CostoTotal"]);

                NombreList.Add(cFactura);
            }
            string Nombre = NombreList.FirstOrDefault().DescripcionBien.ToString();


            txtNombreDelBien.Text = Nombre;
            string Modelo = NombreList.FirstOrDefault().Modelo.ToString();
            txtModelo.Text = Modelo;
            string Serie = NombreList.FirstOrDefault().Serie.ToString();
            txtSerie.Text = Serie;
            string Detalle = NombreList.FirstOrDefault().Detalle.ToString();
            txtDetalle.Text = Detalle;
            string Precio = NombreList.FirstOrDefault().PrecioUnitario.ToString();
            txtPUnitario.Text = Precio;
            string Subtotal = NombreList.FirstOrDefault().SubTotal.ToString();


            if (Convert.ToDouble(Subtotal) > Convert.ToDouble(Precio))
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

                checkboxIVA.Checked = true;
                labelIVA.Visible = true;
                LabelSubTotal1.Text = Subtotal;
            }
            else
            {
                LabelSubTotal1.Text = Subtotal;
            }
            string Descuento = NombreList.FirstOrDefault().Descuento.ToString();
            if (Descuento == "0")
            {
                checkboxDescuento.Checked = false;
                txtDescuento.Visible = false;
                txtDescuento.Text = "0";
            }
            else
            {
                checkboxDescuento.Checked = true;
                txtDescuento.Visible = true;
                txtDescuento.Text = Descuento;
            }
            string CostoT = NombreList.FirstOrDefault().CostoTotal.ToString();
            labelCostototal.Text = CostoT;
            /////////////////////////////////////////////////////////////////////////////////
            DataTable BuscarPartida = new DataTable();
            BuscarPartida = BdModificacionBien.BuscarPartida(Lblbien);

            List<CModificacionBien> PartidaList = new List<CModificacionBien>();
            for (int i = 0; i < BuscarPartida.Rows.Count; i++)
            {
                CModificacionBien cFactura = new CModificacionBien();
                cFactura.Partida = Convert.ToString(BuscarPartida.Rows[i]["Partida"]);
                PartidaList.Add(cFactura);
            }
            string Partida = PartidaList.FirstOrDefault().Partida.ToString();
            ddlPartida.SelectedIndex = ddlPartida.Items.IndexOf(ddlPartida.Items.FindByText(Partida));
            ///////////////////////////////////////////////////////////////////////////////////
            if (!existeBienEnDepreciar(this.Lblbien.Text))
            {
                div9.Visible = false;

            }
            else
            {
                div9.Visible = true;
                DataTable InfoDepreciar = new DataTable();
                InfoDepreciar = BdModificacionBien.InfoDepreciar(Lblbien);

                List<CModificacionBien> VidaUtilList = new List<CModificacionBien>();
                for (int i = 0; i < InfoDepreciar.Rows.Count; i++)
                {
                    CModificacionBien cFactura = new CModificacionBien();
                    cFactura.VidaUtil = Convert.ToInt32(InfoDepreciar.Rows[i]["VidaUtil"]);
                    VidaUtilList.Add(cFactura);
                }
                string VidaUtil = VidaUtilList.FirstOrDefault().VidaUtil.ToString();
                txtVidautil.Text = VidaUtil;
            }
                ////////////////////////////////////////////////////////////////////////////////////
                if (!existeBienEnDimencion(this.Lblbien.Text))
                {
                    div8.Visible = false;

                }
                else
                {
                    div8.Visible = true;
                    DataTable InfoDimencion = new DataTable();
                    InfoDimencion = BdModificacionBien.InfoDimencion(Lblbien);

                    List<CModificacionBien> DimencionList = new List<CModificacionBien>();
                    for (int i = 0; i < InfoDimencion.Rows.Count; i++)
                    {
                        CModificacionBien cFactura = new CModificacionBien();
                        cFactura.Frente = Convert.ToDouble(InfoDimencion.Rows[i]["Frente"]);
                        cFactura.Fondo = Convert.ToDouble(InfoDimencion.Rows[i]["Fondo"]);
                        cFactura.Altura = Convert.ToDouble(InfoDimencion.Rows[i]["Altura"]);
                        cFactura.Diametro = Convert.ToDouble(InfoDimencion.Rows[i]["Diametro"]);
                        DimencionList.Add(cFactura);
                    }
                    string frente = DimencionList.FirstOrDefault().Frente.ToString();
                    string fondo = DimencionList.FirstOrDefault().Fondo.ToString();
                    string altura = DimencionList.FirstOrDefault().Altura.ToString();
                    string diametro = DimencionList.FirstOrDefault().Diametro.ToString();
                    txtFrente.Text = frente;
                    txtFondo.Text = fondo;
                    txtAltura.Text = altura;
                    txtDiametro.Text = diametro;
                }
        }

        /// <summary>
        /// Método para la obtención del area de resguardo inicial 
        /// </summary>
        private void IniciarLlenadoDropDownListAreaDeResguardoInicial()
        {
            List<CModificacionBien> listaAreaResguardo = new List<CModificacionBien>();
            listaAreaResguardo = BdModificacionBien.ObtenerAreaDeResguardoInicial();
            ddlResguardoInicial.DataSource = listaAreaResguardo;
            ddlResguardoInicial.DataTextField = "Nombre";
            ddlResguardoInicial.DataValueField = "IdSeccion";
            ddlResguardoInicial.DataBind();
            ddlResguardoInicial.Items.Insert(0, new ListItem("Seleccionar", "0"));
        }

        /// <summary>
        /// Método para llenar el Drop de Partidas mediante una consulta a la BD. 
        /// </summary>
        private void IniciarLlenadoDropDownListPartida()
        {
            CUsuario Usuario = Page.Session["Usuario"] as CUsuario;
            string TipoPartida = Usuario.TipoPartida;

            List<CModificacionBien> listaPartida = new List<CModificacionBien>();
            listaPartida = BdModificacionBien.ObtenerPartida(TipoPartida);
            ddlPartida.DataSource = listaPartida;
            ddlPartida.DataTextField = "Partida";
            ddlPartida.DataValueField = "idPartida";
            ddlPartida.DataBind();
            ddlPartida.Items.Insert(0, new ListItem("Seleccionar", "0"));
        }

        /// <summary>
        /// Método para llenar el Drop de SubClases mediante una consulta a la BD. 
        /// </summary>
        private void IniciarLlenadoDropDownListSubClase()
        {
            CUsuario Usuario = Page.Session["Usuario"] as CUsuario;
            string TipoPartida = Usuario.TipoPartida;

            List<CModificacionBien> listaSubClase = new List<CModificacionBien>();
            listaSubClase = BdModificacionBien.ObtenerSubClase(TipoPartida);
            ddlSubClase.DataSource = listaSubClase;
            ddlSubClase.DataTextField = "SubClase";
            ddlSubClase.DataValueField = "idSubClase";
            ddlSubClase.DataBind();
            ddlSubClase.Items.Insert(0, new ListItem("Seleccionar", "0"));
        }

        /// <summary>
        /// Método para llenar el Drop de CONAC mediante una consulta a la BD. 
        /// </summary>
        private void IniciarLlenadoddlCatCONAC()
        {
            CUsuario Usuario = Page.Session["Usuario"] as CUsuario;
            string TipoPartida = Usuario.TipoPartida;

            List<CModificacionBien> listaCONAC = new List<CModificacionBien>();
            listaCONAC = BdModificacionBien.ObtenerCONAC(TipoPartida);
            ddlCatCONAC.DataSource = listaCONAC;
            ddlCatCONAC.DataTextField = "Descripcion";
            ddlCatCONAC.DataValueField = "IdCONAC";

         

            ddlCatCONAC.DataBind();
            ddlCatCONAC.Items.Insert(0, new ListItem("Seleccionar", "0"));
        }

        /// <summary>
        /// Método para llenar el Drop de Marcas mediante una consulta a la BD. 
        /// </summary>
        private void IniciarLlenadoDropDownListMarca()
        {
            List<CModificacionBien> listaMarca = new List<CModificacionBien>();
            listaMarca = BdModificacionBien.ObtenerMarca();
            ddlMarca.DataSource = listaMarca;
            ddlMarca.DataTextField = "Descripcion";
            ddlMarca.DataValueField = "IdMarca";
            ddlMarca.DataBind();
            ddlMarca.Items.Insert(0, new ListItem("Seleccionar", "0"));
        }

        /// <summary>
        /// Método para vaidar si la partida que se selecciono es de tipo 
        /// Bienes Muebles, para así mostrar los campos a llenar para insertar
        /// en la tabla Dimensión Bien. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void DropDownListPartida_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList DropPartida = sender as DropDownList;
            string p = DropPartida.SelectedItem.ToString();
            if (p == "BIENES MUEBLES                                                                                                                                                                                                                                                 ")
            {
                div8.Visible = true;
            }
            else
            {
                div8.Visible = false;
            }
        }

        /// <summary>
        /// Método para obtener la clave CONAC. 
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
        /// Método para realizar los alculos correspondientes al ingresar el precio unitario 
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
            Decimal numResult = Math.Truncate(Convert.ToDecimal(ValorConIVA) * 100) / 100;

            Decimal result = Convert.ToDecimal(numResult, CultureInfo.CreateSpecificCulture("en-US"));
            LabelSubTotal1.Text = result.ToString("N2", new CultureInfo("en-US"));

            DataTable BuscarPropiedad = new DataTable();
            BuscarPropiedad = BdModificacionBien.BuscarPropiedad(Lblbien);

            List<CModificacionBien> PropiedadList = new List<CModificacionBien>();
            for (int i = 0; i < BuscarPropiedad.Rows.Count; i++)
            {
                CModificacionBien cFactura = new CModificacionBien();
                cFactura.Propiedad = Convert.ToString(BuscarPropiedad.Rows[i]["Propiedad"]);
                PropiedadList.Add(cFactura);
            }

            String Propiedad = PropiedadList.FirstOrDefault().Propiedad.ToString();

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
        /// Método para validar si el check de descuento esta seleccionado
        /// y así poder realizar los calculos correspondientes al precio unitario. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void checkboxDescuento_CheckedChanged(object sender, EventArgs e)
        {
            if (checkboxDescuento.Checked == true)
            {
                txtDescuento.Visible = true;
            }
            else
            {
                labelCostototal.Text = "0";
                txtDescuento.Visible = false;
                TextBoxPrecioUnitario_TextChanged(sender, e);
            }
        }
 
        /// <summary>
        /// Método para validar si se ingreso una cantidad al campo de descuento,
        /// para realizar los calcullos correspondientes.  
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void TextBoxDescuento_TextChanged(object sender, EventArgs e)
        {
            labelCostototal.Text = "";
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
        }

        /// <summary>
        /// Método para validar si el check de IVA esta seleccionado y así 
        /// realizar los calcuos correspondientes. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void checkboxIVA_CheckedChanged(object sender, EventArgs e)
        {
            if (checkboxIVA.Checked == true)
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
            else
            {
                labelIVA.Text = "0";
                labelIVA.Visible = false;
                TextBoxPrecioUnitario_TextChanged(sender, e);
            }

        }

        /// <summary>
        /// Método para validar si el check de Ajustra costo esta seleccionado
        /// para reemplazar el costo ingresado. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void cbxAjustarCosto_CheckedChanged(object sender, EventArgs e)
        {
            if (cbxAjustarCosto.Checked == true)
            {
                txtAjustarCosto.Visible = true;
            }
            else
            {
                txtAjustarCosto.Text = "";
                txtAjustarCosto.Visible = false;
            }

        }

        /// <summary>
        /// Método para botón guardar registro. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void BtnGuardar_Click(object sender, EventArgs e)
        {
            if (txtNombreDelBien.Text != "")
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
                                        ActualizarBien();
                                        DataTable Datos = new DataTable();
                                        Datos = BdModificacionBien.BuscarGrid(txtNumDoc, txtNumLote, txtNumInv);
                                        GridModificar.DataSource = Datos;
                                        GridModificar.DataBind();

                                        DataTable BuscarPropiedad = new DataTable();
                                        BuscarPropiedad = BdModificacionBien.BuscarPropiedad(Lblbien);

                                        List<CModificacionBien> PropiedadList = new List<CModificacionBien>();
                                        for (int i = 0; i < BuscarPropiedad.Rows.Count; i++)
                                        {
                                            CModificacionBien cFactura = new CModificacionBien();
                                            cFactura.Propiedad = Convert.ToString(BuscarPropiedad.Rows[i]["Propiedad"]);
                                            PropiedadList.Add(cFactura);
                                        }
                                        String Propiedad = PropiedadList.FirstOrDefault().Propiedad.ToString();

                                        String pu = txtPUnitario.Text;
                                        String iva;
                                        if(labelIVA.Text == "")
                                        {
                                            iva = "0";
                                        }else
                                        {
                                            iva = labelIVA.Text;
                                        }
                                        Decimal ValorConIVA = Convert.ToDecimal(pu, CultureInfo.CreateSpecificCulture("en-US")) + (Convert.ToDecimal(pu, CultureInfo.CreateSpecificCulture("en-US")) * Convert.ToDecimal(iva));
                                        Decimal numResult = Math.Truncate(Convert.ToDecimal(ValorConIVA) * 100) / 100;
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
                                            if (txtFrente.Text != "")
                                            {
                                                if (txtFondo.Text != "")
                                                {
                                                    if (txtAltura.Text != "")
                                                    {
                                                        if (txtDiametro.Text != "")
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
                                                if (txtVidautil.Text != "")
                                                {
                                                    insertarEnDepreciar();
                                                }
                                                else
                                                {
                                                    MostrarMensaje("** Vida Util requerida **", "info", "Normal");
                                                }
                                            }
                                        }
                                        MostrarNumInventarioIndividual();
                                        LimpiarRegistro();
                                        Modificacion.Visible = false;
                                        DivFiltrosModificacion.Visible = true;
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
        
        /// <summary>
        /// Método para el botón limpiar reistro. 
        /// </summary>
        public void LimpiarRegistro()
        {

            txtPUnitario.Text = string.Empty;
            checkboxIVA.Checked = false;
            labelIVA.Text = string.Empty;
            txtDescuento.Text = string.Empty;
            LabelSubTotal1.Text = string.Empty;
            labelCostototal.Text = string.Empty;
            checkboxDescuento.Checked = false;
            ddlResguardoInicial.SelectedIndex = 0;
            txtNombreDelBien.Text = string.Empty;
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
            txtFrente.Text = string.Empty;
            txtAltura.Text = string.Empty;
            txtFondo.Text = string.Empty;
            txtDiametro.Text = string.Empty;
            txtVidautil.Text = string.Empty;
            txtDetalle.Text = "";
            txtAjustarCosto.Text = "";
            txtAjustarCosto.Visible = false;
            txtDescuento.Visible = false;
            cbxAjustarCosto.Checked = false;
            div9.Visible = false;
            div8.Visible = false;
        }

        /// <summary>
        /// Método para mostrar una alerta con el número de inventario del Bien. 
        /// </summary>
        /// <param name="n"></param>
        private void MostrarNumero(string n)
        {
            string Msj = "";
            Msj = "MostrarMensaje('" + n + "');";

            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Ntn", Msj, true);
        }

        /// <summary>
        /// Método para el bo´ton limpiar 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtAjustarCosto.Text = "";
            txtPUnitario.Text = string.Empty;
            checkboxIVA.Checked = false;
            labelIVA.Text = string.Empty;
            txtDescuento.Text = string.Empty;
            LabelSubTotal1.Text = string.Empty;
            labelCostototal.Text = string.Empty;
            checkboxDescuento.Checked = false;
            ddlResguardoInicial.SelectedIndex = 0;
            txtNombreDelBien.Text = string.Empty;
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
            txtFrente.Text = string.Empty;
            txtAltura.Text = string.Empty;
            txtFondo.Text = string.Empty;
            txtDiametro.Text = string.Empty;
            txtVidautil.Text = string.Empty;
            txtDetalle.Text = "";
            txtAjustarCosto.Visible = false;
            txtDescuento.Visible = false;
            cbxAjustarCosto.Checked = false;
            txtVidautil.Text = "";
            div9.Visible = false;
        }

        /// <summary>
        /// Método para el botón cacelar registro. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Cancel_Click(object sender, EventArgs e)
        {
            LimpiarTodo();
            Modificacion.Visible = false;
            DivFiltrosModificacion.Visible = true;
            BotonLimpiar.Visible = true;
        }

        /// <summary>
        /// Método para el botoón lmpiar(esto para todos los parametros). 
        /// </summary>
        public void LimpiarTodo()
        {
            txtAjustarCosto.Text = "";
            txtPUnitario.Text = string.Empty;
            checkboxIVA.Checked = false;
            labelIVA.Text = string.Empty;
            txtDescuento.Text = string.Empty;
            LabelSubTotal1.Text = string.Empty;
            labelCostototal.Text = string.Empty;
            checkboxDescuento.Checked = false;
            ddlResguardoInicial.SelectedIndex = 0;
            txtNombreDelBien.Text = string.Empty;
            ddlPartida.SelectedIndex = 0;
            ddlSubClase.SelectedIndex = 0;
            ddlCatCONAC.SelectedIndex = 0;
            LabelGrupo.Text = string.Empty;
            LabelSubGrupo.Text = string.Empty;
            LabelClase.Text = string.Empty;
            ddlMarca.SelectedIndex = 0;
            txtModelo.Text = string.Empty;
            txtSerie.Text = string.Empty;
            txtFrente.Text = string.Empty;
            txtAltura.Text = string.Empty;
            txtFondo.Text = string.Empty;
            txtDiametro.Text = string.Empty;
            txtVidautil.Text = string.Empty;
            LblConcatena.Text = "";
            txtDetalle.Text = "";
            cbxAjustarCosto.Checked = false;
            checkboxDescuento.Checked = false;
        }

        /// <summary>
        /// Método para actualizar los datos del Bien
        /// </summary>
        public void ActualizarBien()
        {
            DataTable TablaTipoPartida = new DataTable();
            TablaTipoPartida = BdModificacionBien.TipoPartida(ddlPartida);
            string tipo;
            ///////
            List<CModificacionBien> PartidaList = new List<CModificacionBien>();
            for (int i = 0; i < TablaTipoPartida.Rows.Count; i++)
            {
                CModificacionBien cTipoPartida = new CModificacionBien();
                cTipoPartida.TipoPartida = Convert.ToInt32(TablaTipoPartida.Rows[i]["TipoPartida"]);

                PartidaList.Add(cTipoPartida);
            }
            tipo = PartidaList.FirstOrDefault().TipoPartida.ToString();
            ///////
            DataTable TablaENSU = new DataTable();
            TablaENSU = BdModificacionBien.idENSU(ddlResguardoInicial);
            string ENSU;
            ///////
            List<CModificacionBien> ENSUList = new List<CModificacionBien>();
            for (int i = 0; i < TablaENSU.Rows.Count; i++)
            {
                CModificacionBien cRegistroDeUnBien = new CModificacionBien();
                cRegistroDeUnBien.IdENSU = Convert.ToInt32(TablaENSU.Rows[i]["idENSU"]);

                ENSUList.Add(cRegistroDeUnBien);
            }
            ENSU = ENSUList.FirstOrDefault().IdENSU.ToString();
            ///////
            DataTable TablaCompra = new DataTable();
            TablaCompra = BdModificacionBien.idCompra(Lblbien);
            string Compra;
            ///////
            List<CModificacionBien> CompraList = new List<CModificacionBien>();
            for (int i = 0; i < TablaCompra.Rows.Count; i++)
            {
                CModificacionBien cRegistroDeUnBien = new CModificacionBien();
                cRegistroDeUnBien.IdCompra = Convert.ToInt32(TablaCompra.Rows[i]["IdCompra"]);

                CompraList.Add(cRegistroDeUnBien);
            }
            Compra = CompraList.FirstOrDefault().IdCompra.ToString();
            ///////

            DataTable BuscarFactura = new DataTable();
            BuscarFactura = BdModificacionBien.BuscarFactura(Lblbien);

            List<CModificacionBien> FacturaList = new List<CModificacionBien>();
            for (int i = 0; i < BuscarFactura.Rows.Count; i++)
            {
                CModificacionBien cFactura = new CModificacionBien();
                cFactura.FechaAdquisicion = Convert.ToString(BuscarFactura.Rows[i]["FechaAdquisicion"]);
                FacturaList.Add(cFactura);
            }
            string fechaAd = FacturaList.FirstOrDefault().FechaAdquisicion.ToString();

            string nombre = txtNombreDelBien.Text;
            int idCONAC = Convert.ToInt32(ddlCatCONAC.SelectedValue);
            int idSubClase = Convert.ToInt32(ddlSubClase.SelectedValue);
            string modelo = txtModelo.Text;
            string serie = txtSerie.Text;
            int marca = Convert.ToInt32(ddlMarca.SelectedValue);
            string detalle = txtDetalle.Text;
            string precio = txtPUnitario.Text;
            String pu = txtPUnitario.Text;
            String iva;
            if(labelIVA.Text == "")
            {
                iva = "0";
            }
            else
            {
                iva = labelIVA.Text;
            }
            Decimal ValorConIVA = Convert.ToDecimal(pu, CultureInfo.CreateSpecificCulture("en-US")) + (Convert.ToDecimal(pu, CultureInfo.CreateSpecificCulture("en-US")) * Convert.ToDecimal(iva));
            Decimal numResult = Math.Truncate(Convert.ToDecimal(ValorConIVA) * 100) / 100;
            Decimal result = Convert.ToDecimal(numResult, CultureInfo.CreateSpecificCulture("en-US"));

            String descuento = txtDescuento.Text;
            Decimal ValorCostoT = (Convert.ToDecimal(result, CultureInfo.CreateSpecificCulture("en-US")) - Convert.ToDecimal(descuento, CultureInfo.CreateSpecificCulture("en-US")));
            Decimal numResult1 = Math.Truncate(Convert.ToDecimal(ValorCostoT) * 100) / 100;
            Decimal result1 = Convert.ToDecimal(numResult1, CultureInfo.CreateSpecificCulture("en-US"));


            string desc = txtDescuento.Text;
            string costoT = Convert.ToString(result1);

            if (cbxAjustarCosto.Checked == true)
            {
                if (txtAjustarCosto.Text != "")
                {
                    costoT = txtAjustarCosto.Text;
                }
            }
            string numIn = Lblbien.Text;

            SqlConnection Conn = new SqlConnection(CConexion.Obtener());
            bool success = false;
            SqlTransaction lTransaccion = null;
            int Valor_Retornado = 0;
            try
            {
                Conn.Open();
                lTransaccion = Conn.BeginTransaction(System.Data.IsolationLevel.Serializable);
                SqlCommand cmd = new SqlCommand("SP_Modificar_Bien", Conn, lTransaccion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                cmd.Parameters.Add(new SqlParameter("@NumInventario", Convert.ToInt64(numIn)));
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

                MostrarMensaje("** Error al Modificar el Bien **", "info", "Normal");
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
        /// Método para insertar en la tabla dimención bien
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
            /////// MAL PROGRAMADO ???????
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


            string Frente = txtFrente.Text;
            string Fondo = txtFondo.Text;
            string Altura = txtAltura.Text;
            string Diametro = txtDiametro.Text;

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
        /// Método para insertar en la tabla depreciar
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

            string vida = txtVidautil.Text;
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

            DataTable BuscarFactura = new DataTable();
            BuscarFactura = BdModificacionBien.BuscarFactura(Lblbien);

            List<CModificacionBien> FacturaList = new List<CModificacionBien>();
            for (int i = 0; i < BuscarFactura.Rows.Count; i++)
            {
                CModificacionBien cFactura = new CModificacionBien();
                cFactura.FechaAdquisicion = Convert.ToString(BuscarFactura.Rows[i]["FechaAdquisicion"]);
                FacturaList.Add(cFactura);
            }
            string fechaAd = FacturaList.FirstOrDefault().FechaAdquisicion.ToString();


            DateTime Fecha = Convert.ToDateTime(fechaAd);
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
                cmd.Parameters.Add(new SqlParameter("@FechaInicio", Convert.ToDateTime(Fecha)));
                cmd.Parameters.Add(new SqlParameter("@FechaTermino", Convert.ToDateTime(FechaFinal)));
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
        /// Método para mostrar en la alerta el número de invernatrio 
        /// que se modificó. 
        /// </summary>
        public void MostrarNumInventarioIndividual()
        {
            ////////////////////////////////////////
            string serie = txtSerie.Text;  
                
            
            

            SqlConnection con1 = new SqlConnection(CConexion.Obtener());
            con1.Open();
            string sqlCad2 = "select NumInventario from FichaBien where Serie = @serie";
            SqlCommand cmdMax2 = new SqlCommand(sqlCad2, con1);
            cmdMax2.Parameters.Add("@serie", SqlDbType.VarChar, 150).Value = serie;
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
            MostrarNumero("Actualización exitosa del Bien &nbsp;" + numero);
        }

        /// <summary>
        /// Método para validar si existe el registro en la tabla
        /// DimencionBien
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public bool existeBienEnDimencion(string num)
        {
            string NumBien = Lblbien.Text;
            SqlConnection con = new SqlConnection(CConexion.Obtener());
            con.Open();
            string sqlCad1 = @"select IdInventario from FichaBien 
                                where NumInventario = @numBien";
            SqlCommand cmdMax1 = new SqlCommand(sqlCad1, con);
            cmdMax1.Parameters.Add("@numBien", SqlDbType.VarChar, 30).Value = NumBien;
            SqlDataReader leerMax1 = cmdMax1.ExecuteReader();
            string numInven;
            if (leerMax1.Read() == true)
            {
                numInven = leerMax1[0].ToString();
            }
            else
            {
                numInven = "";
            }
            con.Close();

            SqlConnection cnn = new SqlConnection(CConexion.Obtener());
            string sql = "select count(*)from DimencionBien where IdInventario = @num";
            cnn.Open();

            try
            {
                SqlCommand query = new SqlCommand(sql, cnn);
                query.Parameters.AddWithValue("@num", numInven);

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
        /// Método para validar si existe el Bien en la tabla
        /// depreciar. 
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public bool existeBienEnDepreciar(string num)
        {
            string NumBien = Lblbien.Text;
            SqlConnection cnn = new SqlConnection(CConexion.Obtener());
            string sql = "select count(*)from Depreciar where NumInventario = @num";
            cnn.Open();

            try
            {
                SqlCommand query = new SqlCommand(sql, cnn);
                query.Parameters.AddWithValue("@num", NumBien);

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
        /// Método para la paginacón del grid
        /// al cambiar de página. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void GridModificar_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridModificar.PageIndex = e.NewPageIndex;
            DataTable Datos = new DataTable();
            Datos = BdModificacionBien.BuscarGrid(txtNumDoc, txtNumLote, txtNumInv);
            GridModificar.DataSource = Datos;
            GridModificar.DataBind();
        }

        /// <summary>
        /// Método para guaradr el número de serie. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnGuardarSerie_Click(object sender, ImageClickEventArgs e)
        {
            ImageButton imageButton = (ImageButton)sender;
            TableCell tableCell = (TableCell)imageButton.Parent;
            GridViewRow row = (GridViewRow)tableCell.Parent;

            GridModificar.SelectedIndex = row.RowIndex;
            int fila = row.RowIndex;
            string serie = ((TextBox)row.Cells[fila].FindControl("TxtNoSerie")).Text;

            if (serie == "")
            {
                MostrarMensaje("* Es necesario asignar un número de serie *", "info", "Normal");
            }
            else
            {
                DataTable FichaBienInfo = new DataTable();
                FichaBienInfo = BdModificacionBien.FichaBienInfo(Lblbien, sender, GridModificar);

                List<CModificacionBien> FichaList = new List<CModificacionBien>();
                for (int i = 0; i < FichaBienInfo.Rows.Count; i++)
                {
                    CModificacionBien cFactura = new CModificacionBien();
                    cFactura.NumInventario = Convert.ToInt64(FichaBienInfo.Rows[i]["NumInventario"]);
                    cFactura.DescripcionBien = Convert.ToString(FichaBienInfo.Rows[i]["DescripcionBien"]);
                    cFactura.IdCONAC = Convert.ToInt32(FichaBienInfo.Rows[i]["IdCONAC"]);
                    cFactura.idSubClase = Convert.ToInt32(FichaBienInfo.Rows[i]["idSubClase"]);
                    cFactura.Modelo = Convert.ToString(FichaBienInfo.Rows[i]["Modelo"]);
                    cFactura.IdMarca = Convert.ToInt32(FichaBienInfo.Rows[i]["IdMarca"]);
                    cFactura.Detalle = Convert.ToString(FichaBienInfo.Rows[i]["Detalle"]);
                    cFactura.TipoPartida = Convert.ToInt32(FichaBienInfo.Rows[i]["TipoPartida"]);
                    cFactura.IdENSU = Convert.ToInt32(FichaBienInfo.Rows[i]["IdENSU"]);
                    cFactura.FechaCaptura = Convert.ToDateTime(FichaBienInfo.Rows[i]["FechaCaptura"]);
                    cFactura.PrecioUnitario = Convert.ToDouble(FichaBienInfo.Rows[i]["PrecioUnitario"]);
                    cFactura.SubTotal = Convert.ToDouble(FichaBienInfo.Rows[i]["SubTotal"]);
                    cFactura.Descuento = Convert.ToDouble(FichaBienInfo.Rows[i]["Descuento"]);
                    cFactura.CostoTotal = Convert.ToDouble(FichaBienInfo.Rows[i]["CostoTotal"]);
                    cFactura.IdCompra = Convert.ToInt32(FichaBienInfo.Rows[i]["IdCompra"]);

                    FichaList.Add(cFactura);
                }
                string NumInventario = FichaList.FirstOrDefault().NumInventario.ToString();
                string DescripcionBien = FichaList.FirstOrDefault().DescripcionBien.ToString();
                string IdCONAC = FichaList.FirstOrDefault().IdCONAC.ToString();
                string idSubClase = FichaList.FirstOrDefault().idSubClase.ToString();
                string Modelo = FichaList.FirstOrDefault().Modelo.ToString();
                string IdMarca = FichaList.FirstOrDefault().IdMarca.ToString();
                string Detalle = FichaList.FirstOrDefault().Detalle.ToString();
                string TipoPartida = FichaList.FirstOrDefault().TipoPartida.ToString();
                string IdENSU = FichaList.FirstOrDefault().IdENSU.ToString();
                string FechaCaptura = FichaList.FirstOrDefault().FechaCaptura.ToString();
                string PrecioUnitario = FichaList.FirstOrDefault().PrecioUnitario.ToString();
                string SubTotal = FichaList.FirstOrDefault().SubTotal.ToString();
                string Descuento = FichaList.FirstOrDefault().Descuento.ToString();
                string CostoTotal = FichaList.FirstOrDefault().CostoTotal.ToString();
                string IdCompra = FichaList.FirstOrDefault().IdCompra.ToString();

                SqlConnection Conn = new SqlConnection(CConexion.Obtener());
                bool success = false;
                SqlTransaction lTransaccion = null;
                int Valor_Retornado = 0;
                try
                {
                    Conn.Open();
                    lTransaccion = Conn.BeginTransaction(System.Data.IsolationLevel.Serializable);
                    SqlCommand cmd = new SqlCommand("SP_Actualizar_Serie", Conn, lTransaccion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Clear();
                    cmd.Parameters.Add(new SqlParameter("@NumInventario", Convert.ToInt64(NumInventario)));
                    cmd.Parameters.Add(new SqlParameter("@DescripcionBien", DescripcionBien));
                    cmd.Parameters.Add(new SqlParameter("@IdCONAC", IdCONAC));
                    cmd.Parameters.Add(new SqlParameter("@IdSubClase", idSubClase));
                    cmd.Parameters.Add(new SqlParameter("@Modelo", Modelo));
                    cmd.Parameters.Add(new SqlParameter("@Serie", serie));
                    cmd.Parameters.Add(new SqlParameter("@IdMarca", IdMarca));
                    cmd.Parameters.Add(new SqlParameter("@Detalle", Detalle));
                    cmd.Parameters.Add(new SqlParameter("@TipoPartida", TipoPartida));
                    cmd.Parameters.Add(new SqlParameter("@IdENSU", IdENSU));
                    cmd.Parameters.Add(new SqlParameter("@FechaCaptura", Convert.ToDateTime(FechaCaptura)));
                    cmd.Parameters.Add(new SqlParameter("@PrecioUnitario", Convert.ToDouble(PrecioUnitario)));
                    cmd.Parameters.Add(new SqlParameter("@SubTotal", Convert.ToDouble(SubTotal)));
                    cmd.Parameters.Add(new SqlParameter("@Descuento", Convert.ToDouble(Descuento)));
                    cmd.Parameters.Add(new SqlParameter("@CostoTotal", Convert.ToDouble(CostoTotal)));
                    cmd.Parameters.Add(new SqlParameter("@IdCompra", IdCompra));
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
                        MostrarMensaje("** Serie actualizada correctamente **", "info", "Normal");
                        DataTable Datos = new DataTable();
                        Datos = BdModificacionBien.BuscarGrid(txtNumDoc, txtNumLote, txtNumInv);
                        GridModificar.DataSource = Datos;
                        GridModificar.DataBind();
                    }
                    else
                    {
                        lTransaccion.Rollback();
                        Conn.Close();
                    }
                }
            }
            
        }

        /// <summary>
        /// Método para limpiar los filtro de busqueda. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void BtnLimpiarBusqueda_Click(object sender, EventArgs e)
        {
            div1.Visible = false;
            txtNumDoc.Text = "";
            txtNumLote.Text = "";
            txtNumInv.Text = "";
            BotonLimpiar.Visible = false;
            divGrid.Visible = false;
        }

        public void LlenarClaveCONAC(string IdCONAC)
        {
            LblGrupo1.Visible = true;
            LblSubGrupo1.Visible = true;
            LblClase.Visible = true;

            SqlConnection con = new SqlConnection(CConexion.Obtener());
            con.Open();
            string cadSql = "select * from Cat_CONAC where IdCONAC = @IdCONAC";
            SqlCommand cmd = new SqlCommand(cadSql, con);
            cmd.Parameters.Add("@IdCONAC", SqlDbType.VarChar, 100).Value = IdCONAC;
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

        protected void GridModificar_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}

