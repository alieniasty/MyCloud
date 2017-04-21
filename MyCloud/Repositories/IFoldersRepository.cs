using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyCloud.Repositories
{
    public interface IFoldersRepository
    {
        List<string> GetFoldersByUser(string identityName);

        Task<bool> CreateNewFolder(string folder, string identityName);

        Task<bool> DeleteFolderAsync(string folderName, string identityName);
    }
}
