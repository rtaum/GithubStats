using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace GithubServices
{
    public class GithubApiClient : IGithubApiClient
    {
        private readonly HttpClient _httpClient;

        public GithubApiClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<Commit>> GetCommitsAsync(string owner, string repository)
        {
            var url = $"https://api.github.com/repos/{owner}/{repository}/commits";

            var request = new HttpRequestMessage(HttpMethod.Get, url);

            var productValue = new ProductInfoHeaderValue("github-app", "1.0");
            request.Headers.UserAgent.Add(productValue);

            var response = await _httpClient.SendAsync(request);
            if (response.StatusCode != HttpStatusCode.OK)
            {
                throw new HttpStatusException(response.StatusCode, "Error on request");
            }

            var body = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<IEnumerable<Commit>>(body);
        }
    }
}
