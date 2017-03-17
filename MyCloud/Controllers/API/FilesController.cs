using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyCloud.Models;

namespace MyCloud.Controllers.API
{
    [Route("/api/[controller]/")]
    public class FilesController : Controller
    {
        private ICloudRepository _repository;
        private IHostingEnvironment _environment;

        public FilesController(ICloudRepository repository, IHostingEnvironment environment)
        {
            _repository = repository;
            _environment = environment;
        }
        
        [HttpPost("upload")]
        public async Task<IActionResult> Upload(IFormFile file, [FromForm]string folder)
        {
            if (file.Length > 0)
            {
                byte[] buffer = null;
                using (var fileStream = file.OpenReadStream())
                {
                    buffer = new byte[fileStream.Length];
                    fileStream.Read(buffer, 0, (int)fileStream.Length);
                }

                string base64File = Convert.ToBase64String(buffer);

                if (await _repository.AddNewFileAsync(base64File, User.Identity.Name, file.FileName, folder))
                {
                    return Created("/api/[controller]/upload", file);
                }

                return BadRequest($"Creation failed: {file}");
            }

            return BadRequest("File is null");
        }

        [HttpGet("getJsonFiles")]
        public IActionResult GetJsonFiles(string folder)
        {
            if (folder == null)
            {
                return BadRequest("No folder name was provided");
            }

            var base64FilesCodes = _repository.GetBase64Files(folder, User.Identity.Name);
            
            if (base64FilesCodes == null || !base64FilesCodes.Any())
            {
                return BadRequest("Files codes list is null");
            }

            return Ok(base64FilesCodes);
        }
    }
}
