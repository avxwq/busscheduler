import Stop from '../models/Stop';

export default interface Route {
    id: string;
    number: string;
    startPoint: string;
    endPoint: string;
    stops?: Stop[];
  }