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

        SimpleToolTip toolTip;
        int colorSize = 27;

        List<Color> colors;
        public List<Color> Colors
        {
            get { return colors; }
        }

        public ColorList()
        {
            colors = new List<Color>();
            ColorSelected = new ColorSelectedEventHandler(OnColorSelected);

            toolTip = new SimpleToolTip();
        }

        public int GetIndexAtPoint(int x, int y)
        {
            int index = (x / colorSize) + ((y / colorSize) * (this.Width / colorSize));
            return index;
        }

        public Color GetColorAtPoint(int x, int y)
        {
            int index = GetIndexAtPoint(x, y);

            if (index >= 0 && index < colors.Count)
                return colors[index];
            return Color.Empty;
        }

        protected void OnColorSelected(Color color)
        {

        }

        protected override void OnMouseClick(MouseEventArgs e)
        {
            int index = GetIndexAtPoint(e.X, e.Y);

            if (index >= 0 && index < colors.Count)
                ColorSelected(colors[index]);
            base.OnMouseClick(e);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            Rectangle rect = new Rectangle(e.X, e.Y, 1, 1);
            int index = GetIndexAtPoint(e.X, e.Y);

            Point p = new Point(e.X, e.Y);
            p = this.PointToScreen(p);
            p.Offset(new Point(15, 15));

            if (index >= 0 && index < colors.Count)
            {
                Color c = colors[index];
                string text = string.Format("R: {0}, G: {1}, B: {2}, HEX: {3}", c.R, c.G, c.B, ColorTranslator.ToHtml(c));
                toolTip.Show(text, p.X, p.Y);
            }
            else
                toolTip.Hide();

            base.OnMouseMove(e);
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            toolTip.Hide();
            base.OnMouseLeave(e);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            int x = 2;
            int y = 0;

            if (colors.Count > 0)
            {
                for (int i = 0; i < colors.Count; i++)
                {
                    Rectangle rect = new Rectangle(x, y, 25, 25);
                    e.Graphics.FillRectangle(new SolidBrush(colors[i]), rect);
                    e.Graphics.DrawRectangle(Pens.Black, rect);

                    x += colorSize;

                    if ((x + 25) >= this.Width)
                    {
                        x = 2;
                        y += colorSize;
                    }
                }
            }
            else
            {
                TextRenderer.DrawText(e.Graphics, "Empty color pallete :(", new Font(FontFamily.GenericSansSerif, 16.75f, FontStyle.Regular), e.ClipRectangle,
                    this.ForeColor, Color.Transparent, TextFormatFlags.VerticalCenter | TextFormatFlags.HorizontalCenter);
            }

            base.OnPaint(e);
        }
    }
}
