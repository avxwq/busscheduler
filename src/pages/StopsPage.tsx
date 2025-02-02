import React, { useState, useEffect } from "react";
import Stop from "../models/Stop"; // Import interfejsu

const StopsPage: React.FC = () => {
  const [stops, setStops] = useState<Stop[]>([]); // Stan przystanków
  const [formData, setFormData] = useState<Stop>({
    id: 0,
    name: "",
    location: "",
    zone: "",
    departures: {
      weekdays: [],
      weekends: [],
      holidays: [],
    },
  }); // Dane formularza
  const [errors, setErrors] = useState<any>({}); // Przechowywanie błędów walidacji
  const [isFormVisible, setIsFormVisible] = useState(false); // Stan do kontrolowania widoczności formularza

  useEffect(() => {
    // Pobieranie listy przystanków z bazy danych przy pierwszym renderze
    const fetchStops = async () => {
      try {
        const response = await fetch("http://localhost:5000/api/stops"); // Endpoint do pobierania przystanków
        if (!response.ok) {
          throw new Error(`Błąd HTTP: ${response.status}`);
        }
        const data = await response.json();
        setStops(data);
      } catch (error) {
        console.error("Błąd podczas pobierania przystanków:", error);
      }
    };

    fetchStops();
  }, []);

  const handleChange = (e: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement>) => {
    const { name, value } = e.target;
    setFormData({ ...formData, [name]: value });
  };

  const handleDeparturesChange = (e: React.ChangeEvent<HTMLTextAreaElement>, type: string) => {
    const { value } = e.target;
    const updatedDepartures = value.split(",").map((time) => time.trim());
    setFormData({
      ...formData,
      departures: {
        ...formData.departures,
        [type]: updatedDepartures,
      },
    });
  };

  const validateTimeFormat = (time: string): boolean => {
    const timePattern = /^([01]?[0-9]|2[0-3]):([0-5]?[0-9])$/;
    return timePattern.test(time);
  };

  const validateForm = (): boolean => {
    let formIsValid = true;
    let validationErrors: any = {};

    if (!formData.name) {
      validationErrors.name = "Nazwa przystanku jest wymagana.";
      formIsValid = false;
    }

    if (!formData.location) {
      validationErrors.location = "Lokalizacja jest wymagana.";
      formIsValid = false;
    }

    if (!formData.zone || !["A", "B", "C"].includes(formData.zone.toUpperCase())) {
      validationErrors.zone = "Strefa musi być jedną z liter: A, B lub C.";
      formIsValid = false;
    }

    const validateDepartures = (departures: string[], type: string) => {
      if (departures.length === 0) {
        validationErrors[type] = `Godziny odjazdów (${type}) są wymagane.`;
        formIsValid = false;
      } else {
        departures.forEach((time, index) => {
          if (!validateTimeFormat(time)) {
            validationErrors[`${type}_${index}`] = `Godzina \"${time}\" w ${type} jest niepoprawna. Format musi być HH:mm.`;
            formIsValid = false;
          }
        });
      }
    };

    validateDepartures(formData.departures.weekdays, "weekdays");
    validateDepartures(formData.departures.weekends, "weekends");
    validateDepartures(formData.departures.holidays, "holidays");

    setErrors(validationErrors);
    return formIsValid;
  };

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    if (!validateForm()) {
      try {
        if (formData.id === 0) {
          console.log("hello");
          // Dodawanie nowego przystanku
          const response = await fetch("http://localhost:5000/api/stops", {
            method: "POST",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify(formData),
          });
          if (!response.ok) {
            throw new Error(`Błąd HTTP: ${response.status}`);
          }
          const newStop = await response.json();
          setStops([...stops, newStop]);
        } else {
          // Aktualizowanie istniejącego przystanku
          await fetch(`http://localhost:5000/api/stops/${formData.id}`, {
            method: "PUT",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify(formData),
          });
          setStops(
            stops.map((stop) =>
              stop.id === formData.id ? { ...formData } : stop
            )
          );
        }

        setFormData({
          id: 0,
          name: "",
          location: "",
          zone: "",
          departures: {
            weekdays: [],
            weekends: [],
            holidays: [],
          },
        });
        setErrors({});
        setIsFormVisible(false);
      } catch (error) {
        console.error("Błąd podczas zapisywania przystanku:", error);
      }
    }
  };

  const handleDelete = async (id: number) => {
    try {
      const response = await fetch(`http://localhost:5000/api/stops/${id}`, { method: "DELETE" });
      if (!response.ok) {
        throw new Error(`Błąd HTTP: ${response.status}`);
      }
      setStops(stops.filter((stop) => stop.id !== id));
    } catch (error) {
      console.error("Błąd podczas usuwania przystanku:", error);
    }
  };

  const handleEdit = (id: number) => {
    const stopToEdit = stops.find((stop) => stop.id === id);
    if (stopToEdit) {
      setFormData(stopToEdit);
      setIsFormVisible(true);
    }
  };

  const handleAddNewStop = () => {
    setFormData({
      id: 0,
      name: "",
      location: "",
      zone: "",
      departures: {
        weekdays: [],
        weekends: [],
        holidays: [],
      },
    });
    setErrors({});
    setIsFormVisible(true);
  };

  const handleHideForm = () => {
    setIsFormVisible(false);
  };

  const sortByName = () => {
    const sortedStops = [...stops].sort((a, b) => a.name.localeCompare(b.name));
    setStops(sortedStops);
  };

  const sortById = () => {
    const sortedStops = [...stops].sort((a, b) => a.id - b.id);
    setStops(sortedStops);
  };

  return (
    <div className="container mx-auto p-4">
      <div className="flex justify-between items-center mb-4">
        <h1 className="text-2xl font-bold">Zarządzanie Przystankami</h1>
        <div className="flex space-x-2">
          <button
            onClick={sortByName}
            className="bg-blue-500 text-white px-4 py-2 rounded"
          >
            Sortuj po nazwie
          </button>
          <button
            onClick={sortById}
            className="bg-green-500 text-white px-4 py-2 rounded"
          >
            Sortuj po ID
          </button>
          <button
            onClick={handleAddNewStop}
            className="bg-orange-500 text-white px-4 py-2 rounded"
          >
            Dodaj Przystanek
          </button>
        </div>
      </div>

      {isFormVisible && (
        <form onSubmit={handleSubmit} className="mb-6 bg-gray-700 p-4 rounded">
          <h2 className="text-xl mb-2">{formData.id === 0 ? "Dodaj Nowy Przystanek" : "Edytuj Przystanek"}</h2>

          <div className="mb-4">
            <label className="block text-white">Nazwa Przystanku</label>
            <input
              type="text"
              name="name"
              value={formData.name}
              onChange={handleChange}
              className="border p-2 w-full bg-white max-w-xl text-black"
            />
            {errors.name && <p className="text-red-500 text-sm">{errors.name}</p>}
          </div>

          <div className="mb-4">
            <label className="block text-white">Lokalizacja</label>
            <input
              type="text"
              name="location"
              value={formData.location}
              onChange={handleChange}
              className="border p-2 w-full bg-white max-w-xl text-black"
            />
            {errors.location && <p className="text-red-500 text-sm">{errors.location}</p>}
          </div>

          <div className="mb-4">
            <label className="block text-white">Strefa</label>
            <input
              type="text"
              name="zone"
              value={formData.zone}
              onChange={handleChange}
              className="border p-2 w-full bg-white max-w-xl text-black"
            />
            {errors.zone && <p className="text-red-500 text-sm">{errors.zone}</p>}
          </div>

          <div className="mb-4">
            <label className="block text-white">Odjazdy (dni robocze)</label>
            <textarea
              name="weekdays"
              value={formData.departures.weekdays.join(", ")}
              onChange={(e) => handleDeparturesChange(e, "weekdays")}
              className="border p-2 w-full bg-white max-w-xl text-black"
              placeholder="Wpisz godziny oddzielone przecinkami"
            />
            {errors.weekdays && <p className="text-red-500 text-sm">{errors.weekdays}</p>}
          </div>

          <div className="mb-4">
            <label className="block text-white">Odjazdy (weekend)</label>
            <textarea
              name="weekends"
              value={formData.departures.weekends.join(", ")}
              onChange={(e) => handleDeparturesChange(e, "weekends")}
              className="border p-2 w-full bg-white max-w-xl text-black"
              placeholder="Wpisz godziny oddzielone przecinkami"
            />
            {errors.weekends && <p className="text-red-500 text-sm">{errors.weekends}</p>}
          </div>

          <div className="mb-4">
            <label className="block text-white">Odjazdy (święta)</label>
            <textarea
              name="holidays"
              value={formData.departures.holidays.join(", ")}
              onChange={(e) => handleDeparturesChange(e, "holidays")}
              className="border p-2 w-full bg-white max-w-xl text-black"
              placeholder="Wpisz godziny oddzielone przecinkami"
            />
            {errors.holidays && <p className="text-red-500 text-sm">{errors.holidays}</p>}
          </div>

          <button type="submit" className="bg-orange-500 text-white px-4 py-2 rounded">
            {formData.id === 0 ? "Dodaj Przystanek" : "Zapisz Zmiany"}
          </button>

          <button
            type="button"
            onClick={handleHideForm}
            className="bg-gray-500 text-white px-4 py-2 rounded ml-2"
          >
            Anuluj
          </button>
        </form>
      )}

      <h2 className="text-xl mb-2">Lista Przystanków</h2>
      <div className="grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-4 gap-4">
      {stops.map((stop) => (
          <div key={stop.id} className="bg-gray-800 p-4 rounded shadow">
            <p className="font-bold">{stop.name}</p>
            <p>Lokalizacja: {stop.location}</p>
            <p>Strefa: {stop.zone}</p>
            <p>Dni robocze: {stop.departures.weekdays.join(", ")}</p>
            <p>Weekend: {stop.departures.weekends.join(", ")}</p>
            <p>Święta: {stop.departures.holidays.join(", ")}</p>
            <div className="flex space-x-2 mt-2">
              <button
                onClick={() => handleEdit(stop.id)}
                className="bg-yellow-500 text-white px-4 py-2 rounded"
              >
                Edytuj
              </button>
              <button
                onClick={() => handleDelete(stop.id)}
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

export default StopsPage;