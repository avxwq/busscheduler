import React, { useState } from 'react';
import Stop from '../../models/Stop';

interface StopFormProps {
  onSubmit: (stop: Partial<Stop>) => void;
  initialData?: Stop | null;
}

export default function StopForm({ onSubmit, initialData = null }: StopFormProps) {
  const [formData, setFormData] = useState(initialData || {
    name: '',
    location: '',
    zone: ''
  });

  const handleSubmit = (e: React.FormEvent) => {
    e.preventDefault();
    onSubmit(formData);
  };

  return (
    <form onSubmit={handleSubmit} className="space-y-4">
      <div>
        <label className="block mb-1">Nazwa przystanku</label>
        <input
          type="text"
          value={formData.name}
          onChange={e => setFormData({ ...formData, name: e.target.value })}
          className="w-full p-2 border rounded bg-gray-300 text-black"
          required
        />
      </div>
      <div>
        <label className="block mb-1">Lokalizacja</label>
        <input
          type="text"
          value={formData.location}
          onChange={e => setFormData({ ...formData, location: e.target.value })}
          className="w-full p-2 border rounded bg-gray-300 text-black"
          required
        />
      </div>
      <div>
        <label className="block mb-1 ">Strefa</label>
        <input
          type="text"
          value={formData.zone}
          onChange={e => setFormData({ ...formData, zone: e.target.value })}
          className="w-full p-2 border rounded bg-gray-300 text-black"
          required
        />
      </div>
      <button 
        type="submit" 
        className="w-full px-4 py-2 bg-orange-400 text-white rounded hover:bg-orange-500"
      >
        {initialData ? 'Zapisz zmiany' : 'Dodaj przystanek'}
      </button>
    </form>
  );
}