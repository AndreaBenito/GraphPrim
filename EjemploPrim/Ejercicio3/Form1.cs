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

        private List<VisualVertex> visualVertices = new List<VisualVertex>();
        private List<VisualEdge> visualEdges = new List<VisualEdge>();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void panelCanvas_MouseClick(object sender, MouseEventArgs e)
        {
            string nombre = txtNodo.Text;
            if (string.IsNullOrWhiteSpace(nombre))
            {
                int i = 1;
                while (grafo1.VERTICES.Any(v => v.dato == $"V{i}")) i++;
                nombre = $"V{i}";
            }
            if (grafo1.VERTICES.Any(v => v.dato == nombre))
            {
                MessageBox.Show("Ya existe un vértice con ese nombre. Cambie el nombre o use otro.");
                return;
            }

            grafo1.AgregarVertice(nombre);

            visualVertices.Add(new VisualVertex(nombre, e.Location));

            grafo1.MostrarListaAdyacencia(dgvNodos);
            grafo1.MostrarMatrizAdyacencia(dgvMatriz);

            txtNodo.Clear();

            panelCanvas.Invalidate();
        }

        private void panelCanvas_Paint(object sender, PaintEventArgs e)
        {
            var g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            foreach (var edge in visualEdges)
            {
                if (edge.IsMST)
                {
                    edge.Color = Color.Green;
                }
                else
                {
                    edge.Color = Color.Black;
                }
                edge.Draw(g);
            }

            foreach (var v in visualVertices)
            {
                v.Draw(g);
            }
        }

        private void btnCrearNodo_Click(object sender, EventArgs e)
        {
            string Dato = txtNodo.Text;

            if (!string.IsNullOrEmpty(Dato))
            {
                if (grafo1.VERTICES.Any(v => v.dato == Dato))
                {
                    MessageBox.Show("Ya existe un vértice con ese nombre");
                    return;
                }

                grafo1.AgregarVertice(Dato);

                Point pos = new Point(30 + visualVertices.Count * 40 % (panelCanvas.Width - 40), 30 + (visualVertices.Count / 10) * 60);
                visualVertices.Add(new VisualVertex(Dato, pos));

                txtNodo.Clear();

                grafo1.MostrarListaAdyacencia(dgvNodos);
                grafo1.MostrarMatrizAdyacencia(dgvMatriz);
                panelCanvas.Invalidate();
            }
            else
            {
                MessageBox.Show("Por favor, ingrese un dato válido para el vértice.");
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

                var va = visualVertices.Find(v => v.Nombre == Origen);
                var vb = visualVertices.Find(v => v.Nombre == Destino);

                if (va != null && vb != null)
                {
                    bool existe = visualEdges.Any(eu => (eu.A == va && eu.B == vb) || (eu.A == vb && eu.B == va));
                    if (!existe)
                        visualEdges.Add(new VisualEdge(va, vb, peso));
                }

                txtOrigen.Clear();
                txtDestino.Clear();
                txtPeso.Clear();
                grafo1.MostrarListaAdyacencia(dgvNodos);
                grafo1.MostrarMatrizAdyacencia(dgvMatriz);

                panelCanvas.Invalidate();
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

            foreach (var edge in visualEdges)
            {
                edge.IsMST = false;
                edge.Color = Color.Black;
            }

            foreach (var ar in grafo1.PrimEdges)
            {
                var match = visualEdges.FirstOrDefault(ve =>
                    (ve.A.Nombre == ar.origen && ve.B.Nombre == ar.destino) ||
                    (ve.A.Nombre == ar.destino && ve.B.Nombre == ar.origen));

                if (match != null)
                {
                    match.IsMST = true;
                    match.Color = Color.Green;
                }
                else
                {
                    var va = visualVertices.Find(v => v.Nombre == ar.origen);
                    var vb = visualVertices.Find(v => v.Nombre == ar.destino);
                    if (va != null && vb != null)
                    {
                        var nueva = new VisualEdge(va, vb, ar.peso) { IsMST = true, Color = Color.Green };
                        visualEdges.Add(nueva);
                    }
                }
            }

            panelCanvas.Invalidate();
        }
    }
}