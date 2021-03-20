using System.Collections.Generic;
using System.Threading.Tasks;

namespace GithubServices
{
    public interface IGithubApiClient
    {
        Task<IEnumerable<Commit>> GetCommitsAsync(string owner, string repository);
    }
}
