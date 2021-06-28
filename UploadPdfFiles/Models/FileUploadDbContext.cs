using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace UploadPdfFiles.Models
{
    public class FileUploadDbContext : DbContext
    {
        public FileUploadDbContext(DbContextOptions<FileUploadDbContext> options)
            :base(options)
        { }
        public DbSet<User> Users { get; set; }
        public DbSet<PdfFile> PdfFiles { get; set; }
   
    }
}
