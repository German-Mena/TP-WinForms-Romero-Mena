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

    public partial class frmModificar : Form
    {
        private Articulo articulo = null;

        public frmModificar(Articulo articulo)
        {
            InitializeComponent();
            this.articulo = articulo;
            this.Text = "Modificar articulo";
            this.BackColor = Color.Bisque;

            cboMarca.DropDownStyle = ComboBoxStyle.DropDownList;
            cboCategoria.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void frmModificar_Load(object sender, EventArgs e)
        {
            CategoriaNegocio categoriaNegocio = new CategoriaNegocio();
            MarcaNegocio marcaNegocio = new MarcaNegocio();

            try
            {
                cboCategoria.DataSource = categoriaNegocio.listar();
                cboCategoria.ValueMember = "ID";
                cboCategoria.DisplayMember = "Descripcion";

                cboMarca.DataSource = marcaNegocio.listar();
                cboMarca.ValueMember = "ID";
                cboMarca.DisplayMember = "Descripcion";

                if (articulo != null)
                {
                    txtCodigo.Text = articulo.Codigo;
                    txtNombre.Text = articulo.Nombre;
                    txtDescripcion.Text = articulo.Descripcion;
                    txtImagen.Text = articulo.Imagen;
                    txtPrecio.Text = articulo.Precio.ToString();
                    cargarImagen(articulo.Imagen);
                    cboMarca.SelectedValue = articulo.Marca.ID;
                    cboCategoria.SelectedValue = articulo.Categoria.ID;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public void cargarImagen(string imagen)
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

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private bool validarModificar()
        {
            if (!(verificarCodigo(txtCodigo.Text)))
            {
                MessageBox.Show("Por favor, escriba un codigo apropiado (Primer caracter letra, segundo y tercer caracter numeros");
                return true;
            }

            if (!(soloNumeros(txtPrecio.Text)))
            {
                MessageBox.Show("Por favor, ingrese solo numeros en el campo [Precio] para poder realizar la siguiente operacion");
                return true;
            }
            return false;
        }

        private bool verificarCodigo(string aleer)
        {
            int i = 0;
            foreach (char car in aleer)
            {
                i++;
                if ((i == 1) && (!(char.IsLetter(car)))) return false;
                if ((i > 1) && (!(char.IsNumber(car)))) return false;
                if (i > 3) return false;
            }
            return true;
        }

        private bool soloNumeros(string aleer)
        {
            bool coma = false;
            foreach (char c in aleer)
            {
                if ((c == ',') && (coma == false))
                {
                    coma = true;
                    continue;
                }
                else if ((c == ',') &&(coma == true))
                {
                    return false;
                }
                else if (!(char.IsNumber(c))) return false;

            }
            return true;
        }



        private void btnAceptar_Click(object sender, EventArgs e)
        {
            ArticuloNegocio negocio = new ArticuloNegocio();

            if (validarModificar())
            {
                return;
            }

            try
            {
                // if (articulo == null) articulo = new Articulo();

                articulo.Codigo = txtCodigo.Text.ToUpper();
                articulo.Nombre = txtNombre.Text;
                articulo.Descripcion = txtDescripcion.Text;

                articulo.Marca = new Marca();
                articulo.Marca = (Marca)cboMarca.SelectedItem;

                articulo.Categoria = new Categoria();
                articulo.Categoria = (Categoria)cboCategoria.SelectedItem;

                articulo.Imagen = txtImagen.Text;

                decimal precio;
                if (!decimal.TryParse(txtPrecio.Text, out precio))
                    MessageBox.Show("El precio del articulo debe ser un numero!");
                else
                {
                    articulo.Precio = precio;
                    negocio.modificar(articulo);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                this.Close();
            }
        }

        private void txtImagen_Leave(object sender, EventArgs e)
        {
            cargarImagen(txtImagen.Text);
        }
    }
}
