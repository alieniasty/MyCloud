using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyCloud.Models;
using MyCloud.Repositories;
using MyCloud.Services;
using MyCloud.ViewModel;

namespace MyCloud.Controllers.API
{
    [Authorize]
    [Route("/api/[controller]/")]
    public class FilesController : Controller
    {
        private IFilesRepository _repository;
        private IRaspberryFiles _raspberry;

        public FilesController(IFilesRepository repository, IRaspberryFiles raspberry)
        {
            _repository = repository;
            _raspberry = raspberry;
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

                return BadRequest("Creation failed");
            }

            return BadRequest("File is null");
        }

        [HttpGet("getJsonFiles")]
        public async Task<IActionResult> GetJsonFiles(string folder)
        {
            if (folder == null)
            {
                return BadRequest("No folder name was provided");
            }

            var base64FilesCodes = await _repository.GetBase64Files(folder, User.Identity.Name);
            
            if (base64FilesCodes == null || !base64FilesCodes.Any())
            {
                return Ok();
            }

            return Ok(base64FilesCodes);
        }

        [HttpPost("deleteFile")]
        public async Task<IActionResult> DeleteFile([FromBody]FileViewModel filevm)
        {
            if (filevm.Base64Code == null)
            {
                return BadRequest("File code was not provided");
            }

            if (!await _repository.DeleteFileAsync(filevm.Base64Code, filevm.Folder , User.Identity.Name))
            {
                return BadRequest();
            }

            return Ok();
        }

        [HttpPost("moveSelectedFiles")]
        public async Task<IActionResult> MoveSelectedFiles([FromBody]MovedFilesViewModel vm) 
        {
            if (ModelState.IsValid)
            {
                if (await _repository.MoveFilesAsync(vm.Codes, vm.CurrentFolder, vm.NewFolder, User.Identity.Name))
                {
                    return Ok();
                }

                return BadRequest();
            }

            return BadRequest();
        }
    }
}
