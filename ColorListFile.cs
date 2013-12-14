using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;
using System.Drawing;

namespace MyColors
{
    public class ColorListFile
    {
        string filePath;

        public ColorListFile()
        {
            filePath = "MyColors.list";
        }

        public ColorListFile(string file)
        {
            filePath = file;
        }

        public IEnumerable<Color> Load()
        {
            if (!File.Exists(filePath))
                throw new FileNotFoundException();

            List<Color> colors = new List<Color>();

            XmlDocument doc = new XmlDocument();
            doc.Load(filePath);

            XmlElement root = doc.DocumentElement;
            XmlNodeList nodes = root.GetElementsByTagName("Color");

            foreach (XmlNode n in nodes)
            {
                int red = 0, green = 0, blue = 0;

                if(n.Attributes["red"] != null)
                    red = Convert.ToInt32(n.Attributes["red"].Value);
                if(n.Attributes["green"] != null)
                    green = Convert.ToInt32(n.Attributes["green"].Value);
                if(n.Attributes["blue"] != null)
                    blue = Convert.ToInt32(n.Attributes["blue"].Value);

                Color c = Color.FromArgb(red, green, blue);
                colors.Add(c);
            }

            return colors;
        }

        public IEnumerable<Color> Load(string path)
        {
            filePath = path;
            return Load();
        }

        public void Save(IEnumerable<Color> colors)
        {
            StreamWriter sw = new StreamWriter(filePath);
            sw.WriteLine("<?xml version=\"1.0\" encoding=\"utf-8\"?>");
            sw.WriteLine("<MyColors>");

            foreach (Color c in colors)
            {
                sw.WriteLine(string.Format("\t<Color red=\"{0}\" green=\"{1}\" blue=\"{2}\" />", c.R, c.G, c.B));
            }

            sw.WriteLine("</MyColors>");
            sw.Close();
        }

        public void Save(IEnumerable<Color> colors, string file)
        {
            filePath = file;
            Save(colors);
        }
    }
}
