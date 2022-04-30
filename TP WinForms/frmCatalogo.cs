﻿using System;
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
        }

        private void frmCatalogo_Load(object sender, EventArgs e)
        {
            ArticuloNegocio negocio = new ArticuloNegocio();
            listaArticulos = negocio.listar();
            dgvArticulos.DataSource = listaArticulos;

            dgvArticulos.Columns["Id"].Visible = false;
            dgvArticulos.Columns["Imagen"].Visible = false;

            // <German>
            // Creo que es practico para no llenar de tanta info la ventana principal
            dgvArticulos.Columns["Descripcion"].Visible = false;
            dgvArticulos.Columns["Marca"].Visible = false;
            dgvArticulos.Columns["Categoria"].Visible = false;
            // ---

            cargarImagen(listaArticulos[0].Imagen);
        }

        private void dgvArticulos_SelectionChanged(object sender, EventArgs e)
        {
            Articulo seleccionado = (Articulo)dgvArticulos.CurrentRow.DataBoundItem;
            cargarImagen(seleccionado.Imagen);
        }

        private void cargarImagen(string imagen)
        {
            try
            {
                pbxArticulos.Load(imagen);
            }
            catch (Exception ex)
            {
                pbxArticulos.Load("https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQXJq6u65-ZDLDMCQMHejY3TGV5Vbj-O343pyR1KoVE8lvmTet4TG319R9tPMgSgxKFgjY&usqp=CAU");
            }
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
                detalle.ShowDialog();
            }
        }
    }
}
