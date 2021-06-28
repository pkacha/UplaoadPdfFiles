using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UploadPdfFiles.Models;

namespace UploadPdfFiles.Controllers
{
    [Route("api/pdfFiles/{userId}")]
    [EnableCors("AllowOrigin")]
    public class PdfFilesController : ControllerBase
    {
        private readonly int MAX_BYTES = 10 * 1024 * 1024;
        private readonly string[] ACCEPTED_FILE_TYPE = new []{ ".pdf" } ;
        private readonly IWebHostEnvironment host;
        private readonly FileUploadDbContext dbContext;

        public PdfFilesController(IWebHostEnvironment host, FileUploadDbContext dbContext)
        {
            this.host = host;
            this.dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult GetFiles()
        {

            var pdfFiles = dbContext.PdfFiles.ToList();
            return Ok(pdfFiles);
        }

        [HttpPost]
        public async Task<IActionResult> Upload(int userId, IFormFile file)
        {
            var user = dbContext.Users.Select(u => u.Id == userId);
            if (user == null)
                return NotFound();

            if (file == null) return BadRequest("Null File");
            if (file.Length == 0) return BadRequest("Empty File");
            if (file.Length >  MAX_BYTES) return BadRequest("Max file size exceeded");
            if (!ACCEPTED_FILE_TYPE.Any(s => s == Path.GetExtension(file.FileName)))
                return BadRequest("Invalid file type");

            var uploadsFolderPath = Path.Combine(host.ContentRootPath, "uploads");
            if (!Directory.Exists(uploadsFolderPath))
                Directory.CreateDirectory(uploadsFolderPath);

            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            var filePath = Path.Combine(uploadsFolderPath, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            var pdfFile = new PdfFile {FileName = fileName};
            dbContext.PdfFiles.Add(pdfFile);
            dbContext.SaveChanges();

            return Ok(pdfFile);

        }
    }
}
