using TechnicalRadiation.Models.InputModels;
using TechnicalRadiation.Models.Dtos;
using System.Collections.Generic;

namespace TechnicalRadiation.Services.Interfaces
{
    public interface IAuthorService
    {
        IEnumerable<AuthorDto> GetAllAuthors();
        AuthorDto GetAuthorById(int id);
        IEnumerable<NewsItemDto> GetAuthorNewsItems(int id);
    }
}