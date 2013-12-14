using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MyColors
{
    public partial class SimpleToolTip : Form
    {
        public SimpleToolTip()
        {
            InitializeComponent();
        }

        protected override bool ShowWithoutActivation
        {
            get
            {
                return true;
            }
        }

        public void Show(string text, int x, int y)
        {
            label1.Text = text;
            this.Location = new Point(x, y);
            this.Show();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            LinearGradientBrush brush = new LinearGradientBrush(new Point(e.ClipRectangle.Left, e.ClipRectangle.Top), new Point(e.ClipRectangle.Left, e.ClipRectangle.Bottom),
                Color.White, Color.Silver);

            e.Graphics.FillRectangle(brush, e.ClipRectangle);
            e.Graphics.DrawRectangle(new Pen(Color.FromArgb(89, 120, 132)), e.ClipRectangle.Deflate(new Size(1, 1)));

            base.OnPaint(e);
        }
    }
}
