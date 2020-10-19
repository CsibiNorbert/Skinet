import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { BasketService } from 'src/app/basket/basket-service.service';
import { IProduct } from 'src/app/shared/models/product.model';
import { BreadcrumbService } from 'xng-breadcrumb';
import { ShopService } from '../services/shop.service';

@Component({
  selector: 'app-product-details',
  templateUrl: './product-details.component.html',
  styleUrls: ['./product-details.component.scss']
})
export class ProductDetailsComponent implements OnInit {
  product: IProduct;
  defaultQuantity = 1;

  /*
    ActivatedRoute is used to get the param from the URL
  */
  constructor(
    private shopService: ShopService,
    private activatedRoute: ActivatedRoute,
    private breadcrumbService: BreadcrumbService,
    private basketService: BasketService) {
      // Before loading the page, set the alias to empty so that it doesn`t show the product ID in the section header
      this.breadcrumbService.set('@productDetails', ' ');
     }

  ngOnInit(): void {
    this.loadProduct();
  }

  loadProduct() {
    // Adding the + sign at the beggining of the string, it casts the string to number
    this.shopService.getProduct(+this.activatedRoute.snapshot.paramMap.get('id')).subscribe(product => {
      this.product = product;
      // Accesing the breadcrumb alias with @ symbol
      this.breadcrumbService.set('@productDetails', product.productName);
    }, error => {
      console.log(error);
    });
  }

  addItemToBasket(){
    this.basketService.addItemToBasket(this.product, this.defaultQuantity);
  }

  incrementOrDecrementQuantity(incremenet: boolean = true){
    if (incremenet) {
      this.defaultQuantity++;
    } else {
      if (this.defaultQuantity > 1) {
        this.defaultQuantity--;
      }
    }
  }
}
