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
    public partial class frmAltaArticulo : Form
    {
        public frmAltaArticulo()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            Articulo arti = new Articulo();
            ArticuloNegocio negocio = new ArticuloNegocio();
            AccesoDatos datos1 = new AccesoDatos();
            AccesoDatos datos2 = new AccesoDatos();
            try
            {
                arti.Nombre = txtNombre.Text;
                arti.Codigo = txtCodigo.Text;
                arti.Descripcion = txtDescripcion.Text;
                arti.Marca = new Marca();

                datos1.setearConsulta(Diccionario.LISTAR_MARCAS);
                datos1.ejecutarLectura();
                while (datos1.Lector.Read())
                {
                    if ((int)datos1.Lector["id"] == int.Parse(txtIDMarca.Text))
                    {
                        arti.Marca.ID = int.Parse(txtIDMarca.Text);
                        arti.Marca.Descripcion = (string)datos1.Lector["nombreMarca"];
                        break;
                    }
                }
                datos1.cerrarConexion();

                arti.Categoria = new Categoria();

                datos2.setearConsulta(Diccionario.LISTAR_CATEGORIAS);
                datos2.ejecutarLectura();
                while (datos2.Lector.Read())
                {
                    if ((int)datos2.Lector["id"] == int.Parse(txtIDCategoria.Text))
                    {
                        arti.Categoria.ID = int.Parse(txtIDCategoria.Text);
                        arti.Categoria.Descripcion = (string)datos2.Lector["nombreCategoria"];
                        break;
                    }
                }
                datos2.cerrarConexion();
                
                arti.Imagen = txtURLImagen.Text;
                arti.Precio = Decimal.Parse(txtPrecio.Text);

                negocio.agregar(arti);
                MessageBox.Show("Agregado con exito!");
                Close();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }

        private void frmAltaArticulo_Load(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void txtPrecio_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtIDCategoria_TextChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void txtIDMarca_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtDescripcion_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void txtNombre_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
