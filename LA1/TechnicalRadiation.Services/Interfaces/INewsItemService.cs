using TechnicalRadiation.Models.InputModels;
using TechnicalRadiation.Models.Dtos;
using System.Collections.Generic;

namespace TechnicalRadiation.Services.Interfaces
{
    public interface INewsItemService
    {
        IEnumerable<NewsItemDto> GetAllNewsItems();
        NewsItemDetailDto GetNewsItemById(int id);
        IEnumerable<NewsItemDto> GetNewsPages(int pageSize, int pageNumber);
        IEnumerable<NewsItemDetailDto> GetAllNewsItemsDetails();
        int CreateNewsItem(NewsItemInputModel nItem);
        bool UpdateNewsItemById(int id, NewsItemInputModel uItem);
        NewsItemDetailDto DeleteNewsItemById(int id);
    }
}