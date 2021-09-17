using TechnicalRadiation.Repositories.Interfaces;
using System.Collections.Generic;
using TechnicalRadiation.Models.Dtos;
using TechnicalRadiation.Models.Entities;
using TechnicalRadiation.Repositories.Contexts;
using System.Linq;
using Microsoft.EntityFrameworkCore;

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
            return _dbContext.Authors.Select(c => new AuthorDto
            {
                Id = c.Id,
                Name = c.Name
            }).ToList();
        }

        public AuthorDto GetAuthorById(int id)
        {
            Author author = _dbContext.Authors.FirstOrDefault<Author>(c => c.Id == id);
            return new AuthorDto
            {
                Id = author.Id,
                Name = author.Name
            };
        }

        public bool DoesExist(int id) => _dbContext.Authors.Any(g => g.Id == id);

        public IEnumerable<NewsItemDto> GetAuthorNewsItems(int id) {
            var auth = _dbContext.Authors.First(a => a.Id == id);

            _dbContext.Entry(auth).Collection(a => a.NewsItems).Load();
            var n = auth.NewsItems.Select(n => new NewsItemDto{
                Id = n.Id,
                Title = n.Title,
                ImgSource = n.ImgSource,
                ShortDescription = n.ShortDescription
            }).ToList();
            return n;

            
        }
    }
}