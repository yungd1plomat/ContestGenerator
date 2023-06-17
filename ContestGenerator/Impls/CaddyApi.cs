﻿using ContestGenerator.Abstractions;
using System.Net.Http;
using System.Text;

namespace ContestGenerator.Impls
{
    public class CaddyApi : ICaddyApi, IDisposable
    {
        const string payload = "{\"listen\":[\":443\"],\"routes\":[{\"handle\":[{\"handler\":\"subroute\",\"routes\":[{\"handle\":[{\"handler\":\"reverse_proxy\",\"upstreams\":[{\"dial\":\"contestgenerator:5000/contest/examplecontest\"}]}]}]}],\"match\":[{\"host\":[\"examplehost\"]}],\"terminal\":true}]}";
        
        private readonly HttpClient _httpClient;

        private readonly ILogger _logger;

        public CaddyApi(ILogger<CaddyApi> logger)
        {
            _logger = logger;
            _httpClient = new HttpClient();
        }

        public async Task AddNewRoute(string domain, string contestName)
        {
            var jsonPayload = payload.Replace("examplehost", domain)
                                     .Replace("examplecontest", contestName);
            using (HttpRequestMessage req = new HttpRequestMessage(HttpMethod.Post, $"http://caddy:2019/config/apps/http/servers/{domain}"))
            {
                req.Content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");
                var resp = await _httpClient.SendAsync(req);
                var content = await resp.Content.ReadAsStringAsync();
                if (!resp.IsSuccessStatusCode)
                    throw new HttpRequestException(content);
                _logger.LogInformation($"Added new route for {domain} to {contestName}");
            }
        }

        public void Dispose()
        {
            _httpClient.Dispose();
        }
    }
}