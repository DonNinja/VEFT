using TechnicalRadiation.Models.InputModels;
using TechnicalRadiation.Models.Dtos;
using TechnicalRadiation.Models.Entities;
using System.Collections.Generic;

namespace TechnicalRadiation.Repositories.Interfaces
{
    public interface INewsItemRepository
    {
        IEnumerable<NewsItemDto> GetAllNewsItems();
        IEnumerable<NewsItemDetailDto> GetAllNewsItemsDetails();
        bool DoesExist(int id);
        NewsItemDetailDto GetNewsItemById(int id);
        IEnumerable<NewsItemDto> GetAuthorNewsItems(int id);

        int CreateNewsItem(NewsItemInputModel nItem);
        void UpdateNewsItemById(int id, NewsItemInputModel uModel);
        void DeleteNewsItemById(int id);
    }

}
