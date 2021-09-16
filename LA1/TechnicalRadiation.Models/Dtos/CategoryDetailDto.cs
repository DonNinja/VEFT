namespace TechnicalRadiation.Models.Dtos
{
    public class CategoryDetailDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Slug { get; set; }    // what is Slug???????
        public int NumberOfNewsItems { get; set; }
    }
}