using Azure.Storage.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Azure.Storage.Service
{
    public interface IBlobSergvice
    {
        Task<BlobInfo> GetBlobAsync(string name);

        Task<IEnumerable<string>> ListBlobsAsync();

        Task UploadFileBlobAsync(string filePath, string filename);

        Task UploadContentBlobAsync(string content, string filename);

        Task DeleteBlobAsync(string blobName);

    }
}
