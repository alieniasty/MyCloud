using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyCloud.ViewModel;

namespace MyCloud.Repositories
{
    public class SharingRepository : ISharingRepository
    {
        public Task<bool> AddSharedFile(string accessUrl, FileViewModel vm, string identityName)
        {
            throw new NotImplementedException();
        }

        public Task<bool> AddSharedFolder(string accessUrl, FolderViewModel vm, string identityName)
        {
            throw new NotImplementedException();
        }

        public string GetSharedFile(string accessUrl)
        {
            throw new NotImplementedException();
        }

        public string GetSharedFolder(string accessUrl)
        {
            throw new NotImplementedException();
        }
    }
}
