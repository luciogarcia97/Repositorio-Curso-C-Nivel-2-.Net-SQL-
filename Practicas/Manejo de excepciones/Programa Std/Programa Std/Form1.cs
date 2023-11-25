using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Programa_Std
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnCalcular_Click(object sender, EventArgs e)
        {
            int resultado;
            try
            {
                resultado = calcular();
                lblResultado.Text = "= " + resultado;

            }
            catch (Exception)
            {
                MessageBox.Show("Error cualquiera, revise el programa");
            }
            finally
            {
                // instrucciones => Se ejecuta si o si, no importa si paso el try o el catch
                //operacion sensible... para evitar la perdida de datos ya que si falla en el try se puede romper la base
            }
        }

        private int calcular()
        {
            int a, b, resultado;
            try
            {
                a = int.Parse(txtb1.Text);
                b = int.Parse(txtb2.Text);

                resultado = a / b;
                return resultado;
            }
            catch (Exception ex)
            {
                // guardar el registro de error en el archivo...
                throw ex;
            }
        }
    }
}
