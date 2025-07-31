export interface Purchase {
  id: number;
  userId: number;
  productId: number;
  quantity: number;
  purchaseDate: string;
}

export interface PurchaseDTO {
  id: number;
  userName: string;
  productName: string;
  productCategory: string;
  quantity: number;
  purchaseDate: string;
}