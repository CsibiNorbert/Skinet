import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-error',
  templateUrl: './error.component.html',
  styleUrls: ['./error.component.scss']
})
export class ErrorComponent implements OnInit {

  error: any;

  constructor(private router: Router) {
    // NavigationExtras is only available in constructors
    // 1 time access via the route ( refreshing the page the error disapears )
    const navigation = this.router.getCurrentNavigation();
    // If the error is present, then we pass it to the error attribute
    this.error = navigation && navigation.extras && navigation.extras.state && navigation.extras.state.error;
   }

  ngOnInit(): void {
  }

}
