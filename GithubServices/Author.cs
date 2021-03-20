using Newtonsoft.Json;
using System;

namespace GithubServices
{
    public class Author
    {
        public string Name { get; set; }

        public string Email { get; set; }

        [JsonProperty("date")]
        public DateTime CommitDate { get; set; }
    }
}
