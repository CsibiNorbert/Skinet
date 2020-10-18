import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { Basket, IBasket, IBasketItem, IBasketTotals } from '../shared/models/basket.model';
import { IProduct } from '../shared/models/product.model';

@Injectable({
  providedIn: 'root'
})
export class BasketService {

  baseUrl = environment.baseUrl;
  private basketSource = new BehaviorSubject<IBasket>(null);
  basket$ = this.basketSource.asObservable();

  private basketTotalSource = new BehaviorSubject<IBasketTotals>(null);
  basketTotal$ = this.basketTotalSource.asObservable();

  constructor(private http: HttpClient) { }

  getBasket(id: string){
    return this.http.get(this.baseUrl + 'basket?id=' + id)
            .pipe(
              map((basket: IBasket) => {
                this.basketSource.next(basket);
                this.calculateTotals();
              })
            );
  }

  setBasket(basket: IBasket) {
    return this.http.post(this.baseUrl + 'basket', basket).subscribe((response: IBasket) => {
      this.basketSource.next(response);
      this.calculateTotals();
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

  private calculateTotals() {
    const basket = this.getCurrentBasketValue();
    const shipping = 0;
    // reduce helps when there are more items in an array with multiple quantities
    // b represents the item & we are adding it to a
    // a represents the result we are returning
    // 0 is an initial value for a, we start from 0
    const subtotal = basket.items.reduce((a, b) => (b.price * b.quantity) + a, 0);
    const total = shipping + subtotal;

    this.basketTotalSource.next({
      shipping,
      total,
      subtotal
    });
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
