using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayTracer
{
    public class Canvas
    {
        public int Width { get; private set; }
        public int Height { get; private set; }

        public Color[,] Pixels { get; private set; }

        public Canvas(int width, int height) { 
            Width = width; 
            Height = height;
            Pixels = new Color[Width, Height];
            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    Pixels[x, y] = new Color(0, 0, 0);
                }
            }
        }

        public void Write(int x, int y, Color color)
        {
            Pixels[x, y] = color;
        }

        public Color At(int x, int y)
        {
            return Pixels[x, y];
        }

        public string ToPPM()
        {
            const int scale = 255;
            string header = $"P3\n{Width} {Height}\n{scale}\n";

            string data = "";
            for (int y = 0; y < Height; y++)
            {
                string line = "";
                for (int x = 0; x < Width; x++)
                {
                    Color pixel = Pixels[x, y];

                    int r = (int)MathF.Round(pixel.R * scale);
                    r = Math.Clamp(r, 0, 255);
                    int g = (int)MathF.Round(pixel.G * scale);
                    g = Math.Clamp(g, 0, 255);
                    int b = (int)MathF.Round(pixel.B * scale);
                    b = Math.Clamp(b, 0, 255);

                    foreach (int v in new int[] { r, g, b })
                    {
                        string s = v.ToString();
                        if (line.Length + s.Length > 70)
                        {
                            data += line.Trim() + "\n";
                            line = "";
                        }
                        line += s + " ";
                    }
                }
                data += line.Trim() + "\n";
            }
            
            return header + data;
        }
    }
}
