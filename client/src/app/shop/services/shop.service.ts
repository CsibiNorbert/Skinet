import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { IPagination } from 'src/app/shared/models/pagination.model';

@Injectable({
  providedIn: 'root'
})
export class ShopService {

baseUrl = 'https://localhost:44347/api/';

constructor(
  private http: HttpClient) { }

public getProducts(): Observable<IPagination>{
  return this.http.get<IPagination>(this.baseUrl + 'products?pageSize=50');
}
}
