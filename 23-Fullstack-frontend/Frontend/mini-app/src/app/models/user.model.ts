export interface Address {
  citta: string;
  via: string;
  cap: string;
}

export interface User {
  id: number;
  name: string;
  address: Address;
}