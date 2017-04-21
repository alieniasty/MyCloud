using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MyCloud.Models;

namespace MyCloud.Repositories
{
    public class FoldersRepository : IFoldersRepository
    {
        private CloudContext _context;

        public FoldersRepository(CloudContext context)
        {
            _context = context;
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
