using TechnicalRadiation.Models.InputModels;
using TechnicalRadiation.Models.Dtos;
using System.Collections.Generic;
using Microsoft.Extensions.Primitives;

namespace TechnicalRadiation.Services.Interfaces
{
    public interface INewsItemService
    {
        IEnumerable<NewsItemDto> GetAllNewsItems();
        NewsItemDetailDto GetNewsItemById(int id);
        IEnumerable<NewsItemDto> GetNewsPages(int pageSize, int pageNumber);
        IEnumerable<NewsItemDetailDto> GetAllNewsItemsDetails();
        IEnumerable<NewsItemDto> GetAuthorNewsItems(int id);
        int CreateNewsItem(NewsItemInputModel nItem);
        void UpdateNewsItemById(int id, NewsItemInputModel uItem);
        void DeleteNewsItemById(int id);
        bool IsValidToken(StringValues stringValues);
    }
}