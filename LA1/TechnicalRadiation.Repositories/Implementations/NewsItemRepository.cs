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
            return _dbContext.NewsItems.Select(n => new NewsItemDto
            {
                Id = n.Id,
                Title = n.Title,
                ImgSource = n.ImgSource,
                ShortDescription = n.ShortDescription
            });
        }

        public IEnumerable<NewsItemDetailDto> GetAllNewsItemsDetails()
        {
            var x = _dbContext.NewsItemsDetails.Include(newsItem => newsItem.Authors);

            var dtos = x.Select(n => new NewsItemDetailDto{
                Id = n.Id,
                Title = n.Title,
                ImgSource = n.ImgSource,
                ShortDescription = n.ShortDescription,
                LongDescription = n.LongDescription,
                PublishDate = n.PublishDate
            }).ToList();

            List<NewsItemDetailDto> outplst = new List<NewsItemDetailDto>();

            foreach (var nwsi in x )
            {
                var auths = nwsi.Authors.ToList();
                Dictionary<string, string> test = new Dictionary<string, string>();
                foreach (var ent in auths)
                {
                    test.Add("href", $"/api/authors/{ent.Id}");
                }
                List<Dictionary<string, string>> tmpDct = new List<Dictionary<string, string>>();
                tmpDct.Add(test);
                var currDto = new NewsItemDetailDto{
                    Id = nwsi.Id,
                    Title = nwsi.Title,
                    ImgSource = nwsi.ImgSource,
                    ShortDescription = nwsi.ShortDescription,
                    LongDescription = nwsi.LongDescription,
                    PublishDate = nwsi.PublishDate
                };

                currDto.Links.AddReference("self", new { href = $"/api/{currDto.Id}" });
                currDto.Links.AddReference("edit", new { href = $"/api/{currDto.Id}" });
                currDto.Links.AddReference("delete", new { href = $"/api/{currDto.Id}" });
                currDto.Links.AddListReference("authors", tmpDct);
                outplst.Add(currDto);
            };
 
            return outplst;
        } 

        public bool DoesExist(int id) => _dbContext.NewsItems.Any(n => n.Id == id);

        public NewsItemDetailDto GetNewsItemById(int id)
        {
            NewsItem newsItem = _dbContext.NewsItems.Include(newsItem => newsItem.Authors).FirstOrDefault<NewsItem>(n => n.Id == id);
            
            var temp = new NewsItemDetailDto
            {
                Id = newsItem.Id,
                Title = newsItem.Title,
                ImgSource = newsItem.ImgSource,
                ShortDescription = newsItem.ShortDescription,
                LongDescription = newsItem.LongDescription,
                PublishDate = newsItem.PublishDate
            };
            


            var auths = newsItem.Authors.ToList();
            Dictionary<string, string> test = new Dictionary<string, string>();
            foreach (var x in auths)
            {
                test.Add("href", $"/api/authors/{x.Id}");
            }
            List<Dictionary<string, string>> tmpp = new List<Dictionary<string, string>>();
            tmpp.Add(test);
            temp.Links.AddReference("self", new { href = $"/api/{newsItem.Id}" });
            temp.Links.AddReference("edit", new { href = $"/api/{newsItem.Id}" });
            temp.Links.AddReference("delete", new { href = $"/api/{newsItem.Id}" });
            temp.Links.AddListReference("authors", tmpp);
            return temp;
        }

        public int CreateNewsItem(NewsItemInputModel nItem)
        {
            var nextId = _dbContext.NewsItems.Count() + 1;
            var x = new NewsItem
            {
                Id = nextId,
                Title = nItem.Title,
                ImgSource = nItem.ImgSource,
                ShortDescription = nItem.ShortDescription,
                LongDescription = nItem.LongDescription,
                PublishDate = nItem.PublishDate,
                ModifiedBy = "Technical Radiation Admin",
                CreatedDate = System.DateTime.Now,
                ModifiedDate = System.DateTime.Now
            };
            _dbContext.NewsItems.Add(x);
            _dbContext.SaveChanges();
            return x.Id;
        }

        public bool UpdateNewsItemById(NewsItemInputModel uItem)
        {
            _dbContext.Update(uItem);
            _dbContext.SaveChanges();
            return true;
        }

        public NewsItemDetailDto DeleteNewsItemById(int id)
        {
            var x = _dbContext.NewsItems.First(a => a.Id == id);
            _dbContext.Remove(x);
            _dbContext.SaveChanges();
            return new NewsItemDetailDto
            {
                Id = x.Id,
                Title = x.Title,
                ImgSource = x.ImgSource,
                ShortDescription = x.ShortDescription,
                LongDescription = x.LongDescription,
                PublishDate = x.PublishDate
            };
        }
    }

}

