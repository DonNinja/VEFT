using TechnicalRadiation.Models.InputModels;
using TechnicalRadiation.Models.Dtos;
using System.Collections.Generic;

namespace TechnicalRadiation.Repositories.Interfaces
{
    public interface ICategoryRepository
    {
        IEnumerable<CategoryDto> GetAllCategories();
        bool DoesExist(int id);
        CategoryDetailDto GetCategoryById(int id);
        int CreateCategory(CategoryInputModel newCat);
        bool UpdateCategoryById(int id, CategoryInputModel newCat);
    
        void DeleteCategoryById(int id);
        void LinkNewsToCategory(int catId, int newsId);
    }

}