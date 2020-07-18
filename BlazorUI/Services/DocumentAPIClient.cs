using BlazorInputFile;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace BlazorUI.Services
{
    public class DocumentAPIClient
    {
        private readonly HttpClient _client;
        public DocumentAPIClient(HttpClient client)
        {
            _client = client;
            _client.BaseAddress = new Uri("https://blazorfilestorage.azurewebsites.net/api/");
        }

        public async Task<HttpResponseMessage> PostDocumentAsync(IFileListEntry file)
        {
            StreamContent content = new StreamContent(file.Data);
            content.Headers.ContentType = new MediaTypeHeaderValue(file.Type);
            return await _client.PostAsync($"InsertDocument/{file.Name}", content);
        }
        public async Task<string[]> GetListOfDocumentNames()
        {
            return await _client.GetFromJsonAsync<string[]>("ListDocumentNames");
        }
        public async Task DeleteFile(string fileName)
        {
            await _client.DeleteAsync($"DeleteDocument/{fileName}");
        }
    }
}
