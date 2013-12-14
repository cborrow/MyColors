using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace MyColors
{
    public class ColorList : Control
    {
        public delegate void ColorSelectedEventHandler(Color color);
        public event ColorSelectedEventHandler ColorSelected;

        List<Color> colors;
        public List<Color> Colors
        {
            get { return colors; }
        }

        public ColorList()
        {
            colors = new List<Color>();
            ColorSelected = new ColorSelectedEventHandler(OnColorSelected);
        }

        protected void OnColorSelected(Color color)
        {

        }

        protected override void OnMouseClick(MouseEventArgs e)
        {
            int index = (e.X / 27) + ((e.Y / 27) * (this.Width / 27));

            if (index >= 0 && index < colors.Count)
                ColorSelected(colors[index]);
            base.OnMouseClick(e);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            int x = 0;
            int y = 0;

            if (colors.Count > 0)
            {
                for (int i = 0; i < colors.Count; i++)
                {
                    Rectangle rect = new Rectangle(x, y, 25, 25);
                    e.Graphics.FillRectangle(new SolidBrush(colors[i]), rect);
                    e.Graphics.DrawRectangle(Pens.Black, rect);

                    x += 27;

                    if ((x + 25) >= this.Width)
                    {
                        x = 0;
                        y += 27;
                    }
                }
            }

            base.OnPaint(e);
        }
    }
}
