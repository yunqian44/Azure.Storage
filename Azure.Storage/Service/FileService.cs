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
        public FileService(CloudFileClient cloudFileClient)
        {
            this._cloudFileClient = cloudFileClient;
        }

        public Task DeleteFileAsync(string name)
        {
            throw new NotImplementedException();
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

        public async Task<MemoryStream> GetFileAsync(string fileName,string downloadPath)
        {
            var fileShare = _cloudFileClient.GetShareReference("bloglogfile");

            await fileShare.CreateIfNotExistsAsync();
            await using var memoryStream = new MemoryStream();
            if (fileShare.Exists())
            {
                var rootDir= fileShare.GetRootDirectoryReference();
                var portraitDir= rootDir.GetDirectoryReference("portrait");
                await portraitDir.CreateIfNotExistsAsync();

                if (portraitDir.Exists())
                {
                    var file= portraitDir.GetFileReference(fileName);
                    
                    await file.DownloadToStreamAsync(memoryStream);
                    
                }
            }
            return memoryStream;
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
                    var file = portraitDir.GetFileReference("fileName");

                    await file.UploadFromFileAsync(filePath);
                }
            }
        }
    }
}
