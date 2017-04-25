using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyCloud.Repositories;

namespace MyCloud.Controllers
{
    public class DiscloseController : Controller
    {
        private ISharingRepository _repository;

        public DiscloseController(ISharingRepository repository)
        {
            _repository = repository;
        }

        public async Task<IActionResult> File()
        {
            var accessUrl = RouteData.Values["id"];
            var fileCode = await _repository.GetSharedFile(accessUrl as string);

            return View((object)fileCode);
        }

        public IActionResult Folder()
        {
            var accessUrl = RouteData.Values["id"];
            var filesCodes = _repository.GetSharedFolder(accessUrl as string);

            return View(filesCodes);
        }
    }
}
