using System;
using System.Threading.Tasks;

public interface IShortUrlService
{
    string GenerateShortUrl(string longUrl, int length);
    Task<ShortUrl> CreateShortUrlAsync(string userName, string longUrl, int length);
    Task<string> GetLongUrlAsync(string shortUrlCode);
}
