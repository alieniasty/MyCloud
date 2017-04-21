using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyCloud.Repositories
{
    public interface IFilesRepository
    {
        IEnumerable<string> GetBase64Files(string folder, string identityName);

        Task<bool> AddNewFileAsync(string base64File, string identityName, string fileName, string folder);

        Task<bool> DeleteFileAsync(string base64Code, string folder, string identityName);
    }
}