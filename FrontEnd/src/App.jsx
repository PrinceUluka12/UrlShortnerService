import React from 'react';
import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
import HomePage from './components/HomePage';
import RedirectHandler from './components/RedirectHandler';
import NotFound from './components/NotFound';
import ErrorPage from './components/ErrorPage';
import 'bootstrap/dist/css/bootstrap.min.css';


function App() {
  return (
    <Router>
    <Routes>
      <Route path="/" element={<HomePage />} />
      <Route path="/redirect/:shortUrl" element={<RedirectHandler />} />
      <Route path="/not-found" element={<NotFound />} />
      <Route path="/error" element={<ErrorPage />} />
    </Routes>
  </Router>
  );
}

export default App;
