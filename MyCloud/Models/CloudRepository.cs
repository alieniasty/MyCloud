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

        public async Task<bool> AddNewFileAsync(string base64File, string identityName, string fileName, string folder)
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

            return await _context.SaveChangesAsync() > 0;
        }

        public IEnumerable<string> GetBase64Files(string folder, string identityName)
        {
            var userFiles = _context.CloudUsers
                .Where(user => user.UserName == identityName)
                .SelectMany(user => user.Base64Files)
                .ToList();

            var codes = userFiles
                .Where(files => files.Folder == folder)
                .Select(files => files.Base64Code)
                .ToList();

            return codes;
        }
    }
}
