import React, { useState, useEffect } from "react";
import { useParams } from "react-router-dom";

const StopDetailsPage: React.FC = () => {
  const { id } = useParams();
  const [stopDetails, setStopDetails] = useState<any>(null);
  const [currentDateTime, setCurrentDateTime] = useState(new Date());

  // Fetch stop details from the API
  useEffect(() => {
    const fetchStopDetails = async () => {
      try {
        const response = await fetch(`http://localhost:5000/api/stops/${id}`);
        if (!response.ok) {
          throw new Error('Failed to fetch stop details');
        }
        const data = await response.json();
        setStopDetails(data);
      } catch (error) {
        console.error("Error fetching stop details:", error);
      }
    };

    fetchStopDetails();

    // Update the current time every second
    const interval = setInterval(() => {
      setCurrentDateTime(new Date());
    }, 1000);

    return () => clearInterval(interval);
  }, [id]);

  // Function to check if the departure time is the closest to the current time
  const isClosestTime = (time: string) => {
    if (!currentDateTime) return false;

    const [hour, minute] = time.split(":").map(Number);
    const now = new Date(currentDateTime);

    // Departure time in milliseconds
    const departureTime = new Date(now.getFullYear(), now.getMonth(), now.getDate(), hour, minute).getTime();
    const currentTime = now.getTime();

    // Check if the departure time is the closest in the next hour
    return departureTime > currentTime && departureTime - currentTime <= 2400000;
  };

  if (!stopDetails) {
    return <div>Ładowanie danych...</div>;
  }

  return (
    <div className="max-w-[1920px] mx-auto p-4">
      <h1 className="text-2xl font-bold mb-4">{stopDetails.name}</h1>
      <div className="bg-white rounded-lg shadow">
        <table className="min-w-full">
          <thead>
            <tr className="bg-gray-800 text-white">
              <th className="px-6 py-3 text-left">Godzina</th>
              <th className="px-6 py-3 text-left">Dni robocze</th>
              <th className="px-6 py-3 text-left">Weekend</th>
              <th className="px-6 py-3 text-left">Święta</th>
            </tr>
          </thead>
          <tbody>
            {Array.from({ length: 18 }, (_, i) => `${i + 6}:00`).map((hour) => (
              <tr key={hour} className="bg-gray-700 text-white">
                <td className="px-6 py-4">{hour}</td>
                {["weekdays", "weekends", "holidays"].map((type) => (
                  <td key={type} className="px-6 py-4">
                    <div className="flex flex-row items-center gap-2">
                      {stopDetails.departures[type]
                        .filter((time: string) => {
                          const departureHour = parseInt(time.split(":")[0], 10); // Parse hour
                          const currentHour = parseInt(hour.split(":")[0], 10); // Hour in the table
                          return departureHour === currentHour; // Compare hours
                        })
                        .map((time: string) => (
                          <div
                            key={time}
                            className={`p-2 rounded-lg ${
                              isClosestTime(time) ? "bg-green-500 text-white" : "bg-gray-500 text-white"
                            }`}
                          >
                            {time.split(":")[1]} {/* Display minutes */}
                          </div>
                        ))}
                    </div>
                  </td>
                ))}
              </tr>
            ))}
          </tbody>
        </table>
      </div>
    </div>
  );
};

export default StopDetailsPage;