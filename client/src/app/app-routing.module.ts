import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ErrorComponent } from './core/error/error.component';
import { TestErrorComponent } from './core/test-error/test-error.component';
import { HomeComponent } from './home/home.component';

// Lazy loading => Check shop route & shop-routing module
const routes: Routes = [
  {path: '', component: HomeComponent},
  {path: 'test-error', component: TestErrorComponent},
  {path: 'error', component: ErrorComponent},
  {path: 'shop', loadChildren: () => import('./shop/shop.module').then(mod => mod.ShopModule)},
  {path: '**', redirectTo: '', pathMatch: 'full'}, // get them back to the home page
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
