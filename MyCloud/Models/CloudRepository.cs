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
            var user = _context.CloudUsers
                .Include(u => u.Folders)
                .ThenInclude(u => u.FileDatas)
                .SingleOrDefault(u => u.UserName == identityName);

            var userFolder = user.Folders
                .SingleOrDefault(f => f.Name == folder);

            userFolder.FileDatas
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
            var userFiles = _context.CloudUsers
                .Where(u => u.UserName == identityName)
                .Include(t => t.Folders)
                .ThenInclude(f => f.FileDatas)

                .SelectMany(f => f.Folders)
                .Where(f => f.Name == folder)
                .SelectMany(f => f.FileDatas)
                .ToList();


            var codes = userFiles
                .Select(uf => uf.Base64Code)
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

        public async Task<bool> DeleteFileAsync(string base64Code, string folder, string identityName)
        {
            var user = _context.CloudUsers
                .Include(u => u.Folders)
                .ThenInclude(u => u.FileDatas)
                .SingleOrDefault(u => u.UserName == identityName);

            var userFolder = user.Folders
                .SingleOrDefault(f => f.Name == folder);

            var fileToBeRemoved = userFolder.FileDatas.FirstOrDefault(fd => fd.Base64Code == base64Code);


            userFolder.FileDatas.Remove(fileToBeRemoved);

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
