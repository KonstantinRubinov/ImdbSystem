import { NgModule } from '@angular/core';
import { FavoritesRoutingModule } from '../favorites/favorites-routing.module';
import { FavoritesComponent } from '../favorites/favorites.component';
import { MainModule } from 'src/app/modules/main.module';

@NgModule({
  imports: [
    FavoritesRoutingModule,
    MainModule
  ],
  declarations: [
    FavoritesComponent
  ]
})
export class FavoritesModule { }
