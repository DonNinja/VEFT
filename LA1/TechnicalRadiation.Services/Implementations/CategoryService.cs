using TechnicalRadiation.Models.InputModels;
using TechnicalRadiation.Models.Exceptions;
using TechnicalRadiation.Models.Dtos;
using System.Collections.Generic;
using TechnicalRadiation.Services.Interfaces;
using TechnicalRadiation.Repositories.Interfaces;
using System.Linq;
using Microsoft.Extensions.Primitives;

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

        public CategoryDetailDto GetCategoryById(int id) {
            if (!_categoryRepository.DoesExist(id))
            {
                throw new ResourceNotFoundException();
            }
            return _categoryRepository.GetCategoryById(id);
            
        }
        public int CreateCategory(CategoryInputModel newCat) {
            return _categoryRepository.CreateCategory(newCat);
        }

        public bool UpdateCategoryById(int id, CategoryInputModel newCat)
        {
            // if (!_categoryRepository.DoesExist())
            // {
                // throw new ResourceNotFoundException();
            // }

            return _categoryRepository.UpdateCategoryById(id, newCat);
        }

        public void DeleteCategoryById(int id)
        {
             _categoryRepository.DeleteCategoryById(id);
        }

        public void LinkNewsToCategory(int catId, int newsId)
        {
            _categoryRepository.LinkNewsToCategory(catId, newsId);
        }

        public bool IsValidToken(StringValues stringValues)
        {
            return "SecretToken" == stringValues;
        }
    }

}
