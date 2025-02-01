import React from 'react';
import { Link } from 'react-router-dom';

export default function Navigation() {
  return (
    <nav className="bg-gray-800 p-4 flex justify-between items-center">
      {/* Lewa część z linkami */}
      <div className="text-1/2xl mx-4 flex gap-4">
        <Link to="/" className="text-white hover:text-gray-300">Strona główna</Link>
        <Link to="/stops" className="text-white hover:text-gray-300">Przystanki</Link>
        <Link to="/lines" className="text-white hover:text-gray-300">Linie</Link>
        <Link to="/schedules" className="text-white hover:text-gray-300">Rozkład jazdy</Link>
      </div>

      {/* Prawa część z przyciskiem do logowania */}
      <div className="mr-4 text-1/2xl">
        <Link to="/login" className="text-white hover:text-gray-300">Panel Administracyjny</Link>
      </div>
    </nav>
  );
}
