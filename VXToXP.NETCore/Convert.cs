using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace VXToXP.NETCore
{
    public static class Convert
    {
        public static void Do(string inpath, string outpath)
        {
            // Make sure everything exists.
            if (!Directory.Exists(inpath)) Directory.CreateDirectory(inpath);
            if (!Directory.Exists(outpath)) Directory.CreateDirectory(outpath);

            // Process our images.
            foreach (var file in new DirectoryInfo(inpath).EnumerateFiles("*.png"))
            {
                using (var tmp = Image.FromFile(file.FullName))
                {
                    var width = tmp.Width / 3;
                    var height = tmp.Height / 4;

                    using (var newimage = new Bitmap(width * 4, height * 4, PixelFormat.Format32bppArgb))
                    {
                        using (var g = Graphics.FromImage(newimage))
                        {
                            g.Clear(Color.Transparent);
                            g.DrawImage(tmp, new Rectangle(0, 0, width, height * 4), new Rectangle(width, 0, width, height * 4), GraphicsUnit.Pixel);
                            g.DrawImage(tmp, new Rectangle(width, 0, width * 3, height * 4), new Rectangle(0, 0, width * 3, height * 4), GraphicsUnit.Pixel);
                            g.Flush();
                        }
                        newimage.Save(Path.Combine(outpath, file.Name), ImageFormat.Png);
                    }
                }
            }
        }
    }
}
