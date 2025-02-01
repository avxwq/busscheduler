import React, { useState } from 'react';
import { Route } from '../../models/Route';

interface TimetableFilterProps {
  routes: Route[];
  onFilterChange: (filteredRoutes: Route[]) => void;
}

const TimetableFilter: React.FC<TimetableFilterProps> = ({ routes, onFilterChange }) => {
  const [routeNumber, setRouteNumber] = useState('');
  const [startPoint, setStartPoint] = useState('');
  const [endPoint, setEndPoint] = useState('');

  const handleFilter = () => {
    const filtered = routes.filter((route) => {
      const matchNumber = !routeNumber || route.number.includes(routeNumber);
      const matchStart = !startPoint || route.startPoint.toLowerCase().includes(startPoint.toLowerCase());
      const matchEnd = !endPoint || route.endPoint.toLowerCase().includes(endPoint.toLowerCase());
      return matchNumber && matchStart && matchEnd;
    });
    onFilterChange(filtered);
  };

  return (
    <div className="space-y-4 mb-6">
      <div className="grid grid-cols-1 md:grid-cols-3 gap-4">
        <input
          type="text"
          placeholder="Numer linii"
          value={routeNumber}
          onChange={(e) => setRouteNumber(e.target.value)}
          className="block w-full px-4 py-2 border border-gray-300 rounded-md focus:outline-none focus:ring-2 focus:ring-blue-500"
        />
        <input
          type="text"
          placeholder="Przystanek początkowy"
          value={startPoint}
          onChange={(e) => setStartPoint(e.target.value)}
          className="block w-full px-4 py-2 border border-gray-300 rounded-md focus:outline-none focus:ring-2 focus:ring-blue-500"
        />
        <input
          type="text"
          placeholder="Przystanek końcowy"
          value={endPoint}
          onChange={(e) => setEndPoint(e.target.value)}
          className="block w-full px-4 py-2 border border-gray-300 rounded-md focus:outline-none focus:ring-2 focus:ring-blue-500"
        />
      </div>
      <button
        onClick={handleFilter}
        className="px-4 py-2 bg-blue-500 text-white rounded-md hover:bg-blue-600 focus:outline-none focus:ring-2 focus:ring-blue-500"
      >
        Filtruj
      </button>
    </div>
  );
};

export default TimetableFilter;