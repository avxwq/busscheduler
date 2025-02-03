import React, { useState, useEffect } from "react";
import Stop from "../models/Stop";
import Route from "../models/Route";

const LinesPage: React.FC = () => {
  const [stops, setStops] = useState<Stop[]>([]);
  const [routes, setRoutes] = useState<Route[]>([]);
  const [editingRoute, setEditingRoute] = useState<Route | null>(null);
  const [newRoute, setNewRoute] = useState<Route>({
    id: "0",
    number: "",
    startPoint: "",
    endPoint: "",
    stops: [],
  });
  const [selectedStops, setSelectedStops] = useState<Stop[]>([]);
  const [searchTerm, setSearchTerm] = useState<string>("");

  useEffect(() => {
    const fetchStops = async () => {
      const response = await fetch("http://localhost:5000/api/stops");
      const data: Stop[] = await response.json();
      setStops(data);
    };

    const fetchRoutes = async () => {
      const response = await fetch("http://localhost:5000/api/routes");
      const data: Route[] = await response.json();
      setRoutes(data);
    };

    fetchStops();
    fetchRoutes();
  }, []);

  const saveRoute = async () => {
    if (!newRoute.number || !newRoute.startPoint || !newRoute.endPoint || selectedStops.length === 0) {
      alert("Wypełnij wszystkie pola i dodaj przystanki!");
      return;
    }

    const routeToSave: Route = {
      ...newRoute,
      stops: selectedStops, // Wysyłamy pełne obiekty Stop[]
    };

    console.log("Wysyłane dane:", JSON.stringify(routeToSave, null, 2)); // 🔍 Debugowanie

    const method = editingRoute ? "PUT" : "POST";
    const endpoint = editingRoute
      ? `http://localhost:5000/api/routes/${editingRoute.id}`
      : "http://localhost:5000/api/routes";

    try {
      const response = await fetch(endpoint, {
        method,
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify(routeToSave),
      });

      const result = await response.json().catch(() => null);

      if (!response.ok) {
        console.error("Błąd API:", result);
        alert(`Błąd: ${result?.message || "Nieznany problem"}`);
        return;
      }

      if (editingRoute) {
        setRoutes(routes.map((r) => (r.id === editingRoute.id ? { ...routeToSave, id: editingRoute.id } : r)));
      } else {
        setRoutes([...routes, { ...routeToSave, id: result.id }]);
      }

      resetForm();
    } catch (error) {
      console.error("Błąd sieci:", error);
    }
  };

  const resetForm = () => {
    setEditingRoute(null);
    setNewRoute({
      id: "0",
      number: "",
      startPoint: "",
      endPoint: "",
      stops: [],
    });
    setSelectedStops([]);
  };

  const handleAddStop = (stop: Stop) => {
    if (!selectedStops.find((s) => s.id === stop.id)) {
      setSelectedStops([...selectedStops, stop]);
    }
  };

  const handleRemoveStop = (stopId: number) => {
    setSelectedStops(selectedStops.filter((stop) => stop.id !== stopId));
  };

  // Filtrowanie przystanków na podstawie frazy wyszukiwania
  const filteredStops = stops.filter((stop) =>
    stop.name.toLowerCase().includes(searchTerm.toLowerCase())
  );

  return (
    <div className="container mx-auto p-4">
      <h1 className="text-2xl font-bold mb-4">Zarządzanie Liniami Autobusowymi</h1>
      <div className="mb-6 bg-gray-700 p-4 rounded text-white">
        <h2 className="text-xl mb-4">{editingRoute ? "Edytuj Linię" : "Dodaj Nową Linię"}</h2>
        <input
          type="text"
          placeholder="Numer linii"
          value={newRoute.number}
          onChange={(e) => setNewRoute({ ...newRoute, number: e.target.value })}
          className="p-2 rounded bg-gray-800 text-white w-full"
        />
        <input
          type="text"
          placeholder="Punkt początkowy"
          value={newRoute.startPoint}
          onChange={(e) => setNewRoute({ ...newRoute, startPoint: e.target.value })}
          className="p-2 rounded bg-gray-800 text-white w-full mt-2"
        />
        <input
          type="text"
          placeholder="Punkt końcowy"
          value={newRoute.endPoint}
          onChange={(e) => setNewRoute({ ...newRoute, endPoint: e.target.value })}
          className="p-2 rounded bg-gray-800 text-white w-full mt-2"
        />

        <h3 className="text-lg mt-4">Dostępne Przystanki:</h3>
        <input
          type="text"
          placeholder="Wyszukaj przystanki..."
          value={searchTerm}
          onChange={(e) => setSearchTerm(e.target.value)}
          className="p-2 rounded bg-gray-800 text-white mb-4 w-full"
        />
        <ul className="list-disc list-inside">
          {filteredStops.map((stop) => (
            <li key={stop.id} className="flex justify-between">
              {stop.name}
              <button onClick={() => handleAddStop(stop)} className="text-green-500 ml-4">
                Dodaj
              </button>
            </li>
          ))}
        </ul>

        <h3 className="text-lg mt-4">Wybrane Przystanki:</h3>
        <ul className="list-disc list-inside">
          {selectedStops.map((stop) => (
            <li key={stop.id} className="flex justify-between">
              {stop.name}
              <button onClick={() => handleRemoveStop(stop.id)} className="text-red-500 ml-4">
                Usuń
              </button>
            </li>
          ))}
        </ul>

        <button onClick={saveRoute} className="bg-green-500 text-white px-4 py-2 rounded mt-4">
          {editingRoute ? "Zapisz Zmiany" : "Zapisz Linię"}
        </button>
      </div>

      {/* Wyświetlanie listy zapisanych linii */}
      <div className="mt-8 bg-gray-700 p-4 rounded text-white">
        <h2 className="text-xl mb-4">Lista Linii</h2>
        <ul className="list-disc list-inside">
          {routes.map((route) => (
            <li key={route.id} className="flex justify-between">
              <span>
                {route.number} - {route.startPoint} → {route.endPoint}
              </span>
              <button
                onClick={() => {
                  setEditingRoute(route);
                  setNewRoute({ ...route, stops: route.stops });
                }}
                className="text-yellow-500 ml-4"
              >
                Edytuj
              </button>
            </li>
          ))}
        </ul>
      </div>
    </div>
  );
};

export default LinesPage;