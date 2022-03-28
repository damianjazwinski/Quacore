using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Quacore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StorageController : Controller
    {
        public IConfiguration Configuration { get; }

        public StorageController(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        [HttpPost("upload")]
        public async Task<IActionResult> Upload([FromForm(Name ="file")]IFormFile file)
        {
            string newFileName = "storage_" + file.FileName;

            Directory.CreateDirectory(Configuration["AppSettings:Storage"]);
            var filePath = Path.Combine(Configuration["AppSettings:Storage"], newFileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
            return Ok(new { newFileName });
        }

        [HttpGet("download/{id}")]
        public async Task<IActionResult> Download(string id)
        {
            string path = Path.Combine(Configuration["AppSettings:Storage"], id);

            try
            {
                return File(await System.IO.File.ReadAllBytesAsync(path), "application/octet-stream");
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
