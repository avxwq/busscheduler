import React from 'react';
import { Stop } from '../../models/Stop';
import { BusLine } from '../../models/BusLine';

interface LinesListProps {
  lines: BusLine[];
  stops: Stop[];
  onDelete: (id: number) => void;
  onEdit: (line: BusLine) => void;
  onSort: (type: 'number' | 'stops') => void;
}

export default function LinesList({ lines, stops, onDelete, onEdit, onSort }: LinesListProps) {
  const getStopName = (stopId: number) => {
    const stop = stops.find(s => s.id === stopId);
    return stop ? stop.name : 'Nieznany przystanek';
  };

  return (
    <div className="space-y-4">
      <div className="flex gap-2 mb-4">
        <button 
          onClick={() => onSort('number')} 
          className="px-4 py-2 bg-orange-400 text-white rounded hover:bg-orange-500"
        >
          Sortuj po numerze
        </button>
        <button 
          onClick={() => onSort('stops')} 
          className="px-4 py-2 bg-orange-400 text-white rounded hover:bg-orange-500"
        >
          Sortuj po liczbie przystanków
        </button>
      </div>
      <div className="grid gap-4">
        {lines.map(line => (
          <div key={line.id} className="p-4 border rounded shadow-sm bg-[#213547] border-[#25354b]">
            <div className="flex justify-between items-center mb-2">
              <h3 className="text-lg font-semibold">Linia {line.number}</h3>
              <div className="flex gap-2">
                <button 
                  onClick={() => onEdit(line)} 
                  className="px-3 py-1 bg-yellow-500 text-white rounded hover:bg-yellow-600"
                >
                  Edytuj
                </button>
                <button 
                  onClick={() => onDelete(line.id)} 
                  className="px-3 py-1 bg-red-500 text-white rounded hover:bg-red-600"
                >
                  Usuń
                </button>
              </div>
            </div>
            <div>
              <p className="font-medium mb-1">Przystanki ({line.stops.length}):</p>
              <div className="grid grid-cols-2 gap-2">
                {line.stops.map((stopId, index) => (
                  <div key={stopId} className="flex items-center">
                    <span className="mr-2">{index + 1}.</span>
                    {getStopName(stopId)}
                  </div>
                ))}
              </div>
            </div>
          </div>
        ))}
      </div>
    </div>
  );
}