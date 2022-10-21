using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using VivesBlog.Sdk;
using VivesBlog.Ui.WebApp.Models;

namespace VivesBlog.Ui.WebApp.Controllers
{
	public class HomeController : Controller
	{
		private readonly ArticleSdk _articleSdk;

		public HomeController(ArticleSdk articleSdk)
		{
            _articleSdk = articleSdk;
		}

		public async Task<IActionResult> Index()
        {
            var articles = await _articleSdk.FindAsync();

            return View(articles);
		}

		public async Task<IActionResult> Details(int id)
        {
            var article = await _articleSdk.GetAsync(id);

			return View(article);
		}

		public IActionResult Privacy()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
