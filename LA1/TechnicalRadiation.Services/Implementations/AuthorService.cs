using TechnicalRadiation.Models.InputModels;
using TechnicalRadiation.Models.Dtos;
using System.Collections.Generic;
using TechnicalRadiation.Services.Interfaces;
using TechnicalRadiation.Repositories.Interfaces;
using TechnicalRadiation.Models.Exceptions;
using System.Linq;
using Microsoft.Extensions.Primitives;

namespace TechnicalRadiation.Services.Implementations
{
    public class AuthorService : IAuthorService
    {
        private readonly IAuthorRepository _authorRepository;

        public AuthorService(IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
        }

        public IEnumerable<AuthorDto> GetAllAuthors()
        {
            return _authorRepository.GetAllAuthors();
        }

        public AuthorDetailDto GetAuthorById(int id)
        {
            if (!_authorRepository.DoesExist(id))
            {
                throw new ResourceNotFoundException();
            }
            return _authorRepository.GetAuthorById(id);
        }
        public int CreateAuthor(AuthorInputModel newAuthor) {
            return _authorRepository.CreateAuthor(newAuthor);
        }

        public void UpdateAuthorById(int id, AuthorInputModel newAuthor)
        {
            // if (!_authorRepository.DoesExist())
            // {
                // throw new ResourceNotFoundException();
            // }

            _authorRepository.UpdateAuthorById(id, newAuthor);
        }

        public void DeleteAuthorById(int id)
        {
             _authorRepository.DeleteAuthorById(id);
        }

        public void LinkAuthorNewsItem(int authorId, int newsId)
        {
            _authorRepository.LinkAuthorNewsItem(authorId, newsId);
        }

        public bool IsValidToken(StringValues stringValues)
        {
            return "SecretToken" == stringValues;
        }
    }

}
