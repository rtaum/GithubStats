using NSubstitute;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace GithubServices.Test
{
	public class GithubServiceTests
	{
		[Fact]
		public async Task For_CommitsCountInLastMonth_GreatherThan50_RepositoryStatus_MustBeVibrant()
		{
			// Arrange
			var _githubApiClient = Substitute.For<IGithubApiClient>();
			_githubApiClient.GetCommitsAsync(Arg.Any<string>(), Arg.Any<string>()).ReturnsForAnyArgs(new List<Commit>()
			{
				new Commit
				{
					CommitDetail = new CommitDetails
                    {
						Author = new Author
                        {
							CommitDate = DateTime.UtcNow.AddDays(-1)
                        }
                    }
				}
			});
			var _githubService = new GithubService(_githubApiClient);

			// Act
			var repositoryStatus = await _githubService.GetRepositoryStatusAsync("owner", "repository");
			
			// Assert
			Assert.Equal(RepositoryStatus.BarelyAlive, repositoryStatus);			
		}
	}
}
