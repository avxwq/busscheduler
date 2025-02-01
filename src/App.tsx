import React from 'react';
import { BrowserRouter as Router, Routes, Route, Navigate } from 'react-router-dom';
import Navigation from '../src/components/Navigation';
import HomePage from './pages/HomePage';
import StopsPage from './pages/StopsPage';
import StopDetailsPage from './pages/StopDetailsPage';
import LinesPage from './pages/LinesPage';
import SchedulesPage from './pages/SchedulesPage';
import AdminDashboard from './pages/AdminDashboard';
import LoginPage from './pages/LoginPage';

export default function App() {
  // Sprawdzenie, czy użytkownik jest zalogowany jako administrator
  const isAuthenticated = localStorage.getItem('adminToken');

  return (
    <Router>
      <div className="min-h-[1080px] min-w-[1920px] bg-slate-600">
        <Navigation />
        <Routes>
          {/* Publiczne widoki */}
          <Route path="/" element={<HomePage />} />
          <Route path="/stops" element={<StopsPage />} />
          <Route path="/stops/:id" element={<StopDetailsPage />} />
          <Route path="/lines" element={<LinesPage />} />
          <Route path="/schedule/:id" element={<SchedulesPage />} />

          {/* Widok logowania */}
          <Route path="/login" element={<LoginPage />} />

          {/* Panel administracyjny */}
          <Route
            path="/admin"
            element={
              isAuthenticated ? (
                <AdminDashboard />
              ) : (
                <Navigate to="/login" replace />
              )
            }
          />
        </Routes>
      </div>
    </Router>
  );
}

