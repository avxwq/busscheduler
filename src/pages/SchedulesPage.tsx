import React, { useState, useEffect } from 'react';
import { useParams, useNavigate } from 'react-router-dom';
import Stop from '../models/Stop';

export default function SchedulePage() {
  const { id } = useParams();
  const navigate = useNavigate();
  const [scheduleDetails, setScheduleDetails] = useState<any>(null);
  const [loading, setLoading] = useState<boolean>(true);  // To show loading state
  const [error, setError] = useState<string | null>(null);  // To handle errors

  // Fetch schedule details from API
  useEffect(() => {
    if (!id) return; // Prevent API call if id is not available

    const fetchScheduleDetails = async () => {
      setLoading(true);
      setError(null);

      try {
        const response = await fetch(`http://localhost:5000/api/routes/${id}`);
        
        if (!response.ok) {
          throw new Error('Failed to fetch schedule details');
        }

        const data = await response.json();
        console.log(data);
        setScheduleDetails(data); // Set API data
      } catch (err: any) {
        setError(err.message); // Set error message
      } finally {
        setLoading(false);
      }
    };

    fetchScheduleDetails();
  }, [id]);

  const handleStopClick = (stopId: number) => {
    navigate(`/stops/${stopId}`);
  };

  if (loading) {
    return <div>Loading...</div>;
  }

  if (error) {
    return <div>Error: {error}</div>;
  }

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
            {scheduleDetails?.stops?.map((stop: Stop) => (
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