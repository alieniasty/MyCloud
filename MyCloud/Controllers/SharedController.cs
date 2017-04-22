using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyCloud.Repositories;

namespace MyCloud.Controllers
{
    public class SharedController : Controller
    {
        private ISharingRepository _repository;

        public SharedController(ISharingRepository repository)
        {
            _repository = repository;
        }

        public IActionResult File()
        {
            var accessUrl = RouteData.Values["id"];
            var file = _repository.GetSharedFile(accessUrl as string);

            return View();
        }

        public IActionResult Folder()
        {
            var accessUrl = RouteData.Values["id"];
            var folder = _repository.GetSharedFolder(accessUrl as string);

            return View();
        }
    }
}
