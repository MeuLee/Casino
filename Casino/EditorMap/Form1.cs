using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EditorMap
{
    public partial class Editor : Form
    {
        private Tile[,] tile;
        private double smaller;
        public Editor()
        {
            InitializeComponent();            
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            double width = double.Parse(TxtWidth.Text),
                height = double.Parse(TxtHeight.Text);
            tile = new Tile[Convert.ToInt32(width), Convert.ToInt32(height)];
            smaller = width > height ? height : width;

            Controls.Clear();
            DrawTileMap();
            AddButtonsToggleColor();
        }

        private void AddButtonsToggleColor()
        {
            //TODO add one button per floor type
        }

        private void DrawTileMap()
        {
            Graphics g = CreateGraphics();
            Pen p = Pens.Black;
            double tileSize = 750.0 / smaller;
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
    }
}
