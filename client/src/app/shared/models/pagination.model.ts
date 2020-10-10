import { IProduct } from './product.model';

/*
  It`s a generic pagination
*/
export interface IPagination {
    pageIndex: number;
    pageSize: number;
    count: number;
    data: IProduct[];
  }
