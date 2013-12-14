using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace MyColors
{
    public static class Extensions
    {
        public static Rectangle Deflate(this Rectangle r, Size amount)
        {
            r.Size -= amount;
            return r;
        }
    }
}
