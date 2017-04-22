using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.ApplicationInsights.AspNetCore.Extensions;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using MyCloud.Repositories;
using MyCloud.ViewModel;

namespace MyCloud.Controllers.API
{
    [Route("/api/[controller]/")]
    public class ShareController : Controller
    {
        private ISharingRepository _repository;

        public ShareController(ISharingRepository repository)
        {
            _repository = repository;
        }

        [HttpPost("shareFile")]
        public async Task<IActionResult> ShareFile(FileViewModel vm)
        {
            var strPathAndQuery = HttpContext.Request.GetUri().PathAndQuery;
            var strUrl = HttpContext.Request.GetUri().AbsoluteUri.Replace(strPathAndQuery, "/");
            var accessUrl = strUrl + $"Shared/File//*generated url*/"; // TODO URL generator

            if (!await _repository.AddSharedFile(accessUrl, vm, User.Identity.Name))
            {
                return BadRequest();
            }

            return Ok(strUrl); 
        }

        [HttpPost("shareSingleFile")]
        public async Task<IActionResult> ShareFolder(FolderViewModel vm)
        {
            var strPathAndQuery = HttpContext.Request.GetUri().PathAndQuery;
            var strUrl = HttpContext.Request.GetUri().AbsoluteUri.Replace(strPathAndQuery, "/");
            var accessUrl = strUrl + $"Shared/Folder//*generated url*/"; // TODO URL generator

            if (!await _repository.AddSharedFolder(accessUrl, vm, User.Identity.Name))
            {
                return BadRequest();
            }

            return Ok(strUrl);
        }
    }
}
