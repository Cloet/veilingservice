using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace veilingservice.Model
{
    public abstract class ImageBase
    {

        public int ID { get; set; }

        public string ImageLocation { get; set; }

        public bool CheckIfImageExists()
        {
            return File.Exists(Path.GetFullPath(ImageLocation));
        }

    }
}
