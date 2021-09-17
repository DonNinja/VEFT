using TechnicalRadiation.Models.InputModels;
using TechnicalRadiation.Models.Dtos;
using System.Collections.Generic;

namespace TechnicalRadiation.Repositories.Interfaces
{
    public interface IAuthorRepository
    {
        IEnumerable<AuthorDto> GetAllAuthors();
        AuthorDetailDto GetAuthorById(int id);
        bool DoesExist(int id);
        int CreateAuthor(AuthorInputModel newAuthor);
        void UpdateAuthorById(int id, AuthorInputModel newAuthor);
    
        void DeleteAuthorById(int id);
        void LinkAuthorNewsItem(int authorId, int newsId);
    }

}