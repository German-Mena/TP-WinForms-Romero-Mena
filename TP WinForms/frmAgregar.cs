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
    public partial class frmAgregar : Form
    {
        public frmAgregar()
        {
            this.BackColor = Color.Bisque;
            InitializeComponent();

            cboMarca.DropDownStyle = ComboBoxStyle.DropDownList;
            cboCategoria.DropDownStyle = ComboBoxStyle.DropDownList;
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

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private bool validarAgregar()
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
            foreach(char car in aleer)
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
            foreach (char c in aleer)
            {
                if (!(char.IsNumber(c))) return false;

            }
            return true;
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {

            if (validarAgregar())
            {
                return;
            }

            Articulo arti = new Articulo();
            ArticuloNegocio negocio = new ArticuloNegocio();
            //AccesoDatos datos1 = new AccesoDatos();
            //AccesoDatos datos2 = new AccesoDatos();
            try
            {
                arti.Nombre = txtNombre.Text;
                arti.Codigo = txtCodigo.Text;
                arti.Descripcion = txtDescripcion.Text;

                arti.Marca = new Marca();
                arti.Marca = (Marca)cboMarca.SelectedItem;

                // <German>
                // Reemplazo codigo para capturar ID de marca desde dropDownList

                //datos1.setearConsulta(Diccionario.LISTAR_MARCAS);
                //datos1.ejecutarLectura();
                //while (datos1.Lector.Read())
                //{
                //    if ((int)datos1.Lector["id"] == int.Parse(txtIDMarca.Text))
                //    {
                //        arti.Marca.ID = int.Parse(txtIDMarca.Text);
                //        arti.Marca.Descripcion = (string)datos1.Lector["nombreMarca"];
                //        break;
                //    }
                //}
                //datos1.cerrarConexion();

                arti.Categoria = new Categoria();
                arti.Categoria = (Categoria)cboCategoria.SelectedItem;

                // <German>
                // Reemplazo codigo para capturar ID de Categoria desde dropDownList

                //datos2.setearConsulta(Diccionario.LISTAR_CATEGORIAS);
                //datos2.ejecutarLectura();
                //while (datos2.Lector.Read())
                //{
                //    if ((int)datos2.Lector["id"] == int.Parse(txtIDCategoria.Text))
                //    {
                //        arti.Categoria.ID = int.Parse(txtIDCategoria.Text);
                //        arti.Categoria.Descripcion = (string)datos2.Lector["nombreCategoria"];
                //        break;
                //    }
                //}
                //datos2.cerrarConexion();

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
            MarcaNegocio marcaNegocio = new MarcaNegocio();
            CategoriaNegocio categoriaNegocio = new CategoriaNegocio();

            try
            {
                cboMarca.DataSource = marcaNegocio.listar();
                cboMarca.ValueMember = "ID";
                cboMarca.DisplayMember = "Descripcion";

                cboCategoria.DataSource = categoriaNegocio.listar();
                cboCategoria.ValueMember = "ID";
                cboCategoria.DisplayMember = "Descripcion";

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }

        private void txtURLImagen_Leave(object sender, EventArgs e)
        {
            cargarImagen(txtURLImagen.Text);
        }


    }
}
