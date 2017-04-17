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
                .Include(u => u.Folders)
                    .ThenInclude(f => f.Base64Files)
                .FirstOrDefault(u => u.UserName == identityName);


            userWithFiles.Folders
                .Select(f => f.Base64Files)
                .FirstOrDefault()
                .Add(new FileData
            {
                Base64Code = base64File,
                Name = $"{DateTime.Now}_{fileName}",
                Folder = folder
            });

            return await _context.SaveChangesAsync() > 0;
        }

        public IEnumerable<string> GetBase64Files(string folder, string identityName)
        {
            var userWithFiles = _context.CloudUsers
                .Where(u => u.UserName == identityName)
                .Include(t => t.Folders)
                    .ThenInclude(f => f.Base64Files)
                .FirstOrDefault();

            var codes = userWithFiles
                .Folders
                .Where(f => f.Name == folder)
                .SelectMany(f => f.Base64Files)
                .Select(b => b.Base64Code);

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
                .Where(u => u.UserName == identityName)
                .Include(t => t.Folders)
                    .ThenInclude(f => f.Base64Files)
                .FirstOrDefault();
        
            var file = userWithFiles
                .Folders
                .SelectMany(f => f.Base64Files)
                .FirstOrDefault(f => f.Base64Code == base64Code);


            var files = userWithFiles.Folders.Select(f => f.Base64Files).FirstOrDefault();
            files.Remove(file);

            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteFolderAsync(string folderName, string identityName)
        {
            var userWithFolders = _context.CloudUsers
                .Where(u => u.UserName == identityName)
                .Include(u => u.Folders)
                .FirstOrDefault();

            var folder = userWithFolders.Folders.FirstOrDefault(f => f.Name == folderName);

            userWithFolders.Folders.Remove(folder);

            return await _context.SaveChangesAsync() > 0;

        }
    }
}
