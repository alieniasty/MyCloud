using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace MyCloud.Models
{
    public class CloudRepository : ICloudRepository
    {
        private CloudContext _context;

        public CloudRepository(CloudContext context)
        {
            _context = context;
        }

        public void AddNewFile(string base64File, string identityName, string fileName, string folder)
        {
            var userWithFiles = _context.CloudUsers
                .Include(n => n.Base64Files)
                .FirstOrDefault(n => n.UserName == identityName);


            userWithFiles.Base64Files.Add(new FileData
            {
                Base64Code = base64File,
                Name = $"{DateTime.Now}_{fileName}",
                Folder = folder
            });

            _context.SaveChangesAsync();
        }
    }
}
