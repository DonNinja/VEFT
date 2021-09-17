using TechnicalRadiation.Models.InputModels;
using TechnicalRadiation.Models.Dtos;
using System.Collections.Generic;

namespace TechnicalRadiation.Repositories.Interfaces
{
    public interface IAuthorRepository
    {
        IEnumerable<AuthorDto> GetAllAuthors();
        AuthorDto GetAuthorById(int id);
        bool DoesExist(int id);
        IEnumerable<NewsItemDto> GetAuthorNewsItems(int id);
    }

}