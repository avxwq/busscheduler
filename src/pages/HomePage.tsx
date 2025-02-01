import React, { useState, useEffect } from 'react';
import { Link } from 'react-router-dom';
import CurrentDayAndTime from '../components/CurrentDay';
import Route from '../models/Route';

export default function HomePage() {
  const [routes, setRoutes] = useState<Route[]>([]); // Przechowywanie tras
  const [isLoading, setIsLoading] = useState<boolean>(true); // Stan ładowania
  const [error, setError] = useState<string | null>(null); // Stan błędu

  useEffect(() => {
    const fetchRoutes = async () => {
      try {
        const response = await fetch('http://localhost:3000/api/routes'); // Endpoint API
        if (!response.ok) {
          throw new Error('Nie udało się pobrać tras z serwera.');
        }
        const data: Route[] = await response.json(); // Oczekiwanie na dane JSON
        setRoutes(data);
        setIsLoading(false);
      } catch (err: any) {
        setError(err.message);
        setIsLoading(false);
      }
    };

    fetchRoutes();
  }, []);

  return (
    <div className="max-w-[1920px] mx-auto p-4">
      <div className='grid grid-cols-2 gap-8 ml-4'>
        <div>
          <h1 className="text-2xl font-bold mb-4">Rozkład Jazdy</h1>
          <p className="mb-6">Witamy na stronie z rozkładem jazdy busów w naszym mieście.</p>
        </div>
        <div className='justify-items-end mr-4'>
          <CurrentDayAndTime />
        </div>
      </div>

      {isLoading ? (
        <div className="text-center my-8">
          <p className="text-lg text-gray-500">Ładowanie tras...</p>
        </div>
      ) : error ? (
        <div className="text-center my-8">
          <p className="text-lg text-red-500">Wystąpił błąd: {error}</p>
        </div>
      ) : (
        <div className="grid grid-cols-2 md:grid-cols-3 lg:grid-cols-4 xl:grid-cols-8 gap-4">
          {routes.map((route) => (
            <Link 
              to={`/schedule/${route.id}`} 
              key={route.id}
              className="block"
            >
              <div className="bg-white rounded-lg shadow-md p-4 hover:shadow-lg transition-shadow duration-200 border border-gray-200 cursor-pointer transform hover:-translate-y-1 transition-transform">
                <div className="text-2xl font-bold text-blue-600 mb-2">
                  Linia {route.number}
                </div>
                <div className="text-sm text-gray-600">
                  <div className="truncate">{route.startPoint}</div>
                  <div className="flex items-center">
                    <svg className="w-4 h-4 mx-2" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                      <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M19 14l-7 7m0 0l-7-7m7 7V3" />
                    </svg>
                  </div>
                  <div className="truncate">{route.endPoint}</div>
                </div>
              </div>
            </Link>
          ))}
        </div>
      )}

      <div className="mt-8 bg-blue-50 rounded-lg p-6">
        <h2 className="text-xl font-semibold mb-3 text-black">Jak korzystać z rozkładu?</h2>
        <ul className="space-y-2 text-gray-700">
          <li>• Wybierz interesującą Cię linię autobusową klikając w odpowiedni kafelek</li>
          <li>• Sprawdź szczegółowy rozkład jazdy dla wybranej linii</li>
          <li>• Zobacz wszystkie przystanki na trasie</li>
        </ul>
      </div>
    </div>
  );
}
