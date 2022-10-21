using System.Threading.Tasks;
using VivesBlog.Model;
using VivesBlog.Sdk;
using VivesBlog.Ui.WebApp.Models;

namespace VivesBlog.Ui.WebApp.Helpers
{
    public class ArticleModelHelper
    {
        private readonly PersonSdk _personSdk;

        public ArticleModelHelper(PersonSdk personSdk)
        {
            _personSdk = personSdk;
        }

        public async Task<ArticleModel> CreateArticleModelAsync(Article article = null)
        {
            article ??= new Article();

            var authors = await _personSdk.FindAsync();

            var articleModel = new ArticleModel
            {
                Article = article,
                Authors = authors
            };

            return articleModel;
        }
    }
}
