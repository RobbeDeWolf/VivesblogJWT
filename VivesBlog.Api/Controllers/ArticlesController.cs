using Microsoft.AspNetCore.Mvc;
using VivesBlog.Model;
using VivesBlog.Services.Abstractions;

namespace VivesBlog.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticlesController : ControllerBase
    {
        private readonly IArticleService _articleService;

        public ArticlesController(IArticleService articleService)
        {
            _articleService = articleService;
        }

        [HttpGet]
        public async Task<IActionResult> Find()
        {
            var people = await _articleService.FindAsync();
            return Ok(people);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute]int id)
        {
            var article = await _articleService.GetAsync(id);
            return Ok(article);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody]Article article)
        {
            var createdArticle = await _articleService.CreateAsync(article);
            return Ok(createdArticle);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute]int id, [FromBody]Article article)
        {
            var updatedArticle = await _articleService.UpdateAsync(id, article);
            return Ok(updatedArticle);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute]int id)
        {
            var isDeleted = await _articleService.DeleteAsync(id);
            return Ok(isDeleted);
        }
    }
}
