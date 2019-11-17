using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Windows.Media.Imaging;

namespace CasinoUI.Utils
{
    public static class Extensions
    {
        public static List<T> GetValues<T>() where T : Enum
        {
            return Enum.GetValues(typeof(T)).Cast<T>().ToList();
        }

        public static void Swap<T>(this List<T> list, int index1, int index2)
        {
            T temp = list[index1];
            list[index1] = list[index2];
            list[index2] = temp;
        }

        public static BitmapImage ToBitmapImage(this Bitmap bitmap)
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

        public static int ToInteger(this string str)
        {
            return int.Parse(str);
        }

        public static bool ToBoolean(this string str)
        {
            return bool.Parse(str);
        }

        public static Bitmap Rotate90(this Bitmap bmp)
        {
            bmp.RotateFlip(RotateFlipType.Rotate90FlipNone);
            return bmp;
        }

        public static T Pick<T>(this Random random, Dictionary<T, double> elementToProbability)
        {
            var totalProbability = elementToProbability.Values.Sum();
            var randomValue = random.NextDouble() * totalProbability;

            foreach (var keyValuePair in elementToProbability)
            {
                if (randomValue < keyValuePair.Value)
                    return keyValuePair.Key;

                randomValue -= keyValuePair.Value;
            }
            return default;
        }
    }
}
