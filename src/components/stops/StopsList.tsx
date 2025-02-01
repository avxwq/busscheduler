import React from 'react';
import { Link } from 'react-router-dom';
import { Stop } from '../../models/Stop';

interface StopsListProps {
  stops: Stop[];
  onDelete: (id: number) => void;
  onEdit: (stop: Stop) => void;
  onSort: (type: 'name' | 'id') => void;
}

export default function StopsList({ stops, onDelete, onEdit, onSort }: StopsListProps) {
  return (
    <div className="space-y-4">
      <div className="flex gap-2 mb-4">
        <button 
          onClick={() => onSort('name')} 
          className="px-4 py-2 bg-orange-400 text-white rounded hover:bg-orange-500"
        >
          Sortuj po nazwie
        </button>
        <button 
          onClick={() => onSort('id')} 
          className="px-4 py-2 bg-orange-400 text-white rounded hover:bg-orange-500"
        >
          Sortuj po ID
        </button>
      </div>
      <div className="grid gap-4">
        {stops.map(stop => (
          <div key={stop.id} className="p-4 border rounded shadow-sm bg-[#213547] border-[#25354b]">
            <h3 className="text-lg font-semibold">{stop.name}</h3>
            <p>Lokalizacja: {stop.location}</p>
            <p>Strefa: {stop.zone}</p>
            <div className="mt-2 flex gap-2">
              <button 
                onClick={() => onEdit(stop)} 
                className="px-3 py-1 bg-yellow-400 text-white rounded hover:bg-yellow-400"
              >
                Edytuj
              </button>
              <button 
                onClick={() => onDelete(stop.id)} 
                className="px-3 py-1 bg-red-400 text-white rounded hover:bg-red-400"
              >
                Usuń
              </button>
              <Link 
                to={`/stops/${stop.id}`}
                className="px-3 py-1 bg-green-400 text-white rounded hover:bg-green-400"
              >
                Szczegóły
              </Link>
            </div>
          </div>
        ))}
      </div>
    </div>
  );
}