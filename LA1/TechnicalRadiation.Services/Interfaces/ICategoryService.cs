using TechnicalRadiation.Models.InputModels;
using TechnicalRadiation.Models.Dtos;
using System.Collections.Generic;
using Microsoft.Extensions.Primitives;

namespace TechnicalRadiation.Services.Interfaces
{
    public interface ICategoryService
    {
        IEnumerable<CategoryDto> GetAllCategories();
        CategoryDetailDto GetCategoryById(int id);
        int CreateCategory(CategoryInputModel newCat);

        bool UpdateCategoryById(int id, CategoryInputModel newCat);
    
        void DeleteCategoryById(int id);

        void LinkNewsToCategory(int catId, int newsId);
        bool IsValidToken(StringValues stringValues);
    }
}