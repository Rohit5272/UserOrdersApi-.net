using Microsoft.AspNetCore.Mvc;
using UserOrderAPI.Data;
using UserOrderAPI.DTOs;
using UserOrderAPI.Models;

namespace UserOrderAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FileController : ControllerBase
    {
        private readonly AppDbContext _context;

        public FileController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost("upload")]
        public async Task<IActionResult> UploadFile([FromForm] UploadFileDto dto)
        {
            if (dto.File == null || dto.File.Length == 0)
            { 
                return BadRequest("No file uploaded.");
            }

            using var memoryStream = new MemoryStream();

            await dto.File.CopyToAsync(memoryStream);

            var fileDocument = new FileDocument
            {
                FileName = dto.File.FileName,
                ContentType = dto.File.ContentType,
                Data = memoryStream.ToArray()
            };

            _context.FileDocuments.Add(fileDocument);

            await _context.SaveChangesAsync();

            return Ok(new 
            {
                FileId = fileDocument.Id, 
                fileDocument.FileName,
            Message = "File uploaded successfully"});
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetFile(int id)
        {
            var file = await _context.FileDocuments.FindAsync(id);

            if (file == null)
            {
                return NotFound();
            }

            return File(
                file.Data,
                file.ContentType,
                file.FileName);
        }
    }
}
