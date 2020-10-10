import { Component, OnInit } from '@angular/core';
import { IBrand } from '../shared/models/brands.model';
import { IType } from '../shared/models/product-type.model';
import { IProduct } from '../shared/models/product.model';
import { ShopService } from './services/shop.service';

@Component({
  selector: 'app-shop',
  templateUrl: './shop.component.html',
  styleUrls: ['./shop.component.scss']
})
export class ShopComponent implements OnInit {
  products: IProduct[];
  brands: IBrand[];
  productTypes: IType[];

  // filtering
  brandIdSelected = 0;
  typeIdSelected = 0;
  // Api doesn`t have pre-defined sortings like the brand & type
  sortSelected = 'name';
  sortOptions = [
    {name: 'Alphabetical', value: 'name'},
    {name: 'Price: Low to High', value: 'priceAsc'},
    {name: 'Price: High to Low', value: 'priceDesc'}
  ];

  constructor(private shopService: ShopService) { }

  ngOnInit(): void {
   this.getProducts();
   this.getBrands();
   this.getTypes();
  }

  getProducts(): void{
    this.shopService.getProducts(this.brandIdSelected, this.typeIdSelected, this.sortSelected).subscribe(response => {
      this.products = response.data;
    }, error => {
      console.log(error);
    });
  }

  getBrands(): void{
    this.shopService.getBrands().subscribe(brands => {
      this.brands = [{id: 0, name: 'All'}, ...brands];
    }, error => {
      console.log(error);
    });
  }

  getTypes(): void{
    this.shopService.getTypes().subscribe(types => {
      this.productTypes = [{id: 0, name: 'All'}, ...types];
    }, error => {
      console.log(error);
    });
  }

  onBrandSelected(brandId: number) {
    this.brandIdSelected = brandId;
    this.getProducts();
  }

  onTypeSelected(typeId: number) {
    this.typeIdSelected = typeId;
    this.getProducts();
  }

  onSortSelected(sortValue: string) {
    this.sortSelected = sortValue;
    this.getProducts();
  }
}
