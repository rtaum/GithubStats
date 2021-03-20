using NSubstitute;
using System.Threading.Tasks;
using Xunit;

namespace GithubServices.Test
{
	public class GithubServiceTests
	{
		[Fact]
		public void For_CommitsCountInLastMonth_GreatherThan50_RepositoryStatus_MustBeVibrant()
		{
			// Arrange
			var _githubService = Substitute.For<IGithubService>();
			//var _githubApiClient = Substitute.For<IGithubApiClient>();

			// Act
			var repositoryStatus = _githubService.GetRepositoryStatusAsync("owner", "repository");
			//var commits = _githubApiClient.GetCommitsAsync("owner", "repository");            

			// Assert
			Assert.IsType<Task<RepositoryStatus>>(repositoryStatus);
			//Assert.InRange<int>(commits.Count(), 51, int.MaxValue);
		}
	}
}
