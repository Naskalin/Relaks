using App.DbConfigurations;
using App.Models;
using App.Utils;
using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace App.Endpoints;

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

//         var results = _db.Database.ExecuteSqlRaw(@"
// SELECT *, highlight(FtsPost, 2, '<b>', '</b>') as Highlight
// FROM FtsPost
// WHERE FtsPost MATCH 'кошк*'
// ");
//         var results = _db.Set<FtsPost>().FromSqlRaw(@"
// SELECT *, highlight(FtsPost, 2, '<b>', '</b>') as Highlight
// FROM FtsPost
// WHERE FtsPost MATCH 'кошк*'
// ");
            
        // TODO: work variant
        var results = from fts in _db.Set<FtsPost>()
            where fts.Match == request.Search + "*"
            orderby fts.Rank
            select new
            {
                PostId = fts.PostId,
                Snippet = _db.Snippet(fts.Match, "-1", "<b>", "</b>", "...", 10),
            };

        return Ok(results);
        // return Ok(new
        // {
        //     DateTime.UtcNow,
        //     results,
        // });
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
                AnyField = "default val"
            };

            var ftsPost = new FtsPost()
            {
                PostId = postId,
                Title = item.Key,
                Description = item.Value,
            };

//             _db.Set<FtsPost>().FromSqlRaw(@"
// INSERT INTO FtsPost(ROWID, Title, Description) VALUES(2, 'Cat', 'myau and murk');
// ");
            _db.Posts.Add(post);
            _db.Set<FtsPost>().Add(ftsPost);
            // _db.Add(ftsPost);
            // _db.Add(post);
        }

        _db.SaveChanges();
    }
}