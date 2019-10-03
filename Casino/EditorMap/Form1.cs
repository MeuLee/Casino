using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace EditorMap
{
    public partial class Editor : Form
    {
        private Tile[,] tile;
        private double bigger, tileSize;
        private Color colorToDraw;
        private string btnText;
        
        public Editor()
        {
            InitializeComponent();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            double width = double.Parse(TxtWidth.Text),
                height = double.Parse(TxtHeight.Text);
            tile = new Tile[Convert.ToInt32(width), Convert.ToInt32(height)];
            bigger = width > height ? width : height;

            Controls.Clear();
            DrawTileMap();
            AddButtonsToggleColor();
        }

        private void AddButtonsToggleColor()
        {
            CreateButton(15, Color.Red, "Floor1");
            CreateButton(45, Color.Gray, "Floor2");
            CreateButton(75, Color.Brown, "Table1");
            CreateButton(105, Color.Green, "Table2");
            CreateButton(135, Color.Yellow, "SlotMachine");
            CreateButton(165, Color.Blue, "Bar");
            CreateButton(195, SystemColors.Control, "Undo");
            CreateXmlButton();
        }

        private void CreateXmlButton()
        {
            Button xml = new Button()
            {
                Text = "Generate xml",
                Location = new Point(850, 225)
            };
            xml.Click += Xml_Click;
            Controls.Add(xml);
        }

        private void Xml_Click(object sender, EventArgs e)
        {
            XmlWriter writer = XmlWriter.Create("map.xml", new XmlWriterSettings() { Indent = true });
            writer.WriteStartDocument();

            writer.WriteStartElement("map");
                writer.WriteStartElement("Dimensions");
                    writer.WriteStartElement("Width");
                        writer.WriteAttributeString("X", tile.GetLength(0).ToString());
                        writer.WriteEndElement();
                        writer.WriteStartElement("Height");
                        writer.WriteAttributeString("Y", tile.GetLength(1).ToString());
                    writer.WriteEndElement();
                writer.WriteEndElement();
                writer.WriteStartElement("Tiles");
                    WriteTilesToXml(writer);
                writer.WriteEndElement();
            writer.WriteEndElement();
            writer.WriteEndDocument();
            writer.Close();
        }

        private void WriteTilesToXml(XmlWriter writer)
        {
            for (int i = 0; i < tile.GetLength(0); i++)
            {
                for (int j = 0; j < tile.GetLength(1); j++)
                {
                    writer.WriteStartElement("Tile");
                    writer.WriteAttributeString("X", i.ToString());
                    writer.WriteAttributeString("Y", j.ToString());
                    writer.WriteAttributeString("Terrain", tile[i, j].TileType);
                    writer.WriteEndElement();
                }
            }
        }

        private void CreateButton(int y, Color c, string str)
        {
            Button btn = new Button()
            {
                Location = new Point(800, y),
                BackColor = c,
                Text = str
            };
            btn.Click += Color_Click;
            Controls.Add(btn);
        }

        private void Color_Click(object sender, EventArgs e)
        {
            colorToDraw = (sender as Button).BackColor;
            btnText = (sender as Button).Text;
        }

        private void DrawTileMap()
        {
            Graphics g = CreateGraphics();
            Pen p = Pens.Black;
            tileSize = 750.0 / bigger;
            g.DrawLine(p, 1, 1, (float)(1 + tileSize * tile.GetLength(0)), 1);
            g.DrawLine(p, 1, 1, 1, (float)(1 + tileSize * tile.GetLength(1)));
            for (double i = 1; i < tile.GetLength(0) + 1; i++)
            {
                g.DrawLine(p, (float)(1 + i * tileSize), 1, 
                              (float)(1 + i * tileSize), (float)(1 + tileSize * tile.GetLength(1)));
            }

            for (double j = 1; j < tile.GetLength(1) + 1; j++)
            {
                g.DrawLine(p, 1, (float)(1 + j * tileSize), 
                              (1 + (float)tileSize * tile.GetLength(0)), (float)(1 + j * tileSize));
            }
        }

        private void Editor_MouseDown(object sender, MouseEventArgs e)
        {
            // bs code just checking if there is no grid yet didnt feel like creating a boolean IDK should work
            if (Controls.Count == 5)
            {
                return;
            }

            var x = Math.Floor((e.X - 1) / tileSize);
            var y = Math.Floor((e.Y - 1) / tileSize);
            try
            {
                tile[(int)x, (int)y] = TileGenerator.CreateTile(btnText);
            }
            catch (IndexOutOfRangeException)
            {
                return;
            }

            Graphics g = CreateGraphics();
            g.FillRectangle(ColorToBrush(colorToDraw), (float)(3 + x * tileSize), (float)(3 + y * tileSize), (float)(tileSize - 4), (float)(tileSize - 4));
        }

        private Brush ColorToBrush(Color c)
        {
            if (c == Color.Red)
            {
                return Brushes.Red;
            }
            else if (c == Color.Gray)
            {
                return Brushes.Gray;
            }
            else if (c == Color.Brown)
            {
                return Brushes.Brown;   
            }
            else if (c == Color.Green)
            {
                return Brushes.Green;
            }
            else if (c == Color.Yellow)
            {
                return Brushes.Yellow;
            }
            else if (c == Color.Blue)
            {
                return Brushes.Blue;
            }
            else
            {
                return Brushes.White;
            }
        }
    }
}
