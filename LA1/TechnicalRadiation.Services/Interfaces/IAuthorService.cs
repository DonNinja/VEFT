using TechnicalRadiation.Models.InputModels;
using TechnicalRadiation.Models.Dtos;
using System.Collections.Generic;
using Microsoft.Extensions.Primitives;

namespace TechnicalRadiation.Services.Interfaces
{
    public interface IAuthorService
    {
        IEnumerable<AuthorDto> GetAllAuthors();
        AuthorDetailDto GetAuthorById(int id);
        int CreateAuthor(AuthorInputModel newAuthor);
        void UpdateAuthorById(int id, AuthorInputModel newAuthor);
        void DeleteAuthorById(int id);
        bool IsValidToken(StringValues stringValues);
        void LinkAuthorNewsItem(int authorId, int newsId);
    }
}