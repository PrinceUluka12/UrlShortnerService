import React from 'react';

const NotFound = () => {
  return (
    <div className="container mt-5 text-center">
      <div className="alert alert-danger" role="alert">
        <h1>404 - URL Not Found</h1>
        <p>The URL you are looking for does not exist or may have been removed.</p>
      </div>
      <a href="/" className="btn btn-primary mt-3">Go Back to Home</a>
    </div>
  );
};

export default NotFound;
