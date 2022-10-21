using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using VivesBlog.Model;
using VivesBlog.Sdk;
using VivesBlog.Ui.WebApp.Helpers;

namespace VivesBlog.Ui.WebApp.Controllers
{
	public class BlogController : Controller
	{
		private readonly ArticleSdk _articleSdk;
        private readonly ArticleModelHelper _articleModelHelper;

		public BlogController(
            ArticleSdk articleSdk, 
            ArticleModelHelper articleModelHelper)
        {
            _articleSdk = articleSdk;
            _articleModelHelper = articleModelHelper;
        }
		public async Task<IActionResult> Index()
        {
            var articles = await _articleSdk.FindAsync();
			return View(articles);
		}

		[HttpGet]
		public async Task<IActionResult> Create()
		{
			var articleModel = await _articleModelHelper.CreateArticleModelAsync();

			return View(articleModel);
		}

		[HttpPost]
		public async Task<IActionResult> Create(Article article)
		{
			if (!ModelState.IsValid)
			{
				var articleModel = await _articleModelHelper.CreateArticleModelAsync(article);
				return View(articleModel);
			}

            await _articleSdk.CreateAsync(article);
			
			return RedirectToAction("Index");
		}

		[HttpGet]
		public async Task<IActionResult> Edit(int id)
        {
            var article = await _articleSdk.GetAsync(id);

			if (article is null)
			{
				return RedirectToAction("Index");
			}

			var articleModel = await _articleModelHelper.CreateArticleModelAsync(article);

			return View(articleModel);
		}

		[HttpPost]
		public async Task<IActionResult> Edit(int id, Article article)
		{
			if (!ModelState.IsValid)
			{
				var articleModel = await _articleModelHelper.CreateArticleModelAsync(article);
				return View(articleModel);
			}

            await _articleSdk.UpdateAsync(id, article);

			return RedirectToAction("Index");
		}

		[HttpGet]
		public async Task<IActionResult> Delete(int id)
        {
            var article = await _articleSdk.GetAsync(id);

            if (article is null)
			{
				return RedirectToAction("Index");
			}

            return View(article);
		}

		[HttpPost]
		[Route("Blog/Delete/{id}")]
		public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _articleSdk.DeleteAsync(id);

			return RedirectToAction("Index");
		}
    }
}
