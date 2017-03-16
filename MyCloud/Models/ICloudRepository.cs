using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyCloud.Models
{
    public interface ICloudRepository
    {
        Task<bool> AddNewFileAsync(string base64File, string identityName, string fileName, string folder);
        IEnumerable<string> GetBase64Files(string folder, string identityName);
    }
}