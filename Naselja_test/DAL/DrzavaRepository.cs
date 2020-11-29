using Naselja_test.Models;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RestSharp;
using System.Collections.Generic;

namespace Naselja_test.DAL
{
    public class DrzavaRepository : IDrzavaRepository
    {
        private readonly IOptions<AppSettings> config;
        private readonly RestClient client;
        public DrzavaRepository(IOptions<AppSettings> _config)
        {
            config = _config;
            client = new RestClient(config.Value.ApiEndpoint);
        }
        public List<Drzava> GetDrzave()
        {
            var request = new RestRequest("Drzave/GetDrzava");
            var response = client.Get(request);
            var result = JsonConvert.DeserializeObject<List<Drzava>>(response.Content);
            return result;

        }
    }
}
