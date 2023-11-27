using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ejercitacion_con_discos
{
    public partial class frmDiscos : Form
    {
        private List<Discos> listaDiscos;
        public frmDiscos()
        {
            InitializeComponent();
        }

        private void frmDiscos_Load(object sender, EventArgs e)
        {
            DiscosNegocio negocio = new DiscosNegocio();
            listaDiscos = negocio.listarDiscos();
            dgvDiscos.DataSource = listaDiscos;
            dgvDiscos.Columns["UrlImagen"].Visible = false;
            cargarImagen(listaDiscos[0].UrlImagen);
            AjustarAnchoColumna();
        }

        private void cargarImagen(string imagen)
        {
            try
            {
                pbxDisco.Load(imagen);
            }
            catch (Exception)
            {

                pbxDisco.Load("https://www.gaithersburgdental.com/wp-content/uploads/2016/10/orionthemes-placeholder-image.png");
            }
        }

        private void dgvDiscos_SelectionChanged(object sender, EventArgs e)
        {
            Discos seleccionado = (Discos)dgvDiscos.CurrentRow.DataBoundItem;
            cargarImagen(seleccionado.UrlImagen);

        }

        private void AjustarAnchoColumna()
        {
            int anchoDeseado = 110;
            if (dgvDiscos.ColumnCount >= 2)
            {
                dgvDiscos.Columns[1].Width = anchoDeseado;
            }
        }
    }
}
