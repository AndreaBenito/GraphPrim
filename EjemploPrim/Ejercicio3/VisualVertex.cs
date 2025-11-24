using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Runtime.InteropServices;

namespace Ejercicio3
{
    internal class VisualVertex
    {
        public string Nombre { get; set; }
        public Point Posicion { get; set; }
        public int Radio { get; set; } = 24;
        public Rectangle Bounds => new Rectangle(Posicion.X - Radio, Posicion.Y - Radio, Radio * 2, Radio * 2);

        public VisualVertex(string nombre, Point pos)
        {
            Nombre = nombre;
            Posicion = pos;
        }

        public void Draw(Graphics g)
        {
            g.FillEllipse(SystemBrushes.ControlLight, Bounds);
            g.DrawEllipse(Pens.Black, Bounds);

            var sf = new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };
            g.DrawString(Nombre, SystemFonts.DefaultFont, Brushes.Black, new PointF(Posicion.X, Posicion.Y), sf);
        }

        public bool HitTest(Point p)
        {
            var dx = p.X - Posicion.X;
            var dy = p.Y - Posicion.Y;
            return dx * dx + dy * dy <= Radio * Radio;
        }
    }
}
