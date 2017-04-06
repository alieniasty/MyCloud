using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyCloud.Models;
using MyCloud.ViewModel;

namespace MyCloud.Controllers.API
{
    [Route("/api/[controller]/")]
    public class FilesController : Controller
    {
        private ICloudRepository _repository;

        public FilesController(ICloudRepository repository)
        {
            _repository = repository;
        }
        
        [HttpPost("upload")]
        public async Task<IActionResult> Upload(IFormFile file, [FromForm]string folder)
        {
            if (file.Length > 0)
            {
                byte[] buffer;
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
                return Ok();
            }

            return Ok(base64FilesCodes);
        }

        [HttpGet("getUserFolders")]
        public IActionResult GetUserFolders()
        {
            var folders = _repository.GetFoldersByUser(User.Identity.Name);

            return Ok(folders);
        }

        [HttpPost("createNewFolder")]
        public async Task<IActionResult> CreateNewFolder([FromBody]FolderViewModel folder)
        {
            if (folder == null)
            {
                return BadRequest("Folder name was not provided.");
            }

            if (!await _repository.CreateNewFolder(folder.Name, User.Identity.Name))
            {
                return BadRequest();
            }

            return Created("api/[controller]/createNewFolder", true);
        }

        [HttpPost("deleteFile")]
        public async Task<IActionResult> DeleteFile([FromBody]FileViewModel file)
        {
            return Ok();
        }
    }
}
