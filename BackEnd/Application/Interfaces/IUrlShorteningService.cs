using Domain.Entities;

namespace Application.Interfaces
{
    public interface IUrlShorteningService
    {
        public string GenerateShortUrl(int length);
        Task<string> CreateShortUrlAsync(string userName, string longUrl, int length);
        Task<string> GetLongUrlAsync(string shortUrl);
    }
}
