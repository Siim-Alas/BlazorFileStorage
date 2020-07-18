using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Microsoft.AspNetCore.StaticFiles;

namespace CrudFunctions
{
    public static class GetDocument
    {
        [FunctionName("GetDocument")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "GetDocument/{name}")] HttpRequest req,
            [Blob("documents/{name}", FileAccess.Read)] Stream document,
            string name,
            ILogger log)
        {
            FileExtensionContentTypeProvider provider = new FileExtensionContentTypeProvider();
            if (!provider.TryGetContentType(name, out string contentType))
            {
                contentType = "application/octet-stream";
            }

            byte[] fileContents = new byte[document.Length];
            await document.ReadAsync(fileContents);

            return new FileContentResult(fileContents, contentType);
        }
    }
}
