using TechnicalRadiation.Repositories.Interfaces;
using System.Collections.Generic;
using TechnicalRadiation.Models.Dtos;
using TechnicalRadiation.Models.Entities;
using TechnicalRadiation.Repositories.Contexts;
using TechnicalRadiation.Models.InputModels;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using TechnicalRadiation.Models.Extensions;

namespace TechnicalRadiation.Repositories.Implementations
{
    public class AuthorRepository : IAuthorRepository
    {
        private readonly NewsDbContext _dbContext;
        public AuthorRepository(NewsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<AuthorDto> GetAllAuthors()
        {
            var authors = _dbContext.Authors.Include(a => a.NewsItems);
            List<AuthorDto> outputList = new List<AuthorDto>();
            foreach (var x in authors)
            {
                outputList.Add(AddHyperMediaToModel(x));
            }
            return outputList;
        }

        public int CreateAuthor(AuthorInputModel newAuthor)
        {
            var nextId = _dbContext.Authors.Max(n => n.Id) + 1;
            var slug = newAuthor.Name.ToLower().Replace(' ', '-');
            var x = new Author
            {
                Id = nextId,
                Name = newAuthor.Name,
                ProfileImgSource = newAuthor.ProfileImgSource,
                ModifiedBy = "TechnicalRadiationAdmin",
                CreatedDate = System.DateTime.Now,
                ModifiedDate = System.DateTime.Now
            };
            _dbContext.Authors.Add(x);
            _dbContext.SaveChanges();
            return x.Id;
        }

        public AuthorDetailDto GetAuthorById(int id)
        {
            Author author = _dbContext.Authors.Include(c => c.NewsItems).FirstOrDefault<Author>(c => c.Id == id);
            var Author = AddHyperMediaToModelDetail(author);
            return new AuthorDetailDto
            {
                Id = Author.Id,
                Name = Author.Name,
                ProfileImgSource = Author.ProfileImgSource,
                Links = Author.Links,
                Bio = Author.Bio
            };
        }

        public bool DoesExist(int id) => _dbContext.Authors.Any(g => g.Id == id);

        public void UpdateAuthorById(int id, AuthorInputModel uItem)
        {
            var nItem = _dbContext.Authors.FirstOrDefault(n => n.Id == id);
            nItem.Name = uItem.Name;
            nItem.ModifiedDate = System.DateTime.Now;

            _dbContext.SaveChanges();
        }

        public void DeleteAuthorById(int id)
        {
            var x = _dbContext.Authors.First(a => a.Id == id);
            _dbContext.Remove(x);
            _dbContext.SaveChanges();
        }
        public static NewsItemDetailDto AddHyperMediaToModel(NewsItem newsItem)
        {
            var temp = new NewsItemDetailDto
            {
                Id = newsItem.Id,
                Title = newsItem.Title,
                ImgSource = newsItem.ImgSource,
                ShortDescription = newsItem.ShortDescription,
                LongDescription = newsItem.LongDescription,
                PublishDate = newsItem.PublishDate
            };

            temp.Links.AddReference("self", new { href = $"/api/{newsItem.Id}" });
            temp.Links.AddReference("edit", new { href = $"/api/{newsItem.Id}" });
            temp.Links.AddReference("delete", new { href = $"/api/{newsItem.Id}" });
            temp.Links.AddListReference("authors", newsItem.Authors.Select(a => new {href = $"/api/authors{a.Id}"}));
            temp.Links.AddListReference("categories", newsItem.Categories.Select(a => new {href = $"/api/categories/{a.Id}"}));
            
            return temp;
        }
        public static AuthorDto AddHyperMediaToModel(Author auth)
        {
            var temp = new AuthorDto
            {
                Id = auth.Id,
                Name = auth.Name
            };

            temp.Links.AddReference("self", new { href = $"/api/{auth.Id}" });
            temp.Links.AddReference("edit", new { href = $"/api/{auth.Id}" });
            temp.Links.AddReference("delete", new { href = $"/api/{auth.Id}" });
            temp.Links.AddReference("newsItems", new { href = $"/api/authors/{auth.Id}/newsItems" });
            temp.Links.AddListReference("newsItemsDetailed", auth.NewsItems.Select(a => new { href = $"/api/{a.Id}"}));
            
            return temp;
        }

        public void LinkAuthorNewsItem(int authorId, int newsId)
        {
            var auth = _dbContext.Authors.Include(c => c.NewsItems).FirstOrDefault<Author>(c => c.Id == authorId);
            var nItem = _dbContext.NewsItems.First(a => a.Id == newsId);
            auth.NewsItems.Add(nItem);
            _dbContext.SaveChanges();
        }

        public static AuthorDetailDto AddHyperMediaToModelDetail(Author auth)
        {
            var temp = new AuthorDetailDto
            {
                Id = auth.Id,
                Name = auth.Name,
                ProfileImgSource = auth.ProfileImgSource,
                Bio = auth.Bio
            };

            temp.Links.AddReference("self", new { href = $"/api/authors/{auth.Id}" });
            temp.Links.AddReference("edit", new { href = $"/api/authors/{auth.Id}" });
            temp.Links.AddReference("delete", new { href = $"/api/authors/{auth.Id}" });
            temp.Links.AddReference("newsItems", new { href = $"/api/authors/{auth.Id}/newsItems" });
            temp.Links.AddListReference("newsItemsDetailed", auth.NewsItems.Select(a => new { href = $"/api/{a.Id}"}));
            
            return temp;
        }

        
    }
}