using Domain.Entities;
using Infrastructure.Repositories;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public class UrlShorteningService : IUrlShorteningService
    {
        private readonly IUrlRepository _repository;// Repository instance for data access
        private const string Characters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";// Character set used for generating short URLs

        // Constructor to inject the IUrlRepository dependency
        public UrlShorteningService(IUrlRepository repository)
        {
            _repository = repository;
        }

        // Method to create a new short URL or retrieve an existing one
        public async Task<string> CreateShortUrlAsync(string userName, string longUrl, int length)
        {
            try
            {
                // Check if the long URL already exists in the database
                var existingEntry = await _repository.GetByLongUrlAsync(longUrl);
                if (existingEntry != null)
                {
                    // Log a debug message and return the existing short URL
                    Log.Debug("Long Url Already exixts in database");
                    return existingEntry.ShortUrl;
                }

                string shortUrl;
                do
                {
                    // Generate a short URL of the specified length
                    shortUrl = GenerateShortUrl(length);
                }
                // Ensure the generated short URL is unique
                while (await _repository.GetByShortUrlAsync(shortUrl) != null);

                // Create a new UrlEntry object with user-provided data

                var newEntry = new UrlEntry
                {
                    UserName = userName,
                    LongUrl = longUrl,
                    ShortUrl = shortUrl,
                    ShortUrlLength = length,
                    CreatedAt = DateTime.UtcNow
                };
                // Save the new entry to the database
                await _repository.CreateEntryAsync(newEntry);
                return shortUrl;// Return the generated short URL
            }
            catch (Exception ex)
            {
                // Log any errors that occur and return null
                Log.Error("Error in CreateShortUrlAsync Method", ex.Message);
                return null;

            }

        }

        // Method to generate a short URL of the specified length
        public string GenerateShortUrl(int length)
        {
            var random = new Random();
            // Create a random string using the specified character set and length
            return new string(Enumerable.Repeat(Characters, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
        // Method to get the long URL associated with a short URL
        public async Task<string> GetLongUrlAsync(string shortUrl)
        {
            try
            {
                // Retrieve the URL entry by the short URL
                var existingEntry = await _repository.GetByShortUrlAsync(shortUrl);
                if (existingEntry != null)
                {
                    // Return the long URL if the entry is found
                    return existingEntry.LongUrl;
                }
                else
                {
                    // Log an error if the short URL does not exist
                    Log.Error($"Short Url {shortUrl} does not exist");
                    return null;
                }
            }
            catch (Exception ex)
            {
                // Log any errors that occur and return null
                Log.Error("Error in GetShortUrlAsync Method", ex.Message);
                return null;
            }

        }
    }
}
