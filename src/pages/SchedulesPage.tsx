import React, { useState, useEffect } from 'react';
import { useParams, useNavigate } from 'react-router-dom';
import Stop from '../models/Stop';

export default function SchedulePage() {
  const { id } = useParams();
  const navigate = useNavigate();
  const [scheduleDetails, setScheduleDetails] = useState<any>(null);

  // Mock dane - później zastąpić wywołaniem API
  useEffect(() => {
    const mockData = {
      id: id || '',
      number: '101',
      startPoint: 'Centrum',
      endPoint: 'Dworzec',
      stops: [
        { id: 1, name: 'Centrum', location: 'ul. Główna 1', zone: 'A' },
        { id: 2, name: 'Dworzec', location: 'ul. Kolejowa 5', zone: 'A' },
        { id: 3, name: 'Parkowa', location: 'ul. Parkowa 12', zone: 'A' },
        { id: 4, name: 'Szpital', location: 'ul. Zdrowia 8', zone: 'A' },
        { id: 5, name: 'Uczelnia', location: 'ul. Akademicka 3', zone: 'A' },
      ],
    };
    setScheduleDetails(mockData);
  }, [id]);

  const handleStopClick = (stopId: number) => {
    navigate(`/stops/${stopId}`);
  };

  return (
    <div className="max-w-[1920px] mx-auto p-4">
      <div className="bg-white rounded-lg shadow">
        <table className="min-w-full">
          <thead>
            <tr className="bg-gray-800 text-white">
              <th className="px-6 py-3 text-left">Przystanek</th>
              <th className="px-6 py-3 text-left">Lokalizacja</th>
              <th className="px-6 py-3 text-left">Strefa</th>
            </tr>
          </thead>
          <tbody>
            {scheduleDetails?.stops.map((stop: Stop) => (
              <tr
                key={stop.id}
                onClick={() => handleStopClick(stop.id)}
                className="cursor-pointer bg-gray-700 hover:bg-gray-600 text-white"
              >
                <td className="px-6 py-4">{stop.name}</td>
                <td className="px-6 py-4">{stop.location}</td>
                <td className="px-6 py-4">{stop.zone}</td>
              </tr>
            ))}
          </tbody>
        </table>
      </div>
    </div>
  );
}
