import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CoreComponent } from './core.component';
import { NavBarComponent } from './nav-bar/nav-bar.component';

// Having singletons
@NgModule({
  imports: [
    CommonModule
  ],
  declarations: [
    CoreComponent,
    NavBarComponent
  ],
  exports: [
    NavBarComponent
  ]
})
export class CoreModule { }
