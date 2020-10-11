import { Component, Input, OnInit } from '@angular/core';
import { ShopParams } from 'src/app/shop/models/shop-params.model';

@Component({
  selector: 'app-paging-header',
  templateUrl: './paging-header.component.html',
  styleUrls: ['./paging-header.component.scss']
})
export class PagingHeaderComponent implements OnInit {

  @Input() totalCount: number;
  @Input() pageNumber: number;
  @Input() pageSize: number;
  constructor() { }

  ngOnInit() {
  }

}
