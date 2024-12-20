namespace Domain.Entities
{
    public class UrlEntry
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string LongUrl { get; set; }
        public string ShortUrl { get; set; }
        public int ShortUrlLength { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
