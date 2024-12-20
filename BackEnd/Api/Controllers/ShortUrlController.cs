using Api.Models;
using Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShortUrlController : ControllerBase
    {
        private readonly IUrlShorteningService _service;
        private readonly ResponseDto _response;

        // Constructor to inject the URL shortening service
        public ShortUrlController(IUrlShorteningService service)
        {
            _service = service;
            _response = new ResponseDto();
        }
        // Endpoint for creating a shortened URL
        [HttpPost("shorten")]
        public async Task<IActionResult> ShortenUrl([FromBody] UrlRequest request)
        {
            if (!ModelState.IsValid)
            {
                // Return bad request if input validation fails
                return BadRequest(ModelState);
            }
            try
            {
                // Call the service to create or get the shortened URL
                var shortUrl = await _service.CreateShortUrlAsync(request.UserName, request.LongUrl, request.Length);
                _response.Result = new { ShortUrl = shortUrl };
                return Ok(_response);
            }
            catch (Exception ex)
            {
                // Log the error and return a 500 Internal Server Error
                Log.Error(ex, "An error occurred while creating the short URL for user {UserName}", request.UserName);
                return StatusCode(500, new { Message = "An unexpected error occurred while processing your request. Please try again later." });
            }

        }
        // Endpoint for retrieving long URL using the shortened URL
        [HttpGet("{shortUrl}")]
        public async Task<IActionResult> RedirectToLongUrl(string shortUrl)
        {
            try
            {
                // Retrieve the original URL by the shortened version
                var entry = await _service.GetLongUrlAsync(shortUrl);
                if (entry == null)
                {
                    // Return URL not found if the shortened URL is not found in the DB
                    _response.IsSuccess = false;
                    _response.Message = "URL not found";
                    return NotFound(_response);
                }
                _response.Result = new { LongUrl = entry };
                return Ok(_response);
            }
            catch (Exception ex)
            {
                // Log the error and return a 500 Internal Server Error
                Log.Error(ex, "An error occurred while redirecting short URL: {ShortUrl}", shortUrl);
                return StatusCode(500, new { Message = "An unexpected error occurred. Please try again later." });
            }

        }
    }
}
