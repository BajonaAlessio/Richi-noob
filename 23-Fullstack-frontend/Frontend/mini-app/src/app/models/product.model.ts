export interface Product {
  id: number;
  name: string;
  price: number;
  categoryId: number;
}

export interface ProductDTO {
  id: number;
  name: string;
  price: number;
  categoryName: string;
}