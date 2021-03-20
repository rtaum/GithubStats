using System;
using System.Linq;
using System.Threading.Tasks;

namespace GithubServices
{
    public class GithubService : IGithubService
    {
        private readonly IGithubApiClient _githubApiClient;

        public GithubService(IGithubApiClient githubApiClient)
        {
            _githubApiClient = githubApiClient;
        }

        public async Task<RepositoryStatus> GetRepositoryStatusAsync(string owner, string repository)
        {
            var commits = await _githubApiClient.GetCommitsAsync(owner, repository);

            var commitsCountLastMonth = commits.Count(commit => commit.CommitDetail.Author.CommitDate.AddMonths(1) >= DateTime.UtcNow);
            
            RepositoryStatus status;

            if (commitsCountLastMonth > 50)
            {
                status = RepositoryStatus.Vibrant;
            }
            else if (commitsCountLastMonth > 10)
            {
                status = RepositoryStatus.Alive;
            }
            else if (commitsCountLastMonth > 0)
            {
                status = RepositoryStatus.BarelyAlive;
            }
            else
            {
                status = RepositoryStatus.Dead;
            }

            return status;
        }

        public async Task<Contributor> GetTopExternalControbutorAsync(string owner, string repository)
        {
            var commits = await _githubApiClient.GetCommitsAsync(owner, repository);

            var authorCommits = commits.GroupBy(commit => commit.CommitDetail.Author.Name)
                .Select(group => new Contributor
                {
                    Name = group.Key,
                    CommitsCount = group.Count()
                })
                .OrderByDescending(author => author.CommitsCount);

            return authorCommits.FirstOrDefault();
        }
    }
}
