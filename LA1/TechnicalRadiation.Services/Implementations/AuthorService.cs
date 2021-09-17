using TechnicalRadiation.Models.InputModels;
using TechnicalRadiation.Models.Dtos;
using System.Collections.Generic;
using TechnicalRadiation.Services.Interfaces;
using TechnicalRadiation.Repositories.Interfaces;
using TechnicalRadiation.Models.Exceptions;
using System.Linq;

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

        public AuthorDto GetAuthorById(int id)
        {
            if (!_authorRepository.DoesExist(id))
            {
                throw new ResourceNotFoundException();
            }
            var a = _authorRepository.GetAuthorById(id);
            return new AuthorDto
            {
                Id = a.Id,
                Name = a.Name
            };

        }

        public IEnumerable<NewsItemDto> GetAuthorNewsItems(int id)
        {
            if (!_authorRepository.DoesExist(id))
            {
                throw new ResourceNotFoundException();
            }
            var a = _authorRepository.GetAuthorNewsItems(id);
            return a;

        }

    }

}
