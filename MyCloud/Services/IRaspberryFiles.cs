using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace MyCloud.Services
{
    public interface IRaspberryFiles
    {
        Task SaveFileToUsb(string newFileBase64Code, string newFileName);
        Task RemoveFileFromUsb(string name);
        long GetUsedSpace();
        long GetTotalSpace();
    }
}
