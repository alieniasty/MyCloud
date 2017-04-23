using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MyCloud.ViewModel;
using MyCloud.Models;

namespace MyCloud.Repositories
{
    public class SharingRepository : ISharingRepository
    {
        private CloudContext _context;

        public SharingRepository(CloudContext context)
        {
            _context = context;
        }

        public async Task<bool> AddSharedFile(string sharingString, FileViewModel vm, string identityName)
        {
            var user = _context.CloudUsers
                .Include(u => u.Folders)
                .ThenInclude(u => u.FileDatas)
                .SingleOrDefault(u => u.UserName == identityName);

            var userFolder = user.Folders
                .SingleOrDefault(f => f.Name == vm.Folder);

            var fileToBeShared = userFolder.FileDatas.FirstOrDefault(fd => fd.Base64Code == vm.Base64Code);

            fileToBeShared.SharingUrl = sharingString;

            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> AddSharedFolder(string sharingString, FolderViewModel vm, string identityName)
        {
            var userWithFolders = _context.CloudUsers
                .Where(u => u.UserName == identityName)
                .Include(u => u.Folders)
                .FirstOrDefault();

            var folderToBeShared = userWithFolders.Folders.FirstOrDefault(f => f.Name == vm.Name);

            folderToBeShared.SharingUrl = sharingString;

            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<string> GetSharedFile(string accessUrl)
        {
            var sharedFile = await _context.FileDatas
                .SingleOrDefaultAsync(f => f.SharingUrl == accessUrl);

            return sharedFile.Base64Code;
        }

        public string GetSharedFolder(string accessUrl)
        {
            throw new NotImplementedException();
        }
    }
}
