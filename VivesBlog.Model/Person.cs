using System.ComponentModel.DataAnnotations;

namespace VivesBlog.Model
{
	public class Person
	{
		public int Id { get; set; }

        [Required] public string FirstName { get; set; } = null!;

        [Required] public string LastName { get; set; } = null!;

		public IList<Article> Articles { get; set; } = new List<Article>();
	}
}
