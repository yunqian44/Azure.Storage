using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Azure.Storage.Extension;
using Azure.Storage.Models;
using Microsoft.Extensions.Options;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
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

        #region 01，获取Blob,根据blob名称+async Task<BlobInfo> GetBlobAsync(string name)
        /// <summary>
        /// 获取Blob,根据blob名称
        /// </summary>
        /// <param name="name">blob名称</param>
        /// <returns></returns>
        public async Task<Azure.Storage.Models.BlobInfo> GetBlobAsync(string name)
        {
            var containerClient = _blobServiceClient.GetBlobContainerClient("picturecontainer");

            var blobClient = containerClient.GetBlobClient(name);
            var blobDownLoadInfo = await blobClient.DownloadAsync();
            return new Azure.Storage.Models.BlobInfo(blobDownLoadInfo.Value.Content, blobDownLoadInfo.Value.ContentType);
        }
        #endregion

        #region 02，获取所有Blob名称+async Task<IEnumerable<string>> ListBlobsNameAsync()
        /// <summary>
        /// 获取所有Blob名称
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<string>> ListBlobsNameAsync()
        {
            var containerClient = _blobServiceClient.GetBlobContainerClient("picturecontainer");
            var items = new List<string>();

            await foreach (var blobItem in containerClient.GetBlobsAsync())
            {
                items.Add(blobItem.Name);
            }
            return items;
        }
        #endregion

        #region 03，上传图片流，根据文件路径和文件名称+async Task UploadFileBlobAsync(string filePath, string filename)
        /// <summary>
        /// 上传图片流，根据文件路径和文件名称
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <param name="filename">文件名称</param>
        /// <returns></returns>
        public async Task UploadFileBlobAsync(string filePath, string filename)
        {
            var containerClient = _blobServiceClient.GetBlobContainerClient("picturecontainer");
            var blobClient = containerClient.GetBlobClient(filename);
            await blobClient.UploadAsync(filePath, new BlobHttpHeaders { ContentType = filePath.GetContentType() });
        }
        #endregion

        #region 04，上传文件流，根据文件内容和文件名称+async Task UploadContentBlobAsync(string content, string filename)
        /// <summary>
        /// 上传文件流，根据文件内容和文件名称
        /// </summary>
        /// <param name="content">文件内容</param>
        /// <param name="filename">文件名称</param>
        /// <returns></returns>
        public async Task UploadContentBlobAsync(string content, string filename)
        {
            var containerClient = _blobServiceClient.GetBlobContainerClient("picturecontainer");
            var blobClient = containerClient.GetBlobClient(filename);
            var bytes = Encoding.UTF8.GetBytes(content);
            await using var memoryStream = new MemoryStream(bytes);
            await blobClient.UploadAsync(memoryStream, new BlobHttpHeaders() { ContentType = filename.GetContentType() });
        }
        #endregion

        #region 05，删除Blob+async Task DeleteBlobAsync(string blobName)
        /// <summary>
        /// 删除Blob
        /// </summary>
        /// <param name="blobName">blob名称</param>
        /// <returns></returns>
        public async Task DeleteBlobAsync(string blobName)
        {
            var containerClient = _blobServiceClient.GetBlobContainerClient("picturecontainer");
            var blobClient = containerClient.GetBlobClient(blobName);
            await blobClient.DeleteIfExistsAsync();
        } 
        #endregion
    }
}
