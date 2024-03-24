using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Nest;
using PixelPalette.Entities;
using PixelPalette.Models;

namespace PixelPalette.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ElasticSearchController : ControllerBase
    {

        private readonly IElasticClient _elasticClient;

        public ElasticSearchController(IElasticClient elasticClient)
        {
            _elasticClient = elasticClient;
        }
        [HttpGet("Search")]
        public async Task<IActionResult> Search(string keyword)
        {
            if (!String.IsNullOrWhiteSpace(keyword))
            {
                var result = await _elasticClient.SearchAsync<User>(s => s
                    .Query(q => q.QueryString(d => d.Query('*' + keyword + '*')))
                    .Size(5000));

                return Ok(result.Documents.ToList());
            }
            return NotFound();
        }
        [HttpPost("Add")]
        public async Task<IActionResult> Add(User user)
        {
            await _elasticClient.IndexDocumentAsync(user);
            return Ok();
        }
    }
}
