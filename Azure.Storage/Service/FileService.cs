using Microsoft.Azure.Storage;
using Microsoft.Azure.Storage.File;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Azure.Storage.Service
{
    public class FileService : IFileService
    {
        private readonly CloudFileClient _cloudFileClient;
        public FileService(CloudStorageAccount cloudStorageClient)
        {
            this._cloudFileClient = cloudStorageClient.CreateCloudFileClient();
        }

        public async Task<bool> DeleteFileAsync(string filename)
        {
            var fileShare = _cloudFileClient.GetShareReference("bloglogfile");

            await fileShare.CreateIfNotExistsAsync();
            if (fileShare.Exists())
            {
                var rootDir = fileShare.GetRootDirectoryReference();
                var portraitDir = rootDir.GetDirectoryReference("portrait");
                await portraitDir.CreateIfNotExistsAsync();

                if (portraitDir.Exists())
                {
                    var file = portraitDir.GetFileReference(filename);

                    return await file.DeleteIfExistsAsync();
                }
            }
            return false;
        }

        public async Task DownFileAsync(string fileName, string downloadPath)
        {
            
            var fileShare = _cloudFileClient.GetShareReference("bloglogfile");

            await fileShare.CreateIfNotExistsAsync();

            if (fileShare.Exists())
            {
                var rootDir = fileShare.GetRootDirectoryReference();
                var portraitDir = rootDir.GetDirectoryReference("portrait");
                await portraitDir.CreateIfNotExistsAsync();

                if (portraitDir.Exists())
                {
                    var file = portraitDir.GetFileReference(fileName);

                    await file.DownloadToFileAsync(downloadPath, FileMode.Create);
                }
            }
        }

        public async Task<string> GetFileContentAsync(string fileName)
        {
            var fileShare = _cloudFileClient.GetShareReference("bloglogfile");

            await fileShare.CreateIfNotExistsAsync();
            if (fileShare.Exists())
            {
                var rootDir= fileShare.GetRootDirectoryReference();
                var portraitDir= rootDir.GetDirectoryReference("portrait");
                await portraitDir.CreateIfNotExistsAsync();

                if (portraitDir.Exists())
                {
                    var file= portraitDir.GetFileReference(fileName);

                    return file.DownloadTextAsync().Result;
                }
            }
            return string.Empty;
        }

        public async Task UpLoadFileAsync(string filePath, string fileName)
        {
            var fileShare = _cloudFileClient.GetShareReference("bloglogfile");

            await fileShare.CreateIfNotExistsAsync();

            if (fileShare.Exists())
            {
                var rootDir = fileShare.GetRootDirectoryReference();
                var portraitDir = rootDir.GetDirectoryReference("portrait");
                await portraitDir.CreateIfNotExistsAsync();

                if (portraitDir.Exists())
                {
                    var file = portraitDir.GetFileReference(fileName);

                    await file.UploadFromFileAsync(filePath);
                }
            }
        }
    }
}
