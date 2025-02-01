import React, { useState } from 'react';
import { Stop } from '../../models/Stop';
import { BusLine } from '../../models/BusLine';

interface LineFormProps {
  onSubmit: (line: Partial<BusLine>) => void;
  initialData?: BusLine | null;
  stops: Stop[];
}

export default function LineForm({ onSubmit, initialData = null, stops }: LineFormProps) {
  const [formData, setFormData] = useState(initialData || {
    number: '',
    stops: []
  });

  const handleSubmit = (e: React.FormEvent) => {
    e.preventDefault();
    onSubmit(formData);
  };

  const handleStopToggle = (stopId: number) => {
    const newStops = formData.stops.includes(stopId)
      ? formData.stops.filter(id => id !== stopId)
      : [...formData.stops, stopId];
    setFormData({ ...formData, stops: newStops });
  };

  return (
    <form onSubmit={handleSubmit} className="space-y-4">
      <div>
        <label className="block mb-1">Numer linii</label>
        <input
          type="text"
          value={formData.number}
          onChange={e => setFormData({ ...formData, number: e.target.value })}
          className="w-full p-2 border rounded bg-gray-300 bg-gray-300 text-black"
          required
        />
      </div>
      <div>
        <label className="block mb-1">Wybierz przystanki</label>
        <div className="grid grid-cols-2 gap-2 max-h-60 overflow-y-auto p-2 border rounded bg-[#213547] border-[#25354b]">
          {stops.map(stop => (
            <label key={stop.id} className="flex items-center space-x-2 ">
              <input
                type="checkbox"
                checked={formData.stops.includes(stop.id)}
                onChange={() => handleStopToggle(stop.id)}
                className="rounded border-gray-300 bg-slate-100"
              />
              <span>{stop.name}</span>
            </label>
          ))}
        </div>
      </div>
      <button 
        type="submit" 
        className="px-4 py-2 bg-orange-400 text-white rounded hover:bg-orange-500"
      >
        {initialData ? 'Zapisz zmiany' : 'Dodaj linię'}
      </button>
    </form>
  );
}