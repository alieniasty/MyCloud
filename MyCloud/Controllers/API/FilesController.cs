using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

                _repository.AddNewFile(base64File, User.Identity.Name, file.FileName, folder);
            }

            return Created("/api/[controller]/upload", file);
        }
    }
}
