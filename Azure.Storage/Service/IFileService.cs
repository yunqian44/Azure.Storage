using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Azure.Storage.Service
{
    public interface IFileService
    {
        Task UpLoadFileAsync(string filePath, string fileName);

        Task DownFileAsync(string fileName, string downloadPath);

        Task<MemoryStream> GetFileAsync(string fileName, string downloadPath);

        Task DeleteFileAsync(string name);

    }
}
