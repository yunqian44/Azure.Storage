using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Azure.Storage.Models;
using Azure.Storage.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Azure.Storage.Controllers
{
    [Route("Blobs")]
    public class BlobExplorerController : Controller
    {
        private readonly IBlobSergvice _blobService;

        public BlobExplorerController(IBlobSergvice blobService)
        {
            this._blobService = blobService;
        }

        [HttpGet("{BlobName}")]
        public async Task<IActionResult> GetBlob(string blobName)
        {
            var data = await _blobService.GetBlobAsync(blobName);
            return File(data.Content, data.ContentType);
        }

        [HttpGet("BlobsName")]
        public async Task<IActionResult> ListBlobsName()
        {
            return Ok(await _blobService.ListBlobsNameAsync());
        }

        [HttpPost("UploadFile")]
        public async Task<IActionResult> UpLoadFile([FromBody] UploadFileRequest request)
        {
            await _blobService.UploadFileBlobAsync(request.FilePath, request.FileName);
            return Ok();
        }

        [HttpPost("UploadContent")]
        public async Task<IActionResult> UploadContent([FromBody] UpLoadContentRequest request)
        {
            await _blobService.UploadContentBlobAsync(request.Content, request.FileName);
            return Ok();
        }

        [HttpDelete("{BlobName}")]
        public async Task<IActionResult> DaleteFile(string blobName)
        {
            await _blobService.DeleteBlobAsync(blobName);
            return Ok();
        }
    }
}
