using System;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using InventariosPJEH.CAccesoDatos;
using InventariosPJEH.CNegocios;
using System.IO;
using System.Web.UI.HtmlControls;
using System.Text;
using System.Collections.Generic;
using System.ComponentModel;

namespace InventariosPJEH
{
    public partial class frmBajaBienSoporte : System.Web.UI.Page
    {
        CReparaBien obJN = new CReparaBien();
        BdReparaBien obJE = new BdReparaBien();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindGridView();
            }

            if (Request.Form["__EVENTTARGET"] == "AccionEliminarr")
            {
                GridViewBuscar_RowDeleting(this, new GridViewDeleteEventArgs(Int32.Parse(Rowindex.Value)));
            }
            else if (Request.Form["__EVENTTARGET"] == "AccionVacio")
            {
                Rowindex.Value = string.Empty;
            }
        }
        public void LimpiarBuscar()
        {
            TextBoxNumInventario.Text = string.Empty;
        }
        protected void BtnBuscar(object sender, EventArgs e)
        {
            //Mostrar u ocultar controles 
            BotonBuscar.Visible = true;          
            TextBoxContador.Visible = true;
            LblTotalBienes.Visible = true;
            BuscarInventario();
            LimpiarBuscar();
        }
        public void BuscarInventario()
        {
            if (TextBoxNumInventario.Text != "")
            {
                TextBoxNumInventario.Visible = true;
                BotonBuscar.Visible = true;
                
                GridViewBuscar.DataSource = BdReparaBien.ConsultarGbBienesSinReparar(TextBoxNumInventario.Text);
                GridViewBuscar.DataBind();
                TextBoxContador.Text = GridViewBuscar.Rows.Count.ToString();
                DivEnviarlistas.Visible = true;                

                if (GridViewBuscar.Rows.Count == 0)
                {
                    MostrarMensaje("** No existen datos con la búsqueda solicitada **", "error", "Normal", "Incorrecto");
                    TextBoxContador.Visible = false;
                    LblTotalBienes.Visible = false;
                    DivEnviarlistas.Visible = false;                 
                    GridViewBuscar.Visible = false;
                }
                else
                {
                    //Código para asignar valores del Grid a los Texbox
                    int indexx = 0;
                    string Idv = GridViewBuscar.DataKeys[indexx].Values["IdInventario"].ToString();
                    string Idact = GridViewBuscar.DataKeys[indexx].Values["IdActividad"].ToString();
                    string Idus = GridViewBuscar.DataKeys[indexx].Values["IdUsuario"].ToString();
                    string IdENS = GridViewBuscar.DataKeys[indexx].Values["IdENSU"].ToString();

                    IdInv.Text = Idv;
                    IdAct.Text = Idact;
                    IdUsu.Text = Idus;
                    IdENSU.Text = IdENS;
                }
            }
            else
            {
                MostrarMensaje("** Campo vacío **", "error", "Normal", "Incorrecto");
                TextBoxContador.Visible = false;
                LblTotalBienes.Visible = false;
                DivEnviarlistas.Visible = false;     
                GridViewBuscar.Visible = false;

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
                Msj = "confirm();";
            else
                Msj = "MostrarMensajeInterval('" + Mensaje + "', '" + Tipo + "');";

            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), ClaveMsj, Msj, true);
        }
        protected void GridViewBuscar_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }
        protected void GridViewBuscar_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            SinReparacion.Visible = true;
            BotonesBuscar.Visible = true;
            LimpiarBuscar();

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
                    string xcod = GridViewBuscar.DataKeys[n].Value.ToString();

                    obJN.IdInventario = xcod;
                    obJE.Quitar_Bien(obJN);

                    GridViewBuscar.EditIndex = -1;

                    GridViewBuscar.DataSource = BdReparaBien.ConsultarGbBienesSinReparar(TextBoxNumInventario.Text);
                    GridViewBuscar.DataBind();
                    TextBoxContador.Text = GridViewBuscar.Rows.Count.ToString();

                    MostrarMensaje("** Bien eliminado **", "error", "Aviso", "Incorrecto");
                    refreshBajaSoporte();
                    TextBoxNumInventario.Visible = true;
                    BuscarInventario();
                    DivTabla.Visible = false;
                    DivEnviarlistas.Visible = false;
                    BtnImprimirLista.Visible = false;
                    Rowindex.Value = "";
                    if (GridViewBuscar.Rows.Count == 0)
                    {
                        SinReparacion.Visible = true;
                        DivEnviarlistas.Visible = false;
                        BtnImprimirLista.Visible = false;

                        MostrarMensaje("** No existen datos con la búsqueda solicitada **", "error", "Normal", "Incorrecto");
                    }
                    else if (GridViewBuscar.Rows.Count != 0)
                    {
                        MostrarMensaje("** No se puede eliminar existen campos relacionados a otras tablas **", "error", "Normal", "Incorrecto");

                    }
                }
                catch (Exception)
                {
                    Rowindex.Value = "";
                    MostrarMensaje("** Error al eliminar **", "error", "Aviso", "Incorrecto");
                }
            }
        }   
        protected void BtnEnviarListas_Click(object sender, EventArgs e)
        {                        
            //Se llama la conexion a la Base de datos//
            SqlConnection cnn = new SqlConnection(CConexion.Obtener());
            bool success = false;
            /*Se declara una transacción para que todo lo que se realiza en ella, desde una simple inserción hasta multiples 
            o que involucren más operaciones sobre la base de datos puedan deshacerse si se presenta un error */

            SqlTransaction lTransaccion = null;
            //Variable para el valor de retorno
            int Valor_Retornado = 0;
            //inicializa un try catch
            cnn.Open();

            lTransaccion = cnn.BeginTransaction(System.Data.IsolationLevel.Serializable);
            //Especificamos el comando, en este caso el nombre del Procedimiento Almacenado, lTransaccion
            SqlCommand cmd = new SqlCommand("SP_BajaSoporte", cnn, lTransaccion);        

            //Se indica al tipo de comando que es de tipo Procedimiento Alamcenado
            cmd.CommandType = CommandType.StoredProcedure;

            try

            {              
                foreach (GridViewRow rowDatos in this.GridViewBuscar.Rows)
                {

                    if (rowDatos.RowType == DataControlRowType.DataRow)
                    {
                        //Se limpian los parametros
                        cmd.Parameters.Clear();
                        //Comienza a mandar a cada uno de los parametros, deben de enviarse en el tipo
                        //de datos que coincida es sql server 

                        cmd.Parameters.AddWithValue("@fechaE", fecha.Text);
                        cmd.Parameters.AddWithValue("@status", "E");
                        cmd.Parameters.AddWithValue("@idusuario", GridViewBuscar.DataKeys[rowDatos.RowIndex].Values[8].ToString());
                        cmd.Parameters.AddWithValue("@IdInventario", GridViewBuscar.DataKeys[rowDatos.RowIndex].Values[0].ToString());
                        cmd.Parameters.AddWithValue("@idENSU", GridViewBuscar.DataKeys[rowDatos.RowIndex].Values[9].ToString());
                        cmd.Parameters.AddWithValue("@idactividad", GridViewBuscar.DataKeys[rowDatos.RowIndex].Values[7].ToString());
                        cmd.Parameters.AddWithValue("@fecha", fecha.Text);

                        //declaramos el valor de retorno
                        SqlParameter ValorRetorno = new SqlParameter("@Comprobacion", SqlDbType.Int);
                        //asigamos el valor de retorno
                        ValorRetorno.Direction = ParameterDirection.Output;
                        cmd.Parameters.Add(ValorRetorno);
                        //Se executa la consulta
                        cmd.ExecuteNonQuery();
                        Valor_Retornado = Convert.ToInt32(ValorRetorno.Value);
                        //Dependiendo del valor de retorno la variable success si el procedimiento retorna un 1 la operación se realizó
                        //con exito de no ser así s emantiene en false y por lo tanto falló la operación
                        if (Valor_Retornado == 1)
                        {
                            success = true;
                        }
                        else
                        {
                            MostrarMensaje("** Error al insertar **", "error", "Normal", "Incorrecto");
                        }
                    }
                }
            }
            catch (Exception)
            {
                MostrarMensaje("** Error al insertar **", "error", "Normal", "Incorrecto");
            }
            finally
            {
                if (success)
                {
                    lTransaccion.Commit();
                    cnn.Close();
                    MostrarMensaje("** Lista Envíada Correctamente **", "error", "Normal", "Incorrecto");                    
                    SinReparacion.Visible = true;
                    BuscarInventario();
                    DivEnviarlistas.Visible = false;                 
                }
                else
                {
                    lTransaccion.Rollback();
                    cnn.Close();
                }
            }        
        }
        protected void time(object sender, EventArgs e)
        {
            fecha.Text = DateTime.Now.ToString("yyyy-MM-ddThh:mm:ss.fff");
        }
        protected void refreshBajaSoporte()
        {
            Response.Redirect("frmBajaBienSoporte.aspx");
        }
        protected void BtnRegresar_Click(object sender, EventArgs e)
        {
            Response.Redirect("frmReparaBien.aspx");
        }
        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Confirms that an HtmlForm control is rendered for the specified ASP.NET
    server control at run time. */
        }
        protected void BtnImprimirListas_Click(object sender, EventArgs e)
        {
            List<CBienSinReparar> LBienSinReparar = BdReparaBien.ConsultarGbBienesSinReparar(TextBoxNumInventario.Text);
            DataTable DatosGrid = ConvertirListaTabla(LBienSinReparar);
            ExportarExcel(DatosGrid);
            
        }
        private void BindGridView()
        {
            GridViewBuscar.DataSource = BdReparaBien.ConsultarGbBienesSinReparar(TextBoxNumInventario.Text);
            GridViewBuscar.DataBind();
            DivEnviarlistas.Visible = true;          
        }

        protected void ExportarExcel(DataTable Tabla)
        {
            //Create a dummy GridView
            GridView GridView1 = new GridView();
            GridView1.AllowPaging = false;
            GridView1.DataSource = Tabla;
            GridView1.DataBind();

            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition",
             "attachment;filename=Lista de Bienes.xls");
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);

            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                //Apply text style to each Row
                GridView1.Rows[i].Attributes.Add("class", "textmode");
            }
            GridView1.RenderControl(hw);

            //style to format numbers to string
            string style = @"<style> .textmode { mso-number-format:\@; } </style>";
            Response.Write(style);
            Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.End();

        }
        public DataTable ConvertirListaTabla(List<CBienSinReparar> Datos)
        {
            PropertyDescriptorCollection props =
                TypeDescriptor.GetProperties(typeof(CBienSinReparar));
            DataTable table = new DataTable();
            for (int i = 0; i < props.Count; i++)
            {
                PropertyDescriptor prop = props[i];
                table.Columns.Add(prop.Name, prop.PropertyType);
            }
            object[] values = new object[props.Count];
            foreach (CBienSinReparar item in Datos)
            {
                for (int i = 0; i < values.Length; i++)
                {
                    values[i] = props[i].GetValue(item);
                }
                table.Rows.Add(values);
            }
            return table;
        }
    }
}