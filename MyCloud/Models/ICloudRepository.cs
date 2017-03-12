using System.Collections.Generic;

namespace MyCloud.Models
{
    public interface ICloudRepository
    {
        void AddNewFile(string base64File, string identityName, string fileName, string folder);
    }
}