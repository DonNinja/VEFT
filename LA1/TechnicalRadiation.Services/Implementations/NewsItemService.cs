using TechnicalRadiation.Models.InputModels;
using TechnicalRadiation.Models.Exceptions;
using TechnicalRadiation.Models.Dtos;
using System.Collections.Generic;
using TechnicalRadiation.Services.Interfaces;
using TechnicalRadiation.Repositories.Interfaces;
using System.Linq;
using TechnicalRadiation.Models;
using TechnicalRadiation.Models.Extensions;
using Microsoft.Extensions.Primitives;

namespace TechnicalRadiation.Services.Implementations
{
    public class NewsItemService : INewsItemService
    {
        private readonly INewsItemRepository _newsItemRepository;

        public NewsItemService(INewsItemRepository newsItemRepository)
        {
            _newsItemRepository = newsItemRepository;
        }



        public IEnumerable<NewsItemDto> GetAllNewsItems()
        {
            var temp = _newsItemRepository.GetAllNewsItems().ToList();
            return temp;


        }

        public IEnumerable<NewsItemDetailDto> GetAllNewsItemsDetails()
        {
            var temp = _newsItemRepository.GetAllNewsItemsDetails().ToList();
            return temp;
        }

        public IEnumerable<NewsItemDto> GetNewsPages(int pageSize, int pageNumber)
        {
            IEnumerable<NewsItemDetailDto> n = GetAllNewsItemsDetails();
            IEnumerable<NewsItemDto> p = n.OrderBy(n => n.PublishDate).Select(n => new NewsItemDto
            {
                Id = n.Id,
                Title = n.Title,
                ImgSource = n.ImgSource,
                ShortDescription = n.ShortDescription,
                Links = n.Links
            });
            var e = new Envelope<NewsItemDto>(pageNumber, pageSize, p);
            return e.Items;
        }

        public NewsItemDetailDto GetNewsItemById(int id)
        {
            if (!_newsItemRepository.DoesExist(id))
            {
                throw new ResourceNotFoundException();
            }
            var n = _newsItemRepository.GetNewsItemById(id);
            return n;

        }

        public IEnumerable<NewsItemDto> GetAuthorNewsItems(int id)
        {
            // if (!_authorRepository.DoesExist(id))
            // {
            //     throw new ResourceNotFoundException();
            // }
            var a = _newsItemRepository.GetAuthorNewsItems(id);
            return a;
        }

        public int CreateNewsItem(NewsItemInputModel nItem)
        {
            return _newsItemRepository.CreateNewsItem(nItem);
        }
        public void UpdateNewsItemById(int id, NewsItemInputModel uModel)
        {
            if (!_newsItemRepository.DoesExist(id))
            {
                throw new ResourceNotFoundException();
            }

            _newsItemRepository.UpdateNewsItemById(id, uModel);
        }

        public void DeleteNewsItemById(int id)
        {
            _newsItemRepository.DeleteNewsItemById(id);
        }

        public bool IsValidToken(StringValues stringValues)
        {
            return "SecretToken" == stringValues;
        }
    }

}
