using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using dominio;
using negocio;

namespace Pokemons
{
    public partial class frmAltaPokemon : Form
    {
        private Pokemon pokemon = null;
        public frmAltaPokemon()
        {
            InitializeComponent();
        }
        public frmAltaPokemon(Pokemon pokemon)
        {
            InitializeComponent();
            this.pokemon = pokemon;
            Text = "Modificar Pokemon";
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            PokemonNegocio negocio = new PokemonNegocio();
            try
            {
                if (pokemon == null) pokemon = new Pokemon(); // Con esto confirmamos que el pokemon existe, sino existe es porque lo estamos cargando
                
                pokemon.Numero = int.Parse(tbxNumero.Text);
                pokemon.Nombre = tbxNombre.Text;
                pokemon.Descripcion = tbxDescripcion.Text;
                pokemon.UrlImagen = tbxUrl.Text;
                pokemon.Tipo = (Elemento)cbxSeleccionTipo.SelectedItem;
                pokemon.Debilidad = (Elemento)cbxSeleccionDebilidad.SelectedItem;

                if(pokemon.Id != 0)
                {
                negocio.modificar(pokemon);
                MessageBox.Show("Modificado exitosamente");
                }
                else
                {
                negocio.agregar(pokemon);
                MessageBox.Show("Agregado exitosamente");
                }
                
                Close();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }

        private void frmAltaPokemon_Load(object sender, EventArgs e)
        {
            ElementoNegocio elementoNegocio = new ElementoNegocio();
            try
            {
                cbxSeleccionTipo.DataSource = elementoNegocio.listar();
                cbxSeleccionTipo.ValueMember = "Id";
                cbxSeleccionTipo.DisplayMember= "Descripcion";

                cbxSeleccionDebilidad.DataSource = elementoNegocio.listar();
                cbxSeleccionDebilidad.ValueMember = "Id";
                cbxSeleccionDebilidad.DisplayMember = "Descripcion";


                //Validamos si el pokemon es vacio.
                if (pokemon != null)
                {
                    //Con esto precargamos todos los datos del pokemon seleccionado
                    tbxNumero.Text = pokemon.Numero.ToString();
                    tbxNombre.Text = pokemon.Nombre; ;
                    tbxDescripcion.Text = pokemon.Descripcion;
                    tbxUrl.Text = pokemon.UrlImagen;
                    cargarImagen(pokemon.UrlImagen); //ESTO ES PARA QUE CARGUE LA IMAGEN AL MOMENTO DE ENTRAR AL MODIFICAR
                    cbxSeleccionTipo.SelectedValue = pokemon.Tipo.Id;
                    cbxSeleccionDebilidad.SelectedValue = pokemon.Debilidad.Id;
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }

        private void tbxUrl_Leave(object sender, EventArgs e)
        {
            cargarImagen(tbxUrl.Text);
        }

        private void cargarImagen(string imagen)
        {
            try
            {
                pbxPokemonCargar.Load(imagen);
            }
            catch (Exception ex)
            {
                pbxPokemonCargar.Load("https://uning.es/wp-content/uploads/2016/08/ef3-placeholder-image.jpg");
            }
        }

        private void btnAgregarImagen_Click(object sender, EventArgs e)
        {
            OpenFileDialog archivo = new OpenFileDialog();
            archivo.Filter = "jpg|*.jpg";
            if (archivo.ShowDialog() == DialogResult.OK)
            {
                tbxUrl.Text = archivo.FileName;
                cargarImagen(archivo.FileName);

                //guardo la imagen
                
            }
        }
    }
}
