import React, { useState } from 'react';
import axios from 'axios';

// Component for the home page where users can create shortened URLs
const HomePage = () => {
  // State hooks for managing form inputs and the response
  const [userName, setUserName] = useState('');
  const [longUrl, setLongUrl] = useState('');
  const [length, setLength] = useState(8);
  const [shortUrl, setShortUrl] = useState(null);

   // Handle form submission
  const handleSubmit = async (e) => {
    e.preventDefault();// Prevent default form submission behavior

    try {
      // Make a POST request to the API to create a shortened URL
      const response = await axios.post('https://localhost:7038/api/UrlShortener/shorten', {
        userName,
        longUrl,
        length
      });
// Set the response data (short URL) in the state
      setShortUrl(response.data.result.shortUrl);
    } catch (error) {
       // Log the error and display an alert if the request fails
      console.error('Error creating short URL:', error);
      alert('An error occurred while generating the short URL.');
    }
  };

  return (
    <div className="container mt-5">
      <div className="card p-4 shadow-lg">
        <h1 className="text-center mb-4">🌐 URL Shortening Service</h1>
        <form onSubmit={handleSubmit}>
          <div className="mb-3">
            <label className="form-label">User Name:</label>
            <input
              type="text"
              className="form-control"
              value={userName}
              onChange={(e) => setUserName(e.target.value)}
              placeholder="Enter your name"
              required
            />
          </div>
          <div className="mb-3">
            <label className="form-label">Long URL:</label>
            <input
              type="url"
              className="form-control"
              value={longUrl}
              onChange={(e) => setLongUrl(e.target.value)}
              placeholder="Enter the long URL"
              required
            />
          </div>
          <div className="mb-3">
            <label className="form-label">Short URL Length:</label>
            <input
              type="number"
              className="form-control"
              value={length}
              onChange={(e) => setLength(e.target.value)}
              min="5"
              max="50"
              required
            />
          </div>
          <button type="submit" className="btn btn-primary w-100">Generate Short URL</button>
        </form>

          {/* Display the generated short URL if available */}
        {shortUrl && (
          <div className="alert alert-success mt-4 text-center">
            <h4>Generated Short URL:</h4>
            <a href={`http://localhost:5173/redirect/${shortUrl}`} target="_blank" rel="noopener noreferrer" className="text-decoration-none">
              {`http://localhost:5173/redirect/${shortUrl}`}
            </a>
          </div>
        )}
      </div>
    </div>
  );
  
};

export default HomePage;
