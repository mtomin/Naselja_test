using Naselja_test.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Net;

namespace Naselja_test.DAL
{
    public class NaseljeRepository : INaseljeRepository
    {
        private readonly IOptions<AppSettings> config;
        private readonly RestClient client;
        public NaseljeRepository(IOptions<AppSettings> _config)
        {
            config = _config;
            client = new RestClient(config.Value.ApiEndpoint);
        }

        public void AddNaselje(Naselje naselje)
        {
            var request = new RestRequest("Naselja/PostNaselje");
            request.AddJsonBody(naselje, "application/json");
            var response = client.Post(request);
            if (response.StatusCode == HttpStatusCode.Created)
            {
                var kreiranoNaselje = JsonConvert.DeserializeObject<Naselje>(response.Content);
                if (kreiranoNaselje?.ID == 0 || kreiranoNaselje?.ID == null)
                {
                    throw new Exception("Pogreška prilikom kreiranja naselja.");
                }
            }
            else
            {
                throw new Exception("Pogreška prilikom obrađivanja zahtjeva.");
            }
        }

        public int BrojNaselja()
        {
            var result = 0;
            var request = new RestRequest("Naselja/GetBrojNaselja");
            var response = client.Get(request);
            if (response.StatusCode == HttpStatusCode.OK)
                result = JsonConvert.DeserializeObject<int>(response.Content);
            return result;
        }

        public void DeleteNaselje(int naseljeID)
        {
            var request = new RestRequest("Naselja/DeleteNaselje/{id}").AddUrlSegment("id", naseljeID);
            var response = client.Delete(request);
        }

        public List<Naselje> GetNaselja()
        {
            throw new NotImplementedException();
        }

        public List<Naselje> GetNaseljaPaged(int page, int pageSize = 20)
        {
            var result = new List<Naselje>() { new Naselje { ID = 0, Naziv = "Trenutno nije moguće dohvatiti popis naselja iz baze", Drzava = new Drzava() } };
            var request = new RestRequest("Naselja/GetNaseljePage");
            request.AddParameter("page", page);
            request.AddParameter("pageSize", pageSize);
            var response = client.Get(request);
            if (response.StatusCode == HttpStatusCode.OK)
                result = JsonConvert.DeserializeObject<List<Naselje>>(response.Content);
            return result;
        }

        public void UpdateNaselje(Naselje naselje)
        {
            var request = new RestRequest("Naselja/PutNaselje/{id}").AddUrlSegment("id", naselje.ID);
            request.AddJsonBody(naselje, "application/json");
            var response = client.Put(request);
            if (response.StatusCode != HttpStatusCode.NoContent)
                throw new Exception("Pogreška prilikom obrade zahtjeva");

        }
    }
}
