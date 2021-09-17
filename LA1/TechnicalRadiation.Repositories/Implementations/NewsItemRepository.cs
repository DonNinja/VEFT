using TechnicalRadiation.Repositories.Interfaces;
using System.Collections.Generic;
using TechnicalRadiation.Models.Dtos;
using TechnicalRadiation.Models.Entities;
using TechnicalRadiation.Repositories.Contexts;
using System.Linq;
using TechnicalRadiation.Models.InputModels;
using Microsoft.EntityFrameworkCore;
using TechnicalRadiation.Models.Extensions;

namespace TechnicalRadiation.Repositories.Implementations
{
    public class NewsItemRepository : INewsItemRepository
    {
        private readonly NewsDbContext _dbContext;
        public NewsItemRepository(NewsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<NewsItemDto> GetAllNewsItems()
        {
            var newsItems = _dbContext.NewsItems;
            List<NewsItemDetailDto> withLinks = new List<NewsItemDetailDto>();
            List<NewsItemDto> withLinksDto = new List<NewsItemDto>();
            foreach (var x in withLinks)
            {
                withLinksDto.Add(new NewsItemDto {
                    Id = x.Id,
                    Title= x.Title,
                    ImgSource= x.ImgSource,
                    ShortDescription= x.ShortDescription,
                    Links= x.Links
                });

            }
            return withLinksDto;
        }

        public IEnumerable<NewsItemDetailDto> GetAllNewsItemsDetails()
        {
            var x = _dbContext.NewsItemsDetails.Include(newsItem => newsItem.Authors).Include(newsItem => newsItem.Categories);
            List<NewsItemDetailDto> outplst = new List<NewsItemDetailDto>();
            foreach (var nwsi in x )
            {
                outplst.Add(AddHyperMediaToModel(nwsi));
            };
            return outplst;
        } 

        public IEnumerable<NewsItemDto> GetAuthorNewsItems(int id) {
            var news = _dbContext.NewsItemsDetails
            .Include(newsItem => newsItem.Authors)
            .Include(newsItem => newsItem.Categories)
            .Where(a => a.Authors.Any(al => al.Id == id));

            List<NewsItemDetailDto> withLinks = new List<NewsItemDetailDto>();
            List<NewsItemDto> withLinksDto = new List<NewsItemDto>();
            foreach (var nwsi in news)
            {
                withLinks.Add(AddHyperMediaToModel(nwsi));
            }

            foreach (var x in withLinks)
            {
                withLinksDto.Add(new NewsItemDto {
                    Id = x.Id,
                    Title= x.Title,
                    ImgSource= x.ImgSource,
                    ShortDescription= x.ShortDescription,
                    Links= x.Links
                });

            }
            return withLinksDto;
        }

        public bool DoesExist(int id) => _dbContext.NewsItems.Any(n => n.Id == id);

        public NewsItemDetailDto GetNewsItemById(int id)
        {
            NewsItem newsItem = _dbContext.NewsItems.Include(newsItem => newsItem.Authors).Include(newsItem => newsItem.Categories).FirstOrDefault<NewsItem>(n => n.Id == id);
            
            return AddHyperMediaToModel(newsItem);
        }

        public int CreateNewsItem(NewsItemInputModel nItem)
        {
            var nextId = _dbContext.NewsItems.Last().Id + 1;
            var x = new NewsItem
            {
                Id = nextId,
                Title = nItem.Title,
                ImgSource = nItem.ImgSource,
                ShortDescription = nItem.ShortDescription,
                LongDescription = nItem.LongDescription,
                PublishDate = nItem.PublishDate,
                ModifiedBy = "TechnicalRadiationAdmin",
                CreatedDate = System.DateTime.Now,
                ModifiedDate = System.DateTime.Now
            };
            _dbContext.NewsItems.Add(x);
            _dbContext.SaveChanges();
            return x.Id;
        }

        public void UpdateNewsItemById(int id, NewsItemInputModel uItem)
        {
            var nItem = _dbContext.NewsItems.FirstOrDefault(n => n.Id == id);
            nItem.Title = uItem.Title;
            nItem.ImgSource = uItem.ImgSource;
            nItem.ShortDescription = uItem.ShortDescription;
            nItem.LongDescription = uItem.LongDescription;
            nItem.PublishDate = uItem.PublishDate;
            nItem.ModifiedDate = System.DateTime.Now;

            _dbContext.SaveChanges();
        }

        public void DeleteNewsItemById(int id)
        {
            var x = _dbContext.NewsItems.First(a => a.Id == id);
            _dbContext.Remove(x);
            _dbContext.SaveChanges();
        }
        public static NewsItemDetailDto AddHyperMediaToModel(NewsItem newsItem)
        {
            var temp = new NewsItemDetailDto
            {
                Id = newsItem.Id,
                Title = newsItem.Title,
                ImgSource = newsItem.ImgSource,
                ShortDescription = newsItem.ShortDescription,
                LongDescription = newsItem.LongDescription,
                PublishDate = newsItem.PublishDate
            };

            temp.Links.AddReference("self", new { href = $"/api/{newsItem.Id}" });
            temp.Links.AddReference("edit", new { href = $"/api/{newsItem.Id}" });
            temp.Links.AddReference("delete", new { href = $"/api/{newsItem.Id}" });
            temp.Links.AddListReference("authors", newsItem.Authors.Select(a => new {href = $"/api/authors/{a.Id}"}));
            temp.Links.AddListReference("categories", newsItem.Categories.Select(a => new {href = $"/api/categories/{a.Id}"}));
            
            return temp;
        }
    }
}

