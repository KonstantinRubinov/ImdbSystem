import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MovieCardHorizontalComponent } from '../components/cards/movie-card-horizontal/movie-card-horizontal.component';
import { MovieCardVerticalComponent } from '../components/cards/movie-card-vertical/movie-card-vertical.component';
import { MovieCardFlippingComponent } from '../components/cards/movie-card-flipping/movie-card-flipping.component';

import { FormsModule } from "@angular/forms";
import { RouterModule } from '@angular/router';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    RouterModule
  ],
  declarations: [
    MovieCardVerticalComponent,
    MovieCardHorizontalComponent,
    MovieCardFlippingComponent
  ],
  exports:[
    MovieCardVerticalComponent,
    MovieCardHorizontalComponent,
    MovieCardFlippingComponent,
    CommonModule,
    FormsModule,
    RouterModule
  ]
})
export class MainModule { }
