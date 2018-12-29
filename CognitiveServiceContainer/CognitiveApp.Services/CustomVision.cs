using CognitiveApp.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CognitiveApp.Services
{
    public class CustomVision : ICustomVision
    {
        private readonly HttpClient _client;
        private readonly string _projectId;
        public CustomVision(HttpClient client, string projectId)
        {
            _client = client;
            _projectId = projectId;
        }

        public async Task<CustomVisionResponse> FromByteArrayImage(Stream payloadImage)
        {
            StreamContent payloadContent = new StreamContent(payloadImage);
            payloadContent.Headers.Add("Content-Type", "application/octet-stream");
            var response = await _client.PostAsync($"{_projectId}/image", payloadContent);
            response.EnsureSuccessStatusCode();
            return JsonConvert.DeserializeObject<CustomVisionResponse>(await response.Content.ReadAsStringAsync());
        }

        public async Task<CustomVisionResponse> FromUrlImage(Uri urlImage)
        {
            string payloadSerialize = JsonConvert.SerializeObject(new CustomVisionRequest
            {
                Url = urlImage.ToString()
            });
            StringContent payloadContent = new StringContent(payloadSerialize, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync($"{_projectId}/url", payloadContent);
            response.EnsureSuccessStatusCode();
            return JsonConvert.DeserializeObject<CustomVisionResponse>(await response.Content.ReadAsStringAsync());
        }
    }
}
