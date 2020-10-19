import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ErrorComponent } from './core/error/error.component';
import { TestErrorComponent } from './core/test-error/test-error.component';
import { HomeComponent } from './home/home.component';

// Lazy loading => Check shop route & shop-routing module
const routes: Routes = [
  {path: '', component: HomeComponent, data: {breadcrumb: 'Home'}},
  {path: 'test-error', component: TestErrorComponent, data: {breadcrumb: 'Test Errors'}},
  {path: 'error', component: ErrorComponent, data: {breadcrumb: 'Server Error'}},
  {path: 'shop', data: {breadcrumb: 'Shop'}, loadChildren: () => import('./shop/shop.module').then(mod => mod.ShopModule)},
  {path: 'basket', data: {breadcrumb: 'Basket'}, loadChildren: () => import('./basket/basket.module').then(mod => mod.BasketModule)},
  {path: 'checkout',
    data: {breadcrumb: 'Checkout'},
    loadChildren: () => import('./checkout/checkout.module').then(mod => mod.CheckoutModule)},
  {path: '**', redirectTo: '', pathMatch: 'full'}, // get them back to the home page, maybe a not-found comp?
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
