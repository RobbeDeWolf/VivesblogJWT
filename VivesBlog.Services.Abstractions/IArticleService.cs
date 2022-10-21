using VivesBlog.Model;

namespace VivesBlog.Services.Abstractions;

public interface IArticleService
{
    Task<IList<Article>> FindAsync();
    Task<Article?> GetAsync(int id);
    Task<Article?> CreateAsync(Article article);
    Task<Article?> UpdateAsync(int id, Article article);
    Task<bool> DeleteAsync(int id);
}