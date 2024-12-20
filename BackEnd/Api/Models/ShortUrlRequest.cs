namespace Api.Models
{
    public class ShortUrlRequest
    {
        public string UserName { get; set; }
        public string LongUrl { get; set; }
        public int Length { get; set; }
    }
}
