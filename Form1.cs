using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace MyColors
{
    public partial class Form1 : Form
    {
        Color selectedColor;
        byte red, green, blue = 0;

        bool ignoreUpdate = false;

        public Form1()
        {
            InitializeComponent();

            selectedColor = Color.White;
            panel1.BackColor = selectedColor;

            colorList1.ColorSelected += new ColorList.ColorSelectedEventHandler(colorList1_ColorSelected);
            string path = Path.Combine(Application.ExecutablePath, "MyColors.list");

            if (File.Exists(path))
            {
                ColorListFile clf = new ColorListFile(path);
                IEnumerable<Color> colors = clf.Load();

                colorList1.Colors.AddRange(colors);
                colorList1.Refresh();
            }
        }

        void colorList1_ColorSelected(Color color)
        {
            selectedColor = color;

            ignoreUpdate = true;

            trackBar1.Value = color.R;
            trackBar2.Value = color.G;
            trackBar3.Value = color.B;

            ignoreUpdate = false;

            panel1.BackColor = selectedColor;
        }

        public void UpdateColor()
        {
            if (!ignoreUpdate)
            {
                selectedColor = Color.FromArgb(red, green, blue);
                panel1.BackColor = selectedColor;
            }
        }

        public void SaveColorList()
        {
            ColorListFile clf = new ColorListFile(Path.Combine(Application.ExecutablePath, "MyColors.list"));
            clf.Save(colorList1.Colors);
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            //SaveColorList();

            base.OnClosing(e);
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            red = Convert.ToByte(trackBar1.Value);
            label1.Text = string.Format("Red : {0}", red);

            UpdateColor();
        }

        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            green = Convert.ToByte(trackBar2.Value);
            label2.Text = string.Format("Green : {0}", green);

            UpdateColor();
        }

        private void trackBar3_Scroll(object sender, EventArgs e)
        {
            blue = Convert.ToByte(trackBar3.Value);
            label3.Text = string.Format("Blue : {0}", blue);

            UpdateColor();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Random r = new Random(DateTime.Now.Millisecond);
            red = Convert.ToByte(r.Next(255));
            green = Convert.ToByte(r.Next(255));
            blue = Convert.ToByte(r.Next(255));

            ignoreUpdate = true;

            trackBar1.Value = red;
            trackBar2.Value = green;
            trackBar3.Value = blue;

            ignoreUpdate = false;

            UpdateColor();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            colorList1.Colors.Add(selectedColor);
            colorList1.Refresh();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            colorList1.Colors.Clear();
            colorList1.Refresh();
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string file = saveFileDialog1.FileName;

                ColorListFile clf = new ColorListFile(file);
                clf.Save(colorList1.Colors);
            }
        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string file = openFileDialog1.FileName;

                ColorListFile clf = new ColorListFile(file);
                IEnumerable<Color> colors = clf.Load();

                colorList1.Colors.Clear();
                colorList1.Colors.AddRange(colors);
                colorList1.Refresh();
            }
        }
    }
}
