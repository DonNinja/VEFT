using TechnicalRadiation.Models.InputModels;
using TechnicalRadiation.Models.Dtos;
using System.Collections.Generic;

namespace TechnicalRadiation.Repositories.Interfaces
{
    public interface ICategoryRepository
    {
        IEnumerable<CategoryDto> GetAllCategories();
        bool DoesExist(int id);
        CategoryDto GetCategoryById(int id);
    }

}