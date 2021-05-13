import { NgModule } from '@angular/core';
import { ImdbRoutingModule } from '../imdb/imdb-routing.module';
import { ImdbComponent } from '../imdb/imdb.component';
import { MainModule } from 'src/app/modules/main.module';

@NgModule({
  imports: [
    ImdbRoutingModule,
    MainModule
  ],
  declarations: [
    ImdbComponent
  ]
})
export class ImdbModule { }
