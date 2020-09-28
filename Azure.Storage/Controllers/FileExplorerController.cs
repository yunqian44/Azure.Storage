using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azure.Storage.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;

namespace Azure.Storage.Controllers
{
    [Route("File")]
    public class FileExplorerController : Controller
    {

        private readonly IFileService _fileService;

        public FileExplorerController(IFileService fileService)
        {
            this._fileService = fileService;
        }


        /// <summary>
        /// 上传文件
        /// </summary>
        /// <returns></returns>
        [HttpPost("UploadFile")]
        public async Task UploadFile()
        {
            string filePath = "D:\\Azure_File_UpLoad\\100.jpg";
            string fileName = "100.jpg";
            await _fileService.UpLoadFileAsync(filePath, fileName);
        }

        [HttpGet("DownloadFile")]
        public async Task DownloadFile()
        {
            string filePath = "D:\\Azure_File_DownLoad\\100.jpg";
            string fileName = "100.jpg";
            await _fileService.DownFileAsync(fileName, filePath);
        }

        [HttpGet("GetFileContent")]
        public async Task<IActionResult> GetFileContentAsync()
        {
            string fileName = "AZ-300考试说明.txt";
            var data= await _fileService.GetFileContentAsync(fileName);
            return Ok(data);
        }


        [HttpDelete("DeleteFile")]
        public async Task<IActionResult> DeleteFileAsync()
        {
            string fileName = "AZ-300考试说明.txt";
            await _fileService.DeleteFileAsync(fileName);
            return Ok();
        }
    }
}
