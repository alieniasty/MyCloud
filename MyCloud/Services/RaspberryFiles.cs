using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.Extensions.Configuration;

namespace MyCloud.Services
{
    public class RaspberryFiles : IRaspberryFiles
    {
        private IConfigurationRoot _config;

        public RaspberryFiles(IConfigurationRoot config)
        {
            _config = config;
        }

        public Task SaveFileToUsb(string newFileBase64Code, string newFileName)
        {
            return Task.Run(() => File.WriteAllBytes($"{_config["Storage:HardDrive"]}\\{newFileName}", Convert.FromBase64String(newFileBase64Code)));
        }

        public Task RemoveFileFromUsb(string name)
        {
            return Task.Run(() => File.Delete($"{_config["Storage:HardDrive"]}\\{name}"));
        }

        public long GetUsedSpace()
        {
            var allDrives = DriveInfo.GetDrives();
            var label = $"{_config["Storage:HardDrive"]}\\".ToUpper();
            var drive = allDrives.SingleOrDefault(d => d.Name == label);

            return drive.TotalSize - drive.TotalFreeSpace;
        }

        public long GetTotalSpace()
        {
            var allDrives = DriveInfo.GetDrives();

            var label = $"{_config["Storage:HardDrive"]}\\".ToUpper();
            var drive = allDrives.SingleOrDefault(d => d.Name == label);

            return drive.TotalSize;
        }
    }
}
