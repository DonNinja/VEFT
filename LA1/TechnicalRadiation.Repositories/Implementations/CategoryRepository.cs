using TechnicalRadiation.Repositories.Interfaces;
using System.Collections.Generic;
using TechnicalRadiation.Models.Dtos;
using TechnicalRadiation.Models.Entities;
using TechnicalRadiation.Repositories.Contexts;
using System.Linq;
using TechnicalRadiation.Models.Extensions;
using Microsoft.EntityFrameworkCore;
using TechnicalRadiation.Models.InputModels;

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
            var cats = _dbContext.Categories;
            List<CategoryDto> outputList = new List<CategoryDto>();
            foreach (var x in cats)
            {
                outputList.Add(AddHyperMediaToModel(x));
            }
            return outputList;
        }

        public bool DoesExist(int id) => _dbContext.Categories.Any(g => g.Id == id);

        public CategoryDetailDto GetCategoryById(int id)
        {
            Category category = _dbContext.Categories.Include(c => c.NewsItems).FirstOrDefault<Category>(c => c.Id == id);
            var catNoDetails = AddHyperMediaToModel(category);
            return new CategoryDetailDto
            {
                Id = catNoDetails.Id,
                Name = catNoDetails.Name,
                Slug = catNoDetails.Slug,
                NumberOfNewsItems = category.NewsItems.Count(),
                Links = catNoDetails.Links
            };
        }

        public int CreateCategory(CategoryInputModel newCat)
        {
            var nextId = _dbContext.Categories.Max(n => n.Id) + 1;
            var slug = newCat.Name.ToLower().Replace(' ', '-');
            var x = new Category
            {
                Id = nextId,
                Name = newCat.Name,
                Slug = slug,
                ModifiedBy = "TechnicalRadiationAdmin",
                CreatedDate = System.DateTime.Now,
                ModifiedDate = System.DateTime.Now
            };
            _dbContext.Categories.Add(x);
            _dbContext.SaveChanges();
            return x.Id;
        }

        public bool UpdateCategoryById(int id, CategoryInputModel uItem)
        {
            
            var category = _dbContext.Categories.FirstOrDefault(n => n.Id == id);
            var slug = uItem.Name.ToLower().Replace(' ', '-');
            category.Name = uItem.Name;
            category.Slug = slug;
            category.ModifiedDate = System.DateTime.Now;

            _dbContext.SaveChanges();
            
            return true;
        }

        public void DeleteCategoryById(int id)
        {
            var x = _dbContext.Categories.First(a => a.Id == id);
            _dbContext.Remove(x);
            _dbContext.SaveChanges();
        }

        public void LinkNewsToCategory(int catId, int newsId)
        {
            var cat = _dbContext.Categories.Include(c => c.NewsItems).FirstOrDefault<Category>(c => c.Id == catId);
            var nItem = _dbContext.NewsItems.First(a => a.Id == newsId);
            cat.NewsItems.Add(nItem);
            _dbContext.SaveChanges();
        }
        private CategoryDto AddHyperMediaToModel(Category category)
        {
            var temp = new CategoryDto
            {
                Id = category.Id,
                Name = category.Name,
                Slug = category.Slug
            };
            temp.Links.AddReference("self", new { href = $"/api/categories/{category.Id}" });
            temp.Links.AddReference("edit", new { href = $"/api/categories/{category.Id}" });
            temp.Links.AddReference("delete", new { href = $"/api/categories/{category.Id}" });

            return temp;
        }
    }
}