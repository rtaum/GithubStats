using System.Threading.Tasks;

namespace GithubServices
{
    public interface IGithubService
    {
        Task<RepositoryStatus> GetRepositoryStatusAsync(string owner, string repository);

        Task<Contributor> GetTopExternalControbutorAsync(string owner, string repository);
    }
}
