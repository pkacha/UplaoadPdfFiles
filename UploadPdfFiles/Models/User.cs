using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace UploadPdfFiles.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Firstname { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }

        public ICollection<PdfFile> PdfFiles { get; set; }

        public User()
        {
            PdfFiles = new Collection<PdfFile>();
        }

    }
}
