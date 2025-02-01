import React, { useState } from 'react';

const AdminDashboard = () => {
  const [stops, setStops] = useState([]);

  const handleAddStop = () => {
    const newStop = { id: stops.length + 1, name: `Przystanek ${stops.length + 1}` };
    setStops([...stops, newStop]);
  };

  const handleLogout = () => {
    localStorage.removeItem('adminToken'); // Wylogowanie użytkownika
    window.location.reload();
  };

  return (
    <div className="p-4">
      <h1 className="text-2xl font-bold mb-4">Panel Administracyjny</h1>
      <button
        onClick={handleAddStop}
        className="bg-green-500 text-white px-4 py-2 rounded mb-4"
      >
        Dodaj przystanek
      </button>
      <button
        onClick={handleLogout}
        className="bg-red-500 text-white px-4 py-2 rounded ml-4"
      >
        Wyloguj się
      </button>
      <ul className="mt-4">
        {stops.map((stop) => (
          <li key={stop.id} className="border p-2 mb-2">
            {stop.name}
          </li>
        ))}
      </ul>
    </div>
  );
};

export default AdminDashboard;
