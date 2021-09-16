using System;

namespace TechnicalRadiation.Models.Entities {
    public class NewsItem {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ImgSource { get; set; }
        public string ShortDescription { get; set; }
        public string LongDescription { get; set; }
        public DateTime PublishDate { get; set; }
        // Don't know if these need to be added
        // ModifiedBy (code-generated)
        // CreatedDate (code-generated)
        // ModifiedDate (code-generated)
    }
}