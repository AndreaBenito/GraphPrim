using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ejercicio3
{
    public partial class Form1 : Form
    {
        Grafo grafo1 = new Grafo();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnCrearNodo_Click(object sender, EventArgs e)
        {
            string Dato = txtNodo.Text;

            if (!string.IsNullOrEmpty(Dato))
            {
                grafo1.AgregarVertice(Dato);
                
                txtNodo.Clear();

                grafo1.MostrarListaAdyacencia(dgvNodos);
                grafo1.MostrarMatrizAdyacencia(dgvMatriz);

               
            }
            else
            {
                MessageBox.Show("Por favor, ingrese un dato válido para el vertice.");
            }
        }

        private void btnCrearAristaDirigida_Click(object sender, EventArgs e)
        {
            string Origen = txtOrigen.Text;
            string Destino = txtDestino.Text;
            int peso = string.IsNullOrEmpty(txtPeso.Text) ? 1 : int.Parse(txtPeso.Text);

            if (!string.IsNullOrEmpty(Origen) && !string.IsNullOrEmpty(Destino))
            {
                grafo1.AgregarAristas(Origen, Destino, peso);
                MessageBox.Show("Arista creada exitosamente.");

                txtOrigen.Clear();
                txtDestino.Clear();
                txtPeso.Clear();
                grafo1.MostrarListaAdyacencia(dgvNodos);
                grafo1.MostrarMatrizAdyacencia(dgvMatriz);


            }
            else
            {
                MessageBox.Show("Por favor, ingrese datos válidos para el origen y destino de la arista.");
            }
        }

        private void btnAplicarPrim_Click(object sender, EventArgs e)
        {
            grafo1.Prim();
            grafo1.MostrarMatrizPrim(dgvPrim);
        }
    }
}
