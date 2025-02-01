import React from 'react';
import { Route } from '../../models/Route';

interface TimetableListProps {
  routes: Route[];
}

const TimetableList: React.FC<TimetableListProps> = ({ routes }) => {
  return (
    <div className="overflow-x-auto">
      <table className="min-w-full divide-y divide-gray-200">
        <thead className="bg-gray-50">
          <tr>
            <th className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">Numer linii</th>
            <th className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">Z</th>
            <th className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">Do</th>
            <th className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">Odjazd</th>
            <th className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">Przyjazd</th>
            <th className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">Dni kursowania</th>
          </tr>
        </thead>
        <tbody className="bg-white divide-y divide-gray-200">
          {routes.map((route) => (
            <tr key={route.id}>
              <td className="px-6 py-4 whitespace-nowrap">{route.number}</td>
              <td className="px-6 py-4 whitespace-nowrap">{route.startPoint}</td>
              <td className="px-6 py-4 whitespace-nowrap">{route.endPoint}</td>
              <td className="px-6 py-4 whitespace-nowrap">{route.departureTime}</td>
              <td className="px-6 py-4 whitespace-nowrap">{route.arrivalTime}</td>
              <td className="px-6 py-4 whitespace-nowrap">
                {route.weekdays
                  .map((day, index) => day ? ['Pon', 'Wt', 'Śr', 'Czw', 'Pt', 'Sob', 'Ndz'][index] : '')
                  .filter(Boolean)
                  .join(', ')}
              </td>
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
};

export default TimetableList;