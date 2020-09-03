using Azure.Storage.Blobs;
using Azure.Storage.Models;
using Microsoft.Azure.Storage;
using Microsoft.Azure.Storage.Blob;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Azure.Storage.Service
{
    public class BlobService : IBlobSergvice
    {
        private readonly BlobServiceClient _blobServiceClient;

        public BlobService(BlobServiceClient blobServiceClient)
        {
            this._blobServiceClient = blobServiceClient;
        }

        public async Task<BlobInfo> GetBlobAsync(string name)
        {
            var containerClient = _blobServiceClient.GetBlobContainerClient("youtube");

            var blobClient = containerClient.GetBlobClient(name);
            var blobDownLoadInfo= await blobClient.DownloadAsync();
            return new BlobInfo(blobDownLoadInfo.Value.Content, blobDownLoadInfo.Value.ContentType);
        }

        public Task<IEnumerable<string>> ListBlobsAsync()
        {
            throw new NotImplementedException();
        }

        public Task UploadFileBlobAsync(string filePath, string filename)
        {
            throw new NotImplementedException();
        }

        public Task UploadContentBlobAsync(string content, string filename)
        {
            throw new NotImplementedException();
        }

        public Task DeleteBlobAsync(string blobName)
        {
            throw new NotImplementedException();
        }
    }
}
