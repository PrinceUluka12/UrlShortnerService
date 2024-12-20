using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public interface IUrlRepository
    {
        Task<UrlEntry> GetByShortUrlAsync(string shortUrl);
        Task<UrlEntry> CreateEntryAsync(UrlEntry entry);
        Task<UrlEntry> GetByLongUrlAsync(string longUrl);
    }
}
