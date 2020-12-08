using InventariosPJEH.CAccesoDatos;
using InventariosPJEH.CNegocios;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace InventariosPJEH
{
    public partial class frmBajaBienAdmin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
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
        protected void GridViewBBA_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string CommandName = e.CommandName;
            int index = -1;
            GridViewRow Row;

            if (e.CommandArgument.ToString() != "")
            {
                index = Convert.ToInt32(e.CommandArgument);
                Row = GridRevisarBien.Rows[index];
            }
            else
            {
                Row = (GridViewRow)((DropDownList)e.CommandSource).NamingContainer;
            }

            Int64 NumInventario = Convert.ToInt64(Row.Cells[0].Text);
            int IdInventario = Convert.ToInt32(Row.Cells[12].Text);
            TResultado ResModInventario = new TResultado();

            if (CommandName == "BajaTemporal")
            {
                DropDownList DdlClas = Row.FindControl("DdlClasificacion") as DropDownList;
                if (DdlClas.SelectedValue != "0")
                {
                    string Clasificacion = DdlClas.SelectedValue;
                    int RegModificados = BdBajaBienAdmin.ModificarBajaBien(IdInventario, "BT", Clasificacion, ref ResModInventario);

                    if (ResModInventario.Exito)
                    {
                        MostrarMensaje("La modificación se realizó correctamente", "info", "Normal", "ModificacionCorrecta");
                    }
                    else
                    {
                        MostrarMensaje(ResModInventario.Mensaje.FirstOrDefault(), "error", "Normal", "ErrorResModInventario");
                    }
                }
                else
                    MostrarMensaje("Seleccione una clasificación antes de dar clic en la baja.", "error", "Normal", "ErrorDdlClasificacion");
            }
            else if (CommandName == "BajaDefinitiva")
            {
                DropDownList DdlClas = Row.FindControl("DdlClasificacion") as DropDownList;
                if (DdlClas.SelectedValue != "0")
                {
                    string Clasificacion = DdlClas.SelectedValue;
                    int RegModificados = BdBajaBienAdmin.ModificarBajaBien(IdInventario, "BD", Clasificacion, ref ResModInventario);

                    if (ResModInventario.Exito)
                        MostrarMensaje("La modificación se realizó correctamente", "info", "Normal", "ModificacionCorrecta");
                    else
                        MostrarMensaje(ResModInventario.Mensaje.FirstOrDefault(), "error", "Normal", "ErrorResModInventario");
                }
                else
                    MostrarMensaje("Seleccione una clasificación antes de dar clic en la baja.", "error", "Normal", "ErrorDdlClasificacion");
            }
            else if (CommandName == "Liberar")
            {
                int IdENSU = Convert.ToInt32(Row.Cells[13].Text);

                int RegModificados = BdBajaBienAdmin.EliminarGrupoResguardo(IdENSU, ref ResModInventario);

                if (ResModInventario.Exito)
                    MostrarMensaje("El Bien se Libero de forma correcta", "info", "Normal", "LiberacionCorrecta");
                else
                    MostrarMensaje(ResModInventario.Mensaje.FirstOrDefault(), "error", "Normal", "ErrorResEliminoResguardo");
            }
            else if (CommandName == "Quitar")
            {
                int RegModificados = BdBajaBienAdmin.ModificarFichaBien(NumInventario, 1, ref ResModInventario);

                if (ResModInventario.Exito)
                    MostrarMensaje("La modificación se realizó correctamente", "info", "Normal", "ModificacionCorrecta");
                else
                    MostrarMensaje(ResModInventario.Mensaje.FirstOrDefault(), "error", "Normal", "ErrorResModInventario");
            }
        }
        protected void GridBajaTemporal_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string CommandName = e.CommandName;
            int index = -1;
            GridViewRow Row;

            if (e.CommandArgument.ToString() != "")
            {
                index = Convert.ToInt32(e.CommandArgument);
                Row = GridBajaTemporal.Rows[index];
            }
            else
            {
                Row = (GridViewRow)((DropDownList)e.CommandSource).NamingContainer;
            }


            Int64 NumInventario = Convert.ToInt64(Row.Cells[0].Text);
            int IdInventario = Convert.ToInt32(Row.Cells[9].Text);
            TResultado ResModInventario = new TResultado();

            //if (CommandName == "")
            //{
            //    CheckBox CheckUbucacion = Row.FindControl("CheckBoxUbicacion") as CheckBox;
            //    if (CheckUbucacion.Checked == true)
            //    {
            //        divUbicacionB.Visible = true;

            //    }
            //    else
            //        divUbicacionB.Visible = false;
            //    TxtNumResguardo.Text = "";
            //    lblNomResguardante.Text = "";
            //    DropGrupoAsignado.DataSource = null;
            //    DropGrupoAsignado.DataBind();
            //    lblENSU.Text = "";
            //}

            if (CommandName == "Eliminar")
            {
                    int RegModificados = BdBajaBienAdmin.ModificarBajaBien(IdInventario, "R", "", ref ResModInventario);

                    if (ResModInventario.Exito)
                        MostrarMensaje("La modificación se realizó correctamente", "info", "Normal", "ModificacionCorrecta");
                    else
                        MostrarMensaje(ResModInventario.Mensaje.FirstOrDefault(), "error", "Normal", "ErrorResModInventario");
            }

        }

        protected void GridBajaDefinitiva_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            string CommandName = e.CommandName;
            int index = -1;
            GridViewRow Row;

            if (e.CommandArgument.ToString() != "")
            {
                index = Convert.ToInt32(e.CommandArgument);
                Row = GridBajaDefinitiva.Rows[index];
            }
            else
            {
                Row = (GridViewRow)((DropDownList)e.CommandSource).NamingContainer;
            }


            Int64 NumInventario = Convert.ToInt64(Row.Cells[0].Text);
            int IdInventario = Convert.ToInt32(Row.Cells[8].Text);
            TResultado ResModInventario = new TResultado();

            if (CommandName == "Eliminar1")
            {
                int RegModificados = BdBajaBienAdmin.ModificarBajaBien(IdInventario, "R", "", ref ResModInventario);

                if (ResModInventario.Exito)
                    MostrarMensaje("La modificación se realizó correctamente", "info", "Normal", "ModificacionCorrecta");
                else
                    MostrarMensaje(ResModInventario.Mensaje.FirstOrDefault(), "error", "Normal", "ErrorResModInventario");
            }
        }

        protected void BtnAgregar_Click(object sender, EventArgs e)
        {
            Int64 NumInventario = Convert.ToInt64(TextBoxNoInventario.Text);

            TResultado ResFichaBien = new TResultado();
            List<CFichaBienBaja> LBien = BdBajaBienAdmin.ConsultarFichaBien(Convert.ToInt64(TextBoxNoInventario.Text), ref ResFichaBien);

            if (ResFichaBien.Exito)
            {
                if (LBien.Count > 0)
                {
                    CFichaBienBaja FichaBien = LBien.FirstOrDefault();
                    if (FichaBien.IdActividad == 1)
                    {
                        //List<CUbicacion> LUbicacion = BdBajaBienAdmin.ConsultarUbicacion(FichaBien.IdInventario, ref ResFichaBien);

                        //CUbicacion UbicacionInventario = LUbicacion.FirstOrDefault();
                        CUsuario Usuario = Page.Session["Usuario"] as CUsuario;

                        CUbicacion Ubicacion = new CUbicacion();
                        Ubicacion.IdInventario = FichaBien.IdInventario;
                        Ubicacion.IdActividad = 9;
                        Ubicacion.IdUsuario = Convert.ToInt32(Usuario.IdUsuario); //OBTENER DE VARIABLE DE SESIÒN EL ID DEL USUARIO QUE ESTA LOGUEADO
                        Ubicacion.Estatus = "E";

                        TResultado ResInsertarUbicacion = new TResultado();
                        int IdHistoricoUbicacion = BdBajaBienAdmin.InsertarHistoricoUbicacion(Ubicacion, ref ResInsertarUbicacion);

                        if (ResInsertarUbicacion.Exito)
                        {
                            TResultado ResConsultaFicha = new TResultado();
                            List<CFichaBienBaja> LFichaBien = BdBajaBienAdmin.ConsultarFichaBienPorActividad(9, ref ResConsultaFicha);
                        }
                        else
                            MostrarMensaje(ResInsertarUbicacion.Mensaje.FirstOrDefault(), "error", "Normal", "ErrorResInsertar");
                    }
                    else
                        MostrarMensaje("El bien no se puede asignar actualmente.", "error", "Normal", "ErrorIdActividad");

                }
                else
                    MostrarMensaje("No se encontrò el bien, favor de corroborar el Numero de inventario proporcionado.", "error", "Normal", "ErrorListaBien");
            }
            else
                MostrarMensaje(ResFichaBien.Mensaje.FirstOrDefault(), "error", "Normal", "ResFichaBienError");

        }

        protected void RbActividadBien_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton RadioSeleccionado = sender as RadioButton;
            TResultado ResConsultaFicha = new TResultado();
            List<CFichaBienBaja> LFichaBien = new List<CFichaBienBaja>();

            divGeneral.Visible = false;
            divConsultaBT.Visible = false;
            div6.Visible = false;

            if (RadioSeleccionado.ID == "RbRevisarBienes")
            {
                LFichaBien = BdBajaBienAdmin.ConsultarFichaBienPorTipoBaja("R", 9,ref ResConsultaFicha);
                divGeneral.Visible = true;
                GridRevisarBien.DataSource = LFichaBien;
                GridRevisarBien.DataBind();

                LblNumBienes.Text = LFichaBien.Count.ToString();
            }
            else if (RadioSeleccionado.ID == "RbConsultaBajaT")
            {
                LFichaBien = BdBajaBienAdmin.ConsultarFichaBienPorTipoBaja("BT", 9,ref ResConsultaFicha);
                divConsultaBT.Visible = true;
                GridBajaTemporal.DataSource = LFichaBien;
                GridBajaTemporal.DataBind();

                LblNumBiene.Text = LFichaBien.Count.ToString();
            }
            else if (RadioSeleccionado.ID == "RbConsultaBD")
            {
                LFichaBien = BdBajaBienAdmin.ConsultarFichaBienPorTipoBaja("BD", 9, ref ResConsultaFicha);
                div6.Visible = true;
                GridBajaDefinitiva.DataSource = LFichaBien;
                GridBajaDefinitiva.DataBind();

                TotaldeBienes.Text = LFichaBien.Count.ToString();
            }

            Page.Session["LFichaBien"] = LFichaBien;
        }

        protected void BtnFiltro_Click(object sender, EventArgs e)
        {
            List<CFichaBienBaja> LFichaBien = Page.Session["LFichaBien"] as List<CFichaBienBaja>;
            Int64 NumInventario = Convert.ToInt64(TxtNumInventario.Text);

            List<CFichaBienBaja> LFichaFiltro = (from FichaB in LFichaBien
                                                 where FichaB.NumInventario == NumInventario
                                                 select FichaB).ToList();

            if (RbRevisarBienes.Checked)
            {
                GridRevisarBien.DataSource = LFichaFiltro;
                GridRevisarBien.DataBind();
            }
            else if (RbConsultaBajaT.Checked)
            {
                GridBajaTemporal.DataSource = LFichaFiltro;
                GridBajaTemporal.DataBind();
            }
            else if (RbConsultaBD.Checked)
            {
                GridBajaDefinitiva.DataSource = LFichaFiltro;
                GridBajaDefinitiva.DataBind();
            }
        }

        //protected void BntBT_Click(object sender, EventArgs e)
        //{
        //    if (HFNumResguardo.Value != "")
        //    {
        //        int RegModificados = BdBajaBienAdmin.BajaTemporalBien(IdInventario, "R", "", ref ResModInventario);

        //        if (ResModInventario.Exito)
        //            MostrarMensaje("La modificación se realizó correctamente", "info", "Normal", "ModificacionCorrecta");
        //        else
        //            MostrarMensaje(ResModInventario.Mensaje.FirstOrDefault(), "error", "Normal", "ErrorResModInventario");
        //    }
        //    else
        //        MostrarMensaje("No puede realizar la modificacion debido a que no ha insertardo un numero de resguardo valido ", "error", "Normal", "ErrorResModInventario");
        //}


        protected void TxtNumResguardo_TextChanged(object sender, EventArgs e)
        {
            int NumResguardo = 0;
            TResultado ResGrupoResguardo = new TResultado();

            if (int.TryParse(TxtNumResguardo.Text, out NumResguardo))
            {
                List<CGrupoResguardo> LGrupoResguardo = BdBajaBienAdmin.ConsultarGrupoResguardo(NumResguardo, ref ResGrupoResguardo);

                if (LGrupoResguardo.Count > 0)
                {
                    lblNomResguardante.Text = LGrupoResguardo.FirstOrDefault().NombreCompleto;
                    HFIdEmpleado.Value = LGrupoResguardo.FirstOrDefault().IdEmpleado.ToString();

                    ListItem Grupo = new ListItem();
                    Grupo.Text = "--Seleccione un grupo--";
                    Grupo.Value = "0";

                    DropGrupoAsignado.DataSource = LGrupoResguardo;
                    DropGrupoAsignado.DataTextField = "Grupo";
                    DropGrupoAsignado.DataValueField = "Grupo";
                    DropGrupoAsignado.DataBind();

                    Page.Session["LGrupoResguardo"] = LGrupoResguardo;

                    DropGrupoAsignado.Items.Insert(0, Grupo);

                    HFNumResguardo.Value = NumResguardo.ToString();
                }
                else
                {
                    MostrarMensaje("El resguardo proporcionado no tiene grupos", "error", "Normal", "ErrorListaBien");
                    HFNumResguardo.Value = "";
                }  
            }
            else 
            {
                MostrarMensaje("Introduce un Numero de Resguardo Valido", "error", "Normal", "ErrorListaBien");
                HFNumResguardo.Value = "";
            }

        }

        protected void DropGrupoAsignado_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<CGrupoResguardo> LGrupo = Page.Session["LGrupoResguardo"] as List<CGrupoResguardo>;
            string Grup = DropGrupoAsignado.SelectedValue;

            CGrupoResguardo GrupoSelec = (from G in LGrupo
                                          where G.Grupo == Grup
                                          select G).FirstOrDefault();

            lblENSU.Text = GrupoSelec.Edificio + "-" + GrupoSelec.Nivel + "-" + GrupoSelec.Seccion;

            HFIdENSU.Value = GrupoSelec.IdENSU.ToString();
            HFGrupo.Value = GrupoSelec.Grupo;
        }

        protected void CheckBoxUbicacion_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox CheckSeleccionado = sender as CheckBox;

            if (CheckSeleccionado.Checked)
            {
                divUbicacionB.Visible = true;
            }
            else
            {
                GridViewRowCollection Rows = GridBajaTemporal.Rows;
                int FilasSelec = 0;
                foreach (GridViewRow Row in Rows)
                {
                    CheckBox CheckRow = Row.Cells[7].Controls[1] as CheckBox;
                    if (CheckRow.Checked)
                        FilasSelec += 1;
                }

                if (FilasSelec == 0)
                    divUbicacionB.Visible = false;
                
            }
        }
        protected void BntBT_Click1(object sender, EventArgs e)
        {
            GridViewRowCollection Rows = GridBajaTemporal.Rows;
            List<string> LIdInventarios = new List<string>();
            TResultado ResModInventario = new TResultado();
            CUbicacion BajaTemporal = new CUbicacion();

            //List<CFichaBienBaja> LFichaBien = new List<CFichaBienBaja>();

            CUsuario Usuario = Page.Session["Usuario"] as CUsuario;
            BajaTemporal.IdUsuario = Convert.ToInt32(Usuario.IdUsuario);
            BajaTemporal.IdActividad = 6;
            BajaTemporal.IdEmpleado = Convert.ToInt32(HFIdEmpleado.Value);
            BajaTemporal.Grupo = HFGrupo.Value;
            BajaTemporal.FechaResguardo = DateTime.Now;
            BajaTemporal.IdResguardo = Convert.ToInt32(TxtNumResguardo.Text);
            BajaTemporal.Grupo = DropGrupoAsignado.SelectedValue;
            BajaTemporal.IdENSU = Convert.ToInt32(HFIdENSU.Value);

            foreach (GridViewRow Row in Rows)
            {
                CheckBox CheckRow = Row.Cells[7].Controls[1] as CheckBox;
                
                if (CheckRow.Checked)
                {
                    //LIdInventarios.Add(Row.Cells[9].ToString());
                    BajaTemporal.IdInventario = Convert.ToInt32(Row.Cells[9].Text);
                    BajaTemporal.TipoPartida = Convert.ToInt32(Row.Cells[10].Text);
                    int RegModificados = BdBajaBienAdmin.BajaTemporalBien(BajaTemporal, ref ResModInventario);
                }
                if (!ResModInventario.Exito)
                {
                    MostrarMensaje(ResModInventario.Mensaje.FirstOrDefault(), "info", "Normal", "ModificacionCorrecta");
                    break;
                }
            }

            if (ResModInventario.Exito)
                MostrarMensaje("La modificación se realizó correctamente", "info", "Normal", "ModificacionCorrecta");
        }

        protected void BtnBD1_Click(object sender, EventArgs e)
        {
            List<CFichaBienBaja> LFichaBien = Page.Session["LFichaBien"] as List<CFichaBienBaja>;
            CUsuario Usuario = Page.Session["Usuario"] as CUsuario;
            TResultado ResModInventario = new TResultado();

            foreach (CFichaBienBaja FB in LFichaBien)
            {
                ResModInventario = new TResultado();
                CUbicacion BajaDefinitiva = new CUbicacion();

                BajaDefinitiva.IdInventario = FB.IdInventario;
                BajaDefinitiva.IdActividad = 7;
                BajaDefinitiva.IdUsuario = Convert.ToInt32(Usuario.IdUsuario); //OBTENER DE VARIABLE DE SESIÒN EL ID DEL USUARIO QUE ESTA LOGUEADO
                BajaDefinitiva.OficioBien = TxtNumOficio.Text;

                int RegModificados = BdBajaBienAdmin.BajaDefinitivaBien(BajaDefinitiva, ref ResModInventario);

                if (!ResModInventario.Exito)
                {
                    MostrarMensaje(ResModInventario.Mensaje.FirstOrDefault(), "info", "Normal", "ModificacionCorrecta");
                    break;
                }
            }
            if (ResModInventario.Exito)
                MostrarMensaje("La modificación se realizó correctamente", "info", "Normal", "ModificacionCorrecta");
        }

        protected void BtnImrpimirLista_Click(object sender, EventArgs e)
        {
            List<CFichaBienBaja> LFichaBien = Page.Session["LFichaBien"] as List<CFichaBienBaja>;
            DataTable DatosGrid = ConvertirListaTabla(LFichaBien);
            ExpExcelDataTable(DatosGrid);
        }

        public DataTable ConvertirListaTabla(List<CFichaBienBaja> Datos)
        {
            PropertyDescriptorCollection props =
                TypeDescriptor.GetProperties(typeof(CFichaBienBaja));
            DataTable table = new DataTable();
            for (int i = 0; i < props.Count; i++)
            {
                PropertyDescriptor prop = props[i];
                table.Columns.Add(prop.Name, prop.PropertyType);
            }
            object[] values = new object[props.Count];
            foreach (CFichaBienBaja item in Datos)
            {
                for (int i = 0; i < values.Length; i++)
                {
                    values[i] = props[i].GetValue(item);
                }
                table.Rows.Add(values);
            }
            return table;
        }

        protected void ExpExcelDataTable(DataTable Tabla)
        {
            //Create a dummy GridView
            GridView GridView1 = new GridView();
            GridView1.AllowPaging = false;
            GridView1.DataSource = Tabla;
            GridView1.DataBind();

            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition",
             "attachment;filename=DataTable.xls");
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
    }
}
