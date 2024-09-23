using Sistema.Negocio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Imaging;
using System.IO;



namespace Sistema.Presentacion
{
    public partial class FrmArticulo : Form
    {
        private string RutaOrigen;
        private string RutaDestino;
        private string Directorio = @"C:\Users\jsalazar\Udemy\Desarrolla Sistemas en C# .Net, SQL Server, 4 Capas\Repo\Solucion\Imagenes\";
        private string NombreArticulo;
        public FrmArticulo()
        {
            InitializeComponent();
        }

        #region Listar 
        private void Listar()
        {
            try
            {
                DgvListado.DataSource = NArticulo.Listar();
                this.Formato();
                this.Limpiar();
                TxtTotal.Text = "Total: " + DgvListado.Rows.Count.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace);
            }

        }
        #endregion
        #region Buscar
        private void Buscar()
        {
            try
            {
                DgvListado.DataSource = NArticulo.Buscar(TxtBuscar.Text);
                this.Formato();
                TxtTotal.Text = TxtTotal.Text + DgvListado.Rows.Count.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace);
            }

        }
        #endregion
        #region Formato
        private void Formato()
        {
            DgvListado.Columns[0].Visible = false;
            DgvListado.Columns[2].Visible = false;
            DgvListado.Columns[0].Width = 100;
            DgvListado.Columns[1].Width = 50;
            DgvListado.Columns[3].Width = 100;
            DgvListado.Columns[3].HeaderText = "Categoría";
            DgvListado.Columns[4].Width = 100;
            DgvListado.Columns[4].HeaderText = "Código";
            DgvListado.Columns[5].Width = 150;
            DgvListado.Columns[6].Width = 100;
            DgvListado.Columns[6].HeaderText = "Precio Venta";
            DgvListado.Columns[7].Width = 60;
            DgvListado.Columns[8].Width = 200;
            DgvListado.Columns[8].HeaderText = "Descripción";
            DgvListado.Columns[9].Width = 100;
            DgvListado.Columns[10].Width = 100;

        }
        #endregion
        #region Limpiar
        private void Limpiar()
        {

            TxtBuscar.Clear();
            TxtNombre.Clear();
            TxtDescripcion.Clear();
            TxtCodigo.Clear();
            pnCodigo.BackgroundImage = null;
            btnGuardar.Enabled = true;
            TxtPrecioVenta.Clear();
            TxtStock.Clear();
            TxtImagen.Clear();
            PicImagen.Image = null;
            btnInsertar.Visible = true;
            btnActualizar.Visible = false;
            ErrorIcono.Clear();

            this.RutaDestino = "";
            this.RutaOrigen = "";

            DgvListado.Columns[0].Visible = false;
            btnActivar.Visible = false;
            btnDesactivar.Visible = false;
            btnEliminar.Visible = false;
            chkSeleccionar.Checked = false;
        }
        #endregion
        #region Mensajes de error
        private void MensajeError(string Mensaje)
        {
            MessageBox.Show(Mensaje, "Sistemas de Ventas", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        private void MensajeOK(string Mensaje)
        {
            MessageBox.Show(Mensaje, "Sistemas de Ventas", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        #endregion

        private void CargarCategoria()
        {
            try
            {
                CboCategoria.DataSource = NCategoria.Seleccionar();
                CboCategoria.ValueMember = "idcategoria";
                CboCategoria.DisplayMember = "Nombre";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }

        private void FrmArticulo_Load(object sender, EventArgs e)
        {
            this.Listar();
            this.CargarCategoria();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            this.Buscar();
        }

        private void btnCargarImagen_Click(object sender, EventArgs e)
        {
            OpenFileDialog file = new OpenFileDialog();
            file.Filter = "Image files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png";
            if (file.ShowDialog() == DialogResult.OK)
            {
                PicImagen.Image = Image.FromFile(file.FileName);
                TxtImagen.Text = file.FileName.Substring(file.FileName.LastIndexOf("\\") + 1);
                this.RutaOrigen = file.FileName;

            }
        }

        private void btnGenerar_Click(object sender, EventArgs e)
        {
            Zen.Barcode.Code128BarcodeDraw mGenerarCodeB = Zen.Barcode.BarcodeDrawFactory.Code128WithChecksum;

            pnCodigo.BackgroundImage = mGenerarCodeB.Draw(TxtCodigo.Text, 60);
            btnGuardar.Enabled = true;


        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            Image imgFinla = (Image)pnCodigo.BackgroundImage.Clone();

            SaveFileDialog saveCode = new SaveFileDialog();
            saveCode.AddExtension = true;
            saveCode.Filter = "Image PNG (*.png) | *.png";
            saveCode.ShowDialog();
            if (!string.IsNullOrEmpty(saveCode.FileName))
            {
                imgFinla.Save(saveCode.FileName, ImageFormat.Png);
            }
            imgFinla.Dispose();
        }

        private void btnInsertar_Click(object sender, EventArgs e)
        {
            try
            {
                string Rpta = "";
                if (CboCategoria.Text == string.Empty || TxtNombre.Text == string.Empty || TxtPrecioVenta.Text == string.Empty || TxtStock.Text == string.Empty)
                {
                    MensajeError("Falta ingresar algunos datos, serán remarcados");
                    ErrorIcono.SetError(CboCategoria, "Seleccione una categoria");
                    ErrorIcono.SetError(TxtPrecioVenta, "Ingresa una precio de venta");
                    ErrorIcono.SetError(TxtStock, "Ingresa el stock del articulo");
                    ErrorIcono.SetError(TxtNombre, "Ingrese un nombre");
                }
                else
                {
                    Rpta = NArticulo.Insertar(Convert.ToInt32(CboCategoria.SelectedValue), TxtCodigo.Text.Trim(), TxtNombre.Text.Trim(), Convert.ToDecimal(TxtPrecioVenta.Text), Convert.ToInt32(TxtStock.Text), TxtDescripcion.Text, TxtImagen.Text.Trim());
                    if (Rpta.Equals("OK"))
                    {
                        this.MensajeOK("Se inserto de forma correcta el registro");
                        if (TxtImagen.Text != string.Empty)
                        {
                            this.RutaDestino = this.Directorio + TxtImagen.Text;
                            File.Copy(this.RutaOrigen, RutaDestino);

                        }
                        this.Listar();
                    }
                    else
                    {
                        this.MensajeError(Rpta);

                    }

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }

        private void DgvListado_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try {
                this.Limpiar();
                btnActualizar.Visible = true;
                btnInsertar.Visible = false;
                txtID.Text = DgvListado.CurrentRow.Cells["ID"].Value.ToString();
                CboCategoria.SelectedValue = DgvListado.CurrentRow.Cells["idcategoria"].Value.ToString();
                TxtCodigo.Text = DgvListado.CurrentRow.Cells["Codigo"].Value.ToString();
                this.NombreArticulo = DgvListado.CurrentRow.Cells["Nombre"].Value.ToString();
                TxtNombre.Text = DgvListado.CurrentRow.Cells["Nombre"].Value.ToString();
                TxtPrecioVenta.Text = DgvListado.CurrentRow.Cells["Precio_venta"].Value.ToString();
                TxtStock.Text = DgvListado.CurrentRow.Cells["Cantidad"].Value.ToString();
                TxtDescripcion.Text = DgvListado.CurrentRow.Cells["Descripcion"].Value.ToString();
                string Imagen;
                Imagen = DgvListado.CurrentRow.Cells["Imagen"].Value.ToString();
                if (Imagen != string.Empty)
                {
                    PicImagen.Image = Image.FromFile(this.Directorio + Imagen);
                    TxtImagen.Text = Imagen;
                }
                else {
                    PicImagen.Image = null;
                    TxtImagen.Text = "";  
                }
                TabGeneral.SelectedIndex = 1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Seleccione desde la celda nombre." + " Error: " + ex.Message);
            }
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            try
            {
                string Rpta = "";
                if (txtID.Text == "" || CboCategoria.Text == string.Empty || TxtNombre.Text == string.Empty || TxtPrecioVenta.Text == string.Empty || TxtStock.Text == string.Empty)
                {
                    MensajeError("Falta ingresar algunos datos, serán remarcados");
                    ErrorIcono.SetError(CboCategoria, "Seleccione una categoria");
                    ErrorIcono.SetError(TxtPrecioVenta, "Ingresa una precio de venta");
                    ErrorIcono.SetError(TxtStock, "Ingresa el stock del articulo");
                    ErrorIcono.SetError(TxtNombre, "Ingrese un nombre");
                }
                else
                {
                    Rpta = NArticulo.Actualizar(Convert.ToInt32(txtID.Text), Convert.ToInt32(CboCategoria.SelectedValue), TxtCodigo.Text.Trim(),this.NombreArticulo ,TxtNombre.Text.Trim(), Convert.ToDecimal(TxtPrecioVenta.Text), Convert.ToInt32(TxtStock.Text), TxtDescripcion.Text, TxtImagen.Text.Trim());
                    if (Rpta.Equals("OK"))
                    {
                        this.MensajeOK("Se Actualizo de forma correcta el registro");
                        if (TxtImagen.Text != string.Empty && this.RutaOrigen != string.Empty)
                        {
                            this.RutaDestino = this.Directorio + TxtImagen.Text;
                            File.Copy(this.RutaOrigen, RutaDestino);

                        }
                        this.Listar();
                        TabGeneral.SelectedIndex = 0;
                        
                    }
                    else
                    {
                        this.MensajeError(Rpta);

                    }

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Limpiar();
            TabGeneral.SelectedIndex = 0;
        }

        private void DgvListado_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == DgvListado.Columns["Seleccionar"].Index)
            {
                DataGridViewCheckBoxCell chkEliminar = (DataGridViewCheckBoxCell)DgvListado.Rows[e.RowIndex].Cells["Seleccionar"];
                chkEliminar.Value = !Convert.ToBoolean(chkEliminar.Value);
            }
        }

        private void chkSeleccionar_CheckedChanged(object sender, EventArgs e)
        {
            if (chkSeleccionar.Checked)
            {
                DgvListado.Columns[0].Visible = true;
                btnActivar.Visible = true;
                btnDesactivar.Visible = true;
                btnEliminar.Visible = true;
            }
            else
            {
                DgvListado.Columns[0].Visible = false;
                btnActivar.Visible = false;
                btnDesactivar.Visible = false;
                btnEliminar.Visible = false;

            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult Opcion;
                Opcion = MessageBox.Show("¿Desea eliminar el(los) registro(s)?", "Sistema de ventas", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (Opcion == DialogResult.OK)
                {
                    int codigo;
                    string Rpta = "";
                    string Imagen = "";

                    foreach (DataGridViewRow row in DgvListado.Rows)
                    {
                        if (Convert.ToBoolean(row.Cells[0].Value))
                        {
                            codigo = Convert.ToInt32(row.Cells[1].Value);
                            Imagen = Convert.ToString(row.Cells[9].Value);
                            Rpta = NArticulo.Eliminar(codigo);
                            if (Rpta.Equals("OK"))
                            {
                                this.MensajeOK("Se eliminó el registro:" + row.Cells[5].Value.ToString());
                                File.Delete(this.Directorio + Imagen);
                            }
                            else
                            {
                                this.MensajeError(Rpta);
                            }
                        }
                    }
                    this.Listar();
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }

        private void btnDesactivar_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult Opcion;
                Opcion = MessageBox.Show("¿Desea Desactivar  el(los) registro(s)?", "Sistema de ventas", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (Opcion == DialogResult.OK)
                {
                    int codigo;
                    string Rpta = "";

                    foreach (DataGridViewRow row in DgvListado.Rows)
                    {
                        if (Convert.ToBoolean(row.Cells[0].Value))
                        {
                            codigo = Convert.ToInt32(row.Cells[1].Value);
                            Rpta = NArticulo.DesActivar(codigo);
                            if (Rpta.Equals("OK"))
                            {
                                this.MensajeOK("Se Desactivó el registro:" + row.Cells[2].Value.ToString());
                            }
                            else
                            {
                                this.MensajeError(Rpta);
                            }
                        }
                    }
                    this.Listar();
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }

        private void btnActivar_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult Opcion;
                Opcion = MessageBox.Show("¿Desea Activar  el(los) registro(s)?", "Sistema de ventas", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (Opcion == DialogResult.OK)
                {
                    int codigo;
                    string Rpta = "";

                    foreach (DataGridViewRow row in DgvListado.Rows)
                    {
                        if (Convert.ToBoolean(row.Cells[0].Value))
                        {
                            codigo = Convert.ToInt32(row.Cells[1].Value);
                            Rpta = NArticulo.Activar(codigo);
                            if (Rpta.Equals("OK"))
                            {
                                this.MensajeOK("Se Activo el registro:" + row.Cells[2].Value.ToString());
                            }
                            else
                            {
                                this.MensajeError(Rpta);
                            }
                        }
                    }
                    this.Listar();
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }
    }
}
