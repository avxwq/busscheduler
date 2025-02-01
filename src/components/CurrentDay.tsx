import React, { useState, useEffect } from "react";

const CurrentDayAndTime: React.FC = () => {
  const [currentDateTime, setCurrentDateTime] = useState(new Date());

  useEffect(() => {
    // Ustawiamy interwał, aby aktualizować czas co sekundę
    const interval = setInterval(() => {
      setCurrentDateTime(new Date());
    }, 1000);

    // Czyszczenie interwału przy unmount
    return () => clearInterval(interval);
  }, []);

  const daysOfWeek = [
    "Niedziela",
    "Poniedziałek",
    "Wtorek",
    "Środa",
    "Czwartek",
    "Piątek",
    "Sobota",
  ];

  // Pobieramy dane
  const dayName = daysOfWeek[currentDateTime.getDay()];
  const date = currentDateTime.toLocaleDateString("pl-PL", {
    year: "numeric",
    month: "long",
    day: "numeric",
  });
  const time = currentDateTime.toLocaleTimeString("pl-PL");

  return (
    <div style={{ textAlign: "center", fontSize: "18px", fontWeight: "bold",  }}>
      <p>{dayName}</p>
      <p>{date}</p>
      <p>{time}</p>
    </div>
  );
};

export default CurrentDayAndTime;

