import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { IPagination } from './shared/models/pagination.model';
import { IProduct } from './shared/models/product.model';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {

  products: IProduct[];
  constructor(private http: HttpClient){}

  ngOnInit(): void {
    this.http.get('https://localhost:44347/api/products').subscribe((response: IPagination) => {
      console.log(response);
      this.products = response.data;
    }, error => {
      console.log(error);
    });
  }

}
