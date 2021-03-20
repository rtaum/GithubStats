using GithubServices;
using GithubStats.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace GithubStats.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RepositoryController : ControllerBase
    {
        private readonly IGithubService _githubService;

        public RepositoryController(IGithubService githubService)
        {
            _githubService = githubService;
        }

        [HttpGet("status/{owner}/{repository}")]
        public async Task<ActionResult> GetStatus(string owner, string repository)
        {
            var status = await _githubService.GetRepositoryStatusAsync(owner, repository);
            var response = new RepositoryStatusModel
            {
                Status = status
            };

            return Ok(response);
        }

        [HttpGet("top/{owner}/{repository}")]
        public async Task<ActionResult> GetTopContributer(string owner, string repository)
        {
            var topgun = await _githubService.GetTopExternalControbutorAsync(owner, repository);
            var response = new ContributorModel
            {
                Contributor = topgun
            };


            return Ok(response);
        }
    }
}
