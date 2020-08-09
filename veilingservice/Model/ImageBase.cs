using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace veilingservice.Model
{
    public abstract class ImageBase
    {

        public int ID { get; set; }

        public string ImageLocation { get; set; }

        public double AspectRatio { get; set; }

        public static double CalculateAspectRatio(Stream fileStream) {

            var ratio = 1.0;

            using (var imag = Image.FromStream(fileStream)) {
                double width = imag.Width;
                double height = imag.Height;
                ratio = width / height;
            }

            return ratio;
        }

        public bool CheckIfImageExists()
        {
            return File.Exists(Path.GetFullPath(ImageLocation));
        }

    }
}
