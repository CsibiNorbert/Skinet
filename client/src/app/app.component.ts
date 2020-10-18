import { Component, OnInit } from '@angular/core';
import { BasketService } from './basket/basket-service.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {

  constructor(private basketService: BasketService){}

  ngOnInit(): void {

    const localStorageBasketId = localStorage.getItem('basket_id');
    
    if (localStorageBasketId) {
      this.basketService.getBasket(localStorageBasketId).subscribe(() => {
        console.log('initialised basket');
      }, error => {
        console.log(error);
      });
    }
   
  }

}
