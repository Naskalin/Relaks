using App.DbConfigurations;
using App.Models;
using App.Utils;
using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace App.Endpoints;

public class FtsResult
{
    public Guid Id { get; set; }
    public string Snippet { get; set; } = null!;
}

public class SearchResult<T>
{
    
}

public class PostList : EndpointBaseAsync
    .WithRequest<BaseListRequest>
    .WithActionResult
{
    private readonly AppDbContext _db;

    public PostList(AppDbContext db)
    {
        _db = db;
    }

    [HttpGet("/api/posts")]
    public override async Task<ActionResult> HandleAsync(
        [FromMultiSource] BaseListRequest request,
        CancellationToken cancellationToken = new())
    {
        if (!_db.Posts.Any())
        {
            Seed();
        }

        // TODO: work variant
        // var results = from postFts in _db.Set<PostFts>()
        //     join post in _db.Posts on postFts.Id equals post.Id 
        //     where postFts.Match == request.Search + "*"
        //     orderby postFts.Rank
        //     select new
        //     {
        //         Id = postFts.Id,
        //         Snippet = _db.Snippet(postFts.Match, "-1", "<b>", "</b>", "...", 10),
        //     };

        var results = FindFtsResults(request.Search);
        
        var data = results.Join(_db.Posts,
            x => x.Id,
            p => p.Id,
            (fts, post) => new
            {
                Snippet = fts.Snippet,
                Post = post
            }
            
            ).ToList();

        return Ok(data);
    }

    private IQueryable<FtsResult> FindFtsResults(string search)
    {
        return from postFts in _db.Set<PostFts>()
            where postFts.Match == search + "*"
            orderby postFts.Rank
            select new FtsResult()
            {
                Id = postFts.Id,
                Snippet = _db.Snippet(postFts.Match, "-1", "<b>", "</b>", "...", 10),
            };
    }

    private void Seed()
    {
        var data = new Dictionary<string, string>()
        {
            {"Кошка", "мяукает и мурлыкает"},
            {"Собака", "бывает так, что очень громко лает"},
            {"Рыба", "плавает в воде и молчит"},
            {"Бычара", "быки могут больно забодатать"},
            {"Волчара", "волки живут в стаях"},
            {"Cat", "murk and myau"},
            {"batman", "my dog say gav-gav"},
        };

        foreach (var item in data)
        {
            var postId = Guid.NewGuid();

            var post = new Post()
            {
                Id = postId,
                Title = item.Key,
                Description = item.Value,
                AnyField = "default val"
            };

            _db.Posts.Add(post);
        }

        _db.SaveChanges();
    }
}