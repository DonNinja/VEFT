using TechnicalRadiation.Models.InputModels;
using TechnicalRadiation.Models.Dtos;
using System.Collections.Generic;

namespace TechnicalRadiation.Services.Interfaces
{
    public interface ICategoryService
    {
        IEnumerable<CategoryDto> GetAllCategories();
        CategoryDto GetCategoryById(int id);
    }
}