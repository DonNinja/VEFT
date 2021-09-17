using TechnicalRadiation.Models.InputModels;
using TechnicalRadiation.Models.Exceptions;
using TechnicalRadiation.Models.Dtos;
using System.Collections.Generic;
using TechnicalRadiation.Services.Interfaces;
using TechnicalRadiation.Repositories.Interfaces;
using System.Linq;

namespace TechnicalRadiation.Services.Implementations {
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }



        public IEnumerable<CategoryDto> GetAllCategories() {
            return _categoryRepository.GetAllCategories();
        }

        public CategoryDto GetCategoryById(int id) {
            if (!_categoryRepository.DoesExist(id))
            {
                throw new ResourceNotFoundException();
            }
            var c = _categoryRepository.GetCategoryById(id);
            return new CategoryDto {
                Id = c.Id,
                Name = c.Name,
                Slug = c.Slug
            };

        }
    }

}
