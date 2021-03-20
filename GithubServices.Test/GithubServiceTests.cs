using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace GithubServices.Test
{
    public class GithubServiceTests
	{
		[Theory]
		[InlineData(0, RepositoryStatus.Dead)]
		[InlineData(1, RepositoryStatus.BarelyAlive)]
		[InlineData(10, RepositoryStatus.BarelyAlive)]
		[InlineData(11, RepositoryStatus.Alive)]
		[InlineData(50, RepositoryStatus.Alive)]
		[InlineData(51, RepositoryStatus.Vibrant)]
		public async Task For_CommitsCountInLastMonth_RepositoryStatus_ShouldReturnCorrectValue(int count, RepositoryStatus status)
		{
			// Arrange			
			var _githubApiClient = Substitute.For<IGithubApiClient>();
			var commitsList = Enumerable.Repeat(
					new Commit()
					{
						CommitDetail = new CommitDetails
						{
							Author = new Author
							{
								CommitDate = DateTime.UtcNow.AddDays(-1)
							}
						}
					},
					count
				).ToList();
			_githubApiClient.GetCommitsAsync(Arg.Any<string>(), Arg.Any<string>()).ReturnsForAnyArgs(commitsList);
			var _githubService = new GithubService(_githubApiClient);

			// Act
			var repositoryStatus = await _githubService.GetRepositoryStatusAsync("owner", "repository");
			
			// Assert
			Assert.Equal(status, repositoryStatus);
		}

		[Fact]
		public async Task Should_Return_Top_Contributor()
        {
			// Arrange			
			var _githubApiClient = Substitute.For<IGithubApiClient>();
			var commitsList = new List<Commit>
			{
					new Commit()
					{
						CommitDetail = new CommitDetails
						{
							Author = new Author
							{
								Name = "Top Contributor"
							}
						}
					},
					new Commit()
					{
						CommitDetail = new CommitDetails
						{
							Author = new Author
							{
								Name = "Average Contributor"
							}
						}
					},
					new Commit()
					{
						CommitDetail = new CommitDetails
						{
							Author = new Author
							{
								Name = "Top Contributor"
							}
						}
					}
			};
			_githubApiClient.GetCommitsAsync(Arg.Any<string>(), Arg.Any<string>()).ReturnsForAnyArgs(commitsList);
			var _githubService = new GithubService(_githubApiClient);

			// Act
			var topContributor = await _githubService.GetTopExternalControbutorAsync("owner", "repository");

			// Assert
			Assert.Equal("Top Contributor", topContributor.Name);
		}
	}
}