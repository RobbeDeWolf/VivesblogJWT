using Microsoft.EntityFrameworkCore;
using VivesBlog.Data;
using VivesBlog.Services;
using VivesBlog.Services.Abstractions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<VivesBlogDbContext>(options =>
{
    options.UseInMemoryDatabase(nameof(VivesBlogDbContext));
});
builder.Services.AddScoped<IArticleService, ArticleService>();
builder.Services.AddScoped<IPersonService, PersonService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    var scope = app.Services.CreateScope();
    var dbContext = scope.ServiceProvider.GetRequiredService<VivesBlogDbContext>();
    if (dbContext.Database.IsInMemory())
    {
        dbContext.Seed();
    }
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
