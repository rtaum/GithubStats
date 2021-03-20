using GithubStats.Controllers;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using System.Threading.Tasks;
using Xunit;

namespace GithubServices.Test
{
	public class RepositoryControllerTests
	{
		[Fact]
		public async Task GetStatus_Returns_A_RepositoryStatusModel()
		{
			// Arrange
			var _githubService = Substitute.For<IGithubService>();
			var controller = new RepositoryController(_githubService);

			// Act
			var result = await controller.GetStatus("owner", "repository");

			// Assert
			Assert.IsType<OkObjectResult>(result);
		}
	}
}
