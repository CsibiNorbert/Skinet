export interface IProduct {
    id: number;
    productName: string;
    price: number;
    productType: string;
    productBrand: string;

    description?: string;
    pictureUrl?: string;
  }