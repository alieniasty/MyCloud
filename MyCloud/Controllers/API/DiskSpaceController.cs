using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyCloud.Services;

namespace MyCloud.Controllers.API
{
    [Authorize]
    [Route("/api/[controller]/")]
    public class DiskSpaceController : Controller
    {
        private IRaspberryFiles _raspberry; 

        public DiskSpaceController(IRaspberryFiles raspberry)
        {
            _raspberry = raspberry;
        }

        [HttpGet("UsedSpace")]
        public IActionResult UsedSpace()
        {
            var result = _raspberry.GetUsedSpace() / 1024f / 1024f / 1024f;
            result = (float) Math.Round(result, 2);
            return Ok(result);
        }

        [HttpGet("TotalSpace")]
        public IActionResult TotalSpace()
        {
            var result = _raspberry.GetTotalSpace() / 1024f / 1024f / 1024f;
            result = (float) Math.Round(result, 2);
            return Ok(result);
        }
    }
}
  