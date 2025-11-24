using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ejercicio3
{
    internal class VisualEdge
    {
        public VisualVertex A { get; set; }
        public VisualVertex B { get; set; }
        public int Peso { get; set; }
        public Color Color { get; set; } = Color.Black;
        public bool IsMST { get; set; } = false;

        public VisualEdge(VisualVertex a, VisualVertex b, int peso)
        {
            A = a;
            B = b;
            Peso = peso;
        }

        public void Draw(Graphics g)
        {
            using (var pen = new Pen(Color, IsMST ? 4f : 2f))
            {
                g.DrawLine(pen, A.Posicion, B.Posicion);
            }

            var mx = (A.Posicion.X + B.Posicion.X) / 2;
            var my = (A.Posicion.Y + B.Posicion.Y) / 2;
            var sf = new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };
            g.DrawString(Peso.ToString(), SystemFonts.DefaultFont, Brushes.Black, new PointF(mx, my), sf);
        }

        public bool MatchesNames(string nameA, string nameB, int peso)
        {
            return Peso == peso && ((A.Nombre == nameA && B.Nombre == nameB) || (A.Nombre == nameB && B.Nombre == nameA));
        }
    }
}
