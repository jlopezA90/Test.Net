using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RestSharp;
using System.Configuration;
using client.test.Models;

namespace client.test.Services
{
    public class ApiRequest
    {
        private readonly ApiUrl _appSettings = new();
        public async Task<T> GetTAsync<T>(string resource)
        {
            RestClient client = new(Startup.MainApi);
            var request = new RestRequest(resource, Method.Get);
            var response = await client.GetAsync<T>(request);
            return response;
        }

        public async Task<T> PostAsync<T>(string resource, object value)
        {
            RestClient client = new(Startup.MainApi);
            var request = new RestRequest(resource).AddJsonBody(value);
            var response = await client.PostAsync<T>(request);
            return response;
        }

        public async Task<T> PutAsync<T>(string resource, object value)
        {
            RestClient client = new(Startup.MainApi);
            var request = new RestRequest(resource).AddJsonBody(value);
            var response = await client.PutAsync<T>(request);
            return response;
        }
    }
}
