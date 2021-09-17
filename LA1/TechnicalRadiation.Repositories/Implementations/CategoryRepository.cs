using TechnicalRadiation.Repositories.Interfaces;
using System.Collections.Generic;
using TechnicalRadiation.Models.Dtos;
using TechnicalRadiation.Models.Entities;
using TechnicalRadiation.Repositories.Contexts;
using System.Linq;

namespace TechnicalRadiation.Repositories.Implementations
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly NewsDbContext _dbContext;
        public CategoryRepository(NewsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<CategoryDto> GetAllCategories()
        {
            return _dbContext.Categories.Select(c => new CategoryDto
            {
                Id = c.Id,
                Name = c.Name,
                Slug = c.Slug
            }).ToList();
        }

        public bool DoesExist(int id) => _dbContext.Categories.Any(g => g.Id == id);

        public CategoryDto GetCategoryById(int id)
        {
            Category category = _dbContext.Categories.FirstOrDefault<Category>(c => c.Id == id);
            return new CategoryDto
            {
                Id = category.Id,
                Name = category.Name,
                Slug = category.Slug
            };
        }
    }
}