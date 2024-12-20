import React, { useEffect } from 'react';
import { useParams, useNavigate } from 'react-router-dom';
import axios from 'axios';


// Component responsible for handling the redirection logic
const RedirectHandler = () => {
  // Extract the shortUrl parameter from the route
    const { shortUrl } = useParams();
     // Hook to programmatically navigate the user
    const navigate = useNavigate();

    useEffect(() => {
        const redirectToLongUrl = async () => {
          try {
            // Make a request to the backend to get the long URL
            const response = await axios.get(`https://localhost:7038/api/UrlShortener/${shortUrl}`);
            
            setTimeout(() => {
              console.log(response.status)
              // If the response is successful (status 200), open the URL in a new tab
            if (response.status === 200 && response.data.isSuccess == true) {              
              window.open(response.data.result.longUrl, '_blank');
              window.close();
            } else {
               // If not found, navigate to the 'not-found' page
              navigate('/not-found'); 
            }
            }, 3000)
            
          } catch (error) {
            // Log the error and navigate to the 'error' page if there's an issue with the request
            console.error('Error fetching the original URL:', error);
            navigate('/error'); 
          }
        };
           // Trigger the redirection logic when the component mounts
        redirectToLongUrl();
      }, [shortUrl, navigate]);
    // Display a loading message while the redirection is processed
      return (
        <div className="container text-center mt-5">
          <div className="spinner-border text-primary" role="status">
            <span className="visually-hidden">Redirecting...</span>
          </div>
          <p className="mt-3">Redirecting...</p>
        </div>
      );
    };
    
    export default RedirectHandler;