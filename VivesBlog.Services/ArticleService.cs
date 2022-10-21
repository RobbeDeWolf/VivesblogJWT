using Microsoft.EntityFrameworkCore;
using VivesBlog.Data;
using VivesBlog.Model;
using VivesBlog.Services.Abstractions;

namespace VivesBlog.Services;

public class ArticleService : IArticleService
{
    private readonly VivesBlogDbContext _dbContext;

    public ArticleService(VivesBlogDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IList<Article>> FindAsync()
    {
        return await _dbContext.Articles
            .Select(a => new Article
            {
                Title = a.Title,
                Content = a.Content,
                Description = a.Description,
                AuthorId = a.AuthorId,
                Author = new Person
                {
                    Id = a.Author.Id,
                    FirstName = a.Author.FirstName,
                    LastName = a.Author.LastName
                }
            })
            .ToListAsync();
    }

    public async Task<Article?> GetAsync(int id)
    {
        return await _dbContext.Articles
            .Select(a => new Article
            {
                Title = a.Title,
                Content = a.Content,
                Description = a.Description,
                AuthorId = a.AuthorId,
                Author = new Person
                {
                    Id = a.Author.Id,
                    FirstName = a.Author.FirstName,
                    LastName = a.Author.LastName
                }
            })
            .SingleOrDefaultAsync(a => a.Id == id);
    }

    public async Task<Article?> CreateAsync(Article article)
    {
        article.CreatedDate = DateTime.UtcNow;

        _dbContext.Articles.Add(article);

        await _dbContext.SaveChangesAsync();

        return article;
    }

    public async Task<Article?> UpdateAsync(int id, Article article)
    {
        var dbArticle = await GetAsync(id);

        if (dbArticle is null)
        {
            return null;
        }

        dbArticle.Title = article.Title;
        dbArticle.Description = article.Description;
        dbArticle.Content = article.Content;
        dbArticle.AuthorId = article.AuthorId;

        await _dbContext.SaveChangesAsync();

        return dbArticle;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var article = new Article { Id = id };
        _dbContext.Articles.Attach(article);
        _dbContext.Articles.Remove(article);
        await _dbContext.SaveChangesAsync();
        return true;
    }
}