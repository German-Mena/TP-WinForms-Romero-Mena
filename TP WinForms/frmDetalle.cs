using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using dominio;
using negocio;

namespace TP_WinForms
{
    public partial class frmDetalle : Form
    {
        private Articulo articulo = null;

        public frmDetalle(Articulo articulo)
        {
            InitializeComponent();
            this.articulo = articulo;

            txtCodigo.ReadOnly = true;
            txtNombre.ReadOnly = true;
            txtDescripcion.ReadOnly = true;
            txtMarca.ReadOnly = true;
            txtCategoria.ReadOnly = true;
            txtPrecio.ReadOnly = true;

            //this.BackColor = Color.Beige;
            this.BackColor = Color.Bisque;
        }

        private void frmDetalle_Load(object sender, EventArgs e)
        {
            txtCodigo.Text = articulo.Codigo;
            txtNombre.Text = articulo.Nombre;
            txtDescripcion.Text = articulo.Descripcion;
            txtMarca.Text = articulo.Marca.Descripcion;
            txtCategoria.Text = articulo.Categoria.Descripcion;
            txtPrecio.Text = articulo.Precio.ToString();
            cargarImagen(articulo.Imagen);
        }

        private void cargarImagen(string imagen)
        {
            try
            {
                pbxArticulo.Load(imagen);
            }
            catch (Exception ex)
            {
                pbxArticulo.Load(Diccionario.IMAGE_NOTFOUND);
            }
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
