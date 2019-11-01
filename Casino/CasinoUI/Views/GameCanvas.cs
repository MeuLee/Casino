using CasinoUI.Model.Cards;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace CasinoUI.Views
{
    public class GameCanvas : Canvas
    {
        public List<Card> CardsToDraw { get; set; } = new List<Card>();
        protected override void OnRender(DrawingContext dc)
        {
            foreach (Card card in CardsToDraw)
            {
                dc.DrawImage(ToBitmapImage(card.Image), 
                             new Rect(((int)card.Value - 1) * 80, (int)card.Suit * 135, 80, 135));
            }
        }

        public static BitmapImage ToBitmapImage(Bitmap bitmap)
        {
            using (var memory = new MemoryStream())
            {
                bitmap.Save(memory, ImageFormat.Png);
                memory.Position = 0;

                var bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.StreamSource = memory;
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.EndInit();
                bitmapImage.Freeze();
                return bitmapImage;
            }
        }
    }
}
