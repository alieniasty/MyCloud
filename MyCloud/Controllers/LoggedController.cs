using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyCloud.Models;

namespace MyCloud.Controllers
{
    public class LoggedController : Controller
    {
        private ICloudRepository _repository;

        public LoggedController(ICloudRepository repository)
        {
            _repository = repository;
        }

        public IActionResult MainPanel()
        {
            return View();
        }
    }
}
