using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using negocio;
using dominio;

namespace TP_WinForms
{
    public partial class frmCatalogo : Form
    {

        private List<Articulo> listaArticulos;
        public frmCatalogo()
        {
            InitializeComponent();
            //this.BackColor = Color.Beige;
            this.BackColor = Color.Bisque;
        }

        private void frmCatalogo_Load(object sender, EventArgs e)
        {
            cargar();
            cboCampo.Items.Add("Nombre");
            cboCampo.Items.Add("Descripcion");
            cboCampo.Items.Add("Precio");
        }

        private void dgvArticulos_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvArticulos.CurrentRow != null)
            {
                Articulo seleccionado = (Articulo)dgvArticulos.CurrentRow.DataBoundItem;
                cargarImagen(seleccionado.Imagen);
            } 

        }

        private void cargarImagen(string imagen)
        {
            try
            {
                pbxArticulos.Load(imagen);
            }
            catch (Exception ex)
            {
                pbxArticulos.Load(Diccionario.IMAGE_NOTFOUND);
            }
        }


        private void cargar()
        {
            ArticuloNegocio negocio = new ArticuloNegocio();
            try
            {
                listaArticulos = negocio.listar();
                dgvArticulos.DataSource = listaArticulos;

                ocultarColumnas();
                cargarImagen(listaArticulos[0].Imagen);
            }
            catch (ArgumentOutOfRangeException)
            {
                MessageBox.Show("No hay articulos dados de alta hasta el momento","Atención",MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void ocultarColumnas()
        {
            dgvArticulos.Columns["Id"].Visible = false;
            dgvArticulos.Columns["Imagen"].Visible = false;
            dgvArticulos.Columns["Estado"].Visible = false;

            // <German>
            // Creo que es practico para no llenar de tanta info la ventana principal
            dgvArticulos.Columns["Descripcion"].Visible = false;
            dgvArticulos.Columns["Marca"].Visible = false;
            dgvArticulos.Columns["Categoria"].Visible = false;
        }

        private void btnDetalle_Click(object sender, EventArgs e)
        {
            if (dgvArticulos.CurrentRow is null)
            {
                MessageBox.Show("Seleccione un articulo por favor...");
            }
            else
            {
                Articulo articuloSeleccionado;
                articuloSeleccionado = (Articulo)dgvArticulos.CurrentRow.DataBoundItem;

                frmDetalle detalle = new frmDetalle(articuloSeleccionado);
                
                // <German>
                // Me gustaria que cuando se muestren las ventanas auxiliares, frmCatalogo se oculte.
                // Con this.Hide() se puede hacer, pero despues no puedo mostrar otra vez esta ventana
                detalle.ShowDialog();
            }
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            if (dgvArticulos.CurrentRow is null)
            {
                MessageBox.Show("Seleccione un articulo por favor...");
            }
            else
            {
                Articulo seleccionado = (Articulo)dgvArticulos.CurrentRow.DataBoundItem;
                frmModificar modificar = new frmModificar(seleccionado);

                // <German>
                // Me gustaria que cuando se muestren las ventanas auxiliares, frmCatalogo se oculte.
                // Con this.Hide() se puede hacer, pero despues no puedo mostrar otra vez esta ventana

                modificar.ShowDialog();
            }
            cargar();
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
         
            frmAgregar alta = new frmAgregar();
            alta.ShowDialog();
            cargar();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            ///Eliminacion fisica - martin
            ArticuloNegocio negocio = new ArticuloNegocio();
            Articulo seleccionado;
            try
            {
                DialogResult result = MessageBox.Show("¿De verdad desea eliminar este articulo?", "Eliminando", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);  
                if (result == DialogResult.Yes)
                {
                    seleccionado = (Articulo)dgvArticulos.CurrentRow.DataBoundItem;
                    negocio.eliminar(seleccionado.ID);
                    cargar();

                }
                
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }

        private void btnFiltro_Click(object sender, EventArgs e)
        {
            ArticuloNegocio negocio = new ArticuloNegocio();

            try
            {
                string campo = cboCampo.SelectedItem.ToString();
                string criterio = cboCriterio.SelectedItem.ToString();
                string filtro = txtFiltroAvanzado.Text;

                dgvArticulos.DataSource = negocio.filtrar(campo, criterio, filtro);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnBaja_Click(object sender, EventArgs e)
        {
            ArticuloNegocio negocio = new ArticuloNegocio();
            Articulo seleccionado;
            try
            {
                DialogResult result = MessageBox.Show("¿De verdad desea dar de baja este articulo?", "Baja de articulo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    seleccionado = (Articulo)dgvArticulos.CurrentRow.DataBoundItem;
                    negocio.baja(seleccionado.ID);
                    cargar();

                }

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }

        private void btnVerArtBaja_Click(object sender, EventArgs e)
        {
            frmArticulosBaja baja = new frmArticulosBaja();
            baja.ShowDialog();
            cargar();
        }

        private void txtFiltro_TextChanged(object sender, EventArgs e)
        {
            List<Articulo> listaFiltrada;
            string filtro = txtFiltro.Text;
            if (filtro != "")
            {
                listaFiltrada = listaArticulos.FindAll(x => x.Nombre.ToUpper().Contains(filtro.ToUpper()) ||
                x.Marca.Descripcion.ToUpper().Contains(filtro.ToUpper()) ||
                x.Categoria.Descripcion.ToUpper().Contains(filtro.ToUpper()) ||
                x.Codigo.ToUpper().Contains(filtro.ToUpper()));
            }
            else
            {
                listaFiltrada = listaArticulos;
            }
            dgvArticulos.DataSource = null;
            dgvArticulos.DataSource = listaFiltrada;
            ocultarColumnas();
        }

        private void cboCampo_SelectedIndexChanged(object sender, EventArgs e)
        {
            string opcion = cboCampo.SelectedItem.ToString();
            if (opcion == "Precio")
            {
                cboCriterio.Items.Clear();
                cboCriterio.Items.Add("Mayor a");
                cboCriterio.Items.Add("Menor a");
                cboCriterio.Items.Add("Igual a");
            }
            else
            {
                cboCriterio.Items.Clear();
                cboCriterio.Items.Add("Comienza con");
                cboCriterio.Items.Add("Termina con");
                cboCriterio.Items.Add("Contiene");
            }
        }
    }
}
