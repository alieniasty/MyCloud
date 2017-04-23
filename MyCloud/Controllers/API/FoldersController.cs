using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyCloud.Models;
using MyCloud.Repositories;
using MyCloud.ViewModel;

namespace MyCloud.Controllers.API
{
    [Authorize]
    [Route("/api/[controller]/")]
    public class FoldersController : Controller
    {
        private IFoldersRepository _repository;

        public FoldersController(IFoldersRepository repository)
        {
            _repository = repository;
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

        [HttpPost("deleteFolder")]
        public async Task<IActionResult> DeleteFolder([FromBody] FolderViewModel folder)
        {
            if (folder.Name == null)
            {
                return BadRequest("Folder name was not provided");
            }

            if (!await _repository.DeleteFolderAsync(folder.Name, User.Identity.Name))
            {
                return BadRequest();
            }

            return Ok();
        }
    }
}
