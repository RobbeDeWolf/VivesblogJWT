using System.Collections.Generic;
using VivesBlog.Model;

namespace VivesBlog.Ui.WebApp.Models
{
	public class ArticleModel
	{
		public Article Article { get; set; }
		public IList<Person> Authors { get; set; }
	}
}
