using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace veilingservice.Helpers
{
    public static class FileContentType
    {
        public static string GetContentType(string path)
        {
            var ext = Path.GetExtension(path).ToLowerInvariant();
            return MimeTypes[ext];
        }

        public static string GetExtension(string contentType)
        {
            var ext = MimeTypes
                .Where(x => x.Value == contentType);

            if (ext == null)
                return "";

            return ext.FirstOrDefault().Key;
        }

        private static Dictionary<string, string> MimeTypes
            => new Dictionary<string, string>
            {
                {".txt", "text/plain"},
                {".pdf", "application/pdf"},
                {".doc", "application/vnd.ms-word"},
                {".docx", "application/vnd.ms-word"},
                {".xls", "application/vnd.ms-excel"},
                {".xlsx", "application/vnd.openxmlformats officedocument.spreadsheetml.sheet"},  
                {".png", "image/png"},
                {".jpg", "image/jpeg"},
                {".jpeg", "image/jpeg"},
                {".gif", "image/gif"},
                {".csv", "text/csv"}
            };
        


    }
}
