import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { Basket, IBasket, IBasketItem } from '../shared/models/basket.model';
import { IProduct } from '../shared/models/product.model';

@Injectable({
  providedIn: 'root'
})
export class BasketService {

  baseUrl = environment.baseUrl;
  private basketSource = new BehaviorSubject<IBasket>(null);
  basket$ = this.basketSource.asObservable();

  constructor(private http: HttpClient) { }


  getBasket(id: string){
    return this.http.get(this.baseUrl + 'basket?id=' + id)
            .pipe(
              map((basket: IBasket) => {
                this.basketSource.next(basket);
                console.log(this.getCurrentBasketValue(), 'cur');
              })
            );
  }

  setBasket(basket: IBasket) {
    return this.http.post(this.baseUrl + 'basket', basket).subscribe((response: IBasket) => {
      this.basketSource.next(response);
      console.log(response);
    }, error => {
      console.log(error);
    });
  }

  // Helper method to get current basket obj value instead of subscribing to obs
  getCurrentBasketValue() {
    return this.basketSource.value;
  }

  addItemToBasket(item: IProduct, quantity = 1) {
    const itemToAdd: IBasketItem = this.mapProductItemToBasketItem(item, quantity);

    const basket = this.getCurrentBasketValue() ?? this.createBasket();
    basket.items = this.AddOrUpdateItem(basket.items, itemToAdd, quantity);

    this.setBasket(basket);
  }

  private AddOrUpdateItem(items: IBasketItem[], item: IBasketItem, quantity: number): IBasketItem[] {
    const itemIndex = items.findIndex(i => i.id === item.id);
    // Not found in basket
    if (itemIndex === -1) {
      item.quantity = quantity;
      items.push(item);
    } else {
      items[itemIndex].quantity += quantity;
    }

    return items;
  }

  private createBasket(): IBasket {
    const basket = new Basket();
    // Some level of persistance
    localStorage.setItem('basket_id', basket.id);
    return basket;
  }

  private mapProductItemToBasketItem(item: IProduct, quantity: number): IBasketItem {
    return {
      id: item.id,
      productName: item.productName,
      price: item.price,
      quantity,
      pictureUrl: item.pictureUrl,
      brand: item.productBrand,
      type: item.productType
    };
  }
}
