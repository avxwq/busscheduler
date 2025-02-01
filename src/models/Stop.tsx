export default interface Stop {
    id: number;
    name: string;
    location: string;
    zone: string;
    departures: {
      weekdays: string[];
      weekends: string[];
      holidays: string[];
    }
  }