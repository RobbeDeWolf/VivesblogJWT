using System.ComponentModel.DataAnnotations;

namespace VivesBlog.Model
{
	public class Article
	{
		public int Id{ get; set; }

        [Required] public string Title { get; set; } = null!;
        [Required] public string Description { get; set; } = null!;
        [Required] public string Content { get; set; } = null!;
        
        [Required]
		public int AuthorId { get; set; }

        public Person Author { get; set; } = default!;
		public DateTime CreatedDate{ get; set; }
	}
}
