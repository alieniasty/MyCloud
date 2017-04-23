using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.ApplicationInsights.AspNetCore.Extensions;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using MyCloud.Repositories;
using MyCloud.Services;
using MyCloud.ViewModel;

namespace MyCloud.Controllers.API
{
    [Route("/api/[controller]/")]
    public class ShareController : Controller
    {
        private ISharingRepository _repository;
        private IRandomUrl _randomService;

        public ShareController(ISharingRepository repository, IRandomUrl randomService)
        {
            _randomService = randomService;
            _repository = repository;
        }

        [HttpPost("shareFile")]
        public async Task<IActionResult> ShareFile([FromBody]FileViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var strPathAndQuery = HttpContext.Request.GetUri().PathAndQuery;
                var strUrl = HttpContext.Request.GetUri().AbsoluteUri.Replace(strPathAndQuery, "/");

                var sharingString = _randomService.Generate();

                var accessUrl = strUrl + $"Disclose/File/{sharingString}"; 

                if (!await _repository.AddSharedFile(sharingString, vm, User.Identity.Name))
                {
                    return BadRequest();
                }

                return Ok(accessUrl); 
            }

            return BadRequest("Model state is not valid");
        }

        [HttpPost("shareSingleFile")]
        public async Task<IActionResult> ShareFolder([FromBody]FolderViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var strPathAndQuery = HttpContext.Request.GetUri().PathAndQuery;
                var strUrl = HttpContext.Request.GetUri().AbsoluteUri.Replace(strPathAndQuery, "/");

                var sharingString = _randomService.Generate();

                var accessUrl = strUrl + $"Disclose/Folder/{sharingString}"; 

                if (!await _repository.AddSharedFolder(sharingString, vm, User.Identity.Name))
                {
                    return BadRequest();
                }

                return Ok(accessUrl);
            }

            return BadRequest("Model state is not valid");
        }
    }
}
