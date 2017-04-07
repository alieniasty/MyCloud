using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyCloud.Models
{
    public interface ICloudRepository
    {
        IEnumerable<string> GetBase64Files(string folder, string identityName);

        List<string> GetFoldersByUser(string identityName);

        Task<bool> AddNewFileAsync(string base64File, string identityName, string fileName, string folder);

        Task<bool> CreateNewFolder(string folder, string identityName);

        Task<bool> DeleteFileAsync(string base64Code, string identityName);
    }
}