import React, { useState, useEffect } from "react";
import Stop from "../models/Stop";
import Route from "../models/Route";

const LinesPage: React.FC = () => {
  const [stops, setStops] = useState<Stop[]>([]); // Przystanki pobrane z bazy danych
  const [routes, setRoutes] = useState<Route[]>([]); // Linie pobrane z bazy danych
  const [editingRoute, setEditingRoute] = useState<Route | null>(null); // Edytowana linia
  const [newRoute, setNewRoute] = useState<Route>({
    id: "",
    number: "",
    startPoint: "",
    endPoint: "",
    stops: [],
  });
  const [selectedStops, setSelectedStops] = useState<Stop[]>([]);

  // Pobieranie przystanków i tras z bazy danych
  useEffect(() => {
    const fetchStops = async () => {
      const response = await fetch("/api/stops");
      const data: Stop[] = await response.json();
      setStops(data);
    };

    const fetchRoutes = async () => {
      const response = await fetch("/api/routes");
      const data: Route[] = await response.json();
      setRoutes(data);
    };

    fetchStops();
    fetchRoutes();
  }, []);

  // Zapis nowej linii lub edytowanej linii do bazy danych
  const saveRoute = async (route: Route) => {
    const method = route.id ? "PUT" : "POST";
    const endpoint = route.id ? `/api/routes/${route.id}` : "/api/routes";

    const response = await fetch(endpoint, {
      method,
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify(route),
    });

    if (response.ok) {
      const savedRoute: Route = await response.json();
      if (route.id) {
        // Aktualizacja istniejącej linii
        setRoutes(routes.map((r) => (r.id === savedRoute.id ? savedRoute : r)));
      } else {
        // Dodanie nowej linii
        setRoutes([...routes, savedRoute]);
      }

      resetForm();
    } else {
      console.error("Błąd podczas zapisywania linii");
    }
  };

  // Usuwanie linii z bazy danych
  const deleteRoute = async (routeId: string) => {
    const response = await fetch(`/api/routes/${routeId}`, {
      method: "DELETE",
    });

    if (response.ok) {
      setRoutes(routes.filter((route) => route.id !== routeId));
    } else {
      console.error("Błąd podczas usuwania linii");
    }
  };

  // Przygotowanie formularza do edycji linii
  const startEditing = (route: Route) => {
    setEditingRoute(route);
    setNewRoute(route);
    setSelectedStops(route.stops || []);
  };

  // Resetowanie formularza
  const resetForm = () => {
    setEditingRoute(null);
    setNewRoute({
      id: "",
      number: "",
      startPoint: "",
      endPoint: "",
      stops: [],
    });
    setSelectedStops([]);
  };

  // Obsługa dodawania przystanku do linii
  const handleAddStop = (stop: Stop) => {
    if (!selectedStops.find((s) => s.id === stop.id)) {
      setSelectedStops([...selectedStops, stop]);
    }
  };

  // Obsługa usuwania przystanku z linii
  const handleRemoveStop = (stopId: number) => {
    setSelectedStops(selectedStops.filter((stop) => stop.id !== stopId));
  };

  // Obsługa zapisu linii
  const handleSaveRoute = () => {
    if (
      newRoute.number &&
      newRoute.startPoint &&
      newRoute.endPoint &&
      selectedStops.length > 0
    ) {
      const routeToSave: Route = {
        ...newRoute,
        stops: selectedStops,
      };
      saveRoute(routeToSave);
    } else {
      alert("Wypełnij wszystkie pola i dodaj przystanki!");
    }
  };

  return (
    <div className="container mx-auto p-4">
      <h1 className="text-2xl font-bold mb-4">Zarządzanie Liniami Autobusowymi</h1>

      {/* Formularz dodawania/edycji linii */}
      <div className="mb-6 bg-gray-700 p-4 rounded text-white">
        <h2 className="text-xl mb-4">
          {editingRoute ? "Edytuj Linię" : "Dodaj Nową Linię"}
        </h2>
        <div className="grid grid-cols-1 md:grid-cols-2 gap-4">
          <input
            type="text"
            placeholder="Numer linii"
            value={newRoute.number}
            onChange={(e) => setNewRoute({ ...newRoute, number: e.target.value })}
            className="p-2 rounded bg-gray-800 text-white"
          />
          <input
            type="text"
            placeholder="Punkt początkowy"
            value={newRoute.startPoint}
            onChange={(e) =>
              setNewRoute({ ...newRoute, startPoint: e.target.value })
            }
            className="p-2 rounded bg-gray-800 text-white"
          />
          <input
            type="text"
            placeholder="Punkt końcowy"
            value={newRoute.endPoint}
            onChange={(e) =>
              setNewRoute({ ...newRoute, endPoint: e.target.value })
            }
            className="p-2 rounded bg-gray-800 text-white"
          />
        </div>

        {/* Lista wybranych przystanków */}
        <h3 className="text-lg mt-4">Wybrane Przystanki:</h3>
        <ul className="list-disc list-inside">
          {selectedStops.map((stop) => (
            <li key={stop.id} className="flex justify-between">
              {stop.name}
              <button
                onClick={() => handleRemoveStop(stop.id)}
                className="text-red-500 ml-4"
              >
                Usuń
              </button>
            </li>
          ))}
        </ul>

        <div className="flex gap-4 mt-4">
          <button
            onClick={handleSaveRoute}
            className="bg-green-500 text-white px-4 py-2 rounded"
          >
            {editingRoute ? "Zapisz Zmiany" : "Zapisz Linię"}
          </button>
          {editingRoute && (
            <button
              onClick={resetForm}
              className="bg-gray-500 text-white px-4 py-2 rounded"
            >
              Anuluj
            </button>
          )}
        </div>
      </div>

      {/* Lista tras */}
      <h2 className="text-xl mb-4">Lista Linii</h2>
      <div className="grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-3 gap-4">
        {routes.map((route) => (
          <div key={route.id} className="bg-gray-800 p-4 rounded shadow">
            <p className="font-bold">Numer: {route.number}</p>
            <p>Początek: {route.startPoint}</p>
            <p>Koniec: {route.endPoint}</p>
            <p>Liczba przystanków: {route.stops?.length || 0}</p>
            <div className="flex gap-2 mt-2">
              <button
                onClick={() => startEditing(route)}
                className="bg-blue-500 text-white px-4 py-2 rounded"
              >
                Edytuj
              </button>
              <button
                onClick={() => deleteRoute(route.id)}
                className="bg-red-500 text-white px-4 py-2 rounded"
              >
                Usuń
              </button>
            </div>
          </div>
        ))}
      </div>
    </div>
  );
};

export default LinesPage;
