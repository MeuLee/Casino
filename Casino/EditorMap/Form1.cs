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
        private float bigger, tileSize;
        private Color colorToDraw;
        private string btnText;
        private bool _mouseIsDown = false;
        private int brushSize = 1;
        private bool _drawVerticalTable = false;
        private bool _drawHorizontalTable = false;
        
        public Editor()
        {
            InitializeComponent();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            float width = float.Parse(TxtWidth.Text),
                height = float.Parse(TxtHeight.Text);
            tile = new Tile[Convert.ToInt32(width), Convert.ToInt32(height)];
            bigger = width > height ? width : height;

            Controls.Clear();
            DrawTileMap();
            AddButtonsToggleColor();
            AddPaintBrushSize();
        }

        #region programmatically add controls
        private void AddButtonsToggleColor()
        {
            CreateButton(15, Color.Red, "Floor1");
            CreateButton(45, Color.Gray, "Floor2");
            CreateButton(75, Color.Yellow, "SlotMachine");
            CreateButton(105, Color.Blue, "Bar");
            CreateButton(135, SystemColors.Control, "Undo");
            CreateVerticalTableButton();
            CreateHorizontalTableButton();
            CreateXmlButton();
        }

        private void CreateHorizontalTableButton()
        {
            Button hTable = new Button()
            {
                Text = "Horizontal table",
                Location = new Point(850, 225)
            };
            hTable.Click += HTable_Click;
            Controls.Add(hTable);
        }

        private void HTable_Click(object sender, EventArgs e)
        {
            _drawVerticalTable = false;
            _drawHorizontalTable = true;
        }

        private void CreateVerticalTableButton()
        {
            Button vTable = new Button()
            {
                Text = "Vertical table",
                Location = new Point(850, 195)
            };
            vTable.Click += VTable_Click;
            Controls.Add(vTable);
        }

        private void VTable_Click(object sender, EventArgs e)
        {
            _drawVerticalTable = true;
            _drawHorizontalTable = false;
        }

        private void CreateXmlButton()
        {
            Button xml = new Button()
            {
                Text = "Generate xml",
                Location = new Point(850, 255)
            };
            xml.Click += Xml_Click;
            Controls.Add(xml);
        }

        private void AddPaintBrushSize()
        {
            ComboBox box = new ComboBox()
            {
                DropDownStyle = ComboBoxStyle.DropDownList,
                Location = new Point(850, 165),
                Name = "CboBrushSize",
                Width = 75
            };
            box.Items.Add(1);
            box.Items.Add(3);
            box.Items.Add(5);
            box.SelectedIndex = 1;
            Controls.Add(box);
        }

        private void CreateButton(int y, Color c, string str)
        {
            Button btn = new Button()
            {
                Location = new Point(850, y),
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
            _drawHorizontalTable = false;
            _drawVerticalTable = false;
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
        #endregion

        private void WriteTilesToXml(XmlWriter writer)
        {
            for (int i = 0; i < tile.GetLength(0); i++)
            {
                for (int j = 0; j < tile.GetLength(1); j++)
                {
                    writer.WriteStartElement("Tile");
                    writer.WriteAttributeString("X", i.ToString());
                    writer.WriteAttributeString("Y", j.ToString());
                    writer.WriteAttributeString("Terrain", tile[i, j].TileType); // don't forget to fill all tiles if you get nullpointer here!
                    writer.WriteEndElement();
                }
            }
        }

        private void DrawTileMap()
        {
            Graphics g = CreateGraphics();
            Pen p = Pens.Black;
            tileSize = 600.0f / bigger;
            g.DrawLine(p, 1.0f, 1.0f, 1.0f + tileSize * tile.GetLength(0), 1.0f);
            g.DrawLine(p, 1.0f, 1.0f, 1.0f, 1.0f + tileSize * tile.GetLength(1));
            for (float i = 1; i < tile.GetLength(0) + 1; i++)
            {
                g.DrawLine(p, 1.0f + i * tileSize, 1.0f,
                              1.0f + i * tileSize, 1.0f + tileSize * tile.GetLength(1));
            }

            for (float j = 1; j < tile.GetLength(1) + 1; j++)
            {
                g.DrawLine(p, 1.0f, 1.0f + j * tileSize, 
                              (1.0f + tileSize * tile.GetLength(0)), 1.0f + j * tileSize);
            }
        }

        private void Editor_MouseDown(object sender, MouseEventArgs e)
        {
            if (_drawHorizontalTable || _drawVerticalTable)
            {
                DrawTable(e);
                return;
            }

            _mouseIsDown = true;
            brushSize = int.Parse(Controls.OfType<ComboBox>().First().Text);
        }

        private void DrawTable(MouseEventArgs e)
        {
            var x = Math.Floor((e.X - 1) / tileSize);
            var y = Math.Floor((e.Y - 1) / tileSize);

            if (_drawHorizontalTable)
            {
                colorToDraw = Color.Brown;
                for (int i = (int)x, index = 1; i < x + 4; i++, index++)
                {
                    DrawOneTile(i, y, $"Table{index}");
                    DrawOneTile(i, y + 2, $"Table{index + 8}");
                }
                DrawOneTile(x, y + 1, "Table5");
                DrawOneTile(x + 3, y + 1, "Table8");
                colorToDraw = Color.Green;
                DrawOneTile(x + 1, y + 1, "Table6");
                DrawOneTile(x + 2, y + 1, "Table7");
            }
            else if (_drawVerticalTable)
            {
                colorToDraw = Color.Brown;
                DrawOneTile(x, y, "Table9");
                DrawOneTile(x + 1, y, "Table5");
                DrawOneTile(x + 2, y, "Table1");
                DrawOneTile(x, y + 1, "Table10");
                DrawOneTile(x + 2, y + 1, "Table2");
                DrawOneTile(x, y + 2, "Table11");
                DrawOneTile(x + 2, y + 2, "Table3");
                DrawOneTile(x, y + 3, "Table12");
                DrawOneTile(x + 1, y + 3, "Table8");
                DrawOneTile(x + 2, y + 3, "Table4");
                colorToDraw = Color.Green;
                DrawOneTile(x + 1, y + 1, "Table6");
                DrawOneTile(x + 1, y + 2, "Table7");
            }
        }

        private void Editor_MouseUp(object sender, MouseEventArgs e)
        {
            _mouseIsDown = false;
        }

        private void Editor_MouseMove(object sender, MouseEventArgs e)
        {
            if (!_mouseIsDown) return;
            // bs code just checking if there is no grid yet didnt feel like creating a boolean IDK should work
            if (Controls.Count == 5)
            {
                return;
            }

            var x = Math.Floor((e.X - 1) / tileSize);
            var y = Math.Floor((e.Y - 1) / tileSize);

            switch (brushSize)
            {
                case 1:
                    DrawOneTile(x, y, btnText);
                    break;
                case 3:
                    DrawThreeTiles(x, y);
                    break;
                case 5:
                    DrawFiveTiles(x, y);
                    break;
            }
        }

        private void DrawFiveTiles(double x, double y)
        {
            for (int i = (int)x - 2; i <= (int)x + 2; i++)
            {
                for (int j = (int)y - 2; j <= y + 2; j++)
                {
                    DrawOneTile(i, j, btnText);
                }
            }
        }

        private void DrawThreeTiles(double x, double y)
        {
            for (int i = (int)x - 1; i <= (int)x + 1; i++)
            {
                for (int j = (int)y - 1; j <= y + 1; j++)
                {
                    DrawOneTile(i, j, btnText);
                }
            }
        }

        private void DrawOneTile(double x, double y, string xmlText)
        {
            try
            {
                tile[(int)x, (int)y] = TileGenerator.CreateTile(xmlText);
            }
            catch (IndexOutOfRangeException)
            {
                return;
            }
            Graphics g = CreateGraphics();
            g.FillRectangle(ColorToBrush(colorToDraw), (float)Math.Ceiling((float)x * tileSize + 3.0f), (float)Math.Ceiling((float)y * tileSize + 3.0f), (float)Math.Ceiling(tileSize - 4.0f), (float)Math.Ceiling(tileSize - 4.0f));
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
