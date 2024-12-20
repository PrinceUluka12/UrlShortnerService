import React from 'react';

const ErrorPage = () => {
  return (
    <div className="container mt-5 text-center">
      <div className="alert alert-warning" role="alert">
        <h1>An Error Occurred</h1>
        <p>Sorry, there was an issue processing your request. Please try again later.</p>
      </div>
      <a href="/" className="btn btn-secondary mt-3">Go Back to Home</a>
    </div>
  );
};

export default ErrorPage;
