using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace CrudFunctions
{
    public static class InsertDocument
    {
        [FunctionName("InsertDocument")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "InsertDocument/{name}")] HttpRequest req,
            [Blob("documents/{name}", FileAccess.Write)] Stream document,
            ILogger log)
        {
            await req.Body.CopyToAsync(document);

            return new OkResult();
        }
    }
}
