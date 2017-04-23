using System.Threading.Tasks;
using MyCloud.ViewModel;

namespace MyCloud.Repositories
{
    public interface ISharingRepository
    {
        Task<bool> AddSharedFile(string accessUrl, FileViewModel vm, string identityName);

        Task<bool> AddSharedFolder(string accessUrl, FolderViewModel vm, string identityName);

        Task<string> GetSharedFile(string accessUrl);

        string GetSharedFolder(string accessUrl);
    }
}