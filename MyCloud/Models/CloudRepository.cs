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

        public List<string> GetFoldersByUser(string identityName)
        {
            var folders = _context.CloudUsers
                .Where(u => u.UserName == identityName)
                .SelectMany(u => u.Folders)
                .Select(f => f.Name)
                .ToList();

            return folders;
        }

        public async Task<bool> CreateNewFolder(string folder, string identityName)
        {
            var userWithFolders = _context.CloudUsers
                .Include(u => u.Folders)
                .FirstOrDefault(u => u.UserName == identityName);

            userWithFolders.Folders.Add(new Folder
            {
                Name = folder
            });

            return await _context.SaveChangesAsync() > 0;

        }

        public async Task<bool> DeleteFileAsync(string base64Code, string identityName)
        {
            var userWithFiles = _context.CloudUsers
                .Include(n => n.Base64Files)
                .FirstOrDefault(n => n.UserName == identityName);

            var file = userWithFiles
                .Base64Files
                .FirstOrDefault(f => f.Base64Code == base64Code);


            userWithFiles.Base64Files.Remove(file);

            return await _context.SaveChangesAsync() > 0;
        }
    }
}
