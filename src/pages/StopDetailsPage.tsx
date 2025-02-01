import React, { useState, useEffect } from "react";
import { useParams } from "react-router-dom";

const StopDetailsPage: React.FC = () => {
  const { id } = useParams();
  const [stopDetails, setStopDetails] = useState<any>(null);
  const [currentDateTime, setCurrentDateTime] = useState(new Date());

  // Mock dane - później zastąpić wywołaniem API
  useEffect(() => {
    const mockStopDetails = {
      id,
      name: `Przystanek ${id}`,
      departures: {
        weekdays: [
          "06:15", "06:45", "07:15", "07:45", "08:15", "08:45", 
          "09:08", "09:17", "09:31", "09:40", "09:48", "10:15", "10:30", "11:00", "11:15", 
          "12:00", "12:30", "13:00", "13:45", "14:30",
          "15:08", "15:17", "15:31", "15:40", "15:48", 
          "16:00", "16:45", "17:30", "18:00", "18:30", "19:15", 
          "20:00", "20:30", "21:00", "21:30", "22:15", "23:00"
        ],
        weekends: [
          "07:30", "08:15", "08:45", "09:30", "10:15", "10:45", 
          "11:15", "11:45", "12:30", "13:00", "13:45", "14:30", 
          "15:15", "16:00", "16:45", "17:30", "18:15", "18:45", 
          "19:30", "20:15", "21:00", "21:45", "22:30", "23:15"
        ],
        holidays: [
          "08:00", "08:30", "09:00", "09:45", "10:10", "10:30", 
          "11:15", "11:45", "12:20", "12:50", "13:30", "14:10", 
          "14:50", "15:30", "16:15", "16:50", "17:30", "18:00", 
          "18:45", "19:20", "20:00", "20:45", "21:30", "22:15"
        ]
      },     
    };
    setStopDetails(mockStopDetails);

    // Aktualizowanie aktualnego czasu co sekundę
    const interval = setInterval(() => {
      setCurrentDateTime(new Date());
    }, 1000);

    return () => clearInterval(interval);
  }, [id]);

  // Funkcja pomocnicza do porównania godzin
  const isClosestTime = (time: string) => {
    if (!currentDateTime) return false;

    const [hour, minute] = time.split(":").map(Number);
    const now = new Date(currentDateTime);

    // Czas odjazdu w milisekundach
    const departureTime = new Date(now.getFullYear(), now.getMonth(), now.getDate(), hour, minute).getTime();
    const currentTime = now.getTime();

    // Sprawdzenie, czy czas odjazdu jest najbliższy w ciągu najbliższej godziny
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
                          const departureHour = parseInt(time.split(":")[0], 10); // Parsowanie godziny
                          const currentHour = parseInt(hour.split(":")[0], 10); // Parsowanie godziny w tabeli
                          return departureHour === currentHour; // Porównanie godzin
                        })
                        .map((time: string) => (
                          <div
                            key={time}
                            className={`p-2 rounded-lg ${
                              isClosestTime(time) ? "bg-green-500 text-white" : "bg-gray-500 text-white"
                            }`}
                          >
                            {time.split(":")[1]} {/* Wyświetlanie minut */}
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
