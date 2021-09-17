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

        int CreateNewsItem(NewsItemInputModel nItem);
        bool UpdateNewsItemById(NewsItemInputModel uModel);
        NewsItemDetailDto DeleteNewsItemById(int id);
    }

}
