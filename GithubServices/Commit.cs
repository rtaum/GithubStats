using Newtonsoft.Json;

namespace GithubServices
{
    public class Commit
    {
        public string Sha { get; set; }

        [JsonProperty("commit")]
        public CommitDetails CommitDetail { get; set; }
    }
}
