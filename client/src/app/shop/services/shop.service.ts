import { HttpClient, HttpParams, HttpResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { IBrand } from 'src/app/shared/models/brands.model';
import { IPagination } from 'src/app/shared/models/pagination.model';
import { IType } from 'src/app/shared/models/product-type.model';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class ShopService {

  baseUrl = 'https://localhost:44347/api/';

  constructor(private http: HttpClient) { }

  public getProducts(brandId?: number, typeId?: number, sortValue?: string): Observable<IPagination>{
    // filter params object
    let params = new HttpParams();
    if (brandId) {
      params = params.append('brandId', brandId.toString());
    }
    if (typeId) {
      params = params.append('typeId', typeId.toString());
    }
    if (sortValue) {
      params = params.append('sort', sortValue);
    }

    return this.http.get<IPagination>(this.baseUrl + 'products', {observe: 'response', params})
    .pipe(
      // We map the HttpResponse to an IPagination
      map(response => {
        // the body will be the IPagination object
        return response.body;
      })
    );
  }

  public getBrands(): Observable<IBrand[]>{
    return this.http.get<IBrand[]>(this.baseUrl + 'products/brands');
  }

  public getTypes(): Observable<IType[]>{
    return this.http.get<IType[]>(this.baseUrl + 'products/types');
  }
}
