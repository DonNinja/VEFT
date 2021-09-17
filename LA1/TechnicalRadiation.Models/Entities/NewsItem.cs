using System;
using System.Collections.Generic;

namespace TechnicalRadiation.Models.Entities {
    public class NewsItem {
    
        public int Id { get; set; }
        public string Title { get; set; }
        public string ImgSource { get; set; }
        public string ShortDescription { get; set; }
        public string LongDescription { get; set; }
        public DateTime PublishDate { get; set; }
        // Don't know if these need to be added
        public string ModifiedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public ICollection<Author> Authors { get; set; }
        public ICollection<Category> Categories { get; set; }
    }
}