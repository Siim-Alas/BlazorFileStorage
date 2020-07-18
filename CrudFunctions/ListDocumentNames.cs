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
using System.Linq;
using System.Collections;

namespace CrudFunctions
{
    public static class ListDocumentNames
    {
        [FunctionName("ListDocumentNames")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)] HttpRequest req,
            [Blob("documents", FileAccess.Read)] CloudBlobContainer container,
            ILogger log)
        {
            IEnumerable blobNames = container.ListBlobs().OfType<CloudBlockBlob>().Select(b => b.Name);
            return new OkObjectResult(blobNames);
        }
    }
}
