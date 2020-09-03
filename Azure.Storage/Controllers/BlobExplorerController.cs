using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
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

        // GET /api/Files
        // Called by the page when it's first loaded, whenever new files are uploaded, and every
        // five seconds on a timer.
        [HttpGet("{BlobName}")]
        public async Task<IActionResult> GetBlob(string blobName)
        {
            var data = await _blobService.GetBlobAsync(blobName);
            return File(data.Content, data.ContentType);
        }

        // POST /api/Files
        // Called once for each file uploaded.
        [HttpGet("list")]
        public async Task<IActionResult> ListBlobs(IFormFile file)
        {
            return Ok(await _blobService.ListBlobsAsync());
        }

        //// GET /api/Files/{filename}
        //[HttpPost("UploadFile")]
        //public async Task<IActionResult> UpLoadFile([FromBody] UpLoadFileRequest request)
        //{
        //    await _blobService.UploadFileBlobAsync(request.FilePath, request.FileName);
        //    return Ok();
        //}

        //[HttpPost("UpLoadContent")]
        //public async Task<IActionResult> UploadContent([FromBody] UpLoadContentRequest request)
        //{
        //    await _blobService.UploadContentBlobAsync(request.Content, request.FileName);
        //    return Ok();
        //}

        [HttpDelete("{BlobName}")]
        public async Task<IActionResult> DaleteFile(string blobName)
        {
            await _blobService.DeleteBlobAsync(blobName);
            return Ok();
        }
    }
}
