import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { IBrand } from '../shared/models/brands.model';
import { IType } from '../shared/models/product-type.model';
import { IProduct } from '../shared/models/product.model';
import { ShopParams } from './models/shop-params.model';
import { ShopService } from './services/shop.service';

@Component({
  selector: 'app-shop',
  templateUrl: './shop.component.html',
  styleUrls: ['./shop.component.scss']
})
export class ShopComponent implements OnInit {

  // When structural directives like *ngIf are in place, then static false.
  // Which means we put conditions and it`s not static
  @ViewChild('search', {static: false}) searchTerm: ElementRef;

  products: IProduct[];
  brands: IBrand[];
  productTypes: IType[];

  // Pagination
  totalCount: number;

  shopParams = new ShopParams();
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
    this.shopService.getProducts(this.shopParams).subscribe(response => {
      this.products = response.data;
      this.shopParams.pageNumber = response.pageIndex;
      this.shopParams.pageSize = response.pageSize;
      this.totalCount = response.count;
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
    this.shopParams.brandId = brandId;
    this.shopParams.pageNumber = 1;
    this.getProducts();
  }

  onTypeSelected(typeId: number) {
    this.shopParams.typeId = typeId;
    this.shopParams.pageNumber = 1;
    this.getProducts();
  }

  onSortSelected(sortValue: string) {
    this.shopParams.sort = sortValue;
    this.getProducts();
  }

  onPageChangedHandler(pageNum: number) {
    if (this.shopParams.pageNumber !== pageNum) {
      this.shopParams.pageNumber = pageNum;
      this.getProducts();
    }
  }

  onSearch(){
    this.shopParams.search = this.searchTerm.nativeElement.value;
    this.shopParams.pageNumber = 1;
    this.getProducts();
  }

  onReset(){
    this.searchTerm.nativeElement.value = '';

    // reset all filters
    this.shopParams = new ShopParams();
    this.getProducts();
  }
}
