using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Microsoft.Azure.Storage.Blob;

namespace CrudFunctions
{
    public static class DeleteDocument
    {
        [FunctionName("DeleteDocument")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "delete", Route = "DeleteDocument/{name}")] HttpRequest req,
            [Blob("documents/{name}", FileAccess.Read)] CloudBlockBlob document,
            ILogger log)
        {
            await document.DeleteIfExistsAsync();
            return new OkResult();
        }
    }
}
